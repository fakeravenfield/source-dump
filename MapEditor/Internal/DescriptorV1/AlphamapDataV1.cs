using System;
using System.IO;
using System.Linq;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000768 RID: 1896
	[Serializable]
	public struct AlphamapDataV1
	{
		// Token: 0x06002EE0 RID: 12000 RVA: 0x0010A0C0 File Offset: 0x001082C0
		public AlphamapDataV1(TerrainAlphamap alphamap)
		{
			this.resolution = alphamap.width;
			this.data = AlphamapDataV1.EncodeAlphamap(alphamap);
			this.layers = (from layer in alphamap.GetLayers()
			select new AlphamapLayerDataV1(layer)).ToArray<AlphamapLayerDataV1>();
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x0010A11C File Offset: 0x0010831C
		private static string EncodeAlphamap(TerrainAlphamap alphamap)
		{
			int width = alphamap.width;
			int height = alphamap.height;
			int numLayers = alphamap.numLayers;
			if (width != height)
			{
				throw new ArgumentException("Terrain alphamap must be square.", "terrain");
			}
			byte[] bytes = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					for (int i = 0; i < numLayers; i++)
					{
						TerrainAlphamap.Layer layer = alphamap.GetLayer(i);
						double[,] alpha = alphamap.GetAlpha(layer, 0, 0, width, height);
						for (int j = 0; j < width; j++)
						{
							for (int k = 0; k < height; k++)
							{
								float value = (float)alpha[j, k];
								binaryWriter.Write(value);
							}
						}
					}
					binaryWriter.Flush();
					bytes = memoryStream.ToArray();
				}
			}
			return UtilsIO.Base64Encode(bytes);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x0010A210 File Offset: 0x00108410
		public void CopyTo(TerrainAlphamap alphamap, MaterialList materialList)
		{
			if (alphamap.width != this.resolution)
			{
				throw new MapDescriptorException.InvalidTerrainData(string.Format("Invalid terrain alphamap resolution: must be equal to {0}. Scene not configured correctly.", this.resolution), null);
			}
			AlphamapLayerDataV1[] array = this.layers;
			for (int i = 0; i < array.Length; i++)
			{
				MapEditorMaterial material = array[i].FindMaterial(materialList);
				alphamap.CreateEmptyLayer(material);
			}
			this.DecodeAlphamap(alphamap);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x0010A278 File Offset: 0x00108478
		private void DecodeAlphamap(TerrainAlphamap alphamap)
		{
			int num = this.resolution;
			int num2 = this.resolution;
			int num3 = this.layers.Length;
			if (num <= 0 || num > 10000 || num2 <= 0 || num2 > 10000)
			{
				throw new MapDescriptorException.InvalidTerrainData("Invalid terrain alphamap size: must be larger than zero and less than 10k.", null);
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(UtilsIO.Base64Decode(this.data)))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						double[,,] array = new double[num, num2, num3];
						for (int i = 0; i < num3; i++)
						{
							for (int j = 0; j < num; j++)
							{
								for (int k = 0; k < num2; k++)
								{
									float num4 = binaryReader.ReadSingle();
									array[j, k, i] = (double)num4;
								}
							}
						}
						try
						{
							alphamap.SetAlpha(0, 0, array);
						}
						catch (Exception inner)
						{
							throw new MapDescriptorException.InvalidTerrainData("Alphamap size does not match size of terrain.", inner);
						}
					}
				}
			}
			catch (Exception inner2)
			{
				throw new MapDescriptorException.InvalidTerrainData("Unable to decode terrain alphamap.", inner2);
			}
		}

		// Token: 0x04002B0C RID: 11020
		public int resolution;

		// Token: 0x04002B0D RID: 11021
		public AlphamapLayerDataV1[] layers;

		// Token: 0x04002B0E RID: 11022
		public string data;
	}
}
