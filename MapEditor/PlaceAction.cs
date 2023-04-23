using System;

namespace MapEditor
{
	// Token: 0x02000727 RID: 1831
	public class PlaceAction : UndoableAction
	{
		// Token: 0x06002DEF RID: 11759 RVA: 0x0001FA74 File Offset: 0x0001DC74
		public PlaceAction(params MapEditorObject[] editorObjects)
		{
			this.editorObjects = editorObjects;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x00107890 File Offset: 0x00105A90
		public override void Redo()
		{
			MapEditorObject[] array = this.editorObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Undelete();
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x001078BC File Offset: 0x00105ABC
		public override void Undo()
		{
			MapEditorObject[] array = this.editorObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Delete();
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x001078E8 File Offset: 0x00105AE8
		public override void Discard()
		{
			MapEditorObject[] array = this.editorObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Destroy();
			}
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x0001FA83 File Offset: 0x0001DC83
		public override long SizeInBytes()
		{
			return 262144L * (long)this.editorObjects.Length;
		}

		// Token: 0x04002A32 RID: 10802
		private readonly MapEditorObject[] editorObjects;
	}
}
