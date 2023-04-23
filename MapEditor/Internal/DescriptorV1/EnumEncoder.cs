using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200076D RID: 1901
	public class EnumEncoder<T>
	{
		// Token: 0x06002EEE RID: 12014 RVA: 0x000204D3 File Offset: 0x0001E6D3
		public string Encode(T value)
		{
			return value.ToString();
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000204E2 File Offset: 0x0001E6E2
		public T Decode(string value)
		{
			return (T)((object)Enum.Parse(typeof(T), value));
		}
	}
}
