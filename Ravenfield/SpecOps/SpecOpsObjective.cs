using System;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003C6 RID: 966
	public class SpecOpsObjective
	{
		// Token: 0x06001802 RID: 6146 RVA: 0x0001298B File Offset: 0x00010B8B
		public virtual bool CompleteObjective()
		{
			if (this.objective.isCompleted)
			{
				return false;
			}
			this.objective.SetCompleted();
			this.specOps.OnObjectiveCompleted(this);
			return true;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual bool IsCompletedTest()
		{
			return false;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000129B4 File Offset: 0x00010BB4
		public virtual void Update()
		{
			if (this.objective != null && !this.objective.isCompleted && this.IsCompletedTest())
			{
				this.CompleteObjective();
			}
		}

		// Token: 0x040019E7 RID: 6631
		public ObjectiveEntry objective;

		// Token: 0x040019E8 RID: 6632
		public SpecOpsMode specOps;
	}
}
