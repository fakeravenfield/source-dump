using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenfield.Dialog
{
	// Token: 0x020003DE RID: 990
	public class DialogCanvas : DialogPlayerBase
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x00012EAE File Offset: 0x000110AE
		private void OnEnable()
		{
			DialogCanvas.instance = this;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00012EB6 File Offset: 0x000110B6
		private void OnDisable()
		{
			DialogCanvas.instance = null;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000A5600 File Offset: 0x000A3800
		private void Start()
		{
			DialogPlayerBase.SetFontSize(this.dialogText, 3);
			if (this.dialog != null)
			{
				this.StartDialog(this.dialog);
			}
			this.effectImage.enabled = true;
			this.effectImage.color = new Color(0f, 0f, 0f, 0f);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00012EBE File Offset: 0x000110BE
		private void Update()
		{
			if (this.HasDialog() && this.currentLine != null)
			{
				this.UpdateDialog();
			}
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000A5664 File Offset: 0x000A3864
		private void UpdateDialog()
		{
			if (this.currentLine.nextLineTrigger == Dialog.DialogLine.NextLineTrigger.Automatic)
			{
				this.TryNextLine();
			}
			else if (this.currentLine.nextLineTrigger == Dialog.DialogLine.NextLineTrigger.PlayerInput)
			{
				if (DialogPlayerBase.GetNextLineInput())
				{
					this.TryNextLine();
				}
			}
			else if (this.currentLine.nextLineTrigger == Dialog.DialogLine.NextLineTrigger.AnimationTriggered && this.animationAdvanceEvents > 0 && this.TryNextLine())
			{
				this.animationAdvanceEvents--;
			}
			if (this.lineActor != null)
			{
				this.lineActor.forceMouthIdle = !this.isPrintingNormalCharacter;
			}
			if (!this.effectDoneLastFrame)
			{
				if (this.activeEffect == Dialog.DialogLine.Effect.Flash)
				{
					this.effectImage.color = new Color(1f, 1f, 1f, 1f - this.effectAction.Ratio());
					this.dialogParent.anchoredPosition = new Vector2(Mathf.Sin(Time.time * 150f), Mathf.Sin(Time.time * 130f)) * 8f * Mathf.Clamp01(2f - 2f * this.effectAction.Ratio());
				}
				if (this.activeEffect == Dialog.DialogLine.Effect.FadeIn)
				{
					this.effectImage.color = new Color(0f, 0f, 0f, 1f - this.effectAction.Ratio());
				}
				if (this.activeEffect == Dialog.DialogLine.Effect.FadeOut)
				{
					this.effectImage.color = new Color(0f, 0f, 0f, this.effectAction.Ratio());
				}
			}
			else
			{
				this.dialogParent.anchoredPosition = Vector2.zero;
			}
			this.effectDoneLastFrame = this.effectAction.TrueDone();
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00012ED6 File Offset: 0x000110D6
		private bool HasDialog()
		{
			return this.dialog != null;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000A5828 File Offset: 0x000A3A28
		private void StartDialog(Dialog dialog)
		{
			this.dialog = dialog;
			this.lineIndex = this.startLineIndex - 1;
			this.animationAdvanceEvents = 0;
			this.SetLineActor(this.actors[0]);
			SpriteActorDatabase[] spriteDatabases = dialog.spriteDatabases;
			for (int i = 0; i < spriteDatabases.Length; i++)
			{
				spriteDatabases[i].InitializeRuntimeData();
			}
			BaseDialogActor[] array = this.actors;
			for (int i = 0; i < array.Length; i++)
			{
				SpriteDialogActor spriteDialogActor = array[i] as SpriteDialogActor;
				if (spriteDialogActor != null)
				{
					spriteDialogActor.SetActiveDatabases(dialog.spriteDatabases);
				}
			}
			this.NextLine();
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00012EE4 File Offset: 0x000110E4
		private void SetLineActor(BaseDialogActor actor)
		{
			this.lineActor = actor;
			if (this.lineActor != null)
			{
				this.blipSound = actor.blipSound;
				return;
			}
			this.blipSound = null;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00012F0F File Offset: 0x0001110F
		private void EndDialog()
		{
			this.dialog = null;
			this.lineActor = null;
			if (this.quitToMenuWhenDone)
			{
				GameManager.ReturnToMenu();
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00012F2C File Offset: 0x0001112C
		public void QueueAnimationAdvanceEvent()
		{
			this.animationAdvanceEvents++;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000A58B8 File Offset: 0x000A3AB8
		private bool TryNextLine()
		{
			float num = Time.time - this.currentLineStartTime;
			if (!this.isPrintingText && num > this.currentLine.nextLineDelay)
			{
				this.EndLine();
				this.NextLine();
				return true;
			}
			return false;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00012F3C File Offset: 0x0001113C
		private void EndLine()
		{
			if (this.lineActor != null)
			{
				this.lineActor.StopTalking();
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000A58F8 File Offset: 0x000A3AF8
		private static string FormatLineText(string text, Text targetTextField)
		{
			text = text.Replace("\\n", "\n");
			TextGenerator cachedTextGenerator = targetTextField.cachedTextGenerator;
			cachedTextGenerator.Populate(text, targetTextField.GetGenerationSettings(targetTextField.rectTransform.rect.size));
			text = text.Replace("\n", "");
			string text2 = text;
			for (int i = 1; i < cachedTextGenerator.lineCount; i++)
			{
				int startIndex = cachedTextGenerator.lines[i].startCharIdx - 1;
				text2 = text2.Insert(startIndex, "\n");
			}
			return text2;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000A5988 File Offset: 0x000A3B88
		private void PrintCurrentLineText()
		{
			if (!string.IsNullOrEmpty(this.currentLine.character))
			{
				this.characterText.text = this.currentLine.character;
			}
			int startIndex = 0;
			if (this.currentLine.clearText)
			{
				this.currentLineCumulativeString = this.currentLine.text;
				this.dialogText.text = "";
			}
			else
			{
				startIndex = this.currentFormattedLineLength;
				this.currentLineCumulativeString += this.currentLine.text;
			}
			string text = DialogCanvas.FormatLineText(this.currentLineCumulativeString, this.dialogText);
			this.currentFormattedLineLength = text.Length;
			if (this.currentLine.actorIndex >= 0 && this.currentLine.actorIndex < this.actors.Length)
			{
				this.SetLineActor(this.actors[this.currentLine.actorIndex]);
				if (this.lineActor != null)
				{
					this.lineActor.StartTalking();
				}
			}
			base.StartPrinting(this.dialogText, text, startIndex, true);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000A5A94 File Offset: 0x000A3C94
		public override void OnPrintDone()
		{
			if (this.currentLine.actorIndex >= 0 && this.currentLine.actorIndex < this.actors.Length)
			{
				this.SetLineActor(this.actors[this.currentLine.actorIndex]);
				if (this.lineActor != null)
				{
					this.lineActor.StopTalking();
				}
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000A5AF8 File Offset: 0x000A3CF8
		private void NextLine()
		{
			this.lineIndex++;
			this.currentLineStartTime = Time.time;
			if (this.lineIndex >= this.dialog.lines.Count)
			{
				this.EndDialog();
				return;
			}
			this.currentLine = this.dialog.lines[this.lineIndex];
			foreach (Dialog.ActorAction actorAction in this.currentLine.actorActions)
			{
				this.actors[actorAction.actorIndex].TriggerPose(actorAction.actionName);
			}
			this.dialogPanel.SetActive(this.currentLine.showDialogPanel);
			if (this.currentLine.showDialogPanel)
			{
				this.PrintCurrentLineText();
			}
			if (this.currentLine.effect != Dialog.DialogLine.Effect.None)
			{
				this.activeEffect = this.currentLine.effect;
			}
			if (this.currentLine.effect == Dialog.DialogLine.Effect.Flash)
			{
				this.effectAction.StartLifetime(0.4f);
			}
			else if (this.currentLine.effect == Dialog.DialogLine.Effect.FadeIn || this.currentLine.effect == Dialog.DialogLine.Effect.FadeOut)
			{
				this.effectAction.StartLifetime(2f);
			}
			if (this.currentLine.sound != null)
			{
				this.audioSource.PlayOneShot(this.currentLine.sound);
			}
		}

		// Token: 0x04001A5F RID: 6751
		private const float FLASH_SHAKE_FREQUENCY_X = 150f;

		// Token: 0x04001A60 RID: 6752
		private const float FLASH_SHAKE_FREQUENCY_Y = 130f;

		// Token: 0x04001A61 RID: 6753
		private const float FLASH_SHAKE_MAGNITUDE = 8f;

		// Token: 0x04001A62 RID: 6754
		private const int TEXT_LINES = 3;

		// Token: 0x04001A63 RID: 6755
		private const float FLASH_TIME = 0.4f;

		// Token: 0x04001A64 RID: 6756
		private const float FADE_TIME = 2f;

		// Token: 0x04001A65 RID: 6757
		public static DialogCanvas instance;

		// Token: 0x04001A66 RID: 6758
		public BaseDialogActor[] actors;

		// Token: 0x04001A67 RID: 6759
		public RectTransform dialogParent;

		// Token: 0x04001A68 RID: 6760
		public GameObject dialogPanel;

		// Token: 0x04001A69 RID: 6761
		public Text characterText;

		// Token: 0x04001A6A RID: 6762
		public Text dialogText;

		// Token: 0x04001A6B RID: 6763
		public RawImage effectImage;

		// Token: 0x04001A6C RID: 6764
		public Dialog dialog;

		// Token: 0x04001A6D RID: 6765
		public int startLineIndex;

		// Token: 0x04001A6E RID: 6766
		public float textScreenSizePercentage = 0.2f;

		// Token: 0x04001A6F RID: 6767
		public bool quitToMenuWhenDone;

		// Token: 0x04001A70 RID: 6768
		private int animationAdvanceEvents;

		// Token: 0x04001A71 RID: 6769
		private BaseDialogActor lineActor;

		// Token: 0x04001A72 RID: 6770
		private Dialog.DialogLine currentLine;

		// Token: 0x04001A73 RID: 6771
		private int lineIndex;

		// Token: 0x04001A74 RID: 6772
		private float currentLineStartTime;

		// Token: 0x04001A75 RID: 6773
		private string currentLineCumulativeString;

		// Token: 0x04001A76 RID: 6774
		private int currentFormattedLineLength;

		// Token: 0x04001A77 RID: 6775
		private TimedAction effectAction = new TimedAction(1f, false);

		// Token: 0x04001A78 RID: 6776
		private TimedAction blipCooldown = new TimedAction(0.1f, false);

		// Token: 0x04001A79 RID: 6777
		private bool effectDoneLastFrame = true;

		// Token: 0x04001A7A RID: 6778
		private Dialog.DialogLine.Effect activeEffect;
	}
}
