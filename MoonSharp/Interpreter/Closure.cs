using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007A6 RID: 1958
	public class Closure : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x0002134B File Offset: 0x0001F54B
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x00021353 File Offset: 0x0001F553
		public int EntryPointByteCodeLocation { get; private set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x0002135C File Offset: 0x0001F55C
		// (set) Token: 0x06003053 RID: 12371 RVA: 0x00021364 File Offset: 0x0001F564
		public Script OwnerScript { get; private set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0002136D File Offset: 0x0001F56D
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x00021375 File Offset: 0x0001F575
		internal ClosureContext ClosureContext { get; private set; }

		// Token: 0x06003056 RID: 12374 RVA: 0x0002137E File Offset: 0x0001F57E
		internal Closure(Script script, int idx, SymbolRef[] symbols, IEnumerable<DynValue> resolvedLocals)
		{
			this.OwnerScript = script;
			this.EntryPointByteCodeLocation = idx;
			if (symbols.Length != 0)
			{
				this.ClosureContext = new ClosureContext(symbols, resolvedLocals);
				return;
			}
			this.ClosureContext = Closure.emptyClosure;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000213B2 File Offset: 0x0001F5B2
		public DynValue Call()
		{
			return this.OwnerScript.Call(this);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000213C0 File Offset: 0x0001F5C0
		public DynValue Call(params object[] args)
		{
			return this.OwnerScript.Call(this, args);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0010DC00 File Offset: 0x0010BE00
		public DynValue Call(params DynValue[] args)
		{
			return this.OwnerScript.Call(this, args);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000213CF File Offset: 0x0001F5CF
		public ScriptFunctionDelegate GetDelegate()
		{
			return (object[] args) => this.Call(args).ToObject();
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000213DD File Offset: 0x0001F5DD
		public ScriptFunctionDelegate<T> GetDelegate<T>()
		{
			return (object[] args) => this.Call(args).ToObject<T>();
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000213EB File Offset: 0x0001F5EB
		public int GetUpvaluesCount()
		{
			return this.ClosureContext.Count;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000213F8 File Offset: 0x0001F5F8
		public string GetUpvalueName(int idx)
		{
			return this.ClosureContext.Symbols[idx];
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x00021407 File Offset: 0x0001F607
		public DynValue GetUpvalue(int idx)
		{
			return this.ClosureContext[idx];
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x0010DC1C File Offset: 0x0010BE1C
		public Closure.UpvaluesType GetUpvaluesType()
		{
			int upvaluesCount = this.GetUpvaluesCount();
			if (upvaluesCount == 0)
			{
				return Closure.UpvaluesType.None;
			}
			if (upvaluesCount == 1 && this.GetUpvalueName(0) == "_ENV")
			{
				return Closure.UpvaluesType.Environment;
			}
			return Closure.UpvaluesType.Closure;
		}

		// Token: 0x04002BB6 RID: 11190
		private static ClosureContext emptyClosure = new ClosureContext();

		// Token: 0x020007A7 RID: 1959
		public enum UpvaluesType
		{
			// Token: 0x04002BB9 RID: 11193
			None,
			// Token: 0x04002BBA RID: 11194
			Environment,
			// Token: 0x04002BBB RID: 11195
			Closure
		}
	}
}
