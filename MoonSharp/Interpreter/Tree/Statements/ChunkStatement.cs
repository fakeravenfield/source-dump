using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007F0 RID: 2032
	internal class ChunkStatement : Statement, IClosureBuilder
	{
		// Token: 0x060032C1 RID: 12993 RVA: 0x00112ED8 File Offset: 0x001110D8
		public ChunkStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Scope.PushFunction(this, true);
			this.m_Env = lcontext.Scope.DefineLocal("_ENV");
			this.m_VarArgs = lcontext.Scope.DefineLocal("...");
			this.m_Block = new CompositeStatement(lcontext);
			if (lcontext.Lexer.Current.Type != TokenType.Eof)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "<eof> expected near '{0}'", new object[]
				{
					lcontext.Lexer.Current.Text
				});
			}
			this.m_StackFrame = lcontext.Scope.PopFunction();
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x00112F84 File Offset: 0x00111184
		public override void Compile(ByteCode bc)
		{
			Instruction instruction = bc.Emit_Meta("<chunk-root>", OpCodeMetadataType.ChunkEntrypoint, null);
			int jumpPointForLastInstruction = bc.GetJumpPointForLastInstruction();
			bc.Emit_BeginFn(this.m_StackFrame);
			bc.Emit_Args(new SymbolRef[]
			{
				this.m_VarArgs
			});
			bc.Emit_Load(SymbolRef.Upvalue("_ENV", 0));
			bc.Emit_Store(this.m_Env, 0, 0);
			bc.Emit_Pop(1);
			this.m_Block.Compile(bc);
			bc.Emit_Ret(0);
			instruction.NumVal = bc.GetJumpPointForLastInstruction() - jumpPointForLastInstruction;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x00002FD8 File Offset: 0x000011D8
		public SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol)
		{
			return null;
		}

		// Token: 0x04002CEB RID: 11499
		private Statement m_Block;

		// Token: 0x04002CEC RID: 11500
		private RuntimeScopeFrame m_StackFrame;

		// Token: 0x04002CED RID: 11501
		private SymbolRef m_Env;

		// Token: 0x04002CEE RID: 11502
		private SymbolRef m_VarArgs;
	}
}
