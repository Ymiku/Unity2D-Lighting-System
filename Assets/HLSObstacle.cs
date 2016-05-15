using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace HLS{
	[RequireComponent(typeof(Collider2D))][RequireComponent(typeof(SpriteRenderer))]
	public class HLSObstacle : MonoBehaviour {
		private IGetPointsable _thisColliderable;
		// Use this for initialization
		void Start () {
			Collider2D collider = GetComponent<Collider2D> ();
			if (collider is PolygonCollider2D) {
				_thisColliderable = new HLSPolygonCollider (transform);
				return;
			}
			if (collider is EdgeCollider2D) {
				_thisColliderable = new HLSEdgeCollider (transform);
				return;
			}
			if (collider is BoxCollider2D) {
				_thisColliderable = new HLSBoxCollider (transform);
				return;
			}
			if (collider is CircleCollider2D) {
				_thisColliderable = new HLSCircleCollider (transform);
				return;
			}
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
			HLSManager.Instance.GetPointsEvent += GetPoints;
		}
		void Dispose()
		{
			HLSManager.Instance.GetPointsEvent -= GetPoints;
		}
			
		void GetPoints()
		{
			_thisColliderable.GetPoints ();
		}
	}
}
