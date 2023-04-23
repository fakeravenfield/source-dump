using System;

namespace MapEditor
{
	// Token: 0x02000683 RID: 1667
	public abstract class TerrainTool : AbstractTool, IShowTerrainSidebar
	{
		// Token: 0x06002A6E RID: 10862 RVA: 0x0000257D File Offset: 0x0000077D
		public override bool IsSelectionChangeAllowed()
		{
			return false;
		}
	}
}
