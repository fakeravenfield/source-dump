using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200069E RID: 1694
	public class DropdownWithText : MonoBehaviour
	{
		// Token: 0x06002B00 RID: 11008 RVA: 0x0001D89E File Offset: 0x0001BA9E
		private void Start()
		{
			this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.SelectedIndexChanged));
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0001D8BC File Offset: 0x0001BABC
		public void SetDescription(string text)
		{
			this.description.text = text;
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0001D8CA File Offset: 0x0001BACA
		public void SetOptions(params string[] options)
		{
			this.dropdown.ClearOptions();
			this.dropdown.AddOptions(options.ToList<string>());
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
		public int GetSelectedIndex()
		{
			return this.dropdown.value;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0001D8F5 File Offset: 0x0001BAF5
		public void SetSelectedIndex(int index)
		{
			this.dropdown.value = index;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00100DA4 File Offset: 0x000FEFA4
		public string GetSelectedValue()
		{
			int selectedIndex = this.GetSelectedIndex();
			return this.dropdown.options[selectedIndex].text;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x00100DD0 File Offset: 0x000FEFD0
		public void SetSelectedValue(string value)
		{
			int selectedIndex = this.dropdown.options.FindIndex((Dropdown.OptionData opt) => opt.text == value);
			this.SetSelectedIndex(selectedIndex);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x00100E10 File Offset: 0x000FF010
		private void SelectedIndexChanged(int index)
		{
			if (this.onSelectedIndexChanged != null)
			{
				this.onSelectedIndexChanged.Invoke(index);
			}
			if (this.onSelectedValueChanged != null)
			{
				string selectedValue = this.GetSelectedValue();
				this.onSelectedValueChanged.Invoke(selectedValue);
			}
		}

		// Token: 0x040027F2 RID: 10226
		public Text description;

		// Token: 0x040027F3 RID: 10227
		public Dropdown dropdown;

		// Token: 0x040027F4 RID: 10228
		[NonSerialized]
		public DropdownWithText.IndexChangedEvent onSelectedIndexChanged = new DropdownWithText.IndexChangedEvent();

		// Token: 0x040027F5 RID: 10229
		[NonSerialized]
		public DropdownWithText.ValueChangedEvent onSelectedValueChanged = new DropdownWithText.ValueChangedEvent();

		// Token: 0x0200069F RID: 1695
		public class IndexChangedEvent : UnityEvent<int>
		{
		}

		// Token: 0x020006A0 RID: 1696
		public class ValueChangedEvent : UnityEvent<string>
		{
		}
	}
}
