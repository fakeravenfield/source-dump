using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public struct TimedAction
{
	// Token: 0x06001449 RID: 5193 RVA: 0x000102B4 File Offset: 0x0000E4B4
	public TimedAction(float lifetime, bool unscaledTime = false)
	{
		this.lifetime = lifetime;
		this.end = 0f;
		this.lied = true;
		this.unscaledTime = unscaledTime;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000102D6 File Offset: 0x0000E4D6
	public void Start()
	{
		this.end = this.GetTime() + this.lifetime;
		this.lied = false;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x000102F2 File Offset: 0x0000E4F2
	public void StartLifetime(float lifetime)
	{
		this.lifetime = lifetime;
		this.Start();
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x00010301 File Offset: 0x0000E501
	public void Stop()
	{
		this.end = 0f;
		this.lied = true;
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x00010315 File Offset: 0x0000E515
	public float Remaining()
	{
		return this.end - this.GetTime();
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x00010324 File Offset: 0x0000E524
	public float Elapsed()
	{
		return this.lifetime - this.Remaining();
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x00010333 File Offset: 0x0000E533
	public float Ratio()
	{
		return Mathf.Clamp01(1f - (this.end - this.GetTime()) / this.lifetime);
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x00010354 File Offset: 0x0000E554
	public bool TrueDone()
	{
		return this.end <= this.GetTime();
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x00010367 File Offset: 0x0000E567
	private float GetTime()
	{
		if (!this.unscaledTime)
		{
			return Time.time;
		}
		return Time.unscaledTime;
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0001037C File Offset: 0x0000E57C
	public bool Done()
	{
		if (!this.TrueDone())
		{
			return false;
		}
		if (!this.lied)
		{
			this.lied = true;
			return false;
		}
		return true;
	}

	// Token: 0x040015A6 RID: 5542
	private float lifetime;

	// Token: 0x040015A7 RID: 5543
	private float end;

	// Token: 0x040015A8 RID: 5544
	private bool unscaledTime;

	// Token: 0x040015A9 RID: 5545
	private bool lied;
}
