using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009E2 RID: 2530
	[Proxy(typeof(UnityEngine.Plane))]
	public class PlaneProxy : IProxy
	{
		// Token: 0x06004A71 RID: 19057 RVA: 0x00034A43 File Offset: 0x00032C43
		[MoonSharpHidden]
		public PlaneProxy(UnityEngine.Plane value)
		{
			this._value = value;
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00034A52 File Offset: 0x00032C52
		public PlaneProxy(Vector3Proxy inNormal, Vector3Proxy inPoint)
		{
			if (inNormal == null)
			{
				throw new ScriptRuntimeException("argument 'inNormal' is nil");
			}
			if (inPoint == null)
			{
				throw new ScriptRuntimeException("argument 'inPoint' is nil");
			}
			this._value = new UnityEngine.Plane(inNormal._value, inPoint._value);
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00034A8D File Offset: 0x00032C8D
		public PlaneProxy(Vector3Proxy inNormal, float d)
		{
			if (inNormal == null)
			{
				throw new ScriptRuntimeException("argument 'inNormal' is nil");
			}
			this._value = new UnityEngine.Plane(inNormal._value, d);
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00131B80 File Offset: 0x0012FD80
		public PlaneProxy(Vector3Proxy a, Vector3Proxy b, Vector3Proxy c)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			if (c == null)
			{
				throw new ScriptRuntimeException("argument 'c' is nil");
			}
			this._value = new UnityEngine.Plane(a._value, b._value, c._value);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00034AB5 File Offset: 0x00032CB5
		public PlaneProxy()
		{
			this._value = default(UnityEngine.Plane);
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x00034AC9 File Offset: 0x00032CC9
		// (set) Token: 0x06004A77 RID: 19063 RVA: 0x00034AD6 File Offset: 0x00032CD6
		public float distance
		{
			get
			{
				return this._value.distance;
			}
			set
			{
				this._value.distance = value;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x00034AE4 File Offset: 0x00032CE4
		public PlaneProxy flipped
		{
			get
			{
				return PlaneProxy.New(this._value.flipped);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x00034AF6 File Offset: 0x00032CF6
		// (set) Token: 0x06004A7A RID: 19066 RVA: 0x00034B08 File Offset: 0x00032D08
		public Vector3Proxy normal
		{
			get
			{
				return Vector3Proxy.New(this._value.normal);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.normal = value._value;
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00034B29 File Offset: 0x00032D29
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00034B36 File Offset: 0x00032D36
		[MoonSharpHidden]
		public static PlaneProxy New(UnityEngine.Plane value)
		{
			return new PlaneProxy(value);
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00034B3E File Offset: 0x00032D3E
		[MoonSharpUserDataMetamethod("__call")]
		public static PlaneProxy Call(DynValue _, Vector3Proxy inNormal, Vector3Proxy inPoint)
		{
			return new PlaneProxy(inNormal, inPoint);
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00034B47 File Offset: 0x00032D47
		[MoonSharpUserDataMetamethod("__call")]
		public static PlaneProxy Call(DynValue _, Vector3Proxy inNormal, float d)
		{
			return new PlaneProxy(inNormal, d);
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00034B50 File Offset: 0x00032D50
		[MoonSharpUserDataMetamethod("__call")]
		public static PlaneProxy Call(DynValue _, Vector3Proxy a, Vector3Proxy b, Vector3Proxy c)
		{
			return new PlaneProxy(a, b, c);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00034B5A File Offset: 0x00032D5A
		[MoonSharpUserDataMetamethod("__call")]
		public static PlaneProxy Call(DynValue _)
		{
			return new PlaneProxy();
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00034B61 File Offset: 0x00032D61
		public Vector3Proxy ClosestPointOnPlane(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPointOnPlane(point._value));
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00034B87 File Offset: 0x00032D87
		public void Flip()
		{
			this._value.Flip();
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00034B94 File Offset: 0x00032D94
		public float GetDistanceToPoint(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.GetDistanceToPoint(point._value);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x00034BB5 File Offset: 0x00032DB5
		public bool GetSide(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.GetSide(point._value);
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00034BD6 File Offset: 0x00032DD6
		public bool Raycast(RayProxy ray, out float enter)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			enter = 0f;
			return this._value.Raycast(ray._value, out enter);
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x00034BFF File Offset: 0x00032DFF
		public bool SameSide(Vector3Proxy inPt0, Vector3Proxy inPt1)
		{
			if (inPt0 == null)
			{
				throw new ScriptRuntimeException("argument 'inPt0' is nil");
			}
			if (inPt1 == null)
			{
				throw new ScriptRuntimeException("argument 'inPt1' is nil");
			}
			return this._value.SameSide(inPt0._value, inPt1._value);
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00131BDC File Offset: 0x0012FDDC
		public void Set3Points(Vector3Proxy a, Vector3Proxy b, Vector3Proxy c)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			if (c == null)
			{
				throw new ScriptRuntimeException("argument 'c' is nil");
			}
			this._value.Set3Points(a._value, b._value, c._value);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00034C34 File Offset: 0x00032E34
		public void SetNormalAndPosition(Vector3Proxy inNormal, Vector3Proxy inPoint)
		{
			if (inNormal == null)
			{
				throw new ScriptRuntimeException("argument 'inNormal' is nil");
			}
			if (inPoint == null)
			{
				throw new ScriptRuntimeException("argument 'inPoint' is nil");
			}
			this._value.SetNormalAndPosition(inNormal._value, inPoint._value);
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x00034C69 File Offset: 0x00032E69
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x00034C7C File Offset: 0x00032E7C
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x00034C8A File Offset: 0x00032E8A
		public void Translate(Vector3Proxy translation)
		{
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			this._value.Translate(translation._value);
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00034CAB File Offset: 0x00032EAB
		public static PlaneProxy Translate(PlaneProxy plane, Vector3Proxy translation)
		{
			if (plane == null)
			{
				throw new ScriptRuntimeException("argument 'plane' is nil");
			}
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			return PlaneProxy.New(UnityEngine.Plane.Translate(plane._value, translation._value));
		}

		// Token: 0x0400317A RID: 12666
		[MoonSharpHidden]
		public UnityEngine.Plane _value;
	}
}
