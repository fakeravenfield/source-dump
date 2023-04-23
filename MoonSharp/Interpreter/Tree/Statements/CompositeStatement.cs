using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007F1 RID: 2033
	internal class CompositeStatement : Statement
	{
		// Token: 0x060032C4 RID: 12996 RVA: 0x00113014 File Offset: 0x00111214
		public CompositeStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			while (!lcontext.Lexer.Current.IsEndOfBlock())
			{
				bool flag;
				Statement item = Statement.CreateStatement(lcontext, out flag);
				this.m_Statements.Add(item);
				if (flag)
				{
					break;
				}
			}
			while (lcontext.Lexer.Current.Type == TokenType.SemiColon)
			{
				lcontext.Lexer.Next();
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x00113080 File Offset: 0x00111280
		public override void Compile(ByteCode bc)
		{
			if (this.m_Statements != null)
			{
				foreach (Statement statement in this.m_Statements)
				{
					statement.Compile(bc);
				}
			}
		}

		// Token: 0x04002CEF RID: 11503
		private List<Statement> m_Statements = new List<Statement>();
	}
}
