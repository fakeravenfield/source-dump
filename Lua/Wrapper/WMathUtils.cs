using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000972 RID: 2418
	[Name("MathUtils")]
	[Doc("This class provides some math functionality that is useful for 3d maths")]
	public static class WMathUtils
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x00029AC7 File Offset: 0x00027CC7
		[Doc("Given a line L and point P, get the point on L that is closest to P.[..]")]
		public static Vector3 LineVsPointClosest(Vector3 lineOrigin, Vector3 lineDirection, Vector3 point)
		{
			return SMath.LineVsPointClosest(lineOrigin, lineDirection, point);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00029AD1 File Offset: 0x00027CD1
		[Doc("Given a line L and point P, get the point on L that is closest to P.[..] Returns the point as a value T, where point = origin(L) + direction(L) * T")]
		public static float LineVsPointClosestT(Vector3 lineOrigin, Vector3 lineDirection, Vector3 point)
		{
			return SMath.LineVsPointClosestT(lineOrigin, lineDirection, point);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x00029ADB File Offset: 0x00027CDB
		[Doc("Given a line segment S and point P, get the point on S that is closest to P.[..]")]
		public static Vector3 LineSegmentVsPointClosest(Vector3 segmentStart, Vector3 segmentEnd, Vector3 point)
		{
			return SMath.LineSegmentVsPointClosest(segmentStart, segmentEnd, point);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x00029AE5 File Offset: 0x00027CE5
		[Doc("Given a line segment S and point P, get the point on S that is closest to P.[..] Returns the point as a value T, where point = Lerp(start(S), end(S), T)")]
		public static float LineSegmentVsPointClosestT(Vector3 segmentStart, Vector3 segmentEnd, Vector3 point)
		{
			return SMath.LineSegmentVsPointClosestT(segmentStart, segmentEnd, point);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00029AEF File Offset: 0x00027CEF
		[Doc("Returns the look rotation like Quaternion.LookRotation(), but with the up vector being fully constrained instead of the forward vector.")]
		public static Quaternion LookRotationConstrainUp(Vector3 forward, Vector3 up)
		{
			return SMath.LookRotationRespectUp(forward, up);
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x00029AF8 File Offset: 0x00027CF8
		[Doc("A frame rate independent alternative to damping using Mathf.Lerp()[..]")]
		public static float Damp(float current, float target, float smoothing, float deltaTime)
		{
			return SMath.Damp(current, target, smoothing, deltaTime);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00029B03 File Offset: 0x00027D03
		[Doc("A frame rate independent alternative to damping using Vector3.Lerp()")]
		public static Vector3 Damp(Vector3 current, Vector3 target, float smoothing, float deltaTime)
		{
			return SMath.Damp(current, target, smoothing, deltaTime);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00029B0E File Offset: 0x00027D0E
		[Doc("A frame rate independent alternative to damping using Quaternion.Lerp()")]
		public static Quaternion DampLinear(Quaternion current, Quaternion target, float smoothing, float deltaTime)
		{
			return SMath.DampLerp(current, target, smoothing, deltaTime);
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00029B19 File Offset: 0x00027D19
		[Doc("A frame rate independent alternative to damping using Quaternion.Slerp()")]
		public static Quaternion DampSpherical(Quaternion current, Quaternion target, float smoothing, float deltaTime)
		{
			return SMath.DampSlerp(current, target, smoothing, deltaTime);
		}
	}
}
