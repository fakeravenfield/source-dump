using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x0200079D RID: 1949
	internal class AsyncDebugger : IDebugger
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x00020E97 File Offset: 0x0001F097
		// (set) Token: 0x06002FD5 RID: 12245 RVA: 0x00020E9F File Offset: 0x0001F09F
		public DebugService DebugService { get; private set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		// (set) Token: 0x06002FD7 RID: 12247 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		public Regex ErrorRegex { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x00020EB9 File Offset: 0x0001F0B9
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x00020EC1 File Offset: 0x0001F0C1
		public Script Script { get; private set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x00020ECA File Offset: 0x0001F0CA
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x00020ED2 File Offset: 0x0001F0D2
		public bool PauseRequested { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x00020EDB File Offset: 0x0001F0DB
		// (set) Token: 0x06002FDD RID: 12253 RVA: 0x00020EE3 File Offset: 0x0001F0E3
		public string Name { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x00020EEC File Offset: 0x0001F0EC
		// (set) Token: 0x06002FDF RID: 12255 RVA: 0x00020EF4 File Offset: 0x0001F0F4
		public int Id { get; private set; }

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0010C16C File Offset: 0x0010A36C
		public AsyncDebugger(Script script, Func<SourceCode, string> sourceFinder, string name)
		{
			object obj = AsyncDebugger.s_AsyncDebuggerIdLock;
			lock (obj)
			{
				this.Id = AsyncDebugger.s_AsyncDebuggerIdCounter++;
			}
			this.m_SourceFinder = sourceFinder;
			this.ErrorRegex = new Regex("\\A.*\\Z");
			this.Script = script;
			this.m_WatchItems = new List<WatchItem>[6];
			this.Name = name;
			for (int i = 0; i < this.m_WatchItems.Length; i++)
			{
				this.m_WatchItems[i] = new List<WatchItem>(64);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x00020EFD File Offset: 0x0001F0FD
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x0010C234 File Offset: 0x0010A434
		public IAsyncDebuggerClient Client
		{
			get
			{
				return this.m_Client__;
			}
			set
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (this.m_Client__ != null && this.m_Client__ != value)
					{
						this.m_Client__.Unbind();
					}
					if (value != null)
					{
						for (int i = 0; i < this.Script.SourceCodeCount; i++)
						{
							if (this.m_SourcesMap.ContainsKey(i))
							{
								value.OnSourceCodeChanged(i);
							}
						}
					}
					this.m_Client__ = value;
				}
			}
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0010C2C0 File Offset: 0x0010A4C0
		DebuggerAction IDebugger.GetAction(int ip, SourceRef sourceref)
		{
			this.PauseRequested = false;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.SendStopEvent();
				}
			}
			DebuggerAction result;
			for (;;)
			{
				@lock = this.m_Lock;
				lock (@lock)
				{
					if (this.Client == null)
					{
						result = new DebuggerAction
						{
							Action = DebuggerAction.ActionType.Run
						};
						break;
					}
					if (this.m_PendingAction != null)
					{
						DebuggerAction pendingAction = this.m_PendingAction;
						this.m_PendingAction = null;
						result = pendingAction;
						break;
					}
				}
				this.Sleep(10);
			}
			return result;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0010C378 File Offset: 0x0010A578
		public void QueueAction(DebuggerAction action)
		{
			for (;;)
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (this.m_PendingAction == null)
					{
						this.m_PendingAction = action;
						break;
					}
				}
				this.Sleep(10);
			}
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x00020F05 File Offset: 0x0001F105
		private void Sleep(int v)
		{
			Thread.Sleep(10);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0010C3D0 File Offset: 0x0010A5D0
		private DynamicExpression CreateDynExpr(string code)
		{
			DynamicExpression result;
			try
			{
				result = this.Script.CreateDynamicExpression(code);
			}
			catch (Exception ex)
			{
				result = this.Script.CreateConstantDynamicExpression(code, DynValue.NewString(ex.Message));
			}
			return result;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x00020F0E File Offset: 0x0001F10E
		List<DynamicExpression> IDebugger.GetWatchItems()
		{
			return new List<DynamicExpression>();
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x00020F15 File Offset: 0x0001F115
		bool IDebugger.IsPauseRequested()
		{
			return this.PauseRequested;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0000296E File Offset: 0x00000B6E
		void IDebugger.RefreshBreakpoints(IEnumerable<SourceRef> refs)
		{
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x0000296E File Offset: 0x00000B6E
		void IDebugger.SetByteCode(string[] byteCode)
		{
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0010C418 File Offset: 0x0010A618
		void IDebugger.SetSourceCode(SourceCode sourceCode)
		{
			this.m_SourcesMap[sourceCode.SourceID] = sourceCode;
			bool flag = false;
			string text = this.m_SourceFinder(sourceCode);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					if (!File.Exists(text))
					{
						flag = true;
					}
					goto IL_3C;
				}
				catch
				{
					flag = true;
					goto IL_3C;
				}
			}
			flag = true;
			IL_3C:
			if (flag)
			{
				text = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".lua");
				File.WriteAllText(text, sourceCode.Code + this.GetFooterForTempFile());
				this.m_SourcesOverride[sourceCode.SourceID] = text;
			}
			else if (text != sourceCode.Name)
			{
				this.m_SourcesOverride[sourceCode.SourceID] = text;
			}
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.OnSourceCodeChanged(sourceCode.SourceID);
				}
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x00020F1D File Offset: 0x0001F11D
		private string GetFooterForTempFile()
		{
			return "\n\n----------------------------------------------------------------------------------------------------------\n-- This file has been generated by the debugger as a placeholder for a script snippet stored in memory. --\n-- If you restart the host process, the contents of this file are not valid anymore.                    --\n----------------------------------------------------------------------------------------------------------\n";
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00020F24 File Offset: 0x0001F124
		public string GetSourceFile(int sourceId)
		{
			if (this.m_SourcesOverride.ContainsKey(sourceId))
			{
				return this.m_SourcesOverride[sourceId];
			}
			if (this.m_SourcesMap.ContainsKey(sourceId))
			{
				return this.m_SourcesMap[sourceId].Name;
			}
			return null;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00020F62 File Offset: 0x0001F162
		public bool IsSourceOverride(int sourceId)
		{
			return this.m_SourcesOverride.ContainsKey(sourceId);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0010C52C File Offset: 0x0010A72C
		void IDebugger.SignalExecutionEnded()
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.OnExecutionEnded();
				}
			}
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x0010C57C File Offset: 0x0010A77C
		bool IDebugger.SignalRuntimeException(ScriptRuntimeException ex)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client == null)
				{
					return false;
				}
			}
			this.Client.OnException(ex);
			this.PauseRequested = this.ErrorRegex.IsMatch(ex.Message);
			return this.PauseRequested;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0010C5F0 File Offset: 0x0010A7F0
		void IDebugger.Update(WatchType watchType, IEnumerable<WatchItem> items)
		{
			List<WatchItem> list = this.m_WatchItems[(int)watchType];
			list.Clear();
			list.AddRange(items);
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.OnWatchesUpdated(watchType);
				}
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00020F70 File Offset: 0x0001F170
		public List<WatchItem> GetWatches(WatchType watchType)
		{
			return this.m_WatchItems[(int)watchType];
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00020F7A File Offset: 0x0001F17A
		public SourceCode GetSource(int id)
		{
			if (this.m_SourcesMap.ContainsKey(id))
			{
				return this.m_SourcesMap[id];
			}
			return null;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x0010C654 File Offset: 0x0010A854
		public SourceCode FindSourceByName(string path)
		{
			path = path.Replace('\\', '/').ToUpperInvariant();
			foreach (KeyValuePair<int, string> keyValuePair in this.m_SourcesOverride)
			{
				if (keyValuePair.Value.Replace('\\', '/').ToUpperInvariant() == path)
				{
					return this.m_SourcesMap[keyValuePair.Key];
				}
			}
			return this.m_SourcesMap.Values.FirstOrDefault((SourceCode s) => s.Name.Replace('\\', '/').ToUpperInvariant() == path);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00020F98 File Offset: 0x0001F198
		void IDebugger.SetDebugService(DebugService debugService)
		{
			this.DebugService = debugService;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00020FA1 File Offset: 0x0001F1A1
		public DynValue Evaluate(string expression)
		{
			return this.CreateDynExpr(expression).Evaluate(null);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		DebuggerCaps IDebugger.GetDebuggerCaps()
		{
			return DebuggerCaps.CanDebugSourceCode | DebuggerCaps.HasLineBasedBreakpoints;
		}

		// Token: 0x04002B94 RID: 11156
		private static object s_AsyncDebuggerIdLock = new object();

		// Token: 0x04002B95 RID: 11157
		private static int s_AsyncDebuggerIdCounter = 0;

		// Token: 0x04002B96 RID: 11158
		private object m_Lock = new object();

		// Token: 0x04002B97 RID: 11159
		private IAsyncDebuggerClient m_Client__;

		// Token: 0x04002B98 RID: 11160
		private DebuggerAction m_PendingAction;

		// Token: 0x04002B99 RID: 11161
		private List<WatchItem>[] m_WatchItems;

		// Token: 0x04002B9A RID: 11162
		private Dictionary<int, SourceCode> m_SourcesMap = new Dictionary<int, SourceCode>();

		// Token: 0x04002B9B RID: 11163
		private Dictionary<int, string> m_SourcesOverride = new Dictionary<int, string>();

		// Token: 0x04002B9C RID: 11164
		private Func<SourceCode, string> m_SourceFinder;
	}
}
