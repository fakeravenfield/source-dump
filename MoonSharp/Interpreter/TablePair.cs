using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007BC RID: 1980
	public struct TablePair
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x00021F59 File Offset: 0x00020159
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x00021F61 File Offset: 0x00020161
		public DynValue Key
		{
			get
			{
				return this.key;
			}
			private set
			{
				this.Key = this.key;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x00021F6F File Offset: 0x0002016F
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x00021F77 File Offset: 0x00020177
		public DynValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.key.IsNotNil())
				{
					this.Value = value;
				}
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x00021F8D File Offset: 0x0002018D
		public TablePair(DynValue key, DynValue val)
		{
			this.key = key;
			this.value = val;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x00021F9D File Offset: 0x0002019D
		public static TablePair Nil
		{
			get
			{
				return TablePair.s_NilNode;
			}
		}

		// Token: 0x04002C17 RID: 11287
		private static TablePair s_NilNode = new TablePair(DynValue.Nil, DynValue.Nil);

		// Token: 0x04002C18 RID: 11288
		private DynValue key;

		// Token: 0x04002C19 RID: 11289
		private DynValue value;
	}
}
