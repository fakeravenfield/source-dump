using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006E2 RID: 1762
	public class MessageUI : WindowBase
	{
		// Token: 0x06002C52 RID: 11346 RVA: 0x001035D0 File Offset: 0x001017D0
		protected override void Awake()
		{
			base.Awake();
			this.buttonOk.onClick.AddListener(delegate()
			{
				this.Close(MessageUI.DialogResult.Ok);
			});
			this.buttonYes.onClick.AddListener(delegate()
			{
				this.Close(MessageUI.DialogResult.Yes);
			});
			this.buttonNo.onClick.AddListener(delegate()
			{
				this.Close(MessageUI.DialogResult.No);
			});
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x0001E757 File Offset: 0x0001C957
		public void Reset()
		{
			this.SetButtons(MessageUI.Buttons.Ok);
			this.SetInputText("");
			this.SetInputVisibility(false);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0001E772 File Offset: 0x0001C972
		public void SetMessage(string message)
		{
			this.messageLabel.text = message;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0001E780 File Offset: 0x0001C980
		public void SetButtons(MessageUI.Buttons buttons)
		{
			this.buttonOk.gameObject.SetActive(buttons == MessageUI.Buttons.Ok);
			this.buttonYes.gameObject.SetActive(buttons == MessageUI.Buttons.YesNo);
			this.buttonNo.gameObject.SetActive(buttons == MessageUI.Buttons.YesNo);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0001E7BE File Offset: 0x0001C9BE
		public void SetInputText(string text)
		{
			this.inputField.text = text;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public void SetInputVisibility(bool visible)
		{
			this.inputContainer.gameObject.SetActive(visible);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0001E7DF File Offset: 0x0001C9DF
		public void SetCallback(MessageUI.Callback callback)
		{
			this.callback = callback;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		protected override void OnShow()
		{
			this.dialogResult = MessageUI.DialogResult.None;
			base.OnShow();
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0001E7F7 File Offset: 0x0001C9F7
		protected override void OnHide()
		{
			if (this.callback != null)
			{
				this.callback(this);
				this.callback = null;
			}
			this.Reset();
			base.OnHide();
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0001E820 File Offset: 0x0001CA20
		private void Close(MessageUI.DialogResult dialogResult)
		{
			this.dialogResult = dialogResult;
			base.Hide();
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x0001E82F File Offset: 0x0001CA2F
		public MessageUI.DialogResult GetDialogResult()
		{
			return this.dialogResult;
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0001E837 File Offset: 0x0001CA37
		public string GetInputText()
		{
			return this.inputField.text;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0001E844 File Offset: 0x0001CA44
		public static MessageUI GetInstance()
		{
			return MessageUIAssistant.GetInstance().messageUI;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0001E850 File Offset: 0x0001CA50
		public static void ShowMessage(string message, string title, MessageUI.Callback callback = null)
		{
			MessageUI instance = MessageUI.GetInstance();
			instance.Reset();
			instance.SetTitle(title.ToUpper());
			instance.SetMessage(message);
			instance.SetCallback(callback);
			instance.Show();
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0001E87C File Offset: 0x0001CA7C
		public static void ShowQuestion(string message, string title, MessageUI.Callback callback)
		{
			MessageUI instance = MessageUI.GetInstance();
			instance.Reset();
			instance.SetButtons(MessageUI.Buttons.YesNo);
			instance.SetTitle(title.ToUpper());
			instance.SetMessage(message);
			instance.SetCallback(callback);
			instance.Show();
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x0001E8AF File Offset: 0x0001CAAF
		public static void ShowPrompt(string message, string title, string value, MessageUI.Callback callback)
		{
			MessageUI instance = MessageUI.GetInstance();
			instance.Reset();
			instance.SetTitle(title.ToUpper());
			instance.SetMessage(message);
			instance.SetInputText(value);
			instance.SetInputVisibility(true);
			instance.SetCallback(callback);
			instance.Show();
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0001E8E9 File Offset: 0x0001CAE9
		public static IEnumerator ShowMessageRoutine(string message, string title)
		{
			bool isOpen = true;
			MessageUI.ShowMessage(message, title, delegate(MessageUI msg)
			{
				isOpen = false;
			});
			while (isOpen)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0001E8FF File Offset: 0x0001CAFF
		public static IEnumerator ShowQuestionRoutine(string message, string title)
		{
			bool isOpen = true;
			MessageUI.ShowQuestion(message, title, delegate(MessageUI msg)
			{
				isOpen = false;
			});
			while (isOpen)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x040028E0 RID: 10464
		public Text messageLabel;

		// Token: 0x040028E1 RID: 10465
		public InputField inputField;

		// Token: 0x040028E2 RID: 10466
		public RectTransform inputContainer;

		// Token: 0x040028E3 RID: 10467
		public Button buttonOk;

		// Token: 0x040028E4 RID: 10468
		public Button buttonYes;

		// Token: 0x040028E5 RID: 10469
		public Button buttonNo;

		// Token: 0x040028E6 RID: 10470
		private MessageUI.DialogResult dialogResult;

		// Token: 0x040028E7 RID: 10471
		private MessageUI.Callback callback;

		// Token: 0x020006E3 RID: 1763
		public enum DialogResult
		{
			// Token: 0x040028E9 RID: 10473
			None,
			// Token: 0x040028EA RID: 10474
			Ok,
			// Token: 0x040028EB RID: 10475
			Cancel,
			// Token: 0x040028EC RID: 10476
			Yes,
			// Token: 0x040028ED RID: 10477
			No
		}

		// Token: 0x020006E4 RID: 1764
		public enum Buttons
		{
			// Token: 0x040028EF RID: 10479
			Ok,
			// Token: 0x040028F0 RID: 10480
			YesNo
		}

		// Token: 0x020006E5 RID: 1765
		// (Invoke) Token: 0x06002C69 RID: 11369
		public delegate void Callback(MessageUI messageUI);
	}
}
