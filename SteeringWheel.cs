using System;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class SteeringWheel : VehicleControl
{
	// Token: 0x0600152B RID: 5419 RVA: 0x00099FC4 File Offset: 0x000981C4
	private void Update()
	{
		try
		{
			float target = 0f;
			if (this.driverSeat.IsOccupied())
			{
				target = this.driverSeat.occupant.controller.CarInput().x;
			}
			this.turn = Mathf.MoveTowards(this.turn, target, this.steerRate * Time.deltaTime);
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			localEulerAngles.z = this.turn * this.maxRotation;
			base.transform.localEulerAngles = localEulerAngles;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x0400171D RID: 5917
	public Seat driverSeat;

	// Token: 0x0400171E RID: 5918
	public float maxRotation = 45f;

	// Token: 0x0400171F RID: 5919
	public float steerRate = 5f;

	// Token: 0x04001720 RID: 5920
	private float turn;
}
