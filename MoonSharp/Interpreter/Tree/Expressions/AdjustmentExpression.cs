using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000802 RID: 2050
	internal class AdjustmentExpression : Expression
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x00023231 File Offset: 0x00021431
		public AdjustmentExpression(ScriptLoadingContext lcontext, Expression exp) : base(lcontext)
		{
			this.expression = exp;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x00023241 File Offset: 0x00021441
		public override void Compile(ByteCode bc)
		{
			this.expression.Compile(bc);
			bc.Emit_Scalar();
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x00023256 File Offset: 0x00021456
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.expression.Eval(context).ToScalar();
		}

		// Token: 0x04002D31 RID: 11569
		private Expression expression;
	}
}
