using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class QualitySwitcher : MonoBehaviour
{
	// Token: 0x06000944 RID: 2372 RVA: 0x0006AA98 File Offset: 0x00068C98
	private void Awake()
	{
		bool flag = !Options.IsInitialized() || Options.GetDropdown(OptionDropdown.Id.Quality) >= this.hqLevel;
		this.hqObject.SetActive(flag);
		if (this.lqObject != null)
		{
			this.lqObject.SetActive(!flag);
		}
		if (flag)
		{
			this.activeObject = this.hqObject;
			return;
		}
		this.activeObject = this.lqObject;
	}

	// Token: 0x04000A25 RID: 2597
	public int hqLevel = 5;

	// Token: 0x04000A26 RID: 2598
	public GameObject hqObject;

	// Token: 0x04000A27 RID: 2599
	public GameObject lqObject;

	// Token: 0x04000A28 RID: 2600
	[NonSerialized]
	public GameObject activeObject;
}
