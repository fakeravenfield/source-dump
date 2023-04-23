using System;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution.Scopes
{
	// Token: 0x020008B8 RID: 2232
	internal class BuildTimeScopeFrame
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x00026148 File Offset: 0x00024348
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x00026150 File Offset: 0x00024350
		public bool HasVarArgs { get; private set; }

		// Token: 0x0600384F RID: 14415 RVA: 0x001257D4 File Offset: 0x001239D4
		internal BuildTimeScopeFrame(bool hasVarArgs)
		{
			this.HasVarArgs = hasVarArgs;
			this.m_ScopeTreeHead = (this.m_ScopeTreeRoot = new BuildTimeScopeBlock(null));
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x00026159 File Offset: 0x00024359
		internal void PushBlock()
		{
			this.m_ScopeTreeHead = this.m_ScopeTreeHead.AddChild();
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0002616C File Offset: 0x0002436C
		internal RuntimeScopeBlock PopBlock()
		{
			BuildTimeScopeBlock scopeTreeHead = this.m_ScopeTreeHead;
			this.m_ScopeTreeHead.ResolveGotos();
			this.m_ScopeTreeHead = this.m_ScopeTreeHead.Parent;
			if (this.m_ScopeTreeHead == null)
			{
				throw new InternalErrorException("Can't pop block - stack underflow");
			}
			return scopeTreeHead.ScopeBlock;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000261A8 File Offset: 0x000243A8
		internal RuntimeScopeFrame GetRuntimeFrameData()
		{
			if (this.m_ScopeTreeHead != this.m_ScopeTreeRoot)
			{
				throw new InternalErrorException("Misaligned scope frames/blocks!");
			}
			this.m_ScopeFrame.ToFirstBlock = this.m_ScopeTreeRoot.ScopeBlock.To;
			return this.m_ScopeFrame;
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x00125810 File Offset: 0x00123A10
		internal SymbolRef Find(string name)
		{
			for (BuildTimeScopeBlock buildTimeScopeBlock = this.m_ScopeTreeHead; buildTimeScopeBlock != null; buildTimeScopeBlock = buildTimeScopeBlock.Parent)
			{
				SymbolRef symbolRef = buildTimeScopeBlock.Find(name);
				if (symbolRef != null)
				{
					return symbolRef;
				}
			}
			return null;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000261E4 File Offset: 0x000243E4
		internal SymbolRef DefineLocal(string name)
		{
			return this.m_ScopeTreeHead.Define(name);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000261F2 File Offset: 0x000243F2
		internal SymbolRef TryDefineLocal(string name)
		{
			if (this.m_ScopeTreeHead.Find(name) != null)
			{
				this.m_ScopeTreeHead.Rename(name);
			}
			return this.m_ScopeTreeHead.Define(name);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0002621A File Offset: 0x0002441A
		internal void ResolveLRefs()
		{
			this.m_ScopeTreeRoot.ResolveGotos();
			this.m_ScopeTreeRoot.ResolveLRefs(this);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x00026234 File Offset: 0x00024434
		internal int AllocVar(SymbolRef var)
		{
			var.i_Index = this.m_ScopeFrame.DebugSymbols.Count;
			this.m_ScopeFrame.DebugSymbols.Add(var);
			return var.i_Index;
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x00026263 File Offset: 0x00024463
		internal int GetPosForNextVar()
		{
			return this.m_ScopeFrame.DebugSymbols.Count;
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x00026275 File Offset: 0x00024475
		internal void DefineLabel(LabelStatement label)
		{
			this.m_ScopeTreeHead.DefineLabel(label);
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x00026283 File Offset: 0x00024483
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_ScopeTreeHead.RegisterGoto(gotostat);
		}

		// Token: 0x04002F81 RID: 12161
		private BuildTimeScopeBlock m_ScopeTreeRoot;

		// Token: 0x04002F82 RID: 12162
		private BuildTimeScopeBlock m_ScopeTreeHead;

		// Token: 0x04002F83 RID: 12163
		private RuntimeScopeFrame m_ScopeFrame = new RuntimeScopeFrame();
	}
}
