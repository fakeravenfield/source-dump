using System;

namespace MapEditor
{
	// Token: 0x0200065B RID: 1627
	[TypeSerializer(typeof(MapEditorMaterial))]
	public class MaterialSerializer : ITypeSerializer
	{
		// Token: 0x0600295B RID: 10587 RVA: 0x0001C648 File Offset: 0x0001A848
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			return ctx.materialList.FindOrNull(value);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0001C656 File Offset: 0x0001A856
		public string Serialize(object value)
		{
			return ((MapEditorMaterial)value).guid;
		}
	}
}
