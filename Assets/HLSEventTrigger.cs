using UnityEngine;
using System.Collections;
namespace HLS{
	public class HLSEventTrigger : MonoBehaviour {
		private Transform _trans;
		public delegate void LightingEventHandler();
		private LightingEventHandler _lightingEnterDelegate;
		private LightingEventHandler _lightingExitDelegate;
		private bool _isLighting = false;
		void Start()
		{
			_trans = transform;
		}
		public void SetCallBack(LightingEventHandler enter,LightingEventHandler exit)
		{
			_lightingEnterDelegate = new LightingEventHandler (enter);
			_lightingExitDelegate = new LightingEventHandler (exit);
		}
		public void OnLightingEnter()
		{
			_lightingEnterDelegate ();
		}
		public void OnLightingExit()
		{
			_lightingExitDelegate ();
		}
		public void LightingCheck()
		{
			bool isLighting = false;
			for (int i = 0; i < HLSManager.Instance.lightsList.Count; i++) {
				if (Check (new Vector2 (HLSManager.Instance.lightsList [i].transform.position.x, HLSManager.Instance.lightsList [i].transform.position.y), HLSManager.Instance.lightsList [i].radius)) {
					isLighting = true;
					break;
				}
			}
			if (isLighting == _isLighting)
				return;
			_isLighting = isLighting;
			if (_isLighting) {
				OnLightingEnter ();
			} else {
				OnLightingExit ();
			}
		}
		public bool Check(Vector2 source,float radius)
		{
			Vector2 dir = new Vector2 (_trans.position.x - source.x, _trans.position.y - source.y);
			if (dir.magnitude > radius)
				return false;
			RaycastHit2D hit = Physics2D.Raycast(source,dir,dir.magnitude,HLSManager.Instance.rayLayer);
			if (hit.collider == null)
				return true;
			return false;
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
			HLSManager.Instance.TriggerEvent += LightingCheck;
		}
		void Dispose()
		{
			HLSManager.Instance.TriggerEvent -= LightingCheck;
		}
	}
}