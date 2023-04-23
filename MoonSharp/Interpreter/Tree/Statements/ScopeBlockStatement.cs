using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007FF RID: 2047
	internal class ScopeBlockStatement : Statement
	{
		// Token: 0x06003303 RID: 13059 RVA: 0x00114184 File Offset: 0x00112384
		public ScopeBlockStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Scope.PushBlock();
			this.m_Do = NodeBase.CheckTokenType(lcontext, TokenType.Do).GetSourceRef(true);
			this.m_Block = new CompositeStatement(lcontext);
			this.m_End = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_Do);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x00114214 File Offset: 0x00112414
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Do))
			{
				bc.Emit_Enter(this.m_StackFrame);
			}
			this.m_Block.Compile(bc);
			using (bc.EnterSource(this.m_End))
			{
				bc.Emit_Leave(this.m_StackFrame);
			}
		}

		// Token: 0x04002D28 RID: 11560
		private Statement m_Block;

		// Token: 0x04002D29 RID: 11561
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04002D2A RID: 11562
		private SourceRef m_Do;

		// Token: 0x04002D2B RID: 11563
		private SourceRef m_End;
	}
}
