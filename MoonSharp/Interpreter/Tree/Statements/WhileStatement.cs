using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000800 RID: 2048
	internal class WhileStatement : Statement
	{
		// Token: 0x06003305 RID: 13061 RVA: 0x00114298 File Offset: 0x00112498
		public WhileStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.While);
			this.m_Condition = Expression.Expr(lcontext);
			this.m_Start = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			lcontext.Scope.PushBlock();
			NodeBase.CheckTokenType(lcontext, TokenType.Do);
			this.m_Block = new CompositeStatement(lcontext);
			this.m_End = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_Start);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x00114348 File Offset: 0x00112548
		public override void Compile(ByteCode bc)
		{
			Loop loop = new Loop
			{
				Scope = this.m_StackFrame
			};
			bc.LoopTracker.Loops.Push(loop);
			bc.PushSourceRef(this.m_Start);
			int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
			this.m_Condition.Compile(bc);
			Instruction instruction = bc.Emit_Jump(OpCode.Jf, -1, 0);
			bc.Emit_Enter(this.m_StackFrame);
			this.m_Block.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_End);
			bc.Emit_Leave(this.m_StackFrame);
			bc.Emit_Jump(OpCode.Jump, jumpPointForNextInstruction, 0);
			bc.LoopTracker.Loops.Pop();
			int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
			foreach (Instruction instruction2 in loop.BreakJumps)
			{
				instruction2.NumVal = jumpPointForNextInstruction2;
			}
			instruction.NumVal = jumpPointForNextInstruction2;
			bc.PopSourceRef();
		}

		// Token: 0x04002D2C RID: 11564
		private Expression m_Condition;

		// Token: 0x04002D2D RID: 11565
		private Statement m_Block;

		// Token: 0x04002D2E RID: 11566
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04002D2F RID: 11567
		private SourceRef m_Start;

		// Token: 0x04002D30 RID: 11568
		private SourceRef m_End;
	}
}
