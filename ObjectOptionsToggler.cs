using System;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class ObjectOptionsToggler : MonoBehaviour
{
	// Token: 0x0600092B RID: 2347 RVA: 0x0006A544 File Offset: 0x00068744
	private void Awake()
	{
		bool toggle = Options.GetToggle(this.option);
		if (this.trueObject != null)
		{
			this.trueObject.SetActive(toggle);
		}
		if (this.falseObject != null)
		{
			this.falseObject.SetActive(!toggle);
		}
	}

	// Token: 0x04000A05 RID: 2565
	public OptionToggle.Id option;

	// Token: 0x04000A06 RID: 2566
	public GameObject trueObject;

	// Token: 0x04000A07 RID: 2567
	public GameObject falseObject;
}
