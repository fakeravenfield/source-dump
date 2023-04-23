using System;

namespace MapEditor
{
	// Token: 0x0200072F RID: 1839
	public abstract class UndoableAction
	{
		// Token: 0x06002E1E RID: 11806
		public abstract void Undo();

		// Token: 0x06002E1F RID: 11807
		public abstract void Redo();

		// Token: 0x06002E20 RID: 11808
		public abstract void Discard();

		// Token: 0x06002E21 RID: 11809
		public abstract long SizeInBytes();
	}
}
