using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000780 RID: 1920
	public class Scope
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x0002086D File Offset: 0x0001EA6D
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x00020875 File Offset: 0x0001EA75
		public string name { get; private set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x0002087E File Offset: 0x0001EA7E
		// (set) Token: 0x06002F50 RID: 12112 RVA: 0x00020886 File Offset: 0x0001EA86
		public int variablesReference { get; private set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x0002088F File Offset: 0x0001EA8F
		// (set) Token: 0x06002F52 RID: 12114 RVA: 0x00020897 File Offset: 0x0001EA97
		public bool expensive { get; private set; }

		// Token: 0x06002F53 RID: 12115 RVA: 0x000208A0 File Offset: 0x0001EAA0
		public Scope(string name, int variablesReference, bool expensive = false)
		{
			this.name = name;
			this.variablesReference = variablesReference;
			this.expensive = expensive;
		}
	}
}
