using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009BB RID: 2491
	[Proxy(typeof(CapsuleCollider))]
	public class CapsuleColliderProxy : IProxy
	{
		// Token: 0x06004342 RID: 17218 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		[MoonSharpHidden]
		public CapsuleColliderProxy(CapsuleCollider value)
		{
			this._value = value;
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x0002E5CF File Offset: 0x0002C7CF
		public CapsuleColliderProxy()
		{
			this._value = new CapsuleCollider();
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x0002E5E2 File Offset: 0x0002C7E2
		// (set) Token: 0x06004345 RID: 17221 RVA: 0x0002E5F4 File Offset: 0x0002C7F4
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

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x0002E615 File Offset: 0x0002C815
		// (set) Token: 0x06004347 RID: 17223 RVA: 0x0002E622 File Offset: 0x0002C822
		public int direction
		{
			get
			{
				return this._value.direction;
			}
			set
			{
				this._value.direction = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x0002E630 File Offset: 0x0002C830
		// (set) Token: 0x06004349 RID: 17225 RVA: 0x0002E63D File Offset: 0x0002C83D
		public float height
		{
			get
			{
				return this._value.height;
			}
			set
			{
				this._value.height = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x0002E64B File Offset: 0x0002C84B
		// (set) Token: 0x0600434B RID: 17227 RVA: 0x0002E658 File Offset: 0x0002C858
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

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x0002E666 File Offset: 0x0002C866
		public RigidbodyProxy attachedRigidbody
		{
			get
			{
				return RigidbodyProxy.New(this._value.attachedRigidbody);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x0002E678 File Offset: 0x0002C878
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x0002E68A File Offset: 0x0002C88A
		// (set) Token: 0x0600434F RID: 17231 RVA: 0x0002E697 File Offset: 0x0002C897
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

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x0002E6A5 File Offset: 0x0002C8A5
		// (set) Token: 0x06004351 RID: 17233 RVA: 0x0002E6B2 File Offset: 0x0002C8B2
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

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x0002E6C0 File Offset: 0x0002C8C0
		// (set) Token: 0x06004353 RID: 17235 RVA: 0x0002E6CD File Offset: 0x0002C8CD
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

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x0002E6DB File Offset: 0x0002C8DB
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x0002E6ED File Offset: 0x0002C8ED
		// (set) Token: 0x06004356 RID: 17238 RVA: 0x0002E6FA File Offset: 0x0002C8FA
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

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x0002E708 File Offset: 0x0002C908
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x0002E71A File Offset: 0x0002C91A
		// (set) Token: 0x06004359 RID: 17241 RVA: 0x0002E727 File Offset: 0x0002C927
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

		// Token: 0x0600435A RID: 17242 RVA: 0x0002E735 File Offset: 0x0002C935
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x0013027C File Offset: 0x0012E47C
		[MoonSharpHidden]
		public static CapsuleColliderProxy New(CapsuleCollider value)
		{
			if (value == null)
			{
				return null;
			}
			CapsuleColliderProxy capsuleColliderProxy = (CapsuleColliderProxy)ObjectCache.Get(typeof(CapsuleColliderProxy), value);
			if (capsuleColliderProxy == null)
			{
				capsuleColliderProxy = new CapsuleColliderProxy(value);
				ObjectCache.Add(typeof(CapsuleColliderProxy), value, capsuleColliderProxy);
			}
			return capsuleColliderProxy;
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x0002E73D File Offset: 0x0002C93D
		[MoonSharpUserDataMetamethod("__call")]
		public static CapsuleColliderProxy Call(DynValue _)
		{
			return new CapsuleColliderProxy();
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x0002E744 File Offset: 0x0002C944
		public Vector3Proxy ClosestPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPoint(position._value));
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x0002E76A File Offset: 0x0002C96A
		public Vector3Proxy ClosestPointOnBounds(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPointOnBounds(position._value));
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x0002E790 File Offset: 0x0002C990
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

		// Token: 0x06004360 RID: 17248 RVA: 0x0002E7C6 File Offset: 0x0002C9C6
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x0002E7D4 File Offset: 0x0002C9D4
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x0002E7E1 File Offset: 0x0002C9E1
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003154 RID: 12628
		[MoonSharpHidden]
		public CapsuleCollider _value;
	}
}
