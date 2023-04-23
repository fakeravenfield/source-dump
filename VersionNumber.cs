using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000210 RID: 528
public class VersionNumber : MonoBehaviour
{
	// Token: 0x06000DFB RID: 3579 RVA: 0x0007DDBC File Offset: 0x0007BFBC
	private void Start()
	{
		Text component = base.GetComponent<Text>();
		string format = GameManager.instance.isBeta ? this.betaFormat : this.stableFormat;
		component.text = string.Format(format, GameManager.instance.buildNumber);
	}

	// Token: 0x04000EF4 RID: 3828
	public string stableFormat = "EARLY ACCESS BUILD {0}";

	// Token: 0x04000EF5 RID: 3829
	public string betaFormat = "EA{0} Beta (WIP)";
}
