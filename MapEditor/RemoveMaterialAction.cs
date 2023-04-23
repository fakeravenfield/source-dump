using System;

namespace MapEditor
{
	// Token: 0x02000728 RID: 1832
	public class RemoveMaterialAction : UndoableAction
	{
		// Token: 0x06002DF4 RID: 11764 RVA: 0x0001FA95 File Offset: 0x0001DC95
		public RemoveMaterialAction(MaterialList materialList, MapEditorMaterial material)
		{
			this.materialList = materialList;
			this.removedMaterial = material;
			this.originalMaterial = material.ShallowCopy();
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0001FAB7 File Offset: 0x0001DCB7
		public override void Undo()
		{
			this.removedMaterial.CopyFrom(this.originalMaterial);
			this.materialList.Add(this.removedMaterial);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0001FADB File Offset: 0x0001DCDB
		public override void Redo()
		{
			this.materialList.Remove(this.removedMaterial);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0001FAEE File Offset: 0x0001DCEE
		public override long SizeInBytes()
		{
			return 288L;
		}

		// Token: 0x04002A33 RID: 10803
		private readonly MaterialList materialList;

		// Token: 0x04002A34 RID: 10804
		private readonly MapEditorMaterial removedMaterial;

		// Token: 0x04002A35 RID: 10805
		private readonly MapEditorMaterial originalMaterial;
	}
}
