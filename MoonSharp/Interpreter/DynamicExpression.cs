using System;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C8 RID: 1992
	public class DynamicExpression : IScriptPrivateResource
	{
		// Token: 0x060031BC RID: 12732 RVA: 0x000226AD File Offset: 0x000208AD
		internal DynamicExpression(Script S, string strExpr, DynamicExprExpression expr)
		{
			this.ExpressionCode = strExpr;
			this.OwnerScript = S;
			this.m_Exp = expr;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000226CA File Offset: 0x000208CA
		internal DynamicExpression(Script S, string strExpr, DynValue constant)
		{
			this.ExpressionCode = strExpr;
			this.OwnerScript = S;
			this.m_Constant = constant;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000226E7 File Offset: 0x000208E7
		public DynValue Evaluate(ScriptExecutionContext context = null)
		{
			context = (context ?? this.OwnerScript.CreateDynamicExecutionContext(null));
			this.CheckScriptOwnership(context.GetScript());
			if (this.m_Constant != null)
			{
				return this.m_Constant;
			}
			return this.m_Exp.Eval(context);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x00022723 File Offset: 0x00020923
		public SymbolRef FindSymbol(ScriptExecutionContext context)
		{
			this.CheckScriptOwnership(context.GetScript());
			if (this.m_Exp != null)
			{
				return this.m_Exp.FindDynamic(context);
			}
			return null;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x00022747 File Offset: 0x00020947
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x0002274F File Offset: 0x0002094F
		public Script OwnerScript { get; private set; }

		// Token: 0x060031C2 RID: 12738 RVA: 0x00022758 File Offset: 0x00020958
		public bool IsConstant()
		{
			return this.m_Constant != null;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00022763 File Offset: 0x00020963
		public override int GetHashCode()
		{
			return this.ExpressionCode.GetHashCode();
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x0010F7E8 File Offset: 0x0010D9E8
		public override bool Equals(object obj)
		{
			DynamicExpression dynamicExpression = obj as DynamicExpression;
			return dynamicExpression != null && dynamicExpression.ExpressionCode == this.ExpressionCode;
		}

		// Token: 0x04002C33 RID: 11315
		private DynamicExprExpression m_Exp;

		// Token: 0x04002C34 RID: 11316
		private DynValue m_Constant;

		// Token: 0x04002C35 RID: 11317
		public readonly string ExpressionCode;
	}
}
