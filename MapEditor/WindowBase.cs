using System;
using System.Reflection;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020006C0 RID: 1728
	public abstract class WindowBase : MonoBehaviour
	{
		// Token: 0x06002B9A RID: 11162 RVA: 0x00101F78 File Offset: 0x00100178
		protected void Initialize()
		{
			if (this.frame == null)
			{
				Transform parent = base.transform.parent;
				this.frame = UnityEngine.Object.Instantiate<WindowFrame>(this.windowPrefab, parent);
				this.frame.SetContent(this);
				this.frame.gameObject.SetActive(false);
				this.OnInitialize();
			}
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnHide()
		{
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnShow()
		{
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void Awake()
		{
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0001DF62 File Offset: 0x0001C162
		protected void Start()
		{
			this.Initialize();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0001DF6A File Offset: 0x0001C16A
		public WindowFrame GetFrame()
		{
			this.Initialize();
			return this.frame;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0001DF78 File Offset: 0x0001C178
		public void SetTitle(string title)
		{
			this.GetFrame().SetTitle(title);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0001DF86 File Offset: 0x0001C186
		public void Hide()
		{
			if (this.IsVisible())
			{
				this.OnHide();
				this.GetFrame().gameObject.SetActive(false);
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0001DFA7 File Offset: 0x0001C1A7
		public void Show()
		{
			if (!this.IsVisible())
			{
				this.GetFrame().gameObject.SetActive(true);
				this.OnShow();
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		public bool IsVisible()
		{
			return this.GetFrame().gameObject.activeInHierarchy;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0001DFDA File Offset: 0x0001C1DA
		public void RegisterAllOnValueChangeCallbacks(DelOnValueChangedCallback callback)
		{
			this.RegisterAllOnValueChangeCallbacks(callback, this);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00101FD4 File Offset: 0x001001D4
		private void RegisterAllOnValueChangeCallbacks(DelOnValueChangedCallback callback, object current)
		{
			foreach (FieldInfo fieldInfo in current.GetType().GetFields())
			{
				IValueChangeCallbackProvider valueChangeCallbackProvider = fieldInfo.GetValue(current) as IValueChangeCallbackProvider;
				if (valueChangeCallbackProvider != null)
				{
					valueChangeCallbackProvider.RegisterOnValueChangeCallback(callback);
				}
				IValueChangeFieldsProvider valueChangeFieldsProvider = fieldInfo.GetValue(current) as IValueChangeFieldsProvider;
				if (valueChangeFieldsProvider != null)
				{
					this.RegisterAllOnValueChangeCallbacks(callback, valueChangeFieldsProvider);
				}
			}
		}

		// Token: 0x0400284D RID: 10317
		public WindowFrame windowPrefab;

		// Token: 0x0400284E RID: 10318
		public string initialTitle;

		// Token: 0x0400284F RID: 10319
		protected WindowFrame frame;
	}
}
