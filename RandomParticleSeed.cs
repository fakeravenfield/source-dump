using System;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class RandomParticleSeed : MonoBehaviour
{
	// Token: 0x06000946 RID: 2374 RVA: 0x0000828E File Offset: 0x0000648E
	private void Awake()
	{
		base.GetComponent<ParticleSystem>().randomSeed = (uint)UnityEngine.Random.Range(0, int.MaxValue);
	}
}
