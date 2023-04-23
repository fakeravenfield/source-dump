using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x0200073F RID: 1855
	public struct ValidationResult
	{
		// Token: 0x06002E6E RID: 11886 RVA: 0x0001FF2A File Offset: 0x0001E12A
		public ValidationResult(string message, Action openDetails = null)
		{
			this.message = message;
			Action action;
			if (openDetails != null)
			{
				action = openDetails;
			}
			else
			{
				action = delegate()
				{
				};
			}
			this.openDetails = action;
		}

		// Token: 0x04002A93 RID: 10899
		public static readonly ValidationResult empty = new ValidationResult("", delegate()
		{
		});

		// Token: 0x04002A94 RID: 10900
		public readonly string message;

		// Token: 0x04002A95 RID: 10901
		public readonly Action openDetails;
	}
}
