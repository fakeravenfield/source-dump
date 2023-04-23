using System;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x02000372 RID: 882
	[Serializable]
	public class LookAtModifier : ScriptedPathEdgeModifier
	{
		// Token: 0x0600164F RID: 5711 RVA: 0x00011938 File Offset: 0x0000FB38
		public override void OnPassed(ScriptedPathSeeker seeker)
		{
			if (this.stop)
			{
				seeker.CancelOverrideLookAtPoint(this.target);
				return;
			}
			seeker.OverrideLookAtPoint(seeker.path.localToWorld.MultiplyPoint(this.lookAtPoint), this.target);
		}

		// Token: 0x040018B2 RID: 6322
		public bool stop;

		// Token: 0x040018B3 RID: 6323
		public Vector3 lookAtPoint;

		// Token: 0x040018B4 RID: 6324
		public LookAtModifier.Target target;

		// Token: 0x02000373 RID: 883
		public enum Target
		{
			// Token: 0x040018B6 RID: 6326
			Body,
			// Token: 0x040018B7 RID: 6327
			Head
		}
	}
}
