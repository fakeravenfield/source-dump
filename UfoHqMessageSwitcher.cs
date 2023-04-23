using System;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class UfoHqMessageSwitcher : MonoBehaviour
{
	// Token: 0x0600111F RID: 4383 RVA: 0x0000D8F5 File Offset: 0x0000BAF5
	private void Start()
	{
		this.preTrigger.SetActive(!GameManager.triggeredUfo);
		this.postTrigger.SetActive(GameManager.triggeredUfo);
	}

	// Token: 0x0400126B RID: 4715
	public GameObject preTrigger;

	// Token: 0x0400126C RID: 4716
	public GameObject postTrigger;
}
