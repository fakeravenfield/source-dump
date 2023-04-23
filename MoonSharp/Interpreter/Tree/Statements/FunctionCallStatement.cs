using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007F6 RID: 2038
	internal class FunctionCallStatement : Statement
	{
		// Token: 0x060032CF RID: 13007 RVA: 0x00023028 File Offset: 0x00021228
		public FunctionCallStatement(ScriptLoadingContext lcontext, FunctionCallExpression functionCallExpression) : base(lcontext)
		{
			this.m_FunctionCallExpression = functionCallExpression;
			lcontext.Source.Refs.Add(this.m_FunctionCallExpression.SourceRef);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0011365C File Offset: 0x0011185C
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_FunctionCallExpression.SourceRef))
			{
				this.m_FunctionCallExpression.Compile(bc);
				this.RemoveBreakpointStop(bc.Emit_Pop(1));
			}
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x00023053 File Offset: 0x00021253
		private void RemoveBreakpointStop(Instruction instruction)
		{
			instruction.SourceCodeRef = null;
		}

		// Token: 0x04002D00 RID: 11520
		private FunctionCallExpression m_FunctionCallExpression;
	}
}
