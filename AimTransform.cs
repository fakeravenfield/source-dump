using System;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class AimTransform : MonoBehaviour
{
	// Token: 0x0600095E RID: 2398 RVA: 0x0006AE64 File Offset: 0x00069064
	private void LateUpdate()
	{
		float target = 0f;
		if (this.userSeat == null || (this.userSeat.IsOccupied() && this.userSeat.occupant.IsAiming()))
		{
			target = 1f;
		}
		this.aim = Mathf.MoveTowards(this.aim, target, Time.deltaTime * this.changeSpeed);
		float t = Mathf.SmoothStep(0f, 1f, this.aim);
		base.transform.position = Vector3.Lerp(this.idleTransform.position, this.aimTransform.position, t);
		base.transform.rotation = Quaternion.Slerp(this.idleTransform.rotation, this.aimTransform.rotation, t);
	}

	// Token: 0x04000A45 RID: 2629
	public Transform idleTransform;

	// Token: 0x04000A46 RID: 2630
	public Transform aimTransform;

	// Token: 0x04000A47 RID: 2631
	public float changeSpeed = 5f;

	// Token: 0x04000A48 RID: 2632
	public Seat userSeat;

	// Token: 0x04000A49 RID: 2633
	private float aim;
}
