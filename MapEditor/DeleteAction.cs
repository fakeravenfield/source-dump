using System;

namespace MapEditor
{
	// Token: 0x02000726 RID: 1830
	public class DeleteAction : UndoableAction
	{
		// Token: 0x06002DEA RID: 11754 RVA: 0x0001FA5A File Offset: 0x0001DC5A
		public DeleteAction(Selection selection)
		{
			this.editor = MapEditor.instance;
			this.selection = selection;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x00107788 File Offset: 0x00105988
		public override void Redo()
		{
			foreach (SelectableObject selectableObject in this.selection.GetObjects())
			{
				if (!selectableObject.IsActionDisabled(MapEditor.Action.Delete))
				{
					selectableObject.Delete();
				}
			}
			this.editor.SetSelection(this.selection.RemoveDestroyed());
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x001077E0 File Offset: 0x001059E0
		public override void Undo()
		{
			foreach (SelectableObject selectableObject in this.selection.GetObjects())
			{
				if (!selectableObject.IsActionDisabled(MapEditor.Action.Delete))
				{
					selectableObject.Undelete();
				}
			}
			this.editor.SetSelection(this.selection);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x00107830 File Offset: 0x00105A30
		public override void Discard()
		{
			foreach (SelectableObject selectableObject in this.selection.GetObjects())
			{
				if (selectableObject.IsDeleted())
				{
					selectableObject.Destroy();
				}
			}
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0010786C File Offset: 0x00105A6C
		public override long SizeInBytes()
		{
			return 262144L * (long)this.selection.GetLength();
		}

		// Token: 0x04002A30 RID: 10800
		private readonly MapEditor editor;

		// Token: 0x04002A31 RID: 10801
		private readonly Selection selection;
	}
}
