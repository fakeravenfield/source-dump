using System;

namespace MapEditor
{
	// Token: 0x0200065E RID: 1630
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class ShowInMapEditorAttribute : Attribute
	{
		// Token: 0x06002964 RID: 10596 RVA: 0x0001C682 File Offset: 0x0001A882
		public ShowInMapEditorAttribute()
		{
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0001C695 File Offset: 0x0001A895
		public ShowInMapEditorAttribute(int order)
		{
			this.order = order;
		}

		// Token: 0x040026FA RID: 9978
		public int order;

		// Token: 0x040026FB RID: 9979
		public string name = "";
	}
}
