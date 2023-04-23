using System;

namespace Lua
{
	// Token: 0x0200090D RID: 2317
	public sealed class ResolveDynValueAsAttribute : Attribute
	{
		// Token: 0x06003AE7 RID: 15079 RVA: 0x00027AD1 File Offset: 0x00025CD1
		public ResolveDynValueAsAttribute(Type targetType)
		{
			this.targetType = targetType;
		}

		// Token: 0x04003042 RID: 12354
		public Type targetType;
	}
}
