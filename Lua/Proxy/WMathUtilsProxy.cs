using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A1D RID: 2589
	[Proxy(typeof(WMathUtils))]
	public class WMathUtilsProxy : IProxy
	{
		// Token: 0x0600526B RID: 21099 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0003CC4B File Offset: 0x0003AE4B
		public static float Damp(float current, float target, float smoothing, float deltaTime)
		{
			return WMathUtils.Damp(current, target, smoothing, deltaTime);
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x0003CC56 File Offset: 0x0003AE56
		public static Vector3Proxy Damp(Vector3Proxy current, Vector3Proxy target, float smoothing, float deltaTime)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return Vector3Proxy.New(WMathUtils.Damp(current._value, target._value, smoothing, deltaTime));
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		public static QuaternionProxy DampLinear(QuaternionProxy current, QuaternionProxy target, float smoothing, float deltaTime)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return QuaternionProxy.New(WMathUtils.DampLinear(current._value, target._value, smoothing, deltaTime));
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x0003CCC2 File Offset: 0x0003AEC2
		public static QuaternionProxy DampSpherical(QuaternionProxy current, QuaternionProxy target, float smoothing, float deltaTime)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return QuaternionProxy.New(WMathUtils.DampSpherical(current._value, target._value, smoothing, deltaTime));
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x001391C4 File Offset: 0x001373C4
		public static Vector3Proxy LineSegmentVsPointClosest(Vector3Proxy segmentStart, Vector3Proxy segmentEnd, Vector3Proxy point)
		{
			if (segmentStart == null)
			{
				throw new ScriptRuntimeException("argument 'segmentStart' is nil");
			}
			if (segmentEnd == null)
			{
				throw new ScriptRuntimeException("argument 'segmentEnd' is nil");
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(WMathUtils.LineSegmentVsPointClosest(segmentStart._value, segmentEnd._value, point._value));
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00139218 File Offset: 0x00137418
		public static float LineSegmentVsPointClosestT(Vector3Proxy segmentStart, Vector3Proxy segmentEnd, Vector3Proxy point)
		{
			if (segmentStart == null)
			{
				throw new ScriptRuntimeException("argument 'segmentStart' is nil");
			}
			if (segmentEnd == null)
			{
				throw new ScriptRuntimeException("argument 'segmentEnd' is nil");
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return WMathUtils.LineSegmentVsPointClosestT(segmentStart._value, segmentEnd._value, point._value);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x00139268 File Offset: 0x00137468
		public static Vector3Proxy LineVsPointClosest(Vector3Proxy lineOrigin, Vector3Proxy lineDirection, Vector3Proxy point)
		{
			if (lineOrigin == null)
			{
				throw new ScriptRuntimeException("argument 'lineOrigin' is nil");
			}
			if (lineDirection == null)
			{
				throw new ScriptRuntimeException("argument 'lineDirection' is nil");
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(WMathUtils.LineVsPointClosest(lineOrigin._value, lineDirection._value, point._value));
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x001392BC File Offset: 0x001374BC
		public static float LineVsPointClosestT(Vector3Proxy lineOrigin, Vector3Proxy lineDirection, Vector3Proxy point)
		{
			if (lineOrigin == null)
			{
				throw new ScriptRuntimeException("argument 'lineOrigin' is nil");
			}
			if (lineDirection == null)
			{
				throw new ScriptRuntimeException("argument 'lineDirection' is nil");
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return WMathUtils.LineVsPointClosestT(lineOrigin._value, lineDirection._value, point._value);
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
		public static QuaternionProxy LookRotationConstrainUp(Vector3Proxy forward, Vector3Proxy up)
		{
			if (forward == null)
			{
				throw new ScriptRuntimeException("argument 'forward' is nil");
			}
			if (up == null)
			{
				throw new ScriptRuntimeException("argument 'up' is nil");
			}
			return QuaternionProxy.New(WMathUtils.LookRotationConstrainUp(forward._value, up._value));
		}
	}
}
