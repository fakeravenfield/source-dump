using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class MeanFilter
{
	// Token: 0x0600140C RID: 5132 RVA: 0x000953D0 File Offset: 0x000935D0
	public MeanFilter(int taps)
	{
		this.taps = Mathf.Max(taps, 1);
		this.queue = new Queue<float>(this.taps);
		this.queueSum = 0f;
		for (int i = 0; i < this.taps; i++)
		{
			this.queue.Enqueue(0f);
		}
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x0000FF9A File Offset: 0x0000E19A
	public float Tick(float input)
	{
		this.queueSum -= this.queue.Dequeue();
		this.queueSum += input;
		this.queue.Enqueue(input);
		return this.Value();
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
	public float Value()
	{
		return this.queueSum / (float)this.taps;
	}

	// Token: 0x04001583 RID: 5507
	private int taps;

	// Token: 0x04001584 RID: 5508
	private Queue<float> queue;

	// Token: 0x04001585 RID: 5509
	private float queueSum;
}
