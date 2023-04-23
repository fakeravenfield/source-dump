using System;

namespace MapEditor
{
	// Token: 0x0200072D RID: 1837
	public class SetPropertyAction : UndoableAction
	{
		// Token: 0x06002E13 RID: 11795 RVA: 0x0001FCB7 File Offset: 0x0001DEB7
		public SetPropertyAction(PropertyBinding property, object newValue)
		{
			this.property = property;
			this.oldValue = property.GetValue();
			this.newValue = null;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00107B84 File Offset: 0x00105D84
		public override void Undo()
		{
			this.property.SetValue(this.oldValue);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00107BA8 File Offset: 0x00105DA8
		public override void Redo()
		{
			this.property.SetValue(this.newValue);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x0001FCDA File Offset: 0x0001DEDA
		public override long SizeInBytes()
		{
			return 100L;
		}

		// Token: 0x04002A42 RID: 10818
		public readonly PropertyBinding property;

		// Token: 0x04002A43 RID: 10819
		public object newValue;

		// Token: 0x04002A44 RID: 10820
		private object oldValue;
	}
}
