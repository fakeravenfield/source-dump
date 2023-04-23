using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000807 RID: 2055
	internal class DynamicExprExpression : Expression
	{
		// Token: 0x06003321 RID: 13089 RVA: 0x000232BE File Offset: 0x000214BE
		public DynamicExprExpression(Expression exp, ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Anonymous = true;
			this.m_Exp = exp;
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000232D5 File Offset: 0x000214D5
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.m_Exp.Eval(context);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000232E3 File Offset: 0x000214E3
		public override void Compile(ByteCode bc)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000232EA File Offset: 0x000214EA
		public override SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return this.m_Exp.FindDynamic(context);
		}

		// Token: 0x04002D54 RID: 11604
		private Expression m_Exp;
	}
}
