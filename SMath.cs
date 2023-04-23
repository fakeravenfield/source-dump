using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000308 RID: 776
public static class SMath
{
	// Token: 0x0600142D RID: 5165 RVA: 0x00010100 File Offset: 0x0000E300
	public static Vector3 LineVsPointClosest(Vector3 origin, Vector3 direction, Vector3 point)
	{
		return Vector3.Project(point - origin, direction) + origin;
	}

	// Token: 0x0600142E RID: 5166 RVA: 0x00010115 File Offset: 0x0000E315
	public static float LineVsPointClosestT(Vector3 origin, Vector3 direction, Vector3 point)
	{
		return SMath.ProjectScalar(point - origin, direction);
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x00095870 File Offset: 0x00093A70
	public static Vector3 LineSegmentVsPointClosest(Vector3 a, Vector3 b, Vector3 point)
	{
		Vector3 vector = b - a;
		return a + Mathf.Clamp01(SMath.LineVsPointClosestT(a, vector, point)) * vector;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x00010124 File Offset: 0x0000E324
	public static float LineSegmentVsPointClosestT(Vector3 a, Vector3 b, Vector3 point)
	{
		return Mathf.Clamp01(SMath.LineVsPointClosestT(a, b - a, point));
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x00010139 File Offset: 0x0000E339
	public static float ProjectScalar(Vector3 a, Vector3 onto)
	{
		return Vector3.Dot(a, onto) / onto.sqrMagnitude;
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x0001014A File Offset: 0x0000E34A
	public static float BearingRadian(Vector3 delta)
	{
		return Mathf.Atan2(delta.z, delta.x);
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x0001015D File Offset: 0x0000E35D
	public static float Bearing(Vector3 delta)
	{
		return SMath.BearingRadian(delta) * 57.29578f;
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0001016B File Offset: 0x0000E36B
	public static Quaternion LookRotationRespectUp(Vector3 forward, Vector3 up)
	{
		return Quaternion.LookRotation(up, forward) * Quaternion.Euler(0f, 0f, 180f) * Quaternion.Euler(90f, 0f, 0f);
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000101A6 File Offset: 0x0000E3A6
	public static float Damp(float source, float target, float smoothing, float dt)
	{
		return Mathf.Lerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000101BC File Offset: 0x0000E3BC
	public static Vector3 Damp(Vector3 source, Vector3 target, float smoothing, float dt)
	{
		return Vector3.Lerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000101D2 File Offset: 0x0000E3D2
	public static Quaternion DampLerp(Quaternion source, Quaternion target, float smoothing, float dt)
	{
		return Quaternion.Lerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000101E8 File Offset: 0x0000E3E8
	public static Quaternion DampSlerp(Quaternion source, Quaternion target, float smoothing, float dt)
	{
		return Quaternion.Slerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x000101FE File Offset: 0x0000E3FE
	public static Quaternion DeltaRotation(Quaternion from, Quaternion to)
	{
		return to * Quaternion.Inverse(from);
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x000958A0 File Offset: 0x00093AA0
	public static Vector3 Median(List<Vector3> points)
	{
		List<float> list = new List<float>();
		List<float> list2 = new List<float>();
		List<float> list3 = new List<float>();
		foreach (Vector3 vector in points)
		{
			list.Add(vector.x);
			list2.Add(vector.y);
			list3.Add(vector.z);
		}
		list.Sort();
		list2.Sort();
		list3.Sort();
		int index = points.Count / 2;
		return new Vector3(list[index], list2[index], list3[index]);
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x0001020C File Offset: 0x0000E40C
	public static float SignedAngleBetween(Vector3 v1, Vector3 v2, Vector3 axis)
	{
		return Mathf.Atan2(Vector3.Dot(axis, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x00095958 File Offset: 0x00093B58
	public static Bounds TransformBounds(Bounds local, Matrix4x4 localToWorld)
	{
		Bounds result = new Bounds(localToWorld.MultiplyPoint(local.min), Vector3.zero);
		result.Encapsulate(localToWorld.MultiplyPoint(new Vector3(local.min.x, local.min.y, local.max.z)));
		result.Encapsulate(localToWorld.MultiplyPoint(new Vector3(local.max.x, local.min.y, local.max.z)));
		result.Encapsulate(localToWorld.MultiplyPoint(new Vector3(local.max.x, local.min.y, local.min.z)));
		result.Encapsulate(localToWorld.MultiplyPoint(new Vector3(local.min.x, local.max.y, local.min.z)));
		result.Encapsulate(localToWorld.MultiplyPoint(new Vector3(local.min.x, local.max.y, local.max.z)));
		result.Encapsulate(localToWorld.MultiplyPoint(local.max));
		result.Encapsulate(localToWorld.MultiplyPoint(new Vector3(local.max.x, local.max.y, local.min.z)));
		return result;
	}

	// Token: 0x02000309 RID: 777
	public static class V2D
	{
		// Token: 0x0600143D RID: 5181 RVA: 0x00095AE0 File Offset: 0x00093CE0
		public static bool LineSegementsIntersect(Vector2 p, Vector2 p2, Vector2 q, Vector2 q2, out Vector2 intersection)
		{
			intersection = default(Vector2);
			Vector2 vector = p2 - p;
			Vector2 v = q2 - q;
			float num = vector.Cross(v);
			float a = (q - p).Cross(vector);
			if (Mathf.Approximately(num, 0f) && Mathf.Approximately(a, 0f))
			{
				return false;
			}
			if (Mathf.Approximately(num, 0f) && !Mathf.Approximately(a, 0f))
			{
				return false;
			}
			float num2 = (q - p).Cross(v) / num;
			float num3 = (q - p).Cross(vector) / num;
			if (!Mathf.Approximately(num, 0f) && 0f <= num2 && num2 <= 1f && 0f <= num3 && num3 <= 1f)
			{
				intersection = p + num2 * vector;
				return true;
			}
			return false;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00095BC0 File Offset: 0x00093DC0
		public static float RadiansFromTo(float origin, float target)
		{
			float num = Mathf.DeltaAngle(origin, target);
			if (num < 0f)
			{
				num += 6.2831855f;
			}
			return num;
		}
	}
}
