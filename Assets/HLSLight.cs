using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace HLS{
	[RequireComponent(typeof(MeshFilter))][RequireComponent(typeof(MeshRenderer))]
	public class HLSLight : MonoBehaviour {
		public float radius = 0f;
		private MeshFilter meshFilter;
		private Mesh _mesh;
		private const float ToDegree = 180f / Mathf.PI;
		private List<HLSHitInfo> hitInfoList = new List<HLSHitInfo>();
		// Use this for initialization
		void Start () {
			meshFilter = GetComponent<MeshFilter> ();
			_mesh = new Mesh ();
			meshFilter.mesh = _mesh;
			if (radius == 0f)
				radius = HLSManager.Instance.rayDis;
		}
		public void Lighting()
		{
			hitInfoList.Clear ();
			SRaycast ();
			CreatMesh ();
		}
		void AddHitInfo(HLSHitInfo hitInfo)
		{
			for (int i = 0; i < hitInfoList.Count; i++) {
				if (hitInfo.angle>hitInfoList[i].angle)
				{
					hitInfoList.Insert (i,hitInfo);
					return;
				}
			}
			hitInfoList.Add (hitInfo);
		}
		void OnEnable()
		{
			Regist ();
		}
		void OnDisable()
		{
			Dispose ();
		}
		void Regist()
		{
			HLSManager.Instance.lightsList.Add (this);
			HLSManager.Instance.LightingEvent += Lighting;
		}
		void Dispose()
		{
			HLSManager.Instance.lightsList.Remove (this);
			HLSManager.Instance.GetPointsEvent -= Lighting;
		}
		// Update is called once per frame
		private void SRaycast()
		{
			RaycastHit2D hit2D;
			Vector2 lightingPos = GetLightingPos();
			float rayDis = radius;
			LayerMask rayLayer = HLSManager.Instance.rayLayer;
			List<Vector2> pointsList = HLSManager.Instance.pointsList;
			for (int i = 0; i < pointsList.Count; i++) {
				Vector2 dir = pointsList[i] - lightingPos;
				Vector2 lightDir;
				lightDir = new Vector2 (dir.x * 0.9998f + dir.y * 0.0175f, -dir.x * 0.0175f + dir.y * 0.9998f);
				hit2D = Physics2D.Raycast(lightingPos,lightDir,rayDis,rayLayer);
				HandleHit(hit2D,lightDir);
				lightDir = dir;
				hit2D = Physics2D.Raycast(lightingPos,lightDir,rayDis,rayLayer);
				HandleHit(hit2D,lightDir);
				lightDir = new Vector2(dir.x*0.9998f-dir.y*0.0175f,dir.x*0.0175f+dir.y*0.9998f);
				hit2D = Physics2D.Raycast(lightingPos,lightDir,rayDis,rayLayer);
				HandleHit(hit2D,lightDir);
			}
		}
		Vector2 GetLightingPos()
		{
			return new Vector2(transform.position.x,transform.position.y);
		}
		private void HandleHit(RaycastHit2D hit2D,Vector2 dir)
		{
			if (hit2D.collider != null) {
				AddHitInfo (new HLSHitInfo (hit2D.point,GetDegree(dir)));
			}
			else
			{
				Vector2 rayVector = dir.normalized*HLSManager.Instance.rayDis;
				AddHitInfo (new HLSHitInfo (new Vector2 (transform.position.x + rayVector.x, transform.position.y + rayVector.y), GetDegree(dir)));
			}
		}
		void CreatMesh()
		{

			Vector3[] verts = new Vector3[hitInfoList.Count+1];
			int triIndex = 0;
			int[] tris = new int[hitInfoList.Count*3];
			verts [0] = Vector3.zero; 
			for (int i = 0; i < hitInfoList.Count; i++) {
				verts [i+1] = transform.InverseTransformPoint(new Vector3 (hitInfoList[i].point.x,hitInfoList[i].point.y,transform.position.z));
			}
			for (int i=1; i<verts.Length; i++) {
				tris [triIndex] = 0;
				tris [triIndex + 1] = i;
				tris [triIndex + 2] = i+1;
				triIndex += 3;
			}
			tris [tris.Length - 3] = 0;
			tris [tris.Length - 2] = verts.Length-1;
			tris [tris.Length - 1] = 1;

			_mesh.Clear ();
			_mesh.vertices = verts;
			_mesh.triangles = tris;
			_mesh.RecalculateNormals ();
		}
		public float GetDegree(Vector2 dir)
		{
			if (dir.y == 0) {
				if (dir.x > 0) {
					return 0f;
				} else {
					return 180f;
				}
			}
			float oppo = dir.y;
			float hy = dir.magnitude;
			float degree = Mathf.Asin (oppo / hy) * ToDegree;
			if (dir.x < 0 && dir.y > 0)
				return 180f - degree;
			if (dir.x <= 0 && dir.y < 0)
				return 180f - degree;
			if (dir.x > 0 && dir.y < 0)
				return 360f + degree;
			return degree;
		}


	}
}
