using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009AE RID: 2478
	[Proxy(typeof(AnimationCurve))]
	public class AnimationCurveProxy : IProxy
	{
		// Token: 0x06004045 RID: 16453 RVA: 0x0002B8D8 File Offset: 0x00029AD8
		[MoonSharpHidden]
		public AnimationCurveProxy(AnimationCurve value)
		{
			this._value = value;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0002B8E7 File Offset: 0x00029AE7
		public AnimationCurveProxy()
		{
			this._value = new AnimationCurve();
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x0002B8FA File Offset: 0x00029AFA
		public int length
		{
			get
			{
				return this._value.length;
			}
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0002B907 File Offset: 0x00029B07
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x0012FC50 File Offset: 0x0012DE50
		[MoonSharpHidden]
		public static AnimationCurveProxy New(AnimationCurve value)
		{
			if (value == null)
			{
				return null;
			}
			AnimationCurveProxy animationCurveProxy = (AnimationCurveProxy)ObjectCache.Get(typeof(AnimationCurveProxy), value);
			if (animationCurveProxy == null)
			{
				animationCurveProxy = new AnimationCurveProxy(value);
				ObjectCache.Add(typeof(AnimationCurveProxy), value, animationCurveProxy);
			}
			return animationCurveProxy;
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0002B90F File Offset: 0x00029B0F
		[MoonSharpUserDataMetamethod("__call")]
		public static AnimationCurveProxy Call(DynValue _)
		{
			return new AnimationCurveProxy();
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0002B916 File Offset: 0x00029B16
		public int AddKey(float time, float value)
		{
			return this._value.AddKey(time, value);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0002B925 File Offset: 0x00029B25
		public static AnimationCurveProxy Constant(float timeStart, float timeEnd, float value)
		{
			return AnimationCurveProxy.New(AnimationCurve.Constant(timeStart, timeEnd, value));
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x0002B934 File Offset: 0x00029B34
		public static AnimationCurveProxy EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			return AnimationCurveProxy.New(AnimationCurve.EaseInOut(timeStart, valueStart, timeEnd, valueEnd));
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0002B944 File Offset: 0x00029B44
		public float Evaluate(float time)
		{
			return this._value.Evaluate(time);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x0002B952 File Offset: 0x00029B52
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x0002B95F File Offset: 0x00029B5F
		public static AnimationCurveProxy Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			return AnimationCurveProxy.New(AnimationCurve.Linear(timeStart, valueStart, timeEnd, valueEnd));
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x0002B96F File Offset: 0x00029B6F
		public void RemoveKey(int index)
		{
			this._value.RemoveKey(index);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x0002B97D File Offset: 0x00029B7D
		public void SmoothTangents(int index, float weight)
		{
			this._value.SmoothTangents(index, weight);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x0002B98C File Offset: 0x00029B8C
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003147 RID: 12615
		[MoonSharpHidden]
		public AnimationCurve _value;
	}
}
