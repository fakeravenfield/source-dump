using System;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class SineHover : MonoBehaviour
{
	// Token: 0x0600094C RID: 2380 RVA: 0x00008308 File Offset: 0x00006508
	private void Update()
	{
		base.transform.localPosition = new Vector3(0f, Mathf.Sin(Time.time * this.frequency) * this.amplitude, 0f);
	}

	// Token: 0x04000A2D RID: 2605
	public float amplitude;

	// Token: 0x04000A2E RID: 2606
	public float frequency;
}
