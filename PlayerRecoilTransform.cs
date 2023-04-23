using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class PlayerRecoilTransform : MonoBehaviour
{
	// Token: 0x06000977 RID: 2423 RVA: 0x00008548 File Offset: 0x00006748
	private void Awake()
	{
		this.originPosition = base.transform.localPosition;
		this.originEuler = base.transform.localEulerAngles;
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0006B4C8 File Offset: 0x000696C8
	public void LateUpdate()
	{
		if (this.activePlayerSeat != null && (!this.activePlayerSeat.IsOccupied() || this.activePlayerSeat.occupant.aiControlled))
		{
			return;
		}
		base.transform.localPosition = this.originPosition + Vector3.Scale(PlayerFpParent.instance.GetSpringLocalPosition(), this.positionWeight);
		base.transform.localEulerAngles = this.originEuler + Vector3.Scale(PlayerFpParent.instance.GetSpringLocalEuler(this.applyCameraKick), this.rotationWeight);
	}

	// Token: 0x04000A5B RID: 2651
	public Seat activePlayerSeat;

	// Token: 0x04000A5C RID: 2652
	public Vector3 positionWeight = Vector3.one;

	// Token: 0x04000A5D RID: 2653
	public Vector3 rotationWeight = Vector3.one;

	// Token: 0x04000A5E RID: 2654
	public bool applyCameraKick;

	// Token: 0x04000A5F RID: 2655
	private Vector3 originPosition;

	// Token: 0x04000A60 RID: 2656
	private Vector3 originEuler;
}
