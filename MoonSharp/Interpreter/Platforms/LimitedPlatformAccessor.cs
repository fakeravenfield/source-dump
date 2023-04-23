using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x0200081B RID: 2075
	public class LimitedPlatformAccessor : PlatformAccessorBase
	{
		// Token: 0x06003388 RID: 13192 RVA: 0x00002FD8 File Offset: 0x000011D8
		public override string GetEnvironmentVariable(string envvarname)
		{
			return null;
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x00023657 File Offset: 0x00021857
		public override CoreModules FilterSupportedCoreModules(CoreModules module)
		{
			return module & ~(CoreModules.OS_System | CoreModules.IO);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00023660 File Offset: 0x00021860
		public override Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x00023660 File Offset: 0x00021860
		public override Stream IO_GetStandardStream(StandardFileType type)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x00023660 File Offset: 0x00021860
		public override string IO_OS_GetTempFilename()
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00023660 File Offset: 0x00021860
		public override void OS_ExitFast(int exitCode)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x00023660 File Offset: 0x00021860
		public override bool OS_FileExists(string file)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x00023660 File Offset: 0x00021860
		public override void OS_FileDelete(string file)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x00023660 File Offset: 0x00021860
		public override void OS_FileMove(string src, string dst)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x00023660 File Offset: 0x00021860
		public override int OS_Execute(string cmdline)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x0002366C File Offset: 0x0002186C
		public override string GetPlatformNamePrefix()
		{
			return "limited";
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void DefaultPrint(string content)
		{
		}
	}
}
