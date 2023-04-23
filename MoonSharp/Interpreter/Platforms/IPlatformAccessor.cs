using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x0200081A RID: 2074
	public interface IPlatformAccessor
	{
		// Token: 0x0600337A RID: 13178
		CoreModules FilterSupportedCoreModules(CoreModules module);

		// Token: 0x0600337B RID: 13179
		string GetEnvironmentVariable(string envvarname);

		// Token: 0x0600337C RID: 13180
		bool IsRunningOnAOT();

		// Token: 0x0600337D RID: 13181
		string GetPlatformName();

		// Token: 0x0600337E RID: 13182
		void DefaultPrint(string content);

		// Token: 0x0600337F RID: 13183
		string DefaultInput(string prompt);

		// Token: 0x06003380 RID: 13184
		Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode);

		// Token: 0x06003381 RID: 13185
		Stream IO_GetStandardStream(StandardFileType type);

		// Token: 0x06003382 RID: 13186
		string IO_OS_GetTempFilename();

		// Token: 0x06003383 RID: 13187
		void OS_ExitFast(int exitCode);

		// Token: 0x06003384 RID: 13188
		bool OS_FileExists(string file);

		// Token: 0x06003385 RID: 13189
		void OS_FileDelete(string file);

		// Token: 0x06003386 RID: 13190
		void OS_FileMove(string src, string dst);

		// Token: 0x06003387 RID: 13191
		int OS_Execute(string cmdline);
	}
}
