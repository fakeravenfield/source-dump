using System;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class Joystick : VehicleControl
{
	// Token: 0x06001516 RID: 5398 RVA: 0x00099C1C File Offset: 0x00097E1C
	private void Update()
	{
		try
		{
			Vector2 zero = Vector2.zero;
			if (this.driverSeat.IsOccupied())
			{
				Vector4 vector = this.driverSeat.occupant.controller.AirplaneInput();
				zero.x = Mathf.Clamp(vector.z, -1f, 1f);
				zero.y = Mathf.Clamp(vector.w, -1f, 1f);
			}
			this.movement = Vector2.MoveTowards(this.movement, zero, this.moveSpeed * Time.deltaTime);
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			if (this.vehicleInputType == VehicleControl.VehicleInput.Helicopter)
			{
				localEulerAngles.x = this.movement.y * this.maxAngle;
				localEulerAngles.z = -this.movement.x * this.maxAngle;
			}
			else
			{
				localEulerAngles.x = this.movement.y * this.maxAngle;
				localEulerAngles.y = this.movement.x * this.maxAngle;
			}
			base.transform.localEulerAngles = this.filteredEuler.Tick(localEulerAngles);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x04001703 RID: 5891
	public Seat driverSeat;

	// Token: 0x04001704 RID: 5892
	public float maxAngle = 20f;

	// Token: 0x04001705 RID: 5893
	public float moveSpeed = 5f;

	// Token: 0x04001706 RID: 5894
	private Vector2 movement = Vector2.zero;

	// Token: 0x04001707 RID: 5895
	private MeanFilterVector3 filteredEuler = new MeanFilterVector3(10);
}
