using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Dialog
{
	// Token: 0x020003D8 RID: 984
	[CreateAssetMenu(menuName = "Dialog/Dialog")]
	public class Dialog : ScriptableObject
	{
		// Token: 0x04001A48 RID: 6728
		public SpriteActorDatabase[] spriteDatabases;

		// Token: 0x04001A49 RID: 6729
		public List<Dialog.DialogLine> lines;

		// Token: 0x020003D9 RID: 985
		[Serializable]
		public class DialogLine
		{
			// Token: 0x04001A4A RID: 6730
			public bool showDialogPanel = true;

			// Token: 0x04001A4B RID: 6731
			public bool clearText = true;

			// Token: 0x04001A4C RID: 6732
			public string character = "";

			// Token: 0x04001A4D RID: 6733
			public string text = "";

			// Token: 0x04001A4E RID: 6734
			public int actorIndex;

			// Token: 0x04001A4F RID: 6735
			public Dialog.ActorAction[] actorActions;

			// Token: 0x04001A50 RID: 6736
			public AudioClip sound;

			// Token: 0x04001A51 RID: 6737
			public Dialog.DialogLine.Effect effect;

			// Token: 0x04001A52 RID: 6738
			public Dialog.DialogLine.NextLineTrigger nextLineTrigger;

			// Token: 0x04001A53 RID: 6739
			public float nextLineDelay;

			// Token: 0x020003DA RID: 986
			public enum NextLineTrigger
			{
				// Token: 0x04001A55 RID: 6741
				PlayerInput,
				// Token: 0x04001A56 RID: 6742
				Automatic,
				// Token: 0x04001A57 RID: 6743
				AnimationTriggered
			}

			// Token: 0x020003DB RID: 987
			public enum Effect
			{
				// Token: 0x04001A59 RID: 6745
				None,
				// Token: 0x04001A5A RID: 6746
				Flash,
				// Token: 0x04001A5B RID: 6747
				FadeIn,
				// Token: 0x04001A5C RID: 6748
				FadeOut
			}
		}

		// Token: 0x020003DC RID: 988
		[Serializable]
		public class ActorAction
		{
			// Token: 0x04001A5D RID: 6749
			public int actorIndex;

			// Token: 0x04001A5E RID: 6750
			public string actionName;
		}
	}
}
