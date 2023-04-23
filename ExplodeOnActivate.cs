using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class ExplodeOnActivate : MonoBehaviour
{
	// Token: 0x060008E5 RID: 2277 RVA: 0x00007DA1 File Offset: 0x00005FA1
	private void OnEnable()
	{
		base.StartCoroutine(this.Explode());
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00007DB0 File Offset: 0x00005FB0
	private IEnumerator Explode()
	{
		yield return new WaitForSeconds(this.delay);
		ActorManager.Explode(null, null, base.transform.position, this.explosion, this.armorRating, false);
		yield break;
	}

	// Token: 0x040009A0 RID: 2464
	public ExplodingProjectile.ExplosionConfiguration explosion;

	// Token: 0x040009A1 RID: 2465
	public Vehicle.ArmorRating armorRating;

	// Token: 0x040009A2 RID: 2466
	public float delay = 0.1f;
}
