using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006A8 RID: 1704
	public class InputWithText : MonoBehaviour
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x0001D9C6 File Offset: 0x0001BBC6
		public bool isFocused
		{
			get
			{
				return this.input.isFocused;
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
		private void Start()
		{
			this.input.onValueChanged.AddListener(new UnityAction<string>(this.TextChanged));
			this.input.onEndEdit.AddListener(new UnityAction<string>(this.EndEdit));
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0001DA0D File Offset: 0x0001BC0D
		public void SetDescription(string text)
		{
			this.description.text = text;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0001DA1B File Offset: 0x0001BC1B
		public void SetText(string text)
		{
			this.input.text = text;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0001DA29 File Offset: 0x0001BC29
		public string GetText()
		{
			return this.input.text;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0001DA36 File Offset: 0x0001BC36
		private void EndEdit(string text)
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(text);
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		private void TextChanged(string text)
		{
			if (this.onTextChanged != null)
			{
				this.onTextChanged.Invoke(text);
			}
		}

		// Token: 0x040027FB RID: 10235
		public Text description;

		// Token: 0x040027FC RID: 10236
		public InputField input;

		// Token: 0x040027FD RID: 10237
		[NonSerialized]
		public InputWithText.TextChangedEvent onTextChanged = new InputWithText.TextChangedEvent();

		// Token: 0x040027FE RID: 10238
		[NonSerialized]
		public InputWithText.TextChangedEvent onEndEdit = new InputWithText.TextChangedEvent();

		// Token: 0x020006A9 RID: 1705
		public class TextChangedEvent : UnityEvent<string>
		{
		}
	}
}
