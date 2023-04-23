using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class MB3_DisableHiddenAnimations : MonoBehaviour
{
	// Token: 0x0600010A RID: 266 RVA: 0x00002DE0 File Offset: 0x00000FE0
	private void Start()
	{
		if (base.GetComponent<SkinnedMeshRenderer>() == null)
		{
			Debug.LogError("The MB3_CullHiddenAnimations script was placed on and object " + base.name + " which has no SkinnedMeshRenderer attached");
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00041694 File Offset: 0x0003F894
	private void OnBecameVisible()
	{
		for (int i = 0; i < this.animationsToCull.Count; i++)
		{
			if (this.animationsToCull[i] != null)
			{
				this.animationsToCull[i].enabled = true;
			}
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x000416E0 File Offset: 0x0003F8E0
	private void OnBecameInvisible()
	{
		for (int i = 0; i < this.animationsToCull.Count; i++)
		{
			if (this.animationsToCull[i] != null)
			{
				this.animationsToCull[i].enabled = false;
			}
		}
	}

	// Token: 0x040000BC RID: 188
	public List<Animation> animationsToCull = new List<Animation>();
}
