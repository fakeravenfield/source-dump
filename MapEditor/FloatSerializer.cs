using System;

namespace MapEditor
{
	// Token: 0x02000659 RID: 1625
	[TypeSerializer(typeof(float))]
	public class FloatSerializer : ITypeSerializer
	{
		// Token: 0x06002955 RID: 10581 RVA: 0x000FCA80 File Offset: 0x000FAC80
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			float num = 0f;
			if (float.TryParse(value, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x0001C629 File Offset: 0x0001A829
		public string Serialize(object value)
		{
			return value.ToString();
		}
	}
}
