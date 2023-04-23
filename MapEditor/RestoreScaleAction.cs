using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200072B RID: 1835
	public class RestoreScaleAction : UndoableAction
	{
		// Token: 0x06002E07 RID: 11783 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		public RestoreScaleAction(Selection selection)
		{
			this.selection = selection;
			this.initialScale = new List<Vector3>(selection.GetLength());
			this.finalScale = new List<Vector3>(selection.GetLength());
			this.RecordScale(this.initialScale);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0001FC03 File Offset: 0x0001DE03
		public override void Redo()
		{
			this.ScaleSelection(this.finalScale);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0001FC11 File Offset: 0x0001DE11
		public override void Undo()
		{
			this.RecordScale(this.finalScale);
			this.ScaleSelection(this.initialScale);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00107AB4 File Offset: 0x00105CB4
		public override long SizeInBytes()
		{
			return 24L * (long)this.initialScale.Capacity + 24L * (long)this.finalScale.Capacity + 32L * (long)this.selection.GetLength();
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00107AF8 File Offset: 0x00105CF8
		private void ScaleSelection(List<Vector3> scale)
		{
			int num = 0;
			SelectableObject[] objects = this.selection.GetObjects();
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].transform.localScale = scale[num];
				num++;
			}
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00107B3C File Offset: 0x00105D3C
		private void RecordScale(List<Vector3> scale)
		{
			scale.Clear();
			foreach (SelectableObject selectableObject in this.selection.GetObjects())
			{
				scale.Add(selectableObject.transform.localScale);
			}
		}

		// Token: 0x04002A3C RID: 10812
		public readonly Selection selection;

		// Token: 0x04002A3D RID: 10813
		private List<Vector3> initialScale;

		// Token: 0x04002A3E RID: 10814
		private List<Vector3> finalScale;
	}
}
