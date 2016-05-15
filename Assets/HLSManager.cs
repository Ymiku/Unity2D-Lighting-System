using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace HLS{
	public class HLSManager : UnitySingleton<HLSManager> {
		public Camera mainCamera;
		public float rayDis = 10f;
		public LayerMask rayLayer;
		public delegate void HLSVoidEventHandler();
		public event HLSVoidEventHandler GetPointsEvent;
		public event HLSVoidEventHandler LightingEvent;
		public event HLSVoidEventHandler TriggerEvent;
		[HideInInspector]
		public List<Vector2> pointsList = new List<Vector2>();
		[HideInInspector]
		public List<HLSLight> lightsList = new List<HLSLight>();
		// Use this for initialization
		public void GetPoints()
		{
			pointsList.Clear ();
			if(GetPointsEvent!=null)
			GetPointsEvent();
		}
		public void Lighting()
		{
			if(LightingEvent!=null)
			LightingEvent ();
		}
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {
			GetPoints ();
			Lighting ();
			if(TriggerEvent!=null)
			TriggerEvent ();
		}


	}
}