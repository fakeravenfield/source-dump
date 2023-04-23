using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public struct ActorData
{
	// Token: 0x060002AF RID: 687 RVA: 0x00003C19 File Offset: 0x00001E19
	public void SetCurrentPoint(CapturePoint point)
	{
		this.currentPoint = point;
		this.currentPointValidAction.StartLifetime(1f);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00003C32 File Offset: 0x00001E32
	public CapturePoint GetCurrentCapturePoint()
	{
		if (this.currentPointValidAction.TrueDone())
		{
			return null;
		}
		return this.currentPoint;
	}

	// Token: 0x04000230 RID: 560
	public bool canCaptureFlag;

	// Token: 0x04000231 RID: 561
	public bool dead;

	// Token: 0x04000232 RID: 562
	public Vector3 position;

	// Token: 0x04000233 RID: 563
	public Vector3 facingDirection;

	// Token: 0x04000234 RID: 564
	public int team;

	// Token: 0x04000235 RID: 565
	public CapturePoint currentPoint;

	// Token: 0x04000236 RID: 566
	public TimedAction currentPointValidAction;

	// Token: 0x04000237 RID: 567
	public bool isOnPlayerSquad;

	// Token: 0x04000238 RID: 568
	public bool visibleOnMinimap;
}
