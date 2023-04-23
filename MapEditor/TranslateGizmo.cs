using System;

namespace MapEditor
{
	// Token: 0x0200060A RID: 1546
	public class TranslateGizmo : TransformGizmo
	{
		// Token: 0x0600279E RID: 10142 RVA: 0x0001B598 File Offset: 0x00019798
		protected override void Start()
		{
			this.followPart = base.GetComponentInChildren<FollowGroundGizmoPart>();
			base.Start();
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0001B5AC File Offset: 0x000197AC
		public bool IsFollowingGround()
		{
			return this.followPart && this.followPart.IsSelected();
		}

		// Token: 0x0400259F RID: 9631
		private FollowGroundGizmoPart followPart;
	}
}
