using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class BloodParticle : MonoBehaviour
{
	// Token: 0x060008B5 RID: 2229 RVA: 0x00068C78 File Offset: 0x00066E78
	private void Start()
	{
		if (BloodParticle.BLOOD_PARTICLE_SETTING == BloodParticle.BloodParticleType.BloodExplosions)
		{
			this.expires = Time.time + 40f;
			base.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			return;
		}
		if (BloodParticle.BLOOD_PARTICLE_SETTING == BloodParticle.BloodParticleType.DecalOnly)
		{
			this.expires = Time.time + 0.5f;
			return;
		}
		this.expires = Time.time + 5f;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00068CEC File Offset: 0x00066EEC
	private void Update()
	{
		if (Time.time > this.expires)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.velocity += Physics.gravity * Time.deltaTime;
		Vector3 b = this.velocity * Time.deltaTime;
		RaycastHit hitInfo;
		if (Physics.Raycast(new Ray(base.transform.position, b.normalized), out hitInfo, b.magnitude, 1))
		{
			if (hitInfo.rigidbody == null)
			{
				if (BloodParticle.BLOOD_PARTICLE_SETTING == BloodParticle.BloodParticleType.BloodExplosions)
				{
					ActorManager.CreateBloodExplosion(hitInfo);
				}
				DecalManager.AddDecal(hitInfo.point, hitInfo.normal, UnityEngine.Random.Range(0.7f, 2.5f), (this.team == 0) ? DecalManager.DecalType.BloodBlue : DecalManager.DecalType.BloodRed);
			}
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.transform.position += b;
	}

	// Token: 0x04000952 RID: 2386
	public static BloodParticle.BloodParticleType BLOOD_PARTICLE_SETTING;

	// Token: 0x04000953 RID: 2387
	private const int LAYER_MASK = 1;

	// Token: 0x04000954 RID: 2388
	private const float LIFETIME = 5f;

	// Token: 0x04000955 RID: 2389
	public const float LAUNCH_SPEED = 2f;

	// Token: 0x04000956 RID: 2390
	private const float LIFETIME_DECALS_ONLY = 0.5f;

	// Token: 0x04000957 RID: 2391
	public const float LAUNCH_SPEED_DECALS_ONLY = 2f;

	// Token: 0x04000958 RID: 2392
	private const float LIFETIME_SECRET = 40f;

	// Token: 0x04000959 RID: 2393
	public const float LAUNCH_SPEED_SECRET = 8f;

	// Token: 0x0400095A RID: 2394
	private float expires;

	// Token: 0x0400095B RID: 2395
	public Vector3 velocity;

	// Token: 0x0400095C RID: 2396
	public int team;

	// Token: 0x02000131 RID: 305
	public enum BloodParticleType
	{
		// Token: 0x0400095E RID: 2398
		Default,
		// Token: 0x0400095F RID: 2399
		Reduced,
		// Token: 0x04000960 RID: 2400
		DecalOnly,
		// Token: 0x04000961 RID: 2401
		None,
		// Token: 0x04000962 RID: 2402
		BloodExplosions
	}
}
