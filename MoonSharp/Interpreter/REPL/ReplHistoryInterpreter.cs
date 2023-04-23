using System;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x02000816 RID: 2070
	public class ReplHistoryInterpreter : ReplInterpreter
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x0002355A File Offset: 0x0002175A
		public ReplHistoryInterpreter(Script script, int historySize) : base(script)
		{
			this.m_History = new string[historySize];
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0002357D File Offset: 0x0002177D
		public override DynValue Evaluate(string input)
		{
			this.m_Navi = -1;
			this.m_Last = (this.m_Last + 1) % this.m_History.Length;
			this.m_History[this.m_Last] = input;
			return base.Evaluate(input);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x00116924 File Offset: 0x00114B24
		public string HistoryPrev()
		{
			if (this.m_Navi == -1)
			{
				this.m_Navi = this.m_Last;
			}
			else
			{
				this.m_Navi = (this.m_Navi - 1 + this.m_History.Length) % this.m_History.Length;
			}
			if (this.m_Navi >= 0)
			{
				return this.m_History[this.m_Navi];
			}
			return null;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000235B2 File Offset: 0x000217B2
		public string HistoryNext()
		{
			if (this.m_Navi == -1)
			{
				return null;
			}
			this.m_Navi = (this.m_Navi + 1) % this.m_History.Length;
			if (this.m_Navi >= 0)
			{
				return this.m_History[this.m_Navi];
			}
			return null;
		}

		// Token: 0x04002D73 RID: 11635
		private string[] m_History;

		// Token: 0x04002D74 RID: 11636
		private int m_Last = -1;

		// Token: 0x04002D75 RID: 11637
		private int m_Navi = -1;
	}
}
