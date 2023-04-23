using System;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x02000817 RID: 2071
	public class ReplInterpreter
	{
		// Token: 0x0600336F RID: 13167 RVA: 0x000235EE File Offset: 0x000217EE
		public ReplInterpreter(Script script)
		{
			this.m_Script = script;
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x00023608 File Offset: 0x00021808
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x00023610 File Offset: 0x00021810
		public bool HandleDynamicExprs { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x00023619 File Offset: 0x00021819
		// (set) Token: 0x06003373 RID: 13171 RVA: 0x00023621 File Offset: 0x00021821
		public bool HandleClassicExprsSyntax { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x0002362A File Offset: 0x0002182A
		public virtual bool HasPendingCommand
		{
			get
			{
				return this.m_CurrentCommand.Length > 0;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x0002363A File Offset: 0x0002183A
		public virtual string CurrentPendingCommand
		{
			get
			{
				return this.m_CurrentCommand;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x00023642 File Offset: 0x00021842
		public virtual string ClassicPrompt
		{
			get
			{
				if (!this.HasPendingCommand)
				{
					return ">";
				}
				return ">>";
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x00116980 File Offset: 0x00114B80
		public virtual DynValue Evaluate(string input)
		{
			bool flag = !this.HasPendingCommand;
			bool flag2 = input == "";
			this.m_CurrentCommand += input;
			if (this.m_CurrentCommand.Length == 0)
			{
				return DynValue.Void;
			}
			this.m_CurrentCommand += "\n";
			DynValue result;
			try
			{
				if (flag && this.HandleClassicExprsSyntax && this.m_CurrentCommand.StartsWith("="))
				{
					this.m_CurrentCommand = "return " + this.m_CurrentCommand.Substring(1);
				}
				DynValue dynValue;
				if (flag && this.HandleDynamicExprs && this.m_CurrentCommand.StartsWith("?"))
				{
					string code = this.m_CurrentCommand.Substring(1);
					dynValue = this.m_Script.CreateDynamicExpression(code).Evaluate(null);
				}
				else
				{
					DynValue function = this.m_Script.LoadString(this.m_CurrentCommand, null, "stdin");
					dynValue = this.m_Script.Call(function);
				}
				this.m_CurrentCommand = "";
				result = dynValue;
			}
			catch (SyntaxErrorException ex)
			{
				if (flag2 || !ex.IsPrematureStreamTermination)
				{
					this.m_CurrentCommand = "";
					ex.Rethrow();
					throw;
				}
				result = null;
			}
			catch (ScriptRuntimeException ex2)
			{
				this.m_CurrentCommand = "";
				ex2.Rethrow();
				throw;
			}
			catch (Exception)
			{
				this.m_CurrentCommand = "";
				throw;
			}
			return result;
		}

		// Token: 0x04002D76 RID: 11638
		private Script m_Script;

		// Token: 0x04002D77 RID: 11639
		private string m_CurrentCommand = string.Empty;
	}
}
