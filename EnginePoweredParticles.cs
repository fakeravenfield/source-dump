using System;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class EnginePoweredParticles : MonoBehaviour
{
	// Token: 0x060014EC RID: 5356 RVA: 0x00099410 File Offset: 0x00097610
	private void Awake()
	{
		try
		{
			this.vehicle = base.GetComponentInParent<Vehicle>();
			ParticleSystem component = base.GetComponent<ParticleSystem>();
			if (component == null)
			{
				base.enabled = false;
				throw new Exception("EnginePoweredParticles has no ParticleSystem Component next to it.");
			}
			this.emission = component.emission;
			this.emission.enabled = false;
			this.rateOverTime = this.emission.rateOverTimeMultiplier;
			this.rateOverDistance = this.emission.rateOverDistanceMultiplier;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000994A0 File Offset: 0x000976A0
	private void Update()
	{
		try
		{
			this.emission.enabled = (this.vehicle.engine.power > 0f);
			if (this.enginePowerControlsEmissionRate)
			{
				this.emission.rateOverTimeMultiplier = this.vehicle.engine.power * this.rateOverTime;
				this.emission.rateOverDistanceMultiplier = this.vehicle.engine.power * this.rateOverDistance;
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040016C7 RID: 5831
	public bool enginePowerControlsEmissionRate;

	// Token: 0x040016C8 RID: 5832
	private Vehicle vehicle;

	// Token: 0x040016C9 RID: 5833
	private ParticleSystem.EmissionModule emission;

	// Token: 0x040016CA RID: 5834
	private float rateOverTime;

	// Token: 0x040016CB RID: 5835
	private float rateOverDistance;
}
