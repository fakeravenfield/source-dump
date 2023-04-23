using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020008F4 RID: 2292
	internal class FileUserData : StreamFileUserDataBase
	{
		// Token: 0x06003A59 RID: 14937 RVA: 0x0012B778 File Offset: 0x00129978
		public FileUserData(Script script, string filename, Encoding encoding, string mode)
		{
			Stream stream = Script.GlobalOptions.Platform.IO_OpenFile(script, filename, encoding, mode);
			StreamReader reader = stream.CanRead ? new StreamReader(stream, encoding) : null;
			StreamWriter writer = stream.CanWrite ? new StreamWriter(stream, encoding) : null;
			base.Initialize(stream, reader, writer);
		}
	}
}
