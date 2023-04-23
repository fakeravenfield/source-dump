using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x0200080C RID: 2060
	internal class IndexExpression : Expression, IVariable
	{
		// Token: 0x0600333D RID: 13117 RVA: 0x000233B4 File Offset: 0x000215B4
		public IndexExpression(Expression baseExp, Expression indexExp, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_BaseExp = baseExp;
			this.m_IndexExp = indexExp;
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000233CB File Offset: 0x000215CB
		public IndexExpression(Expression baseExp, string name, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_BaseExp = baseExp;
			this.m_Name = name;
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x0011577C File Offset: 0x0011397C
		public override void Compile(ByteCode bc)
		{
			this.m_BaseExp.Compile(bc);
			if (this.m_Name != null)
			{
				bc.Emit_Index(DynValue.NewString(this.m_Name), true, false);
				return;
			}
			if (this.m_IndexExp is LiteralExpression)
			{
				LiteralExpression literalExpression = (LiteralExpression)this.m_IndexExp;
				bc.Emit_Index(literalExpression.Value, false, false);
				return;
			}
			this.m_IndexExp.Compile(bc);
			bc.Emit_Index(null, false, this.m_IndexExp is ExprListExpression);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x00115800 File Offset: 0x00113A00
		public void CompileAssignment(ByteCode bc, int stackofs, int tupleidx)
		{
			this.m_BaseExp.Compile(bc);
			if (this.m_Name != null)
			{
				bc.Emit_IndexSet(stackofs, tupleidx, DynValue.NewString(this.m_Name), true, false);
				return;
			}
			if (this.m_IndexExp is LiteralExpression)
			{
				LiteralExpression literalExpression = (LiteralExpression)this.m_IndexExp;
				bc.Emit_IndexSet(stackofs, tupleidx, literalExpression.Value, false, false);
				return;
			}
			this.m_IndexExp.Compile(bc);
			bc.Emit_IndexSet(stackofs, tupleidx, null, false, this.m_IndexExp is ExprListExpression);
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x00115888 File Offset: 0x00113A88
		public override DynValue Eval(ScriptExecutionContext context)
		{
			DynValue dynValue = this.m_BaseExp.Eval(context).ToScalar();
			DynValue dynValue2 = (this.m_IndexExp != null) ? this.m_IndexExp.Eval(context).ToScalar() : DynValue.NewString(this.m_Name);
			if (dynValue.Type != DataType.Table)
			{
				throw new DynamicExpressionException("Attempt to index non-table.");
			}
			if (dynValue2.IsNilOrNan())
			{
				throw new DynamicExpressionException("Attempt to index with nil or nan key.");
			}
			return dynValue.Table.Get(dynValue2) ?? DynValue.Nil;
		}

		// Token: 0x04002D67 RID: 11623
		private Expression m_BaseExp;

		// Token: 0x04002D68 RID: 11624
		private Expression m_IndexExp;

		// Token: 0x04002D69 RID: 11625
		private string m_Name;
	}
}
