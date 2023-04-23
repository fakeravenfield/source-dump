using System;
using System.IO;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200045E RID: 1118
	public static class MB_TGAWriter
	{
		// Token: 0x06001C3B RID: 7227 RVA: 0x000B92A8 File Offset: 0x000B74A8
		public static void Write(Color[] pixels, int width, int height, string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			FileStream output = File.Create(path);
			MB_TGAWriter.Write(pixels, width, height, output);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000B92D4 File Offset: 0x000B74D4
		public static void Write(Color[] pixels, int width, int height, Stream output)
		{
			byte[] array = new byte[pixels.Length * 4];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = pixels[num];
					array[num2] = (byte)(color.b * 255f);
					array[num2 + 1] = (byte)(color.g * 255f);
					array[num2 + 2] = (byte)(color.r * 255f);
					array[num2 + 3] = (byte)(color.a * 255f);
					num++;
					num2 += 4;
				}
			}
			byte[] array2 = new byte[18];
			array2[2] = 2;
			array2[12] = (byte)(width & 255);
			array2[13] = (byte)((width & 65280) >> 8);
			array2[14] = (byte)(height & 255);
			array2[15] = (byte)((height & 65280) >> 8);
			array2[16] = 32;
			byte[] buffer = array2;
			using (BinaryWriter binaryWriter = new BinaryWriter(output))
			{
				binaryWriter.Write(buffer);
				binaryWriter.Write(array);
			}
		}
	}
}
