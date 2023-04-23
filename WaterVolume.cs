using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class WaterVolume : MonoBehaviour
{
	// Token: 0x06000CB8 RID: 3256 RVA: 0x0000A69A File Offset: 0x0000889A
	private void Start()
	{
		WaterLevel.RegisterWaterVolume(this);
	}
}
