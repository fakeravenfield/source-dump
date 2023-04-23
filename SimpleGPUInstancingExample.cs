using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class SimpleGPUInstancingExample : MonoBehaviour
{
	// Token: 0x06000093 RID: 147 RVA: 0x0003F4B8 File Offset: 0x0003D6B8
	private void Start()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		if (this.Objects != null && this.Objects.Length != 0)
		{
			for (int i = 0; i < this.Objects.Length; i++)
			{
				materialPropertyBlock.SetColor("_Color", this.Objects[i].ObjectColor);
				this.Objects[i].ObjectRenderer.SetPropertyBlock(materialPropertyBlock);
			}
		}
	}

	// Token: 0x04000054 RID: 84
	public SimpleGPUInstancingComponent[] Objects;
}
