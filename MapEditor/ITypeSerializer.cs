using System;

namespace MapEditor
{
	// Token: 0x0200064A RID: 1610
	public interface ITypeSerializer
	{
		// Token: 0x0600290D RID: 10509
		string Serialize(object value);

		// Token: 0x0600290E RID: 10510
		object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx);
	}
}
