using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000729 RID: 1833
	public class RestorePositionAction : UndoableAction
	{
		// Token: 0x06002DF9 RID: 11769 RVA: 0x0001FAF6 File Offset: 0x0001DCF6
		public RestorePositionAction(Selection selection)
		{
			this.selection = selection;
			this.initialPositions = new List<Vector3>(selection.GetLength());
			this.finalPositions = new List<Vector3>(selection.GetLength());
			this.RecordPositions(this.initialPositions);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public override void Redo()
		{
			this.MoveSelection(this.finalPositions);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0001FB43 File Offset: 0x0001DD43
		public override void Undo()
		{
			this.RecordPositions(this.finalPositions);
			this.MoveSelection(this.initialPositions);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00107914 File Offset: 0x00105B14
		public override long SizeInBytes()
		{
			return 24L * (long)this.initialPositions.Capacity + 24L * (long)this.finalPositions.Capacity + 32L * (long)this.selection.GetLength();
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x00107958 File Offset: 0x00105B58
		private void MoveSelection(List<Vector3> positions)
		{
			int num = 0;
			SelectableObject[] objects = this.selection.GetObjects();
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].transform.position = positions[num];
				num++;
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x0010799C File Offset: 0x00105B9C
		private void RecordPositions(List<Vector3> positions)
		{
			positions.Clear();
			foreach (SelectableObject selectableObject in this.selection.GetObjects())
			{
				positions.Add(selectableObject.transform.position);
			}
		}

		// Token: 0x04002A36 RID: 10806
		public readonly Selection selection;

		// Token: 0x04002A37 RID: 10807
		private List<Vector3> initialPositions;

		// Token: 0x04002A38 RID: 10808
		private List<Vector3> finalPositions;
	}
}
