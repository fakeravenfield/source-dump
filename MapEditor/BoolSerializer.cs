using System;

namespace MapEditor
{
	// Token: 0x02000656 RID: 1622
	[TypeSerializer(typeof(bool))]
	public class BoolSerializer : ITypeSerializer
	{
		// Token: 0x0600294C RID: 10572 RVA: 0x000FCA10 File Offset: 0x000FAC10
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			bool flag = false;
			if (bool.TryParse(value, out flag))
			{
				return flag;
			}
			return null;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0001C629 File Offset: 0x0001A829
		public string Serialize(object value)
		{
			return value.ToString();
		}
	}
}
