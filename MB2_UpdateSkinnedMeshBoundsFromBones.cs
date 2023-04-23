using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class MB2_UpdateSkinnedMeshBoundsFromBones : MonoBehaviour
{
	// Token: 0x06000101 RID: 257 RVA: 0x0004154C File Offset: 0x0003F74C
	private void Start()
	{
		this.smr = base.GetComponent<SkinnedMeshRenderer>();
		if (this.smr == null)
		{
			Debug.LogError("Need to attach MB2_UpdateSkinnedMeshBoundsFromBones script to an object with a SkinnedMeshRenderer component attached.");
			return;
		}
		this.bones = this.smr.bones;
		bool updateWhenOffscreen = this.smr.updateWhenOffscreen;
		this.smr.updateWhenOffscreen = true;
		this.smr.updateWhenOffscreen = updateWhenOffscreen;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00002D74 File Offset: 0x00000F74
	private void Update()
	{
		if (this.smr != null)
		{
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(this.bones, this.smr);
		}
	}

	// Token: 0x040000AE RID: 174
	private SkinnedMeshRenderer smr;

	// Token: 0x040000AF RID: 175
	private Transform[] bones;
}
