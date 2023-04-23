using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200077E RID: 1918
	public class Message
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0002072D File Offset: 0x0001E92D
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x00020735 File Offset: 0x0001E935
		public int id { get; private set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x0002073E File Offset: 0x0001E93E
		// (set) Token: 0x06002F36 RID: 12086 RVA: 0x00020746 File Offset: 0x0001E946
		public string format { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x0002074F File Offset: 0x0001E94F
		// (set) Token: 0x06002F38 RID: 12088 RVA: 0x00020757 File Offset: 0x0001E957
		public object variables { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x00020760 File Offset: 0x0001E960
		// (set) Token: 0x06002F3A RID: 12090 RVA: 0x00020768 File Offset: 0x0001E968
		public object showUser { get; private set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x00020771 File Offset: 0x0001E971
		// (set) Token: 0x06002F3C RID: 12092 RVA: 0x00020779 File Offset: 0x0001E979
		public object sendTelemetry { get; private set; }

		// Token: 0x06002F3D RID: 12093 RVA: 0x00020782 File Offset: 0x0001E982
		public Message(int id, string format, object variables = null, bool user = true, bool telemetry = false)
		{
			this.id = id;
			this.format = format;
			this.variables = variables;
			this.showUser = user;
			this.sendTelemetry = telemetry;
		}
	}
}
