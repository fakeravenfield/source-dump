using System;

namespace MapEditor
{
	// Token: 0x02000606 RID: 1542
	public class TerrainBrushGizmo : AbstractGizmo
	{
		// Token: 0x06002782 RID: 10114 RVA: 0x0001B44F File Offset: 0x0001964F
		protected override void Awake()
		{
			this.projector = base.GetComponentInChildren<TerrainProjector>();
			base.Awake();
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0001B463 File Offset: 0x00019663
		public void SetSize(float size)
		{
			this.projector.SetSize(size);
		}

		// Token: 0x0400258B RID: 9611
		private TerrainProjector projector;
	}
}
