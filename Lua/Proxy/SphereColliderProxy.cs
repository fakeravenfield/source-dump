using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A04 RID: 2564
	[Proxy(typeof(SphereCollider))]
	public class SphereColliderProxy : IProxy
	{
		// Token: 0x06004F4D RID: 20301 RVA: 0x00039892 File Offset: 0x00037A92
		[MoonSharpHidden]
		public SphereColliderProxy(SphereCollider value)
		{
			this._value = value;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x000398A1 File Offset: 0x00037AA1
		public SphereColliderProxy()
		{
			this._value = new SphereCollider();
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x000398B4 File Offset: 0x00037AB4
		// (set) Token: 0x06004F50 RID: 20304 RVA: 0x000398C6 File Offset: 0x00037AC6
		public Vector3Proxy center
		{
			get
			{
				return Vector3Proxy.New(this._value.center);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.center = value._value;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x000398E7 File Offset: 0x00037AE7
		// (set) Token: 0x06004F52 RID: 20306 RVA: 0x000398F4 File Offset: 0x00037AF4
		public float radius
		{
			get
			{
				return this._value.radius;
			}
			set
			{
				this._value.radius = value;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x00039902 File Offset: 0x00037B02
		public RigidbodyProxy attachedRigidbody
		{
			get
			{
				return RigidbodyProxy.New(this._value.attachedRigidbody);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x00039914 File Offset: 0x00037B14
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x00039926 File Offset: 0x00037B26
		// (set) Token: 0x06004F56 RID: 20310 RVA: 0x00039933 File Offset: 0x00037B33
		public float contactOffset
		{
			get
			{
				return this._value.contactOffset;
			}
			set
			{
				this._value.contactOffset = value;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x00039941 File Offset: 0x00037B41
		// (set) Token: 0x06004F58 RID: 20312 RVA: 0x0003994E File Offset: 0x00037B4E
		public bool enabled
		{
			get
			{
				return this._value.enabled;
			}
			set
			{
				this._value.enabled = value;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x0003995C File Offset: 0x00037B5C
		// (set) Token: 0x06004F5A RID: 20314 RVA: 0x00039969 File Offset: 0x00037B69
		public bool isTrigger
		{
			get
			{
				return this._value.isTrigger;
			}
			set
			{
				this._value.isTrigger = value;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x00039977 File Offset: 0x00037B77
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x00039989 File Offset: 0x00037B89
		// (set) Token: 0x06004F5D RID: 20317 RVA: 0x00039996 File Offset: 0x00037B96
		public string tag
		{
			get
			{
				return this._value.tag;
			}
			set
			{
				this._value.tag = value;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x000399A4 File Offset: 0x00037BA4
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004F5F RID: 20319 RVA: 0x000399B6 File Offset: 0x00037BB6
		// (set) Token: 0x06004F60 RID: 20320 RVA: 0x000399C3 File Offset: 0x00037BC3
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x000399D1 File Offset: 0x00037BD1
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x00138388 File Offset: 0x00136588
		[MoonSharpHidden]
		public static SphereColliderProxy New(SphereCollider value)
		{
			if (value == null)
			{
				return null;
			}
			SphereColliderProxy sphereColliderProxy = (SphereColliderProxy)ObjectCache.Get(typeof(SphereColliderProxy), value);
			if (sphereColliderProxy == null)
			{
				sphereColliderProxy = new SphereColliderProxy(value);
				ObjectCache.Add(typeof(SphereColliderProxy), value, sphereColliderProxy);
			}
			return sphereColliderProxy;
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x000399D9 File Offset: 0x00037BD9
		[MoonSharpUserDataMetamethod("__call")]
		public static SphereColliderProxy Call(DynValue _)
		{
			return new SphereColliderProxy();
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x000399E0 File Offset: 0x00037BE0
		public Vector3Proxy ClosestPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPoint(position._value));
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00039A06 File Offset: 0x00037C06
		public Vector3Proxy ClosestPointOnBounds(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPointOnBounds(position._value));
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00039A2C File Offset: 0x00037C2C
		public bool Raycast(RayProxy ray, RaycastHitProxy hitInfo, float maxDistance)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			if (hitInfo == null)
			{
				throw new ScriptRuntimeException("argument 'hitInfo' is nil");
			}
			return this._value.Raycast(ray._value, out hitInfo._value, maxDistance);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x00039A62 File Offset: 0x00037C62
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00039A70 File Offset: 0x00037C70
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00039A7D File Offset: 0x00037C7D
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003293 RID: 12947
		[MoonSharpHidden]
		public SphereCollider _value;
	}
}
