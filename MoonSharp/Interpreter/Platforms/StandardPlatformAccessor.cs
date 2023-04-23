using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x02000820 RID: 2080
	public class StandardPlatformAccessor : PlatformAccessorBase
	{
		// Token: 0x060033BD RID: 13245 RVA: 0x00116DAC File Offset: 0x00114FAC
		public static FileAccess ParseFileAccess(string mode)
		{
			mode = mode.Replace("b", "");
			if (mode == "r")
			{
				return FileAccess.Read;
			}
			if (mode == "r+")
			{
				return FileAccess.ReadWrite;
			}
			if (mode == "w")
			{
				return FileAccess.Write;
			}
			mode == "w+";
			return FileAccess.ReadWrite;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x00116E08 File Offset: 0x00115008
		public static FileMode ParseFileMode(string mode)
		{
			mode = mode.Replace("b", "");
			if (mode == "r")
			{
				return FileMode.Open;
			}
			if (mode == "r+")
			{
				return FileMode.OpenOrCreate;
			}
			if (mode == "w")
			{
				return FileMode.Create;
			}
			if (mode == "w+")
			{
				return FileMode.Truncate;
			}
			return FileMode.Append;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x0002374B File Offset: 0x0002194B
		public override Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode)
		{
			return new FileStream(filename, StandardPlatformAccessor.ParseFileMode(mode), StandardPlatformAccessor.ParseFileAccess(mode), FileShare.Read | FileShare.Write | FileShare.Delete);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x00023762 File Offset: 0x00021962
		public override string GetEnvironmentVariable(string envvarname)
		{
			return Environment.GetEnvironmentVariable(envvarname);
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0002376A File Offset: 0x0002196A
		public override Stream IO_GetStandardStream(StandardFileType type)
		{
			switch (type)
			{
			case StandardFileType.StdIn:
				return Console.OpenStandardInput();
			case StandardFileType.StdOut:
				return Console.OpenStandardOutput();
			case StandardFileType.StdErr:
				return Console.OpenStandardError();
			default:
				throw new ArgumentException("type");
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x0002379C File Offset: 0x0002199C
		public override void DefaultPrint(string content)
		{
			Console.WriteLine(content);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000237A4 File Offset: 0x000219A4
		public override string IO_OS_GetTempFilename()
		{
			return Path.GetTempFileName();
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000237AB File Offset: 0x000219AB
		public override void OS_ExitFast(int exitCode)
		{
			Environment.Exit(exitCode);
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000237B3 File Offset: 0x000219B3
		public override bool OS_FileExists(string file)
		{
			return File.Exists(file);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000237BB File Offset: 0x000219BB
		public override void OS_FileDelete(string file)
		{
			File.Delete(file);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000237C3 File Offset: 0x000219C3
		public override void OS_FileMove(string src, string dst)
		{
			File.Move(src, dst);
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000237CC File Offset: 0x000219CC
		public override int OS_Execute(string cmdline)
		{
			Process process = Process.Start(new ProcessStartInfo("cmd.exe", string.Format("/C {0}", cmdline))
			{
				ErrorDialog = false
			});
			process.WaitForExit();
			return process.ExitCode;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000091D3 File Offset: 0x000073D3
		public override CoreModules FilterSupportedCoreModules(CoreModules module)
		{
			return module;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000237FA File Offset: 0x000219FA
		public override string GetPlatformNamePrefix()
		{
			return "std";
		}
	}
}
