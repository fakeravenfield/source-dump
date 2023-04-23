using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Lua;
using MoonSharp.Interpreter.DataStructs;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.IO;
using UnityEngine;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008B3 RID: 2227
	internal sealed class Processor
	{
		// Token: 0x060037D4 RID: 14292 RVA: 0x00121608 File Offset: 0x0011F808
		public Processor(Script script, Table globalContext, ByteCode byteCode)
		{
			this.m_CoroutinesStack = new List<Processor>();
			this.m_Debug = new Processor.DebugContext();
			this.m_RootChunk = byteCode;
			this.m_GlobalTable = globalContext;
			this.m_Script = script;
			this.m_State = CoroutineState.Main;
			DynValue.NewCoroutine(new Coroutine(this));
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x00121690 File Offset: 0x0011F890
		private Processor(Processor parentProcessor)
		{
			this.m_Debug = parentProcessor.m_Debug;
			this.m_RootChunk = parentProcessor.m_RootChunk;
			this.m_GlobalTable = parentProcessor.m_GlobalTable;
			this.m_Script = parentProcessor.m_Script;
			this.m_Parent = parentProcessor;
			this.m_State = CoroutineState.NotStarted;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x00121718 File Offset: 0x0011F918
		public DynValue Call(DynValue function, DynValue[] args)
		{
			List<Processor> list = (this.m_Parent != null) ? this.m_Parent.m_CoroutinesStack : this.m_CoroutinesStack;
			if (list.Count > 0 && list[list.Count - 1] != this)
			{
				return list[list.Count - 1].Call(function, args);
			}
			this.EnterProcessor();
			DynValue result;
			try
			{
				IDisposable disposable = this.m_Script.PerformanceStats.StartStopwatch(PerformanceCounter.Execution);
				this.m_CanYield = false;
				try
				{
					int instructionPtr = this.PushClrToScriptStackFrame(CallStackItemFlags.CallEntryPoint, function, args);
					result = this.Processing_Loop(instructionPtr);
				}
				finally
				{
					this.m_CanYield = true;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			finally
			{
				this.LeaveProcessor();
			}
			return result;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x001217DC File Offset: 0x0011F9DC
		private int PushClrToScriptStackFrame(CallStackItemFlags flags, DynValue function, DynValue[] args)
		{
			if (function == null)
			{
				function = this.m_ValueStack.Peek(0);
			}
			else
			{
				this.m_ValueStack.Push(function);
			}
			args = this.Internal_AdjustTuple(args);
			for (int i = 0; i < args.Length; i++)
			{
				this.m_ValueStack.Push(args[i]);
			}
			this.m_ValueStack.Push(DynValue.NewNumber((double)args.Length));
			this.m_ExecutionStack.Push(new CallStackItem
			{
				BasePointer = this.m_ValueStack.Count,
				Debug_EntryPoint = function.Function.EntryPointByteCodeLocation,
				ReturnAddress = -1,
				ClosureScope = function.Function.ClosureContext,
				CallingSourceRef = SourceRef.GetClrLocation(),
				Flags = flags
			});
			return function.Function.EntryPointByteCodeLocation;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x001218AC File Offset: 0x0011FAAC
		private void LeaveProcessor()
		{
			this.m_ExecutionNesting--;
			this.m_OwningThreadID = -1;
			bool flag = this.m_Parent == null;
			if (!flag)
			{
				this.m_Parent.m_CoroutinesStack.RemoveAt(this.m_Parent.m_CoroutinesStack.Count - 1);
			}
			if (this.m_ExecutionNesting == 0 && this.m_Debug != null && this.m_Debug.DebuggerEnabled && this.m_Debug.DebuggerAttached != null)
			{
				this.m_Debug.DebuggerAttached.SignalExecutionEnded();
			}
			if (flag && this.m_ExecutionNesting == 0 && this.m_CoroutinesStack.Count == 0 && this.m_ValueStack.Count > 0)
			{
				this.m_ValueStack.Clear();
				this.stats.PushClear();
			}
			this.LogStats();
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x00121980 File Offset: 0x0011FB80
		private void PrintStackState(string prefix)
		{
			Debug.Log(string.Format("{0}: Main Processor ValueStack size={1}, coroutineCount={2}, executionNesting={3}", new object[]
			{
				prefix,
				this.m_ValueStack.Count,
				this.m_CoroutinesStack.Count,
				this.m_ExecutionNesting
			}));
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x00025E2A File Offset: 0x0002402A
		private int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x001219DC File Offset: 0x0011FBDC
		private void EnterProcessor()
		{
			int threadId = this.GetThreadId();
			if (this.m_OwningThreadID >= 0 && this.m_OwningThreadID != threadId && this.m_Script.Options.CheckThreadAccess)
			{
				throw new InvalidOperationException(string.Format("Cannot enter the same MoonSharp processor from two different threads : {0} and {1}", this.m_OwningThreadID, threadId));
			}
			this.m_OwningThreadID = threadId;
			this.m_ExecutionNesting++;
			if (this.m_Parent != null)
			{
				this.m_Parent.m_CoroutinesStack.Add(this);
			}
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x00025E36 File Offset: 0x00024036
		internal SourceRef GetCoroutineSuspendedLocation()
		{
			return this.GetCurrentSourceRef(this.m_SavedInstructionPtr);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x00025E44 File Offset: 0x00024044
		public ProcessorStats GetProcessorStats()
		{
			return this.stats;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x00121A64 File Offset: 0x0011FC64
		internal static bool IsDumpStream(Stream stream)
		{
			if (stream.Length >= 8L)
			{
				using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
				{
					long num = (long)binaryReader.ReadUInt64();
					stream.Seek(-8L, SeekOrigin.Current);
					return num == 1877195438928383261L;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x00121AC4 File Offset: 0x0011FCC4
		internal int Dump(Stream stream, int baseAddress, bool hasUpvalues)
		{
			int j;
			using (BinaryWriter binaryWriter = new BinDumpBinaryWriter(stream, Encoding.UTF8))
			{
				Dictionary<SymbolRef, int> dictionary = new Dictionary<SymbolRef, int>();
				Instruction instruction = this.FindMeta(ref baseAddress);
				if (instruction == null)
				{
					throw new ArgumentException("baseAddress");
				}
				binaryWriter.Write(1877195438928383261UL);
				binaryWriter.Write(336);
				binaryWriter.Write(hasUpvalues);
				binaryWriter.Write(instruction.NumVal);
				for (int i = 0; i <= instruction.NumVal; i++)
				{
					SymbolRef[] array;
					SymbolRef symbolRef;
					this.m_RootChunk.Code[baseAddress + i].GetSymbolReferences(out array, out symbolRef);
					if (symbolRef != null)
					{
						this.AddSymbolToMap(dictionary, symbolRef);
					}
					if (array != null)
					{
						foreach (SymbolRef s in array)
						{
							this.AddSymbolToMap(dictionary, s);
						}
					}
				}
				foreach (SymbolRef symbolRef2 in dictionary.Keys.ToArray<SymbolRef>())
				{
					if (symbolRef2.i_Env != null)
					{
						this.AddSymbolToMap(dictionary, symbolRef2.i_Env);
					}
				}
				SymbolRef[] array3 = new SymbolRef[dictionary.Count];
				foreach (KeyValuePair<SymbolRef, int> keyValuePair in dictionary)
				{
					array3[keyValuePair.Value] = keyValuePair.Key;
				}
				binaryWriter.Write(dictionary.Count);
				SymbolRef[] array2 = array3;
				for (j = 0; j < array2.Length; j++)
				{
					array2[j].WriteBinary(binaryWriter);
				}
				array2 = array3;
				for (j = 0; j < array2.Length; j++)
				{
					array2[j].WriteBinaryEnv(binaryWriter, dictionary);
				}
				for (int k = 0; k <= instruction.NumVal; k++)
				{
					this.m_RootChunk.Code[baseAddress + k].WriteBinary(binaryWriter, baseAddress, dictionary);
				}
				j = instruction.NumVal + baseAddress + 1;
			}
			return j;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x00025E4C File Offset: 0x0002404C
		private void AddSymbolToMap(Dictionary<SymbolRef, int> symbolMap, SymbolRef s)
		{
			if (!symbolMap.ContainsKey(s))
			{
				symbolMap.Add(s, symbolMap.Count);
			}
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x00121CE8 File Offset: 0x0011FEE8
		internal int Undump(Stream stream, int sourceID, Table envTable, out bool hasUpvalues)
		{
			int count = this.m_RootChunk.Code.Count;
			SourceRef chunkRef = new SourceRef(sourceID, 0, 0, 0, 0, false);
			int result;
			using (BinaryReader binaryReader = new BinDumpBinaryReader(stream, Encoding.UTF8))
			{
				if (binaryReader.ReadUInt64() != 1877195438928383261UL)
				{
					throw new ArgumentException("Not a MoonSharp chunk");
				}
				if (binaryReader.ReadInt32() != 336)
				{
					throw new ArgumentException("Invalid version");
				}
				hasUpvalues = binaryReader.ReadBoolean();
				int num = binaryReader.ReadInt32();
				int num2 = binaryReader.ReadInt32();
				SymbolRef[] array = new SymbolRef[num2];
				for (int i = 0; i < num2; i++)
				{
					array[i] = SymbolRef.ReadBinary(binaryReader);
				}
				for (int j = 0; j < num2; j++)
				{
					array[j].ReadBinaryEnv(binaryReader, array);
				}
				for (int k = 0; k <= num; k++)
				{
					Instruction item = Instruction.ReadBinary(chunkRef, binaryReader, count, envTable, array);
					this.m_RootChunk.Code.Add(item);
				}
				result = count;
			}
			return result;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x00025E64 File Offset: 0x00024064
		public DynValue Coroutine_Create(Closure closure)
		{
			Processor processor = new Processor(this);
			processor.m_ValueStack.Push(DynValue.NewClosure(closure));
			return DynValue.NewCoroutine(new Coroutine(processor));
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x00025E88 File Offset: 0x00024088
		public CoroutineState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x00025E90 File Offset: 0x00024090
		// (set) Token: 0x060037E5 RID: 14309 RVA: 0x00025E98 File Offset: 0x00024098
		public Coroutine AssociatedCoroutine { get; set; }

		// Token: 0x060037E6 RID: 14310 RVA: 0x00121E00 File Offset: 0x00120000
		public DynValue Coroutine_Resume(DynValue[] args)
		{
			this.EnterProcessor();
			DynValue result;
			try
			{
				int instructionPtr = 0;
				if (this.m_State != CoroutineState.NotStarted && this.m_State != CoroutineState.Suspended && this.m_State != CoroutineState.ForceSuspended)
				{
					throw ScriptRuntimeException.CannotResumeNotSuspended(this.m_State);
				}
				if (this.m_State == CoroutineState.NotStarted)
				{
					instructionPtr = this.PushClrToScriptStackFrame(CallStackItemFlags.ResumeEntryPoint, null, args);
				}
				else if (this.m_State == CoroutineState.Suspended)
				{
					this.m_ValueStack.Push(DynValue.NewTuple(args));
					instructionPtr = this.m_SavedInstructionPtr;
				}
				else if (this.m_State == CoroutineState.ForceSuspended)
				{
					if (args != null && args.Length != 0)
					{
						throw new ArgumentException("When resuming a force-suspended coroutine, args must be empty.");
					}
					instructionPtr = this.m_SavedInstructionPtr;
				}
				this.m_State = CoroutineState.Running;
				DynValue dynValue = this.Processing_Loop(instructionPtr);
				if (dynValue.Type == DataType.YieldRequest)
				{
					if (dynValue.YieldRequest.Forced)
					{
						this.m_State = CoroutineState.ForceSuspended;
						result = dynValue;
					}
					else
					{
						this.m_State = CoroutineState.Suspended;
						result = DynValue.NewTuple(dynValue.YieldRequest.ReturnValues);
					}
				}
				else
				{
					this.m_State = CoroutineState.Dead;
					result = dynValue;
				}
			}
			catch (Exception)
			{
				this.m_State = CoroutineState.Dead;
				throw;
			}
			finally
			{
				this.LeaveProcessor();
			}
			return result;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x00121F1C File Offset: 0x0012011C
		internal Instruction FindMeta(ref int baseAddress)
		{
			Instruction instruction = this.m_RootChunk.Code[baseAddress];
			while (instruction.OpCode == OpCode.Nop)
			{
				baseAddress++;
				instruction = this.m_RootChunk.Code[baseAddress];
			}
			if (instruction.OpCode != OpCode.Meta)
			{
				return null;
			}
			return instruction;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x00025EA1 File Offset: 0x000240A1
		internal void AttachDebugger(IDebugger debugger)
		{
			this.m_Debug.DebuggerAttached = debugger;
			this.m_Debug.LineBasedBreakPoints = ((debugger.GetDebuggerCaps() & DebuggerCaps.HasLineBasedBreakpoints) > (DebuggerCaps)0);
			debugger.SetDebugService(new DebugService(this.m_Script, this));
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x00025ED7 File Offset: 0x000240D7
		// (set) Token: 0x060037EA RID: 14314 RVA: 0x00025EE4 File Offset: 0x000240E4
		internal bool DebuggerEnabled
		{
			get
			{
				return this.m_Debug.DebuggerEnabled;
			}
			set
			{
				this.m_Debug.DebuggerEnabled = value;
			}
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x00121F6C File Offset: 0x0012016C
		private void ListenDebugger(Instruction instr, int instructionPtr)
		{
			bool flag = false;
			if (instr.SourceCodeRef != null && this.m_Debug.LastHlRef != null)
			{
				if (this.m_Debug.LineBasedBreakPoints)
				{
					flag = (instr.SourceCodeRef.SourceIdx != this.m_Debug.LastHlRef.SourceIdx || instr.SourceCodeRef.FromLine != this.m_Debug.LastHlRef.FromLine);
				}
				else
				{
					flag = (instr.SourceCodeRef != this.m_Debug.LastHlRef);
				}
			}
			else if (this.m_Debug.LastHlRef == null)
			{
				flag = (instr.SourceCodeRef != null);
			}
			if (this.m_Debug.DebuggerAttached.IsPauseRequested() || (instr.SourceCodeRef != null && instr.SourceCodeRef.Breakpoint && flag))
			{
				this.m_Debug.DebuggerCurrentAction = DebuggerAction.ActionType.None;
				this.m_Debug.DebuggerCurrentActionTarget = -1;
			}
			switch (this.m_Debug.DebuggerCurrentAction)
			{
			case DebuggerAction.ActionType.ByteCodeStepOver:
				if (this.m_Debug.DebuggerCurrentActionTarget != instructionPtr)
				{
					return;
				}
				break;
			case DebuggerAction.ActionType.ByteCodeStepOut:
			case DebuggerAction.ActionType.StepOut:
				if (this.m_ExecutionStack.Count >= this.m_Debug.ExStackDepthAtStep)
				{
					return;
				}
				break;
			case DebuggerAction.ActionType.StepIn:
				if (this.m_ExecutionStack.Count >= this.m_Debug.ExStackDepthAtStep && (instr.SourceCodeRef == null || instr.SourceCodeRef == this.m_Debug.LastHlRef))
				{
					return;
				}
				break;
			case DebuggerAction.ActionType.StepOver:
				if (instr.SourceCodeRef == null || instr.SourceCodeRef == this.m_Debug.LastHlRef || this.m_ExecutionStack.Count > this.m_Debug.ExStackDepthAtStep)
				{
					return;
				}
				break;
			case DebuggerAction.ActionType.Run:
				if (this.m_Debug.LineBasedBreakPoints)
				{
					this.m_Debug.LastHlRef = instr.SourceCodeRef;
				}
				return;
			}
			this.RefreshDebugger(false, instructionPtr);
			DebuggerAction action;
			for (;;)
			{
				action = this.m_Debug.DebuggerAttached.GetAction(instructionPtr, instr.SourceCodeRef);
				switch (action.Action)
				{
				case DebuggerAction.ActionType.ByteCodeStepIn:
					goto IL_25F;
				case DebuggerAction.ActionType.ByteCodeStepOver:
					goto IL_278;
				case DebuggerAction.ActionType.ByteCodeStepOut:
				case DebuggerAction.ActionType.StepIn:
				case DebuggerAction.ActionType.StepOver:
				case DebuggerAction.ActionType.StepOut:
					goto IL_226;
				case DebuggerAction.ActionType.Run:
					goto IL_293;
				case DebuggerAction.ActionType.ToggleBreakpoint:
					this.ToggleBreakPoint(action, null);
					this.RefreshDebugger(true, instructionPtr);
					break;
				case DebuggerAction.ActionType.SetBreakpoint:
					this.ToggleBreakPoint(action, new bool?(true));
					this.RefreshDebugger(true, instructionPtr);
					break;
				case DebuggerAction.ActionType.ClearBreakpoint:
					this.ToggleBreakPoint(action, new bool?(false));
					this.RefreshDebugger(true, instructionPtr);
					break;
				case DebuggerAction.ActionType.ResetBreakpoints:
					this.ResetBreakPoints(action);
					this.RefreshDebugger(true, instructionPtr);
					break;
				case DebuggerAction.ActionType.Refresh:
					this.RefreshDebugger(false, instructionPtr);
					break;
				case DebuggerAction.ActionType.HardRefresh:
					this.RefreshDebugger(true, instructionPtr);
					break;
				}
			}
			IL_226:
			this.m_Debug.DebuggerCurrentAction = action.Action;
			this.m_Debug.LastHlRef = instr.SourceCodeRef;
			this.m_Debug.ExStackDepthAtStep = this.m_ExecutionStack.Count;
			return;
			IL_25F:
			this.m_Debug.DebuggerCurrentAction = DebuggerAction.ActionType.ByteCodeStepIn;
			this.m_Debug.DebuggerCurrentActionTarget = -1;
			return;
			IL_278:
			this.m_Debug.DebuggerCurrentAction = DebuggerAction.ActionType.ByteCodeStepOver;
			this.m_Debug.DebuggerCurrentActionTarget = instructionPtr + 1;
			return;
			IL_293:
			this.m_Debug.DebuggerCurrentAction = DebuggerAction.ActionType.Run;
			this.m_Debug.LastHlRef = instr.SourceCodeRef;
			this.m_Debug.DebuggerCurrentActionTarget = -1;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x001222B8 File Offset: 0x001204B8
		private void ResetBreakPoints(DebuggerAction action)
		{
			SourceCode sourceCode = this.m_Script.GetSourceCode(action.SourceID);
			this.ResetBreakPoints(sourceCode, new HashSet<int>(action.Lines));
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x001222EC File Offset: 0x001204EC
		internal HashSet<int> ResetBreakPoints(SourceCode src, HashSet<int> lines)
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (SourceRef sourceRef in src.Refs)
			{
				if (!sourceRef.CannotBreakpoint)
				{
					sourceRef.Breakpoint = lines.Contains(sourceRef.FromLine);
					if (sourceRef.Breakpoint)
					{
						hashSet.Add(sourceRef.FromLine);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x00122370 File Offset: 0x00120570
		private bool ToggleBreakPoint(DebuggerAction action, bool? state)
		{
			SourceCode sourceCode = this.m_Script.GetSourceCode(action.SourceID);
			bool flag = false;
			foreach (SourceRef sourceRef in sourceCode.Refs)
			{
				if (!sourceRef.CannotBreakpoint && sourceRef.IncludesLocation(action.SourceID, action.SourceLine, action.SourceCol))
				{
					flag = true;
					if (state == null)
					{
						sourceRef.Breakpoint = !sourceRef.Breakpoint;
					}
					else
					{
						sourceRef.Breakpoint = state.Value;
					}
					if (sourceRef.Breakpoint)
					{
						this.m_Debug.BreakPoints.Add(sourceRef);
					}
					else
					{
						this.m_Debug.BreakPoints.Remove(sourceRef);
					}
				}
			}
			if (flag)
			{
				return true;
			}
			int num = int.MaxValue;
			SourceRef sourceRef2 = null;
			foreach (SourceRef sourceRef3 in sourceCode.Refs)
			{
				if (!sourceRef3.CannotBreakpoint)
				{
					int locationDistance = sourceRef3.GetLocationDistance(action.SourceID, action.SourceLine, action.SourceCol);
					if (locationDistance < num)
					{
						num = locationDistance;
						sourceRef2 = sourceRef3;
					}
				}
			}
			if (sourceRef2 != null)
			{
				if (state == null)
				{
					sourceRef2.Breakpoint = !sourceRef2.Breakpoint;
				}
				else
				{
					sourceRef2.Breakpoint = state.Value;
				}
				if (sourceRef2.Breakpoint)
				{
					this.m_Debug.BreakPoints.Add(sourceRef2);
				}
				else
				{
					this.m_Debug.BreakPoints.Remove(sourceRef2);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x00122530 File Offset: 0x00120730
		private void RefreshDebugger(bool hard, int instructionPtr)
		{
			SourceRef currentSourceRef = this.GetCurrentSourceRef(instructionPtr);
			ScriptExecutionContext context = new ScriptExecutionContext(this, null, currentSourceRef, false);
			List<DynamicExpression> watchItems = this.m_Debug.DebuggerAttached.GetWatchItems();
			List<WatchItem> items = this.Debugger_GetCallStack(currentSourceRef);
			List<WatchItem> items2 = this.Debugger_RefreshWatches(context, watchItems);
			List<WatchItem> items3 = this.Debugger_RefreshVStack();
			List<WatchItem> items4 = this.Debugger_RefreshLocals(context);
			List<WatchItem> items5 = this.Debugger_RefreshThreads(context);
			this.m_Debug.DebuggerAttached.Update(WatchType.CallStack, items);
			this.m_Debug.DebuggerAttached.Update(WatchType.Watches, items2);
			this.m_Debug.DebuggerAttached.Update(WatchType.VStack, items3);
			this.m_Debug.DebuggerAttached.Update(WatchType.Locals, items4);
			this.m_Debug.DebuggerAttached.Update(WatchType.Threads, items5);
			if (hard)
			{
				this.m_Debug.DebuggerAttached.RefreshBreakpoints(this.m_Debug.BreakPoints);
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x00122608 File Offset: 0x00120808
		private List<WatchItem> Debugger_RefreshThreads(ScriptExecutionContext context)
		{
			return (from c in (this.m_Parent != null) ? this.m_Parent.m_CoroutinesStack : this.m_CoroutinesStack
			select new WatchItem
			{
				Address = c.AssociatedCoroutine.ReferenceID,
				Name = "coroutine #" + c.AssociatedCoroutine.ReferenceID.ToString()
			}).ToList<WatchItem>();
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x0012265C File Offset: 0x0012085C
		private List<WatchItem> Debugger_RefreshVStack()
		{
			List<WatchItem> list = new List<WatchItem>();
			for (int i = 0; i < Math.Min(32, this.m_ValueStack.Count); i++)
			{
				list.Add(new WatchItem
				{
					Address = i,
					Value = this.m_ValueStack.Peek(i)
				});
			}
			return list;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x001226B4 File Offset: 0x001208B4
		private List<WatchItem> Debugger_RefreshWatches(ScriptExecutionContext context, List<DynamicExpression> watchList)
		{
			return (from w in watchList
			select this.Debugger_RefreshWatch(context, w)).ToList<WatchItem>();
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x001226EC File Offset: 0x001208EC
		private List<WatchItem> Debugger_RefreshLocals(ScriptExecutionContext context)
		{
			List<WatchItem> list = new List<WatchItem>();
			CallStackItem callStackItem = this.m_ExecutionStack.Peek(0);
			if (callStackItem != null && callStackItem.Debug_Symbols != null && callStackItem.LocalScope != null)
			{
				int num = Math.Min(callStackItem.Debug_Symbols.Length, callStackItem.LocalScope.Length);
				for (int i = 0; i < num; i++)
				{
					list.Add(new WatchItem
					{
						IsError = false,
						LValue = callStackItem.Debug_Symbols[i],
						Value = callStackItem.LocalScope[i],
						Name = callStackItem.Debug_Symbols[i].i_Name
					});
				}
			}
			return list;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x00122784 File Offset: 0x00120984
		private WatchItem Debugger_RefreshWatch(ScriptExecutionContext context, DynamicExpression dynExpr)
		{
			WatchItem result;
			try
			{
				SymbolRef lvalue = dynExpr.FindSymbol(context);
				DynValue value = dynExpr.Evaluate(context);
				result = new WatchItem
				{
					IsError = dynExpr.IsConstant(),
					LValue = lvalue,
					Value = value,
					Name = dynExpr.ExpressionCode
				};
			}
			catch (Exception ex)
			{
				result = new WatchItem
				{
					IsError = true,
					Value = DynValue.NewString(ex.Message),
					Name = dynExpr.ExpressionCode
				};
			}
			return result;
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x00122810 File Offset: 0x00120A10
		internal List<WatchItem> Debugger_GetCallStack(SourceRef startingRef)
		{
			List<WatchItem> list = new List<WatchItem>();
			for (int i = 0; i < this.m_ExecutionStack.Count; i++)
			{
				CallStackItem callStackItem = this.m_ExecutionStack.Peek(i);
				Instruction instruction = this.m_RootChunk.Code[callStackItem.Debug_EntryPoint];
				string name = (instruction.OpCode == OpCode.Meta) ? instruction.Name : null;
				if (callStackItem.ClrFunction != null)
				{
					list.Add(new WatchItem
					{
						Address = -1,
						BasePtr = -1,
						RetAddress = callStackItem.ReturnAddress,
						Location = startingRef,
						Name = callStackItem.ClrFunction.Name
					});
				}
				else
				{
					list.Add(new WatchItem
					{
						Address = callStackItem.Debug_EntryPoint,
						BasePtr = callStackItem.BasePointer,
						RetAddress = callStackItem.ReturnAddress,
						Name = name,
						Location = startingRef
					});
				}
				startingRef = callStackItem.CallingSourceRef;
				if (callStackItem.Continuation != null)
				{
					list.Add(new WatchItem
					{
						Name = callStackItem.Continuation.Name,
						Location = SourceRef.GetClrLocation()
					});
				}
			}
			return list;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x00025EF2 File Offset: 0x000240F2
		private SourceRef GetCurrentSourceRef(int instructionPtr)
		{
			if (instructionPtr >= 0 && instructionPtr < this.m_RootChunk.Code.Count)
			{
				return this.m_RootChunk.Code[instructionPtr].SourceCodeRef;
			}
			return null;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x00122938 File Offset: 0x00120B38
		private void FillDebugData(InterpreterException ex, int ip)
		{
			if (ip == -99)
			{
				ip = this.m_SavedInstructionPtr;
			}
			else
			{
				ip--;
			}
			ex.InstructionPtr = ip;
			SourceRef currentSourceRef = this.GetCurrentSourceRef(ip);
			ex.DecorateMessage(this.m_Script, currentSourceRef, ip);
			ex.CallStack = this.Debugger_GetCallStack(currentSourceRef);
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x00025F23 File Offset: 0x00024123
		internal Table GetMetatable(DynValue value)
		{
			if (value.Type == DataType.Table)
			{
				return value.Table.MetaTable;
			}
			if (value.Type.CanHaveTypeMetatables())
			{
				return this.m_Script.GetTypeMetatable(value.Type);
			}
			return null;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x00122984 File Offset: 0x00120B84
		internal DynValue GetBinaryMetamethod(DynValue op1, DynValue op2, string eventName)
		{
			Table metatable = this.GetMetatable(op1);
			if (metatable != null)
			{
				DynValue dynValue = metatable.RawGet(eventName);
				if (dynValue != null && dynValue.IsNotNil())
				{
					return dynValue;
				}
			}
			Table metatable2 = this.GetMetatable(op2);
			if (metatable2 != null)
			{
				DynValue dynValue2 = metatable2.RawGet(eventName);
				if (dynValue2 != null && dynValue2.IsNotNil())
				{
					return dynValue2;
				}
			}
			if (op1.Type == DataType.UserData)
			{
				DynValue dynValue3 = op1.UserData.Descriptor.MetaIndex(this.m_Script, op1.UserData.Object, eventName);
				if (dynValue3 != null)
				{
					return dynValue3;
				}
			}
			if (op2.Type == DataType.UserData)
			{
				DynValue dynValue4 = op2.UserData.Descriptor.MetaIndex(this.m_Script, op2.UserData.Object, eventName);
				if (dynValue4 != null)
				{
					return dynValue4;
				}
			}
			return null;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x00122A3C File Offset: 0x00120C3C
		internal DynValue GetMetamethod(DynValue value, string metamethod)
		{
			if (value.Type == DataType.UserData)
			{
				DynValue dynValue = value.UserData.Descriptor.MetaIndex(this.m_Script, value.UserData.Object, metamethod);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return this.GetMetamethodRaw(value, metamethod);
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x00122A84 File Offset: 0x00120C84
		internal DynValue GetMetamethodRaw(DynValue value, string metamethod)
		{
			Table metatable = this.GetMetatable(value);
			if (metatable == null)
			{
				return null;
			}
			DynValue dynValue = metatable.RawGet(metamethod);
			if (dynValue == null || dynValue.IsNil())
			{
				return null;
			}
			return dynValue;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x00025F5A File Offset: 0x0002415A
		internal Script GetScript()
		{
			return this.m_Script;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x00025F62 File Offset: 0x00024162
		private void LogStats()
		{
			this.stats.Log(this.m_ValueStack.Count, this.m_ExecutionStack.Count);
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x00122AB4 File Offset: 0x00120CB4
		private DynValue Processing_Loop(int instructionPtr)
		{
			this.LogStats();
			long num = 0L;
			bool flag = this.AutoYieldCounter > 0L && this.m_CanYield && this.State > CoroutineState.Main;
			for (;;)
			{
				IL_28:
				try
				{
					Instruction instruction;
					for (;;)
					{
						instruction = this.m_RootChunk.Code[instructionPtr];
						if (this.m_Debug.DebuggerAttached != null)
						{
							this.ListenDebugger(instruction, instructionPtr);
						}
						num += 1L;
						if (flag && num > this.AutoYieldCounter)
						{
							break;
						}
						instructionPtr++;
						switch (instruction.OpCode)
						{
						case OpCode.Nop:
						case OpCode.Debug:
						case OpCode.Meta:
							continue;
						case OpCode.Pop:
							this.m_ValueStack.RemoveLast(instruction.NumVal);
							continue;
						case OpCode.Copy:
							this.m_ValueStack.Push(this.m_ValueStack.Peek(instruction.NumVal));
							continue;
						case OpCode.Swap:
							this.ExecSwap(instruction);
							continue;
						case OpCode.Literal:
							this.m_ValueStack.Push(instruction.Value);
							continue;
						case OpCode.Closure:
							this.ExecClosure(instruction);
							continue;
						case OpCode.NewTable:
							if (instruction.NumVal == 0)
							{
								this.m_ValueStack.Push(DynValue.NewTable(this.m_Script));
								continue;
							}
							this.m_ValueStack.Push(DynValue.NewPrimeTable());
							continue;
						case OpCode.TblInitN:
							this.ExecTblInitN(instruction);
							continue;
						case OpCode.TblInitI:
							this.ExecTblInitI(instruction);
							continue;
						case OpCode.StoreLcl:
							this.ExecStoreLcl(instruction);
							continue;
						case OpCode.Local:
						{
							DynValue[] localScope = this.m_ExecutionStack.Peek(0).LocalScope;
							int i_Index = instruction.Symbol.i_Index;
							this.m_ValueStack.Push(localScope[i_Index].AsReadOnly());
							continue;
						}
						case OpCode.StoreUpv:
							this.ExecStoreUpv(instruction);
							continue;
						case OpCode.Upvalue:
							this.m_ValueStack.Push(this.m_ExecutionStack.Peek(0).ClosureScope[instruction.Symbol.i_Index].AsReadOnly());
							continue;
						case OpCode.IndexSet:
						case OpCode.IndexSetN:
						case OpCode.IndexSetL:
							instructionPtr = this.ExecIndexSet(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_31;
							}
							continue;
						case OpCode.Index:
						case OpCode.IndexN:
						case OpCode.IndexL:
							instructionPtr = this.ExecIndex(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_30;
							}
							continue;
						case OpCode.Clean:
							this.ClearBlockData(instruction);
							continue;
						case OpCode.BeginFn:
							this.ExecBeginFn(instruction);
							continue;
						case OpCode.Args:
							this.ExecArgs(instruction);
							continue;
						case OpCode.Call:
						case OpCode.ThisCall:
							instructionPtr = this.Internal_ExecCall(instruction.NumVal, instructionPtr, null, null, instruction.OpCode == OpCode.ThisCall, instruction.Name, null);
							if (instructionPtr == -99)
							{
								goto Block_20;
							}
							continue;
						case OpCode.Ret:
							instructionPtr = this.ExecRet(instruction);
							if (instructionPtr == -99)
							{
								goto IL_5CE;
							}
							if (instructionPtr < 0)
							{
								goto Block_27;
							}
							continue;
						case OpCode.Jump:
							instructionPtr = instruction.NumVal;
							if (instructionPtr == -99)
							{
								goto Block_25;
							}
							continue;
						case OpCode.Jf:
							instructionPtr = this.JumpBool(instruction, false, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_24;
							}
							continue;
						case OpCode.JNil:
						{
							DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
							if (dynValue.Type == DataType.Nil || dynValue.Type == DataType.Void)
							{
								instructionPtr = instruction.NumVal;
							}
							if (instructionPtr == -99)
							{
								goto Block_23;
							}
							continue;
						}
						case OpCode.JFor:
							instructionPtr = this.ExecJFor(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_28;
							}
							continue;
						case OpCode.JtOrPop:
						case OpCode.JfOrPop:
							instructionPtr = this.ExecShortCircuitingOperator(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_21;
							}
							continue;
						case OpCode.Concat:
							instructionPtr = this.ExecConcat(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_9;
							}
							continue;
						case OpCode.LessEq:
							instructionPtr = this.ExecLessEq(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_17;
							}
							continue;
						case OpCode.Less:
							instructionPtr = this.ExecLess(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_18;
							}
							continue;
						case OpCode.Eq:
							instructionPtr = this.ExecEq(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_16;
							}
							continue;
						case OpCode.Add:
							instructionPtr = this.ExecAdd(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_8;
							}
							continue;
						case OpCode.Sub:
							instructionPtr = this.ExecSub(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_11;
							}
							continue;
						case OpCode.Mul:
							instructionPtr = this.ExecMul(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_12;
							}
							continue;
						case OpCode.Div:
							instructionPtr = this.ExecDiv(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_13;
							}
							continue;
						case OpCode.Mod:
							instructionPtr = this.ExecMod(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_14;
							}
							continue;
						case OpCode.Not:
							this.ExecNot(instruction);
							continue;
						case OpCode.Len:
							instructionPtr = this.ExecLen(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_19;
							}
							continue;
						case OpCode.Neg:
							instructionPtr = this.ExecNeg(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_10;
							}
							continue;
						case OpCode.Power:
							instructionPtr = this.ExecPower(instruction, instructionPtr);
							if (instructionPtr == -99)
							{
								goto Block_15;
							}
							continue;
						case OpCode.CNot:
							this.ExecCNot(instruction);
							continue;
						case OpCode.MkTuple:
							this.ExecMkTuple(instruction);
							continue;
						case OpCode.Scalar:
							this.m_ValueStack.Push(this.m_ValueStack.Pop().ToScalar());
							continue;
						case OpCode.Incr:
							this.ExecIncr(instruction);
							continue;
						case OpCode.ToNum:
							this.ExecToNum(instruction);
							continue;
						case OpCode.ToBool:
							this.m_ValueStack.Push(DynValue.NewBoolean(this.m_ValueStack.Pop().ToScalar().CastToBool()));
							continue;
						case OpCode.ExpTuple:
							this.ExecExpTuple(instruction);
							continue;
						case OpCode.IterPrep:
							this.ExecIterPrep(instruction);
							continue;
						case OpCode.IterUpd:
							this.ExecIterUpd(instruction);
							continue;
						case OpCode.Invalid:
							goto IL_59D;
						}
						goto Block_7;
					}
					this.m_SavedInstructionPtr = instructionPtr;
					return DynValue.NewForcedYieldReq();
					Block_7:
					throw new NotImplementedException(string.Format("Execution for {0} not implented yet!", instruction.OpCode));
					Block_8:
					Block_9:
					Block_10:
					Block_11:
					Block_12:
					Block_13:
					Block_14:
					Block_15:
					Block_16:
					Block_17:
					Block_18:
					Block_19:
					Block_20:
					Block_21:
					Block_23:
					Block_24:
					Block_25:
					goto IL_5CE;
					Block_27:
					break;
					Block_28:
					Block_30:
					Block_31:
					goto IL_5CE;
					IL_59D:
					throw new NotImplementedException(string.Format("Invalid opcode : {0}", instruction.Name));
					IL_5CE:
					DynValue result = this.m_ValueStack.Pop().ToScalar();
					if (this.m_CanYield)
					{
						return result;
					}
					if (this.State == CoroutineState.Main)
					{
						throw ScriptRuntimeException.CannotYieldMain();
					}
					throw ScriptRuntimeException.CannotYield();
				}
				catch (InterpreterException ex)
				{
					this.FillDebugData(ex, instructionPtr);
					if (!(ex is ScriptRuntimeException))
					{
						ex.Rethrow();
						throw;
					}
					if (this.m_Debug.DebuggerAttached != null && this.m_Debug.DebuggerAttached.SignalRuntimeException((ScriptRuntimeException)ex) && instructionPtr >= 0 && instructionPtr < this.m_RootChunk.Code.Count)
					{
						this.ListenDebugger(this.m_RootChunk.Code[instructionPtr], instructionPtr);
					}
					for (int i = 0; i < this.m_ExecutionStack.Count; i++)
					{
						CallStackItem callStackItem = this.m_ExecutionStack.Peek(i);
						if (callStackItem.ErrorHandlerBeforeUnwind != null)
						{
							ex.DecoratedMessage = this.PerformMessageDecorationBeforeUnwind(callStackItem.ErrorHandlerBeforeUnwind, ex.DecoratedMessage, this.GetCurrentSourceRef(instructionPtr));
						}
					}
					while (this.m_ExecutionStack.Count > 0)
					{
						CallStackItem callStackItem2 = this.PopToBasePointer();
						if (callStackItem2.ErrorHandler != null)
						{
							instructionPtr = callStackItem2.ReturnAddress;
							if (callStackItem2.ClrFunction == null)
							{
								int num2 = (int)this.m_ValueStack.Pop().Number;
								this.m_ValueStack.RemoveLast(num2 + 1);
							}
							DynValue[] args = new DynValue[]
							{
								DynValue.NewString(ex.DecoratedMessage)
							};
							DynValue item = callStackItem2.ErrorHandler.Invoke(new ScriptExecutionContext(this, callStackItem2.ErrorHandler, this.GetCurrentSourceRef(instructionPtr), false), args, false);
							this.m_ValueStack.Push(item);
							goto IL_28;
						}
						if ((callStackItem2.Flags & CallStackItemFlags.EntryPoint) != CallStackItemFlags.None)
						{
							ex.Rethrow();
							throw;
						}
					}
					ex.Rethrow();
					throw;
				}
				catch (Exception ex2)
				{
					if (Script.GlobalOptions.DecorateAllExceptions)
					{
						ScriptRuntimeException ex3 = new ScriptRuntimeException(ex2);
						this.FillDebugData(ex3, instructionPtr);
						ex3.Rethrow();
						throw ex3;
					}
					throw;
				}
				break;
			}
			return this.m_ValueStack.Pop();
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x001232C4 File Offset: 0x001214C4
		internal string PerformMessageDecorationBeforeUnwind(DynValue messageHandler, string decoratedMessage, SourceRef sourceRef)
		{
			try
			{
				DynValue[] args = new DynValue[]
				{
					DynValue.NewString(decoratedMessage)
				};
				DynValue dynValue = DynValue.Nil;
				if (messageHandler.Type == DataType.Function)
				{
					dynValue = this.Call(messageHandler, args);
				}
				else
				{
					if (messageHandler.Type != DataType.ClrFunction)
					{
						throw new ScriptRuntimeException("error handler not set to a function");
					}
					ScriptExecutionContext executionContext = new ScriptExecutionContext(this, messageHandler.Callback, sourceRef, false);
					dynValue = messageHandler.Callback.Invoke(executionContext, args, false);
				}
				string text = dynValue.ToPrintString();
				if (text != null)
				{
					return text;
				}
			}
			catch (ScriptRuntimeException ex)
			{
				return ex.Message + "\n" + decoratedMessage;
			}
			return decoratedMessage;
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0012336C File Offset: 0x0012156C
		private void AssignLocal(SymbolRef symref, DynValue value)
		{
			CallStackItem callStackItem = this.m_ExecutionStack.Peek(0);
			DynValue dynValue = callStackItem.LocalScope[symref.i_Index];
			if (dynValue == null)
			{
				dynValue = (callStackItem.LocalScope[symref.i_Index] = DynValue.NewNil());
			}
			dynValue.Assign(value);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x001233B4 File Offset: 0x001215B4
		private void ExecStoreLcl(Instruction i)
		{
			DynValue storeValue = this.GetStoreValue(i);
			SymbolRef symbol = i.Symbol;
			this.AssignLocal(symbol, storeValue);
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x001233D8 File Offset: 0x001215D8
		private void ExecStoreUpv(Instruction i)
		{
			DynValue storeValue = this.GetStoreValue(i);
			SymbolRef symbol = i.Symbol;
			CallStackItem callStackItem = this.m_ExecutionStack.Peek(0);
			DynValue dynValue = callStackItem.ClosureScope[symbol.i_Index];
			if (dynValue == null)
			{
				dynValue = (callStackItem.ClosureScope[symbol.i_Index] = DynValue.NewNil());
			}
			dynValue.Assign(storeValue);
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x00123438 File Offset: 0x00121638
		private void ExecSwap(Instruction i)
		{
			DynValue item = this.m_ValueStack.Peek(i.NumVal);
			DynValue item2 = this.m_ValueStack.Peek(i.NumVal2);
			this.m_ValueStack.Set(i.NumVal, item2);
			this.m_ValueStack.Set(i.NumVal2, item);
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x00123490 File Offset: 0x00121690
		private DynValue GetStoreValue(Instruction i)
		{
			int numVal = i.NumVal;
			int numVal2 = i.NumVal2;
			DynValue dynValue = this.m_ValueStack.Peek(numVal);
			if (dynValue.Type == DataType.Tuple)
			{
				if (numVal2 >= dynValue.Tuple.Length)
				{
					return DynValue.NewNil();
				}
				return dynValue.Tuple[numVal2];
			}
			else
			{
				if (numVal2 != 0)
				{
					return DynValue.NewNil();
				}
				return dynValue;
			}
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x001234E8 File Offset: 0x001216E8
		private void ExecClosure(Instruction i)
		{
			Closure function = new Closure(this.m_Script, i.NumVal, i.SymbolList, (from s in i.SymbolList
			select this.GetUpvalueSymbol(s)).ToList<DynValue>());
			this.m_ValueStack.Push(DynValue.NewClosure(function));
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0012353C File Offset: 0x0012173C
		private DynValue GetUpvalueSymbol(SymbolRef s)
		{
			if (s.Type == SymbolRefType.Local)
			{
				return this.m_ExecutionStack.Peek(0).LocalScope[s.i_Index];
			}
			if (s.Type == SymbolRefType.Upvalue)
			{
				return this.m_ExecutionStack.Peek(0).ClosureScope[s.i_Index];
			}
			throw new Exception("unsupported symbol type");
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x0012359C File Offset: 0x0012179C
		private void ExecMkTuple(Instruction i)
		{
			Slice<DynValue> values = new Slice<DynValue>(this.m_ValueStack, this.m_ValueStack.Count - i.NumVal, i.NumVal, false);
			DynValue[] values2 = this.Internal_AdjustTuple(values);
			this.m_ValueStack.RemoveLast(i.NumVal);
			this.m_ValueStack.Push(DynValue.NewTuple(values2));
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x001235FC File Offset: 0x001217FC
		private void ExecToNum(Instruction i)
		{
			double? num = this.m_ValueStack.Pop().ToScalar().CastToNumber();
			if (num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(num.Value));
				return;
			}
			throw ScriptRuntimeException.ConvertToNumberFailed(i.NumVal);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0012364C File Offset: 0x0012184C
		private void ExecIterUpd(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Peek(0);
			this.m_ValueStack.Peek(1).Tuple[2] = dynValue;
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0012367C File Offset: 0x0012187C
		private void ExecExpTuple(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Peek(i.NumVal);
			if (dynValue.Type == DataType.Tuple)
			{
				for (int j = 0; j < dynValue.Tuple.Length; j++)
				{
					this.m_ValueStack.Push(dynValue.Tuple[j]);
				}
				return;
			}
			this.m_ValueStack.Push(dynValue);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x001236DC File Offset: 0x001218DC
		private void ExecIterPrep(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Pop();
			if (dynValue.Type != DataType.Tuple)
			{
				dynValue = DynValue.NewTuple(new DynValue[]
				{
					dynValue,
					DynValue.Nil,
					DynValue.Nil
				});
			}
			DynValue dynValue2 = (dynValue.Tuple.Length >= 1) ? dynValue.Tuple[0] : DynValue.Nil;
			DynValue dynValue3 = (dynValue.Tuple.Length >= 2) ? dynValue.Tuple[1] : DynValue.Nil;
			DynValue dynValue4 = (dynValue.Tuple.Length >= 3) ? dynValue.Tuple[2] : DynValue.Nil;
			if (dynValue2.Type != DataType.Function && dynValue2.Type != DataType.ClrFunction)
			{
				DynValue metamethod = this.GetMetamethod(dynValue2, "__iterator");
				if (metamethod != null && !metamethod.IsNil())
				{
					if (metamethod.Type != DataType.Tuple)
					{
						dynValue = this.GetScript().Call(metamethod, new DynValue[]
						{
							dynValue2,
							dynValue3,
							dynValue4
						});
					}
					else
					{
						dynValue = metamethod;
					}
					dynValue2 = ((dynValue.Tuple.Length >= 1) ? dynValue.Tuple[0] : DynValue.Nil);
					dynValue3 = ((dynValue.Tuple.Length >= 2) ? dynValue.Tuple[1] : DynValue.Nil);
					dynValue4 = ((dynValue.Tuple.Length >= 3) ? dynValue.Tuple[2] : DynValue.Nil);
					this.m_ValueStack.Push(DynValue.NewTuple(new DynValue[]
					{
						dynValue2,
						dynValue3,
						dynValue4
					}));
				}
				else if (dynValue2.Type == DataType.Table)
				{
					DynValue metamethod2 = this.GetMetamethod(dynValue2, "__call");
					if (metamethod2 == null || metamethod2.IsNil())
					{
						this.m_ValueStack.Push(EnumerableWrapper.ConvertTable(dynValue2.Table));
					}
				}
			}
			this.m_ValueStack.Push(DynValue.NewTuple(new DynValue[]
			{
				dynValue2,
				dynValue3,
				dynValue4
			}));
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x001238AC File Offset: 0x00121AAC
		private int ExecJFor(Instruction i, int instructionPtr)
		{
			double number = this.m_ValueStack.Peek(0).Number;
			double number2 = this.m_ValueStack.Peek(1).Number;
			double number3 = this.m_ValueStack.Peek(2).Number;
			if (!((number2 > 0.0) ? (number <= number3) : (number >= number3)))
			{
				return i.NumVal;
			}
			return instructionPtr;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x00123914 File Offset: 0x00121B14
		private void ExecIncr(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Peek(0);
			DynValue dynValue2 = this.m_ValueStack.Peek(i.NumVal);
			if (dynValue.ReadOnly)
			{
				this.m_ValueStack.Pop();
				if (dynValue.ReadOnly)
				{
					dynValue = dynValue.CloneAsWritable();
				}
				this.m_ValueStack.Push(dynValue);
			}
			dynValue.AssignNumber(dynValue.Number + dynValue2.Number);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x00123984 File Offset: 0x00121B84
		private void ExecCNot(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			if (dynValue2.Type != DataType.Boolean)
			{
				throw new InternalErrorException("CNOT had non-bool arg");
			}
			if (dynValue2.CastToBool())
			{
				this.m_ValueStack.Push(DynValue.NewBoolean(!dynValue.CastToBool()));
				return;
			}
			this.m_ValueStack.Push(DynValue.NewBoolean(dynValue.CastToBool()));
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x00123A00 File Offset: 0x00121C00
		private void ExecNot(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			this.m_ValueStack.Push(DynValue.NewBoolean(!dynValue.CastToBool()));
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x00025F85 File Offset: 0x00024185
		private void ExecBeginFn(Instruction i)
		{
			CallStackItem callStackItem = this.m_ExecutionStack.Peek(0);
			callStackItem.Debug_Symbols = i.SymbolList;
			callStackItem.LocalScope = new DynValue[i.NumVal];
			this.ClearBlockData(i);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x00123A38 File Offset: 0x00121C38
		private CallStackItem PopToBasePointer()
		{
			CallStackItem callStackItem = this.m_ExecutionStack.Pop();
			if (callStackItem.BasePointer >= 0)
			{
				this.m_ValueStack.CropAtCount(callStackItem.BasePointer);
			}
			return callStackItem;
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x00123A6C File Offset: 0x00121C6C
		private int PopExecStackAndCheckVStack(int vstackguard)
		{
			CallStackItem callStackItem = this.m_ExecutionStack.Pop();
			if (vstackguard != callStackItem.BasePointer)
			{
				throw new InternalErrorException("StackGuard violation");
			}
			return callStackItem.ReturnAddress;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x00123AA0 File Offset: 0x00121CA0
		private IList<DynValue> CreateArgsListForFunctionCall(int numargs, int offsFromTop)
		{
			if (numargs == 0)
			{
				return new DynValue[0];
			}
			DynValue dynValue = this.m_ValueStack.Peek(offsFromTop);
			if (dynValue.Type == DataType.Tuple && dynValue.Tuple.Length > 1)
			{
				List<DynValue> list = new List<DynValue>();
				for (int i = 0; i < numargs - 1; i++)
				{
					list.Add(this.m_ValueStack.Peek(numargs - i - 1 + offsFromTop));
				}
				for (int j = 0; j < dynValue.Tuple.Length; j++)
				{
					list.Add(dynValue.Tuple[j]);
				}
				return list;
			}
			return new Slice<DynValue>(this.m_ValueStack, this.m_ValueStack.Count - numargs - offsFromTop, numargs, false);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00123B44 File Offset: 0x00121D44
		private void ExecArgs(Instruction I)
		{
			int numargs = (int)this.m_ValueStack.Peek(0).Number;
			IList<DynValue> list = this.CreateArgsListForFunctionCall(numargs, 1);
			for (int i = 0; i < I.SymbolList.Length; i++)
			{
				if (i >= list.Count)
				{
					this.AssignLocal(I.SymbolList[i], DynValue.NewNil());
				}
				else if (i == I.SymbolList.Length - 1 && I.SymbolList[i].i_Name == "...")
				{
					int num = list.Count - i;
					DynValue[] array = new DynValue[num];
					int j = 0;
					while (j < num)
					{
						array[j] = list[i].ToScalar().CloneAsWritable();
						j++;
						i++;
					}
					this.AssignLocal(I.SymbolList[I.SymbolList.Length - 1], DynValue.NewTuple(this.Internal_AdjustTuple(array)));
				}
				else
				{
					this.AssignLocal(I.SymbolList[i], list[i].ToScalar().CloneAsWritable());
				}
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00123C50 File Offset: 0x00121E50
		private int Internal_ExecCall(int argsCount, int instructionPtr, CallbackFunction handler = null, CallbackFunction continuation = null, bool thisCall = false, string debugText = null, DynValue unwindHandler = null)
		{
			DynValue dynValue = this.m_ValueStack.Peek(argsCount);
			CallStackItemFlags callStackItemFlags = thisCall ? CallStackItemFlags.MethodCall : CallStackItemFlags.None;
			if (((this.m_ExecutionStack.Count > this.m_Script.Options.TailCallOptimizationThreshold && this.m_ExecutionStack.Count > 1) || (this.m_ValueStack.Count > this.m_Script.Options.TailCallOptimizationThreshold && this.m_ValueStack.Count > 1)) && instructionPtr >= 0 && instructionPtr < this.m_RootChunk.Code.Count)
			{
				Instruction instruction = this.m_RootChunk.Code[instructionPtr];
				if (instruction.OpCode == OpCode.Ret && instruction.NumVal == 1)
				{
					CallStackItem callStackItem = this.m_ExecutionStack.Peek(0);
					if (callStackItem.ClrFunction == null && callStackItem.Continuation == null && callStackItem.ErrorHandler == null && callStackItem.ErrorHandlerBeforeUnwind == null && continuation == null && unwindHandler == null && handler == null)
					{
						instructionPtr = this.PerformTCO(instructionPtr, argsCount);
						callStackItemFlags |= CallStackItemFlags.TailCall;
					}
				}
			}
			if (dynValue.Type == DataType.ClrFunction)
			{
				IList<DynValue> args = this.CreateArgsListForFunctionCall(argsCount, 0);
				SourceRef currentSourceRef = this.GetCurrentSourceRef(instructionPtr);
				this.m_ExecutionStack.Push(new CallStackItem
				{
					ClrFunction = dynValue.Callback,
					ReturnAddress = instructionPtr,
					CallingSourceRef = currentSourceRef,
					BasePointer = -1,
					ErrorHandler = handler,
					Continuation = continuation,
					ErrorHandlerBeforeUnwind = unwindHandler,
					Flags = callStackItemFlags
				});
				DynValue item = dynValue.Callback.Invoke(new ScriptExecutionContext(this, dynValue.Callback, currentSourceRef, false), args, thisCall);
				this.m_ValueStack.RemoveLast(argsCount + 1);
				this.m_ValueStack.Push(item);
				this.m_ExecutionStack.Pop();
				return this.Internal_CheckForTailRequests(null, instructionPtr);
			}
			if (dynValue.Type == DataType.Function)
			{
				this.m_ValueStack.Push(DynValue.NewNumber((double)argsCount));
				this.m_ExecutionStack.Push(new CallStackItem
				{
					BasePointer = this.m_ValueStack.Count,
					ReturnAddress = instructionPtr,
					Debug_EntryPoint = dynValue.Function.EntryPointByteCodeLocation,
					CallingSourceRef = this.GetCurrentSourceRef(instructionPtr),
					ClosureScope = dynValue.Function.ClosureContext,
					ErrorHandler = handler,
					Continuation = continuation,
					ErrorHandlerBeforeUnwind = unwindHandler,
					Flags = callStackItemFlags
				});
				return dynValue.Function.EntryPointByteCodeLocation;
			}
			DynValue metamethod = this.GetMetamethod(dynValue, "__call");
			if (metamethod != null && metamethod.IsNotNil())
			{
				DynValue[] array = new DynValue[argsCount + 1];
				for (int i = 0; i < argsCount + 1; i++)
				{
					array[i] = this.m_ValueStack.Pop();
				}
				this.m_ValueStack.Push(metamethod);
				for (int j = argsCount; j >= 0; j--)
				{
					this.m_ValueStack.Push(array[j]);
				}
				return this.Internal_ExecCall(argsCount + 1, instructionPtr, handler, continuation, false, null, null);
			}
			throw ScriptRuntimeException.AttemptToCallNonFunc(dynValue.Type, debugText);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x00123F50 File Offset: 0x00122150
		private int PerformTCO(int instructionPtr, int argsCount)
		{
			DynValue[] array = new DynValue[argsCount + 1];
			for (int i = 0; i <= argsCount; i++)
			{
				array[i] = this.m_ValueStack.Pop();
			}
			int returnAddress = this.PopToBasePointer().ReturnAddress;
			int num = (int)this.m_ValueStack.Pop().Number;
			this.m_ValueStack.RemoveLast(num + 1);
			for (int j = argsCount; j >= 0; j--)
			{
				this.m_ValueStack.Push(array[j]);
			}
			return returnAddress;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x00123FD0 File Offset: 0x001221D0
		private int ExecRet(Instruction i)
		{
			CallStackItem callStackItem;
			int num;
			if (i.NumVal == 0)
			{
				callStackItem = this.PopToBasePointer();
				num = callStackItem.ReturnAddress;
				int num2 = (int)this.m_ValueStack.Pop().Number;
				this.m_ValueStack.RemoveLast(num2 + 1);
				this.m_ValueStack.Push(DynValue.Void);
			}
			else
			{
				if (i.NumVal != 1)
				{
					throw new InternalErrorException("RET supports only 0 and 1 ret val scenarios");
				}
				DynValue item = this.m_ValueStack.Pop();
				callStackItem = this.PopToBasePointer();
				num = callStackItem.ReturnAddress;
				int num3 = (int)this.m_ValueStack.Pop().Number;
				this.m_ValueStack.RemoveLast(num3 + 1);
				this.m_ValueStack.Push(item);
				num = this.Internal_CheckForTailRequests(i, num);
			}
			if (callStackItem.Continuation != null)
			{
				this.m_ValueStack.Push(callStackItem.Continuation.Invoke(new ScriptExecutionContext(this, callStackItem.Continuation, i.SourceCodeRef, false), new DynValue[]
				{
					this.m_ValueStack.Pop()
				}, false));
			}
			return num;
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x001240D8 File Offset: 0x001222D8
		private int Internal_CheckForTailRequests(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Peek(0);
			if (dynValue.Type == DataType.TailCallRequest)
			{
				this.m_ValueStack.Pop();
				TailCallData tailCallData = dynValue.TailCallData;
				this.m_ValueStack.Push(tailCallData.Function);
				for (int j = 0; j < tailCallData.Args.Length; j++)
				{
					this.m_ValueStack.Push(tailCallData.Args[j]);
				}
				return this.Internal_ExecCall(tailCallData.Args.Length, instructionPtr, tailCallData.ErrorHandler, tailCallData.Continuation, false, null, tailCallData.ErrorHandlerBeforeUnwind);
			}
			if (dynValue.Type == DataType.YieldRequest)
			{
				this.m_SavedInstructionPtr = instructionPtr;
				return -99;
			}
			return instructionPtr;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x00025FB6 File Offset: 0x000241B6
		private int JumpBool(Instruction i, bool expectedValueForJump, int instructionPtr)
		{
			if (this.m_ValueStack.Pop().ToScalar().CastToBool() == expectedValueForJump)
			{
				return i.NumVal;
			}
			return instructionPtr;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x00124184 File Offset: 0x00122384
		private int ExecShortCircuitingOperator(Instruction i, int instructionPtr)
		{
			bool flag = i.OpCode == OpCode.JtOrPop;
			if (this.m_ValueStack.Peek(0).ToScalar().CastToBool() == flag)
			{
				return i.NumVal;
			}
			this.m_ValueStack.Pop();
			return instructionPtr;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x001241CC File Offset: 0x001223CC
		private int ExecAdd(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			double? num2 = dynValue2.CastToNumber();
			if (num2 != null && num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(num2.Value + num.Value));
				return instructionPtr;
			}
			int num3 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__add", instructionPtr, null);
			if (num3 >= 0)
			{
				return num3;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue2, dynValue);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x00124260 File Offset: 0x00122460
		private int ExecSub(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			double? num2 = dynValue2.CastToNumber();
			if (num2 != null && num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(num2.Value - num.Value));
				return instructionPtr;
			}
			int num3 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__sub", instructionPtr, null);
			if (num3 >= 0)
			{
				return num3;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue2, dynValue);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x001242F4 File Offset: 0x001224F4
		private int ExecMul(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			double? num2 = dynValue2.CastToNumber();
			if (num2 != null && num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(num2.Value * num.Value));
				return instructionPtr;
			}
			int num3 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__mul", instructionPtr, null);
			if (num3 >= 0)
			{
				return num3;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue2, dynValue);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x00124388 File Offset: 0x00122588
		private int ExecMod(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			double? num2 = dynValue2.CastToNumber();
			if (num2 != null && num != null)
			{
				double num3 = Math.IEEERemainder(num2.Value, num.Value);
				if (num3 < 0.0)
				{
					num3 += num.Value;
				}
				this.m_ValueStack.Push(DynValue.NewNumber(num3));
				return instructionPtr;
			}
			int num4 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__mod", instructionPtr, null);
			if (num4 >= 0)
			{
				return num4;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue2, dynValue);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x0012443C File Offset: 0x0012263C
		private int ExecDiv(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			double? num2 = dynValue2.CastToNumber();
			if (num2 != null && num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(num2.Value / num.Value));
				return instructionPtr;
			}
			int num3 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__div", instructionPtr, null);
			if (num3 >= 0)
			{
				return num3;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue2, dynValue);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x001244D0 File Offset: 0x001226D0
		private int ExecPower(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			double? num2 = dynValue2.CastToNumber();
			if (num2 != null && num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(Math.Pow(num2.Value, num.Value)));
				return instructionPtr;
			}
			int num3 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__pow", instructionPtr, null);
			if (num3 >= 0)
			{
				return num3;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue2, dynValue);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x00124568 File Offset: 0x00122768
		private int ExecNeg(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			double? num = dynValue.CastToNumber();
			if (num != null)
			{
				this.m_ValueStack.Push(DynValue.NewNumber(-num.Value));
				return instructionPtr;
			}
			int num2 = this.Internal_InvokeUnaryMetaMethod(dynValue, "__unm", instructionPtr);
			if (num2 >= 0)
			{
				return num2;
			}
			throw ScriptRuntimeException.ArithmeticOnNonNumber(dynValue, null);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x001245CC File Offset: 0x001227CC
		private int ExecEq(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			if (dynValue == dynValue2)
			{
				this.m_ValueStack.Push(DynValue.True);
				return instructionPtr;
			}
			if (dynValue2.Type == DataType.UserData || dynValue.Type == DataType.UserData)
			{
				int num = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__eq", instructionPtr, null);
				if (num >= 0)
				{
					return num;
				}
			}
			if (dynValue.Type != dynValue2.Type)
			{
				if ((dynValue2.Type == DataType.Nil && dynValue.Type == DataType.Void) || (dynValue2.Type == DataType.Void && dynValue.Type == DataType.Nil))
				{
					this.m_ValueStack.Push(DynValue.True);
				}
				else
				{
					this.m_ValueStack.Push(DynValue.False);
				}
				return instructionPtr;
			}
			if (dynValue2.Type == DataType.Table && this.GetMetatable(dynValue2) != null && this.GetMetatable(dynValue2) == this.GetMetatable(dynValue))
			{
				int num2 = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__eq", instructionPtr, null);
				if (num2 >= 0)
				{
					return num2;
				}
			}
			this.m_ValueStack.Push(DynValue.NewBoolean(dynValue.Equals(dynValue2)));
			return instructionPtr;
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x001246E4 File Offset: 0x001228E4
		private int ExecLess(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			if (dynValue2.Type == DataType.Number && dynValue.Type == DataType.Number)
			{
				this.m_ValueStack.Push(DynValue.NewBoolean(dynValue2.Number < dynValue.Number));
			}
			else if (dynValue2.Type == DataType.String && dynValue.Type == DataType.String)
			{
				this.m_ValueStack.Push(DynValue.NewBoolean(dynValue2.String.CompareTo(dynValue.String) < 0));
			}
			else
			{
				int num = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__lt", instructionPtr, null);
				if (num < 0)
				{
					throw ScriptRuntimeException.CompareInvalidType(dynValue2, dynValue);
				}
				return num;
			}
			return instructionPtr;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x001247A0 File Offset: 0x001229A0
		private int ExecLessEq(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			if (dynValue2.Type == DataType.Number && dynValue.Type == DataType.Number)
			{
				this.m_ValueStack.Push(DynValue.False);
				this.m_ValueStack.Push(DynValue.NewBoolean(dynValue2.Number <= dynValue.Number));
			}
			else if (dynValue2.Type == DataType.String && dynValue.Type == DataType.String)
			{
				this.m_ValueStack.Push(DynValue.False);
				this.m_ValueStack.Push(DynValue.NewBoolean(dynValue2.String.CompareTo(dynValue.String) <= 0));
			}
			else
			{
				int num = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__le", instructionPtr, DynValue.False);
				if (num >= 0)
				{
					return num;
				}
				num = this.Internal_InvokeBinaryMetaMethod(dynValue, dynValue2, "__lt", instructionPtr, DynValue.True);
				if (num < 0)
				{
					throw ScriptRuntimeException.CompareInvalidType(dynValue2, dynValue);
				}
				return num;
			}
			return instructionPtr;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x001248A4 File Offset: 0x00122AA4
		private int ExecLen(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			if (dynValue.Type == DataType.String)
			{
				this.m_ValueStack.Push(DynValue.NewNumber((double)dynValue.String.Length));
			}
			else
			{
				int num = this.Internal_InvokeUnaryMetaMethod(dynValue, "__len", instructionPtr);
				if (num >= 0)
				{
					return num;
				}
				if (dynValue.Type != DataType.Table)
				{
					throw ScriptRuntimeException.LenOnInvalidType(dynValue);
				}
				this.m_ValueStack.Push(DynValue.NewNumber((double)dynValue.Table.Length));
			}
			return instructionPtr;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x00124930 File Offset: 0x00122B30
		private int ExecConcat(Instruction i, int instructionPtr)
		{
			DynValue dynValue = this.m_ValueStack.Pop().ToScalar();
			DynValue dynValue2 = this.m_ValueStack.Pop().ToScalar();
			string text = dynValue.CastToString();
			string text2 = dynValue2.CastToString();
			if (text != null && text2 != null)
			{
				this.m_ValueStack.Push(DynValue.NewString(text2 + text));
				return instructionPtr;
			}
			int num = this.Internal_InvokeBinaryMetaMethod(dynValue2, dynValue, "__concat", instructionPtr, null);
			if (num >= 0)
			{
				return num;
			}
			throw ScriptRuntimeException.ConcatOnNonString(dynValue2, dynValue);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x001249B0 File Offset: 0x00122BB0
		private void ExecTblInitI(Instruction i)
		{
			DynValue val = this.m_ValueStack.Pop();
			DynValue dynValue = this.m_ValueStack.Peek(0);
			if (dynValue.Type != DataType.Table)
			{
				throw new InternalErrorException("Unexpected type in table ctor : {0}", new object[]
				{
					dynValue
				});
			}
			dynValue.Table.InitNextArrayKeys(val, i.NumVal != 0);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x00124A0C File Offset: 0x00122C0C
		private void ExecTblInitN(Instruction i)
		{
			DynValue dynValue = this.m_ValueStack.Pop();
			DynValue key = this.m_ValueStack.Pop();
			DynValue dynValue2 = this.m_ValueStack.Peek(0);
			if (dynValue2.Type != DataType.Table)
			{
				throw new InternalErrorException("Unexpected type in table ctor : {0}", new object[]
				{
					dynValue2
				});
			}
			dynValue2.Table.Set(key, dynValue.ToScalar());
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x00124A70 File Offset: 0x00122C70
		private int ExecIndexSet(Instruction i, int instructionPtr)
		{
			int j = 100;
			bool isDirectIndexing = i.OpCode == OpCode.IndexSetN;
			bool flag = i.OpCode == OpCode.IndexSetL;
			DynValue dynValue = i.Value ?? this.m_ValueStack.Pop();
			DynValue dynValue2 = dynValue.ToScalar();
			DynValue dynValue3 = this.m_ValueStack.Pop().ToScalar();
			DynValue storeValue = this.GetStoreValue(i);
			while (j > 0)
			{
				j--;
				DynValue metamethodRaw;
				if (dynValue3.Type == DataType.Table)
				{
					if (!flag && !dynValue3.Table.Get(dynValue2).IsNil())
					{
						dynValue3.Table.Set(dynValue2, storeValue);
						return instructionPtr;
					}
					metamethodRaw = this.GetMetamethodRaw(dynValue3, "__newindex");
					if (metamethodRaw == null || metamethodRaw.IsNil())
					{
						if (flag)
						{
							throw new ScriptRuntimeException("cannot multi-index a table. userdata expected");
						}
						dynValue3.Table.Set(dynValue2, storeValue);
						return instructionPtr;
					}
				}
				else if (dynValue3.Type == DataType.UserData)
				{
					UserData userData = dynValue3.UserData;
					if (!userData.Descriptor.SetIndex(this.GetScript(), userData.Object, dynValue, storeValue, isDirectIndexing))
					{
						throw ScriptRuntimeException.UserDataMissingField(userData.Descriptor.Name, dynValue2.String);
					}
					return instructionPtr;
				}
				else
				{
					metamethodRaw = this.GetMetamethodRaw(dynValue3, "__newindex");
					if (metamethodRaw == null || metamethodRaw.IsNil())
					{
						throw ScriptRuntimeException.IndexType(dynValue3);
					}
				}
				if (metamethodRaw.Type == DataType.Function || metamethodRaw.Type == DataType.ClrFunction)
				{
					if (flag)
					{
						throw new ScriptRuntimeException("cannot multi-index through metamethods. userdata expected");
					}
					this.m_ValueStack.Pop();
					this.m_ValueStack.Push(metamethodRaw);
					this.m_ValueStack.Push(dynValue3);
					this.m_ValueStack.Push(dynValue2);
					this.m_ValueStack.Push(storeValue);
					return this.Internal_ExecCall(3, instructionPtr, null, null, false, null, null);
				}
				else
				{
					dynValue3 = metamethodRaw;
				}
			}
			throw ScriptRuntimeException.LoopInNewIndex();
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x00124C48 File Offset: 0x00122E48
		private int ExecIndex(Instruction i, int instructionPtr)
		{
			int j = 100;
			bool isDirectIndexing = i.OpCode == OpCode.IndexN;
			bool flag = i.OpCode == OpCode.IndexL;
			DynValue dynValue = i.Value ?? this.m_ValueStack.Pop();
			DynValue dynValue2 = dynValue.ToScalar();
			DynValue dynValue3 = this.m_ValueStack.Pop().ToScalar();
			while (j > 0)
			{
				j--;
				DynValue metamethodRaw;
				if (dynValue3.Type == DataType.Table)
				{
					if (!flag)
					{
						DynValue dynValue4 = dynValue3.Table.Get(dynValue2);
						if (!dynValue4.IsNil())
						{
							this.m_ValueStack.Push(dynValue4.AsReadOnly());
							return instructionPtr;
						}
					}
					metamethodRaw = this.GetMetamethodRaw(dynValue3, "__index");
					if (metamethodRaw == null || metamethodRaw.IsNil())
					{
						if (flag)
						{
							throw new ScriptRuntimeException("cannot multi-index a table. userdata expected");
						}
						this.m_ValueStack.Push(DynValue.Nil);
						return instructionPtr;
					}
				}
				else if (dynValue3.Type == DataType.UserData)
				{
					UserData userData = dynValue3.UserData;
					DynValue dynValue5 = userData.Descriptor.Index(this.GetScript(), userData.Object, dynValue, isDirectIndexing);
					if (dynValue5 == null)
					{
						throw ScriptRuntimeException.UserDataMissingField(userData.Descriptor.Name, dynValue2.String);
					}
					this.m_ValueStack.Push(dynValue5.AsReadOnly());
					return instructionPtr;
				}
				else
				{
					metamethodRaw = this.GetMetamethodRaw(dynValue3, "__index");
					if (metamethodRaw == null || metamethodRaw.IsNil())
					{
						throw ScriptRuntimeException.IndexType(dynValue3);
					}
				}
				if (metamethodRaw.Type == DataType.Function || metamethodRaw.Type == DataType.ClrFunction)
				{
					if (flag)
					{
						throw new ScriptRuntimeException("cannot multi-index through metamethods. userdata expected");
					}
					this.m_ValueStack.Push(metamethodRaw);
					this.m_ValueStack.Push(dynValue3);
					this.m_ValueStack.Push(dynValue2);
					return this.Internal_ExecCall(2, instructionPtr, null, null, false, null, null);
				}
				else
				{
					dynValue3 = metamethodRaw;
				}
			}
			throw ScriptRuntimeException.LoopInIndex();
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00124E1C File Offset: 0x0012301C
		private void ClearBlockData(Instruction I)
		{
			int numVal = I.NumVal;
			int numVal2 = I.NumVal2;
			DynValue[] localScope = this.m_ExecutionStack.Peek(0).LocalScope;
			if (numVal2 >= 0 && numVal >= 0 && numVal2 >= numVal)
			{
				Array.Clear(localScope, numVal, numVal2 - numVal + 1);
			}
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00124E64 File Offset: 0x00123064
		public DynValue GetGenericSymbol(SymbolRef symref)
		{
			switch (symref.i_Type)
			{
			case SymbolRefType.Local:
				return this.GetTopNonClrFunction().LocalScope[symref.i_Index];
			case SymbolRefType.Upvalue:
				return this.GetTopNonClrFunction().ClosureScope[symref.i_Index];
			case SymbolRefType.Global:
				return this.GetGlobalSymbol(this.GetGenericSymbol(symref.i_Env), symref.i_Name);
			case SymbolRefType.DefaultEnv:
				return DynValue.NewTable(this.GetScript().Globals);
			default:
				throw new InternalErrorException("Unexpected {0} LRef at resolution: {1}", new object[]
				{
					symref.i_Type,
					symref.i_Name
				});
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x00025FD8 File Offset: 0x000241D8
		private DynValue GetGlobalSymbol(DynValue dynValue, string name)
		{
			if (dynValue.Type != DataType.Table)
			{
				throw new InvalidOperationException(string.Format("_ENV is not a table but a {0}", dynValue.Type));
			}
			return dynValue.Table.Get(name);
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x0002600A File Offset: 0x0002420A
		private void SetGlobalSymbol(DynValue dynValue, string name, DynValue value)
		{
			if (dynValue.Type != DataType.Table)
			{
				throw new InvalidOperationException(string.Format("_ENV is not a table but a {0}", dynValue.Type));
			}
			dynValue.Table.Set(name, value ?? DynValue.Nil);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x00124F0C File Offset: 0x0012310C
		public void AssignGenericSymbol(SymbolRef symref, DynValue value)
		{
			switch (symref.i_Type)
			{
			case SymbolRefType.Local:
			{
				CallStackItem topNonClrFunction = this.GetTopNonClrFunction();
				DynValue dynValue = topNonClrFunction.LocalScope[symref.i_Index];
				if (dynValue == null)
				{
					dynValue = (topNonClrFunction.LocalScope[symref.i_Index] = DynValue.NewNil());
				}
				dynValue.Assign(value);
				return;
			}
			case SymbolRefType.Upvalue:
			{
				CallStackItem topNonClrFunction2 = this.GetTopNonClrFunction();
				DynValue dynValue2 = topNonClrFunction2.ClosureScope[symref.i_Index];
				if (dynValue2 == null)
				{
					dynValue2 = (topNonClrFunction2.ClosureScope[symref.i_Index] = DynValue.NewNil());
				}
				dynValue2.Assign(value);
				return;
			}
			case SymbolRefType.Global:
				this.SetGlobalSymbol(this.GetGenericSymbol(symref.i_Env), symref.i_Name, value);
				return;
			case SymbolRefType.DefaultEnv:
				throw new ArgumentException("Can't AssignGenericSymbol on a DefaultEnv symbol");
			default:
				throw new InternalErrorException("Unexpected {0} LRef at resolution: {1}", new object[]
				{
					symref.i_Type,
					symref.i_Name
				});
			}
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00124FFC File Offset: 0x001231FC
		private CallStackItem GetTopNonClrFunction()
		{
			CallStackItem callStackItem = null;
			for (int i = 0; i < this.m_ExecutionStack.Count; i++)
			{
				callStackItem = this.m_ExecutionStack.Peek(i);
				if (callStackItem.ClrFunction == null)
				{
					break;
				}
			}
			return callStackItem;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00125038 File Offset: 0x00123238
		public SymbolRef FindSymbolByName(string name)
		{
			if (this.m_ExecutionStack.Count > 0)
			{
				CallStackItem topNonClrFunction = this.GetTopNonClrFunction();
				if (topNonClrFunction != null)
				{
					if (topNonClrFunction.Debug_Symbols != null)
					{
						for (int i = topNonClrFunction.Debug_Symbols.Length - 1; i >= 0; i--)
						{
							SymbolRef symbolRef = topNonClrFunction.Debug_Symbols[i];
							if (symbolRef.i_Name == name && topNonClrFunction.LocalScope[i] != null)
							{
								return symbolRef;
							}
						}
					}
					ClosureContext closureScope = topNonClrFunction.ClosureScope;
					if (closureScope != null)
					{
						for (int j = 0; j < closureScope.Symbols.Length; j++)
						{
							if (closureScope.Symbols[j] == name)
							{
								return SymbolRef.Upvalue(name, j);
							}
						}
					}
				}
			}
			if (name != "_ENV")
			{
				SymbolRef envSymbol = this.FindSymbolByName("_ENV");
				return SymbolRef.Global(name, envSymbol);
			}
			return SymbolRef.DefaultEnv;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x00125104 File Offset: 0x00123304
		private DynValue[] Internal_AdjustTuple(IList<DynValue> values)
		{
			if (values == null || values.Count == 0)
			{
				return new DynValue[0];
			}
			if (values[values.Count - 1].Type != DataType.Tuple)
			{
				DynValue[] array = new DynValue[values.Count];
				for (int i = 0; i < values.Count; i++)
				{
					array[i] = values[i].ToScalar();
				}
				return array;
			}
			DynValue[] array2 = new DynValue[values.Count - 1 + values[values.Count - 1].Tuple.Length];
			for (int j = 0; j < values.Count - 1; j++)
			{
				array2[j] = values[j].ToScalar();
			}
			for (int k = 0; k < values[values.Count - 1].Tuple.Length; k++)
			{
				array2[values.Count + k - 1] = values[values.Count - 1].Tuple[k];
			}
			if (array2[array2.Length - 1].Type == DataType.Tuple)
			{
				return this.Internal_AdjustTuple(array2);
			}
			return array2;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x00125210 File Offset: 0x00123410
		private int Internal_InvokeUnaryMetaMethod(DynValue op1, string eventName, int instructionPtr)
		{
			DynValue dynValue = null;
			if (op1.Type == DataType.UserData)
			{
				dynValue = op1.UserData.Descriptor.MetaIndex(this.m_Script, op1.UserData.Object, eventName);
			}
			if (dynValue == null)
			{
				Table metatable = this.GetMetatable(op1);
				if (metatable != null)
				{
					DynValue dynValue2 = metatable.RawGet(eventName);
					if (dynValue2 != null && dynValue2.IsNotNil())
					{
						dynValue = dynValue2;
					}
				}
			}
			if (dynValue != null)
			{
				this.m_ValueStack.Push(dynValue);
				this.m_ValueStack.Push(op1);
				return this.Internal_ExecCall(1, instructionPtr, null, null, false, null, null);
			}
			return -1;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x0012529C File Offset: 0x0012349C
		private int Internal_InvokeBinaryMetaMethod(DynValue l, DynValue r, string eventName, int instructionPtr, DynValue extraPush = null)
		{
			DynValue binaryMetamethod = this.GetBinaryMetamethod(l, r, eventName);
			if (binaryMetamethod != null)
			{
				if (extraPush != null)
				{
					this.m_ValueStack.Push(extraPush);
				}
				this.m_ValueStack.Push(binaryMetamethod);
				this.m_ValueStack.Push(l);
				this.m_ValueStack.Push(r);
				return this.Internal_ExecCall(2, instructionPtr, null, null, false, null, null);
			}
			return -1;
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x00125300 File Offset: 0x00123500
		private DynValue[] StackTopToArray(int items, bool pop)
		{
			DynValue[] array = new DynValue[items];
			if (pop)
			{
				for (int i = 0; i < items; i++)
				{
					array[i] = this.m_ValueStack.Pop();
				}
			}
			else
			{
				for (int j = 0; j < items; j++)
				{
					array[j] = this.m_ValueStack[this.m_ValueStack.Count - 1 - j];
				}
			}
			return array;
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x00125360 File Offset: 0x00123560
		private DynValue[] StackTopToArrayReverse(int items, bool pop)
		{
			DynValue[] array = new DynValue[items];
			if (pop)
			{
				for (int i = 0; i < items; i++)
				{
					array[items - 1 - i] = this.m_ValueStack.Pop();
				}
			}
			else
			{
				for (int j = 0; j < items; j++)
				{
					array[items - 1 - j] = this.m_ValueStack[this.m_ValueStack.Count - 1 - j];
				}
			}
			return array;
		}

		// Token: 0x04002F5B RID: 12123
		private ByteCode m_RootChunk;

		// Token: 0x04002F5C RID: 12124
		private FastStack<DynValue> m_ValueStack = new FastStack<DynValue>(131072);

		// Token: 0x04002F5D RID: 12125
		private FastStack<CallStackItem> m_ExecutionStack = new FastStack<CallStackItem>(131072);

		// Token: 0x04002F5E RID: 12126
		private List<Processor> m_CoroutinesStack;

		// Token: 0x04002F5F RID: 12127
		private Table m_GlobalTable;

		// Token: 0x04002F60 RID: 12128
		private Script m_Script;

		// Token: 0x04002F61 RID: 12129
		private Processor m_Parent;

		// Token: 0x04002F62 RID: 12130
		private CoroutineState m_State;

		// Token: 0x04002F63 RID: 12131
		private bool m_CanYield = true;

		// Token: 0x04002F64 RID: 12132
		private int m_SavedInstructionPtr = -1;

		// Token: 0x04002F65 RID: 12133
		private Processor.DebugContext m_Debug;

		// Token: 0x04002F66 RID: 12134
		private int m_OwningThreadID = -1;

		// Token: 0x04002F67 RID: 12135
		private int m_ExecutionNesting;

		// Token: 0x04002F68 RID: 12136
		private const ulong DUMP_CHUNK_MAGIC = 1877195438928383261UL;

		// Token: 0x04002F69 RID: 12137
		private const int DUMP_CHUNK_VERSION = 336;

		// Token: 0x04002F6B RID: 12139
		private const int YIELD_SPECIAL_TRAP = -99;

		// Token: 0x04002F6C RID: 12140
		internal long AutoYieldCounter;

		// Token: 0x04002F6D RID: 12141
		public ProcessorStats stats;

		// Token: 0x020008B4 RID: 2228
		private class DebugContext
		{
			// Token: 0x04002F6E RID: 12142
			public bool DebuggerEnabled = true;

			// Token: 0x04002F6F RID: 12143
			public IDebugger DebuggerAttached;

			// Token: 0x04002F70 RID: 12144
			public DebuggerAction.ActionType DebuggerCurrentAction = DebuggerAction.ActionType.None;

			// Token: 0x04002F71 RID: 12145
			public int DebuggerCurrentActionTarget = -1;

			// Token: 0x04002F72 RID: 12146
			public SourceRef LastHlRef;

			// Token: 0x04002F73 RID: 12147
			public int ExStackDepthAtStep = -1;

			// Token: 0x04002F74 RID: 12148
			public List<SourceRef> BreakPoints = new List<SourceRef>();

			// Token: 0x04002F75 RID: 12149
			public bool LineBasedBreakPoints;
		}
	}
}
