using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C4 RID: 1988
	[Serializable]
	public class InternalErrorException : InterpreterException
	{
		// Token: 0x0600317E RID: 12670 RVA: 0x00022273 File Offset: 0x00020473
		internal InternalErrorException(string message) : base(message)
		{
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x0002227C File Offset: 0x0002047C
		internal InternalErrorException(string format, params object[] args) : base(format, args)
		{
		}
	}
}
