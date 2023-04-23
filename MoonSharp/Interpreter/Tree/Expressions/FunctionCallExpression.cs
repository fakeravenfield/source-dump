using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000809 RID: 2057
	internal class FunctionCallExpression : Expression
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x0002333D File Offset: 0x0002153D
		// (set) Token: 0x0600332A RID: 13098 RVA: 0x00023345 File Offset: 0x00021545
		internal SourceRef SourceRef { get; private set; }

		// Token: 0x0600332B RID: 13099 RVA: 0x00114F88 File Offset: 0x00113188
		public FunctionCallExpression(ScriptLoadingContext lcontext, Expression function, Token thisCallName) : base(lcontext)
		{
			Token token = thisCallName ?? lcontext.Lexer.Current;
			this.m_Name = ((thisCallName != null) ? thisCallName.Text : null);
			this.m_DebugErr = function.GetFriendlyDebugName();
			this.m_Function = function;
			TokenType type = lcontext.Lexer.Current.Type;
			if (type <= TokenType.Brk_Open_Round)
			{
				if (type != TokenType.Brk_Open_Curly)
				{
					if (type != TokenType.Brk_Open_Round)
					{
						goto IL_183;
					}
					Token originalToken = lcontext.Lexer.Current;
					lcontext.Lexer.Next();
					Token token2 = lcontext.Lexer.Current;
					if (token2.Type == TokenType.Brk_Close_Round)
					{
						this.m_Arguments = new List<Expression>();
						this.SourceRef = token.GetSourceRef(token2, true);
						lcontext.Lexer.Next();
						return;
					}
					this.m_Arguments = Expression.ExprList(lcontext);
					this.SourceRef = token.GetSourceRef(NodeBase.CheckMatch(lcontext, originalToken, TokenType.Brk_Close_Round, ")"), true);
					return;
				}
			}
			else
			{
				if (type - TokenType.String <= 1)
				{
					this.m_Arguments = new List<Expression>();
					Expression item = new LiteralExpression(lcontext, lcontext.Lexer.Current);
					this.m_Arguments.Add(item);
					this.SourceRef = token.GetSourceRef(lcontext.Lexer.Current, true);
					return;
				}
				if (type != TokenType.Brk_Open_Curly_Shared)
				{
					goto IL_183;
				}
			}
			this.m_Arguments = new List<Expression>();
			this.m_Arguments.Add(new TableConstructor(lcontext, lcontext.Lexer.Current.Type == TokenType.Brk_Open_Curly_Shared));
			this.SourceRef = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			return;
			IL_183:
			throw new SyntaxErrorException(lcontext.Lexer.Current, "function arguments expected")
			{
				IsPrematureStreamTermination = (lcontext.Lexer.Current.Type == TokenType.Eof)
			};
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x00115148 File Offset: 0x00113348
		public override void Compile(ByteCode bc)
		{
			this.m_Function.Compile(bc);
			int num = this.m_Arguments.Count;
			if (!string.IsNullOrEmpty(this.m_Name))
			{
				bc.Emit_Copy(0);
				bc.Emit_Index(DynValue.NewString(this.m_Name), true, false);
				bc.Emit_Swap(0, 1);
				num++;
			}
			for (int i = 0; i < this.m_Arguments.Count; i++)
			{
				this.m_Arguments[i].Compile(bc);
			}
			if (!string.IsNullOrEmpty(this.m_Name))
			{
				bc.Emit_ThisCall(num, this.m_DebugErr);
				return;
			}
			bc.Emit_Call(num, this.m_DebugErr);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x0002334E File Offset: 0x0002154E
		public override DynValue Eval(ScriptExecutionContext context)
		{
			throw new DynamicExpressionException("Dynamic Expressions cannot call functions.");
		}

		// Token: 0x04002D56 RID: 11606
		private List<Expression> m_Arguments;

		// Token: 0x04002D57 RID: 11607
		private Expression m_Function;

		// Token: 0x04002D58 RID: 11608
		private string m_Name;

		// Token: 0x04002D59 RID: 11609
		private string m_DebugErr;
	}
}
