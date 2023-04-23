using System;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class Plane : Vehicle, IAutoUpgradeComponent
{
	// Token: 0x060014AC RID: 5292 RVA: 0x00010777 File Offset: 0x0000E977
	public Type GetUpgradeType()
	{
		return typeof(Airplane);
	}

	// Token: 0x04001653 RID: 5715
	public AnimationCurve liftVsAngleOfAttack;

	// Token: 0x04001654 RID: 5716
	public AnimationCurve controlVsAngleOfAttack;

	// Token: 0x04001655 RID: 5717
	public Transform thruster;

	// Token: 0x04001656 RID: 5718
	public float baseLift = 0.5f;

	// Token: 0x04001657 RID: 5719
	public new float acceleration = 10f;

	// Token: 0x04001658 RID: 5720
	public float accelerationThrottleUp = 12f;

	// Token: 0x04001659 RID: 5721
	public float accelerationThrottleDown = 8f;

	// Token: 0x0400165A RID: 5722
	public float throttleEngineAudioPitchControl = 0.1f;

	// Token: 0x0400165B RID: 5723
	public float autoPitchTorqueGain = -0.001f;

	// Token: 0x0400165C RID: 5724
	public float perpendicularDrag = 1f;

	// Token: 0x0400165D RID: 5725
	public float pitchSensitivity = 1f;

	// Token: 0x0400165E RID: 5726
	public float yawSensitivity = 0.5f;

	// Token: 0x0400165F RID: 5727
	public float rollSensitivity = 1f;

	// Token: 0x04001660 RID: 5728
	public float liftGainTime = 7f;

	// Token: 0x04001661 RID: 5729
	public float controlWhenBurning = 0.1f;

	// Token: 0x04001662 RID: 5730
	public float flightAltitudeMultiplier = 1f;

	// Token: 0x04001663 RID: 5731
	public GameObject[] landingGearActivationObjects;
}
