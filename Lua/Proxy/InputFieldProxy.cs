using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine.UI;

namespace Lua.Proxy
{
	// Token: 0x020009CD RID: 2509
	[Proxy(typeof(InputField))]
	public class InputFieldProxy : IProxy
	{
		// Token: 0x060045AE RID: 17838 RVA: 0x000308B5 File Offset: 0x0002EAB5
		[MoonSharpHidden]
		public InputFieldProxy(InputField value)
		{
			this._value = value;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060045AF RID: 17839 RVA: 0x000308C4 File Offset: 0x0002EAC4
		// (set) Token: 0x060045B0 RID: 17840 RVA: 0x000308D1 File Offset: 0x0002EAD1
		public char asteriskChar
		{
			get
			{
				return this._value.asteriskChar;
			}
			set
			{
				this._value.asteriskChar = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x000308DF File Offset: 0x0002EADF
		// (set) Token: 0x060045B2 RID: 17842 RVA: 0x000308EC File Offset: 0x0002EAEC
		public float caretBlinkRate
		{
			get
			{
				return this._value.caretBlinkRate;
			}
			set
			{
				this._value.caretBlinkRate = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060045B3 RID: 17843 RVA: 0x000308FA File Offset: 0x0002EAFA
		// (set) Token: 0x060045B4 RID: 17844 RVA: 0x0003090C File Offset: 0x0002EB0C
		public ColorProxy caretColor
		{
			get
			{
				return ColorProxy.New(this._value.caretColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.caretColor = value._value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x0003092D File Offset: 0x0002EB2D
		// (set) Token: 0x060045B6 RID: 17846 RVA: 0x0003093A File Offset: 0x0002EB3A
		public int caretPosition
		{
			get
			{
				return this._value.caretPosition;
			}
			set
			{
				this._value.caretPosition = value;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x00030948 File Offset: 0x0002EB48
		// (set) Token: 0x060045B8 RID: 17848 RVA: 0x00030955 File Offset: 0x0002EB55
		public int caretWidth
		{
			get
			{
				return this._value.caretWidth;
			}
			set
			{
				this._value.caretWidth = value;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x00030963 File Offset: 0x0002EB63
		// (set) Token: 0x060045BA RID: 17850 RVA: 0x00030970 File Offset: 0x0002EB70
		public int characterLimit
		{
			get
			{
				return this._value.characterLimit;
			}
			set
			{
				this._value.characterLimit = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060045BB RID: 17851 RVA: 0x0003097E File Offset: 0x0002EB7E
		// (set) Token: 0x060045BC RID: 17852 RVA: 0x0003098B File Offset: 0x0002EB8B
		public bool customCaretColor
		{
			get
			{
				return this._value.customCaretColor;
			}
			set
			{
				this._value.customCaretColor = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x00030999 File Offset: 0x0002EB99
		public float flexibleHeight
		{
			get
			{
				return this._value.flexibleHeight;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000309A6 File Offset: 0x0002EBA6
		public float flexibleWidth
		{
			get
			{
				return this._value.flexibleWidth;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000309B3 File Offset: 0x0002EBB3
		public bool isFocused
		{
			get
			{
				return this._value.isFocused;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000309C0 File Offset: 0x0002EBC0
		public int layoutPriority
		{
			get
			{
				return this._value.layoutPriority;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000309CD File Offset: 0x0002EBCD
		public float minHeight
		{
			get
			{
				return this._value.minHeight;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000309DA File Offset: 0x0002EBDA
		public float minWidth
		{
			get
			{
				return this._value.minWidth;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x000309E7 File Offset: 0x0002EBE7
		public bool multiLine
		{
			get
			{
				return this._value.multiLine;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x000309F4 File Offset: 0x0002EBF4
		public float preferredHeight
		{
			get
			{
				return this._value.preferredHeight;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x00030A01 File Offset: 0x0002EC01
		public float preferredWidth
		{
			get
			{
				return this._value.preferredWidth;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x00030A0E File Offset: 0x0002EC0E
		// (set) Token: 0x060045C7 RID: 17863 RVA: 0x00030A1B File Offset: 0x0002EC1B
		public bool readOnly
		{
			get
			{
				return this._value.readOnly;
			}
			set
			{
				this._value.readOnly = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x00030A29 File Offset: 0x0002EC29
		// (set) Token: 0x060045C9 RID: 17865 RVA: 0x00030A36 File Offset: 0x0002EC36
		public int selectionAnchorPosition
		{
			get
			{
				return this._value.selectionAnchorPosition;
			}
			set
			{
				this._value.selectionAnchorPosition = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x00030A44 File Offset: 0x0002EC44
		// (set) Token: 0x060045CB RID: 17867 RVA: 0x00030A56 File Offset: 0x0002EC56
		public ColorProxy selectionColor
		{
			get
			{
				return ColorProxy.New(this._value.selectionColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.selectionColor = value._value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x00030A77 File Offset: 0x0002EC77
		// (set) Token: 0x060045CD RID: 17869 RVA: 0x00030A84 File Offset: 0x0002EC84
		public int selectionFocusPosition
		{
			get
			{
				return this._value.selectionFocusPosition;
			}
			set
			{
				this._value.selectionFocusPosition = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x00030A92 File Offset: 0x0002EC92
		// (set) Token: 0x060045CF RID: 17871 RVA: 0x00030A9F File Offset: 0x0002EC9F
		public bool shouldActivateOnSelect
		{
			get
			{
				return this._value.shouldActivateOnSelect;
			}
			set
			{
				this._value.shouldActivateOnSelect = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x00030AAD File Offset: 0x0002ECAD
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x00030ABA File Offset: 0x0002ECBA
		public bool shouldHideMobileInput
		{
			get
			{
				return this._value.shouldHideMobileInput;
			}
			set
			{
				this._value.shouldHideMobileInput = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00030AC8 File Offset: 0x0002ECC8
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x00030AD5 File Offset: 0x0002ECD5
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

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x00030AE3 File Offset: 0x0002ECE3
		// (set) Token: 0x060045D5 RID: 17877 RVA: 0x00130CB8 File Offset: 0x0012EEB8
		public TextProxy textComponent
		{
			get
			{
				return TextProxy.New(this._value.textComponent);
			}
			set
			{
				Text textComponent = null;
				if (value != null)
				{
					textComponent = value._value;
				}
				this._value.textComponent = textComponent;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x00030AF5 File Offset: 0x0002ECF5
		public bool wasCanceled
		{
			get
			{
				return this._value.wasCanceled;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x0002D8A9 File Offset: 0x0002BAA9
		public static int allSelectableCount
		{
			get
			{
				return Selectable.allSelectableCount;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00030B02 File Offset: 0x0002ED02
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(this._value.animator);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x00030B14 File Offset: 0x0002ED14
		// (set) Token: 0x060045DA RID: 17882 RVA: 0x00130CE0 File Offset: 0x0012EEE0
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

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x00030B26 File Offset: 0x0002ED26
		// (set) Token: 0x060045DC RID: 17884 RVA: 0x00030B33 File Offset: 0x0002ED33
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

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060045DD RID: 17885 RVA: 0x00030B41 File Offset: 0x0002ED41
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x00030B53 File Offset: 0x0002ED53
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060045DF RID: 17887 RVA: 0x00030B65 File Offset: 0x0002ED65
		public ScriptEventProxy onValueChanged
		{
			get
			{
				return ScriptEventProxy.New(WInputField.GetOnValueChanged(this._value));
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00030B77 File Offset: 0x0002ED77
		public ScriptEventProxy onEndEdit
		{
			get
			{
				return ScriptEventProxy.New(WInputField.OnEndEdit(this._value));
			}
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x00030B89 File Offset: 0x0002ED89
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x00130D08 File Offset: 0x0012EF08
		[MoonSharpHidden]
		public static InputFieldProxy New(InputField value)
		{
			if (value == null)
			{
				return null;
			}
			InputFieldProxy inputFieldProxy = (InputFieldProxy)ObjectCache.Get(typeof(InputFieldProxy), value);
			if (inputFieldProxy == null)
			{
				inputFieldProxy = new InputFieldProxy(value);
				ObjectCache.Add(typeof(InputFieldProxy), value, inputFieldProxy);
			}
			return inputFieldProxy;
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x00030B91 File Offset: 0x0002ED91
		public void ActivateInputField()
		{
			this._value.ActivateInputField();
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x00030B9E File Offset: 0x0002ED9E
		public void CalculateLayoutInputHorizontal()
		{
			this._value.CalculateLayoutInputHorizontal();
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x00030BAB File Offset: 0x0002EDAB
		public void CalculateLayoutInputVertical()
		{
			this._value.CalculateLayoutInputVertical();
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x00030BB8 File Offset: 0x0002EDB8
		public void DeactivateInputField()
		{
			this._value.DeactivateInputField();
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x00030BC5 File Offset: 0x0002EDC5
		public void ForceLabelUpdate()
		{
			this._value.ForceLabelUpdate();
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x00030BD2 File Offset: 0x0002EDD2
		public void GraphicUpdateComplete()
		{
			this._value.GraphicUpdateComplete();
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x00030BDF File Offset: 0x0002EDDF
		public void LayoutComplete()
		{
			this._value.LayoutComplete();
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x00030BEC File Offset: 0x0002EDEC
		public void MoveTextEnd(bool shift)
		{
			this._value.MoveTextEnd(shift);
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00030BFA File Offset: 0x0002EDFA
		public void MoveTextStart(bool shift)
		{
			this._value.MoveTextStart(shift);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00030C08 File Offset: 0x0002EE08
		public void SetTextWithoutNotify(string input)
		{
			this._value.SetTextWithoutNotify(input);
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00030C16 File Offset: 0x0002EE16
		public bool IsInteractable()
		{
			return this._value.IsInteractable();
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00030C23 File Offset: 0x0002EE23
		public void Select()
		{
			this._value.Select();
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00030C30 File Offset: 0x0002EE30
		public bool IsActive()
		{
			return this._value.IsActive();
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x00030C3D File Offset: 0x0002EE3D
		public bool IsDestroyed()
		{
			return this._value.IsDestroyed();
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00030C4A File Offset: 0x0002EE4A
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003165 RID: 12645
		[MoonSharpHidden]
		public InputField _value;
	}
}
