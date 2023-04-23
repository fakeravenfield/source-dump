using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class WaterRipples : MonoBehaviour
{
	// Token: 0x06000985 RID: 2437 RVA: 0x0006B5F4 File Offset: 0x000697F4
	private void Awake()
	{
		this.rigidbody = base.GetComponentInParent<Rigidbody>();
		this.particles = base.GetComponent<ParticleSystem>();
		this.particles.Stop();
		this.emitParticleAction = new TimedAction(1f / Mathf.Max(this.particles.emission.rateOverTime.constant, 0.1f), false);
		this.emitParams = default(ParticleSystem.EmitParams);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0006B668 File Offset: 0x00069868
	private void Update()
	{
		Vector3 position = base.transform.position;
		float num;
		if ((this.rigidbody == null || this.rigidbody.velocity.magnitude > this.speedThreshold) && WaterLevel.IsInWater(position, out num))
		{
			float y = position.y + num;
			if (this.emitParticleAction.Done())
			{
				position.y = y;
				this.emitParams.position = position;
				this.particles.Emit(this.emitParams, 1);
				this.emitParticleAction.Start();
			}
		}
	}

	// Token: 0x04000A67 RID: 2663
	public float speedThreshold = 2f;

	// Token: 0x04000A68 RID: 2664
	private Rigidbody rigidbody;

	// Token: 0x04000A69 RID: 2665
	private ParticleSystem particles;

	// Token: 0x04000A6A RID: 2666
	private ParticleSystem.EmitParams emitParams;

	// Token: 0x04000A6B RID: 2667
	private TimedAction emitParticleAction;
}
