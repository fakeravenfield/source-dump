using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x020007A0 RID: 1952
	internal interface IAsyncDebuggerClient
	{
		// Token: 0x06003010 RID: 12304
		void SendStopEvent();

		// Token: 0x06003011 RID: 12305
		void OnWatchesUpdated(WatchType watchType);

		// Token: 0x06003012 RID: 12306
		void OnSourceCodeChanged(int sourceID);

		// Token: 0x06003013 RID: 12307
		void OnExecutionEnded();

		// Token: 0x06003014 RID: 12308
		void OnException(ScriptRuntimeException ex);

		// Token: 0x06003015 RID: 12309
		void Unbind();
	}
}
