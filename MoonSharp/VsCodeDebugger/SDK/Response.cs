using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000797 RID: 1943
	public class Response : ProtocolMessage
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		// (set) Token: 0x06002FAE RID: 12206 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public bool success { get; private set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x00020CF1 File Offset: 0x0001EEF1
		// (set) Token: 0x06002FB0 RID: 12208 RVA: 0x00020CF9 File Offset: 0x0001EEF9
		public string message { get; private set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x00020D02 File Offset: 0x0001EF02
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x00020D0A File Offset: 0x0001EF0A
		public int request_seq { get; private set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x00020D13 File Offset: 0x0001EF13
		// (set) Token: 0x06002FB4 RID: 12212 RVA: 0x00020D1B File Offset: 0x0001EF1B
		public string command { get; private set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x00020D24 File Offset: 0x0001EF24
		// (set) Token: 0x06002FB6 RID: 12214 RVA: 0x00020D2C File Offset: 0x0001EF2C
		public ResponseBody body { get; private set; }

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00020D35 File Offset: 0x0001EF35
		public Response(Table req) : base("response")
		{
			this.success = true;
			this.request_seq = req.Get("seq").ToObject<int>();
			this.command = req.Get("command").ToObject<string>();
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00020D75 File Offset: 0x0001EF75
		public void SetBody(ResponseBody bdy)
		{
			this.success = true;
			this.body = bdy;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x00020D85 File Offset: 0x0001EF85
		public void SetErrorBody(string msg, ResponseBody bdy = null)
		{
			this.success = false;
			this.message = msg;
			this.body = bdy;
		}
	}
}
