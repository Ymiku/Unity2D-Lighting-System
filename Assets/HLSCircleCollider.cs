using UnityEngine;
using System.Collections;
namespace HLS{
	public class HLSCircleCollider : HLSColliderBase {

		private CircleCollider2D _collider;
		public HLSCircleCollider(Transform trans):base(trans)
		{
			_collider = trans.GetComponent<CircleCollider2D> ();
		}
		public override void GetPoints ()
		{
			base.GetPoints ();
			if (!CheckIfInScreen ())
				return;
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(-_collider.radius+_collider.offset.x,_collider.offset.y)));
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(_collider.radius+_collider.offset.x,_collider.offset.y)));
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(_collider.offset.x,_collider.radius+_collider.offset.y)));
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(_collider.offset.x,-_collider.radius+_collider.offset.y)));
		}
		public override bool CheckIfInScreen ()
		{
			return base.CheckIfInScreen ();
		}
	}
}