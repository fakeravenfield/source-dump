using System;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x0200037F RID: 895
	public struct TriggerVolumeActorState
	{
		// Token: 0x06001694 RID: 5780 RVA: 0x00011DC3 File Offset: 0x0000FFC3
		public void ClearTeamCounters()
		{
			this.team0Actors = 0;
			this.team1Actors = 0;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000A0D08 File Offset: 0x0009EF08
		public bool GetActorIsInside(int actorIndex)
		{
			int num = actorIndex % 64;
			long num2 = 1L << (num & 31);
			if (actorIndex < 64)
			{
				return (this.s0 & num2) != 0L;
			}
			if (actorIndex < 128)
			{
				return (this.s1 & num2) != 0L;
			}
			if (actorIndex < 192)
			{
				return (this.s2 & num2) != 0L;
			}
			return (this.s3 & num2) != 0L;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000A0D6C File Offset: 0x0009EF6C
		public bool UpdateActorFlag(int actorIndex, bool isInside, bool isTeam0)
		{
			int num = actorIndex % 64;
			long num2 = 1L << (num & 31);
			long clearMask = ~num2;
			long writeMask = (isInside ? 1L : 0L) << (num & 31);
			bool flag;
			if (actorIndex < 64)
			{
				this.s0 = this.UpdateActorBit(this.s0, num2, clearMask, writeMask, out flag);
			}
			else if (actorIndex < 128)
			{
				this.s1 = this.UpdateActorBit(this.s1, num2, clearMask, writeMask, out flag);
			}
			else if (actorIndex < 192)
			{
				this.s2 = this.UpdateActorBit(this.s2, num2, clearMask, writeMask, out flag);
			}
			else
			{
				this.s3 = this.UpdateActorBit(this.s3, num2, clearMask, writeMask, out flag);
			}
			if (isInside)
			{
				if (isTeam0)
				{
					this.team0Actors++;
				}
				else
				{
					this.team1Actors++;
				}
			}
			return flag != isInside;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00011DD3 File Offset: 0x0000FFD3
		private long UpdateActorBit(long container, long bitMask, long clearMask, long writeMask, out bool prevValue)
		{
			prevValue = ((container & bitMask) != 0L);
			return (container & clearMask) | writeMask;
		}

		// Token: 0x04001902 RID: 6402
		public const int TOTAL_ACTORS_SUPPORTED = 256;

		// Token: 0x04001903 RID: 6403
		private const int TYPE_BITS = 64;

		// Token: 0x04001904 RID: 6404
		private long s0;

		// Token: 0x04001905 RID: 6405
		private long s1;

		// Token: 0x04001906 RID: 6406
		private long s2;

		// Token: 0x04001907 RID: 6407
		private long s3;

		// Token: 0x04001908 RID: 6408
		public int team0Actors;

		// Token: 0x04001909 RID: 6409
		public int team1Actors;
	}
}
