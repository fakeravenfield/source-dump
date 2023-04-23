using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x020009E8 RID: 2536
	[Proxy(typeof(RawImage))]
	public class RawImageProxy : IProxy
	{
		// Token: 0x06004B1F RID: 19231 RVA: 0x00035798 File Offset: 0x00033998
		[MoonSharpHidden]
		public RawImageProxy(RawImage value)
		{
			this._value = value;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x000357A7 File Offset: 0x000339A7
		public TextureProxy mainTexture
		{
			get
			{
				return TextureProxy.New(this._value.mainTexture);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x000357B9 File Offset: 0x000339B9
		// (set) Token: 0x06004B22 RID: 19234 RVA: 0x00131D64 File Offset: 0x0012FF64
		public TextureProxy texture
		{
			get
			{
				return TextureProxy.New(this._value.texture);
			}
			set
			{
				Texture texture = null;
				if (value != null)
				{
					texture = value._value;
				}
				this._value.texture = texture;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x000357CB File Offset: 0x000339CB
		// (set) Token: 0x06004B24 RID: 19236 RVA: 0x000357DD File Offset: 0x000339DD
		public RectProxy uvRect
		{
			get
			{
				return RectProxy.New(this._value.uvRect);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.uvRect = value._value;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x000357FE File Offset: 0x000339FE
		// (set) Token: 0x06004B26 RID: 19238 RVA: 0x0003580B File Offset: 0x00033A0B
		public bool isMaskingGraphic
		{
			get
			{
				return this._value.isMaskingGraphic;
			}
			set
			{
				this._value.isMaskingGraphic = value;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x00035819 File Offset: 0x00033A19
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x00035826 File Offset: 0x00033A26
		public bool maskable
		{
			get
			{
				return this._value.maskable;
			}
			set
			{
				this._value.maskable = value;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x00035834 File Offset: 0x00033A34
		public CanvasProxy canvas
		{
			get
			{
				return CanvasProxy.New(this._value.canvas);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x00035846 File Offset: 0x00033A46
		// (set) Token: 0x06004B2B RID: 19243 RVA: 0x00035858 File Offset: 0x00033A58
		public ColorProxy color
		{
			get
			{
				return ColorProxy.New(this._value.color);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.color = value._value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06004B2C RID: 19244 RVA: 0x00030609 File Offset: 0x0002E809
		public static MaterialProxy defaultGraphicMaterial
		{
			get
			{
				return MaterialProxy.New(Graphic.defaultGraphicMaterial);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x00035879 File Offset: 0x00033A79
		public MaterialProxy defaultMaterial
		{
			get
			{
				return MaterialProxy.New(this._value.defaultMaterial);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06004B2E RID: 19246 RVA: 0x0003588B File Offset: 0x00033A8B
		public int depth
		{
			get
			{
				return this._value.depth;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x00035898 File Offset: 0x00033A98
		// (set) Token: 0x06004B30 RID: 19248 RVA: 0x00131D8C File Offset: 0x0012FF8C
		public MaterialProxy material
		{
			get
			{
				return MaterialProxy.New(this._value.material);
			}
			set
			{
				Material material = null;
				if (value != null)
				{
					material = value._value;
				}
				this._value.material = material;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06004B31 RID: 19249 RVA: 0x000358AA File Offset: 0x00033AAA
		public MaterialProxy materialForRendering
		{
			get
			{
				return MaterialProxy.New(this._value.materialForRendering);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06004B32 RID: 19250 RVA: 0x000358BC File Offset: 0x00033ABC
		// (set) Token: 0x06004B33 RID: 19251 RVA: 0x000358CE File Offset: 0x00033ACE
		public Vector4Proxy raycastPadding
		{
			get
			{
				return Vector4Proxy.New(this._value.raycastPadding);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.raycastPadding = value._value;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06004B34 RID: 19252 RVA: 0x000358EF File Offset: 0x00033AEF
		// (set) Token: 0x06004B35 RID: 19253 RVA: 0x000358FC File Offset: 0x00033AFC
		public bool raycastTarget
		{
			get
			{
				return this._value.raycastTarget;
			}
			set
			{
				this._value.raycastTarget = value;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06004B36 RID: 19254 RVA: 0x0003590A File Offset: 0x00033B0A
		public RectTransformProxy rectTransform
		{
			get
			{
				return RectTransformProxy.New(this._value.rectTransform);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x0003591C File Offset: 0x00033B1C
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x0003592E File Offset: 0x00033B2E
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x00035940 File Offset: 0x00033B40
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00131DB4 File Offset: 0x0012FFB4
		[MoonSharpHidden]
		public static RawImageProxy New(RawImage value)
		{
			if (value == null)
			{
				return null;
			}
			RawImageProxy rawImageProxy = (RawImageProxy)ObjectCache.Get(typeof(RawImageProxy), value);
			if (rawImageProxy == null)
			{
				rawImageProxy = new RawImageProxy(value);
				ObjectCache.Add(typeof(RawImageProxy), value, rawImageProxy);
			}
			return rawImageProxy;
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00035948 File Offset: 0x00033B48
		public void SetNativeSize()
		{
			this._value.SetNativeSize();
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x00035955 File Offset: 0x00033B55
		public void Cull(RectProxy clipRect, bool validRect)
		{
			if (clipRect == null)
			{
				throw new ScriptRuntimeException("argument 'clipRect' is nil");
			}
			this._value.Cull(clipRect._value, validRect);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00131E00 File Offset: 0x00130000
		public MaterialProxy GetModifiedMaterial(MaterialProxy baseMaterial)
		{
			Material baseMaterial2 = null;
			if (baseMaterial != null)
			{
				baseMaterial2 = baseMaterial._value;
			}
			return MaterialProxy.New(this._value.GetModifiedMaterial(baseMaterial2));
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x00035977 File Offset: 0x00033B77
		public void RecalculateClipping()
		{
			this._value.RecalculateClipping();
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00035984 File Offset: 0x00033B84
		public void RecalculateMasking()
		{
			this._value.RecalculateMasking();
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00035991 File Offset: 0x00033B91
		public void SetClipRect(RectProxy clipRect, bool validRect)
		{
			if (clipRect == null)
			{
				throw new ScriptRuntimeException("argument 'clipRect' is nil");
			}
			this._value.SetClipRect(clipRect._value, validRect);
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x000359B3 File Offset: 0x00033BB3
		public void SetClipSoftness(Vector2Proxy clipSoftness)
		{
			if (clipSoftness == null)
			{
				throw new ScriptRuntimeException("argument 'clipSoftness' is nil");
			}
			this._value.SetClipSoftness(clipSoftness._value);
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x000359D4 File Offset: 0x00033BD4
		public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this._value.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x000359E4 File Offset: 0x00033BE4
		public void CrossFadeColor(ColorProxy targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			if (targetColor == null)
			{
				throw new ScriptRuntimeException("argument 'targetColor' is nil");
			}
			this._value.CrossFadeColor(targetColor._value, duration, ignoreTimeScale, useAlpha);
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00035A09 File Offset: 0x00033C09
		public void CrossFadeColor(ColorProxy targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (targetColor == null)
			{
				throw new ScriptRuntimeException("argument 'targetColor' is nil");
			}
			this._value.CrossFadeColor(targetColor._value, duration, ignoreTimeScale, useAlpha, useRGB);
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x00035A30 File Offset: 0x00033C30
		public RectProxy GetPixelAdjustedRect()
		{
			return RectProxy.New(this._value.GetPixelAdjustedRect());
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x00035A42 File Offset: 0x00033C42
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00035A4F File Offset: 0x00033C4F
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x00035A5C File Offset: 0x00033C5C
		public void OnCullingChanged()
		{
			this._value.OnCullingChanged();
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x00035A69 File Offset: 0x00033C69
		public Vector2Proxy PixelAdjustPoint(Vector2Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector2Proxy.New(this._value.PixelAdjustPoint(point._value));
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x00131E2C File Offset: 0x0013002C
		public bool Raycast(Vector2Proxy sp, CameraProxy eventCamera)
		{
			if (sp == null)
			{
				throw new ScriptRuntimeException("argument 'sp' is nil");
			}
			Camera eventCamera2 = null;
			if (eventCamera != null)
			{
				eventCamera2 = eventCamera._value;
			}
			return this._value.Raycast(sp._value, eventCamera2);
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x00035A8F File Offset: 0x00033C8F
		public void SetAllDirty()
		{
			this._value.SetAllDirty();
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00035A9C File Offset: 0x00033C9C
		public void SetLayoutDirty()
		{
			this._value.SetLayoutDirty();
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x00035AA9 File Offset: 0x00033CA9
		public void SetMaterialDirty()
		{
			this._value.SetMaterialDirty();
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00035AB6 File Offset: 0x00033CB6
		public void SetVerticesDirty()
		{
			this._value.SetVerticesDirty();
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x00035AC3 File Offset: 0x00033CC3
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00035AD0 File Offset: 0x00033CD0
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00035ADD File Offset: 0x00033CDD
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400317F RID: 12671
		[MoonSharpHidden]
		public RawImage _value;
	}
}
