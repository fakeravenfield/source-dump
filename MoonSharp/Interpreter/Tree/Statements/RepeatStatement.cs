using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007FD RID: 2045
	internal class RepeatStatement : Statement
	{
		// Token: 0x060032FE RID: 13054 RVA: 0x00113EE4 File Offset: 0x001120E4
		public RepeatStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_Repeat = NodeBase.CheckTokenType(lcontext, TokenType.Repeat).GetSourceRef(true);
			lcontext.Scope.PushBlock();
			this.m_Block = new CompositeStatement(lcontext);
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Until);
			this.m_Condition = Expression.Expr(lcontext);
			this.m_Until = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_Repeat);
			lcontext.Source.Refs.Add(this.m_Until);
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x00113F90 File Offset: 0x00112190
		public override void Compile(ByteCode bc)
		{
			Loop loop = new Loop
			{
				Scope = this.m_StackFrame
			};
			bc.PushSourceRef(this.m_Repeat);
			bc.LoopTracker.Loops.Push(loop);
			int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
			bc.Emit_Enter(this.m_StackFrame);
			this.m_Block.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_Until);
			this.m_Condition.Compile(bc);
			bc.Emit_Leave(this.m_StackFrame);
			bc.Emit_Jump(OpCode.Jf, jumpPointForNextInstruction, 0);
			bc.LoopTracker.Loops.Pop();
			int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
			foreach (Instruction instruction in loop.BreakJumps)
			{
				instruction.NumVal = jumpPointForNextInstruction2;
			}
			bc.PopSourceRef();
		}

		// Token: 0x04002D21 RID: 11553
		private Expression m_Condition;

		// Token: 0x04002D22 RID: 11554
		private Statement m_Block;

		// Token: 0x04002D23 RID: 11555
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04002D24 RID: 11556
		private SourceRef m_Repeat;

		// Token: 0x04002D25 RID: 11557
		private SourceRef m_Until;
	}
}
