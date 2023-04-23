using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000670 RID: 1648
	[CreateAssetMenu(menuName = "Map Editor/Terrain Biome")]
	public class TerrainBiomeData : ScriptableObject
	{
		// Token: 0x060029EE RID: 10734 RVA: 0x0001CC98 File Offset: 0x0001AE98
		public string GetDisplayName()
		{
			if (!string.IsNullOrEmpty(this.nameOverride))
			{
				return this.nameOverride;
			}
			return base.name;
		}

		// Token: 0x0400273A RID: 10042
		public string nameOverride = "";

		// Token: 0x0400273B RID: 10043
		public bool allowNewMaps = true;

		// Token: 0x0400273C RID: 10044
		public float waterLevel;

		// Token: 0x0400273D RID: 10045
		public float waterLevelScale;

		// Token: 0x0400273E RID: 10046
		public int terrainHeight = 300;

		// Token: 0x0400273F RID: 10047
		public GameObject mapGeneratorPrefab;

		// Token: 0x04002740 RID: 10048
		public GameObject pileTexturerPrefab;

		// Token: 0x04002741 RID: 10049
		public GameObject waterLevelPrefab;

		// Token: 0x04002742 RID: 10050
		public GameObject backdropPrefab;
	}
}
