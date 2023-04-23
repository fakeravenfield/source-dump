using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using TMPro;

namespace Lua.Proxy
{
	// Token: 0x02000A09 RID: 2569
	[Proxy(typeof(TextMeshProUGUI))]
	public class TextMeshProUGUIProxy : IProxy
	{
		// Token: 0x06004FC0 RID: 20416 RVA: 0x00039EF0 File Offset: 0x000380F0
		[MoonSharpHidden]
		public TextMeshProUGUIProxy(TextMeshProUGUI value)
		{
			this._value = value;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x00039EFF File Offset: 0x000380FF
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x00039F11 File Offset: 0x00038111
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x00039F23 File Offset: 0x00038123
		// (set) Token: 0x06004FC4 RID: 20420 RVA: 0x00039F30 File Offset: 0x00038130
		public string text
		{
			get
			{
				return WTextMeshProUGUI.GetText(this._value);
			}
			set
			{
				WTextMeshProUGUI.SetText(this._value, value);
			}
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00039F3E File Offset: 0x0003813E
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x001385EC File Offset: 0x001367EC
		[MoonSharpHidden]
		public static TextMeshProUGUIProxy New(TextMeshProUGUI value)
		{
			if (value == null)
			{
				return null;
			}
			TextMeshProUGUIProxy textMeshProUGUIProxy = (TextMeshProUGUIProxy)ObjectCache.Get(typeof(TextMeshProUGUIProxy), value);
			if (textMeshProUGUIProxy == null)
			{
				textMeshProUGUIProxy = new TextMeshProUGUIProxy(value);
				ObjectCache.Add(typeof(TextMeshProUGUIProxy), value, textMeshProUGUIProxy);
			}
			return textMeshProUGUIProxy;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x00039F46 File Offset: 0x00038146
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003298 RID: 12952
		[MoonSharpHidden]
		public TextMeshProUGUI _value;
	}
}
