using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class VehicleFov : MonoBehaviour
{
	// Token: 0x060014FF RID: 5375 RVA: 0x00010C46 File Offset: 0x0000EE46
	private void Awake()
	{
		this.Apply();
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x00010C4E File Offset: 0x0000EE4E
	public void Apply()
	{
		base.GetComponent<Camera>().fieldOfView = Options.GetSlider(OptionSlider.Id.VehicleFov);
	}
}
