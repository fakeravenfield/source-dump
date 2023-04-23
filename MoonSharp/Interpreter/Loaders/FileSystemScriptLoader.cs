using System;
using System.IO;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000822 RID: 2082
	public class FileSystemScriptLoader : ScriptLoaderBase
	{
		// Token: 0x060033D0 RID: 13264 RVA: 0x000237B3 File Offset: 0x000219B3
		public override bool ScriptFileExists(string name)
		{
			return File.Exists(name);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x0002385A File Offset: 0x00021A5A
		public override object LoadFile(string file, Table globalContext)
		{
			return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		}
	}
}
