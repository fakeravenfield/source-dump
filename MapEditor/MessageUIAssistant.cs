using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020006EA RID: 1770
	public class MessageUIAssistant : MonoBehaviour
	{
		// Token: 0x06002C7C RID: 11388 RVA: 0x0001E970 File Offset: 0x0001CB70
		private void Awake()
		{
			MessageUIAssistant.instance = this;
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x0001E978 File Offset: 0x0001CB78
		public static MessageUIAssistant GetInstance()
		{
			return MessageUIAssistant.instance;
		}

		// Token: 0x040028FD RID: 10493
		public MessageUI messageUI;

		// Token: 0x040028FE RID: 10494
		private static MessageUIAssistant instance;
	}
}
