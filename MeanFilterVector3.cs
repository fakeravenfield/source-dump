using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class MeanFilterVector3
{
	// Token: 0x06001412 RID: 5138 RVA: 0x000954E0 File Offset: 0x000936E0
	public MeanFilterVector3(int taps)
	{
		this.taps = Mathf.Max(taps, 1);
		this.queue = new Queue<Vector3>(this.taps);
		this.queueSum = Vector3.zero;
		for (int i = 0; i < this.taps; i++)
		{
			this.queue.Enqueue(Vector3.zero);
		}
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x00095540 File Offset: 0x00093740
	public Vector3 Tick(Vector3 input)
	{
		this.queueSum -= this.queue.Dequeue();
		this.queueSum += input;
		this.queue.Enqueue(input);
		return this.Value();
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
	public Vector3 Value()
	{
		return this.queueSum / (float)this.taps;
	}

	// Token: 0x04001589 RID: 5513
	private int taps;

	// Token: 0x0400158A RID: 5514
	private Queue<Vector3> queue;

	// Token: 0x0400158B RID: 5515
	private Vector3 queueSum;
}
