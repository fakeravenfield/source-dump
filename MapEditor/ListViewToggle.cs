using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006AD RID: 1709
	public class ListViewToggle : MonoBehaviour
	{
		// Token: 0x06002B31 RID: 11057 RVA: 0x0001DAF7 File Offset: 0x0001BCF7
		public void Clear()
		{
			Utils.DestroyChildren(this.itemContainer.gameObject);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x00101068 File Offset: 0x000FF268
		public Toggle Add(string text, UnityAction onSelect = null)
		{
			Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.itemPrefab, this.itemContainer);
			toggle.name = text;
			toggle.isOn = false;
			toggle.SetText(text);
			if (onSelect != null)
			{
				toggle.onValueChanged.AddListener(delegate(bool isOn)
				{
					if (isOn)
					{
						onSelect();
					}
				});
			}
			if (this.group)
			{
				toggle.group = this.group;
			}
			return toggle;
		}

		// Token: 0x04002804 RID: 10244
		public RectTransform itemContainer;

		// Token: 0x04002805 RID: 10245
		public Toggle itemPrefab;

		// Token: 0x04002806 RID: 10246
		public ToggleGroup group;
	}
}
