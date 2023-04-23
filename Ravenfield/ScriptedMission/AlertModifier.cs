using System;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x0200036E RID: 878
	public class AlertModifier : ScriptedPathEdgeModifier
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x000118D3 File Offset: 0x0000FAD3
		public override void OnPassed(ScriptedPathSeeker seeker)
		{
			seeker.modifierData.isNotAlert = !this.isAlert;
		}

		// Token: 0x040018A7 RID: 6311
		public bool isAlert;
	}
}
