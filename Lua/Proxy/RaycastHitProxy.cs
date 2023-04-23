using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009EA RID: 2538
	[Proxy(typeof(RaycastHit))]
	public class RaycastHitProxy : IProxy
	{
		// Token: 0x06004B60 RID: 19296 RVA: 0x00035C07 File Offset: 0x00033E07
		[MoonSharpHidden]
		public RaycastHitProxy(RaycastHit value)
		{
			this._value = value;
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x00035C16 File Offset: 0x00033E16
		public RaycastHitProxy()
		{
			this._value = default(RaycastHit);
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x00035C2A File Offset: 0x00033E2A
		// (set) Token: 0x06004B63 RID: 19299 RVA: 0x00035C3C File Offset: 0x00033E3C
		public Vector3Proxy barycentricCoordinate
		{
			get
			{
				return Vector3Proxy.New(this._value.barycentricCoordinate);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.barycentricCoordinate = value._value;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x00035C5D File Offset: 0x00033E5D
		public ColliderProxy collider
		{
			get
			{
				return ColliderProxy.New(this._value.collider);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x00035C6F File Offset: 0x00033E6F
		// (set) Token: 0x06004B66 RID: 19302 RVA: 0x00035C7C File Offset: 0x00033E7C
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

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x00035C8A File Offset: 0x00033E8A
		public Vector2Proxy lightmapCoord
		{
			get
			{
				return Vector2Proxy.New(this._value.lightmapCoord);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06004B68 RID: 19304 RVA: 0x00035C9C File Offset: 0x00033E9C
		// (set) Token: 0x06004B69 RID: 19305 RVA: 0x00035CAE File Offset: 0x00033EAE
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

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x00035CCF File Offset: 0x00033ECF
		// (set) Token: 0x06004B6B RID: 19307 RVA: 0x00035CE1 File Offset: 0x00033EE1
		public Vector3Proxy point
		{
			get
			{
				return Vector3Proxy.New(this._value.point);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.point = value._value;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06004B6C RID: 19308 RVA: 0x00035D02 File Offset: 0x00033F02
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(this._value.rigidbody);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06004B6D RID: 19309 RVA: 0x00035D14 File Offset: 0x00033F14
		public Vector2Proxy textureCoord
		{
			get
			{
				return Vector2Proxy.New(this._value.textureCoord);
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x00035D26 File Offset: 0x00033F26
		public Vector2Proxy textureCoord2
		{
			get
			{
				return Vector2Proxy.New(this._value.textureCoord2);
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x00035D38 File Offset: 0x00033F38
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x00035D4A File Offset: 0x00033F4A
		public int triangleIndex
		{
			get
			{
				return this._value.triangleIndex;
			}
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00035D57 File Offset: 0x00033F57
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00035D64 File Offset: 0x00033F64
		[MoonSharpHidden]
		public static RaycastHitProxy New(RaycastHit value)
		{
			return new RaycastHitProxy(value);
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00035D6C File Offset: 0x00033F6C
		[MoonSharpUserDataMetamethod("__call")]
		public static RaycastHitProxy Call(DynValue _)
		{
			return new RaycastHitProxy();
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00035D73 File Offset: 0x00033F73
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003181 RID: 12673
		[MoonSharpHidden]
		public RaycastHit _value;
	}
}
