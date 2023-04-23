using System;
using System.IO;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200076C RID: 1900
	[Serializable]
	public struct CoverPointsDataV1
	{
		// Token: 0x06002EEC RID: 12012 RVA: 0x0010A440 File Offset: 0x00108640
		public CoverPointsDataV1(MemoryStream stream)
		{
			byte[] bytes = stream.ToArray();
			this.data = UtilsIO.Base64Encode(bytes);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x0010A460 File Offset: 0x00108660
		public MemoryStream ToStream()
		{
			MemoryStream result;
			try
			{
				if (string.IsNullOrEmpty(this.data))
				{
					throw new Exception("No bytes found in Base64 encoded data string.");
				}
				result = new MemoryStream(UtilsIO.Base64Decode(this.data));
			}
			catch (Exception inner)
			{
				throw new MapDescriptorException.InvalidCoverPoints("Unable to decompress or deserialize cover points from descriptor.", inner);
			}
			return result;
		}

		// Token: 0x04002B13 RID: 11027
		public string data;
	}
}
