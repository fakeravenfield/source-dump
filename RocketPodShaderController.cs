using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class RocketPodShaderController : MonoBehaviour
{
	// Token: 0x06000948 RID: 2376 RVA: 0x000082A6 File Offset: 0x000064A6
	private void Awake()
	{
		this.PROPERTY_ID = Shader.PropertyToID("_CutoffUV");
		this.material = base.GetComponent<Renderer>().material;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x000082C9 File Offset: 0x000064C9
	private void Update()
	{
		if (this.weapon.ammo != this.lastAmmo)
		{
			this.lastAmmo = this.weapon.ammo;
			this.UpdateMaterial();
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0006AB08 File Offset: 0x00068D08
	private void UpdateMaterial()
	{
		float value = (float)this.weapon.ammo / (float)this.weapon.configuration.ammo;
		this.material.SetFloat(this.PROPERTY_ID, value);
	}

	// Token: 0x04000A29 RID: 2601
	private int PROPERTY_ID;

	// Token: 0x04000A2A RID: 2602
	private int lastAmmo = int.MinValue;

	// Token: 0x04000A2B RID: 2603
	public Weapon weapon;

	// Token: 0x04000A2C RID: 2604
	private Material material;
}
