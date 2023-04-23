using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007B5 RID: 1973
	public class RefIdObject
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00021B4C File Offset: 0x0001FD4C
		public int ReferenceID
		{
			get
			{
				return this.m_RefID;
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x00021B54 File Offset: 0x0001FD54
		public string FormatTypeString(string typeString)
		{
			return string.Format("{0}: {1:X8}", typeString, this.m_RefID);
		}

		// Token: 0x04002BFE RID: 11262
		private static int s_RefIDCounter;

		// Token: 0x04002BFF RID: 11263
		private int m_RefID = ++RefIdObject.s_RefIDCounter;
	}
}
