using System;

namespace MapEditor
{
	// Token: 0x020005E7 RID: 1511
	public struct MapDescriptorSettings
	{
		// Token: 0x04002513 RID: 9491
		public static readonly MapDescriptorSettings Defaults = new MapDescriptorSettings
		{
			generateNavMesh = false,
			savePathfinding = false,
			isAutosave = false
		};

		// Token: 0x04002514 RID: 9492
		public bool generateNavMesh;

		// Token: 0x04002515 RID: 9493
		public bool savePathfinding;

		// Token: 0x04002516 RID: 9494
		public bool isAutosave;
	}
}
