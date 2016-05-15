using UnityEngine;
using System.Collections;
namespace HLS{
	public class HLSEdgeCollider : HLSColliderBase {
		private EdgeCollider2D _collider;
		public HLSEdgeCollider(Transform trans):base(trans)
		{
			_collider = trans.GetComponent<EdgeCollider2D> ();
		}
		public override void GetPoints ()
		{
			base.GetPoints ();
			if (!CheckIfInScreen ())
				return;
			for (int i = 1; i < _collider.pointCount; i++) {
				HLSManager.Instance.pointsList.Add (_transform.TransformPoint(_collider.points[i]));
			}
		}
		public override bool CheckIfInScreen ()
		{
			return base.CheckIfInScreen ();
		}
	}
}