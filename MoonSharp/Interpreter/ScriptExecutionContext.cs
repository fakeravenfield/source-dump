using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C9 RID: 1993
	public class ScriptExecutionContext : IScriptPrivateResource
	{
		// Token: 0x060031C5 RID: 12741 RVA: 0x00022770 File Offset: 0x00020970
		internal ScriptExecutionContext(Processor p, CallbackFunction callBackFunction, SourceRef sourceRef, bool isDynamic = false)
		{
			this.IsDynamicExecution = isDynamic;
			this.m_Processor = p;
			this.m_Callback = callBackFunction;
			this.CallingLocation = sourceRef;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x00022795 File Offset: 0x00020995
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x0002279D File Offset: 0x0002099D
		public bool IsDynamicExecution { get; private set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000227A6 File Offset: 0x000209A6
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x000227AE File Offset: 0x000209AE
		public SourceRef CallingLocation { get; private set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000227B7 File Offset: 0x000209B7
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x000227CE File Offset: 0x000209CE
		public object AdditionalData
		{
			get
			{
				if (this.m_Callback == null)
				{
					return null;
				}
				return this.m_Callback.AdditionalData;
			}
			set
			{
				if (this.m_Callback == null)
				{
					throw new InvalidOperationException("Cannot set additional data on a context which has no callback");
				}
				this.m_Callback.AdditionalData = value;
			}
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000227EF File Offset: 0x000209EF
		public Table GetMetatable(DynValue value)
		{
			return this.m_Processor.GetMetatable(value);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000227FD File Offset: 0x000209FD
		public DynValue GetMetamethod(DynValue value, string metamethod)
		{
			return this.m_Processor.GetMetamethod(value, metamethod);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x0010F814 File Offset: 0x0010DA14
		public DynValue GetMetamethodTailCall(DynValue value, string metamethod, params DynValue[] args)
		{
			DynValue metamethod2 = this.GetMetamethod(value, metamethod);
			if (metamethod2 == null)
			{
				return null;
			}
			return DynValue.NewTailCallReq(metamethod2, args);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x0002280C File Offset: 0x00020A0C
		public DynValue GetBinaryMetamethod(DynValue op1, DynValue op2, string eventName)
		{
			return this.m_Processor.GetBinaryMetamethod(op1, op2, eventName);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x0002281C File Offset: 0x00020A1C
		public Script GetScript()
		{
			return this.m_Processor.GetScript();
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00022829 File Offset: 0x00020A29
		public Coroutine GetCallingCoroutine()
		{
			return this.m_Processor.AssociatedCoroutine;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x0010F838 File Offset: 0x0010DA38
		public DynValue EmulateClassicCall(CallbackArguments args, string functionName, Func<LuaState, int> callback)
		{
			LuaState luaState = new LuaState(this, args, functionName);
			int retvals = callback(luaState);
			return luaState.GetReturnValue(retvals);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x0010F860 File Offset: 0x0010DA60
		public DynValue Call(DynValue func, params DynValue[] args)
		{
			if (func.Type == DataType.Function)
			{
				return this.GetScript().Call(func, args);
			}
			if (func.Type == DataType.ClrFunction)
			{
				for (;;)
				{
					DynValue dynValue = func.Callback.Invoke(this, args, false);
					if (dynValue.Type == DataType.YieldRequest)
					{
						break;
					}
					if (dynValue.Type != DataType.TailCallRequest)
					{
						return dynValue;
					}
					TailCallData tailCallData = dynValue.TailCallData;
					if (tailCallData.Continuation != null || tailCallData.ErrorHandler != null)
					{
						goto IL_61;
					}
					args = tailCallData.Args;
					func = tailCallData.Function;
				}
				throw ScriptRuntimeException.CannotYield();
				IL_61:
				throw new ScriptRuntimeException("the function passed cannot be called directly. wrap in a script function instead.");
			}
			int i = 10;
			while (i > 0)
			{
				DynValue metamethod = this.GetMetamethod(func, "__call");
				if (metamethod == null && metamethod.IsNil())
				{
					throw ScriptRuntimeException.AttemptToCallNonFunc(func.Type, null);
				}
				func = metamethod;
				if (func.Type == DataType.Function || func.Type == DataType.ClrFunction)
				{
					return this.Call(func, args);
				}
			}
			throw ScriptRuntimeException.LoopInCall();
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00022836 File Offset: 0x00020A36
		public DynValue EvaluateSymbol(SymbolRef symref)
		{
			if (symref == null)
			{
				return DynValue.Nil;
			}
			return this.m_Processor.GetGenericSymbol(symref);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x0002284D File Offset: 0x00020A4D
		public DynValue EvaluateSymbolByName(string symbol)
		{
			return this.EvaluateSymbol(this.FindSymbolByName(symbol));
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x0002285C File Offset: 0x00020A5C
		public SymbolRef FindSymbolByName(string symbol)
		{
			return this.m_Processor.FindSymbolByName(symbol);
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x0010F940 File Offset: 0x0010DB40
		public Table CurrentGlobalEnv
		{
			get
			{
				DynValue dynValue = this.EvaluateSymbolByName("_ENV");
				if (dynValue == null || dynValue.Type != DataType.Table)
				{
					return null;
				}
				return dynValue.Table;
			}
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0002286A File Offset: 0x00020A6A
		public void PerformMessageDecorationBeforeUnwind(DynValue messageHandler, ScriptRuntimeException exception)
		{
			if (messageHandler != null)
			{
				exception.DecoratedMessage = this.m_Processor.PerformMessageDecorationBeforeUnwind(messageHandler, exception.Message, this.CallingLocation);
				return;
			}
			exception.DecoratedMessage = exception.Message;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x0002289A File Offset: 0x00020A9A
		public Script OwnerScript
		{
			get
			{
				return this.GetScript();
			}
		}

		// Token: 0x04002C37 RID: 11319
		private Processor m_Processor;

		// Token: 0x04002C38 RID: 11320
		private CallbackFunction m_Callback;
	}
}
