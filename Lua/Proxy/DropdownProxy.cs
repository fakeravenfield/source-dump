using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x020009C1 RID: 2497
	[Proxy(typeof(Dropdown))]
	public class DropdownProxy : IProxy
	{
		// Token: 0x06004426 RID: 17446 RVA: 0x0002F4A6 File Offset: 0x0002D6A6
		[MoonSharpHidden]
		public DropdownProxy(Dropdown value)
		{
			this._value = value;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x0002F4B5 File Offset: 0x0002D6B5
		// (set) Token: 0x06004428 RID: 17448 RVA: 0x0002F4C2 File Offset: 0x0002D6C2
		public float alphaFadeSpeed
		{
			get
			{
				return this._value.alphaFadeSpeed;
			}
			set
			{
				this._value.alphaFadeSpeed = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06004429 RID: 17449 RVA: 0x0002F4D0 File Offset: 0x0002D6D0
		// (set) Token: 0x0600442A RID: 17450 RVA: 0x00130438 File Offset: 0x0012E638
		public ImageProxy captionImage
		{
			get
			{
				return ImageProxy.New(this._value.captionImage);
			}
			set
			{
				Image captionImage = null;
				if (value != null)
				{
					captionImage = value._value;
				}
				this._value.captionImage = captionImage;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x0002F4E2 File Offset: 0x0002D6E2
		// (set) Token: 0x0600442C RID: 17452 RVA: 0x00130460 File Offset: 0x0012E660
		public TextProxy captionText
		{
			get
			{
				return TextProxy.New(this._value.captionText);
			}
			set
			{
				Text captionText = null;
				if (value != null)
				{
					captionText = value._value;
				}
				this._value.captionText = captionText;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600442D RID: 17453 RVA: 0x0002F4F4 File Offset: 0x0002D6F4
		// (set) Token: 0x0600442E RID: 17454 RVA: 0x00130488 File Offset: 0x0012E688
		public ImageProxy itemImage
		{
			get
			{
				return ImageProxy.New(this._value.itemImage);
			}
			set
			{
				Image itemImage = null;
				if (value != null)
				{
					itemImage = value._value;
				}
				this._value.itemImage = itemImage;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x0002F506 File Offset: 0x0002D706
		// (set) Token: 0x06004430 RID: 17456 RVA: 0x001304B0 File Offset: 0x0012E6B0
		public TextProxy itemText
		{
			get
			{
				return TextProxy.New(this._value.itemText);
			}
			set
			{
				Text itemText = null;
				if (value != null)
				{
					itemText = value._value;
				}
				this._value.itemText = itemText;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x0002F518 File Offset: 0x0002D718
		// (set) Token: 0x06004432 RID: 17458 RVA: 0x0002F525 File Offset: 0x0002D725
		public int value
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

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x0002D8A9 File Offset: 0x0002BAA9
		public static int allSelectableCount
		{
			get
			{
				return Selectable.allSelectableCount;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x0002F533 File Offset: 0x0002D733
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(this._value.animator);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x0002F545 File Offset: 0x0002D745
		// (set) Token: 0x06004436 RID: 17462 RVA: 0x001304D8 File Offset: 0x0012E6D8
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

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x0002F557 File Offset: 0x0002D757
		// (set) Token: 0x06004438 RID: 17464 RVA: 0x0002F564 File Offset: 0x0002D764
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

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x0002F572 File Offset: 0x0002D772
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x0002F584 File Offset: 0x0002D784
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x0002F596 File Offset: 0x0002D796
		public ScriptEventProxy onValueChanged
		{
			get
			{
				return ScriptEventProxy.New(WDropdown.GetOnValueChanged(this._value));
			}
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00130500 File Offset: 0x0012E700
		[MoonSharpHidden]
		public static DropdownProxy New(Dropdown value)
		{
			if (value == null)
			{
				return null;
			}
			DropdownProxy dropdownProxy = (DropdownProxy)ObjectCache.Get(typeof(DropdownProxy), value);
			if (dropdownProxy == null)
			{
				dropdownProxy = new DropdownProxy(value);
				ObjectCache.Add(typeof(DropdownProxy), value, dropdownProxy);
			}
			return dropdownProxy;
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		public void AddOptions(List<string> options)
		{
			this._value.AddOptions(options);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x0002F5BE File Offset: 0x0002D7BE
		public void ClearOptions()
		{
			this._value.ClearOptions();
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x0002F5CB File Offset: 0x0002D7CB
		public void Hide()
		{
			this._value.Hide();
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x0002F5D8 File Offset: 0x0002D7D8
		public void RefreshShownValue()
		{
			this._value.RefreshShownValue();
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x0002F5E5 File Offset: 0x0002D7E5
		public void SetValueWithoutNotify(int input)
		{
			this._value.SetValueWithoutNotify(input);
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x0002F5F3 File Offset: 0x0002D7F3
		public void Show()
		{
			this._value.Show();
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x0002F600 File Offset: 0x0002D800
		public bool IsInteractable()
		{
			return this._value.IsInteractable();
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x0002F60D File Offset: 0x0002D80D
		public void Select()
		{
			this._value.Select();
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x0002F61A File Offset: 0x0002D81A
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x0002F627 File Offset: 0x0002D827
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x0002F634 File Offset: 0x0002D834
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400315A RID: 12634
		[MoonSharpHidden]
		public Dropdown _value;
	}
}
