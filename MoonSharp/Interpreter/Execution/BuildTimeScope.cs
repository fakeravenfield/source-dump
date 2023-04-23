using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Execution.Scopes;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A1 RID: 2209
	internal class BuildTimeScope
	{
		// Token: 0x06003760 RID: 14176 RVA: 0x00025620 File Offset: 0x00023820
		public void PushFunction(IClosureBuilder closureBuilder, bool hasVarArgs)
		{
			this.m_ClosureBuilders.Add(closureBuilder);
			this.m_Frames.Add(new BuildTimeScopeFrame(hasVarArgs));
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x0002563F File Offset: 0x0002383F
		public void PushBlock()
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().PushBlock();
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x00025651 File Offset: 0x00023851
		public RuntimeScopeBlock PopBlock()
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().PopBlock();
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x00120C58 File Offset: 0x0011EE58
		public RuntimeScopeFrame PopFunction()
		{
			BuildTimeScopeFrame buildTimeScopeFrame = this.m_Frames.Last<BuildTimeScopeFrame>();
			buildTimeScopeFrame.ResolveLRefs();
			this.m_Frames.RemoveAt(this.m_Frames.Count - 1);
			this.m_ClosureBuilders.RemoveAt(this.m_ClosureBuilders.Count - 1);
			return buildTimeScopeFrame.GetRuntimeFrameData();
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x00120CAC File Offset: 0x0011EEAC
		public SymbolRef Find(string name)
		{
			SymbolRef symbolRef = this.m_Frames.Last<BuildTimeScopeFrame>().Find(name);
			if (symbolRef != null)
			{
				return symbolRef;
			}
			for (int i = this.m_Frames.Count - 2; i >= 0; i--)
			{
				SymbolRef symbolRef2 = this.m_Frames[i].Find(name);
				if (symbolRef2 != null)
				{
					symbolRef2 = this.CreateUpValue(this, symbolRef2, i, this.m_Frames.Count - 2);
					if (symbolRef2 != null)
					{
						return symbolRef2;
					}
				}
			}
			return this.CreateGlobalReference(name);
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x00120D24 File Offset: 0x0011EF24
		public SymbolRef CreateGlobalReference(string name)
		{
			if (name == "_ENV")
			{
				throw new InternalErrorException("_ENV passed in CreateGlobalReference");
			}
			SymbolRef envSymbol = this.Find("_ENV");
			return SymbolRef.Global(name, envSymbol);
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x00025663 File Offset: 0x00023863
		public void ForceEnvUpValue()
		{
			this.Find("_ENV");
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x00120D5C File Offset: 0x0011EF5C
		private SymbolRef CreateUpValue(BuildTimeScope buildTimeScope, SymbolRef symb, int closuredFrame, int currentFrame)
		{
			if (closuredFrame == currentFrame)
			{
				return this.m_ClosureBuilders[currentFrame + 1].CreateUpvalue(this, symb);
			}
			SymbolRef symbol = this.CreateUpValue(buildTimeScope, symb, closuredFrame, currentFrame - 1);
			return this.m_ClosureBuilders[currentFrame + 1].CreateUpvalue(this, symbol);
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x00025671 File Offset: 0x00023871
		public SymbolRef DefineLocal(string name)
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().DefineLocal(name);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x00025684 File Offset: 0x00023884
		public SymbolRef TryDefineLocal(string name)
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().TryDefineLocal(name);
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x00025697 File Offset: 0x00023897
		public bool CurrentFunctionHasVarArgs()
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().HasVarArgs;
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000256A9 File Offset: 0x000238A9
		internal void DefineLabel(LabelStatement label)
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().DefineLabel(label);
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000256BC File Offset: 0x000238BC
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().RegisterGoto(gotostat);
		}

		// Token: 0x04002EE7 RID: 12007
		private List<BuildTimeScopeFrame> m_Frames = new List<BuildTimeScopeFrame>();

		// Token: 0x04002EE8 RID: 12008
		private List<IClosureBuilder> m_ClosureBuilders = new List<IClosureBuilder>();
	}
}
