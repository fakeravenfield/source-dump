using System;

namespace MapEditor
{
	// Token: 0x0200065A RID: 1626
	[TypeSerializer(typeof(int))]
	public class IntSerializer : ITypeSerializer
	{
		// Token: 0x06002958 RID: 10584 RVA: 0x000FCAA8 File Offset: 0x000FACA8
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			int num = 0;
			if (int.TryParse(value, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x0001C629 File Offset: 0x0001A829
		public string Serialize(object value)
		{
			return value.ToString();
		}
	}
}
