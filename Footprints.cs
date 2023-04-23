using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class Footprints : MonoBehaviour
{
	// Token: 0x06001092 RID: 4242 RVA: 0x0008A28C File Offset: 0x0008848C
	private void Awake()
	{
		this.cooldown = new TimedAction[this.sources.Length];
		this.particleSystem = base.GetComponent<ParticleSystem>();
		for (int i = 0; i < this.sources.Length; i++)
		{
			this.cooldown[i] = new TimedAction(1f, false);
		}
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0008A2E4 File Offset: 0x000884E4
	private void Update()
	{
		for (int i = 0; i < this.sources.Length; i++)
		{
			Transform transform = this.sources[i];
			if (transform.position.y < base.transform.position.y && this.cooldown[i].TrueDone())
			{
				this.cooldown[i].Start();
				Vector3 position = transform.position;
				position.y = base.transform.position.y;
				Quaternion rotation = Quaternion.LookRotation(-transform.right.ToGround());
				base.StartCoroutine(this.SpawnParticle(position, rotation));
			}
		}
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0000D370 File Offset: 0x0000B570
	private IEnumerator SpawnParticle(Vector3 position, Quaternion rotation)
	{
		yield return new WaitForSeconds(this.spawnDelay);
		ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
		emitParams.position = position + rotation * this.localParticleOffset;
		emitParams.rotation3D = new Vector3(0f, 0f, rotation.eulerAngles.y);
		this.particleSystem.Emit(emitParams, 1);
		yield break;
	}

	// Token: 0x040011CD RID: 4557
	public Transform[] sources;

	// Token: 0x040011CE RID: 4558
	private TimedAction[] cooldown;

	// Token: 0x040011CF RID: 4559
	private ParticleSystem particleSystem;

	// Token: 0x040011D0 RID: 4560
	public Vector3 localParticleOffset = Vector3.zero;

	// Token: 0x040011D1 RID: 4561
	public float spawnDelay = 0.1f;
}
