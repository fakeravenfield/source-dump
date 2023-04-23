using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C3 RID: 1987
	[Serializable]
	public class DynamicExpressionException : ScriptRuntimeException
	{
		// Token: 0x0600317C RID: 12668 RVA: 0x0002224C File Offset: 0x0002044C
		public DynamicExpressionException(string format, params object[] args) : base("<dynamic>: " + format, args)
		{
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x00022260 File Offset: 0x00020460
		public DynamicExpressionException(string message) : base("<dynamic>: " + message)
		{
		}
	}
}
