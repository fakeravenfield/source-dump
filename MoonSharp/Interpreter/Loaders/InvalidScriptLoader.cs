using System;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000824 RID: 2084
	internal class InvalidScriptLoader : IScriptLoader
	{
		// Token: 0x060033D6 RID: 13270 RVA: 0x0002386D File Offset: 0x00021A6D
		internal InvalidScriptLoader(string frameworkname)
		{
			this.m_Error = string.Format("Loading scripts from files is not automatically supported on {0}. \nPlease implement your own IScriptLoader (possibly, extending ScriptLoaderBase for easier implementation),\nuse a preexisting loader like EmbeddedResourcesScriptLoader or UnityAssetsScriptLoader or load scripts from strings.", frameworkname);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x00023886 File Offset: 0x00021A86
		public object LoadFile(string file, Table globalContext)
		{
			throw new PlatformNotSupportedException(this.m_Error);
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000091D3 File Offset: 0x000073D3
		public string ResolveFileName(string filename, Table globalContext)
		{
			return filename;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x00023886 File Offset: 0x00021A86
		public string ResolveModuleName(string modname, Table globalContext)
		{
			throw new PlatformNotSupportedException(this.m_Error);
		}

		// Token: 0x04002D8C RID: 11660
		private string m_Error;
	}
}
