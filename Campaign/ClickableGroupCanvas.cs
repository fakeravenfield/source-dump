using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003F3 RID: 1011
	[RequireComponent(typeof(Canvas))]
	public class ClickableGroupCanvas : MonoBehaviour
	{
		// Token: 0x06001963 RID: 6499 RVA: 0x00013B16 File Offset: 0x00011D16
		private void Awake()
		{
			this.canvas = base.GetComponent<Canvas>();
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00013B24 File Offset: 0x00011D24
		public void Update()
		{
			if (this.autoHideOnStartBattleUIOpen && StartBattleUi.IsOpen())
			{
				this.canvas.enabled = false;
				return;
			}
			this.canvas.enabled = (this.invertActiveGroups ^ ClickableManager.IsAnyClickableGroupActive(this.activeClickableGroups));
		}

		// Token: 0x04001B1D RID: 6941
		public int[] activeClickableGroups;

		// Token: 0x04001B1E RID: 6942
		public bool invertActiveGroups;

		// Token: 0x04001B1F RID: 6943
		public bool autoHideOnStartBattleUIOpen = true;

		// Token: 0x04001B20 RID: 6944
		private Canvas canvas;
	}
}
