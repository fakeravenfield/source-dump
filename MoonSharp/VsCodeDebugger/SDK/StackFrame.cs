using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200077F RID: 1919
	public class StackFrame
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000207B9 File Offset: 0x0001E9B9
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000207C1 File Offset: 0x0001E9C1
		public int id { get; private set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000207CA File Offset: 0x0001E9CA
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000207D2 File Offset: 0x0001E9D2
		public Source source { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000207DB File Offset: 0x0001E9DB
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x000207E3 File Offset: 0x0001E9E3
		public int line { get; private set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000207EC File Offset: 0x0001E9EC
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x000207F4 File Offset: 0x0001E9F4
		public int column { get; private set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000207FD File Offset: 0x0001E9FD
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x00020805 File Offset: 0x0001EA05
		public string name { get; private set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x0002080E File Offset: 0x0001EA0E
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x00020816 File Offset: 0x0001EA16
		public int? endLine { get; private set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x0002081F File Offset: 0x0001EA1F
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x00020827 File Offset: 0x0001EA27
		public int? endColumn { get; private set; }

		// Token: 0x06002F4C RID: 12108 RVA: 0x00020830 File Offset: 0x0001EA30
		public StackFrame(int id, string name, Source source, int line, int column = 0, int? endLine = null, int? endColumn = null)
		{
			this.id = id;
			this.name = name;
			this.source = source;
			this.line = line;
			this.column = column;
			this.endLine = endLine;
			this.endColumn = endColumn;
		}
	}
}
