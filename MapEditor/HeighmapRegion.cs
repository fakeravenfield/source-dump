using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000668 RID: 1640
	public struct HeighmapRegion
	{
		// Token: 0x060029A4 RID: 10660 RVA: 0x000FD550 File Offset: 0x000FB750
		public HeighmapRegion(MapEditorTerrain terrain)
		{
			this.heightmap = terrain.GetHeightmap();
			this.size = this.heightmap.width;
			this.xCenter = Mathf.RoundToInt((float)(this.size / 2));
			this.yCenter = this.xCenter;
			this.width = this.heightmap.width;
			this.height = this.heightmap.height;
			this.xBase = 0;
			this.yBase = 0;
			this.heights = this.heightmap.GetHeights(this.xBase, this.yBase, this.width, this.height);
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000FD5F4 File Offset: 0x000FB7F4
		public HeighmapRegion(MapEditorTerrain terrain, float brushSize, Vector3 brushPosition)
		{
			this.heightmap = terrain.GetHeightmap();
			Vector2 vector = terrain.WorldPositionToHeightmapCoords(brushPosition);
			this.size = Mathf.RoundToInt(brushSize * (float)this.heightmap.resolution);
			int num = Mathf.RoundToInt((float)(this.size / 2));
			this.width = this.size;
			this.height = this.size;
			this.xCenter = Mathf.RoundToInt((float)(this.width / 2));
			this.yCenter = Mathf.RoundToInt((float)(this.height / 2));
			this.xBase = Mathf.FloorToInt(vector.x) - num;
			this.yBase = Mathf.FloorToInt(vector.y) - num;
			int num2 = this.heightmap.width;
			int num3 = this.heightmap.height;
			if (this.xBase + this.size > num2)
			{
				this.width = num2 - this.xBase;
			}
			else if (this.xBase < 0)
			{
				this.xCenter += this.xBase;
				this.width = Mathf.Max(0, this.size + this.xBase);
				this.xBase = 0;
			}
			if (this.yBase + this.size > num3)
			{
				this.height = num3 - this.yBase;
			}
			else if (this.yBase < 0)
			{
				this.yCenter += this.yBase;
				this.height = Mathf.Max(0, this.size + this.yBase);
				this.yBase = 0;
			}
			this.heights = this.heightmap.GetHeights(this.xBase, this.yBase, this.width, this.height);
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000FD7A4 File Offset: 0x000FB9A4
		private HeighmapRegion(HeighmapRegion other)
		{
			this.size = other.size;
			this.xCenter = other.xCenter;
			this.yCenter = other.yCenter;
			this.width = other.width;
			this.height = other.height;
			this.xBase = other.xBase;
			this.yBase = other.yBase;
			this.heightmap = other.heightmap;
			this.heights = new double[this.width, this.height];
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.heights[i, j] = other.heights[i, j];
				}
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0001C975 File Offset: 0x0001AB75
		public HeighmapRegion Clone()
		{
			return new HeighmapRegion(this);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000FD864 File Offset: 0x000FBA64
		public void ForEach(HeighmapRegion.HeightmapModifier modifier)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.heights[i, j] = modifier(this.heights[i, j], i, j);
				}
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x0001C982 File Offset: 0x0001AB82
		public void Apply()
		{
			this.heightmap.SetHeights(this.xBase, this.yBase, this.heights);
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x0001C9A1 File Offset: 0x0001ABA1
		public double GetHeight(int x, int y)
		{
			x = Mathf.Clamp(x, 0, this.width - 1);
			y = Mathf.Clamp(y, 0, this.height - 1);
			return this.heights[x, y];
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000FD8B8 File Offset: 0x000FBAB8
		public double DistanceFalloff(int x, int y)
		{
			Vector2 b = new Vector2((float)this.xCenter, (float)this.yCenter);
			float magnitude = (new Vector2((float)x, (float)y) - b).magnitude;
			return (double)Mathf.Clamp01(1f - 2f * magnitude / (float)(this.size - 1));
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000FD910 File Offset: 0x000FBB10
		public double Multisample(int x, int y, int count)
		{
			int num = x - count / 2;
			int num2 = num + count;
			int num3 = y - count / 2;
			int num4 = num3 + count;
			double num5 = 0.0;
			for (x = num; x < num2; x++)
			{
				for (y = num3; y < num4; y++)
				{
					num5 += this.GetHeight(x, y);
				}
			}
			return num5 / (double)(count * count);
		}

		// Token: 0x0400271C RID: 10012
		public readonly int size;

		// Token: 0x0400271D RID: 10013
		public readonly int yCenter;

		// Token: 0x0400271E RID: 10014
		public readonly int xCenter;

		// Token: 0x0400271F RID: 10015
		public readonly int width;

		// Token: 0x04002720 RID: 10016
		public readonly int height;

		// Token: 0x04002721 RID: 10017
		public readonly int xBase;

		// Token: 0x04002722 RID: 10018
		public readonly int yBase;

		// Token: 0x04002723 RID: 10019
		public readonly double[,] heights;

		// Token: 0x04002724 RID: 10020
		public readonly TerrainHeightmap heightmap;

		// Token: 0x02000669 RID: 1641
		// (Invoke) Token: 0x060029AE RID: 10670
		public delegate double HeightmapModifier(double height, int x, int y);
	}
}
