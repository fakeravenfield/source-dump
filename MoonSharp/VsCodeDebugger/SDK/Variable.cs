using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000781 RID: 1921
	public class Variable
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000208BD File Offset: 0x0001EABD
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x000208C5 File Offset: 0x0001EAC5
		public string name { get; private set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000208CE File Offset: 0x0001EACE
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x000208D6 File Offset: 0x0001EAD6
		public string value { get; private set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000208DF File Offset: 0x0001EADF
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000208E7 File Offset: 0x0001EAE7
		public int variablesReference { get; private set; }

		// Token: 0x06002F5A RID: 12122 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public Variable(string name, string value, int variablesReference = 0)
		{
			this.name = name;
			this.value = value;
			this.variablesReference = variablesReference;
		}
	}
}
