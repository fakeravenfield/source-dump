using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007FA RID: 2042
	internal class IfStatement : Statement
	{
		// Token: 0x060032E8 RID: 13032 RVA: 0x00113A90 File Offset: 0x00111C90
		public IfStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			while (lcontext.Lexer.Current.Type != TokenType.Else && lcontext.Lexer.Current.Type != TokenType.End)
			{
				this.m_Ifs.Add(this.CreateIfBlock(lcontext));
			}
			if (lcontext.Lexer.Current.Type == TokenType.Else)
			{
				this.m_Else = this.CreateElseBlock(lcontext);
			}
			this.m_End = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x00113B34 File Offset: 0x00111D34
		private IfStatement.IfBlock CreateIfBlock(ScriptLoadingContext lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.If, TokenType.ElseIf);
			lcontext.Scope.PushBlock();
			IfStatement.IfBlock ifBlock = new IfStatement.IfBlock();
			ifBlock.Exp = Expression.Expr(lcontext);
			ifBlock.Source = token.GetSourceRef(NodeBase.CheckTokenType(lcontext, TokenType.Then), true);
			ifBlock.Block = new CompositeStatement(lcontext);
			ifBlock.StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(ifBlock.Source);
			return ifBlock;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x00113BB4 File Offset: 0x00111DB4
		private IfStatement.IfBlock CreateElseBlock(ScriptLoadingContext lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Else);
			lcontext.Scope.PushBlock();
			IfStatement.IfBlock ifBlock = new IfStatement.IfBlock();
			ifBlock.Block = new CompositeStatement(lcontext);
			ifBlock.StackFrame = lcontext.Scope.PopBlock();
			ifBlock.Source = token.GetSourceRef(true);
			lcontext.Source.Refs.Add(ifBlock.Source);
			return ifBlock;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x00113C1C File Offset: 0x00111E1C
		public override void Compile(ByteCode bc)
		{
			List<Instruction> list = new List<Instruction>();
			Instruction instruction = null;
			foreach (IfStatement.IfBlock ifBlock in this.m_Ifs)
			{
				using (bc.EnterSource(ifBlock.Source))
				{
					if (instruction != null)
					{
						instruction.NumVal = bc.GetJumpPointForNextInstruction();
					}
					ifBlock.Exp.Compile(bc);
					instruction = bc.Emit_Jump(OpCode.Jf, -1, 0);
					bc.Emit_Enter(ifBlock.StackFrame);
					ifBlock.Block.Compile(bc);
				}
				using (bc.EnterSource(this.m_End))
				{
					bc.Emit_Leave(ifBlock.StackFrame);
				}
				list.Add(bc.Emit_Jump(OpCode.Jump, -1, 0));
			}
			instruction.NumVal = bc.GetJumpPointForNextInstruction();
			if (this.m_Else != null)
			{
				using (bc.EnterSource(this.m_Else.Source))
				{
					bc.Emit_Enter(this.m_Else.StackFrame);
					this.m_Else.Block.Compile(bc);
				}
				using (bc.EnterSource(this.m_End))
				{
					bc.Emit_Leave(this.m_Else.StackFrame);
				}
			}
			foreach (Instruction instruction2 in list)
			{
				instruction2.NumVal = bc.GetJumpPointForNextInstruction();
			}
		}

		// Token: 0x04002D12 RID: 11538
		private List<IfStatement.IfBlock> m_Ifs = new List<IfStatement.IfBlock>();

		// Token: 0x04002D13 RID: 11539
		private IfStatement.IfBlock m_Else;

		// Token: 0x04002D14 RID: 11540
		private SourceRef m_End;

		// Token: 0x020007FB RID: 2043
		private class IfBlock
		{
			// Token: 0x04002D15 RID: 11541
			public Expression Exp;

			// Token: 0x04002D16 RID: 11542
			public Statement Block;

			// Token: 0x04002D17 RID: 11543
			public RuntimeScopeBlock StackFrame;

			// Token: 0x04002D18 RID: 11544
			public SourceRef Source;
		}
	}
}
