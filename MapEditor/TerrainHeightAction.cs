using System;

namespace MapEditor
{
	// Token: 0x0200072E RID: 1838
	public class TerrainHeightAction : UndoableAction
	{
		// Token: 0x06002E18 RID: 11800 RVA: 0x0001FCDF File Offset: 0x0001DEDF
		public TerrainHeightAction(HeighmapRegion before)
		{
			this.before = before;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x0001FCEE File Offset: 0x0001DEEE
		public void SetAfter(HeighmapRegion after)
		{
			this.after = after;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		public override void Redo()
		{
			this.after.Apply();
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x00107BCC File Offset: 0x00105DCC
		public override void Undo()
		{
			this.before.Apply();
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x0001FD04 File Offset: 0x0001DF04
		public override long SizeInBytes()
		{
			return 8L * (long)this.before.heights.Length + 64L + 8L * (long)this.after.heights.Length + 64L;
		}

		// Token: 0x04002A45 RID: 10821
		private readonly HeighmapRegion before;

		// Token: 0x04002A46 RID: 10822
		private HeighmapRegion after;
	}
}
