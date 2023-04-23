using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class WireGuidedMissile : Rocket
{
	// Token: 0x0600087E RID: 2174 RVA: 0x00007854 File Offset: 0x00005A54
	public override void StartTravelling()
	{
		base.StartTravelling();
		this.muzzle = this.sourceWeapon.CurrentMuzzle();
		this.travelDirection = base.transform.forward;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0006875C File Offset: 0x0006695C
	protected override void UpdatePosition()
	{
		if (this.travelDistance > this.controlLossDistance)
		{
			base.UpdatePosition();
			return;
		}
		this.travelDistance += this.configuration.speed * Time.deltaTime;
		if (this.sourceWeapon.isActiveAndEnabled)
		{
			this.travelDirection = this.muzzle.forward;
			Vector3 a = Vector3.ProjectOnPlane(base.transform.position - this.muzzle.position, this.muzzle.forward);
			this.travelDirection = this.muzzle.forward + Vector3.ClampMagnitude(-a * this.correctionGain, this.configuration.speed * this.maxCorrection * Time.deltaTime);
		}
		this.velocity = this.travelDirection * this.configuration.speed;
		this.Travel(this.velocity * Time.deltaTime);
	}

	// Token: 0x0400091F RID: 2335
	private Vector3 travelDirection;

	// Token: 0x04000920 RID: 2336
	private Transform muzzle;

	// Token: 0x04000921 RID: 2337
	public float correctionGain = 0.002f;

	// Token: 0x04000922 RID: 2338
	public float maxCorrection = 0.05f;

	// Token: 0x04000923 RID: 2339
	public float controlLossDistance = 400f;
}
