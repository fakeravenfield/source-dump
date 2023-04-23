using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
[Serializable]
public class MaterialTarget
{
	// Token: 0x06000920 RID: 2336 RVA: 0x0000811C File Offset: 0x0000631C
	public bool HasTarget()
	{
		return this.targetRenderer != null;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0006A240 File Offset: 0x00068440
	public Material Get()
	{
		if (this.cachedMaterial == null)
		{
			try
			{
				this.cachedMaterial = this.targetRenderer.materials[this.materialSlot];
			}
			catch (Exception e)
			{
				ModManager.HandleModException(e);
			}
		}
		return this.cachedMaterial;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0006A294 File Offset: 0x00068494
	public Material GetSharedMaterial()
	{
		Material result;
		try
		{
			result = this.targetRenderer.sharedMaterials[this.materialSlot];
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
			result = null;
		}
		return result;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0006A2D0 File Offset: 0x000684D0
	public void SetMaterial(Material material)
	{
		this.cachedMaterial = material;
		try
		{
			Material[] sharedMaterials = this.targetRenderer.sharedMaterials;
			sharedMaterials[this.materialSlot] = material;
			this.targetRenderer.sharedMaterials = sharedMaterials;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040009F6 RID: 2550
	public Renderer targetRenderer;

	// Token: 0x040009F7 RID: 2551
	public int materialSlot;

	// Token: 0x040009F8 RID: 2552
	private Material cachedMaterial;
}
