using System;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class UvOffset : MonoBehaviour
{
	// Token: 0x0600097C RID: 2428 RVA: 0x000085AA File Offset: 0x000067AA
	protected virtual void Awake()
	{
		this.material = this.targetMaterial.Get();
		if (this.material == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x000085D2 File Offset: 0x000067D2
	public void SetOffset(Vector2 offset)
	{
		this.material.mainTextureOffset = offset;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000085E0 File Offset: 0x000067E0
	public void IncrementOffset(Vector2 increment)
	{
		this.material.mainTextureOffset += increment;
	}

	// Token: 0x04000A62 RID: 2658
	public MaterialTarget targetMaterial;

	// Token: 0x04000A63 RID: 2659
	private Material material;
}
