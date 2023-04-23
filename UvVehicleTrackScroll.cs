using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class UvVehicleTrackScroll : UvOffset
{
	// Token: 0x06000980 RID: 2432 RVA: 0x000085F9 File Offset: 0x000067F9
	protected override void Awake()
	{
		base.Awake();
		this.vehicle = base.GetComponentInParent<Vehicle>();
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0006B560 File Offset: 0x00069760
	private void Update()
	{
		float d = Vector3.Dot(this.vehicle.LocalVelocity(), this.speedAxis);
		base.IncrementOffset(d * this.offsetPerSpeed * Time.deltaTime);
	}

	// Token: 0x04000A64 RID: 2660
	public Vector3 speedAxis = Vector3.forward;

	// Token: 0x04000A65 RID: 2661
	public Vector2 offsetPerSpeed = new Vector2(0f, 1f);

	// Token: 0x04000A66 RID: 2662
	private Vehicle vehicle;
}
