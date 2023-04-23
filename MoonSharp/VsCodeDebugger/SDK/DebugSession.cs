using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000793 RID: 1939
	public abstract class DebugSession : ProtocolServer
	{
		// Token: 0x06002F8D RID: 12173 RVA: 0x00020BF7 File Offset: 0x0001EDF7
		public DebugSession(bool debuggerLinesStartAt1, bool debuggerPathsAreURI = false)
		{
			this._debuggerLinesStartAt1 = debuggerLinesStartAt1;
			this._debuggerPathsAreURI = debuggerPathsAreURI;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x00020C1B File Offset: 0x0001EE1B
		public void SendResponse(Response response, ResponseBody body = null)
		{
			if (body != null)
			{
				response.SetBody(body);
			}
			base.SendMessage(response);
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0010B750 File Offset: 0x00109950
		public void SendErrorResponse(Response response, int id, string format, object arguments = null, bool user = true, bool telemetry = false)
		{
			Message message = new Message(id, format, arguments, user, telemetry);
			string msg = Utilities.ExpandVariables(message.format, message.variables, true);
			response.SetErrorBody(msg, new ErrorResponseBody(message));
			base.SendMessage(response);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0010B794 File Offset: 0x00109994
		protected override void DispatchRequest(string command, Table args, Response response)
		{
			if (args == null)
			{
				args = new Table(null);
			}
			try
			{
				if (command != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(command);
					if (num <= 1531507585U)
					{
						if (num <= 466561496U)
						{
							if (num <= 215793505U)
							{
								if (num != 197563686U)
								{
									if (num == 215793505U)
									{
										if (command == "setFunctionBreakpoints")
										{
											this.SetFunctionBreakpoints(response, args);
											goto IL_41E;
										}
									}
								}
								else if (command == "threads")
								{
									this.Threads(response, args);
									goto IL_41E;
								}
							}
							else if (num != 362146064U)
							{
								if (num == 466561496U)
								{
									if (command == "source")
									{
										this.Source(response, args);
										goto IL_41E;
									}
								}
							}
							else if (command == "setExceptionBreakpoints")
							{
								this.SetExceptionBreakpoints(response, args);
								goto IL_41E;
							}
						}
						else if (num <= 699097794U)
						{
							if (num != 515025409U)
							{
								if (num == 699097794U)
								{
									if (command == "variables")
									{
										this.Variables(response, args);
										goto IL_41E;
									}
								}
							}
							else if (command == "stepOut")
							{
								this.StepOut(response, args);
								goto IL_41E;
							}
						}
						else if (num != 1117107268U)
						{
							if (num != 1172766443U)
							{
								if (num == 1531507585U)
								{
									if (command == "setBreakpoints")
									{
										this.SetBreakpoints(response, args);
										goto IL_41E;
									}
								}
							}
							else if (command == "disconnect")
							{
								this.Disconnect(response, args);
								goto IL_41E;
							}
						}
						else if (command == "attach")
						{
							this.Attach(response, args);
							goto IL_41E;
						}
					}
					else if (num <= 2977070660U)
					{
						if (num <= 1887753101U)
						{
							if (num != 1555467752U)
							{
								if (num == 1887753101U)
								{
									if (command == "pause")
									{
										this.Pause(response, args);
										goto IL_41E;
									}
								}
							}
							else if (command == "next")
							{
								this.Next(response, args);
								goto IL_41E;
							}
						}
						else if (num != 2900637194U)
						{
							if (num == 2977070660U)
							{
								if (command == "continue")
								{
									this.Continue(response, args);
									goto IL_41E;
								}
							}
						}
						else if (command == "evaluate")
						{
							this.Evaluate(response, args);
							goto IL_41E;
						}
					}
					else if (num <= 3751489852U)
					{
						if (num != 3465713227U)
						{
							if (num == 3751489852U)
							{
								if (command == "stackTrace")
								{
									this.StackTrace(response, args);
									goto IL_41E;
								}
							}
						}
						else if (command == "initialize")
						{
							if (args["linesStartAt1"] != null)
							{
								this._clientLinesStartAt1 = args.Get("linesStartAt1").ToObject<bool>();
							}
							string text = args.Get("pathFormat").ToObject<string>();
							if (text != null)
							{
								if (text != null)
								{
									if (text == "uri")
									{
										this._clientPathsAreURI = true;
										goto IL_33C;
									}
									if (text == "path")
									{
										this._clientPathsAreURI = false;
										goto IL_33C;
									}
								}
								this.SendErrorResponse(response, 1015, "initialize: bad value '{_format}' for pathFormat", new
								{
									_format = text
								}, true, false);
								return;
							}
							IL_33C:
							this.Initialize(response, args);
							goto IL_41E;
						}
					}
					else if (num != 4055382118U)
					{
						if (num != 4060112084U)
						{
							if (num == 4120737864U)
							{
								if (command == "scopes")
								{
									this.Scopes(response, args);
									goto IL_41E;
								}
							}
						}
						else if (command == "stepIn")
						{
							this.StepIn(response, args);
							goto IL_41E;
						}
					}
					else if (command == "launch")
					{
						this.Launch(response, args);
						goto IL_41E;
					}
				}
				this.SendErrorResponse(response, 1014, "unrecognized request: {_request}", new
				{
					_request = command
				}, true, false);
				IL_41E:;
			}
			catch (Exception ex)
			{
				this.SendErrorResponse(response, 1104, "error while processing request '{_request}' (exception: {_exception})", new
				{
					_request = command,
					_exception = ex.Message
				}, true, false);
			}
			if (command == "disconnect")
			{
				base.Stop();
			}
		}

		// Token: 0x06002F91 RID: 12177
		public abstract void Initialize(Response response, Table args);

		// Token: 0x06002F92 RID: 12178
		public abstract void Launch(Response response, Table arguments);

		// Token: 0x06002F93 RID: 12179
		public abstract void Attach(Response response, Table arguments);

		// Token: 0x06002F94 RID: 12180
		public abstract void Disconnect(Response response, Table arguments);

		// Token: 0x06002F95 RID: 12181 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void SetFunctionBreakpoints(Response response, Table arguments)
		{
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void SetExceptionBreakpoints(Response response, Table arguments)
		{
		}

		// Token: 0x06002F97 RID: 12183
		public abstract void SetBreakpoints(Response response, Table arguments);

		// Token: 0x06002F98 RID: 12184
		public abstract void Continue(Response response, Table arguments);

		// Token: 0x06002F99 RID: 12185
		public abstract void Next(Response response, Table arguments);

		// Token: 0x06002F9A RID: 12186
		public abstract void StepIn(Response response, Table arguments);

		// Token: 0x06002F9B RID: 12187
		public abstract void StepOut(Response response, Table arguments);

		// Token: 0x06002F9C RID: 12188
		public abstract void Pause(Response response, Table arguments);

		// Token: 0x06002F9D RID: 12189
		public abstract void StackTrace(Response response, Table arguments);

		// Token: 0x06002F9E RID: 12190
		public abstract void Scopes(Response response, Table arguments);

		// Token: 0x06002F9F RID: 12191
		public abstract void Variables(Response response, Table arguments);

		// Token: 0x06002FA0 RID: 12192 RVA: 0x00020C2E File Offset: 0x0001EE2E
		public virtual void Source(Response response, Table arguments)
		{
			this.SendErrorResponse(response, 1020, "Source not supported", null, true, false);
		}

		// Token: 0x06002FA1 RID: 12193
		public abstract void Threads(Response response, Table arguments);

		// Token: 0x06002FA2 RID: 12194
		public abstract void Evaluate(Response response, Table arguments);

		// Token: 0x06002FA3 RID: 12195 RVA: 0x00020C44 File Offset: 0x0001EE44
		protected int ConvertDebuggerLineToClient(int line)
		{
			if (this._debuggerLinesStartAt1)
			{
				if (!this._clientLinesStartAt1)
				{
					return line - 1;
				}
				return line;
			}
			else
			{
				if (!this._clientLinesStartAt1)
				{
					return line;
				}
				return line + 1;
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x00020C69 File Offset: 0x0001EE69
		protected int ConvertClientLineToDebugger(int line)
		{
			if (this._debuggerLinesStartAt1)
			{
				if (!this._clientLinesStartAt1)
				{
					return line + 1;
				}
				return line;
			}
			else
			{
				if (!this._clientLinesStartAt1)
				{
					return line;
				}
				return line - 1;
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0010BC14 File Offset: 0x00109E14
		protected string ConvertDebuggerPathToClient(string path)
		{
			if (!this._debuggerPathsAreURI)
			{
				if (this._clientPathsAreURI)
				{
					try
					{
						return new Uri(path).AbsoluteUri;
					}
					catch
					{
						return null;
					}
					return path;
				}
				return path;
			}
			if (this._clientPathsAreURI)
			{
				return path;
			}
			return new Uri(path).LocalPath;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x0010BC70 File Offset: 0x00109E70
		protected string ConvertClientPathToDebugger(string clientPath)
		{
			if (clientPath == null)
			{
				return null;
			}
			if (this._debuggerPathsAreURI)
			{
				if (this._clientPathsAreURI)
				{
					return clientPath;
				}
				return new Uri(clientPath).AbsoluteUri;
			}
			else
			{
				if (!this._clientPathsAreURI)
				{
					return clientPath;
				}
				if (Uri.IsWellFormedUriString(clientPath, UriKind.Absolute))
				{
					return new Uri(clientPath).LocalPath;
				}
				Console.Error.WriteLine("path not well formed: '{0}'", clientPath);
				return null;
			}
		}

		// Token: 0x04002B75 RID: 11125
		private bool _debuggerLinesStartAt1;

		// Token: 0x04002B76 RID: 11126
		private bool _debuggerPathsAreURI;

		// Token: 0x04002B77 RID: 11127
		private bool _clientLinesStartAt1 = true;

		// Token: 0x04002B78 RID: 11128
		private bool _clientPathsAreURI = true;
	}
}
