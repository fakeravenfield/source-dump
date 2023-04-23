using System;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class SplashSpawnProps : MonoBehaviour
{
	// Token: 0x060010A9 RID: 4265 RVA: 0x0000D4E6 File Offset: 0x0000B6E6
	private void Start()
	{
		base.InvokeRepeating("Spawn", this.startTime, this.periodTime);
		base.Invoke("Cancel", this.duration);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x000058DF File Offset: 0x00003ADF
	private void Cancel()
	{
		base.CancelInvoke();
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0008A568 File Offset: 0x00088768
	private void Spawn()
	{
		Vector3 position = base.transform.position + new Vector3(UnityEngine.Random.Range(-this.spawnRandomPositionMagnitude.x, this.spawnRandomPositionMagnitude.x), UnityEngine.Random.Range(-this.spawnRandomPositionMagnitude.y, this.spawnRandomPositionMagnitude.y), UnityEngine.Random.Range(-this.spawnRandomPositionMagnitude.z, this.spawnRandomPositionMagnitude.z));
		Vector3 euler = base.transform.eulerAngles + new Vector3(UnityEngine.Random.Range(-this.spawnRandomRotationMagnitude.x, this.spawnRandomRotationMagnitude.x), UnityEngine.Random.Range(-this.spawnRandomRotationMagnitude.y, this.spawnRandomRotationMagnitude.y), UnityEngine.Random.Range(-this.spawnRandomRotationMagnitude.z, this.spawnRandomRotationMagnitude.z));
		UnityEngine.Object.Instantiate<GameObject>(this.prefabs[UnityEngine.Random.Range(0, this.prefabs.Length)], position, Quaternion.Euler(euler));
	}

	// Token: 0x040011E0 RID: 4576
	public GameObject[] prefabs;

	// Token: 0x040011E1 RID: 4577
	public float startTime = 3f;

	// Token: 0x040011E2 RID: 4578
	public float periodTime = 1f;

	// Token: 0x040011E3 RID: 4579
	public float duration = 20f;

	// Token: 0x040011E4 RID: 4580
	public Vector3 spawnRandomPositionMagnitude;

	// Token: 0x040011E5 RID: 4581
	public Vector3 spawnRandomRotationMagnitude;
}
