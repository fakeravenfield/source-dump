using System;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class ControlSurface : VehicleControl
{
	// Token: 0x0600150B RID: 5387 RVA: 0x00010C8D File Offset: 0x0000EE8D
	private void Awake()
	{
		this.baseLocalEulerAngles = base.transform.localEulerAngles;
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x00099A34 File Offset: 0x00097C34
	private void LateUpdate()
	{
		try
		{
			Vector4 a = Vector4.zero;
			if (this.seat.IsOccupied())
			{
				a = this.seat.occupant.controller.AirplaneInput();
			}
			float target = Mathf.Clamp(Vector4.Dot(a, this.input), -1f, 1f);
			this.turnAmount = Mathf.MoveTowards(this.turnAmount, target, this.turnSpeed * Time.deltaTime);
			base.transform.localEulerAngles = this.baseLocalEulerAngles + this.rotation * this.turnAmount;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040016F5 RID: 5877
	public Seat seat;

	// Token: 0x040016F6 RID: 5878
	public Vector4 input;

	// Token: 0x040016F7 RID: 5879
	public Vector3 rotation;

	// Token: 0x040016F8 RID: 5880
	public float turnSpeed = 4f;

	// Token: 0x040016F9 RID: 5881
	private float turnAmount;

	// Token: 0x040016FA RID: 5882
	private Vector3 baseLocalEulerAngles;
}
