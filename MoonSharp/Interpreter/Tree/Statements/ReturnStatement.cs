using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007FE RID: 2046
	internal class ReturnStatement : Statement
	{
		// Token: 0x06003300 RID: 13056 RVA: 0x000231D7 File Offset: 0x000213D7
		public ReturnStatement(ScriptLoadingContext lcontext, Expression e, SourceRef sref) : base(lcontext)
		{
			this.m_Expression = e;
			this.m_Ref = sref;
			lcontext.Source.Refs.Add(sref);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x00114088 File Offset: 0x00112288
		public ReturnStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			Token token = lcontext.Lexer.Current;
			lcontext.Lexer.Next();
			Token token2 = lcontext.Lexer.Current;
			if (token2.IsEndOfBlock() || token2.Type == TokenType.SemiColon)
			{
				this.m_Expression = null;
				this.m_Ref = token.GetSourceRef(true);
			}
			else
			{
				this.m_Expression = new ExprListExpression(Expression.ExprList(lcontext), lcontext);
				this.m_Ref = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			}
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x00114128 File Offset: 0x00112328
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Ref))
			{
				if (this.m_Expression != null)
				{
					this.m_Expression.Compile(bc);
					bc.Emit_Ret(1);
				}
				else
				{
					bc.Emit_Ret(0);
				}
			}
		}

		// Token: 0x04002D26 RID: 11558
		private Expression m_Expression;

		// Token: 0x04002D27 RID: 11559
		private SourceRef m_Ref;
	}
}
