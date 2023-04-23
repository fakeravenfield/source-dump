using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200072A RID: 1834
	public class RestoreRotationAction : UndoableAction
	{
		// Token: 0x06002E00 RID: 11776 RVA: 0x0001FB5D File Offset: 0x0001DD5D
		public RestoreRotationAction(Selection selection)
		{
			this.selection = selection;
			this.initialRotations = new List<Quaternion>(selection.GetLength());
			this.finalRotations = new List<Quaternion>(selection.GetLength());
			this.RecordRotations(this.initialRotations);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		public override void Redo()
		{
			this.RotateSelection(this.finalRotations);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0001FBAA File Offset: 0x0001DDAA
		public override void Undo()
		{
			this.RecordRotations(this.finalRotations);
			this.RotateSelection(this.initialRotations);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x001079E4 File Offset: 0x00105BE4
		public override long SizeInBytes()
		{
			return 24L * (long)this.initialRotations.Capacity + 24L * (long)this.finalRotations.Capacity + 32L * (long)this.selection.GetLength();
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x00107A28 File Offset: 0x00105C28
		private void RotateSelection(List<Quaternion> quaternions)
		{
			int num = 0;
			SelectableObject[] objects = this.selection.GetObjects();
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].transform.rotation = quaternions[num];
				num++;
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00107A6C File Offset: 0x00105C6C
		private void RecordRotations(List<Quaternion> quaternions)
		{
			quaternions.Clear();
			foreach (SelectableObject selectableObject in this.selection.GetObjects())
			{
				quaternions.Add(selectableObject.transform.rotation);
			}
		}

		// Token: 0x04002A39 RID: 10809
		public readonly Selection selection;

		// Token: 0x04002A3A RID: 10810
		private List<Quaternion> initialRotations;

		// Token: 0x04002A3B RID: 10811
		private List<Quaternion> finalRotations;
	}
}
