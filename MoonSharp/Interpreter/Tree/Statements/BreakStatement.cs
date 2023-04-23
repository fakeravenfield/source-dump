using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007EF RID: 2031
	internal class BreakStatement : Statement
	{
		// Token: 0x060032BF RID: 12991 RVA: 0x00022FCC File Offset: 0x000211CC
		public BreakStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_Ref = NodeBase.CheckTokenType(lcontext, TokenType.Break).GetSourceRef(true);
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x00112E0C File Offset: 0x0011100C
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Ref))
			{
				if (bc.LoopTracker.Loops.Count == 0)
				{
					throw new SyntaxErrorException(base.Script, this.m_Ref, "<break> at line {0} not inside a loop", new object[]
					{
						this.m_Ref.FromLine
					});
				}
				ILoop loop = bc.LoopTracker.Loops.Peek(0);
				if (loop.IsBoundary())
				{
					throw new SyntaxErrorException(base.Script, this.m_Ref, "<break> at line {0} not inside a loop", new object[]
					{
						this.m_Ref.FromLine
					});
				}
				loop.CompileBreak(bc);
			}
		}

		// Token: 0x04002CEA RID: 11498
		private SourceRef m_Ref;
	}
}
