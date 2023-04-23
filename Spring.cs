using System;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class Spring
{
	// Token: 0x06001444 RID: 5188 RVA: 0x00095BE8 File Offset: 0x00093DE8
	public Spring(float spring, float drag, Vector3 min, Vector3 max, int iterations)
	{
		this.spring = spring;
		this.drag = drag;
		this.position = Vector3.zero;
		this.velocity = Vector3.zero;
		this.min = min;
		this.max = max;
		this.iterations = iterations;
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x000102A0 File Offset: 0x0000E4A0
	public void AddVelocity(Vector3 delta)
	{
		this.velocity += delta;
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x00095C38 File Offset: 0x00093E38
	public void Update()
	{
		float d = Time.deltaTime / (float)this.iterations;
		for (int i = 0; i < this.iterations; i++)
		{
			this.velocity -= (this.position * this.spring + this.velocity * this.drag) * d;
			this.position = Vector3.Min(Vector3.Max(this.position + this.velocity * d, this.min), this.max);
		}
	}

	// Token: 0x0400159D RID: 5533
	private float spring;

	// Token: 0x0400159E RID: 5534
	private float drag;

	// Token: 0x0400159F RID: 5535
	public Vector3 position;

	// Token: 0x040015A0 RID: 5536
	public Vector3 velocity;

	// Token: 0x040015A1 RID: 5537
	private Vector3 min;

	// Token: 0x040015A2 RID: 5538
	private Vector3 max;

	// Token: 0x040015A3 RID: 5539
	private int iterations;
}
