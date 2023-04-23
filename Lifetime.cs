using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class Lifetime : MonoBehaviour
{
	// Token: 0x06000918 RID: 2328 RVA: 0x00008045 File Offset: 0x00006245
	private void Start()
	{
		this.lifetimeAction = new TimedAction(this.lifetime, false);
		this.lifetimeAction.Start();
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00008064 File Offset: 0x00006264
	private void Update()
	{
		if (this.lifetimeAction.TrueDone())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040009ED RID: 2541
	public float lifetime = 1f;

	// Token: 0x040009EE RID: 2542
	private TimedAction lifetimeAction;
}
