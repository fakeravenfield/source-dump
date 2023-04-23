using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000667 RID: 1639
	public struct BiomeTerrainSettings : IEquatable<BiomeTerrainSettings>
	{
		// Token: 0x0600299C RID: 10652 RVA: 0x0001C8C4 File Offset: 0x0001AAC4
		public BiomeTerrainSettings(int size, int heightmapResolution, int alphamapResolution, bool useMapGenerator)
		{
			this.size = size;
			this.heightmapResolution = heightmapResolution;
			this.alphamapResolution = alphamapResolution;
			this.useMapGenerator = useMapGenerator;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000FD448 File Offset: 0x000FB648
		public void OverrideWithSaneDefaults()
		{
			if (this.size < 512 || this.size > 4096)
			{
				this.size = BiomeTerrainSettings.standard.size;
				Debug.LogError("Terrain size out of bounds. Using standard value.");
			}
			if (this.heightmapResolution < 512 || this.heightmapResolution > 4096)
			{
				this.heightmapResolution = BiomeTerrainSettings.standard.heightmapResolution;
				Debug.LogError("Heightmap resolution out of bounds. Using standard value.");
			}
			if (this.alphamapResolution < 512 || this.alphamapResolution > 4096)
			{
				this.alphamapResolution = BiomeTerrainSettings.standard.alphamapResolution;
				Debug.LogError("Alphamap resolution out of bounds. Using standard value.");
			}
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000FD4F4 File Offset: 0x000FB6F4
		public override int GetHashCode()
		{
			return (((575893102 * -1521134295 + this.size.GetHashCode()) * -1521134295 + this.heightmapResolution.GetHashCode()) * -1521134295 + this.alphamapResolution.GetHashCode()) * -1521134295 + this.useMapGenerator.GetHashCode();
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0001C8E3 File Offset: 0x0001AAE3
		public bool Equals(BiomeTerrainSettings other)
		{
			return this.size == other.size && this.heightmapResolution == other.heightmapResolution && this.alphamapResolution == other.alphamapResolution && this.useMapGenerator == other.useMapGenerator;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0001C91F File Offset: 0x0001AB1F
		public override bool Equals(object obj)
		{
			return obj is BiomeTerrainSettings && this.Equals((BiomeTerrainSettings)obj);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x0001C937 File Offset: 0x0001AB37
		public static bool operator ==(BiomeTerrainSettings lhs, BiomeTerrainSettings rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0001C941 File Offset: 0x0001AB41
		public static bool operator !=(BiomeTerrainSettings lhs, BiomeTerrainSettings rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04002716 RID: 10006
		public static readonly BiomeTerrainSettings empty = default(BiomeTerrainSettings);

		// Token: 0x04002717 RID: 10007
		public static readonly BiomeTerrainSettings standard = new BiomeTerrainSettings(1024, 1024, 1024, false);

		// Token: 0x04002718 RID: 10008
		public int size;

		// Token: 0x04002719 RID: 10009
		public int heightmapResolution;

		// Token: 0x0400271A RID: 10010
		public int alphamapResolution;

		// Token: 0x0400271B RID: 10011
		public bool useMapGenerator;
	}
}
