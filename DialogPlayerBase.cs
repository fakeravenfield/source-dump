using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CE RID: 206
[RequireComponent(typeof(AudioSource))]
public class DialogPlayerBase : MonoBehaviour
{
	// Token: 0x06000640 RID: 1600 RVA: 0x00005E63 File Offset: 0x00004063
	public virtual void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0005EF38 File Offset: 0x0005D138
	public static void SetFontSize(Text target, int textLines)
	{
		target.fontSize = 20;
		Rect rect = target.rectTransform.rect;
		float preferredHeight = target.cachedTextGenerator.GetPreferredHeight("|", target.GetGenerationSettings(rect.size));
		float num = 20f / preferredHeight;
		int fontSize = Mathf.FloorToInt(rect.height / (float)textLines * num);
		target.fontSize = fontSize;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00005E71 File Offset: 0x00004071
	public virtual void OnPlayBlipSound()
	{
		this.audioSource.PlayOneShot(this.blipSound);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0000296E File Offset: 0x00000B6E
	public virtual void OnPrintDone()
	{
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00005E84 File Offset: 0x00004084
	public static bool GetFastForwardKey()
	{
		return Input.GetKey(KeyCode.LeftShift);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00005E90 File Offset: 0x00004090
	public static bool GetNextLineInput()
	{
		return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00005EAC File Offset: 0x000040AC
	private static bool IsMetaCharacter(char character)
	{
		return character == '\\' || character == '<' || character == '>';
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00005EBF File Offset: 0x000040BF
	private static bool IsSeparator(char character)
	{
		return character == '.' || character == '!' || character == '?';
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00005ED2 File Offset: 0x000040D2
	public void StartPrinting(Text target, string text, int startIndex, bool canFastForward)
	{
		if (this.printTextCoroutine != null)
		{
			base.StopCoroutine(this.printTextCoroutine);
			this.printTextCoroutine = null;
		}
		this.printTextCoroutine = base.StartCoroutine(this.PrintTextCoroutine(target, text, startIndex, canFastForward));
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00005F06 File Offset: 0x00004106
	private IEnumerator PrintTextCoroutine(Text target, string text, int startIndex, bool canFastForward)
	{
		if (!string.IsNullOrEmpty(text))
		{
			target.text = text.Substring(0, startIndex + 1);
			this.isPrintingText = true;
			yield return new WaitForSeconds(0.1f);
			TimedAction blipCooldown = new TimedAction(1f, false);
			int num;
			for (int i = startIndex; i < text.Length; i = num + 1)
			{
				char character = text[i];
				target.text = text.Substring(0, i + 1);
				this.isPrintingNormalCharacter = (!DialogPlayerBase.IsSeparator(character) && !DialogPlayerBase.IsMetaCharacter(character));
				if (this.isPrintingNormalCharacter && blipCooldown.TrueDone())
				{
					blipCooldown.StartLifetime(UnityEngine.Random.Range(0.03f, 0.06f));
					this.OnPlayBlipSound();
				}
				if (!DialogPlayerBase.IsMetaCharacter(character))
				{
					float seconds = 0.03f;
					if (canFastForward && DialogPlayerBase.GetFastForwardKey())
					{
						seconds = 0.001f;
					}
					else if (DialogPlayerBase.IsSeparator(character))
					{
						seconds = 0.5f;
					}
					yield return new WaitForSeconds(seconds);
				}
				num = i;
			}
		}
		this.isPrintingText = false;
		this.OnPrintDone();
		this.printTextCoroutine = null;
		yield break;
	}

	// Token: 0x04000626 RID: 1574
	private const float PRINT_START_DURATION = 0.1f;

	// Token: 0x04000627 RID: 1575
	private const float PRINT_CHARACTER_DURATION = 0.03f;

	// Token: 0x04000628 RID: 1576
	private const float PRINT_SEPARATOR_DURATION = 0.5f;

	// Token: 0x04000629 RID: 1577
	private const float PRINT_FAST_FORWARD_DURATION = 0.001f;

	// Token: 0x0400062A RID: 1578
	private const float BLIP_COOLDOWN_MIN = 0.03f;

	// Token: 0x0400062B RID: 1579
	private const float BLIP_COOLDOWN_MAX = 0.06f;

	// Token: 0x0400062C RID: 1580
	private const int BASE_FONT_SIZE = 20;

	// Token: 0x0400062D RID: 1581
	[NonSerialized]
	public AudioSource audioSource;

	// Token: 0x0400062E RID: 1582
	public AudioClip blipSound;

	// Token: 0x0400062F RID: 1583
	[NonSerialized]
	public bool isPrintingText;

	// Token: 0x04000630 RID: 1584
	[NonSerialized]
	public bool isPrintingNormalCharacter;

	// Token: 0x04000631 RID: 1585
	private Coroutine printTextCoroutine;
}
