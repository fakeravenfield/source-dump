using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class PatchedAnimatorPlayback : MonoBehaviour
{
	// Token: 0x06000ED6 RID: 3798 RVA: 0x00081BD4 File Offset: 0x0007FDD4
	public void InitializePatchedPlayback(Animator animator, PatchedAnimationDatabase database)
	{
		this.animator = animator;
		this.patchedAnimation = this.CreatePatchedAnimationHeirarchy();
		Dictionary<AnimationClip, string> dictionary = new Dictionary<AnimationClip, string>();
		string name = this.animator.runtimeAnimatorController.name;
		foreach (AnimationClip animationClip in this.animator.runtimeAnimatorController.animationClips)
		{
			if (!dictionary.ContainsKey(animationClip))
			{
				AnimationClip animationClip2 = database.FindPatchedAnimationClip(name, animationClip.name);
				if (animationClip2 != null)
				{
					string name2 = animationClip.name;
					this.patchedAnimation.AddClip(animationClip2, name2);
					dictionary.Add(animationClip, name2);
				}
				else
				{
					Debug.LogErrorFormat("Patched animation database did not contain clip {0}", new object[]
					{
						animationClip.name
					});
				}
			}
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0000BD83 File Offset: 0x00009F83
	public void Cleanup()
	{
		UnityEngine.Object.Destroy(this.patchedAnimation);
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0000BD90 File Offset: 0x00009F90
	private Animation CreatePatchedAnimationHeirarchy()
	{
		return base.transform.gameObject.AddComponent<Animation>();
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0000BDA2 File Offset: 0x00009FA2
	private void LateUpdate()
	{
		this.patchedAnimation.Sample();
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0000BDAF File Offset: 0x00009FAF
	private void OnEnable()
	{
		this.playbackClip = null;
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0000BDB8 File Offset: 0x00009FB8
	private void Update()
	{
		this.UpdatePlayback();
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00081C94 File Offset: 0x0007FE94
	private void UpdatePlayback()
	{
		AnimatorClipInfo[] currentAnimatorClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
		bool flag = currentAnimatorClipInfo.Length != 0;
		if (this.animator.IsInTransition(0))
		{
			AnimatorClipInfo clipInfo = this.animator.GetNextAnimatorClipInfo(0)[0];
			AnimatorStateInfo nextAnimatorStateInfo = this.animator.GetNextAnimatorStateInfo(0);
			if (this.playbackClip != clipInfo.clip)
			{
				this.playbackClip = clipInfo.clip;
				AnimatorTransitionInfo animatorTransitionInfo = this.animator.GetAnimatorTransitionInfo(0);
				this.OnClipTransition(clipInfo, nextAnimatorStateInfo, animatorTransitionInfo.duration);
				return;
			}
		}
		else if (flag)
		{
			AnimatorClipInfo clipInfo2 = currentAnimatorClipInfo[0];
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (clipInfo2.clip != this.playbackClip)
			{
				this.playbackClip = clipInfo2.clip;
				this.OnClipPlay(clipInfo2, currentAnimatorStateInfo);
			}
		}
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0000BDC0 File Offset: 0x00009FC0
	private string GetClipID(AnimationClip originalClip)
	{
		return originalClip.name;
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x00081D68 File Offset: 0x0007FF68
	private void OnClipTransition(AnimatorClipInfo clipInfo, AnimatorStateInfo stateInfo, float duration)
	{
		try
		{
			string clipID = this.GetClipID(clipInfo.clip);
			this.patchedAnimation.CrossFade(clipID, duration);
			this.SynchronizeClip(this.patchedAnimation[clipID], stateInfo);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x00081DBC File Offset: 0x0007FFBC
	private void OnClipPlay(AnimatorClipInfo clipInfo, AnimatorStateInfo stateInfo)
	{
		try
		{
			string clipID = this.GetClipID(clipInfo.clip);
			this.patchedAnimation.Play(clipID);
			this.SynchronizeClip(this.patchedAnimation[clipID], stateInfo);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0000BDC8 File Offset: 0x00009FC8
	private void SynchronizeClip(AnimationState state, AnimatorStateInfo stateInfo)
	{
		state.speed = stateInfo.speed * stateInfo.speedMultiplier;
		state.normalizedTime = stateInfo.normalizedTime;
		state.wrapMode = (stateInfo.loop ? WrapMode.Loop : WrapMode.ClampForever);
	}

	// Token: 0x04000FA7 RID: 4007
	private const int LAYER_INDEX = 0;

	// Token: 0x04000FA8 RID: 4008
	public Animator animator;

	// Token: 0x04000FA9 RID: 4009
	public Animation patchedAnimation;

	// Token: 0x04000FAA RID: 4010
	private AnimationClip playbackClip;
}
