using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007A8 RID: 1960
	public class Coroutine : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x0002143D File Offset: 0x0001F63D
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x00021445 File Offset: 0x0001F645
		public Coroutine.CoroutineType Type { get; private set; }

		// Token: 0x06003065 RID: 12389 RVA: 0x0002144E File Offset: 0x0001F64E
		internal Coroutine(CallbackFunction function)
		{
			this.Type = Coroutine.CoroutineType.ClrCallback;
			this.m_ClrCallback = function;
			this.OwnerScript = null;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0002146B File Offset: 0x0001F66B
		internal Coroutine(Processor proc)
		{
			this.Type = Coroutine.CoroutineType.Coroutine;
			this.m_Processor = proc;
			this.m_Processor.AssociatedCoroutine = this;
			this.OwnerScript = proc.GetScript();
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x00021499 File Offset: 0x0001F699
		internal void MarkClrCallbackAsDead()
		{
			if (this.Type != Coroutine.CoroutineType.ClrCallback)
			{
				throw new InvalidOperationException("State must be CoroutineType.ClrCallback");
			}
			this.Type = Coroutine.CoroutineType.ClrCallbackDead;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000214B6 File Offset: 0x0001F6B6
		public IEnumerable<DynValue> AsTypedEnumerable()
		{
			if (this.Type != Coroutine.CoroutineType.Coroutine)
			{
				throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
			}
			while (this.State == CoroutineState.NotStarted || this.State == CoroutineState.Suspended || this.State == CoroutineState.ForceSuspended)
			{
				yield return this.Resume();
			}
			yield break;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000214C6 File Offset: 0x0001F6C6
		public IEnumerable<object> AsEnumerable()
		{
			foreach (DynValue dynValue in this.AsTypedEnumerable())
			{
				yield return dynValue.ToScalar().ToObject();
			}
			IEnumerator<DynValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000214D6 File Offset: 0x0001F6D6
		public IEnumerable<T> AsEnumerable<T>()
		{
			foreach (DynValue dynValue in this.AsTypedEnumerable())
			{
				yield return dynValue.ToScalar().ToObject<T>();
			}
			IEnumerator<DynValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000214E6 File Offset: 0x0001F6E6
		public IEnumerator AsUnityCoroutine()
		{
			foreach (DynValue dynValue in this.AsTypedEnumerable())
			{
				yield return null;
			}
			IEnumerator<DynValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000214F5 File Offset: 0x0001F6F5
		public DynValue Resume(params DynValue[] args)
		{
			this.CheckScriptOwnership(args);
			if (this.Type == Coroutine.CoroutineType.Coroutine)
			{
				return this.m_Processor.Coroutine_Resume(args);
			}
			throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0010DC50 File Offset: 0x0010BE50
		public DynValue Resume(ScriptExecutionContext context, params DynValue[] args)
		{
			this.CheckScriptOwnership(context);
			this.CheckScriptOwnership(args);
			if (this.Type == Coroutine.CoroutineType.Coroutine)
			{
				return this.m_Processor.Coroutine_Resume(args);
			}
			if (this.Type == Coroutine.CoroutineType.ClrCallback)
			{
				DynValue result = this.m_ClrCallback.Invoke(context, args, false);
				this.MarkClrCallbackAsDead();
				return result;
			}
			throw ScriptRuntimeException.CannotResumeNotSuspended(CoroutineState.Dead);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0002151D File Offset: 0x0001F71D
		public DynValue Resume()
		{
			return this.Resume(new DynValue[0]);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0002152B File Offset: 0x0001F72B
		public DynValue Resume(ScriptExecutionContext context)
		{
			return this.Resume(context, new DynValue[0]);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0010DCA4 File Offset: 0x0010BEA4
		public DynValue Resume(params object[] args)
		{
			if (this.Type != Coroutine.CoroutineType.Coroutine)
			{
				throw new InvalidOperationException("Only non-CLR coroutines can be resumed with this overload of the Resume method. Use the overload accepting a ScriptExecutionContext instead");
			}
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(this.OwnerScript, args[i]);
			}
			return this.Resume(array);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0010DCF4 File Offset: 0x0010BEF4
		public DynValue Resume(ScriptExecutionContext context, params object[] args)
		{
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(context.GetScript(), args[i]);
			}
			return this.Resume(context, array);
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x0002153A File Offset: 0x0001F73A
		public CoroutineState State
		{
			get
			{
				if (this.Type == Coroutine.CoroutineType.ClrCallback)
				{
					return CoroutineState.NotStarted;
				}
				if (this.Type == Coroutine.CoroutineType.ClrCallbackDead)
				{
					return CoroutineState.Dead;
				}
				return this.m_Processor.State;
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0002155D File Offset: 0x0001F75D
		public WatchItem[] GetStackTrace(int skip, SourceRef entrySourceRef = null)
		{
			if (this.State != CoroutineState.Running)
			{
				entrySourceRef = this.m_Processor.GetCoroutineSuspendedLocation();
			}
			return this.m_Processor.Debugger_GetCallStack(entrySourceRef).Skip(skip).ToArray<WatchItem>();
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x0002158C File Offset: 0x0001F78C
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x00021594 File Offset: 0x0001F794
		public Script OwnerScript { get; private set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0002159D File Offset: 0x0001F79D
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x000215AA File Offset: 0x0001F7AA
		public long AutoYieldCounter
		{
			get
			{
				return this.m_Processor.AutoYieldCounter;
			}
			set
			{
				this.m_Processor.AutoYieldCounter = value;
			}
		}

		// Token: 0x04002BBD RID: 11197
		private CallbackFunction m_ClrCallback;

		// Token: 0x04002BBE RID: 11198
		private Processor m_Processor;

		// Token: 0x020007A9 RID: 1961
		public enum CoroutineType
		{
			// Token: 0x04002BC1 RID: 11201
			Coroutine,
			// Token: 0x04002BC2 RID: 11202
			ClrCallback,
			// Token: 0x04002BC3 RID: 11203
			ClrCallbackDead
		}
	}
}
