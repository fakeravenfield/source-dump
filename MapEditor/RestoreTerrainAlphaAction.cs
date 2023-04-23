using System;

namespace MapEditor
{
	// Token: 0x0200072C RID: 1836
	public class RestoreTerrainAlphaAction : UndoableAction
	{
		// Token: 0x06002E0E RID: 11790 RVA: 0x0001FC2B File Offset: 0x0001DE2B
		public RestoreTerrainAlphaAction(TerrainAlphamap alphamap)
		{
			this.alphamap = alphamap;
			this.before = alphamap.GetState();
			this.after = null;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0001FC4D File Offset: 0x0001DE4D
		public override void Redo()
		{
			this.after.Revert();
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x0001FC5A File Offset: 0x0001DE5A
		public override void Undo()
		{
			this.after = this.alphamap.GetState();
			this.before.Revert();
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Discard()
		{
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0001FC78 File Offset: 0x0001DE78
		public override long SizeInBytes()
		{
			return 8L * (long)this.before.samples.Length + 64L + ((this.after != null) ? (8L * (long)this.after.samples.Length) : 0L) + 64L;
		}

		// Token: 0x04002A3F RID: 10815
		private readonly TerrainAlphamap alphamap;

		// Token: 0x04002A40 RID: 10816
		private readonly TerrainAlphamap.State before;

		// Token: 0x04002A41 RID: 10817
		private TerrainAlphamap.State after;
	}
}
