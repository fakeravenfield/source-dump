using System;

namespace MapEditor
{
	// Token: 0x020005CA RID: 1482
	[Serializable]
	public class ReplacementTableData
	{
		// Token: 0x06002672 RID: 9842 RVA: 0x0001A98D File Offset: 0x00018B8D
		public ReplacementTableData()
		{
			this.entries = new ReplacementTableData.Entry[0];
		}

		// Token: 0x040024D0 RID: 9424
		public ReplacementTableData.Entry[] entries;

		// Token: 0x020005CB RID: 1483
		[Serializable]
		public struct Entry
		{
			// Token: 0x040024D1 RID: 9425
			public string old;

			// Token: 0x040024D2 RID: 9426
			public string @new;
		}
	}
}
