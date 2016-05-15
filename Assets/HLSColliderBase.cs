using UnityEngine;
using System.Collections;
namespace HLS{
	public interface IGetPointsable{
		void GetPoints ();
	}
	public class HLSColliderBase:IGetPointsable {
		protected Transform _transform;
		protected float _offScreenDistanceX;
		protected float _offScreenDistanceY;
		public HLSColliderBase(Transform trans)
		{
			_transform = trans;
			_offScreenDistanceX = trans.GetComponent<SpriteRenderer>().bounds.size.x;
			_offScreenDistanceY = trans.GetComponent<SpriteRenderer>().bounds.size.y;
		}
		public virtual void GetPoints()
		{
			
		}
		public virtual bool CheckIfInScreen()
		{
			float tx = _transform.position.x;
			float ty = _transform.position.y;
			Vector3 pos = HLSManager.Instance.mainCamera.transform.position;
			if (tx > pos.x) {
				tx -= _offScreenDistanceX;
				if (tx < pos.x)
					tx = pos.x;
			} else {
				tx += _offScreenDistanceX;
				if (tx > pos.x)
					tx = pos.x;
			}
			if (ty > pos.y) {
				ty -= _offScreenDistanceY;
				if (ty < pos.y)
					ty = pos.y;
			} else {
				ty += _offScreenDistanceY;
				if (ty > pos.y)
					ty = pos.y;
			}
			pos = HLSManager.Instance.mainCamera.WorldToViewportPoint (new Vector3 (tx, ty, _transform.position.z));
			if (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f)
				return true;
			return false;
		}
	}
}