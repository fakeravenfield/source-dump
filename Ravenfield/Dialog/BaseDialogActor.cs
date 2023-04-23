using System;
using System.Collections;
using UnityEngine;

namespace Ravenfield.Dialog
{
	// Token: 0x020003D4 RID: 980
	public class BaseDialogActor : MonoBehaviour
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x00012C97 File Offset: 0x00010E97
		public void StartTalking()
		{
			this.StopTalking();
			this.forceMouthIdle = false;
			this.talkCoroutine = base.StartCoroutine(this.Talk());
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00012CB8 File Offset: 0x00010EB8
		public void StopTalking()
		{
			if (this.IsTalking())
			{
				base.StopCoroutine(this.talkCoroutine);
				this.talkCoroutine = null;
			}
			this.SetMouthIdleFrame();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00012CDB File Offset: 0x00010EDB
		public bool IsTalking()
		{
			return this.talkCoroutine != null;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00012CE6 File Offset: 0x00010EE6
		private IEnumerator Talk()
		{
			for (;;)
			{
				if (!this.forceMouthIdle)
				{
					int i = UnityEngine.Random.Range(3, 7);
					int num;
					for (int j = 0; j < i; j = num + 1)
					{
						this.NextTalkFrame();
						yield return new WaitForSeconds(0.1f);
						num = j;
					}
					this.SetMouthIdleFrame();
					yield return new WaitForSeconds(0.2f);
				}
				else
				{
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield break;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void NextTalkFrame()
		{
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void SetMouthIdleFrame()
		{
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void Blink()
		{
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00012CF5 File Offset: 0x00010EF5
		public virtual void TriggerPose(string name)
		{
			this.SetMouthIdleFrame();
		}

		// Token: 0x04001A30 RID: 6704
		private const float PAUSE_BETWEEN_TALK_FRAMES = 0.1f;

		// Token: 0x04001A31 RID: 6705
		private const float PAUSE_BETWEEN_WORDS = 0.2f;

		// Token: 0x04001A32 RID: 6706
		private const int MIN_FRAMES_PER_WORD = 3;

		// Token: 0x04001A33 RID: 6707
		private const int MAX_FRAMES_PER_WORD = 7;

		// Token: 0x04001A34 RID: 6708
		private Coroutine talkCoroutine;

		// Token: 0x04001A35 RID: 6709
		public AudioClip blipSound;

		// Token: 0x04001A36 RID: 6710
		[NonSerialized]
		public bool forceMouthIdle;
	}
}
