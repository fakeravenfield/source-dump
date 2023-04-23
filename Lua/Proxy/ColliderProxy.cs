using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009BD RID: 2493
	[Proxy(typeof(Collider))]
	public class ColliderProxy : IProxy
	{
		// Token: 0x06004380 RID: 17280 RVA: 0x0002E974 File Offset: 0x0002CB74
		[MoonSharpHidden]
		public ColliderProxy(Collider value)
		{
			this._value = value;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x0002E983 File Offset: 0x0002CB83
		public ColliderProxy()
		{
			this._value = new Collider();
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x0002E996 File Offset: 0x0002CB96
		public RigidbodyProxy attachedRigidbody
		{
			get
			{
				return RigidbodyProxy.New(this._value.attachedRigidbody);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x0002E9BA File Offset: 0x0002CBBA
		// (set) Token: 0x06004385 RID: 17285 RVA: 0x0002E9C7 File Offset: 0x0002CBC7
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

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x0002E9D5 File Offset: 0x0002CBD5
		// (set) Token: 0x06004387 RID: 17287 RVA: 0x0002E9E2 File Offset: 0x0002CBE2
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

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x0002E9F0 File Offset: 0x0002CBF0
		// (set) Token: 0x06004389 RID: 17289 RVA: 0x0002E9FD File Offset: 0x0002CBFD
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

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x0002EA0B File Offset: 0x0002CC0B
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x0002EA1D File Offset: 0x0002CC1D
		// (set) Token: 0x0600438C RID: 17292 RVA: 0x0002EA2A File Offset: 0x0002CC2A
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

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x0002EA38 File Offset: 0x0002CC38
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600438E RID: 17294 RVA: 0x0002EA4A File Offset: 0x0002CC4A
		// (set) Token: 0x0600438F RID: 17295 RVA: 0x0002EA57 File Offset: 0x0002CC57
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

		// Token: 0x06004390 RID: 17296 RVA: 0x0002EA65 File Offset: 0x0002CC65
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x00130314 File Offset: 0x0012E514
		[MoonSharpHidden]
		public static ColliderProxy New(Collider value)
		{
			if (value == null)
			{
				return null;
			}
			ColliderProxy colliderProxy = (ColliderProxy)ObjectCache.Get(typeof(ColliderProxy), value);
			if (colliderProxy == null)
			{
				colliderProxy = new ColliderProxy(value);
				ObjectCache.Add(typeof(ColliderProxy), value, colliderProxy);
			}
			return colliderProxy;
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x0002EA6D File Offset: 0x0002CC6D
		[MoonSharpUserDataMetamethod("__call")]
		public static ColliderProxy Call(DynValue _)
		{
			return new ColliderProxy();
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x0002EA74 File Offset: 0x0002CC74
		public Vector3Proxy ClosestPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPoint(position._value));
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x0002EA9A File Offset: 0x0002CC9A
		public Vector3Proxy ClosestPointOnBounds(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPointOnBounds(position._value));
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
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

		// Token: 0x06004396 RID: 17302 RVA: 0x0002EAF6 File Offset: 0x0002CCF6
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0002EB04 File Offset: 0x0002CD04
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x0002EB11 File Offset: 0x0002CD11
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003156 RID: 12630
		[MoonSharpHidden]
		public Collider _value;
	}
}
