using System;
using System.IO;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000783 RID: 1923
	public class Source
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x00020967 File Offset: 0x0001EB67
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x0002096F File Offset: 0x0001EB6F
		public string name { get; private set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x00020978 File Offset: 0x0001EB78
		// (set) Token: 0x06002F63 RID: 12131 RVA: 0x00020980 File Offset: 0x0001EB80
		public string path { get; private set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x00020989 File Offset: 0x0001EB89
		// (set) Token: 0x06002F65 RID: 12133 RVA: 0x00020991 File Offset: 0x0001EB91
		public int sourceReference { get; private set; }

		// Token: 0x06002F66 RID: 12134 RVA: 0x0002099A File Offset: 0x0001EB9A
		public Source(string name, string path, int sourceReference = 0)
		{
			this.name = name;
			this.path = path;
			this.sourceReference = sourceReference;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000209B7 File Offset: 0x0001EBB7
		public Source(string path, int sourceReference = 0)
		{
			this.name = Path.GetFileName(path);
			this.path = path;
			this.sourceReference = sourceReference;
		}
	}
}
