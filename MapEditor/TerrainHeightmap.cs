using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000671 RID: 1649
	public class TerrainHeightmap
	{
		// Token: 0x060029F0 RID: 10736 RVA: 0x000FE4E0 File Offset: 0x000FC6E0
		public TerrainHeightmap(Terrain terrain)
		{
			TerrainData terrainData = terrain.terrainData;
			if (terrainData.heightmapResolution != terrainData.heightmapResolution)
			{
				throw new ArgumentException("Terrain heightmap must be square.", "terrain");
			}
			this.terrain = terrain;
			this.terrainData = terrainData;
			this.width = terrainData.heightmapResolution;
			this.height = terrainData.heightmapResolution;
			this.resolution = (double)((float)this.width / this.terrainData.size.x);
			this.heightmap = new double[this.width, this.height];
			this.ReadFromTerrain(0, 0, this.width, this.height);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000FE588 File Offset: 0x000FC788
		public double FindMaximumValue()
		{
			double num = 0.0;
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					num = Math.Max(num, this.heightmap[i, j]);
				}
			}
			return num;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000FE5D8 File Offset: 0x000FC7D8
		public double[,] GetHeights(int xBase, int yBase, int width, int height)
		{
			this.VerifyArgs(xBase, yBase, width, height);
			double[,] array = new double[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[i, j] = this.heightmap[i + xBase, j + yBase];
				}
			}
			return array;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000FE62C File Offset: 0x000FC82C
		public void SetHeights(int xBase, int yBase, double[,] heights)
		{
			int num = heights.GetUpperBound(0) + 1;
			int num2 = heights.GetUpperBound(1) + 1;
			this.VerifyArgs(xBase, yBase, num, num2);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					this.heightmap[i + xBase, j + yBase] = heights[i, j];
				}
			}
			this.WriteToTerrain(xBase, yBase, num, num2);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000FE694 File Offset: 0x000FC894
		public void WriteToTerrain(int xBase, int yBase, int width, int height)
		{
			this.VerifyArgs(xBase, yBase, width, height);
			float[,] array = new float[height, width];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[j, i] = (float)this.heightmap[i + xBase, j + yBase];
				}
			}
			this.terrainData.SetHeights(xBase, yBase, array);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000FE6F8 File Offset: 0x000FC8F8
		public void ReadFromTerrain(int xBase, int yBase, int width, int height)
		{
			this.VerifyArgs(xBase, yBase, width, height);
			float[,] heights = this.terrainData.GetHeights(xBase, yBase, width, height);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					this.heightmap[i + xBase, j + yBase] = (double)heights[j, i];
				}
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000FE754 File Offset: 0x000FC954
		private void VerifyArgs(int xBase, int yBase, int width, int height)
		{
			if (width < 0)
			{
				throw new ArgumentException("Region width must be larger than or equal to zero.", "width");
			}
			if (height < 0)
			{
				throw new ArgumentException("Region height must be larger than or equal to zero.", "height");
			}
			if (xBase < 0 || xBase >= this.width || yBase < 0 || yBase >= this.height || xBase + width > this.width || yBase + height > this.height)
			{
				throw new ArgumentOutOfRangeException("Trying to access data outside of heightmap.");
			}
		}

		// Token: 0x04002743 RID: 10051
		public readonly int width;

		// Token: 0x04002744 RID: 10052
		public readonly int height;

		// Token: 0x04002745 RID: 10053
		public readonly double resolution;

		// Token: 0x04002746 RID: 10054
		public readonly Terrain terrain;

		// Token: 0x04002747 RID: 10055
		public readonly TerrainData terrainData;

		// Token: 0x04002748 RID: 10056
		private readonly double[,] heightmap;
	}
}
