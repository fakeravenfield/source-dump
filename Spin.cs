using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class Spin : MonoBehaviour
{
	// Token: 0x0600095A RID: 2394 RVA: 0x000083F4 File Offset: 0x000065F4
	private void Update()
	{
		base.transform.Rotate(Vector3.up * this.speed * Time.deltaTime, Space.World);
	}

	// Token: 0x04000A40 RID: 2624
	public float speed = 180f;
}
