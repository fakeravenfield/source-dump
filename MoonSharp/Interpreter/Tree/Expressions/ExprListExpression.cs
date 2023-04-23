using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000808 RID: 2056
	internal class ExprListExpression : Expression
	{
		// Token: 0x06003325 RID: 13093 RVA: 0x000232F8 File Offset: 0x000214F8
		public ExprListExpression(List<Expression> exps, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.expressions = exps;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x00023308 File Offset: 0x00021508
		public Expression[] GetExpressions()
		{
			return this.expressions.ToArray();
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x00114F14 File Offset: 0x00113114
		public override void Compile(ByteCode bc)
		{
			foreach (Expression expression in this.expressions)
			{
				expression.Compile(bc);
			}
			if (this.expressions.Count > 1)
			{
				bc.Emit_MkTuple(this.expressions.Count);
			}
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x00023315 File Offset: 0x00021515
		public override DynValue Eval(ScriptExecutionContext context)
		{
			if (this.expressions.Count >= 1)
			{
				return this.expressions[0].Eval(context);
			}
			return DynValue.Void;
		}

		// Token: 0x04002D55 RID: 11605
		private List<Expression> expressions;
	}
}
