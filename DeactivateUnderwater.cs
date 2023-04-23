using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class DeactivateUnderwater : MonoBehaviour
{
	// Token: 0x060008C2 RID: 2242 RVA: 0x00007BEE File Offset: 0x00005DEE
	private void Start()
	{
		base.gameObject.SetActive(this.invert ^ WaterLevel.IsInWater(base.transform.position));
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Update()
	{
	}

	// Token: 0x0400096F RID: 2415
	public bool invert;
}
