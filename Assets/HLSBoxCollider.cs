using UnityEngine;
using System.Collections;
namespace HLS{
	public class HLSBoxCollider : HLSColliderBase {

		private BoxCollider2D _collider;
		public HLSBoxCollider(Transform trans):base(trans)
		{
			_collider = trans.GetComponent<BoxCollider2D> ();
		}
		public override void GetPoints ()
		{
			base.GetPoints ();
			if (!CheckIfInScreen ())
				return;
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(_collider.size.x/2f+_collider.offset.x,_collider.size.y/2f+_collider.offset.y)));
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(-_collider.size.x/2f+_collider.offset.x,_collider.size.y/2f+_collider.offset.y)));
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(_collider.size.x/2f+_collider.offset.x,-_collider.size.y/2f+_collider.offset.y)));
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(new Vector2(-_collider.size.x/2f+_collider.offset.x,-_collider.size.y/2f+_collider.offset.y)));
		}
		public override bool CheckIfInScreen ()
		{
			return base.CheckIfInScreen ();
		}
	}
}
