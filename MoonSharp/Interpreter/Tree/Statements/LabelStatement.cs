using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007FC RID: 2044
	internal class LabelStatement : Statement
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x0002314A File Offset: 0x0002134A
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x00023152 File Offset: 0x00021352
		public string Label { get; private set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x0002315B File Offset: 0x0002135B
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x00023163 File Offset: 0x00021363
		public int Address { get; private set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x0002316C File Offset: 0x0002136C
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x00023174 File Offset: 0x00021374
		public SourceRef SourceRef { get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x0002317D File Offset: 0x0002137D
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x00023185 File Offset: 0x00021385
		public Token NameToken { get; private set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x0002318E File Offset: 0x0002138E
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x00023196 File Offset: 0x00021396
		internal int DefinedVarsCount { get; private set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x0002319F File Offset: 0x0002139F
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x000231A7 File Offset: 0x000213A7
		internal string LastDefinedVarName { get; private set; }

		// Token: 0x060032F9 RID: 13049 RVA: 0x00113E04 File Offset: 0x00112004
		public LabelStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			NodeBase.CheckTokenType(lcontext, TokenType.DoubleColon);
			this.NameToken = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			NodeBase.CheckTokenType(lcontext, TokenType.DoubleColon);
			this.SourceRef = this.NameToken.GetSourceRef(true);
			this.Label = this.NameToken.Text;
			lcontext.Scope.DefineLabel(this);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000231B0 File Offset: 0x000213B0
		internal void SetDefinedVars(int definedVarsCount, string lastDefinedVarsName)
		{
			this.DefinedVarsCount = definedVarsCount;
			this.LastDefinedVarName = lastDefinedVarsName;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000231C0 File Offset: 0x000213C0
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_Gotos.Add(gotostat);
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x00113E74 File Offset: 0x00112074
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Clean(this.m_StackFrame);
			this.Address = bc.GetJumpPointForLastInstruction();
			foreach (GotoStatement gotoStatement in this.m_Gotos)
			{
				gotoStatement.SetAddress(this.Address);
			}
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000231CE File Offset: 0x000213CE
		internal void SetScope(RuntimeScopeBlock runtimeScopeBlock)
		{
			this.m_StackFrame = runtimeScopeBlock;
		}

		// Token: 0x04002D1F RID: 11551
		private List<GotoStatement> m_Gotos = new List<GotoStatement>();

		// Token: 0x04002D20 RID: 11552
		private RuntimeScopeBlock m_StackFrame;
	}
}
