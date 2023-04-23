using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008E4 RID: 2276
	[MoonSharpModule(Namespace = "os")]
	public class OsSystemModule
	{
		// Token: 0x060039E0 RID: 14816 RVA: 0x00128840 File Offset: 0x00126A40
		[MoonSharpModuleMethod]
		public static DynValue execute(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "execute", DataType.String, true);
			if (dynValue.IsNil())
			{
				return DynValue.NewBoolean(true);
			}
			DynValue result;
			try
			{
				int num = Script.GlobalOptions.Platform.OS_Execute(dynValue.String);
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString("exit"),
					DynValue.NewNumber((double)num)
				});
			}
			catch (Exception)
			{
				result = DynValue.Nil;
			}
			return result;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x001288CC File Offset: 0x00126ACC
		[MoonSharpModuleMethod]
		public static DynValue exit(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "exit", DataType.Number, true);
			int exitCode = 0;
			if (dynValue.IsNotNil())
			{
				exitCode = (int)dynValue.Number;
			}
			Script.GlobalOptions.Platform.OS_ExitFast(exitCode);
			throw new InvalidOperationException("Unreachable code.. reached.");
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x00128914 File Offset: 0x00126B14
		[MoonSharpModuleMethod]
		public static DynValue getenv(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "getenv", DataType.String, false);
			string environmentVariable = Script.GlobalOptions.Platform.GetEnvironmentVariable(dynValue.String);
			if (environmentVariable == null)
			{
				return DynValue.Nil;
			}
			return DynValue.NewString(environmentVariable);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x00128958 File Offset: 0x00126B58
		[MoonSharpModuleMethod]
		public static DynValue remove(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "remove", DataType.String, false).String;
			DynValue result;
			try
			{
				if (Script.GlobalOptions.Platform.OS_FileExists(@string))
				{
					Script.GlobalOptions.Platform.OS_FileDelete(@string);
					result = DynValue.True;
				}
				else
				{
					result = DynValue.NewTuple(new DynValue[]
					{
						DynValue.Nil,
						DynValue.NewString("{0}: No such file or directory.", new object[]
						{
							@string
						}),
						DynValue.NewNumber(-1.0)
					});
				}
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message),
					DynValue.NewNumber(-1.0)
				});
			}
			return result;
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x00128A28 File Offset: 0x00126C28
		[MoonSharpModuleMethod]
		public static DynValue rename(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "rename", DataType.String, false).String;
			string string2 = args.AsType(1, "rename", DataType.String, false).String;
			DynValue result;
			try
			{
				if (!Script.GlobalOptions.Platform.OS_FileExists(@string))
				{
					result = DynValue.NewTuple(new DynValue[]
					{
						DynValue.Nil,
						DynValue.NewString("{0}: No such file or directory.", new object[]
						{
							@string
						}),
						DynValue.NewNumber(-1.0)
					});
				}
				else
				{
					Script.GlobalOptions.Platform.OS_FileMove(@string, string2);
					result = DynValue.True;
				}
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message),
					DynValue.NewNumber(-1.0)
				});
			}
			return result;
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0002732B File Offset: 0x0002552B
		[MoonSharpModuleMethod]
		public static DynValue setlocale(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString("n/a");
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x00027337 File Offset: 0x00025537
		[MoonSharpModuleMethod]
		public static DynValue tmpname(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(Script.GlobalOptions.Platform.IO_OS_GetTempFilename());
		}
	}
}
