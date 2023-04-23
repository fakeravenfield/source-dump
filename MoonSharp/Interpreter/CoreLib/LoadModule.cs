using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008E0 RID: 2272
	[MoonSharpModule]
	public class LoadModule
	{
		// Token: 0x06003992 RID: 14738 RVA: 0x00128040 File Offset: 0x00126240
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			DynValue dynValue = globalTable.Get("package");
			if (dynValue.IsNil())
			{
				dynValue = DynValue.NewTable(globalTable.OwnerScript);
				globalTable["package"] = dynValue;
			}
			else if (dynValue.Type != DataType.Table)
			{
				throw new InternalErrorException("'package' global variable was found and it is not a table");
			}
			string str = Path.DirectorySeparatorChar.ToString() + "\n;\n?\n!\n-\n";
			dynValue.Table.Set("config", DynValue.NewString(str));
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x00026DBB File Offset: 0x00024FBB
		[MoonSharpModuleMethod]
		public static DynValue load(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.load_impl(executionContext, args, null);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x00026DC5 File Offset: 0x00024FC5
		[MoonSharpModuleMethod]
		public static DynValue loadsafe(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.load_impl(executionContext, args, LoadModule.GetSafeDefaultEnv(executionContext));
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x001280C0 File Offset: 0x001262C0
		public static DynValue load_impl(ScriptExecutionContext executionContext, CallbackArguments args, Table defaultEnv)
		{
			DynValue result;
			try
			{
				Script script = executionContext.GetScript();
				DynValue dynValue = args[0];
				string text = "";
				if (dynValue.Type == DataType.Function)
				{
					DynValue dynValue2;
					for (;;)
					{
						dynValue2 = executionContext.GetScript().Call(dynValue);
						if (dynValue2.Type != DataType.String || dynValue2.String.Length <= 0)
						{
							break;
						}
						text += dynValue2.String;
					}
					if (!dynValue2.IsNil())
					{
						return DynValue.NewTuple(new DynValue[]
						{
							DynValue.Nil,
							DynValue.NewString("reader function must return a string")
						});
					}
				}
				else if (dynValue.Type == DataType.String)
				{
					text = dynValue.String;
				}
				else
				{
					args.AsType(0, "load", DataType.Function, false);
				}
				DynValue dynValue3 = args.AsType(1, "load", DataType.String, true);
				DynValue dynValue4 = args.AsType(3, "load", DataType.Table, true);
				result = script.LoadString(text, (!dynValue4.IsNil()) ? dynValue4.Table : defaultEnv, (!dynValue3.IsNil()) ? dynValue3.String : "=(load)");
			}
			catch (SyntaxErrorException ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.DecoratedMessage ?? ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x00026DD4 File Offset: 0x00024FD4
		[MoonSharpModuleMethod]
		public static DynValue loadfile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.loadfile_impl(executionContext, args, null);
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x00026DDE File Offset: 0x00024FDE
		[MoonSharpModuleMethod]
		public static DynValue loadfilesafe(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.loadfile_impl(executionContext, args, LoadModule.GetSafeDefaultEnv(executionContext));
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0012820C File Offset: 0x0012640C
		private static DynValue loadfile_impl(ScriptExecutionContext executionContext, CallbackArguments args, Table defaultEnv)
		{
			DynValue result;
			try
			{
				Script script = executionContext.GetScript();
				DynValue dynValue = args.AsType(0, "loadfile", DataType.String, false);
				DynValue dynValue2 = args.AsType(2, "loadfile", DataType.Table, true);
				result = script.LoadFile(dynValue.String, dynValue2.IsNil() ? defaultEnv : dynValue2.Table, null);
			}
			catch (SyntaxErrorException ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.DecoratedMessage ?? ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x00026DED File Offset: 0x00024FED
		private static Table GetSafeDefaultEnv(ScriptExecutionContext executionContext)
		{
			Table currentGlobalEnv = executionContext.CurrentGlobalEnv;
			if (currentGlobalEnv == null)
			{
				throw new ScriptRuntimeException("current environment cannot be backtracked.");
			}
			return currentGlobalEnv;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x001282A0 File Offset: 0x001264A0
		[MoonSharpModuleMethod]
		public static DynValue dofile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				Script script = executionContext.GetScript();
				DynValue dynValue = args.AsType(0, "dofile", DataType.String, false);
				result = DynValue.NewTailCallReq(script.LoadFile(dynValue.String, null, null), Array.Empty<DynValue>());
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x001282F4 File Offset: 0x001264F4
		[MoonSharpModuleMethod]
		public static DynValue __require_clr_impl(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Script script = executionContext.GetScript();
			DynValue dynValue = args.AsType(0, "__require_clr_impl", DataType.String, false);
			return script.RequireModule(dynValue.String, null);
		}

		// Token: 0x04002FF6 RID: 12278
		[MoonSharpModuleMethod]
		public const string require = "\nfunction(modulename)\n\tif (package == nil) then package = { }; end\n\tif (package.loaded == nil) then package.loaded = { }; end\n\n\tlocal m = package.loaded[modulename];\n\n\tif (m ~= nil) then\n\t\treturn m;\n\tend\n\n\tlocal func = __require_clr_impl(modulename);\n\n\tlocal res = func(modulename);\n\n\tif (res == nil) then\n\t\tres = true;\n\tend\n\n\tpackage.loaded[modulename] = res;\n\n\treturn res;\nend";
	}
}
