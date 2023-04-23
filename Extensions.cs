using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public static class Extensions
{
	// Token: 0x06001401 RID: 5121 RVA: 0x0000FF10 File Offset: 0x0000E110
	public static float ProjectionScalar(this Vector3 vector, Vector3 onto)
	{
		return Vector3.Dot(vector, onto) / Vector3.Dot(vector, onto);
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x0000FF21 File Offset: 0x0000E121
	public static Vector3 ToGround(this Vector3 v)
	{
		v.y = 0f;
		return v;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0000FF30 File Offset: 0x0000E130
	public static Vector3 ToLocalZGround(this Vector3 v)
	{
		v.z = 0f;
		return v;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x0000FF3F File Offset: 0x0000E13F
	public static Vector2 ToVector2XZ(this Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x0000FF52 File Offset: 0x0000E152
	public static Vector3 ToVector3XZ(this Vector2 v)
	{
		return new Vector3(v.x, 0f, v.y);
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x0000FF6A File Offset: 0x0000E16A
	public static float Cross(this Vector2 v, Vector2 v2)
	{
		return v.x * v2.y - v.y * v2.x;
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0000FF87 File Offset: 0x0000E187
	public static float AtanAngle(this Vector2 v)
	{
		return Mathf.Atan2(v.y, v.x);
	}
}
