using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200071D RID: 1821
	public static class UIExtensions
	{
		// Token: 0x06002D9F RID: 11679 RVA: 0x0010677C File Offset: 0x0010497C
		private static void SetObjectText(GameObject self, string text)
		{
			Transform transform = self.transform.Find("Text");
			if (!transform)
			{
				transform = self.transform;
			}
			transform.GetComponentInChildren<Text>().text = text;
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x001067B8 File Offset: 0x001049B8
		public static string GetObjectText(GameObject self)
		{
			Transform transform = self.transform.Find("Text");
			if (!transform)
			{
				transform = self.transform;
			}
			return transform.GetComponentInChildren<Text>().text;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0001F6D7 File Offset: 0x0001D8D7
		public static void SetText(this Toggle self, string text)
		{
			UIExtensions.SetObjectText(self.gameObject, text);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0001F6D7 File Offset: 0x0001D8D7
		public static void SetText(this Button self, string text)
		{
			UIExtensions.SetObjectText(self.gameObject, text);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0001F6E5 File Offset: 0x0001D8E5
		public static string GetText(this Button self)
		{
			return UIExtensions.GetObjectText(self.gameObject);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x0001F6E5 File Offset: 0x0001D8E5
		public static string GetText(this Toggle self)
		{
			return UIExtensions.GetObjectText(self.gameObject);
		}
	}
}
