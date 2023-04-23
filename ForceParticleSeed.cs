using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class ForceParticleSeed : MonoBehaviour
{
	// Token: 0x060008FF RID: 2303 RVA: 0x00069BE4 File Offset: 0x00067DE4
	private void Awake()
	{
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		int randomSeed = UnityEngine.Random.Range(0, int.MaxValue);
		ParticleSystem[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].randomSeed = (uint)randomSeed;
		}
		if (this.play)
		{
			componentsInChildren[0].Play();
		}
	}

	// Token: 0x040009BE RID: 2494
	public bool play;
}
