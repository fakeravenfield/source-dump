using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000795 RID: 1941
	public class Request : ProtocolMessage
	{
		// Token: 0x06002FAB RID: 12203 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		public Request(int id, string cmd, Table arg) : base("request", id)
		{
			this.command = cmd;
			this.arguments = arg;
		}

		// Token: 0x04002B7B RID: 11131
		public string command;

		// Token: 0x04002B7C RID: 11132
		public Table arguments;
	}
}
