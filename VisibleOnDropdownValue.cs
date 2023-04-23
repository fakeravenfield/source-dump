using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E6 RID: 742
public class VisibleOnDropdownValue : MonoBehaviour
{
	// Token: 0x060013A7 RID: 5031 RVA: 0x0000FB37 File Offset: 0x0000DD37
	private void Awake()
	{
		this.graphic = base.GetComponent<Graphic>();
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x0000FB45 File Offset: 0x0000DD45
	private void Update()
	{
		this.graphic.enabled = (this.dropdown.value == this.value);
	}

	// Token: 0x0400151A RID: 5402
	public Dropdown dropdown;

	// Token: 0x0400151B RID: 5403
	public int value;

	// Token: 0x0400151C RID: 5404
	private Graphic graphic;
}
