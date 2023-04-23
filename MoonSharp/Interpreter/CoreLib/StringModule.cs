using System;
using System.IO;
using System.Text;
using MoonSharp.Interpreter.CoreLib.StringLib;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008E6 RID: 2278
	[MoonSharpModule(Namespace = "string")]
	public class StringModule
	{
		// Token: 0x060039F3 RID: 14835 RVA: 0x0012920C File Offset: 0x0012740C
		public static void MoonSharpInit(Table globalTable, Table stringTable)
		{
			Table table = new Table(globalTable.OwnerScript);
			table.Set("__index", DynValue.NewTable(stringTable));
			globalTable.OwnerScript.SetTypeMetatable(DataType.String, table);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x00129244 File Offset: 0x00127444
		[MoonSharpModuleMethod]
		public static DynValue dump(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue function = args.AsType(0, "dump", DataType.Function, false);
			DynValue result;
			try
			{
				byte[] inArray;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					executionContext.GetScript().Dump(function, memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					inArray = memoryStream.ToArray();
				}
				string str = Convert.ToBase64String(inArray);
				result = DynValue.NewString("MoonSharp_dump_b64::" + str);
			}
			catch (Exception ex)
			{
				throw new ScriptRuntimeException(ex.Message);
			}
			return result;
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x001292D4 File Offset: 0x001274D4
		[MoonSharpModuleMethod]
		public static DynValue @char(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			StringBuilder stringBuilder = new StringBuilder(args.Count);
			for (int i = 0; i < args.Count; i++)
			{
				DynValue dynValue = args[i];
				double num = 0.0;
				if (dynValue.Type == DataType.String)
				{
					double? num2 = dynValue.CastToNumber();
					if (num2 == null)
					{
						args.AsType(i, "char", DataType.Number, false);
					}
					else
					{
						num = num2.Value;
					}
				}
				else
				{
					args.AsType(i, "char", DataType.Number, false);
					num = dynValue.Number;
				}
				stringBuilder.Append((char)num);
			}
			return DynValue.NewString(stringBuilder.ToString());
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x00129370 File Offset: 0x00127570
		[MoonSharpModuleMethod]
		public static DynValue @byte(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue vs = args.AsType(0, "byte", DataType.String, false);
			DynValue vi = args.AsType(1, "byte", DataType.Number, true);
			DynValue vj = args.AsType(2, "byte", DataType.Number, true);
			return StringModule.PerformByteLike(vs, vi, vj, (int i) => StringModule.Unicode2Ascii(i));
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x001293D0 File Offset: 0x001275D0
		[MoonSharpModuleMethod]
		public static DynValue unicode(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue vs = args.AsType(0, "unicode", DataType.String, false);
			DynValue vi = args.AsType(1, "unicode", DataType.Number, true);
			DynValue vj = args.AsType(2, "unicode", DataType.Number, true);
			return StringModule.PerformByteLike(vs, vi, vj, (int i) => i);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x00027384 File Offset: 0x00025584
		private static int Unicode2Ascii(int i)
		{
			if (i >= 0 && i < 255)
			{
				return i;
			}
			return 63;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x00129430 File Offset: 0x00127630
		private static DynValue PerformByteLike(DynValue vs, DynValue vi, DynValue vj, Func<int, int> filter)
		{
			string text = StringRange.FromLuaRange(vi, vj, null).ApplyToString(vs.String);
			int length = text.Length;
			DynValue[] array = new DynValue[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = DynValue.NewNumber((double)filter((int)text[i]));
			}
			return DynValue.NewTuple(array);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x00129498 File Offset: 0x00127698
		private static int? AdjustIndex(string s, DynValue vi, int defval)
		{
			if (vi.IsNil())
			{
				return new int?(defval);
			}
			int num = (int)Math.Round(vi.Number, 0);
			if (num == 0)
			{
				return null;
			}
			if (num > 0)
			{
				return new int?(num - 1);
			}
			return new int?(s.Length - num);
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x00027396 File Offset: 0x00025596
		[MoonSharpModuleMethod]
		public static DynValue len(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber((double)args.AsType(0, "len", DataType.String, false).String.Length);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000273B6 File Offset: 0x000255B6
		[MoonSharpModuleMethod]
		public static DynValue match(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "match", new Func<LuaState, int>(KopiLua_StringLib.str_match));
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000273D0 File Offset: 0x000255D0
		[MoonSharpModuleMethod]
		public static DynValue gmatch(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gmatch", new Func<LuaState, int>(KopiLua_StringLib.str_gmatch));
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000273EA File Offset: 0x000255EA
		[MoonSharpModuleMethod]
		public static DynValue gsub(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gsub", new Func<LuaState, int>(KopiLua_StringLib.str_gsub));
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x00027404 File Offset: 0x00025604
		[MoonSharpModuleMethod]
		public static DynValue find(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "find", new Func<LuaState, int>(KopiLua_StringLib.str_find));
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0002741E File Offset: 0x0002561E
		[MoonSharpModuleMethod]
		public static DynValue lower(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(args.AsType(0, "lower", DataType.String, false).String.ToLower());
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x0002743D File Offset: 0x0002563D
		[MoonSharpModuleMethod]
		public static DynValue upper(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(args.AsType(0, "upper", DataType.String, false).String.ToUpper());
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x001294EC File Offset: 0x001276EC
		[MoonSharpModuleMethod]
		public static DynValue rep(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rep", DataType.String, false);
			DynValue dynValue2 = args.AsType(1, "rep", DataType.Number, false);
			DynValue dynValue3 = args.AsType(2, "rep", DataType.String, true);
			if (string.IsNullOrEmpty(dynValue.String) || dynValue2.Number < 1.0)
			{
				return DynValue.NewString("");
			}
			string text = dynValue3.IsNotNil() ? dynValue3.String : null;
			int num = (int)dynValue2.Number;
			StringBuilder stringBuilder = new StringBuilder(dynValue.String.Length * num);
			for (int i = 0; i < num; i++)
			{
				if (i != 0 && text != null)
				{
					stringBuilder.Append(text);
				}
				stringBuilder.Append(dynValue.String);
			}
			return DynValue.NewString(stringBuilder.ToString());
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x0002745C File Offset: 0x0002565C
		[MoonSharpModuleMethod]
		public static DynValue format(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "format", new Func<LuaState, int>(KopiLua_StringLib.str_format));
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x001295BC File Offset: 0x001277BC
		[MoonSharpModuleMethod]
		public static DynValue reverse(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "reverse", DataType.String, false);
			if (string.IsNullOrEmpty(dynValue.String))
			{
				return DynValue.NewString("");
			}
			char[] array = dynValue.String.ToCharArray();
			Array.Reverse(array);
			return DynValue.NewString(new string(array));
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x0012960C File Offset: 0x0012780C
		[MoonSharpModuleMethod]
		public static DynValue sub(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "sub", DataType.String, false);
			DynValue start = args.AsType(1, "sub", DataType.Number, true);
			DynValue end = args.AsType(2, "sub", DataType.Number, true);
			return DynValue.NewString(StringRange.FromLuaRange(start, end, new int?(-1)).ApplyToString(dynValue.String));
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x00129664 File Offset: 0x00127864
		[MoonSharpModuleMethod]
		public static DynValue startsWith(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "startsWith", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "startsWith", DataType.String, true);
			if (dynValue.IsNil() || dynValue2.IsNil())
			{
				return DynValue.False;
			}
			return DynValue.NewBoolean(dynValue.String.StartsWith(dynValue2.String));
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x001296BC File Offset: 0x001278BC
		[MoonSharpModuleMethod]
		public static DynValue endsWith(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "endsWith", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "endsWith", DataType.String, true);
			if (dynValue.IsNil() || dynValue2.IsNil())
			{
				return DynValue.False;
			}
			return DynValue.NewBoolean(dynValue.String.EndsWith(dynValue2.String));
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x00129714 File Offset: 0x00127914
		[MoonSharpModuleMethod]
		public static DynValue contains(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "contains", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "contains", DataType.String, true);
			if (dynValue.IsNil() || dynValue2.IsNil())
			{
				return DynValue.False;
			}
			return DynValue.NewBoolean(dynValue.String.Contains(dynValue2.String));
		}

		// Token: 0x04003013 RID: 12307
		public const string BASE64_DUMP_HEADER = "MoonSharp_dump_b64::";
	}
}
