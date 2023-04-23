using System;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class CameraSway : MonoBehaviour
{
	// Token: 0x06000CC2 RID: 3266 RVA: 0x0000A703 File Offset: 0x00008903
	private void Start()
	{
		this.startPosition = base.transform.position;
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0007A100 File Offset: 0x00078300
	private void Update()
	{
		float num = Time.time * this.frequency;
		base.transform.position = this.startPosition + new Vector3(Mathf.Sin(num * 31f), Mathf.Sin(num * 83f), Mathf.Sin(num * 23f)) * this.magnitude;
	}

	// Token: 0x04000DC2 RID: 3522
	private Vector3 startPosition;

	// Token: 0x04000DC3 RID: 3523
	public float magnitude = 0.1f;

	// Token: 0x04000DC4 RID: 3524
	public float frequency = 0.001f;
}
