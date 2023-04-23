using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class CycleBlendShapes : MonoBehaviour
{
	// Token: 0x060008BF RID: 2239 RVA: 0x00007BC5 File Offset: 0x00005DC5
	private void Awake()
	{
		this.renderer = base.GetComponent<SkinnedMeshRenderer>();
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00068EF4 File Offset: 0x000670F4
	private void Update()
	{
		float num = Time.time * this.frequency;
		int num2 = Mathf.FloorToInt(num);
		float num3 = num - (float)num2;
		int num4 = num2 % this.nBlendShapes;
		int num5 = (num2 + 1) % this.nBlendShapes;
		for (int i = 0; i < this.nBlendShapes; i++)
		{
			if (i == num4)
			{
				this.renderer.SetBlendShapeWeight(i, (1f - num3) * 100f);
			}
			else if (i == num5)
			{
				this.renderer.SetBlendShapeWeight(i, num3 * 100f);
			}
			else
			{
				this.renderer.SetBlendShapeWeight(i, 0f);
			}
		}
	}

	// Token: 0x0400096C RID: 2412
	private SkinnedMeshRenderer renderer;

	// Token: 0x0400096D RID: 2413
	public int nBlendShapes = 10;

	// Token: 0x0400096E RID: 2414
	public float frequency = 1f;
}
