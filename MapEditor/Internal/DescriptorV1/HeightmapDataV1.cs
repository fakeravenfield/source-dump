using System;
using System.IO;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000767 RID: 1895
	[Serializable]
	public struct HeightmapDataV1
	{
		// Token: 0x06002EDE RID: 11998 RVA: 0x00109ED8 File Offset: 0x001080D8
		public HeightmapDataV1(TerrainHeightmap heightmap)
		{
			int width = heightmap.width;
			int height = heightmap.height;
			if (width != height)
			{
				throw new MapDescriptorException("Terrain heighmap must be square.", null);
			}
			double[,] heights = heightmap.GetHeights(0, 0, width, height);
			byte[] bytes = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					for (int i = 0; i < width; i++)
					{
						for (int j = 0; j < height; j++)
						{
							float value = (float)heights[i, j];
							binaryWriter.Write(value);
						}
					}
					binaryWriter.Flush();
					bytes = memoryStream.ToArray();
				}
			}
			this.resolution = width;
			this.data = UtilsIO.Base64Encode(bytes);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00109FB0 File Offset: 0x001081B0
		public void CopyTo(TerrainHeightmap heightmap)
		{
			int num = this.resolution;
			int num2 = this.resolution;
			if (num <= 0 || num > 10000 || num2 <= 0 || num2 > 10000)
			{
				throw new MapDescriptorException.InvalidTerrainData("Invalid terrain heightmap size: must be larger than zero and less than 10k.", null);
			}
			double[,] array = new double[num, num2];
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(UtilsIO.Base64Decode(this.data)))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						for (int i = 0; i < num; i++)
						{
							for (int j = 0; j < num2; j++)
							{
								float num3 = binaryReader.ReadSingle();
								array[i, j] = (double)num3;
							}
						}
					}
				}
			}
			catch (Exception inner)
			{
				throw new MapDescriptorException.InvalidTerrainData("Unable to decode terrain heightmap. Invalid compression or Base64 encoding.", inner);
			}
			try
			{
				heightmap.SetHeights(0, 0, array);
			}
			catch (Exception inner2)
			{
				throw new MapDescriptorException.InvalidTerrainData("Heightmap size does not match size of terrain.", inner2);
			}
		}

		// Token: 0x04002B0A RID: 11018
		public int resolution;

		// Token: 0x04002B0B RID: 11019
		public string data;
	}
}
