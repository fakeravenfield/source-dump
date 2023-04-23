using System;

namespace MapEditor
{
	// Token: 0x0200061F RID: 1567
	public class ActivatedWhileSelected : MeoAssistant, ISelectedNotify
	{
		// Token: 0x06002848 RID: 10312 RVA: 0x0000969C File Offset: 0x0000789C
		public void OnDeselected()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		public void OnSelected()
		{
			base.gameObject.SetActive(true);
		}
	}
}
