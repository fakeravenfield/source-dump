using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class SpeedLimitZone : MonoBehaviour
{
	// Token: 0x06000C6B RID: 3179 RVA: 0x0000A27D File Offset: 0x0000847D
	private void Start()
	{
		base.StartCoroutine(this.Register());
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0000A28C File Offset: 0x0000848C
	private IEnumerator Register()
	{
		yield return new WaitForSeconds(0.1f);
		ActorManager.RegisterSpeedLimitZone(this);
		yield break;
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00078F74 File Offset: 0x00077174
	public bool IsInside(Vector3 position)
	{
		Vector3 vector = base.transform.worldToLocalMatrix.MultiplyPoint(position);
		return Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f;
	}

	// Token: 0x04000D6C RID: 3436
	public float speedMultiplier = 0.5f;
}
