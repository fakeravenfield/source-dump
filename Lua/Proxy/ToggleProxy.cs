using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x02000A0C RID: 2572
	[Proxy(typeof(Toggle))]
	public class ToggleProxy : IProxy
	{
		// Token: 0x0600503C RID: 20540 RVA: 0x0003A565 File Offset: 0x00038765
		[MoonSharpHidden]
		public ToggleProxy(Toggle value)
		{
			this._value = value;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x0003A574 File Offset: 0x00038774
		// (set) Token: 0x0600503E RID: 20542 RVA: 0x0003A581 File Offset: 0x00038781
		public bool isOn
		{
			get
			{
				return this._value.isOn;
			}
			set
			{
				this._value.isOn = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x0002D8A9 File Offset: 0x0002BAA9
		public static int allSelectableCount
		{
			get
			{
				return Selectable.allSelectableCount;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06005040 RID: 20544 RVA: 0x0003A58F File Offset: 0x0003878F
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(this._value.animator);
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06005041 RID: 20545 RVA: 0x0003A5A1 File Offset: 0x000387A1
		// (set) Token: 0x06005042 RID: 20546 RVA: 0x00138760 File Offset: 0x00136960
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

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x0003A5B3 File Offset: 0x000387B3
		// (set) Token: 0x06005044 RID: 20548 RVA: 0x0003A5C0 File Offset: 0x000387C0
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

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06005045 RID: 20549 RVA: 0x0003A5CE File Offset: 0x000387CE
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x0003A5E0 File Offset: 0x000387E0
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06005047 RID: 20551 RVA: 0x0003A5F2 File Offset: 0x000387F2
		public ScriptEventProxy onValueChanged
		{
			get
			{
				return ScriptEventProxy.New(WToggle.GetOnValueChanged(this._value));
			}
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0003A604 File Offset: 0x00038804
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x00138788 File Offset: 0x00136988
		[MoonSharpHidden]
		public static ToggleProxy New(Toggle value)
		{
			if (value == null)
			{
				return null;
			}
			ToggleProxy toggleProxy = (ToggleProxy)ObjectCache.Get(typeof(ToggleProxy), value);
			if (toggleProxy == null)
			{
				toggleProxy = new ToggleProxy(value);
				ObjectCache.Add(typeof(ToggleProxy), value, toggleProxy);
			}
			return toggleProxy;
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x0003A60C File Offset: 0x0003880C
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0003A619 File Offset: 0x00038819
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0003A626 File Offset: 0x00038826
		public void SetIsOnWithoutNotify(bool value)
		{
			this._value.SetIsOnWithoutNotify(value);
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0003A634 File Offset: 0x00038834
		public bool IsInteractable()
		{
			return this._value.IsInteractable();
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x0003A641 File Offset: 0x00038841
		public void Select()
		{
			this._value.Select();
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x0003A64E File Offset: 0x0003884E
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x0003A65B File Offset: 0x0003885B
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0003A668 File Offset: 0x00038868
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400329B RID: 12955
		[MoonSharpHidden]
		public Toggle _value;
	}
}
