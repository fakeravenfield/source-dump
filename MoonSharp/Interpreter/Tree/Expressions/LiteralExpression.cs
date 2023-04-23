using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x0200080D RID: 2061
	internal class LiteralExpression : Expression
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000233E2 File Offset: 0x000215E2
		public DynValue Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000233EA File Offset: 0x000215EA
		public LiteralExpression(ScriptLoadingContext lcontext, DynValue value) : base(lcontext)
		{
			this.m_Value = value;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x00115908 File Offset: 0x00113B08
		public LiteralExpression(ScriptLoadingContext lcontext, Token t) : base(lcontext)
		{
			TokenType type = t.Type;
			if (type <= TokenType.Nil)
			{
				if (type == TokenType.False)
				{
					this.m_Value = DynValue.False;
					goto IL_94;
				}
				if (type == TokenType.Nil)
				{
					this.m_Value = DynValue.Nil;
					goto IL_94;
				}
			}
			else
			{
				if (type == TokenType.True)
				{
					this.m_Value = DynValue.True;
					goto IL_94;
				}
				if (type - TokenType.String <= 1)
				{
					this.m_Value = DynValue.NewString(t.Text).AsReadOnly();
					goto IL_94;
				}
				if (type - TokenType.Number <= 2)
				{
					this.m_Value = DynValue.NewNumber(t.GetNumberValue()).AsReadOnly();
					goto IL_94;
				}
			}
			throw new InternalErrorException("type mismatch");
			IL_94:
			if (this.m_Value == null)
			{
				throw new SyntaxErrorException(t, "unknown literal format near '{0}'", new object[]
				{
					t.Text
				});
			}
			lcontext.Lexer.Next();
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000233FA File Offset: 0x000215FA
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Literal(this.m_Value);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000233E2 File Offset: 0x000215E2
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.m_Value;
		}

		// Token: 0x04002D6A RID: 11626
		private DynValue m_Value;
	}
}
