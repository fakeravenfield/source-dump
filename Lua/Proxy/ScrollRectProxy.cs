using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x020009F9 RID: 2553
	[Proxy(typeof(ScrollRect))]
	public class ScrollRectProxy : IProxy
	{
		// Token: 0x06004E41 RID: 20033 RVA: 0x00038AF4 File Offset: 0x00036CF4
		[MoonSharpHidden]
		public ScrollRectProxy(ScrollRect value)
		{
			this._value = value;
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x00038B03 File Offset: 0x00036D03
		// (set) Token: 0x06004E43 RID: 20035 RVA: 0x00137DEC File Offset: 0x00135FEC
		public RectTransformProxy content
		{
			get
			{
				return RectTransformProxy.New(this._value.content);
			}
			set
			{
				RectTransform content = null;
				if (value != null)
				{
					content = value._value;
				}
				this._value.content = content;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x00038B15 File Offset: 0x00036D15
		// (set) Token: 0x06004E45 RID: 20037 RVA: 0x00038B22 File Offset: 0x00036D22
		public float decelerationRate
		{
			get
			{
				return this._value.decelerationRate;
			}
			set
			{
				this._value.decelerationRate = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x00038B30 File Offset: 0x00036D30
		// (set) Token: 0x06004E47 RID: 20039 RVA: 0x00038B3D File Offset: 0x00036D3D
		public float elasticity
		{
			get
			{
				return this._value.elasticity;
			}
			set
			{
				this._value.elasticity = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x00038B4B File Offset: 0x00036D4B
		public float flexibleHeight
		{
			get
			{
				return this._value.flexibleHeight;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06004E49 RID: 20041 RVA: 0x00038B58 File Offset: 0x00036D58
		public float flexibleWidth
		{
			get
			{
				return this._value.flexibleWidth;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x00038B65 File Offset: 0x00036D65
		// (set) Token: 0x06004E4B RID: 20043 RVA: 0x00038B72 File Offset: 0x00036D72
		public bool horizontal
		{
			get
			{
				return this._value.horizontal;
			}
			set
			{
				this._value.horizontal = value;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x00038B80 File Offset: 0x00036D80
		// (set) Token: 0x06004E4D RID: 20045 RVA: 0x00038B8D File Offset: 0x00036D8D
		public float horizontalNormalizedPosition
		{
			get
			{
				return this._value.horizontalNormalizedPosition;
			}
			set
			{
				this._value.horizontalNormalizedPosition = value;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x00038B9B File Offset: 0x00036D9B
		// (set) Token: 0x06004E4F RID: 20047 RVA: 0x00038BA8 File Offset: 0x00036DA8
		public float horizontalScrollbarSpacing
		{
			get
			{
				return this._value.horizontalScrollbarSpacing;
			}
			set
			{
				this._value.horizontalScrollbarSpacing = value;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x00038BB6 File Offset: 0x00036DB6
		// (set) Token: 0x06004E51 RID: 20049 RVA: 0x00038BC3 File Offset: 0x00036DC3
		public bool inertia
		{
			get
			{
				return this._value.inertia;
			}
			set
			{
				this._value.inertia = value;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x00038BD1 File Offset: 0x00036DD1
		public int layoutPriority
		{
			get
			{
				return this._value.layoutPriority;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00038BDE File Offset: 0x00036DDE
		public float minHeight
		{
			get
			{
				return this._value.minHeight;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x00038BEB File Offset: 0x00036DEB
		public float minWidth
		{
			get
			{
				return this._value.minWidth;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004E55 RID: 20053 RVA: 0x00038BF8 File Offset: 0x00036DF8
		// (set) Token: 0x06004E56 RID: 20054 RVA: 0x00038C0A File Offset: 0x00036E0A
		public Vector2Proxy normalizedPosition
		{
			get
			{
				return Vector2Proxy.New(this._value.normalizedPosition);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.normalizedPosition = value._value;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06004E57 RID: 20055 RVA: 0x00038C2B File Offset: 0x00036E2B
		public float preferredHeight
		{
			get
			{
				return this._value.preferredHeight;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x00038C38 File Offset: 0x00036E38
		public float preferredWidth
		{
			get
			{
				return this._value.preferredWidth;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x00038C45 File Offset: 0x00036E45
		// (set) Token: 0x06004E5A RID: 20058 RVA: 0x00038C52 File Offset: 0x00036E52
		public float scrollSensitivity
		{
			get
			{
				return this._value.scrollSensitivity;
			}
			set
			{
				this._value.scrollSensitivity = value;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x00038C60 File Offset: 0x00036E60
		// (set) Token: 0x06004E5C RID: 20060 RVA: 0x00038C72 File Offset: 0x00036E72
		public Vector2Proxy velocity
		{
			get
			{
				return Vector2Proxy.New(this._value.velocity);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.velocity = value._value;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x00038C93 File Offset: 0x00036E93
		// (set) Token: 0x06004E5E RID: 20062 RVA: 0x00038CA0 File Offset: 0x00036EA0
		public bool vertical
		{
			get
			{
				return this._value.vertical;
			}
			set
			{
				this._value.vertical = value;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x00038CAE File Offset: 0x00036EAE
		// (set) Token: 0x06004E60 RID: 20064 RVA: 0x00038CBB File Offset: 0x00036EBB
		public float verticalNormalizedPosition
		{
			get
			{
				return this._value.verticalNormalizedPosition;
			}
			set
			{
				this._value.verticalNormalizedPosition = value;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x00038CC9 File Offset: 0x00036EC9
		// (set) Token: 0x06004E62 RID: 20066 RVA: 0x00038CD6 File Offset: 0x00036ED6
		public float verticalScrollbarSpacing
		{
			get
			{
				return this._value.verticalScrollbarSpacing;
			}
			set
			{
				this._value.verticalScrollbarSpacing = value;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x00038CE4 File Offset: 0x00036EE4
		// (set) Token: 0x06004E64 RID: 20068 RVA: 0x00137E14 File Offset: 0x00136014
		public RectTransformProxy viewport
		{
			get
			{
				return RectTransformProxy.New(this._value.viewport);
			}
			set
			{
				RectTransform viewport = null;
				if (value != null)
				{
					viewport = value._value;
				}
				this._value.viewport = viewport;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x00038CF6 File Offset: 0x00036EF6
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x00038D08 File Offset: 0x00036F08
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x00038D1A File Offset: 0x00036F1A
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00137E3C File Offset: 0x0013603C
		[MoonSharpHidden]
		public static ScrollRectProxy New(ScrollRect value)
		{
			if (value == null)
			{
				return null;
			}
			ScrollRectProxy scrollRectProxy = (ScrollRectProxy)ObjectCache.Get(typeof(ScrollRectProxy), value);
			if (scrollRectProxy == null)
			{
				scrollRectProxy = new ScrollRectProxy(value);
				ObjectCache.Add(typeof(ScrollRectProxy), value, scrollRectProxy);
			}
			return scrollRectProxy;
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x00038D22 File Offset: 0x00036F22
		public void CalculateLayoutInputHorizontal()
		{
			this._value.CalculateLayoutInputHorizontal();
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x00038D2F File Offset: 0x00036F2F
		public void CalculateLayoutInputVertical()
		{
			this._value.CalculateLayoutInputVertical();
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x00038D3C File Offset: 0x00036F3C
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x00038D49 File Offset: 0x00036F49
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x00038D56 File Offset: 0x00036F56
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x00038D63 File Offset: 0x00036F63
		public void SetLayoutHorizontal()
		{
			this._value.SetLayoutHorizontal();
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x00038D70 File Offset: 0x00036F70
		public void SetLayoutVertical()
		{
			this._value.SetLayoutVertical();
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x00038D7D File Offset: 0x00036F7D
		public void StopMovement()
		{
			this._value.StopMovement();
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x00038D8A File Offset: 0x00036F8A
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00038D97 File Offset: 0x00036F97
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003288 RID: 12936
		[MoonSharpHidden]
		public ScrollRect _value;
	}
}
