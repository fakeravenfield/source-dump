using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x0200081C RID: 2076
	public abstract class PlatformAccessorBase : IPlatformAccessor
	{
		// Token: 0x06003395 RID: 13205
		public abstract string GetPlatformNamePrefix();

		// Token: 0x06003396 RID: 13206 RVA: 0x00116BD8 File Offset: 0x00114DD8
		public string GetPlatformName()
		{
			string text;
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				if (PlatformAutoDetector.IsUnityNative)
				{
					text = "unity." + this.GetUnityPlatformName().ToLower() + "." + this.GetUnityRuntimeName();
				}
				else if (PlatformAutoDetector.IsRunningOnMono)
				{
					text = "unity.dll.mono";
				}
				else
				{
					text = "unity.dll.unknown";
				}
			}
			else if (PlatformAutoDetector.IsRunningOnMono)
			{
				text = "mono";
			}
			else
			{
				text = "dotnet";
			}
			if (PlatformAutoDetector.IsPortableFramework)
			{
				text += ".portable";
			}
			if (PlatformAutoDetector.IsRunningOnClr4)
			{
				text += ".clr4";
			}
			else
			{
				text += ".clr2";
			}
			if (PlatformAutoDetector.IsRunningOnAOT)
			{
				text += ".aot";
			}
			return this.GetPlatformNamePrefix() + "." + text;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0002367B File Offset: 0x0002187B
		private string GetUnityRuntimeName()
		{
			return "mono";
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x00023682 File Offset: 0x00021882
		private string GetUnityPlatformName()
		{
			return "WIN";
		}

		// Token: 0x06003399 RID: 13209
		public abstract void DefaultPrint(string content);

		// Token: 0x0600339A RID: 13210 RVA: 0x00002FD8 File Offset: 0x000011D8
		[Obsolete("Replace with DefaultInput(string)")]
		public virtual string DefaultInput()
		{
			return null;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x00023689 File Offset: 0x00021889
		public virtual string DefaultInput(string prompt)
		{
			return this.DefaultInput();
		}

		// Token: 0x0600339C RID: 13212
		public abstract Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode);

		// Token: 0x0600339D RID: 13213
		public abstract Stream IO_GetStandardStream(StandardFileType type);

		// Token: 0x0600339E RID: 13214
		public abstract string IO_OS_GetTempFilename();

		// Token: 0x0600339F RID: 13215
		public abstract void OS_ExitFast(int exitCode);

		// Token: 0x060033A0 RID: 13216
		public abstract bool OS_FileExists(string file);

		// Token: 0x060033A1 RID: 13217
		public abstract void OS_FileDelete(string file);

		// Token: 0x060033A2 RID: 13218
		public abstract void OS_FileMove(string src, string dst);

		// Token: 0x060033A3 RID: 13219
		public abstract int OS_Execute(string cmdline);

		// Token: 0x060033A4 RID: 13220
		public abstract CoreModules FilterSupportedCoreModules(CoreModules module);

		// Token: 0x060033A5 RID: 13221
		public abstract string GetEnvironmentVariable(string envvarname);

		// Token: 0x060033A6 RID: 13222 RVA: 0x00023691 File Offset: 0x00021891
		public virtual bool IsRunningOnAOT()
		{
			return PlatformAutoDetector.IsRunningOnAOT;
		}
	}
}
