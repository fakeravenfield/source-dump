using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x020009CC RID: 2508
	[Proxy(typeof(Image))]
	public class ImageProxy : IProxy
	{
		// Token: 0x0600455B RID: 17755 RVA: 0x000303DE File Offset: 0x0002E5DE
		[MoonSharpHidden]
		public ImageProxy(Image value)
		{
			this._value = value;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x000303ED File Offset: 0x0002E5ED
		// (set) Token: 0x0600455D RID: 17757 RVA: 0x000303FA File Offset: 0x0002E5FA
		public float alphaHitTestMinimumThreshold
		{
			get
			{
				return this._value.alphaHitTestMinimumThreshold;
			}
			set
			{
				this._value.alphaHitTestMinimumThreshold = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x00030408 File Offset: 0x0002E608
		public static MaterialProxy defaultETC1GraphicMaterial
		{
			get
			{
				return MaterialProxy.New(Image.defaultETC1GraphicMaterial);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x00030414 File Offset: 0x0002E614
		// (set) Token: 0x06004560 RID: 17760 RVA: 0x00030421 File Offset: 0x0002E621
		public float fillAmount
		{
			get
			{
				return this._value.fillAmount;
			}
			set
			{
				this._value.fillAmount = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x0003042F File Offset: 0x0002E62F
		// (set) Token: 0x06004562 RID: 17762 RVA: 0x0003043C File Offset: 0x0002E63C
		public bool fillCenter
		{
			get
			{
				return this._value.fillCenter;
			}
			set
			{
				this._value.fillCenter = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x0003044A File Offset: 0x0002E64A
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x00030457 File Offset: 0x0002E657
		public bool fillClockwise
		{
			get
			{
				return this._value.fillClockwise;
			}
			set
			{
				this._value.fillClockwise = value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x00030465 File Offset: 0x0002E665
		// (set) Token: 0x06004566 RID: 17766 RVA: 0x00030472 File Offset: 0x0002E672
		public int fillOrigin
		{
			get
			{
				return this._value.fillOrigin;
			}
			set
			{
				this._value.fillOrigin = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x00030480 File Offset: 0x0002E680
		public float flexibleHeight
		{
			get
			{
				return this._value.flexibleHeight;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x0003048D File Offset: 0x0002E68D
		public float flexibleWidth
		{
			get
			{
				return this._value.flexibleWidth;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x0003049A File Offset: 0x0002E69A
		public bool hasBorder
		{
			get
			{
				return this._value.hasBorder;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x000304A7 File Offset: 0x0002E6A7
		public int layoutPriority
		{
			get
			{
				return this._value.layoutPriority;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x000304B4 File Offset: 0x0002E6B4
		public TextureProxy mainTexture
		{
			get
			{
				return TextureProxy.New(this._value.mainTexture);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000304C6 File Offset: 0x0002E6C6
		// (set) Token: 0x0600456D RID: 17773 RVA: 0x00130B50 File Offset: 0x0012ED50
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

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x000304D8 File Offset: 0x0002E6D8
		public float minHeight
		{
			get
			{
				return this._value.minHeight;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600456F RID: 17775 RVA: 0x000304E5 File Offset: 0x0002E6E5
		public float minWidth
		{
			get
			{
				return this._value.minWidth;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x000304F2 File Offset: 0x0002E6F2
		// (set) Token: 0x06004571 RID: 17777 RVA: 0x00130B78 File Offset: 0x0012ED78
		public SpriteProxy overrideSprite
		{
			get
			{
				return SpriteProxy.New(this._value.overrideSprite);
			}
			set
			{
				Sprite overrideSprite = null;
				if (value != null)
				{
					overrideSprite = value._value;
				}
				this._value.overrideSprite = overrideSprite;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x00030504 File Offset: 0x0002E704
		public float pixelsPerUnit
		{
			get
			{
				return this._value.pixelsPerUnit;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x00030511 File Offset: 0x0002E711
		// (set) Token: 0x06004574 RID: 17780 RVA: 0x0003051E File Offset: 0x0002E71E
		public float pixelsPerUnitMultiplier
		{
			get
			{
				return this._value.pixelsPerUnitMultiplier;
			}
			set
			{
				this._value.pixelsPerUnitMultiplier = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x0003052C File Offset: 0x0002E72C
		public float preferredHeight
		{
			get
			{
				return this._value.preferredHeight;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x00030539 File Offset: 0x0002E739
		public float preferredWidth
		{
			get
			{
				return this._value.preferredWidth;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x00030546 File Offset: 0x0002E746
		// (set) Token: 0x06004578 RID: 17784 RVA: 0x00030553 File Offset: 0x0002E753
		public bool preserveAspect
		{
			get
			{
				return this._value.preserveAspect;
			}
			set
			{
				this._value.preserveAspect = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x00030561 File Offset: 0x0002E761
		// (set) Token: 0x0600457A RID: 17786 RVA: 0x00130BA0 File Offset: 0x0012EDA0
		public SpriteProxy sprite
		{
			get
			{
				return SpriteProxy.New(this._value.sprite);
			}
			set
			{
				Sprite sprite = null;
				if (value != null)
				{
					sprite = value._value;
				}
				this._value.sprite = sprite;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x00030573 File Offset: 0x0002E773
		// (set) Token: 0x0600457C RID: 17788 RVA: 0x00030580 File Offset: 0x0002E780
		public bool useSpriteMesh
		{
			get
			{
				return this._value.useSpriteMesh;
			}
			set
			{
				this._value.useSpriteMesh = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x0003058E File Offset: 0x0002E78E
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x0003059B File Offset: 0x0002E79B
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

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x000305A9 File Offset: 0x0002E7A9
		// (set) Token: 0x06004580 RID: 17792 RVA: 0x000305B6 File Offset: 0x0002E7B6
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

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x000305C4 File Offset: 0x0002E7C4
		public CanvasProxy canvas
		{
			get
			{
				return CanvasProxy.New(this._value.canvas);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x000305D6 File Offset: 0x0002E7D6
		// (set) Token: 0x06004583 RID: 17795 RVA: 0x000305E8 File Offset: 0x0002E7E8
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

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x00030609 File Offset: 0x0002E809
		public static MaterialProxy defaultGraphicMaterial
		{
			get
			{
				return MaterialProxy.New(Graphic.defaultGraphicMaterial);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x00030615 File Offset: 0x0002E815
		public MaterialProxy defaultMaterial
		{
			get
			{
				return MaterialProxy.New(this._value.defaultMaterial);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x00030627 File Offset: 0x0002E827
		public int depth
		{
			get
			{
				return this._value.depth;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x00030634 File Offset: 0x0002E834
		public MaterialProxy materialForRendering
		{
			get
			{
				return MaterialProxy.New(this._value.materialForRendering);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x00030646 File Offset: 0x0002E846
		// (set) Token: 0x06004589 RID: 17801 RVA: 0x00030658 File Offset: 0x0002E858
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

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00030679 File Offset: 0x0002E879
		// (set) Token: 0x0600458B RID: 17803 RVA: 0x00030686 File Offset: 0x0002E886
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

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x00030694 File Offset: 0x0002E894
		public RectTransformProxy rectTransform
		{
			get
			{
				return RectTransformProxy.New(this._value.rectTransform);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x000306A6 File Offset: 0x0002E8A6
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x000306B8 File Offset: 0x0002E8B8
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000306CA File Offset: 0x0002E8CA
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00130BC8 File Offset: 0x0012EDC8
		[MoonSharpHidden]
		public static ImageProxy New(Image value)
		{
			if (value == null)
			{
				return null;
			}
			ImageProxy imageProxy = (ImageProxy)ObjectCache.Get(typeof(ImageProxy), value);
			if (imageProxy == null)
			{
				imageProxy = new ImageProxy(value);
				ObjectCache.Add(typeof(ImageProxy), value, imageProxy);
			}
			return imageProxy;
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000306D2 File Offset: 0x0002E8D2
		public void CalculateLayoutInputHorizontal()
		{
			this._value.CalculateLayoutInputHorizontal();
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x000306DF File Offset: 0x0002E8DF
		public void CalculateLayoutInputVertical()
		{
			this._value.CalculateLayoutInputVertical();
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x000306EC File Offset: 0x0002E8EC
		public void DisableSpriteOptimizations()
		{
			this._value.DisableSpriteOptimizations();
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00130C14 File Offset: 0x0012EE14
		public bool IsRaycastLocationValid(Vector2Proxy screenPoint, CameraProxy eventCamera)
		{
			if (screenPoint == null)
			{
				throw new ScriptRuntimeException("argument 'screenPoint' is nil");
			}
			Camera eventCamera2 = null;
			if (eventCamera != null)
			{
				eventCamera2 = eventCamera._value;
			}
			return this._value.IsRaycastLocationValid(screenPoint._value, eventCamera2);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x000306F9 File Offset: 0x0002E8F9
		public void OnAfterDeserialize()
		{
			this._value.OnAfterDeserialize();
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x00030706 File Offset: 0x0002E906
		public void OnBeforeSerialize()
		{
			this._value.OnBeforeSerialize();
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x00030713 File Offset: 0x0002E913
		public void SetNativeSize()
		{
			this._value.SetNativeSize();
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x00030720 File Offset: 0x0002E920
		public void Cull(RectProxy clipRect, bool validRect)
		{
			if (clipRect == null)
			{
				throw new ScriptRuntimeException("argument 'clipRect' is nil");
			}
			this._value.Cull(clipRect._value, validRect);
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x00130C50 File Offset: 0x0012EE50
		public MaterialProxy GetModifiedMaterial(MaterialProxy baseMaterial)
		{
			Material baseMaterial2 = null;
			if (baseMaterial != null)
			{
				baseMaterial2 = baseMaterial._value;
			}
			return MaterialProxy.New(this._value.GetModifiedMaterial(baseMaterial2));
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x00030742 File Offset: 0x0002E942
		public void RecalculateClipping()
		{
			this._value.RecalculateClipping();
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x0003074F File Offset: 0x0002E94F
		public void RecalculateMasking()
		{
			this._value.RecalculateMasking();
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x0003075C File Offset: 0x0002E95C
		public void SetClipRect(RectProxy clipRect, bool validRect)
		{
			if (clipRect == null)
			{
				throw new ScriptRuntimeException("argument 'clipRect' is nil");
			}
			this._value.SetClipRect(clipRect._value, validRect);
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x0003077E File Offset: 0x0002E97E
		public void SetClipSoftness(Vector2Proxy clipSoftness)
		{
			if (clipSoftness == null)
			{
				throw new ScriptRuntimeException("argument 'clipSoftness' is nil");
			}
			this._value.SetClipSoftness(clipSoftness._value);
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x0003079F File Offset: 0x0002E99F
		public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this._value.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x000307AF File Offset: 0x0002E9AF
		public void CrossFadeColor(ColorProxy targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			if (targetColor == null)
			{
				throw new ScriptRuntimeException("argument 'targetColor' is nil");
			}
			this._value.CrossFadeColor(targetColor._value, duration, ignoreTimeScale, useAlpha);
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000307D4 File Offset: 0x0002E9D4
		public void CrossFadeColor(ColorProxy targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (targetColor == null)
			{
				throw new ScriptRuntimeException("argument 'targetColor' is nil");
			}
			this._value.CrossFadeColor(targetColor._value, duration, ignoreTimeScale, useAlpha, useRGB);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x000307FB File Offset: 0x0002E9FB
		public RectProxy GetPixelAdjustedRect()
		{
			return RectProxy.New(this._value.GetPixelAdjustedRect());
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x0003080D File Offset: 0x0002EA0D
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x0003081A File Offset: 0x0002EA1A
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00030827 File Offset: 0x0002EA27
		public void OnCullingChanged()
		{
			this._value.OnCullingChanged();
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00030834 File Offset: 0x0002EA34
		public Vector2Proxy PixelAdjustPoint(Vector2Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector2Proxy.New(this._value.PixelAdjustPoint(point._value));
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00130C7C File Offset: 0x0012EE7C
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

		// Token: 0x060045A7 RID: 17831 RVA: 0x0003085A File Offset: 0x0002EA5A
		public void SetAllDirty()
		{
			this._value.SetAllDirty();
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00030867 File Offset: 0x0002EA67
		public void SetLayoutDirty()
		{
			this._value.SetLayoutDirty();
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00030874 File Offset: 0x0002EA74
		public void SetMaterialDirty()
		{
			this._value.SetMaterialDirty();
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x00030881 File Offset: 0x0002EA81
		public void SetVerticesDirty()
		{
			this._value.SetVerticesDirty();
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x0003088E File Offset: 0x0002EA8E
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0003089B File Offset: 0x0002EA9B
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x000308A8 File Offset: 0x0002EAA8
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003164 RID: 12644
		[MoonSharpHidden]
		public Image _value;
	}
}
