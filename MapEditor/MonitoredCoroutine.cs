using System;
using System.Collections;

namespace MapEditor
{
	// Token: 0x0200061B RID: 1563
	internal class MonitoredCoroutine
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x0001BC48 File Offset: 0x00019E48
		public MonitoredCoroutine(Action<Exception> onDone)
		{
			this.onDone = onDone;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x0001BC57 File Offset: 0x00019E57
		public IEnumerator Monitor(IEnumerator job)
		{
			yield return this.JobRunner(job);
			if (this.onDone != null)
			{
				this.onDone(this.error);
			}
			yield break;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0001BC6D File Offset: 0x00019E6D
		private IEnumerator JobRunner(IEnumerator job)
		{
			for (;;)
			{
				try
				{
					if (!job.MoveNext())
					{
						yield break;
					}
				}
				catch (Exception ex)
				{
					this.error = ex;
					yield break;
				}
				yield return job.Current;
			}
			yield break;
		}

		// Token: 0x0400264B RID: 9803
		private Action<Exception> onDone;

		// Token: 0x0400264C RID: 9804
		private Exception error;
	}
}
