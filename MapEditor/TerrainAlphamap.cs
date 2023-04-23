using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200066C RID: 1644
	public class TerrainAlphamap
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x0001CB54 File Offset: 0x0001AD54
		public int width { get; private set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x0001CB5D File Offset: 0x0001AD5D
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x0001CB65 File Offset: 0x0001AD65
		public int height { get; private set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x0001CB6E File Offset: 0x0001AD6E
		public int numLayers
		{
			get
			{
				return this.layers.Count;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x0001CB7B File Offset: 0x0001AD7B
		// (set) Token: 0x060029CE RID: 10702 RVA: 0x0001CB83 File Offset: 0x0001AD83
		public double resolution { get; private set; }

		// Token: 0x060029CF RID: 10703 RVA: 0x000FDC94 File Offset: 0x000FBE94
		public TerrainAlphamap(Terrain terrain)
		{
			this.terrain = terrain;
			this.terrainData = terrain.terrainData;
			this.width = this.terrainData.alphamapWidth;
			this.height = this.terrainData.alphamapHeight;
			this.resolution = (double)((float)this.width / this.terrainData.size.x);
			this.samples = new double[this.width, this.height, this.numLayers];
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000FDD24 File Offset: 0x000FBF24
		public void InitializeFromTerrain()
		{
			TerrainData terrainData = this.terrain.terrainData;
			if (terrainData.alphamapHeight != terrainData.alphamapWidth)
			{
				throw new ArgumentException("Terrain alphamap must be square.", "terrain");
			}
			this.width = terrainData.alphamapWidth;
			this.height = terrainData.alphamapHeight;
			this.resolution = (double)((float)this.width / this.terrainData.size.x);
			this.layers = new List<TerrainAlphamap.Layer>(terrainData.alphamapLayers);
			for (int i = 0; i < terrainData.alphamapLayers; i++)
			{
				MapEditorMaterial material = new MapEditorMaterial(terrainData.terrainLayers[i]);
				TerrainAlphamap.Layer item = new TerrainAlphamap.Layer(this, material);
				this.layers.Add(item);
			}
			this.samples = new double[this.width, this.height, this.numLayers];
			this.ReadFromTerrain(0, 0, this.width, this.height);
			this.NormalizeAll();
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		public void ApplyPileTexturer(PileTexturer pileTexturer)
		{
			pileTexturer.ApplyTexture();
			this.ReadFromTerrain(0, 0, this.width, this.height);
			this.NormalizeAll();
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0001CBAE File Offset: 0x0001ADAE
		public void SetAlpha(TerrainAlphamap.Layer layer, int xBase, int yBase, double[,] alpha)
		{
			this.SetAlpha(layer.FindIndex(), xBase, yBase, alpha);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000FDE10 File Offset: 0x000FC010
		private void SetAlpha(int layer, int xBase, int yBase, double[,] alpha)
		{
			int num = alpha.GetUpperBound(0) + 1;
			int num2 = alpha.GetUpperBound(1) + 1;
			this.VerifyArgs(xBase, yBase, num, num2, layer);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					this.samples[i + xBase, j + yBase, layer] = alpha[i, j];
					this.Normalize(i + xBase, j + yBase);
				}
			}
			this.WriteToTerrain(xBase, yBase, num, num2);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000FDE88 File Offset: 0x000FC088
		public void SetAlpha(int xBase, int yBase, double[,,] alpha)
		{
			int num = alpha.GetUpperBound(0) + 1;
			int num2 = alpha.GetUpperBound(1) + 1;
			int num3 = alpha.GetUpperBound(2) + 1;
			this.VerifyArgs(xBase, yBase, num, num2);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num3; k++)
					{
						this.samples[i + xBase, j + yBase, k] = alpha[i, j, k];
					}
					this.Normalize(i + xBase, j + yBase);
				}
			}
			this.WriteToTerrain(xBase, yBase, num, num2);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000FDF20 File Offset: 0x000FC120
		private void Normalize(int x, int y)
		{
			if (this.numLayers > 0)
			{
				double num = 0.0;
				for (int i = 0; i < this.numLayers; i++)
				{
					num += this.samples[x, y, i];
				}
				if (num > 0.0)
				{
					for (int j = 0; j < this.numLayers; j++)
					{
						this.samples[x, y, j] /= num;
					}
				}
			}
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000FDF94 File Offset: 0x000FC194
		private void NormalizeAll()
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.Normalize(i, j);
				}
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public double[,] GetAlpha(TerrainAlphamap.Layer layer, int xBase, int yBase, int width, int height)
		{
			return this.GetAlpha(layer.FindIndex(), xBase, yBase, width, height);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000FDFCC File Offset: 0x000FC1CC
		private double[,] GetAlpha(int layer, int xBase, int yBase, int width, int height)
		{
			this.VerifyArgs(xBase, yBase, width, height, layer);
			double[,] array = new double[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[i, j] = this.samples[i + xBase, j + yBase, layer];
				}
			}
			return array;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000FE028 File Offset: 0x000FC228
		private void WriteToTerrain(int xBase, int yBase, int width, int height)
		{
			this.VerifyArgs(xBase, yBase, width, height);
			float[,,] array = new float[width, height, this.numLayers];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					for (int k = 0; k < this.numLayers; k++)
					{
						array[j, i, k] = (float)this.samples[i + xBase, j + yBase, k];
					}
				}
			}
			this.terrainData.SetAlphamaps(xBase, yBase, array);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000FE0A4 File Offset: 0x000FC2A4
		private void ReadFromTerrain(int xBase, int yBase, int width, int height)
		{
			this.VerifyArgs(xBase, yBase, width, height);
			float[,,] alphamaps = this.terrainData.GetAlphamaps(xBase, yBase, width, height);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					for (int k = 0; k < this.numLayers; k++)
					{
						this.samples[i + xBase, j + yBase, k] = (double)alphamaps[j, i, k];
					}
				}
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000FE114 File Offset: 0x000FC314
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
				throw new ArgumentException("Trying to access data outside of alphamap.");
			}
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		private void VerifyArgs(int xBase, int yBase, int width, int height, int layer)
		{
			this.VerifyArgs(xBase, yBase, width, height);
			if (layer < 0 || layer >= this.numLayers)
			{
				throw new ArgumentException("Trying to access a layer that does not exist.");
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0001CBFB File Offset: 0x0001ADFB
		public TerrainAlphamap.Layer GetLayer(int layer)
		{
			return this.layers[layer];
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0001CC09 File Offset: 0x0001AE09
		public IEnumerable<TerrainAlphamap.Layer> GetLayers()
		{
			return this.layers;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0001CC11 File Offset: 0x0001AE11
		public TerrainAlphamap.Layer CreateEmptyLayer(MapEditorMaterial material)
		{
			TerrainAlphamap.Layer result = this.AddLayer(material);
			this.samples = new double[this.width, this.height, this.numLayers];
			return result;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000FE188 File Offset: 0x000FC388
		public TerrainAlphamap.Layer CreateLayer(MapEditorMaterial material)
		{
			double[,,] array = new double[this.width, this.height, this.numLayers + 1];
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					for (int k = 0; k < this.numLayers; k++)
					{
						array[i, j, k] = this.samples[i, j, k];
					}
					array[i, j, this.numLayers] = 1E-06;
				}
			}
			this.samples = array;
			TerrainAlphamap.Layer result = this.AddLayer(material);
			this.NormalizeAll();
			this.WriteToTerrain(0, 0, this.width, this.height);
			return result;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000FE238 File Offset: 0x000FC438
		private TerrainAlphamap.Layer AddLayer(MapEditorMaterial material)
		{
			TerrainAlphamap.Layer layer = new TerrainAlphamap.Layer(this, material);
			this.layers.Add(layer);
			List<TerrainLayer> list = this.terrainData.terrainLayers.ToList<TerrainLayer>();
			list.Add(layer.GetMaterial().ToTerrainLayer());
			this.terrainData.terrainLayers = list.ToArray();
			material.onTextureChanged.AddListener(delegate()
			{
				this.MaterialTextureChanged(layer);
			});
			return layer;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000FE2C8 File Offset: 0x000FC4C8
		private void MaterialTextureChanged(TerrainAlphamap.Layer layer)
		{
			int num = layer.FindIndex();
			TerrainLayer[] array = this.terrainData.terrainLayers.ToArray<TerrainLayer>();
			array[num] = layer.GetMaterial().ToTerrainLayer();
			this.terrainData.terrainLayers = array.ToArray<TerrainLayer>();
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000FE30C File Offset: 0x000FC50C
		public void RemoveLayer(TerrainAlphamap.Layer layer)
		{
			if (!this.layers.Contains(layer))
			{
				return;
			}
			int num = layer.FindIndex();
			this.layers.RemoveAt(num);
			List<TerrainLayer> list = this.terrainData.terrainLayers.ToList<TerrainLayer>();
			list.RemoveAt(num);
			this.terrainData.terrainLayers = list.ToArray();
			double[,,] array = new double[this.width, this.height, this.numLayers];
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					for (int k = 0; k < this.numLayers; k++)
					{
						int num2 = (k >= num) ? (k + 1) : k;
						array[i, j, k] = this.samples[i, j, num2];
					}
				}
			}
			this.samples = array;
			this.NormalizeAll();
			this.WriteToTerrain(0, 0, this.width, this.height);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x0001CC37 File Offset: 0x0001AE37
		public TerrainAlphamap.State GetState()
		{
			return new TerrainAlphamap.State(this);
		}

		// Token: 0x0400272E RID: 10030
		public readonly Terrain terrain;

		// Token: 0x0400272F RID: 10031
		public readonly TerrainData terrainData;

		// Token: 0x04002730 RID: 10032
		private List<TerrainAlphamap.Layer> layers = new List<TerrainAlphamap.Layer>();

		// Token: 0x04002731 RID: 10033
		private double[,,] samples;

		// Token: 0x0200066D RID: 1645
		public class State
		{
			// Token: 0x060029E5 RID: 10725 RVA: 0x000FE404 File Offset: 0x000FC604
			public State(TerrainAlphamap alphamap)
			{
				this.alphamap = alphamap;
				this.layers = alphamap.layers.ToArray();
				this.terrainLayers = (TerrainLayer[])alphamap.terrainData.terrainLayers.Clone();
				this.samples = (double[,,])alphamap.samples.Clone();
			}

			// Token: 0x060029E6 RID: 10726 RVA: 0x000FE460 File Offset: 0x000FC660
			public void Revert()
			{
				this.alphamap.layers.Clear();
				this.alphamap.layers.AddRange(this.layers);
				this.alphamap.terrainData.terrainLayers = this.terrainLayers;
				this.alphamap.samples = this.samples;
				this.alphamap.WriteToTerrain(0, 0, this.alphamap.width, this.alphamap.height);
			}

			// Token: 0x04002732 RID: 10034
			public readonly TerrainAlphamap alphamap;

			// Token: 0x04002733 RID: 10035
			public readonly TerrainAlphamap.Layer[] layers;

			// Token: 0x04002734 RID: 10036
			public readonly TerrainLayer[] terrainLayers;

			// Token: 0x04002735 RID: 10037
			public readonly double[,,] samples;
		}

		// Token: 0x0200066E RID: 1646
		public class Layer
		{
			// Token: 0x060029E7 RID: 10727 RVA: 0x0001CC3F File Offset: 0x0001AE3F
			public Layer(TerrainAlphamap alphamap, MapEditorMaterial material)
			{
				this.alphamap = alphamap;
				this.material = material;
			}

			// Token: 0x060029E8 RID: 10728 RVA: 0x0001CC55 File Offset: 0x0001AE55
			public TerrainAlphamap GetAlphamap()
			{
				return this.alphamap;
			}

			// Token: 0x060029E9 RID: 10729 RVA: 0x0001CC5D File Offset: 0x0001AE5D
			public string GetName()
			{
				return this.material.name;
			}

			// Token: 0x060029EA RID: 10730 RVA: 0x0001CC6A File Offset: 0x0001AE6A
			public MapEditorMaterial GetMaterial()
			{
				return this.material;
			}

			// Token: 0x060029EB RID: 10731 RVA: 0x0001CC72 File Offset: 0x0001AE72
			public int FindIndex()
			{
				return this.alphamap.layers.IndexOf(this);
			}

			// Token: 0x04002736 RID: 10038
			private TerrainAlphamap alphamap;

			// Token: 0x04002737 RID: 10039
			private MapEditorMaterial material;
		}
	}
}
