using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A6 RID: 678
public class WeaponStatusIndicator : MonoBehaviour
{
	// Token: 0x060011E8 RID: 4584 RVA: 0x0008D7F4 File Offset: 0x0008B9F4
	public void Update()
	{
		if (this.weapon != null)
		{
			bool flag = !this.weapon.reloading && (this.ignoreUnholster || this.weapon.unholstered) && (this.ignoreCooldown || this.weapon.CoolingDown());
			if (this.textIndicator != null)
			{
				if (this.weapon.reloading)
				{
					this.textIndicator.text = this.reloadText;
				}
				else
				{
					this.textIndicator.text = (flag ? this.readyText : this.notReadyText);
				}
			}
			Color color = flag ? this.readyColor : this.notReadyColor;
			Graphic[] array = this.tintTargets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = color;
			}
			if (this.readyObject != null)
			{
				this.readyObject.SetActive(flag);
			}
			if (this.notReadyObject != null)
			{
				this.notReadyObject.SetActive(!flag);
			}
		}
	}

	// Token: 0x0400130C RID: 4876
	public Weapon weapon;

	// Token: 0x0400130D RID: 4877
	public bool ignoreUnholster = true;

	// Token: 0x0400130E RID: 4878
	public bool ignoreCooldown = true;

	// Token: 0x0400130F RID: 4879
	public Text textIndicator;

	// Token: 0x04001310 RID: 4880
	public string readyText = "";

	// Token: 0x04001311 RID: 4881
	public string notReadyText = "";

	// Token: 0x04001312 RID: 4882
	public string reloadText = "";

	// Token: 0x04001313 RID: 4883
	public Graphic[] tintTargets;

	// Token: 0x04001314 RID: 4884
	public Color readyColor;

	// Token: 0x04001315 RID: 4885
	public Color notReadyColor;

	// Token: 0x04001316 RID: 4886
	public GameObject readyObject;

	// Token: 0x04001317 RID: 4887
	public GameObject notReadyObject;
}
