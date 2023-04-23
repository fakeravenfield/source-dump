using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
[ExecuteInEditMode]
public class MaterialColorChanger : MonoBehaviour
{
	// Token: 0x0600091E RID: 2334 RVA: 0x00008109 File Offset: 0x00006309
	private void OnEnable()
	{
		this.material.color = this.color;
	}

	// Token: 0x040009F4 RID: 2548
	public Material material;

	// Token: 0x040009F5 RID: 2549
	public Color color;
}
