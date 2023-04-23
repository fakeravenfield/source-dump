using System;
using UnityEngine;

namespace TFHC_Shader_Samples
{
	// Token: 0x02000A30 RID: 2608
	public class highlightAnimated : MonoBehaviour
	{
		// Token: 0x060053CC RID: 21452 RVA: 0x0003DE36 File Offset: 0x0003C036
		private void Start()
		{
			this.mat = base.GetComponent<Renderer>().material;
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0003DE49 File Offset: 0x0003C049
		private void OnMouseEnter()
		{
			this.switchhighlighted(true);
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x0003DE52 File Offset: 0x0003C052
		private void OnMouseExit()
		{
			this.switchhighlighted(false);
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0003DE5B File Offset: 0x0003C05B
		private void switchhighlighted(bool highlighted)
		{
			this.mat.SetFloat("_Highlighted", highlighted ? 1f : 0f);
		}

		// Token: 0x040032AF RID: 12975
		private Material mat;
	}
}
