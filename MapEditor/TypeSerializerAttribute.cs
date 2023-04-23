using System;

namespace MapEditor
{
	// Token: 0x0200065F RID: 1631
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	internal sealed class TypeSerializerAttribute : Attribute
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x0001C6AF File Offset: 0x0001A8AF
		public TypeSerializerAttribute(Type serializedType)
		{
			this.serializedType = serializedType;
		}

		// Token: 0x040026FC RID: 9980
		public Type serializedType;
	}
}
