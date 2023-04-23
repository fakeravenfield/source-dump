using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class RigidbodyProjectile : Projectile
{
	// Token: 0x06000787 RID: 1927 RVA: 0x00006D4F File Offset: 0x00004F4F
	protected new virtual void Awake()
	{
		base.GetComponent<Rigidbody>().velocity = base.transform.forward * this.configuration.speed;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00006D77 File Offset: 0x00004F77
	protected override void Update()
	{
		if (Time.time > this.expireTime)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
	}
}
