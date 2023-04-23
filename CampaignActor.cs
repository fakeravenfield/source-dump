using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class CampaignActor : MonoBehaviour
{
	// Token: 0x0600061E RID: 1566 RVA: 0x00005D1C File Offset: 0x00003F1C
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00005D2A File Offset: 0x00003F2A
	private void Start()
	{
		base.StartCoroutine(this.PlayAnimation());
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00005D39 File Offset: 0x00003F39
	private IEnumerator PlayAnimation()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
		for (;;)
		{
			int i = UnityEngine.Random.Range(0, 3);
			int num;
			for (int j = 0; j < i; j = num + 1)
			{
				this.animator.SetTrigger("move");
				yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
				num = j;
			}
			this.animator.SetInteger("alt", UnityEngine.Random.Range(1, 3));
			yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 7f));
			i = UnityEngine.Random.Range(1, 3);
			for (int j = 0; j < i; j = num + 1)
			{
				this.animator.SetTrigger("move");
				yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 7f));
				num = j;
			}
			this.animator.SetInteger("alt", 0);
			yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
		}
		yield break;
	}

	// Token: 0x04000605 RID: 1541
	protected Animator animator;

	// Token: 0x04000606 RID: 1542
	private int altStance;
}
