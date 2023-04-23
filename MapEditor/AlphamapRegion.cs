using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000664 RID: 1636
	public struct AlphamapRegion
	{
		// Token: 0x06002980 RID: 10624 RVA: 0x000FCCD4 File Offset: 0x000FAED4
		public AlphamapRegion(MapEditorTerrain terrain, TerrainAlphamap.Layer layer)
		{
			this.alphamap = terrain.GetAlphamap();
			this.layer = layer;
			this.size = this.alphamap.width;
			this.xCenter = Mathf.RoundToInt((float)(this.size / 2));
			this.yCenter = this.xCenter;
			this.width = this.alphamap.width;
			this.height = this.alphamap.height;
			this.xBase = 0;
			this.yBase = 0;
			this.samples = this.alphamap.GetAlpha(layer, this.xBase, this.yBase, this.width, this.height);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000FCD80 File Offset: 0x000FAF80
		public AlphamapRegion(MapEditorTerrain terrain, TerrainAlphamap.Layer layer, float brushSize, Vector3 brushPosition)
		{
			this.alphamap = terrain.GetAlphamap();
			this.layer = layer;
			Vector2 vector = terrain.WorldPositionToAlphamapCoords(brushPosition);
			this.size = Mathf.RoundToInt(brushSize * (float)this.alphamap.resolution);
			int num = Mathf.RoundToInt((float)(this.size / 2));
			this.width = this.size;
			this.height = this.size;
			this.xCenter = Mathf.RoundToInt((float)(this.width / 2));
			this.yCenter = Mathf.RoundToInt((float)(this.height / 2));
			this.xBase = Mathf.FloorToInt(vector.x) - num;
			this.yBase = Mathf.FloorToInt(vector.y) - num;
			int num2 = this.alphamap.width;
			int num3 = this.alphamap.height;
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
			this.samples = this.alphamap.GetAlpha(layer, this.xBase, this.yBase, this.width, this.height);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000FCF38 File Offset: 0x000FB138
		private AlphamapRegion(AlphamapRegion other)
		{
			this.size = other.size;
			this.xCenter = other.xCenter;
			this.yCenter = other.yCenter;
			this.width = other.width;
			this.height = other.height;
			this.xBase = other.xBase;
			this.yBase = other.yBase;
			this.alphamap = other.alphamap;
			this.layer = other.layer;
			this.samples = new double[this.width, this.height];
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.samples[i, j] = other.samples[i, j];
				}
			}
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0001C7C3 File Offset: 0x0001A9C3
		public AlphamapRegion Clone()
		{
			return new AlphamapRegion(this);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000FD004 File Offset: 0x000FB204
		public void ForEach(AlphamapRegion.AlphaModifier modifier)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.samples[i, j] = modifier(this.samples[i, j], i, j);
				}
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		public void Apply()
		{
			this.alphamap.SetAlpha(this.layer, this.xBase, this.yBase, this.samples);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0001C7F5 File Offset: 0x0001A9F5
		public double GetAlpha(int x, int y)
		{
			x = Mathf.Clamp(x, 0, this.width - 1);
			y = Mathf.Clamp(y, 0, this.height - 1);
			return this.samples[x, y];
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000FD058 File Offset: 0x000FB258
		public double DistanceFalloff(int x, int y)
		{
			Vector2 b = new Vector2((float)this.xCenter, (float)this.yCenter);
			float magnitude = (new Vector2((float)x, (float)y) - b).magnitude;
			return (double)Mathf.Clamp01(1f - 2f * magnitude / (float)(this.size - 1));
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000FD0B0 File Offset: 0x000FB2B0
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
					num5 += this.GetAlpha(x, y);
				}
			}
			return num5 / (double)(count * count);
		}

		// Token: 0x04002703 RID: 9987
		public readonly int size;

		// Token: 0x04002704 RID: 9988
		public readonly int yCenter;

		// Token: 0x04002705 RID: 9989
		public readonly int xCenter;

		// Token: 0x04002706 RID: 9990
		public readonly int width;

		// Token: 0x04002707 RID: 9991
		public readonly int height;

		// Token: 0x04002708 RID: 9992
		public readonly int xBase;

		// Token: 0x04002709 RID: 9993
		public readonly int yBase;

		// Token: 0x0400270A RID: 9994
		public readonly double[,] samples;

		// Token: 0x0400270B RID: 9995
		public readonly TerrainAlphamap alphamap;

		// Token: 0x0400270C RID: 9996
		public readonly TerrainAlphamap.Layer layer;

		// Token: 0x02000665 RID: 1637
		// (Invoke) Token: 0x0600298A RID: 10634
		public delegate double AlphaModifier(double height, int x, int y);
	}
}
