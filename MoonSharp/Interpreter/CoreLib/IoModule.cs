﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.CoreLib.IO;
using MoonSharp.Interpreter.Platforms;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008DD RID: 2269
	[MoonSharpModule(Namespace = "io")]
	public class IoModule
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x001279DC File Offset: 0x00125BDC
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			UserData.RegisterType<FileUserDataBase>(InteropAccessMode.Default, "file");
			Table table = new Table(ioTable.OwnerScript);
			DynValue value = DynValue.NewCallback(new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(IoModule.__index_callback), "__index_callback"));
			table.Set("__index", value);
			ioTable.MetaTable = table;
			IoModule.SetStandardFile(globalTable.OwnerScript, StandardFileType.StdIn, globalTable.OwnerScript.Options.Stdin);
			IoModule.SetStandardFile(globalTable.OwnerScript, StandardFileType.StdOut, globalTable.OwnerScript.Options.Stdout);
			IoModule.SetStandardFile(globalTable.OwnerScript, StandardFileType.StdErr, globalTable.OwnerScript.Options.Stderr);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00127A84 File Offset: 0x00125C84
		private static DynValue __index_callback(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string a = args[1].CastToString();
			if (a == "stdin")
			{
				return IoModule.GetStandardFile(executionContext.GetScript(), StandardFileType.StdIn);
			}
			if (a == "stdout")
			{
				return IoModule.GetStandardFile(executionContext.GetScript(), StandardFileType.StdOut);
			}
			if (a == "stderr")
			{
				return IoModule.GetStandardFile(executionContext.GetScript(), StandardFileType.StdErr);
			}
			return DynValue.Nil;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x00026C7D File Offset: 0x00024E7D
		private static DynValue GetStandardFile(Script S, StandardFileType file)
		{
			return S.Registry.Get("853BEAAF298648839E2C99D005E1DF94_STD_" + file.ToString());
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x00127AF4 File Offset: 0x00125CF4
		private static void SetStandardFile(Script S, StandardFileType file, Stream optionsStream)
		{
			Table registry = S.Registry;
			optionsStream = (optionsStream ?? Script.GlobalOptions.Platform.IO_GetStandardStream(file));
			FileUserDataBase o;
			if (file == StandardFileType.StdIn)
			{
				o = StandardIOFileUserDataBase.CreateInputStream(optionsStream);
			}
			else
			{
				o = StandardIOFileUserDataBase.CreateOutputStream(optionsStream);
			}
			registry.Set("853BEAAF298648839E2C99D005E1DF94_STD_" + file.ToString(), UserData.Create(o));
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x00127B58 File Offset: 0x00125D58
		private static FileUserDataBase GetDefaultFile(ScriptExecutionContext executionContext, StandardFileType file)
		{
			DynValue dynValue = executionContext.GetScript().Registry.Get("853BEAAF298648839E2C99D005E1DF94_" + file.ToString());
			if (dynValue.IsNil())
			{
				dynValue = IoModule.GetStandardFile(executionContext.GetScript(), file);
			}
			return dynValue.CheckUserDataType<FileUserDataBase>("getdefaultfile(" + file.ToString() + ")", -1, TypeValidationFlags.AutoConvert);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00026CA1 File Offset: 0x00024EA1
		private static void SetDefaultFile(ScriptExecutionContext executionContext, StandardFileType file, FileUserDataBase fileHandle)
		{
			IoModule.SetDefaultFile(executionContext.GetScript(), file, fileHandle);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00026CB0 File Offset: 0x00024EB0
		internal static void SetDefaultFile(Script script, StandardFileType file, FileUserDataBase fileHandle)
		{
			script.Registry.Set("853BEAAF298648839E2C99D005E1DF94_" + file.ToString(), UserData.Create(fileHandle));
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x00026CDA File Offset: 0x00024EDA
		public static void SetDefaultFile(Script script, StandardFileType file, Stream stream)
		{
			if (file == StandardFileType.StdIn)
			{
				IoModule.SetDefaultFile(script, file, StandardIOFileUserDataBase.CreateInputStream(stream));
				return;
			}
			IoModule.SetDefaultFile(script, file, StandardIOFileUserDataBase.CreateOutputStream(stream));
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x00026CFA File Offset: 0x00024EFA
		[MoonSharpModuleMethod]
		public static DynValue close(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return (args.AsUserData<FileUserDataBase>(0, "close", true) ?? IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut)).close(executionContext, args);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x00026D1B File Offset: 0x00024F1B
		[MoonSharpModuleMethod]
		public static DynValue flush(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			(args.AsUserData<FileUserDataBase>(0, "close", true) ?? IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut)).flush();
			return DynValue.True;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00026D40 File Offset: 0x00024F40
		[MoonSharpModuleMethod]
		public static DynValue input(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.HandleDefaultStreamSetter(executionContext, args, StandardFileType.StdIn);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x00026D4A File Offset: 0x00024F4A
		[MoonSharpModuleMethod]
		public static DynValue output(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.HandleDefaultStreamSetter(executionContext, args, StandardFileType.StdOut);
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x00127BC8 File Offset: 0x00125DC8
		private static DynValue HandleDefaultStreamSetter(ScriptExecutionContext executionContext, CallbackArguments args, StandardFileType defaultFiles)
		{
			if (args.Count == 0 || args[0].IsNil())
			{
				return UserData.Create(IoModule.GetDefaultFile(executionContext, defaultFiles));
			}
			FileUserDataBase fileUserDataBase;
			if (args[0].Type == DataType.String || args[0].Type == DataType.Number)
			{
				string filename = args[0].CastToString();
				fileUserDataBase = IoModule.Open(executionContext, filename, IoModule.GetUTF8Encoding(), (defaultFiles == StandardFileType.StdIn) ? "r" : "w");
			}
			else
			{
				fileUserDataBase = args.AsUserData<FileUserDataBase>(0, (defaultFiles == StandardFileType.StdIn) ? "input" : "output", false);
			}
			IoModule.SetDefaultFile(executionContext, defaultFiles, fileUserDataBase);
			return UserData.Create(fileUserDataBase);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x00026D54 File Offset: 0x00024F54
		private static Encoding GetUTF8Encoding()
		{
			return new UTF8Encoding(false);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x00127C6C File Offset: 0x00125E6C
		[MoonSharpModuleMethod]
		public static DynValue lines(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "lines", DataType.String, false).String;
			DynValue result;
			try
			{
				List<DynValue> list = new List<DynValue>();
				using (Stream stream = Script.GlobalOptions.Platform.IO_OpenFile(executionContext.GetScript(), @string, null, "r"))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						while (!streamReader.EndOfStream)
						{
							string str = streamReader.ReadLine();
							list.Add(DynValue.NewString(str));
						}
					}
				}
				list.Add(DynValue.Nil);
				result = DynValue.FromObject(executionContext.GetScript(), from s in list
				select s);
			}
			catch (Exception ex)
			{
				throw new ScriptRuntimeException(IoModule.IoExceptionToLuaMessage(ex, @string));
			}
			return result;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x00127D64 File Offset: 0x00125F64
		[MoonSharpModuleMethod]
		public static DynValue open(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "open", DataType.String, false).String;
			DynValue dynValue = args.AsType(1, "open", DataType.String, true);
			DynValue dynValue2 = args.AsType(2, "open", DataType.String, true);
			string text = dynValue.IsNil() ? "r" : dynValue.String;
			if (text.Replace("+", "").Replace("r", "").Replace("a", "").Replace("w", "").Replace("b", "").Replace("t", "").Length > 0)
			{
				throw ScriptRuntimeException.BadArgument(1, "open", "invalid mode");
			}
			DynValue result;
			try
			{
				string text2 = dynValue2.IsNil() ? null : dynValue2.String;
				bool flag = Framework.Do.StringContainsChar(text, 'b');
				Encoding encoding;
				if (text2 == "binary")
				{
					encoding = new BinaryEncoding();
				}
				else if (text2 == null)
				{
					if (!flag)
					{
						encoding = IoModule.GetUTF8Encoding();
					}
					else
					{
						encoding = new BinaryEncoding();
					}
				}
				else
				{
					if (flag)
					{
						throw new ScriptRuntimeException("Can't specify encodings other than nil or 'binary' for binary streams.");
					}
					encoding = Encoding.GetEncoding(text2);
				}
				result = UserData.Create(IoModule.Open(executionContext, @string, encoding, text));
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(IoModule.IoExceptionToLuaMessage(ex, @string))
				});
			}
			return result;
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00026D5C File Offset: 0x00024F5C
		public static string IoExceptionToLuaMessage(Exception ex, string filename)
		{
			if (ex is FileNotFoundException)
			{
				return string.Format("{0}: No such file or directory", filename);
			}
			return ex.Message;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x00127EF0 File Offset: 0x001260F0
		[MoonSharpModuleMethod]
		public static DynValue type(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.UserData)
			{
				return DynValue.Nil;
			}
			FileUserDataBase fileUserDataBase = args[0].UserData.Object as FileUserDataBase;
			if (fileUserDataBase == null)
			{
				return DynValue.Nil;
			}
			if (fileUserDataBase.isopen())
			{
				return DynValue.NewString("file");
			}
			return DynValue.NewString("closed file");
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x00026D78 File Offset: 0x00024F78
		[MoonSharpModuleMethod]
		public static DynValue read(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.GetDefaultFile(executionContext, StandardFileType.StdIn).read(executionContext, args);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x00026D88 File Offset: 0x00024F88
		[MoonSharpModuleMethod]
		public static DynValue write(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut).write(executionContext, args);
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00127F50 File Offset: 0x00126150
		[MoonSharpModuleMethod]
		public static DynValue tmpfile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string filename = Script.GlobalOptions.Platform.IO_OS_GetTempFilename();
			return UserData.Create(IoModule.Open(executionContext, filename, IoModule.GetUTF8Encoding(), "w"));
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x00026D98 File Offset: 0x00024F98
		private static FileUserDataBase Open(ScriptExecutionContext executionContext, string filename, Encoding encoding, string mode)
		{
			return new FileUserData(executionContext.GetScript(), filename, encoding, mode);
		}
	}
}
