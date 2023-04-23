using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x02000A0A RID: 2570
	[Proxy(typeof(Text))]
	public class TextProxy : IProxy
	{
		// Token: 0x06004FC8 RID: 20424 RVA: 0x00039F53 File Offset: 0x00038153
		[MoonSharpHidden]
		public TextProxy(Text value)
		{
			this._value = value;
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x00039F62 File Offset: 0x00038162
		// (set) Token: 0x06004FCA RID: 20426 RVA: 0x00039F6F File Offset: 0x0003816F
		public bool alignByGeometry
		{
			get
			{
				return this._value.alignByGeometry;
			}
			set
			{
				this._value.alignByGeometry = value;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x00039F7D File Offset: 0x0003817D
		public float flexibleHeight
		{
			get
			{
				return this._value.flexibleHeight;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x00039F8A File Offset: 0x0003818A
		public float flexibleWidth
		{
			get
			{
				return this._value.flexibleWidth;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x00039F97 File Offset: 0x00038197
		// (set) Token: 0x06004FCE RID: 20430 RVA: 0x00039FA4 File Offset: 0x000381A4
		public int fontSize
		{
			get
			{
				return this._value.fontSize;
			}
			set
			{
				this._value.fontSize = value;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x00039FB2 File Offset: 0x000381B2
		public int layoutPriority
		{
			get
			{
				return this._value.layoutPriority;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00039FBF File Offset: 0x000381BF
		// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x00039FCC File Offset: 0x000381CC
		public float lineSpacing
		{
			get
			{
				return this._value.lineSpacing;
			}
			set
			{
				this._value.lineSpacing = value;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x00039FDA File Offset: 0x000381DA
		public TextureProxy mainTexture
		{
			get
			{
				return TextureProxy.New(this._value.mainTexture);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x00039FEC File Offset: 0x000381EC
		public float minHeight
		{
			get
			{
				return this._value.minHeight;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x00039FF9 File Offset: 0x000381F9
		public float minWidth
		{
			get
			{
				return this._value.minWidth;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x0003A006 File Offset: 0x00038206
		public float pixelsPerUnit
		{
			get
			{
				return this._value.pixelsPerUnit;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004FD6 RID: 20438 RVA: 0x0003A013 File Offset: 0x00038213
		public float preferredHeight
		{
			get
			{
				return this._value.preferredHeight;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x0003A020 File Offset: 0x00038220
		public float preferredWidth
		{
			get
			{
				return this._value.preferredWidth;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x0003A02D File Offset: 0x0003822D
		// (set) Token: 0x06004FD9 RID: 20441 RVA: 0x0003A03A File Offset: 0x0003823A
		public bool resizeTextForBestFit
		{
			get
			{
				return this._value.resizeTextForBestFit;
			}
			set
			{
				this._value.resizeTextForBestFit = value;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x0003A048 File Offset: 0x00038248
		// (set) Token: 0x06004FDB RID: 20443 RVA: 0x0003A055 File Offset: 0x00038255
		public int resizeTextMaxSize
		{
			get
			{
				return this._value.resizeTextMaxSize;
			}
			set
			{
				this._value.resizeTextMaxSize = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x0003A063 File Offset: 0x00038263
		// (set) Token: 0x06004FDD RID: 20445 RVA: 0x0003A070 File Offset: 0x00038270
		public int resizeTextMinSize
		{
			get
			{
				return this._value.resizeTextMinSize;
			}
			set
			{
				this._value.resizeTextMinSize = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06004FDE RID: 20446 RVA: 0x0003A07E File Offset: 0x0003827E
		// (set) Token: 0x06004FDF RID: 20447 RVA: 0x0003A08B File Offset: 0x0003828B
		public bool supportRichText
		{
			get
			{
				return this._value.supportRichText;
			}
			set
			{
				this._value.supportRichText = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06004FE0 RID: 20448 RVA: 0x0003A099 File Offset: 0x00038299
		// (set) Token: 0x06004FE1 RID: 20449 RVA: 0x0003A0A6 File Offset: 0x000382A6
		public string text
		{
			get
			{
				return this._value.text;
			}
			set
			{
				this._value.text = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x0003A0B4 File Offset: 0x000382B4
		// (set) Token: 0x06004FE3 RID: 20451 RVA: 0x0003A0C1 File Offset: 0x000382C1
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

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x0003A0CF File Offset: 0x000382CF
		// (set) Token: 0x06004FE5 RID: 20453 RVA: 0x0003A0DC File Offset: 0x000382DC
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x0003A0EA File Offset: 0x000382EA
		public CanvasProxy canvas
		{
			get
			{
				return CanvasProxy.New(this._value.canvas);
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x0003A0FC File Offset: 0x000382FC
		// (set) Token: 0x06004FE8 RID: 20456 RVA: 0x0003A10E File Offset: 0x0003830E
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06004FE9 RID: 20457 RVA: 0x00030609 File Offset: 0x0002E809
		public static MaterialProxy defaultGraphicMaterial
		{
			get
			{
				return MaterialProxy.New(Graphic.defaultGraphicMaterial);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x0003A12F File Offset: 0x0003832F
		public MaterialProxy defaultMaterial
		{
			get
			{
				return MaterialProxy.New(this._value.defaultMaterial);
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x0003A141 File Offset: 0x00038341
		public int depth
		{
			get
			{
				return this._value.depth;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x0003A14E File Offset: 0x0003834E
		// (set) Token: 0x06004FED RID: 20461 RVA: 0x00138638 File Offset: 0x00136838
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

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x0003A160 File Offset: 0x00038360
		public MaterialProxy materialForRendering
		{
			get
			{
				return MaterialProxy.New(this._value.materialForRendering);
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x0003A172 File Offset: 0x00038372
		// (set) Token: 0x06004FF0 RID: 20464 RVA: 0x0003A184 File Offset: 0x00038384
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

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x0003A1A5 File Offset: 0x000383A5
		// (set) Token: 0x06004FF2 RID: 20466 RVA: 0x0003A1B2 File Offset: 0x000383B2
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

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06004FF3 RID: 20467 RVA: 0x0003A1C0 File Offset: 0x000383C0
		public RectTransformProxy rectTransform
		{
			get
			{
				return RectTransformProxy.New(this._value.rectTransform);
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x0003A1D2 File Offset: 0x000383D2
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x0003A1E4 File Offset: 0x000383E4
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0003A1F6 File Offset: 0x000383F6
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x00138660 File Offset: 0x00136860
		[MoonSharpHidden]
		public static TextProxy New(Text value)
		{
			if (value == null)
			{
				return null;
			}
			TextProxy textProxy = (TextProxy)ObjectCache.Get(typeof(TextProxy), value);
			if (textProxy == null)
			{
				textProxy = new TextProxy(value);
				ObjectCache.Add(typeof(TextProxy), value, textProxy);
			}
			return textProxy;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0003A1FE File Offset: 0x000383FE
		public void CalculateLayoutInputHorizontal()
		{
			this._value.CalculateLayoutInputHorizontal();
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0003A20B File Offset: 0x0003840B
		public void CalculateLayoutInputVertical()
		{
			this._value.CalculateLayoutInputVertical();
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0003A218 File Offset: 0x00038418
		public void FontTextureChanged()
		{
			this._value.FontTextureChanged();
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0003A225 File Offset: 0x00038425
		public void Cull(RectProxy clipRect, bool validRect)
		{
			if (clipRect == null)
			{
				throw new ScriptRuntimeException("argument 'clipRect' is nil");
			}
			this._value.Cull(clipRect._value, validRect);
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x001386AC File Offset: 0x001368AC
		public MaterialProxy GetModifiedMaterial(MaterialProxy baseMaterial)
		{
			Material baseMaterial2 = null;
			if (baseMaterial != null)
			{
				baseMaterial2 = baseMaterial._value;
			}
			return MaterialProxy.New(this._value.GetModifiedMaterial(baseMaterial2));
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0003A247 File Offset: 0x00038447
		public void RecalculateClipping()
		{
			this._value.RecalculateClipping();
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0003A254 File Offset: 0x00038454
		public void RecalculateMasking()
		{
			this._value.RecalculateMasking();
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0003A261 File Offset: 0x00038461
		public void SetClipRect(RectProxy clipRect, bool validRect)
		{
			if (clipRect == null)
			{
				throw new ScriptRuntimeException("argument 'clipRect' is nil");
			}
			this._value.SetClipRect(clipRect._value, validRect);
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0003A283 File Offset: 0x00038483
		public void SetClipSoftness(Vector2Proxy clipSoftness)
		{
			if (clipSoftness == null)
			{
				throw new ScriptRuntimeException("argument 'clipSoftness' is nil");
			}
			this._value.SetClipSoftness(clipSoftness._value);
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0003A2A4 File Offset: 0x000384A4
		public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this._value.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0003A2B4 File Offset: 0x000384B4
		public void CrossFadeColor(ColorProxy targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			if (targetColor == null)
			{
				throw new ScriptRuntimeException("argument 'targetColor' is nil");
			}
			this._value.CrossFadeColor(targetColor._value, duration, ignoreTimeScale, useAlpha);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0003A2D9 File Offset: 0x000384D9
		public void CrossFadeColor(ColorProxy targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (targetColor == null)
			{
				throw new ScriptRuntimeException("argument 'targetColor' is nil");
			}
			this._value.CrossFadeColor(targetColor._value, duration, ignoreTimeScale, useAlpha, useRGB);
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0003A300 File Offset: 0x00038500
		public RectProxy GetPixelAdjustedRect()
		{
			return RectProxy.New(this._value.GetPixelAdjustedRect());
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0003A312 File Offset: 0x00038512
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0003A31F File Offset: 0x0003851F
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0003A32C File Offset: 0x0003852C
		public void OnCullingChanged()
		{
			this._value.OnCullingChanged();
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0003A339 File Offset: 0x00038539
		public Vector2Proxy PixelAdjustPoint(Vector2Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector2Proxy.New(this._value.PixelAdjustPoint(point._value));
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x001386D8 File Offset: 0x001368D8
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

		// Token: 0x0600500A RID: 20490 RVA: 0x0003A35F File Offset: 0x0003855F
		public void SetAllDirty()
		{
			this._value.SetAllDirty();
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0003A36C File Offset: 0x0003856C
		public void SetLayoutDirty()
		{
			this._value.SetLayoutDirty();
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0003A379 File Offset: 0x00038579
		public void SetMaterialDirty()
		{
			this._value.SetMaterialDirty();
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0003A386 File Offset: 0x00038586
		public void SetNativeSize()
		{
			this._value.SetNativeSize();
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0003A393 File Offset: 0x00038593
		public void SetVerticesDirty()
		{
			this._value.SetVerticesDirty();
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0003A3A0 File Offset: 0x000385A0
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0003A3AD File Offset: 0x000385AD
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0003A3BA File Offset: 0x000385BA
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003299 RID: 12953
		[MoonSharpHidden]
		public Text _value;
	}
}
