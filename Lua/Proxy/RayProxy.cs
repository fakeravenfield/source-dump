using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009E9 RID: 2537
	[Proxy(typeof(Ray))]
	public class RayProxy : IProxy
	{
		// Token: 0x06004B52 RID: 19282 RVA: 0x00035AEA File Offset: 0x00033CEA
		[MoonSharpHidden]
		public RayProxy(Ray value)
		{
			this._value = value;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00035AF9 File Offset: 0x00033CF9
		public RayProxy(Vector3Proxy origin, Vector3Proxy direction)
		{
			if (origin == null)
			{
				throw new ScriptRuntimeException("argument 'origin' is nil");
			}
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			this._value = new Ray(origin._value, direction._value);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00035B34 File Offset: 0x00033D34
		public RayProxy()
		{
			this._value = default(Ray);
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x00035B48 File Offset: 0x00033D48
		// (set) Token: 0x06004B56 RID: 19286 RVA: 0x00035B5A File Offset: 0x00033D5A
		public Vector3Proxy direction
		{
			get
			{
				return Vector3Proxy.New(this._value.direction);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.direction = value._value;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06004B57 RID: 19287 RVA: 0x00035B7B File Offset: 0x00033D7B
		// (set) Token: 0x06004B58 RID: 19288 RVA: 0x00035B8D File Offset: 0x00033D8D
		public Vector3Proxy origin
		{
			get
			{
				return Vector3Proxy.New(this._value.origin);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.origin = value._value;
			}
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x00035BAE File Offset: 0x00033DAE
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00035BBB File Offset: 0x00033DBB
		[MoonSharpHidden]
		public static RayProxy New(Ray value)
		{
			return new RayProxy(value);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00035BC3 File Offset: 0x00033DC3
		[MoonSharpUserDataMetamethod("__call")]
		public static RayProxy Call(DynValue _, Vector3Proxy origin, Vector3Proxy direction)
		{
			return new RayProxy(origin, direction);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00035BCC File Offset: 0x00033DCC
		[MoonSharpUserDataMetamethod("__call")]
		public static RayProxy Call(DynValue _)
		{
			return new RayProxy();
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x00035BD3 File Offset: 0x00033DD3
		public Vector3Proxy GetPoint(float distance)
		{
			return Vector3Proxy.New(this._value.GetPoint(distance));
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x00035BE6 File Offset: 0x00033DE6
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00035BF9 File Offset: 0x00033DF9
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x04003180 RID: 12672
		[MoonSharpHidden]
		public Ray _value;
	}
}
