using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x020009B7 RID: 2487
	[Proxy(typeof(Button))]
	public class ButtonProxy : IProxy
	{
		// Token: 0x06004262 RID: 16994 RVA: 0x0002D89A File Offset: 0x0002BA9A
		[MoonSharpHidden]
		public ButtonProxy(Button value)
		{
			this._value = value;
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x0002D8A9 File Offset: 0x0002BAA9
		public static int allSelectableCount
		{
			get
			{
				return Selectable.allSelectableCount;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x0002D8B0 File Offset: 0x0002BAB0
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(this._value.animator);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x0002D8C2 File Offset: 0x0002BAC2
		// (set) Token: 0x06004266 RID: 16998 RVA: 0x00130078 File Offset: 0x0012E278
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

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x0002D8D4 File Offset: 0x0002BAD4
		// (set) Token: 0x06004268 RID: 17000 RVA: 0x0002D8E1 File Offset: 0x0002BAE1
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

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x0002D8EF File Offset: 0x0002BAEF
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x0002D901 File Offset: 0x0002BB01
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x0002D913 File Offset: 0x0002BB13
		public ScriptEventProxy onClick
		{
			get
			{
				return ScriptEventProxy.New(WButton.GetOnClick(this._value));
			}
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x0002D925 File Offset: 0x0002BB25
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x001300A0 File Offset: 0x0012E2A0
		[MoonSharpHidden]
		public static ButtonProxy New(Button value)
		{
			if (value == null)
			{
				return null;
			}
			ButtonProxy buttonProxy = (ButtonProxy)ObjectCache.Get(typeof(ButtonProxy), value);
			if (buttonProxy == null)
			{
				buttonProxy = new ButtonProxy(value);
				ObjectCache.Add(typeof(ButtonProxy), value, buttonProxy);
			}
			return buttonProxy;
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x0002D92D File Offset: 0x0002BB2D
		public bool IsInteractable()
		{
			return this._value.IsInteractable();
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0002D93A File Offset: 0x0002BB3A
		public void Select()
		{
			this._value.Select();
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0002D947 File Offset: 0x0002BB47
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0002D954 File Offset: 0x0002BB54
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x0002D961 File Offset: 0x0002BB61
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003150 RID: 12624
		[MoonSharpHidden]
		public Button _value;
	}
}
