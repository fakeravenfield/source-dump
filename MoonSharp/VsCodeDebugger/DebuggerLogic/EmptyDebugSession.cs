using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x0200079F RID: 1951
	internal class EmptyDebugSession : DebugSession
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x00020FE6 File Offset: 0x0001F1E6
		internal EmptyDebugSession(MoonSharpVsCodeDebugServer server) : base(true, false)
		{
			this.m_Server = server;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0010C71C File Offset: 0x0010A91C
		public override void Initialize(Response response, Table args)
		{
			this.SendText("Connected to MoonSharp {0} [{1}] on process {2} (PID {3})", new object[]
			{
				"2.0.0.0",
				Script.GlobalOptions.Platform.GetPlatformName(),
				Process.GetCurrentProcess().ProcessName,
				Process.GetCurrentProcess().Id
			});
			this.SendText("No script is set as default for debugging; use the debug console to select the script to debug.\n", Array.Empty<object>());
			this.SendList();
			base.SendResponse(response, new Capabilities
			{
				supportsConfigurationDoneRequest = false,
				supportsFunctionBreakpoints = false,
				supportsConditionalBreakpoints = false,
				supportsEvaluateForHovers = false,
				exceptionBreakpointFilters = new object[0]
			});
			base.SendEvent(new InitializedEvent());
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0010C7C8 File Offset: 0x0010A9C8
		private void SendList()
		{
			int num = this.m_Server.CurrentId ?? -1000;
			this.SendText("==========================================================", Array.Empty<object>());
			foreach (KeyValuePair<int, string> keyValuePair in this.m_Server.GetAttachedDebuggersByIdAndName())
			{
				string text = (keyValuePair.Key == num) ? " (default)" : "";
				this.SendText("{0} : {1}{2}", new object[]
				{
					keyValuePair.Key.ToString().PadLeft(9),
					keyValuePair.Value,
					text
				});
			}
			this.SendText("", Array.Empty<object>());
			this.SendText("Type the number of the script to debug, or '!' to refresh", Array.Empty<object>());
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void Attach(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x00021001 File Offset: 0x0001F201
		public override void Continue(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void Disconnect(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0010C8BC File Offset: 0x0010AABC
		private static string getString(Table args, string property, string dflt = null)
		{
			string text = (string)args[property];
			if (text == null)
			{
				return dflt;
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				return dflt;
			}
			return text;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x0010C8F0 File Offset: 0x0010AAF0
		public override void Evaluate(Response response, Table args)
		{
			string @string = EmptyDebugSession.getString(args, "expression", null);
			if ((EmptyDebugSession.getString(args, "context", null) ?? "hover") == "repl")
			{
				this.ExecuteRepl(@string);
			}
			base.SendResponse(response, null);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x0010C93C File Offset: 0x0010AB3C
		private void ExecuteRepl(string cmd)
		{
			int value = 0;
			if (int.TryParse(cmd, out value))
			{
				this.m_Server.CurrentId = new int?(value);
				this.SendText("Re-attach the debugger to debug the selected script.", Array.Empty<object>());
				this.Unbind();
				return;
			}
			this.SendList();
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void Launch(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00021001 File Offset: 0x0001F201
		public override void Next(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00021001 File Offset: 0x0001F201
		public override void Pause(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void Scopes(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void SetBreakpoints(Response response, Table args)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void StackTrace(Response response, Table args)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00021001 File Offset: 0x0001F201
		public override void StepIn(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x00021001 File Offset: 0x0001F201
		public override void StepOut(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x0010C984 File Offset: 0x0010AB84
		public override void Threads(Response response, Table arguments)
		{
			List<Thread> vars = new List<Thread>
			{
				new Thread(0, "Main Thread")
			};
			base.SendResponse(response, new ThreadsResponseBody(vars));
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public override void Variables(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00021011 File Offset: 0x0001F211
		private void SendText(string msg, params object[] args)
		{
			msg = string.Format(msg, args);
			base.SendEvent(new OutputEvent("console", msg + "\n"));
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00021037 File Offset: 0x0001F237
		public void Unbind()
		{
			this.SendText("Bye.", Array.Empty<object>());
			base.SendEvent(new TerminatedEvent());
		}

		// Token: 0x04002BA4 RID: 11172
		private MoonSharpVsCodeDebugServer m_Server;
	}
}
