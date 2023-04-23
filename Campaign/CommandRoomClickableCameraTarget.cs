using System;

namespace Campaign
{
	// Token: 0x020003ED RID: 1005
	public class CommandRoomClickableCameraTarget : CommandRoomClickable
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x0001385D File Offset: 0x00011A5D
		public override void OnClick()
		{
			base.OnClick();
			CommandRoomMainCamera.instance.TransitionToTarget(this.target);
		}

		// Token: 0x04001AF8 RID: 6904
		public CommandRoomCamera target;
	}
}
