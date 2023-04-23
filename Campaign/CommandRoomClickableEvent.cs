using System;
using UnityEngine.Events;

namespace Campaign
{
	// Token: 0x020003EE RID: 1006
	public class CommandRoomClickableEvent : CommandRoomClickable
	{
		// Token: 0x0600192D RID: 6445 RVA: 0x0001387D File Offset: 0x00011A7D
		public override void OnClick()
		{
			base.OnClick();
			this.OnClickEvent.Invoke();
		}

		// Token: 0x04001AF9 RID: 6905
		public UnityEvent OnClickEvent;
	}
}
