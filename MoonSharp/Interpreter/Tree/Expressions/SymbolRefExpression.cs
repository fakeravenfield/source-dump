using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x0200080E RID: 2062
	internal class SymbolRefExpression : Expression, IVariable
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x001159D8 File Offset: 0x00113BD8
		public SymbolRefExpression(Token T, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_VarName = T.Text;
			if (T.Type == TokenType.VarArgs)
			{
				this.m_Ref = lcontext.Scope.Find("...");
				if (!lcontext.Scope.CurrentFunctionHasVarArgs())
				{
					throw new SyntaxErrorException(T, "cannot use '...' outside a vararg function");
				}
				if (lcontext.IsDynamicExpression)
				{
					throw new DynamicExpressionException("cannot use '...' in a dynamic expression.");
				}
			}
			else if (!lcontext.IsDynamicExpression)
			{
				this.m_Ref = lcontext.Scope.Find(this.m_VarName);
			}
			lcontext.Lexer.Next();
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x00023409 File Offset: 0x00021609
		public SymbolRefExpression(ScriptLoadingContext lcontext, SymbolRef refr) : base(lcontext)
		{
			this.m_Ref = refr;
			if (lcontext.IsDynamicExpression)
			{
				throw new DynamicExpressionException("Unsupported symbol reference expression detected.");
			}
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0002342C File Offset: 0x0002162C
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Load(this.m_Ref);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0002343B File Offset: 0x0002163B
		public void CompileAssignment(ByteCode bc, int stackofs, int tupleidx)
		{
			bc.Emit_Store(this.m_Ref, stackofs, tupleidx);
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0002344C File Offset: 0x0002164C
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return context.EvaluateSymbolByName(this.m_VarName);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0002345A File Offset: 0x0002165A
		public override SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return context.FindSymbolByName(this.m_VarName);
		}

		// Token: 0x04002D6B RID: 11627
		private SymbolRef m_Ref;

		// Token: 0x04002D6C RID: 11628
		private string m_VarName;
	}
}
