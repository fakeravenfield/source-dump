using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class VehicleControl : MonoBehaviour
{
	// Token: 0x0400172A RID: 5930
	public VehicleControl.VehicleInput vehicleInputType;

	// Token: 0x02000347 RID: 839
	public enum VehicleInput
	{
		// Token: 0x0400172C RID: 5932
		Car,
		// Token: 0x0400172D RID: 5933
		Helicopter,
		// Token: 0x0400172E RID: 5934
		Plane
	}
}
