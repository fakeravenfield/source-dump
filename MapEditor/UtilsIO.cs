using System;
using System.IO;
using System.IO.Compression;

namespace MapEditor
{
	// Token: 0x020005F1 RID: 1521
	public static class UtilsIO
	{
		// Token: 0x06002703 RID: 9987 RVA: 0x000F6200 File Offset: 0x000F4400
		public static void CopyStream(Stream input, Stream output)
		{
			byte[] array = new byte[4096];
			int num;
			do
			{
				num = input.Read(array, 0, array.Length);
				output.Write(array, 0, num);
			}
			while (num > 0);
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000F6234 File Offset: 0x000F4434
		public static string Base64Encode(byte[] bytes)
		{
			string result = null;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
					{
						gzipStream.Write(bytes, 0, bytes.Length);
					}
					result = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("Unable to compress or encode Base64 data.", innerException);
			}
			return result;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000F62B8 File Offset: 0x000F44B8
		public static byte[] Base64Decode(string base64)
		{
			byte[] result = null;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(base64)))
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							UtilsIO.CopyStream(gzipStream, memoryStream2);
							result = memoryStream2.ToArray();
						}
					}
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("Unable to decompress or decode Base64 data.", innerException);
			}
			return result;
		}
	}
}
