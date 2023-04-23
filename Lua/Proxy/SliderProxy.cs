using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x02000A01 RID: 2561
	[Proxy(typeof(Slider))]
	public class SliderProxy : IProxy
	{
		// Token: 0x06004F09 RID: 20233 RVA: 0x0003953F File Offset: 0x0003773F
		[MoonSharpHidden]
		public SliderProxy(Slider value)
		{
			this._value = value;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06004F0A RID: 20234 RVA: 0x0003954E File Offset: 0x0003774E
		// (set) Token: 0x06004F0B RID: 20235 RVA: 0x00138204 File Offset: 0x00136404
		public RectTransformProxy fillRect
		{
			get
			{
				return RectTransformProxy.New(this._value.fillRect);
			}
			set
			{
				RectTransform fillRect = null;
				if (value != null)
				{
					fillRect = value._value;
				}
				this._value.fillRect = fillRect;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06004F0C RID: 20236 RVA: 0x00039560 File Offset: 0x00037760
		// (set) Token: 0x06004F0D RID: 20237 RVA: 0x0013822C File Offset: 0x0013642C
		public RectTransformProxy handleRect
		{
			get
			{
				return RectTransformProxy.New(this._value.handleRect);
			}
			set
			{
				RectTransform handleRect = null;
				if (value != null)
				{
					handleRect = value._value;
				}
				this._value.handleRect = handleRect;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x00039572 File Offset: 0x00037772
		// (set) Token: 0x06004F0F RID: 20239 RVA: 0x0003957F File Offset: 0x0003777F
		public float maxValue
		{
			get
			{
				return this._value.maxValue;
			}
			set
			{
				this._value.maxValue = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x0003958D File Offset: 0x0003778D
		// (set) Token: 0x06004F11 RID: 20241 RVA: 0x0003959A File Offset: 0x0003779A
		public float minValue
		{
			get
			{
				return this._value.minValue;
			}
			set
			{
				this._value.minValue = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x000395A8 File Offset: 0x000377A8
		// (set) Token: 0x06004F13 RID: 20243 RVA: 0x000395B5 File Offset: 0x000377B5
		public float normalizedValue
		{
			get
			{
				return this._value.normalizedValue;
			}
			set
			{
				this._value.normalizedValue = value;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x000395C3 File Offset: 0x000377C3
		// (set) Token: 0x06004F15 RID: 20245 RVA: 0x000395D0 File Offset: 0x000377D0
		public float value
		{
			get
			{
				return this._value.value;
			}
			set
			{
				this._value.value = value;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x000395DE File Offset: 0x000377DE
		// (set) Token: 0x06004F17 RID: 20247 RVA: 0x000395EB File Offset: 0x000377EB
		public bool wholeNumbers
		{
			get
			{
				return this._value.wholeNumbers;
			}
			set
			{
				this._value.wholeNumbers = value;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0002D8A9 File Offset: 0x0002BAA9
		public static int allSelectableCount
		{
			get
			{
				return Selectable.allSelectableCount;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x000395F9 File Offset: 0x000377F9
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(this._value.animator);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x0003960B File Offset: 0x0003780B
		// (set) Token: 0x06004F1B RID: 20251 RVA: 0x00138254 File Offset: 0x00136454
		public ImageProxy image
		{
			get
			{
				return ImageProxy.New(this._value.image);
			}
			set
			{
				Image image = null;
				if (value != null)
				{
					image = value._value;
				}
				this._value.image = image;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0003961D File Offset: 0x0003781D
		// (set) Token: 0x06004F1D RID: 20253 RVA: 0x0003962A File Offset: 0x0003782A
		public bool interactable
		{
			get
			{
				return this._value.interactable;
			}
			set
			{
				this._value.interactable = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x00039638 File Offset: 0x00037838
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0003964A File Offset: 0x0003784A
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x0003965C File Offset: 0x0003785C
		public ScriptEventProxy onValueChanged
		{
			get
			{
				return ScriptEventProxy.New(WSlider.GetOnValueChanged(this._value));
			}
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x0003966E File Offset: 0x0003786E
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0013827C File Offset: 0x0013647C
		[MoonSharpHidden]
		public static SliderProxy New(Slider value)
		{
			if (value == null)
			{
				return null;
			}
			SliderProxy sliderProxy = (SliderProxy)ObjectCache.Get(typeof(SliderProxy), value);
			if (sliderProxy == null)
			{
				sliderProxy = new SliderProxy(value);
				ObjectCache.Add(typeof(SliderProxy), value, sliderProxy);
			}
			return sliderProxy;
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x00039676 File Offset: 0x00037876
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x00039683 File Offset: 0x00037883
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x00039690 File Offset: 0x00037890
		public void SetValueWithoutNotify(float input)
		{
			this._value.SetValueWithoutNotify(input);
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0003969E File Offset: 0x0003789E
		public bool IsInteractable()
		{
			return this._value.IsInteractable();
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x000396AB File Offset: 0x000378AB
		public void Select()
		{
			this._value.Select();
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x000396B8 File Offset: 0x000378B8
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000396C5 File Offset: 0x000378C5
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x000396D2 File Offset: 0x000378D2
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003290 RID: 12944
		[MoonSharpHidden]
		public Slider _value;
	}
}
