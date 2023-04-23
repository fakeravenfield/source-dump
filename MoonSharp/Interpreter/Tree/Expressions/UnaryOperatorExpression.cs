using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000810 RID: 2064
	internal class UnaryOperatorExpression : Expression
	{
		// Token: 0x06003353 RID: 13139 RVA: 0x00023468 File Offset: 0x00021668
		public UnaryOperatorExpression(ScriptLoadingContext lcontext, Expression subExpression, Token unaryOpToken) : base(lcontext)
		{
			this.m_OpText = unaryOpToken.Text;
			this.m_Exp = subExpression;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x00115DAC File Offset: 0x00113FAC
		public override void Compile(ByteCode bc)
		{
			this.m_Exp.Compile(bc);
			string opText = this.m_OpText;
			if (opText != null)
			{
				if (opText == "not")
				{
					bc.Emit_Operator(OpCode.Not);
					return;
				}
				if (opText == "#")
				{
					bc.Emit_Operator(OpCode.Len);
					return;
				}
				if (opText == "-")
				{
					bc.Emit_Operator(OpCode.Neg);
					return;
				}
			}
			throw new InternalErrorException("Unexpected unary operator '{0}'", new object[]
			{
				this.m_OpText
			});
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00115E30 File Offset: 0x00114030
		public override DynValue Eval(ScriptExecutionContext context)
		{
			DynValue dynValue = this.m_Exp.Eval(context).ToScalar();
			string opText = this.m_OpText;
			if (opText != null)
			{
				if (opText == "not")
				{
					return DynValue.NewBoolean(!dynValue.CastToBool());
				}
				if (opText == "#")
				{
					return dynValue.GetLength();
				}
				if (opText == "-")
				{
					double? num = dynValue.CastToNumber();
					if (num != null)
					{
						return DynValue.NewNumber(-num.Value);
					}
					throw new DynamicExpressionException("Attempt to perform arithmetic on non-numbers.");
				}
			}
			throw new DynamicExpressionException("Unexpected unary operator '{0}'", new object[]
			{
				this.m_OpText
			});
		}

		// Token: 0x04002D70 RID: 11632
		private Expression m_Exp;

		// Token: 0x04002D71 RID: 11633
		private string m_OpText;
	}
}
