using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class WindDirection : MonoBehaviour
{
	// Token: 0x06000988 RID: 2440 RVA: 0x00008648 File Offset: 0x00006848
	private void Awake()
	{
		this.origin = base.transform.rotation;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0006B6FC File Offset: 0x000698FC
	private void Update()
	{
		if (Time.time >= this.nextChangeTimestamp)
		{
			this.ChangeWind();
		}
		this.turnSpeed += (this.turnAcceleration - this.turnSpeed * this.drag) * Time.deltaTime;
		this.offset += this.turnSpeed * Time.deltaTime;
		this.offset.x = Mathf.Clamp(this.offset.x, -this.maxOffset.x, this.maxOffset.x);
		this.offset.y = Mathf.Clamp(this.offset.y, -this.maxOffset.y, this.maxOffset.y);
		base.transform.rotation = this.origin * Quaternion.Euler(this.offset.x, this.offset.y, 0f);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0006B810 File Offset: 0x00069A10
	private void ChangeWind()
	{
		this.turnAcceleration = new Vector2(UnityEngine.Random.Range(-this.acceleration, this.acceleration), UnityEngine.Random.Range(-this.acceleration, this.acceleration));
		this.nextChangeTimestamp = Time.time + UnityEngine.Random.Range(this.minChangeTime, this.maxChangeTime);
	}

	// Token: 0x04000A6C RID: 2668
	private Quaternion origin;

	// Token: 0x04000A6D RID: 2669
	public Vector2 maxOffset = new Vector2(20f, 50f);

	// Token: 0x04000A6E RID: 2670
	public float acceleration = 100f;

	// Token: 0x04000A6F RID: 2671
	public float drag = 5f;

	// Token: 0x04000A70 RID: 2672
	public float minChangeTime = 0.5f;

	// Token: 0x04000A71 RID: 2673
	public float maxChangeTime = 5f;

	// Token: 0x04000A72 RID: 2674
	private Vector2 offset = Vector2.zero;

	// Token: 0x04000A73 RID: 2675
	private Vector2 turnSpeed = Vector2.zero;

	// Token: 0x04000A74 RID: 2676
	private Vector2 turnAcceleration = Vector2.zero;

	// Token: 0x04000A75 RID: 2677
	private float nextChangeTimestamp;
}
