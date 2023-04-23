using System;

namespace MapEditor
{
	// Token: 0x0200065C RID: 1628
	[TypeSerializer(typeof(string))]
	public class StringSerializer : ITypeSerializer
	{
		// Token: 0x0600295E RID: 10590 RVA: 0x000091D3 File Offset: 0x000073D3
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			return value;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0001C629 File Offset: 0x0001A829
		public string Serialize(object value)
		{
			return value.ToString();
		}
	}
}
