using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020008F7 RID: 2295
	internal class StandardIOFileUserDataBase : StreamFileUserDataBase
	{
		// Token: 0x06003A70 RID: 14960 RVA: 0x000275F2 File Offset: 0x000257F2
		protected override string Close()
		{
			return "cannot close standard file";
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000275F9 File Offset: 0x000257F9
		public static StandardIOFileUserDataBase CreateInputStream(Stream stream)
		{
			StandardIOFileUserDataBase standardIOFileUserDataBase = new StandardIOFileUserDataBase();
			standardIOFileUserDataBase.Initialize(stream, new StreamReader(stream), null);
			return standardIOFileUserDataBase;
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x0002760E File Offset: 0x0002580E
		public static StandardIOFileUserDataBase CreateOutputStream(Stream stream)
		{
			StandardIOFileUserDataBase standardIOFileUserDataBase = new StandardIOFileUserDataBase();
			standardIOFileUserDataBase.Initialize(stream, null, new StreamWriter(stream));
			return standardIOFileUserDataBase;
		}
	}
}
