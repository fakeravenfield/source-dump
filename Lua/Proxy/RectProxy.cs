using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009EB RID: 2539
	[Proxy(typeof(Rect))]
	public class RectProxy : IProxy
	{
		// Token: 0x06004B75 RID: 19317 RVA: 0x00035D86 File Offset: 0x00033F86
		[MoonSharpHidden]
		public RectProxy(Rect value)
		{
			this._value = value;
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00035D95 File Offset: 0x00033F95
		public RectProxy(float x, float y, float width, float height)
		{
			this._value = new Rect(x, y, width, height);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00035DAD File Offset: 0x00033FAD
		public RectProxy(Vector2Proxy position, Vector2Proxy size)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (size == null)
			{
				throw new ScriptRuntimeException("argument 'size' is nil");
			}
			this._value = new Rect(position._value, size._value);
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00035DE8 File Offset: 0x00033FE8
		public RectProxy(RectProxy source)
		{
			if (source == null)
			{
				throw new ScriptRuntimeException("argument 'source' is nil");
			}
			this._value = new Rect(source._value);
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00035E0F File Offset: 0x0003400F
		public RectProxy()
		{
			this._value = default(Rect);
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06004B7A RID: 19322 RVA: 0x00035E23 File Offset: 0x00034023
		// (set) Token: 0x06004B7B RID: 19323 RVA: 0x00035E35 File Offset: 0x00034035
		public Vector2Proxy center
		{
			get
			{
				return Vector2Proxy.New(this._value.center);
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

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x00035E56 File Offset: 0x00034056
		// (set) Token: 0x06004B7D RID: 19325 RVA: 0x00035E63 File Offset: 0x00034063
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

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x00035E71 File Offset: 0x00034071
		// (set) Token: 0x06004B7F RID: 19327 RVA: 0x00035E83 File Offset: 0x00034083
		public Vector2Proxy max
		{
			get
			{
				return Vector2Proxy.New(this._value.max);
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

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x00035EA4 File Offset: 0x000340A4
		// (set) Token: 0x06004B81 RID: 19329 RVA: 0x00035EB6 File Offset: 0x000340B6
		public Vector2Proxy min
		{
			get
			{
				return Vector2Proxy.New(this._value.min);
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

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x00035ED7 File Offset: 0x000340D7
		// (set) Token: 0x06004B83 RID: 19331 RVA: 0x00035EE9 File Offset: 0x000340E9
		public Vector2Proxy position
		{
			get
			{
				return Vector2Proxy.New(this._value.position);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.position = value._value;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06004B84 RID: 19332 RVA: 0x00035F0A File Offset: 0x0003410A
		// (set) Token: 0x06004B85 RID: 19333 RVA: 0x00035F1C File Offset: 0x0003411C
		public Vector2Proxy size
		{
			get
			{
				return Vector2Proxy.New(this._value.size);
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

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x00035F3D File Offset: 0x0003413D
		// (set) Token: 0x06004B87 RID: 19335 RVA: 0x00035F4A File Offset: 0x0003414A
		public float width
		{
			get
			{
				return this._value.width;
			}
			set
			{
				this._value.width = value;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06004B88 RID: 19336 RVA: 0x00035F58 File Offset: 0x00034158
		// (set) Token: 0x06004B89 RID: 19337 RVA: 0x00035F65 File Offset: 0x00034165
		public float x
		{
			get
			{
				return this._value.x;
			}
			set
			{
				this._value.x = value;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x00035F73 File Offset: 0x00034173
		// (set) Token: 0x06004B8B RID: 19339 RVA: 0x00035F80 File Offset: 0x00034180
		public float xMax
		{
			get
			{
				return this._value.xMax;
			}
			set
			{
				this._value.xMax = value;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x00035F8E File Offset: 0x0003418E
		// (set) Token: 0x06004B8D RID: 19341 RVA: 0x00035F9B File Offset: 0x0003419B
		public float xMin
		{
			get
			{
				return this._value.xMin;
			}
			set
			{
				this._value.xMin = value;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x00035FA9 File Offset: 0x000341A9
		// (set) Token: 0x06004B8F RID: 19343 RVA: 0x00035FB6 File Offset: 0x000341B6
		public float y
		{
			get
			{
				return this._value.y;
			}
			set
			{
				this._value.y = value;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x00035FC4 File Offset: 0x000341C4
		// (set) Token: 0x06004B91 RID: 19345 RVA: 0x00035FD1 File Offset: 0x000341D1
		public float yMax
		{
			get
			{
				return this._value.yMax;
			}
			set
			{
				this._value.yMax = value;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x00035FDF File Offset: 0x000341DF
		// (set) Token: 0x06004B93 RID: 19347 RVA: 0x00035FEC File Offset: 0x000341EC
		public float yMin
		{
			get
			{
				return this._value.yMin;
			}
			set
			{
				this._value.yMin = value;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x00035FFA File Offset: 0x000341FA
		public static RectProxy zero
		{
			get
			{
				return RectProxy.New(Rect.zero);
			}
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x00036006 File Offset: 0x00034206
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x00036013 File Offset: 0x00034213
		[MoonSharpHidden]
		public static RectProxy New(Rect value)
		{
			return new RectProxy(value);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0003601B File Offset: 0x0003421B
		[MoonSharpUserDataMetamethod("__call")]
		public static RectProxy Call(DynValue _, float x, float y, float width, float height)
		{
			return new RectProxy(x, y, width, height);
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00036027 File Offset: 0x00034227
		[MoonSharpUserDataMetamethod("__call")]
		public static RectProxy Call(DynValue _, Vector2Proxy position, Vector2Proxy size)
		{
			return new RectProxy(position, size);
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x00036030 File Offset: 0x00034230
		[MoonSharpUserDataMetamethod("__call")]
		public static RectProxy Call(DynValue _, RectProxy source)
		{
			return new RectProxy(source);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00036038 File Offset: 0x00034238
		[MoonSharpUserDataMetamethod("__call")]
		public static RectProxy Call(DynValue _)
		{
			return new RectProxy();
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0003603F File Offset: 0x0003423F
		public bool Contains(Vector2Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.Contains(point._value);
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x00036060 File Offset: 0x00034260
		public bool Contains(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.Contains(point._value);
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00036081 File Offset: 0x00034281
		public bool Contains(Vector3Proxy point, bool allowInverse)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return this._value.Contains(point._value, allowInverse);
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x000360A3 File Offset: 0x000342A3
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x000360B6 File Offset: 0x000342B6
		public static RectProxy MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return RectProxy.New(Rect.MinMaxRect(xmin, ymin, xmax, ymax));
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x000360C6 File Offset: 0x000342C6
		public static Vector2Proxy NormalizedToPoint(RectProxy rectangle, Vector2Proxy normalizedRectCoordinates)
		{
			if (rectangle == null)
			{
				throw new ScriptRuntimeException("argument 'rectangle' is nil");
			}
			if (normalizedRectCoordinates == null)
			{
				throw new ScriptRuntimeException("argument 'normalizedRectCoordinates' is nil");
			}
			return Vector2Proxy.New(Rect.NormalizedToPoint(rectangle._value, normalizedRectCoordinates._value));
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x000360FA File Offset: 0x000342FA
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(RectProxy lhs, RectProxy rhs)
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

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00036129 File Offset: 0x00034329
		[MoonSharpHidden]
		public static bool operator !=(RectProxy lhs, RectProxy rhs)
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

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00036158 File Offset: 0x00034358
		public bool Overlaps(RectProxy other)
		{
			if (other == null)
			{
				throw new ScriptRuntimeException("argument 'other' is nil");
			}
			return this._value.Overlaps(other._value);
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00036179 File Offset: 0x00034379
		public bool Overlaps(RectProxy other, bool allowInverse)
		{
			if (other == null)
			{
				throw new ScriptRuntimeException("argument 'other' is nil");
			}
			return this._value.Overlaps(other._value, allowInverse);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0003619B File Offset: 0x0003439B
		public static Vector2Proxy PointToNormalized(RectProxy rectangle, Vector2Proxy point)
		{
			if (rectangle == null)
			{
				throw new ScriptRuntimeException("argument 'rectangle' is nil");
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector2Proxy.New(Rect.PointToNormalized(rectangle._value, point._value));
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x000361CF File Offset: 0x000343CF
		public void Set(float x, float y, float width, float height)
		{
			this._value.Set(x, y, width, height);
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x000361E1 File Offset: 0x000343E1
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x000361F4 File Offset: 0x000343F4
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x04003182 RID: 12674
		[MoonSharpHidden]
		public Rect _value;
	}
}
