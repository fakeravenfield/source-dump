using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A3 RID: 675
public class GearIndicator : MonoBehaviour
{
	// Token: 0x060011E0 RID: 4576 RVA: 0x0000E07F File Offset: 0x0000C27F
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x0008D6F0 File Offset: 0x0008B8F0
	private void Update()
	{
		if (this.car.IsChangingGears())
		{
			this.text.text = this.neutral;
			return;
		}
		if (this.car.inReverseGear)
		{
			this.text.text = this.reverse;
			return;
		}
		this.text.text = this.forward;
	}

	// Token: 0x04001304 RID: 4868
	public ArcadeCar car;

	// Token: 0x04001305 RID: 4869
	private Text text;

	// Token: 0x04001306 RID: 4870
	public string neutral = "-";

	// Token: 0x04001307 RID: 4871
	public string forward = "FWD";

	// Token: 0x04001308 RID: 4872
	public string reverse = "REV";
}
