using System;

namespace MapEditor
{
	// Token: 0x0200060C RID: 1548
	public static class Layers
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x0001B5DC File Offset: 0x000197DC
		public static int GetSelectableLayer()
		{
			return 26;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public static int GetSelectableLayerMask()
		{
			return Layers.GetMask(26);
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0001B5E9 File Offset: 0x000197E9
		public static int GetGizmoPartLayer()
		{
			return 27;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0001B5ED File Offset: 0x000197ED
		public static int GetGizmoPartLayerMask()
		{
			return Layers.GetMask(27);
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0001B5F6 File Offset: 0x000197F6
		public static int GetPreviewLayer()
		{
			return 29;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0001B5FA File Offset: 0x000197FA
		public static int GetPreviewLayerMask()
		{
			return Layers.GetMask(29);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x0001B603 File Offset: 0x00019803
		public static int GetTerrainLayer()
		{
			return 28;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0001B607 File Offset: 0x00019807
		public static int GetTerrainLayerMask()
		{
			return Layers.GetMask(28);
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0001B610 File Offset: 0x00019810
		public static int GetMask(int layer)
		{
			if (layer < 0 || layer >= 31)
			{
				throw new Exception("Invalid layer number: " + layer.ToString());
			}
			return 1 << layer;
		}

		// Token: 0x040025A1 RID: 9633
		private const int SELECTABLE_LAYER = 26;

		// Token: 0x040025A2 RID: 9634
		private const int GIZMO_PART_LAYER = 27;

		// Token: 0x040025A3 RID: 9635
		private const int TERRAIN_LAYER = 28;

		// Token: 0x040025A4 RID: 9636
		private const int PREVIEW_LAYER = 29;
	}
}
