using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lua;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.IO;
using MoonSharp.Interpreter.Platforms;
using MoonSharp.Interpreter.Tree.Expressions;
using MoonSharp.Interpreter.Tree.Fast_Interface;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007E0 RID: 2016
	public class Script : IScriptPrivateResource
	{
		// Token: 0x06003217 RID: 12823 RVA: 0x00110180 File Offset: 0x0010E380
		static Script()
		{
			Script.DefaultOptions = new ScriptOptions
			{
				DebugPrint = delegate(string s)
				{
					Script.GlobalOptions.Platform.DefaultPrint(s);
				},
				DebugInput = ((string s) => Script.GlobalOptions.Platform.DefaultInput(s)),
				CheckThreadAccess = true,
				ScriptLoader = PlatformAutoDetector.GetDefaultScriptLoader(),
				TailCallOptimizationThreshold = 65536
			};
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x00022AB2 File Offset: 0x00020CB2
		public Script() : this(CoreModules.Preset_Default)
		{
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x001101EC File Offset: 0x0010E3EC
		public Script(CoreModules coreModules)
		{
			this.Options = new ScriptOptions(Script.DefaultOptions);
			this.PerformanceStats = new PerformanceStatistics();
			this.Registry = new Table(this);
			this.m_ByteCode = new ByteCode(this);
			this.m_MainProcessor = new Processor(this, this.m_GlobalTable, this.m_ByteCode);
			this.m_GlobalTable = new Table(this).RegisterCoreModules(coreModules);
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x00022ABF File Offset: 0x00020CBF
		// (set) Token: 0x0600321B RID: 12827 RVA: 0x00022AC6 File Offset: 0x00020CC6
		public static ScriptOptions DefaultOptions { get; private set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x00022ACE File Offset: 0x00020CCE
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x00022AD6 File Offset: 0x00020CD6
		public ScriptOptions Options { get; private set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x00022ADF File Offset: 0x00020CDF
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x00022AE6 File Offset: 0x00020CE6
		public static ScriptGlobalOptions GlobalOptions { get; private set; } = new ScriptGlobalOptions();

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x00022AEE File Offset: 0x00020CEE
		// (set) Token: 0x06003221 RID: 12833 RVA: 0x00022AF6 File Offset: 0x00020CF6
		public PerformanceStatistics PerformanceStats { get; private set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x00022AFF File Offset: 0x00020CFF
		public Table Globals
		{
			get
			{
				return this.m_GlobalTable;
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x00110274 File Offset: 0x0010E474
		public DynValue LoadFunction(string code, Table globalTable = null, string funcFriendlyName = null)
		{
			this.CheckScriptOwnership(globalTable);
			SourceCode sourceCode = new SourceCode(string.Format("libfunc_{0}", funcFriendlyName ?? this.m_Sources.Count.ToString()), code, this.m_Sources.Count, this);
			this.m_Sources.Add(sourceCode);
			int address = Loader_Fast.LoadFunction(this, sourceCode, this.m_ByteCode, globalTable != null || this.m_GlobalTable != null);
			this.SignalSourceCodeChange(sourceCode);
			this.SignalByteCodeChange();
			return this.MakeClosure(address, globalTable ?? this.m_GlobalTable);
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x00110308 File Offset: 0x0010E508
		private void SignalByteCodeChange()
		{
			if (this.m_Debugger != null)
			{
				this.m_Debugger.SetByteCode((from s in this.m_ByteCode.Code
				select s.ToString()).ToArray<string>());
			}
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x00022B07 File Offset: 0x00020D07
		private void SignalSourceCodeChange(SourceCode source)
		{
			if (this.m_Debugger != null)
			{
				this.m_Debugger.SetSourceCode(source);
			}
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x0011035C File Offset: 0x0010E55C
		public DynValue LoadString(string code, Table globalTable = null, string codeFriendlyName = null)
		{
			this.CheckScriptOwnership(globalTable);
			if (code.StartsWith("MoonSharp_dump_b64::"))
			{
				code = code.Substring("MoonSharp_dump_b64::".Length);
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(code)))
				{
					return this.LoadStream(memoryStream, globalTable, codeFriendlyName);
				}
			}
			string text = string.Format("{0}", codeFriendlyName ?? ("chunk_" + this.m_Sources.Count.ToString()));
			SourceCode sourceCode = new SourceCode(codeFriendlyName ?? text, code, this.m_Sources.Count, this);
			this.m_Sources.Add(sourceCode);
			int address = Loader_Fast.LoadChunk(this, sourceCode, this.m_ByteCode);
			this.SignalSourceCodeChange(sourceCode);
			this.SignalByteCodeChange();
			return this.MakeClosure(address, globalTable ?? this.m_GlobalTable);
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x00110448 File Offset: 0x0010E648
		public DynValue LoadStream(Stream stream, Table globalTable = null, string codeFriendlyName = null)
		{
			this.CheckScriptOwnership(globalTable);
			Stream stream2 = new UndisposableStream(stream);
			if (!Processor.IsDumpStream(stream2))
			{
				using (StreamReader streamReader = new StreamReader(stream2))
				{
					string code = streamReader.ReadToEnd();
					return this.LoadString(code, globalTable, codeFriendlyName);
				}
			}
			string text = string.Format("{0}", codeFriendlyName ?? ("dump_" + this.m_Sources.Count.ToString()));
			SourceCode sourceCode = new SourceCode(codeFriendlyName ?? text, string.Format("-- This script was decoded from a binary dump - dump_{0}", this.m_Sources.Count), this.m_Sources.Count, this);
			this.m_Sources.Add(sourceCode);
			bool flag;
			int address = this.m_MainProcessor.Undump(stream2, this.m_Sources.Count - 1, globalTable ?? this.m_GlobalTable, out flag);
			this.SignalSourceCodeChange(sourceCode);
			this.SignalByteCodeChange();
			if (flag)
			{
				return this.MakeClosure(address, globalTable ?? this.m_GlobalTable);
			}
			return this.MakeClosure(address, null);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x0011056C File Offset: 0x0010E76C
		public void Dump(DynValue function, Stream stream)
		{
			this.CheckScriptOwnership(function);
			if (function.Type != DataType.Function)
			{
				throw new ArgumentException("function arg is not a function!");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("stream is readonly!");
			}
			Closure.UpvaluesType upvaluesType = function.Function.GetUpvaluesType();
			if (upvaluesType == Closure.UpvaluesType.Closure)
			{
				throw new ArgumentException("function arg has upvalues other than _ENV");
			}
			UndisposableStream stream2 = new UndisposableStream(stream);
			this.m_MainProcessor.Dump(stream2, function.Function.EntryPointByteCodeLocation, upvaluesType == Closure.UpvaluesType.Environment);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x001105E8 File Offset: 0x0010E7E8
		public DynValue LoadFile(string filename, Table globalContext = null, string friendlyFilename = null)
		{
			this.CheckScriptOwnership(globalContext);
			filename = this.Options.ScriptLoader.ResolveFileName(filename, globalContext ?? this.m_GlobalTable);
			object obj = this.Options.ScriptLoader.LoadFile(filename, globalContext ?? this.m_GlobalTable);
			if (obj is string)
			{
				return this.LoadString((string)obj, globalContext, friendlyFilename ?? filename);
			}
			if (obj is byte[])
			{
				using (MemoryStream memoryStream = new MemoryStream((byte[])obj))
				{
					return this.LoadStream(memoryStream, globalContext, friendlyFilename ?? filename);
				}
			}
			if (obj is Stream)
			{
				try
				{
					return this.LoadStream((Stream)obj, globalContext, friendlyFilename ?? filename);
				}
				finally
				{
					((Stream)obj).Dispose();
				}
			}
			if (obj == null)
			{
				throw new InvalidCastException("Unexpected null from IScriptLoader.LoadFile");
			}
			throw new InvalidCastException(string.Format("Unsupported return type from IScriptLoader.LoadFile : {0}", obj.GetType()));
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x001106F0 File Offset: 0x0010E8F0
		public DynValue DoString(string code, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadString(code, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x00110710 File Offset: 0x0010E910
		public DynValue DoStream(Stream stream, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadStream(stream, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00110730 File Offset: 0x0010E930
		public DynValue DoFile(string filename, Table globalContext = null, string codeFriendlyName = null)
		{
			DynValue function = this.LoadFile(filename, globalContext, codeFriendlyName);
			return this.Call(function);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x00022B1D File Offset: 0x00020D1D
		public static DynValue RunFile(string filename)
		{
			return new Script().DoFile(filename, null, null);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00022B2C File Offset: 0x00020D2C
		public static DynValue RunString(string code)
		{
			return new Script().DoString(code, null, null);
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x00110750 File Offset: 0x0010E950
		private DynValue MakeClosure(int address, Table envTable = null)
		{
			this.CheckScriptOwnership(envTable);
			Closure function;
			if (envTable == null)
			{
				Instruction instruction = this.m_MainProcessor.FindMeta(ref address);
				if (instruction != null && instruction.NumVal2 == 0)
				{
					function = new Closure(this, address, new SymbolRef[]
					{
						SymbolRef.Upvalue("_ENV", 0)
					}, new DynValue[]
					{
						instruction.Value
					});
				}
				else
				{
					function = new Closure(this, address, new SymbolRef[0], new DynValue[0]);
				}
			}
			else
			{
				SymbolRef[] symbols = new SymbolRef[]
				{
					new SymbolRef
					{
						i_Env = null,
						i_Index = 0,
						i_Name = "_ENV",
						i_Type = SymbolRefType.DefaultEnv
					}
				};
				DynValue[] resolvedLocals = new DynValue[]
				{
					DynValue.NewTable(envTable)
				};
				function = new Closure(this, address, symbols, resolvedLocals);
			}
			return DynValue.NewClosure(function);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00022B3B File Offset: 0x00020D3B
		public DynValue Call(DynValue function)
		{
			return this.Call(function, new DynValue[0]);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x00110814 File Offset: 0x0010EA14
		public DynValue Call(DynValue function, params DynValue[] args)
		{
			this.CheckScriptOwnership(function);
			this.CheckScriptOwnership(args);
			if (function.Type != DataType.Function && function.Type != DataType.ClrFunction)
			{
				DynValue metamethod = this.m_MainProcessor.GetMetamethod(function, "__call");
				if (metamethod == null)
				{
					throw new ArgumentException("function is not a function and has no __call metamethod.");
				}
				DynValue[] array = new DynValue[args.Length + 1];
				array[0] = function;
				for (int i = 0; i < args.Length; i++)
				{
					array[i + 1] = args[i];
				}
				function = metamethod;
				args = array;
			}
			else if (function.Type == DataType.ClrFunction)
			{
				return function.Callback.ClrCallback(this.CreateDynamicExecutionContext(function.Callback), new CallbackArguments(args, false));
			}
			return this.m_MainProcessor.Call(function, args);
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x001108CC File Offset: 0x0010EACC
		public DynValue Call(DynValue function, params object[] args)
		{
			DynValue[] array = new DynValue[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = DynValue.FromObject(this, args[i]);
			}
			return this.Call(function, array);
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00022B4A File Offset: 0x00020D4A
		public DynValue Call(object function)
		{
			return this.Call(DynValue.FromObject(this, function));
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x00022B59 File Offset: 0x00020D59
		public DynValue Call(object function, params object[] args)
		{
			return this.Call(DynValue.FromObject(this, function), args);
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x00110904 File Offset: 0x0010EB04
		public DynValue CreateCoroutine(DynValue function)
		{
			this.CheckScriptOwnership(function);
			if (function.Type == DataType.Function)
			{
				return this.m_MainProcessor.Coroutine_Create(function.Function);
			}
			if (function.Type == DataType.ClrFunction)
			{
				return DynValue.NewCoroutine(new Coroutine(function.Callback));
			}
			throw new ArgumentException("function is not of DataType.Function or DataType.ClrFunction");
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x00022B69 File Offset: 0x00020D69
		public DynValue CreateCoroutine(object function)
		{
			return this.CreateCoroutine(DynValue.FromObject(this, function));
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x00022B78 File Offset: 0x00020D78
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x00022B85 File Offset: 0x00020D85
		public bool DebuggerEnabled
		{
			get
			{
				return this.m_MainProcessor.DebuggerEnabled;
			}
			set
			{
				this.m_MainProcessor.DebuggerEnabled = value;
			}
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00110958 File Offset: 0x0010EB58
		public void AttachDebugger(IDebugger debugger)
		{
			this.DebuggerEnabled = true;
			this.m_Debugger = debugger;
			this.m_MainProcessor.AttachDebugger(debugger);
			foreach (SourceCode source in this.m_Sources)
			{
				this.SignalSourceCodeChange(source);
			}
			this.SignalByteCodeChange();
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x00022B93 File Offset: 0x00020D93
		public SourceCode GetSourceCode(int sourceCodeID)
		{
			return this.m_Sources[sourceCodeID];
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x00022BA1 File Offset: 0x00020DA1
		public int SourceCodeCount
		{
			get
			{
				return this.m_Sources.Count;
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x001109CC File Offset: 0x0010EBCC
		public DynValue RequireModule(string modname, Table globalContext = null)
		{
			this.CheckScriptOwnership(globalContext);
			Table globalContext2 = globalContext ?? this.m_GlobalTable;
			string text = this.Options.ScriptLoader.ResolveModuleName(modname, globalContext2);
			if (text == null)
			{
				throw new ScriptRuntimeException("module '{0}' not found", new object[]
				{
					modname
				});
			}
			return this.LoadFile(text, globalContext, text);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x00110A20 File Offset: 0x0010EC20
		public Table GetTypeMetatable(DataType type)
		{
			if (type >= DataType.Nil && type < (DataType)this.m_TypeMetatables.Length)
			{
				return this.m_TypeMetatables[(int)type];
			}
			return null;
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x00110A48 File Offset: 0x0010EC48
		public void SetTypeMetatable(DataType type, Table metatable)
		{
			this.CheckScriptOwnership(metatable);
			int num = (int)type;
			if (num >= 0 && num < this.m_TypeMetatables.Length)
			{
				this.m_TypeMetatables[num] = metatable;
				return;
			}
			throw new ArgumentException("Specified type not supported : " + type.ToString());
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x00022BAE File Offset: 0x00020DAE
		public static void WarmUp()
		{
			new Script(CoreModules.Basic).LoadString("return 1;", null, null);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x00110A94 File Offset: 0x0010EC94
		public DynamicExpression CreateDynamicExpression(string code)
		{
			DynamicExprExpression expr = Loader_Fast.LoadDynamicExpr(this, new SourceCode("__dynamic", code, -1, this));
			return new DynamicExpression(this, code, expr);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public DynamicExpression CreateConstantDynamicExpression(string code, DynValue constant)
		{
			this.CheckScriptOwnership(constant);
			return new DynamicExpression(this, code, constant);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x00022BD5 File Offset: 0x00020DD5
		internal ScriptExecutionContext CreateDynamicExecutionContext(CallbackFunction func = null)
		{
			return new ScriptExecutionContext(this.m_MainProcessor, func, null, true);
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x00022BE5 File Offset: 0x00020DE5
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x00022BED File Offset: 0x00020DED
		public Table Registry { get; private set; }

		// Token: 0x06003245 RID: 12869 RVA: 0x00110AC0 File Offset: 0x0010ECC0
		public static string GetBanner(string subproduct = null)
		{
			subproduct = ((subproduct != null) ? (subproduct + " ") : "");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("MoonSharp {0}{1} [{2}]", subproduct, "2.0.0.0", Script.GlobalOptions.Platform.GetPlatformName()));
			stringBuilder.AppendLine("Copyright (C) 2014-2016 Marco Mastropaolo");
			stringBuilder.AppendLine("http://www.moonsharp.org");
			return stringBuilder.ToString();
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x00008D0C File Offset: 0x00006F0C
		Script IScriptPrivateResource.OwnerScript
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x00022BF6 File Offset: 0x00020DF6
		public ProcessorStats GetProcessorStats()
		{
			return this.m_MainProcessor.GetProcessorStats();
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x00022C03 File Offset: 0x00020E03
		public void ResetProcessorStats()
		{
			this.m_MainProcessor.stats.ResetLog();
		}

		// Token: 0x04002C75 RID: 11381
		public const string VERSION = "2.0.0.0";

		// Token: 0x04002C76 RID: 11382
		public const string LUA_VERSION = "5.2";

		// Token: 0x04002C77 RID: 11383
		private Processor m_MainProcessor;

		// Token: 0x04002C78 RID: 11384
		private ByteCode m_ByteCode;

		// Token: 0x04002C79 RID: 11385
		private List<SourceCode> m_Sources = new List<SourceCode>();

		// Token: 0x04002C7A RID: 11386
		private Table m_GlobalTable;

		// Token: 0x04002C7B RID: 11387
		private IDebugger m_Debugger;

		// Token: 0x04002C7C RID: 11388
		private Table[] m_TypeMetatables = new Table[6];
	}
}
