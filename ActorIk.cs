using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class ActorIk : MonoBehaviour
{
	// Token: 0x060002B1 RID: 689 RVA: 0x00003C49 File Offset: 0x00001E49
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00003C57 File Offset: 0x00001E57
	public void SetHandIkEnabled(bool left, bool right)
	{
		this.leftHandIk = left;
		this.rightHandIk = right;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x000497AC File Offset: 0x000479AC
	private void OnAnimatorIK()
	{
		try
		{
			this.animator.SetLookAtPosition(this.aimPoint);
			if (this.turnBody)
			{
				this.animator.SetLookAtWeight(this.weight, this.weight, this.weight);
			}
			else
			{
				this.animator.SetLookAtWeight(this.weight, 0f, this.weight);
			}
			if (this.leftHandIk)
			{
				this.animator.SetIKPosition(AvatarIKGoal.LeftHand, this.handTargetL.position);
				this.animator.SetIKRotation(AvatarIKGoal.LeftHand, this.handTargetL.rotation);
				this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
				this.animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
			}
			else
			{
				this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
				this.animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
			}
			if (this.rightHandIk)
			{
				this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.handTargetR.position);
				this.animator.SetIKRotation(AvatarIKGoal.RightHand, this.handTargetR.rotation);
				this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
				this.animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
			}
			else
			{
				this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
				this.animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000239 RID: 569
	private Animator animator;

	// Token: 0x0400023A RID: 570
	[NonSerialized]
	public Vector3 aimPoint = Vector3.zero;

	// Token: 0x0400023B RID: 571
	[NonSerialized]
	public bool turnBody = true;

	// Token: 0x0400023C RID: 572
	[NonSerialized]
	public float weight;

	// Token: 0x0400023D RID: 573
	private bool leftHandIk;

	// Token: 0x0400023E RID: 574
	private bool rightHandIk;

	// Token: 0x0400023F RID: 575
	[NonSerialized]
	public Transform handTargetL;

	// Token: 0x04000240 RID: 576
	[NonSerialized]
	public Transform handTargetR;
}
