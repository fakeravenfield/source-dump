using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B6 RID: 2486
	[Proxy(typeof(BoxCollider))]
	public class BoxColliderProxy : IProxy
	{
		// Token: 0x06004245 RID: 16965 RVA: 0x0002D68A File Offset: 0x0002B88A
		[MoonSharpHidden]
		public BoxColliderProxy(BoxCollider value)
		{
			this._value = value;
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x0002D699 File Offset: 0x0002B899
		public BoxColliderProxy()
		{
			this._value = new BoxCollider();
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0002D6AC File Offset: 0x0002B8AC
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x0002D6BE File Offset: 0x0002B8BE
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

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x0002D6DF File Offset: 0x0002B8DF
		// (set) Token: 0x0600424A RID: 16970 RVA: 0x0002D6F1 File Offset: 0x0002B8F1
		public Vector3Proxy size
		{
			get
			{
				return Vector3Proxy.New(this._value.size);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.size = value._value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x0002D712 File Offset: 0x0002B912
		public RigidbodyProxy attachedRigidbody
		{
			get
			{
				return RigidbodyProxy.New(this._value.attachedRigidbody);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x0002D724 File Offset: 0x0002B924
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x0002D736 File Offset: 0x0002B936
		// (set) Token: 0x0600424E RID: 16974 RVA: 0x0002D743 File Offset: 0x0002B943
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

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x0002D751 File Offset: 0x0002B951
		// (set) Token: 0x06004250 RID: 16976 RVA: 0x0002D75E File Offset: 0x0002B95E
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

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x0002D76C File Offset: 0x0002B96C
		// (set) Token: 0x06004252 RID: 16978 RVA: 0x0002D779 File Offset: 0x0002B979
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

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x0002D787 File Offset: 0x0002B987
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x0002D799 File Offset: 0x0002B999
		// (set) Token: 0x06004255 RID: 16981 RVA: 0x0002D7A6 File Offset: 0x0002B9A6
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

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x0002D7B4 File Offset: 0x0002B9B4
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x0002D7C6 File Offset: 0x0002B9C6
		// (set) Token: 0x06004258 RID: 16984 RVA: 0x0002D7D3 File Offset: 0x0002B9D3
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

		// Token: 0x06004259 RID: 16985 RVA: 0x0002D7E1 File Offset: 0x0002B9E1
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x0013002C File Offset: 0x0012E22C
		[MoonSharpHidden]
		public static BoxColliderProxy New(BoxCollider value)
		{
			if (value == null)
			{
				return null;
			}
			BoxColliderProxy boxColliderProxy = (BoxColliderProxy)ObjectCache.Get(typeof(BoxColliderProxy), value);
			if (boxColliderProxy == null)
			{
				boxColliderProxy = new BoxColliderProxy(value);
				ObjectCache.Add(typeof(BoxColliderProxy), value, boxColliderProxy);
			}
			return boxColliderProxy;
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x0002D7E9 File Offset: 0x0002B9E9
		[MoonSharpUserDataMetamethod("__call")]
		public static BoxColliderProxy Call(DynValue _)
		{
			return new BoxColliderProxy();
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
		public Vector3Proxy ClosestPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPoint(position._value));
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x0002D816 File Offset: 0x0002BA16
		public Vector3Proxy ClosestPointOnBounds(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPointOnBounds(position._value));
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x0002D83C File Offset: 0x0002BA3C
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

		// Token: 0x0600425F RID: 16991 RVA: 0x0002D872 File Offset: 0x0002BA72
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0002D880 File Offset: 0x0002BA80
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0002D88D File Offset: 0x0002BA8D
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400314F RID: 12623
		[MoonSharpHidden]
		public BoxCollider _value;
	}
}
