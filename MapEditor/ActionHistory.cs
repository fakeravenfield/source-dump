using System;
using System.Collections.Generic;
using System.Linq;

namespace MapEditor
{
	// Token: 0x02000724 RID: 1828
	public class ActionHistory
	{
		// Token: 0x06002DDF RID: 11743 RVA: 0x0001F9E2 File Offset: 0x0001DBE2
		public ActionHistory()
		{
			this.undoStack = new LinkedList<UndoableAction>();
			this.redoStack = new LinkedList<UndoableAction>();
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x0001FA00 File Offset: 0x0001DC00
		public long SizeInBytes()
		{
			return this.undoStack.Sum((UndoableAction u) => u.SizeInBytes());
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x00107608 File Offset: 0x00105808
		public void Add(UndoableAction a)
		{
			this.undoStack.AddLast(a);
			while (this.undoStack.Count > 800)
			{
				this.undoStack.RemoveFirst();
			}
			double num = (double)this.SizeInBytes();
			while (num > 838860800.0 && this.undoStack.Any<UndoableAction>())
			{
				UndoableAction undoableAction = this.undoStack.First<UndoableAction>();
				this.undoStack.RemoveFirst();
				num -= (double)undoableAction.SizeInBytes();
			}
			foreach (UndoableAction undoableAction2 in this.redoStack)
			{
				undoableAction2.Discard();
			}
			this.redoStack.Clear();
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		public bool CanUndo()
		{
			return this.undoStack.Any<UndoableAction>();
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x0001FA39 File Offset: 0x0001DC39
		public bool CanRedo()
		{
			return this.redoStack.Any<UndoableAction>();
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x001076D4 File Offset: 0x001058D4
		public void Undo()
		{
			if (this.CanUndo())
			{
				UndoableAction undoableAction = this.undoStack.Last<UndoableAction>();
				this.undoStack.RemoveLast();
				this.redoStack.AddLast(undoableAction);
				undoableAction.Undo();
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x00107714 File Offset: 0x00105914
		public void Redo()
		{
			if (this.CanRedo())
			{
				UndoableAction undoableAction = this.redoStack.Last<UndoableAction>();
				this.redoStack.RemoveLast();
				this.undoStack.AddLast(undoableAction);
				undoableAction.Redo();
			}
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x00107754 File Offset: 0x00105954
		public T TopOfUndoStack<T>() where T : UndoableAction
		{
			if (this.CanUndo())
			{
				return this.undoStack.Last<UndoableAction>() as T;
			}
			return default(T);
		}

		// Token: 0x04002A2A RID: 10794
		private const long SIZE_LIMIT = 838860800L;

		// Token: 0x04002A2B RID: 10795
		private const int ITEM_LIMIT = 800;

		// Token: 0x04002A2C RID: 10796
		private readonly LinkedList<UndoableAction> undoStack;

		// Token: 0x04002A2D RID: 10797
		private readonly LinkedList<UndoableAction> redoStack;
	}
}
