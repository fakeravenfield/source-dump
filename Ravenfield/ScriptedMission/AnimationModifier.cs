using System;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x0200036F RID: 879
	public class AnimationModifier : ScriptedPathEdgeModifier
	{
		// Token: 0x0600164B RID: 5707 RVA: 0x000118F1 File Offset: 0x0000FAF1
		public override void OnPassed(ScriptedPathSeeker seeker)
		{
			seeker.callbackTarget.OnAnimationTriggered(this.animation);
		}

		// Token: 0x040018A8 RID: 6312
		private const AnimationModifier.Animation MAX_ANIMATION_VALUE = AnimationModifier.Animation.MeleeSwing;

		// Token: 0x040018A9 RID: 6313
		public AnimationModifier.Animation animation;

		// Token: 0x02000370 RID: 880
		public enum Animation
		{
			// Token: 0x040018AB RID: 6315
			SquadHail,
			// Token: 0x040018AC RID: 6316
			SquadRegroup,
			// Token: 0x040018AD RID: 6317
			SquadHalt,
			// Token: 0x040018AE RID: 6318
			SquadMove,
			// Token: 0x040018AF RID: 6319
			MeleeSwing
		}
	}
}
