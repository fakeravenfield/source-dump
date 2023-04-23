using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000754 RID: 1876
	[Serializable]
	public struct KeyValueDataV1
	{
		// Token: 0x06002EA0 RID: 11936 RVA: 0x0002010F File Offset: 0x0001E30F
		public KeyValueDataV1(string key, string value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x04002AAA RID: 10922
		public string key;

		// Token: 0x04002AAB RID: 10923
		public string value;
	}
}
