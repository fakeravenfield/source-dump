using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EB RID: 491
public class LoadWorkerProgressBar : MonoBehaviour
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x0000AB8F File Offset: 0x00008D8F
	public void RegisterWorker(LoadModWorker worker)
	{
		this.worker = worker;
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0007C024 File Offset: 0x0007A224
	public void Update()
	{
		if (this.worker != null)
		{
			try
			{
				this.bar.anchorMax = new Vector2(this.worker.GetProgress(), 1f);
				this.info.text = string.Format("LOADING {0} ({1})", this.worker.currentFileName, this.worker.state);
			}
			catch (Exception exception)
			{
				Debug.LogError(string.Format("While loading {0} ({1}", this.worker.currentFileName, this.worker.state));
				Debug.LogException(exception);
			}
		}
	}

	// Token: 0x04000E3A RID: 3642
	public Text info;

	// Token: 0x04000E3B RID: 3643
	public RectTransform bar;

	// Token: 0x04000E3C RID: 3644
	private LoadModWorker worker;
}
