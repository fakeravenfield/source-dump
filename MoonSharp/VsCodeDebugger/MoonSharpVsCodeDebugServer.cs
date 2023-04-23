using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.VsCodeDebugger.DebuggerLogic;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger
{
	// Token: 0x02000775 RID: 1909
	public class MoonSharpVsCodeDebugServer : IDisposable
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x00020625 File Offset: 0x0001E825
		public int Port
		{
			get
			{
				return this.m_Port;
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0002062D File Offset: 0x0001E82D
		public MoonSharpVsCodeDebugServer(int port = 41912)
		{
			this.m_Port = port;
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0010AFA4 File Offset: 0x001091A4
		[Obsolete("Use the constructor taking only a port, and the 'Attach' method instead.")]
		public MoonSharpVsCodeDebugServer(Script script, int port, Func<SourceCode, string> sourceFinder = null)
		{
			this.m_Port = port;
			Func<SourceCode, string> sourceFinder2 = sourceFinder;
			if (sourceFinder == null && (sourceFinder2 = MoonSharpVsCodeDebugServer.<>c.<>9__9_0) == null)
			{
				sourceFinder2 = (MoonSharpVsCodeDebugServer.<>c.<>9__9_0 = ((SourceCode s) => s.Name));
			}
			this.m_Current = new AsyncDebugger(script, sourceFinder2, "Default script");
			this.m_DebuggerList.Add(this.m_Current);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0010B028 File Offset: 0x00109228
		public void AttachToScript(Script script, string name, Func<SourceCode, string> sourceFinder = null)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.m_DebuggerList.Any((AsyncDebugger d) => d.Script == script))
				{
					throw new ArgumentException("Script already attached to this debugger.");
				}
				Script script2 = script;
				Func<SourceCode, string> sourceFinder2 = sourceFinder;
				if (sourceFinder == null && (sourceFinder2 = MoonSharpVsCodeDebugServer.<>c.<>9__10_1) == null)
				{
					sourceFinder2 = (MoonSharpVsCodeDebugServer.<>c.<>9__10_1 = ((SourceCode s) => s.Name));
				}
				AsyncDebugger asyncDebugger = new AsyncDebugger(script2, sourceFinder2, name);
				script.AttachDebugger(asyncDebugger);
				this.m_DebuggerList.Add(asyncDebugger);
				if (this.m_Current == null)
				{
					this.m_Current = asyncDebugger;
				}
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0010B0EC File Offset: 0x001092EC
		public IEnumerable<KeyValuePair<int, string>> GetAttachedDebuggersByIdAndName()
		{
			object @lock = this.m_Lock;
			IEnumerable<KeyValuePair<int, string>> result;
			lock (@lock)
			{
				result = (from d in this.m_DebuggerList
				orderby d.Id
				select new KeyValuePair<int, string>(d.Id, d.Name)).ToArray<KeyValuePair<int, string>>();
			}
			return result;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0010B17C File Offset: 0x0010937C
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x0010B1DC File Offset: 0x001093DC
		public int? CurrentId
		{
			get
			{
				object @lock = this.m_Lock;
				int? num;
				lock (@lock)
				{
					int? num2;
					if (this.m_Current == null)
					{
						num = null;
						num2 = num;
					}
					else
					{
						num2 = new int?(this.m_Current.Id);
					}
					num = num2;
				}
				return num;
			}
			set
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (value == null)
					{
						this.m_Current = null;
					}
					else
					{
						AsyncDebugger asyncDebugger = this.m_DebuggerList.FirstOrDefault(delegate(AsyncDebugger d)
						{
							int id = d.Id;
							int? value2 = value;
							return id == value2.GetValueOrDefault() & value2 != null;
						});
						if (asyncDebugger == null)
						{
							throw new ArgumentException("Cannot find debugger with given Id.");
						}
						this.m_Current = asyncDebugger;
					}
				}
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x0010B268 File Offset: 0x00109468
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x0010B2BC File Offset: 0x001094BC
		public Script Current
		{
			get
			{
				object @lock = this.m_Lock;
				Script result;
				lock (@lock)
				{
					result = ((this.m_Current != null) ? this.m_Current.Script : null);
				}
				return result;
			}
			set
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (value == null)
					{
						this.m_Current = null;
					}
					else
					{
						AsyncDebugger asyncDebugger = this.m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Script == value);
						if (asyncDebugger == null)
						{
							throw new ArgumentException("Cannot find debugger with given script associated.");
						}
						this.m_Current = asyncDebugger;
					}
				}
			}
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0010B344 File Offset: 0x00109544
		public void Detach(Script script)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				AsyncDebugger asyncDebugger = this.m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Script == script);
				if (asyncDebugger == null)
				{
					throw new ArgumentException("Cannot detach script - not found.");
				}
				asyncDebugger.Client = null;
				this.m_DebuggerList.Remove(asyncDebugger);
				if (this.m_Current == asyncDebugger)
				{
					if (this.m_DebuggerList.Count > 0)
					{
						this.m_Current = this.m_DebuggerList[this.m_DebuggerList.Count - 1];
					}
					else
					{
						this.m_Current = null;
					}
				}
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x0002065E File Offset: 0x0001E85E
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x00020666 File Offset: 0x0001E866
		public Action<string> Logger { get; set; }

		// Token: 0x06002F18 RID: 12056 RVA: 0x0010B404 File Offset: 0x00109604
		[Obsolete("Use the Attach method instead.")]
		public IDebugger GetDebugger()
		{
			object @lock = this.m_Lock;
			IDebugger current;
			lock (@lock)
			{
				current = this.m_Current;
			}
			return current;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0002066F File Offset: 0x0001E86F
		public void Dispose()
		{
			this.m_StopEvent.Set();
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0010B448 File Offset: 0x00109648
		public MoonSharpVsCodeDebugServer Start()
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.m_Started)
				{
					throw new InvalidOperationException("Cannot start; server has already been started.");
				}
				this.m_StopEvent.Reset();
				TcpListener serverSocket = null;
				serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), this.m_Port);
				serverSocket.Start();
				MoonSharpVsCodeDebugServer.SpawnThread("VsCodeDebugServer_" + this.m_Port.ToString(), delegate
				{
					this.ListenThread(serverSocket);
				});
				this.m_Started = true;
			}
			return this;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0010B50C File Offset: 0x0010970C
		private void ListenThread(TcpListener serverSocket)
		{
			try
			{
				while (!this.m_StopEvent.WaitOne(0))
				{
					Socket clientSocket = serverSocket.AcceptSocket();
					if (clientSocket != null)
					{
						string sessionId = Guid.NewGuid().ToString("N");
						this.Log("[{0}] : Accepted connection from client {1}", new object[]
						{
							sessionId,
							clientSocket.RemoteEndPoint
						});
						MoonSharpVsCodeDebugServer.SpawnThread("VsCodeDebugSession_" + sessionId, delegate
						{
							using (NetworkStream networkStream = new NetworkStream(clientSocket))
							{
								try
								{
									this.RunSession(sessionId, networkStream);
								}
								catch (Exception ex2)
								{
									this.Log("[{0}] : Error : {1}", new object[]
									{
										ex2.Message
									});
								}
							}
							clientSocket.Close();
							this.Log("[{0}] : Client connection closed", new object[]
							{
								sessionId
							});
						});
					}
				}
			}
			catch (Exception ex)
			{
				this.Log("Fatal error in listening thread : {0}", new object[]
				{
					ex.Message
				});
			}
			finally
			{
				if (serverSocket != null)
				{
					serverSocket.Stop();
				}
			}
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x0010B5F8 File Offset: 0x001097F8
		private void RunSession(string sessionId, NetworkStream stream)
		{
			DebugSession debugSession = null;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.m_Current != null)
				{
					debugSession = new MoonSharpDebugSession(this, this.m_Current);
				}
				else
				{
					debugSession = new EmptyDebugSession(this);
				}
			}
			debugSession.ProcessLoop(stream, stream);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x0010B65C File Offset: 0x0010985C
		private void Log(string format, params object[] args)
		{
			Action<string> logger = this.Logger;
			if (logger != null)
			{
				string obj = string.Format(format, args);
				logger(obj);
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x0002067D File Offset: 0x0001E87D
		private static void SpawnThread(string name, Action threadProc)
		{
			new System.Threading.Thread(delegate()
			{
				threadProc();
			})
			{
				IsBackground = true,
				Name = name
			}.Start();
		}

		// Token: 0x04002B38 RID: 11064
		private object m_Lock = new object();

		// Token: 0x04002B39 RID: 11065
		private List<AsyncDebugger> m_DebuggerList = new List<AsyncDebugger>();

		// Token: 0x04002B3A RID: 11066
		private AsyncDebugger m_Current;

		// Token: 0x04002B3B RID: 11067
		private ManualResetEvent m_StopEvent = new ManualResetEvent(false);

		// Token: 0x04002B3C RID: 11068
		private bool m_Started;

		// Token: 0x04002B3D RID: 11069
		private int m_Port;
	}
}
