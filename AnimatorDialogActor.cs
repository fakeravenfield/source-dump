using System;
using Ravenfield.Dialog;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class AnimatorDialogActor : BaseDialogActor
{
	// Token: 0x06000628 RID: 1576 RVA: 0x00005D5F File Offset: 0x00003F5F
	private void Awake()
	{
		if (this.eyes != null)
		{
			this.eyes.ForceOpenEye();
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00005D7A File Offset: 0x00003F7A
	public override void TriggerPose(string name)
	{
		base.TriggerPose(name);
		this.animator.SetTrigger(name);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00005D8F File Offset: 0x00003F8F
	public override void Blink()
	{
		base.Blink();
		if (this.eyes != null)
		{
			this.eyes.Blink();
		}
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00005DB0 File Offset: 0x00003FB0
	public override void NextTalkFrame()
	{
		base.NextTalkFrame();
		if (this.mouth != null)
		{
			this.mouth.RandomPhoneme();
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00005DD1 File Offset: 0x00003FD1
	public override void SetMouthIdleFrame()
	{
		base.SetMouthIdleFrame();
		if (this.mouth != null)
		{
			this.mouth.Idle();
		}
	}

	// Token: 0x0400060C RID: 1548
	public Animator animator;

	// Token: 0x0400060D RID: 1549
	public Eyes eyes;

	// Token: 0x0400060E RID: 1550
	public Mouth mouth;
}
