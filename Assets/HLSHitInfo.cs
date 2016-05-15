using UnityEngine;
using System.Collections;

public struct HLSHitInfo {
	public Vector2 point;
	public float angle;
	public HLSHitInfo(Vector2 point,float angle)
	{
		this.point = point;
		this.angle = angle;
	}
}
