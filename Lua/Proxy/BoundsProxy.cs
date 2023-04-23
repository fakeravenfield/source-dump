using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B5 RID: 2485
	[Proxy(typeof(Bounds))]
	public class BoundsProxy : IProxy
	{
		// Token: 0x06004224 RID: 16932 RVA: 0x0002D2FD File Offset: 0x0002B4FD
		[MoonSharpHidden]
		public BoundsProxy(Bounds value)
		{
			this._value = value;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0002D30C File Offset: 0x0002B50C
		public BoundsProxy(Vector3Proxy center, Vector3Proxy size)
		{
			if (center == null)
			{
				throw new ScriptRuntimeException("argument 'center' is nil");
			}
			if (size == null)
			{
				throw new ScriptRuntimeException("argument 'size' is nil");
			}
			this._value = new Bounds(center._value, size._value);
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x0002D347 File Offset: 0x0002B547
		public BoundsProxy()
		{
			this._value = default(Bounds);
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x0002D35B File Offset: 0x0002B55B
		// (set) Token: 0x06004228 RID: 16936 RVA: 0x0002D36D File Offset: 0x0002B56D
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

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x0002D38E File Offset: 0x0002B58E
		// (set) Token: 0x0600422A RID: 16938 RVA: 0x0002D3A0 File Offset: 0x0002B5A0
		public Vector3Proxy extents
		{
			get
			{
				return Vector3Proxy.New(this._value.extents);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.extents = value._value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600422B RID: 16939 RVA: 0x0002D3C1 File Offset: 0x0002B5C1
		// (set) Token: 0x0600422C RID: 16940 RVA: 0x0002D3D3 File Offset: 0x0002B5D3
		public Vector3Proxy max
		{
			get
			{
				return Vector3Proxy.New(this._value.max);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.max = value._value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x0002D3F4 File Offset: 0x0002B5F4
		// (set) Token: 0x0600422E RID: 16942 RVA: 0x0002D406 File Offset: 0x0002B606
		public Vector3Proxy min
		{
			get
			{
				return Vector3Proxy.New(this._value.min);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.min = value._value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x0002D427 File Offset: 0x0002B627
		// (set) Token: 0x06004230 RID: 16944 RVA: 0x0002D439 File Offset: 0x0002B639
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

		// Token: 0x06004231 RID: 16945 RVA: 0x0002D45A File Offset: 0x0002B65A
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x0002D467 File Offset: 0x0002B667
		[MoonSharpHidden]
		public static BoundsProxy New(Bounds value)
		{
			return new BoundsProxy(value);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x0002D46F File Offset: 0x0002B66F
		[MoonSharpUserDataMetamethod("__call")]
		public static BoundsProxy Call(DynValue _, Vector3Proxy center, Vector3Proxy size)
		{
			return new BoundsProxy(center, size);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x0002D478 File Offset: 0x0002B678
		[MoonSharpUserDataMetamethod("__call")]
		public static BoundsProxy Call(DynValue _)
		{
			return new BoundsProxy();
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x0002D47F File Offset: 0x0002B67F
		public Vector3Proxy ClosestPoint(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPoint(point._value));
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x0002D4A5 File Offset: 0x0002B6A5
		public bool Contains(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.Contains(point._value);
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x0002D4C6 File Offset: 0x0002B6C6
		public void Encapsulate(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			this._value.Encapsulate(point._value);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x0002D4E7 File Offset: 0x0002B6E7
		public void Encapsulate(BoundsProxy bounds)
		{
			if (bounds == null)
			{
				throw new ScriptRuntimeException("argument 'bounds' is nil");
			}
			this._value.Encapsulate(bounds._value);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x0002D508 File Offset: 0x0002B708
		public void Expand(float amount)
		{
			this._value.Expand(amount);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x0002D516 File Offset: 0x0002B716
		public void Expand(Vector3Proxy amount)
		{
			if (amount == null)
			{
				throw new ScriptRuntimeException("argument 'amount' is nil");
			}
			this._value.Expand(amount._value);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x0002D537 File Offset: 0x0002B737
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x0002D54A File Offset: 0x0002B74A
		public bool IntersectRay(RayProxy ray)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			return this._value.IntersectRay(ray._value);
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x0002D56B File Offset: 0x0002B76B
		public bool IntersectRay(RayProxy ray, out float distance)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			distance = 0f;
			return this._value.IntersectRay(ray._value, out distance);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x0002D594 File Offset: 0x0002B794
		public bool Intersects(BoundsProxy bounds)
		{
			if (bounds == null)
			{
				throw new ScriptRuntimeException("argument 'bounds' is nil");
			}
			return this._value.Intersects(bounds._value);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x0002D5B5 File Offset: 0x0002B7B5
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(BoundsProxy lhs, BoundsProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return lhs._value == rhs._value;
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		[MoonSharpHidden]
		public static bool operator !=(BoundsProxy lhs, BoundsProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return lhs._value != rhs._value;
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x0002D613 File Offset: 0x0002B813
		public void SetMinMax(Vector3Proxy min, Vector3Proxy max)
		{
			if (min == null)
			{
				throw new ScriptRuntimeException("argument 'min' is nil");
			}
			if (max == null)
			{
				throw new ScriptRuntimeException("argument 'max' is nil");
			}
			this._value.SetMinMax(min._value, max._value);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x0002D648 File Offset: 0x0002B848
		public float SqrDistance(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.SqrDistance(point._value);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x0002D669 File Offset: 0x0002B869
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x0002D67C File Offset: 0x0002B87C
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x0400314E RID: 12622
		[MoonSharpHidden]
		public Bounds _value;
	}
}
