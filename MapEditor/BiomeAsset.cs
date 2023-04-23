using System;
using System.Collections.Generic;

namespace MapEditor
{
	// Token: 0x020005C7 RID: 1479
	public struct BiomeAsset : IEquatable<BiomeAsset>
	{
		// Token: 0x06002659 RID: 9817 RVA: 0x0001A7A6 File Offset: 0x000189A6
		public BiomeAsset(AssetId id, string path, TerrainBiomeData biome)
		{
			this.id = id;
			this.path = path;
			this.biome = biome;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0001A7BD File Offset: 0x000189BD
		public bool HasValue()
		{
			return this != BiomeAsset.empty;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0001A7CF File Offset: 0x000189CF
		public override bool Equals(object obj)
		{
			return obj is BiomeAsset && this.Equals((BiomeAsset)obj);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0001A7E7 File Offset: 0x000189E7
		public bool Equals(BiomeAsset other)
		{
			return this.id == other.id && this.path == other.path && this.biome == other.biome;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000F4660 File Offset: 0x000F2860
		public override int GetHashCode()
		{
			return ((1825854281 * -1521134295 + EqualityComparer<AssetId>.Default.GetHashCode(this.id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.path)) * -1521134295 + EqualityComparer<TerrainBiomeData>.Default.GetHashCode(this.biome);
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0001A822 File Offset: 0x00018A22
		public static bool operator ==(BiomeAsset lhs, BiomeAsset rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0001A82C File Offset: 0x00018A2C
		public static bool operator !=(BiomeAsset lhs, BiomeAsset rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x040024C3 RID: 9411
		public static readonly BiomeAsset empty;

		// Token: 0x040024C4 RID: 9412
		public readonly AssetId id;

		// Token: 0x040024C5 RID: 9413
		public readonly string path;

		// Token: 0x040024C6 RID: 9414
		public readonly TerrainBiomeData biome;
	}
}
