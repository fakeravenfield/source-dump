using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000228 RID: 552
[Serializable]
public class PatchedAnimationDatabase
{
	// Token: 0x06000ECD RID: 3789 RVA: 0x0000BD1D File Offset: 0x00009F1D
	public void Add(PatchedAnimationClip clip)
	{
		this.patchedAnimations.Add(clip);
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x00081B28 File Offset: 0x0007FD28
	public bool ContainsPatchedAnimationClipsForController(Animator animator)
	{
		return this.patchedAnimations.Any((PatchedAnimationClip a) => a.controllerName == animator.runtimeAnimatorController.name);
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x00081B5C File Offset: 0x0007FD5C
	public AnimationClip FindPatchedAnimationClip(string controllerName, string clipName)
	{
		PatchedAnimationClip patchedAnimationClip = this.patchedAnimations.FirstOrDefault((PatchedAnimationClip a) => a.controllerName == controllerName && a.clipName == clipName);
		if (patchedAnimationClip == null)
		{
			return null;
		}
		return patchedAnimationClip.reconstructedClip;
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x00081BA0 File Offset: 0x0007FDA0
	public void OnDeserialized()
	{
		for (int i = 0; i < this.patchedAnimations.Count; i++)
		{
			this.patchedAnimations[i].ReconstructAndDisposeSerializedData();
		}
	}

	// Token: 0x04000FA3 RID: 4003
	public List<PatchedAnimationClip> patchedAnimations = new List<PatchedAnimationClip>();
}
