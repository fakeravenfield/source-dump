using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class TestShouldEject : MonoBehaviour
{
	// Token: 0x0600115F RID: 4447 RVA: 0x0000DA75 File Offset: 0x0000BC75
	private void Update()
	{
		PathfindingManager.ShouldEjectFromAircraftAtPosition(base.transform.position);
	}
}
