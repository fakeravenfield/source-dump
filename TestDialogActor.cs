using System;
using System.Collections;
using Ravenfield.Dialog;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class TestDialogActor : MonoBehaviour
{
	// Token: 0x0600113B RID: 4411 RVA: 0x0000D975 File Offset: 0x0000BB75
	private void Start()
	{
		base.StartCoroutine(this.ApplyRandomPose());
		base.StartCoroutine(this.BlinkRandomly());
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0000D991 File Offset: 0x0000BB91
	private IEnumerator ApplyRandomPose()
	{
		yield return new WaitForSeconds(1f);
		this.target.TriggerPose("Test");
		yield break;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
	private IEnumerator BlinkRandomly()
	{
		for (;;)
		{
			this.target.Blink();
			yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
		}
		yield break;
	}

	// Token: 0x04001273 RID: 4723
	public SpriteDialogActor target;
}
