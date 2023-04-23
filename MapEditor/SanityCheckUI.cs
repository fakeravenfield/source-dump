using System;
using MapEditor.Internal.SanityCheck;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000704 RID: 1796
	public class SanityCheckUI : WindowBase
	{
		// Token: 0x06002D04 RID: 11524 RVA: 0x0001EF9F File Offset: 0x0001D19F
		protected override void OnInitialize()
		{
			this.rules = ValidationRule.FindAll();
			base.OnInitialize();
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x00105094 File Offset: 0x00103294
		protected override void OnShow()
		{
			bool flag = true;
			this.listViewProblems.gameObject.SetActive(true);
			this.listViewProblems.Clear();
			ValidationRule[] array = this.rules;
			for (int i = 0; i < array.Length; i++)
			{
				ValidationResult validationResult;
				if (!array[i].Validate(out validationResult))
				{
					flag = false;
					this.listViewProblems.Add(validationResult.message, new UnityAction(validationResult.openDetails.Invoke));
				}
			}
			if (flag)
			{
				this.textProblemCounter.text = "No problems found!";
				this.listViewProblems.gameObject.SetActive(false);
			}
			else
			{
				this.textProblemCounter.text = "Please fix the following problems:";
			}
			base.OnShow();
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		protected override void OnHide()
		{
			base.OnHide();
		}

		// Token: 0x0400297C RID: 10620
		public Text textProblemCounter;

		// Token: 0x0400297D RID: 10621
		public ListView listViewProblems;

		// Token: 0x0400297E RID: 10622
		private ValidationRule[] rules;
	}
}
