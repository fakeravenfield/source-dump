using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000250 RID: 592
public class ScriptureUI : MonoBehaviour
{
	// Token: 0x06001050 RID: 4176 RVA: 0x0000D073 File Offset: 0x0000B273
	private void Awake()
	{
		ScriptureUI.instance = this;
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x00088838 File Offset: 0x00086A38
	public static void Show(HalloweenScripture scripture)
	{
		if (ScriptureUI.instance != null)
		{
			ScriptureUI.instance.title.text = scripture.title;
			ScriptureUI.instance.text.text = scripture.text;
			ScriptureUI.instance.textShowAction.Start();
		}
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0000D07B File Offset: 0x0000B27B
	private void Update()
	{
		this.text.enabled = !ScriptureUI.instance.textShowAction.TrueDone();
		this.title.enabled = !ScriptureUI.instance.textShowAction.TrueDone();
	}

	// Token: 0x0400115A RID: 4442
	public static ScriptureUI instance;

	// Token: 0x0400115B RID: 4443
	public Text title;

	// Token: 0x0400115C RID: 4444
	public Text text;

	// Token: 0x0400115D RID: 4445
	private TimedAction textShowAction = new TimedAction(20f, false);
}
