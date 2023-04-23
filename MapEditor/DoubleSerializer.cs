using System;

namespace MapEditor
{
	// Token: 0x02000658 RID: 1624
	[TypeSerializer(typeof(double))]
	public class DoubleSerializer : ITypeSerializer
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x000FCA54 File Offset: 0x000FAC54
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			double num = 0.0;
			if (double.TryParse(value, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0001C629 File Offset: 0x0001A829
		public string Serialize(object value)
		{
			return value.ToString();
		}
	}
}
