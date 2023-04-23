using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class Rotate : MonoBehaviour
{
	// Token: 0x0600142A RID: 5162 RVA: 0x000957F8 File Offset: 0x000939F8
	private void Awake()
	{
		this.finalSpeed = this.speed + new Vector3(UnityEngine.Random.Range(-this.randomSpeedOffset.x, this.randomSpeedOffset.x), UnityEngine.Random.Range(-this.randomSpeedOffset.y, this.randomSpeedOffset.y), UnityEngine.Random.Range(-this.randomSpeedOffset.z, this.randomSpeedOffset.z));
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x000100CF File Offset: 0x0000E2CF
	private void Update()
	{
		base.transform.Rotate(this.finalSpeed * Time.deltaTime, Space.Self);
	}

	// Token: 0x04001594 RID: 5524
	public Vector3 speed;

	// Token: 0x04001595 RID: 5525
	public Vector3 randomSpeedOffset = Vector3.zero;

	// Token: 0x04001596 RID: 5526
	private Vector3 finalSpeed;
}
