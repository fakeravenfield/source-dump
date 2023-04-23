using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class MeanFilterVector2
{
	// Token: 0x0600140F RID: 5135 RVA: 0x00095430 File Offset: 0x00093630
	public MeanFilterVector2(int taps)
	{
		this.taps = Mathf.Max(taps, 1);
		this.queue = new Queue<Vector2>(this.taps);
		this.queueSum = Vector2.zero;
		for (int i = 0; i < this.taps; i++)
		{
			this.queue.Enqueue(Vector2.zero);
		}
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x00095490 File Offset: 0x00093690
	public Vector2 Tick(Vector2 input)
	{
		this.queueSum -= this.queue.Dequeue();
		this.queueSum += input;
		this.queue.Enqueue(input);
		return this.Value();
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
	public Vector2 Value()
	{
		return this.queueSum / (float)this.taps;
	}

	// Token: 0x04001586 RID: 5510
	private int taps;

	// Token: 0x04001587 RID: 5511
	private Queue<Vector2> queue;

	// Token: 0x04001588 RID: 5512
	private Vector2 queueSum;
}
