using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007F9 RID: 2041
	internal class GotoStatement : Statement
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000230B1 File Offset: 0x000212B1
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x000230B9 File Offset: 0x000212B9
		internal SourceRef SourceRef { get; private set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x000230C2 File Offset: 0x000212C2
		// (set) Token: 0x060032DD RID: 13021 RVA: 0x000230CA File Offset: 0x000212CA
		internal Token GotoToken { get; private set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000230D3 File Offset: 0x000212D3
		// (set) Token: 0x060032DF RID: 13023 RVA: 0x000230DB File Offset: 0x000212DB
		public string Label { get; private set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000230E4 File Offset: 0x000212E4
		// (set) Token: 0x060032E1 RID: 13025 RVA: 0x000230EC File Offset: 0x000212EC
		internal int DefinedVarsCount { get; private set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000230F5 File Offset: 0x000212F5
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x000230FD File Offset: 0x000212FD
		internal string LastDefinedVarName { get; private set; }

		// Token: 0x060032E4 RID: 13028 RVA: 0x00113A34 File Offset: 0x00111C34
		public GotoStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.GotoToken = NodeBase.CheckTokenType(lcontext, TokenType.Goto);
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			this.SourceRef = this.GotoToken.GetSourceRef(token, true);
			this.Label = token.Text;
			lcontext.Scope.RegisterGoto(this);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x00023106 File Offset: 0x00021306
		public override void Compile(ByteCode bc)
		{
			this.m_Jump = bc.Emit_Jump(OpCode.Jump, this.m_LabelAddress, 0);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0002311D File Offset: 0x0002131D
		internal void SetDefinedVars(int definedVarsCount, string lastDefinedVarsName)
		{
			this.DefinedVarsCount = definedVarsCount;
			this.LastDefinedVarName = lastDefinedVarsName;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0002312D File Offset: 0x0002132D
		internal void SetAddress(int labelAddress)
		{
			this.m_LabelAddress = labelAddress;
			if (this.m_Jump != null)
			{
				this.m_Jump.NumVal = labelAddress;
			}
		}

		// Token: 0x04002D10 RID: 11536
		private Instruction m_Jump;

		// Token: 0x04002D11 RID: 11537
		private int m_LabelAddress = -1;
	}
}
