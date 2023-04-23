using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000899 RID: 2201
	[Flags]
	public enum MemberDescriptorAccess
	{
		// Token: 0x04002ED0 RID: 11984
		CanRead = 1,
		// Token: 0x04002ED1 RID: 11985
		CanWrite = 2,
		// Token: 0x04002ED2 RID: 11986
		CanExecute = 4
	}
}
