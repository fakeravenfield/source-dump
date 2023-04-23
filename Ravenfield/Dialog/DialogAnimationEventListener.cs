using System;
using UnityEngine;

namespace Ravenfield.Dialog
{
	// Token: 0x020003DD RID: 989
	public class DialogAnimationEventListener : MonoBehaviour
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x00012E95 File Offset: 0x00011095
		private void AdvanceDialog()
		{
			if (DialogCanvas.instance != null)
			{
				DialogCanvas.instance.QueueAnimationAdvanceEvent();
			}
		}
	}
}
