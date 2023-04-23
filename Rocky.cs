using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class Rocky : MonoBehaviour
{
	// Token: 0x06001027 RID: 4135 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Start()
	{
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Update()
	{
	}

	// Token: 0x04001114 RID: 4372
	public GameObject rockPrefab;

	// Token: 0x04001115 RID: 4373
	[Range(0f, 20f)]
	public float depth;

	// Token: 0x04001116 RID: 4374
	[Range(0f, 10f)]
	public float depthOctaves;

	// Token: 0x04001117 RID: 4375
	public float noiseAmplitude = 0.5f;

	// Token: 0x04001118 RID: 4376
	public float decimateDistance = 1f;

	// Token: 0x04001119 RID: 4377
	[Range(0f, 5f)]
	public int iterations;
}
