using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class MB2_UpdateSkinnedMeshBoundsFromBounds : MonoBehaviour
{
	// Token: 0x06000104 RID: 260 RVA: 0x000415B4 File Offset: 0x0003F7B4
	private void Start()
	{
		this.smr = base.GetComponent<SkinnedMeshRenderer>();
		if (this.smr == null)
		{
			Debug.LogError("Need to attach MB2_UpdateSkinnedMeshBoundsFromBounds script to an object with a SkinnedMeshRenderer component attached.");
			return;
		}
		if (this.objects == null || this.objects.Count == 0)
		{
			Debug.LogWarning("The MB2_UpdateSkinnedMeshBoundsFromBounds had no Game Objects. It should have the same list of game objects that the MeshBaker does.");
			this.smr = null;
			return;
		}
		for (int i = 0; i < this.objects.Count; i++)
		{
			if (this.objects[i] == null || this.objects[i].GetComponent<Renderer>() == null)
			{
				Debug.LogError("The list of objects had nulls or game objects without a renderer attached at position " + i.ToString());
				this.smr = null;
				return;
			}
		}
		bool updateWhenOffscreen = this.smr.updateWhenOffscreen;
		this.smr.updateWhenOffscreen = true;
		this.smr.updateWhenOffscreen = updateWhenOffscreen;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00002D95 File Offset: 0x00000F95
	private void Update()
	{
		if (this.smr != null && this.objects != null)
		{
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(this.objects, this.smr);
		}
	}

	// Token: 0x040000B0 RID: 176
	public List<GameObject> objects;

	// Token: 0x040000B1 RID: 177
	private SkinnedMeshRenderer smr;
}
