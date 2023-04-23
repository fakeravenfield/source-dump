using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class ForceIk : MonoBehaviour
{
	// Token: 0x06000349 RID: 841 RVA: 0x0004CED4 File Offset: 0x0004B0D4
	private void Start()
	{
		this.ik = base.GetComponent<ActorIk>();
		this.ik.handTargetL = this.ikTargetL;
		this.ik.handTargetR = this.ikTargetR;
		this.ik.SetHandIkEnabled(this.ikTargetL != null, this.ikTargetR != null);
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0004CF34 File Offset: 0x0004B134
	private void Update()
	{
		if (this.lookTarget != null)
		{
			this.ik.aimPoint = this.lookTarget.position;
			this.ik.weight = 1f;
			this.ik.turnBody = false;
		}
	}

	// Token: 0x040002D4 RID: 724
	public Transform ikTargetL;

	// Token: 0x040002D5 RID: 725
	public Transform ikTargetR;

	// Token: 0x040002D6 RID: 726
	public Transform lookTarget;

	// Token: 0x040002D7 RID: 727
	private ActorIk ik;
}
