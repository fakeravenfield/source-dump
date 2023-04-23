using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200029A RID: 666
public class DropdownExplanation : MonoBehaviour
{
	// Token: 0x060011B5 RID: 4533 RVA: 0x0000DE97 File Offset: 0x0000C097
	private void Awake()
	{
		this.dropdown = base.GetComponentInParent<Dropdown>();
		this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.ValueChanged));
		this.ValueChanged(this.dropdown.value);
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x0000DED2 File Offset: 0x0000C0D2
	private void ValueChanged(int newValue)
	{
		base.gameObject.SetActive(this.dropdown.value == this.item);
	}

	// Token: 0x040012CF RID: 4815
	private Dropdown dropdown;

	// Token: 0x040012D0 RID: 4816
	public int item;
}
