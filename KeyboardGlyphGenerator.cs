using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AC RID: 684
public class KeyboardGlyphGenerator : MonoBehaviour
{
	// Token: 0x06001224 RID: 4644 RVA: 0x0008E3A0 File Offset: 0x0008C5A0
	private void Awake()
	{
		KeyboardGlyphGenerator.instance = this;
		this.glyphLeft = new KeyboardGlyphGenerator.GlyphElements(this.leftGlyphContainer, this.defaultKeyLabelFont);
		this.glyphRight = new KeyboardGlyphGenerator.GlyphElements(this.rightGlyphContainer, this.defaultKeyLabelFont);
		this.glyphCenter = new KeyboardGlyphGenerator.GlyphElements(this.centerGlyphContainer, this.defaultKeyLabelFont);
		this.compoundLabel.gameObject.SetActive(false);
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		KeyboardGlyphGenerator.ForceHide();
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x0008E41C File Offset: 0x0008C61C
	private void Update()
	{
		if (!this.fadeInAction.TrueDone())
		{
			this.canvasGroup.alpha = this.fadeInAction.Ratio();
		}
		else
		{
			this.canvasGroup.alpha = 1f;
		}
		if (this.autoHideAction.TrueDone())
		{
			KeyboardGlyphGenerator.ForceHide();
		}
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x0000E43D File Offset: 0x0000C63D
	private void StartAutoHideTimer()
	{
		this.autoHideAction.Start();
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0008E470 File Offset: 0x0008C670
	public int FadeIn()
	{
		base.gameObject.SetActive(true);
		this.canvasGroup.alpha = 0f;
		this.fadeInAction.Start();
		this.autoHideAction.Start();
		this.currentDisplayIndex = (this.currentDisplayIndex + 1) % int.MaxValue;
		return this.currentDisplayIndex;
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x0000E44A File Offset: 0x0000C64A
	public static void Hide(int displayIndex)
	{
		if (displayIndex == KeyboardGlyphGenerator.instance.currentDisplayIndex)
		{
			KeyboardGlyphGenerator.ForceHide();
		}
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x0000E45E File Offset: 0x0000C65E
	public static void ForceHide()
	{
		if (KeyboardGlyphGenerator.instance != null)
		{
			KeyboardGlyphGenerator.instance.gameObject.SetActive(false);
			KeyboardGlyphGenerator.instance.currentDisplayIndex = -1;
			KeyboardGlyphGenerator.instance.currentDisplayingBinds = default(KeyboardGlyphGenerator.DisplayedBinds);
		}
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x0000E498 File Offset: 0x0000C698
	public static bool IsHidden()
	{
		return !KeyboardGlyphGenerator.instance.gameObject.activeSelf;
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x0008E4CC File Offset: 0x0008C6CC
	private Texture2D GetKeyGraphic(KeyCode keyCode)
	{
		if (keyCode == KeyCode.Return)
		{
			return this.database.keyReturn;
		}
		if (KeyboardGlyphGenerator.TALL_KEYS.Contains(keyCode))
		{
			return this.database.keyTall;
		}
		if (KeyboardGlyphGenerator.ULTRAWIDE_KEYS.Contains(keyCode))
		{
			return this.database.keyUltraWide;
		}
		if (KeyboardGlyphGenerator.WIDE_KEYS.Contains(keyCode))
		{
			return this.database.keyWide;
		}
		return this.database.keySmall;
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x0008E540 File Offset: 0x0008C740
	public int ShowCombinationBind(SteelInput.KeyBinds bind, SteelInput.KeyBinds bind2, string description)
	{
		this.descriptionLabel.text = description;
		this.compoundLabel.text = "+";
		this.compoundLabel.gameObject.SetActive(true);
		if (!this.ShouldUpdateDisplayedGlyphs(new KeyboardGlyphGenerator.DisplayedBinds(bind, bind2)))
		{
			return this.currentDisplayIndex;
		}
		this.HideAllGlyphs();
		this.UpdateBindGlyph(bind, false, this.glyphLeft);
		this.UpdateBindGlyph(bind2, false, this.glyphRight);
		return this.FadeIn();
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x0008E5B8 File Offset: 0x0008C7B8
	public int ShowCompoundBind(SteelInput.KeyBinds bind, SteelInput.KeyBinds bind2, string description)
	{
		this.descriptionLabel.text = description;
		this.compoundLabel.text = "/";
		this.compoundLabel.gameObject.SetActive(true);
		if (!this.ShouldUpdateDisplayedGlyphs(new KeyboardGlyphGenerator.DisplayedBinds(bind, bind2)))
		{
			return this.currentDisplayIndex;
		}
		this.HideAllGlyphs();
		this.UpdateBindGlyph(bind, false, this.glyphLeft);
		this.UpdateBindGlyph(bind2, false, this.glyphRight);
		return this.FadeIn();
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x0008E630 File Offset: 0x0008C830
	public int ShowAxisBind(SteelInput.KeyBinds bind, string description)
	{
		this.descriptionLabel.text = description;
		if (!this.ShouldUpdateDisplayedGlyphs(new KeyboardGlyphGenerator.DisplayedBinds(bind)))
		{
			return this.currentDisplayIndex;
		}
		if (SteelInput.GetInput(bind).HasAnalogBind())
		{
			this.HideAllGlyphs();
			this.UpdateBindGlyph(bind, false, this.glyphCenter);
		}
		else
		{
			this.HideAllGlyphs();
			this.UpdateBindGlyph(bind, false, this.glyphLeft);
			this.UpdateBindGlyph(bind, true, this.glyphRight);
			this.compoundLabel.text = "/";
			this.compoundLabel.gameObject.SetActive(true);
		}
		return this.FadeIn();
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x0008E6D0 File Offset: 0x0008C8D0
	public int ShowBind(SteelInput.KeyBinds bind, string description)
	{
		this.descriptionLabel.text = description;
		if (!this.ShouldUpdateDisplayedGlyphs(new KeyboardGlyphGenerator.DisplayedBinds(bind)))
		{
			return this.currentDisplayIndex;
		}
		this.HideAllGlyphs();
		this.UpdateBindGlyph(bind, false, this.glyphCenter);
		this.compoundLabel.gameObject.SetActive(false);
		return this.FadeIn();
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x0000E4AC File Offset: 0x0000C6AC
	private void HideAllGlyphs()
	{
		this.glyphCenter.Hide();
		this.glyphLeft.Hide();
		this.glyphRight.Hide();
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x0000E4CF File Offset: 0x0000C6CF
	private bool ShouldUpdateDisplayedGlyphs(KeyboardGlyphGenerator.DisplayedBinds newBinds)
	{
		if (newBinds == this.currentDisplayingBinds)
		{
			this.StartAutoHideTimer();
			return false;
		}
		this.currentDisplayingBinds = newBinds;
		return true;
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x0008E72C File Offset: 0x0008C92C
	public void UpdateBindGlyph(SteelInput.KeyBinds bind, bool alt, KeyboardGlyphGenerator.GlyphElements target)
	{
		KeyboardGlyphGenerator.GlyphData bindingGlyph = SteelInput.GetBindingGlyph(bind, alt);
		if (bindingGlyph.isKeyCode)
		{
			this.UpdateKeyCodeGlyph(bindingGlyph.keyCode, target);
		}
		else
		{
			this.UpdateGlyph(bindingGlyph.symbol, target);
		}
		if (!string.IsNullOrEmpty(bindingGlyph.label))
		{
			target.ShowLabel(bindingGlyph.label);
		}
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x0000E4EF File Offset: 0x0000C6EF
	private bool IsMouseKeyBind(KeyCode keyCode)
	{
		return keyCode >= KeyCode.Mouse0 && keyCode <= KeyCode.Mouse6;
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x0008E780 File Offset: 0x0008C980
	public void UpdateMouseKeyGlyph(int mouseButton, KeyboardGlyphGenerator.GlyphElements target)
	{
		if (mouseButton >= 0 && mouseButton < this.database.mouseButtons.Length)
		{
			this.UpdateGlyph(this.database.mouseButtons[mouseButton], target);
			return;
		}
		this.UpdateGlyph(this.database.mouseDefault, target, string.Format("M{0}", mouseButton));
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x0008E7D8 File Offset: 0x0008C9D8
	public void UpdateKeyCodeGlyph(KeyCode keyCode, KeyboardGlyphGenerator.GlyphElements target)
	{
		if (keyCode == KeyCode.None)
		{
			target.Hide();
			target.ShowMainGraphic(this.database.unbound);
			return;
		}
		if (this.IsMouseKeyBind(keyCode))
		{
			this.UpdateMouseKeyGlyph(keyCode - KeyCode.Mouse0, target);
			return;
		}
		target.Hide();
		target.ShowMainGraphic(this.GetKeyGraphic(keyCode));
		target.ShowLabel(this.GetKeyCodeLabelString(keyCode));
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x0000E506 File Offset: 0x0000C706
	private string GetKeyCodeLabelString(KeyCode keyCode)
	{
		if (KeyboardGlyphGenerator.KEY_LABELS.ContainsKey(keyCode))
		{
			return KeyboardGlyphGenerator.KEY_LABELS[keyCode];
		}
		return keyCode.ToString();
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x0000E52E File Offset: 0x0000C72E
	public void UpdateGlyph(Texture2D mainTexture, KeyboardGlyphGenerator.GlyphElements target)
	{
		target.Hide();
		target.ShowMainGraphic(mainTexture);
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x0000E53F File Offset: 0x0000C73F
	public void UpdateGlyph(Texture2D mainTexture, KeyboardGlyphGenerator.GlyphElements target, string label)
	{
		this.UpdateGlyph(mainTexture, target);
		target.ShowLabel(label);
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x0000E551 File Offset: 0x0000C751
	public void UpdateGlyph(Texture2D mainTexture, KeyboardGlyphGenerator.GlyphElements target, Texture2D overlayTexture)
	{
		this.UpdateGlyph(mainTexture, target);
		target.ShowOverlayGraphic(overlayTexture);
	}

	// Token: 0x04001353 RID: 4947
	public static KeyboardGlyphGenerator instance;

	// Token: 0x04001354 RID: 4948
	private static readonly HashSet<KeyCode> WIDE_KEYS = new HashSet<KeyCode>
	{
		KeyCode.Tab,
		KeyCode.CapsLock,
		KeyCode.LeftShift,
		KeyCode.LeftControl,
		KeyCode.RightControl,
		KeyCode.LeftAlt,
		KeyCode.RightAlt,
		KeyCode.AltGr,
		KeyCode.LeftCommand,
		KeyCode.RightCommand
	};

	// Token: 0x04001355 RID: 4949
	private static readonly HashSet<KeyCode> ULTRAWIDE_KEYS = new HashSet<KeyCode>
	{
		KeyCode.RightShift,
		KeyCode.Backspace,
		KeyCode.Space,
		KeyCode.Keypad0
	};

	// Token: 0x04001356 RID: 4950
	private static readonly HashSet<KeyCode> TALL_KEYS = new HashSet<KeyCode>
	{
		KeyCode.KeypadPlus,
		KeyCode.KeypadEnter
	};

	// Token: 0x04001357 RID: 4951
	private static readonly Dictionary<KeyCode, string> KEY_LABELS = new Dictionary<KeyCode, string>
	{
		{
			KeyCode.LeftControl,
			"L Ctrl"
		},
		{
			KeyCode.RightControl,
			"R Ctrl"
		},
		{
			KeyCode.LeftAlt,
			"L Alt"
		},
		{
			KeyCode.RightAlt,
			"R Alt"
		},
		{
			KeyCode.AltGr,
			"Alt Gr"
		},
		{
			KeyCode.LeftShift,
			"L Shift"
		},
		{
			KeyCode.RightShift,
			"R Shift"
		},
		{
			KeyCode.Escape,
			"Esc"
		},
		{
			KeyCode.CapsLock,
			"Caps Lock"
		},
		{
			KeyCode.Return,
			"Enter"
		},
		{
			KeyCode.KeypadEnter,
			"Enter"
		},
		{
			KeyCode.Plus,
			"+"
		},
		{
			KeyCode.KeypadPlus,
			"+"
		},
		{
			KeyCode.Minus,
			"-"
		},
		{
			KeyCode.KeypadMinus,
			"-"
		},
		{
			KeyCode.Asterisk,
			"*"
		},
		{
			KeyCode.KeypadMultiply,
			"*"
		},
		{
			KeyCode.Slash,
			"/"
		},
		{
			KeyCode.KeypadDivide,
			"/"
		},
		{
			KeyCode.Period,
			"."
		},
		{
			KeyCode.KeypadPeriod,
			"."
		},
		{
			KeyCode.Equals,
			"="
		},
		{
			KeyCode.KeypadEquals,
			"="
		}
	};

	// Token: 0x04001358 RID: 4952
	public const float DEFAULT_AUTO_HIDE_TIME = 10f;

	// Token: 0x04001359 RID: 4953
	public KeyboardGlyphGenerator.AssetDatabase database;

	// Token: 0x0400135A RID: 4954
	public Text compoundLabel;

	// Token: 0x0400135B RID: 4955
	public Text descriptionLabel;

	// Token: 0x0400135C RID: 4956
	public RectTransform leftGlyphContainer;

	// Token: 0x0400135D RID: 4957
	public RectTransform rightGlyphContainer;

	// Token: 0x0400135E RID: 4958
	public RectTransform centerGlyphContainer;

	// Token: 0x0400135F RID: 4959
	public Font defaultKeyLabelFont;

	// Token: 0x04001360 RID: 4960
	private CanvasGroup canvasGroup;

	// Token: 0x04001361 RID: 4961
	private KeyboardGlyphGenerator.GlyphElements glyphLeft;

	// Token: 0x04001362 RID: 4962
	private KeyboardGlyphGenerator.GlyphElements glyphRight;

	// Token: 0x04001363 RID: 4963
	private KeyboardGlyphGenerator.GlyphElements glyphCenter;

	// Token: 0x04001364 RID: 4964
	private TimedAction fadeInAction = new TimedAction(0.5f, false);

	// Token: 0x04001365 RID: 4965
	private TimedAction autoHideAction = new TimedAction(10f, false);

	// Token: 0x04001366 RID: 4966
	private int currentDisplayIndex;

	// Token: 0x04001367 RID: 4967
	private KeyboardGlyphGenerator.DisplayedBinds currentDisplayingBinds;

	// Token: 0x020002AD RID: 685
	public struct GlyphElements
	{
		// Token: 0x0600123C RID: 4668 RVA: 0x0008EA84 File Offset: 0x0008CC84
		public GlyphElements(RectTransform container, Font font)
		{
			this.mainGraphic = KeyboardGlyphGenerator.GlyphElements.InstantiatePanel<RawImage>("Main Graphic", container);
			this.overlayGraphic = KeyboardGlyphGenerator.GlyphElements.InstantiatePanel<RawImage>("Overlay Graphic", container);
			this.label = KeyboardGlyphGenerator.GlyphElements.InstantiatePanel<Text>("Label", container);
			this.label.font = font;
			this.label.alignment = TextAnchor.MiddleCenter;
			this.label.fontSize = 16;
			this.label.color = Color.black;
			this.Hide();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0008EB00 File Offset: 0x0008CD00
		private static T InstantiatePanel<T>(string name, RectTransform parent)
		{
			GameObject gameObject = new GameObject(name, new Type[]
			{
				typeof(RectTransform),
				typeof(T)
			});
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.SetParent(parent);
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.offsetMax = Vector2.zero;
			component.offsetMin = Vector2.zero;
			return gameObject.GetComponent<T>();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0000E58D File Offset: 0x0000C78D
		public void Hide()
		{
			this.mainGraphic.gameObject.SetActive(false);
			this.overlayGraphic.gameObject.SetActive(false);
			this.label.gameObject.SetActive(false);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0000E5C2 File Offset: 0x0000C7C2
		public void ShowMainGraphic(Texture2D texture)
		{
			this.mainGraphic.gameObject.SetActive(true);
			this.mainGraphic.texture = texture;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0000E5E1 File Offset: 0x0000C7E1
		public void ShowOverlayGraphic(Texture2D texture)
		{
			this.overlayGraphic.gameObject.SetActive(true);
			this.overlayGraphic.texture = texture;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0000E600 File Offset: 0x0000C800
		public void ShowLabel(string content)
		{
			this.label.gameObject.SetActive(true);
			this.label.text = content;
		}

		// Token: 0x04001368 RID: 4968
		public RawImage mainGraphic;

		// Token: 0x04001369 RID: 4969
		public RawImage overlayGraphic;

		// Token: 0x0400136A RID: 4970
		public Text label;
	}

	// Token: 0x020002AE RID: 686
	[Serializable]
	public struct AssetDatabase
	{
		// Token: 0x0400136B RID: 4971
		public Texture2D keySmall;

		// Token: 0x0400136C RID: 4972
		public Texture2D keyWide;

		// Token: 0x0400136D RID: 4973
		public Texture2D keyUltraWide;

		// Token: 0x0400136E RID: 4974
		public Texture2D keyTall;

		// Token: 0x0400136F RID: 4975
		public Texture2D keyReturn;

		// Token: 0x04001370 RID: 4976
		public Texture2D mouseDefault;

		// Token: 0x04001371 RID: 4977
		public Texture2D mouseScroll;

		// Token: 0x04001372 RID: 4978
		public Texture2D mouseMove;

		// Token: 0x04001373 RID: 4979
		public Texture2D gamepad;

		// Token: 0x04001374 RID: 4980
		public Texture2D unbound;

		// Token: 0x04001375 RID: 4981
		public Texture2D[] mouseButtons;
	}

	// Token: 0x020002AF RID: 687
	public struct GlyphData
	{
		// Token: 0x06001242 RID: 4674 RVA: 0x0000E61F File Offset: 0x0000C81F
		public GlyphData(KeyCode keyCode)
		{
			this.keyCode = keyCode;
			this.isKeyCode = true;
			this.symbol = null;
			this.label = null;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0000E63D File Offset: 0x0000C83D
		public GlyphData(Texture2D symbolTexture)
		{
			this.symbol = symbolTexture;
			this.isKeyCode = false;
			this.keyCode = KeyCode.None;
			this.label = null;
		}

		// Token: 0x04001376 RID: 4982
		public bool isKeyCode;

		// Token: 0x04001377 RID: 4983
		public KeyCode keyCode;

		// Token: 0x04001378 RID: 4984
		public Texture2D symbol;

		// Token: 0x04001379 RID: 4985
		public string label;
	}

	// Token: 0x020002B0 RID: 688
	private struct DisplayedBinds : IEquatable<KeyboardGlyphGenerator.DisplayedBinds>
	{
		// Token: 0x06001244 RID: 4676 RVA: 0x0000E65B File Offset: 0x0000C85B
		public DisplayedBinds(SteelInput.KeyBinds a)
		{
			this.a = a;
			this.type = KeyboardGlyphGenerator.DisplayedBinds.Type.Single;
			this.b = SteelInput.KeyBinds.Horizontal;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0000E672 File Offset: 0x0000C872
		public DisplayedBinds(SteelInput.KeyBinds a, SteelInput.KeyBinds b)
		{
			this.a = a;
			this.b = b;
			this.type = KeyboardGlyphGenerator.DisplayedBinds.Type.Double;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0000E689 File Offset: 0x0000C889
		public override bool Equals(object obj)
		{
			return obj is KeyboardGlyphGenerator.DisplayedBinds && this.Equals((KeyboardGlyphGenerator.DisplayedBinds)obj);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0000E6A1 File Offset: 0x0000C8A1
		public override int GetHashCode()
		{
			return (int)(this.a | (int)this.b << 16);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0000E6B3 File Offset: 0x0000C8B3
		public bool Equals(KeyboardGlyphGenerator.DisplayedBinds other)
		{
			return this.a == other.a && (this.type == KeyboardGlyphGenerator.DisplayedBinds.Type.Single || this.b == other.b);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		public static bool operator ==(KeyboardGlyphGenerator.DisplayedBinds a, KeyboardGlyphGenerator.DisplayedBinds b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		public static bool operator !=(KeyboardGlyphGenerator.DisplayedBinds a, KeyboardGlyphGenerator.DisplayedBinds b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0400137A RID: 4986
		private SteelInput.KeyBinds a;

		// Token: 0x0400137B RID: 4987
		private SteelInput.KeyBinds b;

		// Token: 0x0400137C RID: 4988
		private KeyboardGlyphGenerator.DisplayedBinds.Type type;

		// Token: 0x020002B1 RID: 689
		private enum Type
		{
			// Token: 0x0400137E RID: 4990
			Double,
			// Token: 0x0400137F RID: 4991
			Single
		}
	}
}
