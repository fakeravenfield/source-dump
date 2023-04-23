using System;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x02000371 RID: 881
	public class LeanModifier : ScriptedPathEdgeModifier
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x00011904 File Offset: 0x0000FB04
		public override void OnPassed(ScriptedPathSeeker seeker)
		{
			if (this.stop)
			{
				seeker.modifierData.isLeaning = false;
				return;
			}
			seeker.modifierData.isLeaning = true;
			seeker.modifierData.lean = this.lean;
		}

		// Token: 0x040018B0 RID: 6320
		public bool stop;

		// Token: 0x040018B1 RID: 6321
		public float lean;
	}
}
