using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006AA RID: 1706
	public class ListView : MonoBehaviour
	{
		// Token: 0x06002B26 RID: 11046 RVA: 0x0001DA80 File Offset: 0x0001BC80
		public void Clear()
		{
			Utils.DestroyChildren(this.itemContainer.gameObject);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x00100F74 File Offset: 0x000FF174
		public Button Add(string text, UnityAction onClick = null)
		{
			Button button = UnityEngine.Object.Instantiate<Button>(this.itemPrefab, this.itemContainer);
			button.name = text;
			button.SetText(text);
			if (onClick != null)
			{
				button.onClick.AddListener(onClick);
			}
			return button;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0001DA92 File Offset: 0x0001BC92
		public IEnumerable<Button> Items()
		{
			return from ch in Utils.GetChildren(this.itemContainer.gameObject)
			select ch.GetComponentInChildren<Button>();
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		public int Count()
		{
			return this.Items().Count<Button>();
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x00100FB4 File Offset: 0x000FF1B4
		public void SortItems(Func<Button, int> getItemPriority)
		{
			Button[] array = (from item in this.Items()
			orderby getItemPriority(item) descending
			select item).ToArray<Button>();
			bool[] array2 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Button button = array[i];
				array2[i] = button.gameObject.activeSelf;
				button.gameObject.SetActive(false);
				button.transform.SetParent(null);
			}
			for (int j = 0; j < array.Length; j++)
			{
				Button button2 = array[j];
				button2.transform.SetParent(this.itemContainer, false);
				button2.gameObject.SetActive(array2[j]);
			}
		}

		// Token: 0x040027FF RID: 10239
		public RectTransform itemContainer;

		// Token: 0x04002800 RID: 10240
		public Button itemPrefab;
	}
}
