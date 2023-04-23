using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A05 RID: 2565
	[Proxy(typeof(Sprite))]
	public class SpriteProxy : IProxy
	{
		// Token: 0x06004F6A RID: 20330 RVA: 0x00039A8A File Offset: 0x00037C8A
		[MoonSharpHidden]
		public SpriteProxy(Sprite value)
		{
			this._value = value;
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x00039A99 File Offset: 0x00037C99
		public TextureProxy associatedAlphaSplitTexture
		{
			get
			{
				return TextureProxy.New(this._value.associatedAlphaSplitTexture);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06004F6C RID: 20332 RVA: 0x00039AAB File Offset: 0x00037CAB
		public Vector4Proxy border
		{
			get
			{
				return Vector4Proxy.New(this._value.border);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004F6D RID: 20333 RVA: 0x00039ABD File Offset: 0x00037CBD
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x00039ACF File Offset: 0x00037CCF
		public bool packed
		{
			get
			{
				return this._value.packed;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x00039ADC File Offset: 0x00037CDC
		public Vector2Proxy pivot
		{
			get
			{
				return Vector2Proxy.New(this._value.pivot);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x00039AEE File Offset: 0x00037CEE
		public float pixelsPerUnit
		{
			get
			{
				return this._value.pixelsPerUnit;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004F71 RID: 20337 RVA: 0x00039AFB File Offset: 0x00037CFB
		public RectProxy rect
		{
			get
			{
				return RectProxy.New(this._value.rect);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x00039B0D File Offset: 0x00037D0D
		public float spriteAtlasTextureScale
		{
			get
			{
				return this._value.spriteAtlasTextureScale;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x00039B1A File Offset: 0x00037D1A
		public TextureProxy texture
		{
			get
			{
				return TextureProxy.New(this._value.texture);
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x00039B2C File Offset: 0x00037D2C
		public RectProxy textureRect
		{
			get
			{
				return RectProxy.New(this._value.textureRect);
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x00039B3E File Offset: 0x00037D3E
		public Vector2Proxy textureRectOffset
		{
			get
			{
				return Vector2Proxy.New(this._value.textureRectOffset);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x00039B50 File Offset: 0x00037D50
		public ushort[] triangles
		{
			get
			{
				return this._value.triangles;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004F77 RID: 20343 RVA: 0x00039B5D File Offset: 0x00037D5D
		public Vector2[] uv
		{
			get
			{
				return this._value.uv;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00039B6A File Offset: 0x00037D6A
		public Vector2[] vertices
		{
			get
			{
				return this._value.vertices;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06004F79 RID: 20345 RVA: 0x00039B77 File Offset: 0x00037D77
		// (set) Token: 0x06004F7A RID: 20346 RVA: 0x00039B84 File Offset: 0x00037D84
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

		// Token: 0x06004F7B RID: 20347 RVA: 0x00039B92 File Offset: 0x00037D92
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x001383D4 File Offset: 0x001365D4
		[MoonSharpHidden]
		public static SpriteProxy New(Sprite value)
		{
			if (value == null)
			{
				return null;
			}
			SpriteProxy spriteProxy = (SpriteProxy)ObjectCache.Get(typeof(SpriteProxy), value);
			if (spriteProxy == null)
			{
				spriteProxy = new SpriteProxy(value);
				ObjectCache.Add(typeof(SpriteProxy), value, spriteProxy);
			}
			return spriteProxy;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00039B9A File Offset: 0x00037D9A
		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			return this._value.GetPhysicsShape(shapeIdx, physicsShape);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00039BA9 File Offset: 0x00037DA9
		public int GetPhysicsShapeCount()
		{
			return this._value.GetPhysicsShapeCount();
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x00039BB6 File Offset: 0x00037DB6
		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			return this._value.GetPhysicsShapePointCount(shapeIdx);
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public void OverrideGeometry(Vector2[] vertices, ushort[] triangles)
		{
			this._value.OverrideGeometry(vertices, triangles);
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x00039BD3 File Offset: 0x00037DD3
		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			this._value.OverridePhysicsShape(physicsShapes);
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x00039BE1 File Offset: 0x00037DE1
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x00039BEE File Offset: 0x00037DEE
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003294 RID: 12948
		[MoonSharpHidden]
		public Sprite _value;
	}
}
