using UnityEngine;
using System.Collections;
namespace HLS{
public class HLSPolygonCollider : HLSColliderBase {

	private PolygonCollider2D _collider;
	public HLSPolygonCollider(Transform trans):base(trans)
	{
		_collider = trans.GetComponent<PolygonCollider2D> ();
	}
	public override void GetPoints ()
	{
		base.GetPoints ();
			if (!CheckIfInScreen ())
				return;
		int pointsCount = _collider.GetTotalPointCount ();
		for (int i = 0; i < pointsCount; i++) {
			HLSManager.Instance.pointsList.Add (_transform.TransformPoint(_collider.points[i]));
		}
	}
	public override bool CheckIfInScreen ()
	{
		return base.CheckIfInScreen ();
	}
}
}