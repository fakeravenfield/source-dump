using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution.Scopes
{
	// Token: 0x020008B7 RID: 2231
	internal class BuildTimeScopeBlock
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x0002609F File Offset: 0x0002429F
		// (set) Token: 0x0600383F RID: 14399 RVA: 0x000260A7 File Offset: 0x000242A7
		internal BuildTimeScopeBlock Parent { get; private set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000260B0 File Offset: 0x000242B0
		// (set) Token: 0x06003841 RID: 14401 RVA: 0x000260B8 File Offset: 0x000242B8
		internal List<BuildTimeScopeBlock> ChildNodes { get; private set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000260C1 File Offset: 0x000242C1
		// (set) Token: 0x06003843 RID: 14403 RVA: 0x000260C9 File Offset: 0x000242C9
		internal RuntimeScopeBlock ScopeBlock { get; private set; }

		// Token: 0x06003844 RID: 14404 RVA: 0x00125410 File Offset: 0x00123610
		internal void Rename(string name)
		{
			SymbolRef value = this.m_DefinedNames[name];
			this.m_DefinedNames.Remove(name);
			this.m_DefinedNames.Add(string.Format("@{0}_{1}", name, Guid.NewGuid().ToString("N")), value);
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000260D2 File Offset: 0x000242D2
		internal BuildTimeScopeBlock(BuildTimeScopeBlock parent)
		{
			this.Parent = parent;
			this.ChildNodes = new List<BuildTimeScopeBlock>();
			this.ScopeBlock = new RuntimeScopeBlock();
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x00125460 File Offset: 0x00123660
		internal BuildTimeScopeBlock AddChild()
		{
			BuildTimeScopeBlock buildTimeScopeBlock = new BuildTimeScopeBlock(this);
			this.ChildNodes.Add(buildTimeScopeBlock);
			return buildTimeScopeBlock;
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x00026102 File Offset: 0x00024302
		internal SymbolRef Find(string name)
		{
			return this.m_DefinedNames.GetOrDefault(name);
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x00125484 File Offset: 0x00123684
		internal SymbolRef Define(string name)
		{
			SymbolRef symbolRef = SymbolRef.Local(name, -1);
			this.m_DefinedNames.Add(name, symbolRef);
			this.m_LastDefinedName = name;
			return symbolRef;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x001254B0 File Offset: 0x001236B0
		internal int ResolveLRefs(BuildTimeScopeFrame buildTimeScopeFrame)
		{
			int num = -1;
			int to = -1;
			foreach (SymbolRef var in this.m_DefinedNames.Values)
			{
				int num2 = buildTimeScopeFrame.AllocVar(var);
				if (num < 0)
				{
					num = num2;
				}
				to = num2;
			}
			this.ScopeBlock.From = num;
			this.ScopeBlock.ToInclusive = (this.ScopeBlock.To = to);
			if (num < 0)
			{
				this.ScopeBlock.From = buildTimeScopeFrame.GetPosForNextVar();
			}
			foreach (BuildTimeScopeBlock buildTimeScopeBlock in this.ChildNodes)
			{
				this.ScopeBlock.ToInclusive = Math.Max(this.ScopeBlock.ToInclusive, buildTimeScopeBlock.ResolveLRefs(buildTimeScopeFrame));
			}
			if (this.m_LocalLabels != null)
			{
				foreach (LabelStatement labelStatement in this.m_LocalLabels.Values)
				{
					labelStatement.SetScope(this.ScopeBlock);
				}
			}
			return this.ScopeBlock.ToInclusive;
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x00125614 File Offset: 0x00123814
		internal void DefineLabel(LabelStatement label)
		{
			if (this.m_LocalLabels == null)
			{
				this.m_LocalLabels = new Dictionary<string, LabelStatement>();
			}
			if (this.m_LocalLabels.ContainsKey(label.Label))
			{
				throw new SyntaxErrorException(label.NameToken, "label '{0}' already defined on line {1}", new object[]
				{
					label.Label,
					this.m_LocalLabels[label.Label].SourceRef.FromLine
				});
			}
			this.m_LocalLabels.Add(label.Label, label);
			label.SetDefinedVars(this.m_DefinedNames.Count, this.m_LastDefinedName);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x00026110 File Offset: 0x00024310
		internal void RegisterGoto(GotoStatement gotostat)
		{
			if (this.m_PendingGotos == null)
			{
				this.m_PendingGotos = new List<GotoStatement>();
			}
			this.m_PendingGotos.Add(gotostat);
			gotostat.SetDefinedVars(this.m_DefinedNames.Count, this.m_LastDefinedName);
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x001256B4 File Offset: 0x001238B4
		internal void ResolveGotos()
		{
			if (this.m_PendingGotos == null)
			{
				return;
			}
			foreach (GotoStatement gotoStatement in this.m_PendingGotos)
			{
				LabelStatement labelStatement;
				if (this.m_LocalLabels != null && this.m_LocalLabels.TryGetValue(gotoStatement.Label, out labelStatement))
				{
					if (labelStatement.DefinedVarsCount > gotoStatement.DefinedVarsCount)
					{
						throw new SyntaxErrorException(gotoStatement.GotoToken, "<goto {0}> at line {1} jumps into the scope of local '{2}'", new object[]
						{
							gotoStatement.Label,
							gotoStatement.GotoToken.FromLine,
							labelStatement.LastDefinedVarName
						});
					}
					labelStatement.RegisterGoto(gotoStatement);
				}
				else
				{
					if (this.Parent == null)
					{
						throw new SyntaxErrorException(gotoStatement.GotoToken, "no visible label '{0}' for <goto> at line {1}", new object[]
						{
							gotoStatement.Label,
							gotoStatement.GotoToken.FromLine
						});
					}
					this.Parent.RegisterGoto(gotoStatement);
				}
			}
			this.m_PendingGotos.Clear();
		}

		// Token: 0x04002F7D RID: 12157
		private Dictionary<string, SymbolRef> m_DefinedNames = new Dictionary<string, SymbolRef>();

		// Token: 0x04002F7E RID: 12158
		private List<GotoStatement> m_PendingGotos;

		// Token: 0x04002F7F RID: 12159
		private Dictionary<string, LabelStatement> m_LocalLabels;

		// Token: 0x04002F80 RID: 12160
		private string m_LastDefinedName;
	}
}
