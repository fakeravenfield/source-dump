using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000710 RID: 1808
	public class SystemMessagesUI : MonoBehaviour
	{
		// Token: 0x06002D48 RID: 11592 RVA: 0x0001F2B2 File Offset: 0x0001D4B2
		private void Start()
		{
			this.Clear();
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0001F2BA File Offset: 0x0001D4BA
		public void Clear()
		{
			Utils.DestroyChildren(this.messageContainer);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x0001F2C7 File Offset: 0x0001D4C7
		public void Add(string message)
		{
			UnityEngine.Object.Instantiate<SystemMessage>(this.messagePrefab, this.messageContainer.transform).SetText(message);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0001F2E5 File Offset: 0x0001D4E5
		public static void ShowMessage(string message)
		{
			MapEditor.instance.GetEditorUI().systemMessages.Add(message);
		}

		// Token: 0x040029B9 RID: 10681
		public SystemMessage messagePrefab;

		// Token: 0x040029BA RID: 10682
		public GameObject messageContainer;
	}
}
