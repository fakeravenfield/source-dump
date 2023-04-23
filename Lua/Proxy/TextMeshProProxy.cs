using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using TMPro;

namespace Lua.Proxy
{
	// Token: 0x02000A08 RID: 2568
	[Proxy(typeof(TextMeshPro))]
	public class TextMeshProProxy : IProxy
	{
		// Token: 0x06004FB8 RID: 20408 RVA: 0x00039E8D File Offset: 0x0003808D
		[MoonSharpHidden]
		public TextMeshProProxy(TextMeshPro value)
		{
			this._value = value;
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x00039E9C File Offset: 0x0003809C
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x00039EAE File Offset: 0x000380AE
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06004FBB RID: 20411 RVA: 0x00039EC0 File Offset: 0x000380C0
		// (set) Token: 0x06004FBC RID: 20412 RVA: 0x00039ECD File Offset: 0x000380CD
		public string text
		{
			get
			{
				return WTextMeshPro.GetText(this._value);
			}
			set
			{
				WTextMeshPro.SetText(this._value, value);
			}
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x00039EDB File Offset: 0x000380DB
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x001385A0 File Offset: 0x001367A0
		[MoonSharpHidden]
		public static TextMeshProProxy New(TextMeshPro value)
		{
			if (value == null)
			{
				return null;
			}
			TextMeshProProxy textMeshProProxy = (TextMeshProProxy)ObjectCache.Get(typeof(TextMeshProProxy), value);
			if (textMeshProProxy == null)
			{
				textMeshProProxy = new TextMeshProProxy(value);
				ObjectCache.Add(typeof(TextMeshProProxy), value, textMeshProProxy);
			}
			return textMeshProProxy;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00039EE3 File Offset: 0x000380E3
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003297 RID: 12951
		[MoonSharpHidden]
		public TextMeshPro _value;
	}
}
