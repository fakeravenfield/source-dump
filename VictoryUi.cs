using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E3 RID: 739
public class VictoryUi : MonoBehaviour
{
	// Token: 0x06001392 RID: 5010 RVA: 0x0000FA6D File Offset: 0x0000DC6D
	private void Awake()
	{
		VictoryUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		VictoryUi.instance.victoryContainer.SetActive(false);
		this.gameEnded = false;
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x0000FA98 File Offset: 0x0000DC98
	public static bool GameHasEnded()
	{
		return VictoryUi.instance.gameEnded;
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000936B8 File Offset: 0x000918B8
	public static void EndGame(int winner, bool allowContinueBattle = true)
	{
		if (VictoryUi.instance.gameEnded)
		{
			return;
		}
		bool flag = allowContinueBattle && !GameManager.IsPlayingCampaign() && (Options.GetToggle(OptionToggle.Id.NeverendingBattles) || GameManager.IsSpectating());
		if (GameManager.IsSpectating())
		{
			VictoryUi.instance.audio.PlayOneShot(VictoryUi.instance.victoryClip);
		}
		else
		{
			VictoryUi.instance.audio.PlayOneShot((winner == GameManager.PlayerTeam()) ? VictoryUi.instance.victoryClip : VictoryUi.instance.defeatClip);
		}
		if (GameManager.IsSpectating())
		{
			VictoryUi.instance.victoryText.text = ((winner == 0) ? "EAGLE VICTORY" : "RAVEN VICTORY");
		}
		else if (winner == GameManager.PlayerTeam())
		{
			VictoryUi.instance.victoryText.text = "VICTORY";
		}
		else
		{
			VictoryUi.instance.victoryText.text = "DEFEAT";
		}
		VictoryUi.instance.gameEnded = true;
		VictoryUi.instance.victoryContainer.SetActive(true);
		VictoryUi.instance.victoryText.CrossFadeAlpha(0f, 0f, true);
		VictoryUi.instance.victoryLine.CrossFadeAlpha(0f, 0f, true);
		VictoryUi.instance.victoryBackground.CrossFadeAlpha(0f, 0f, true);
		if (GameManager.OnWin(winner, flag))
		{
			if (flag)
			{
				VictoryUi.PeekVictoryGraphics();
				return;
			}
			FpsActorController.instance.PlayVictoryCameraAnimation();
		}
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
	public static void FadeInVictoryGraphics()
	{
		VictoryUi.instance.StartCoroutine(VictoryUi.instance.FadeInVictoryGraphicsCoroutine());
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x0000FABB File Offset: 0x0000DCBB
	private IEnumerator FadeInVictoryGraphicsCoroutine()
	{
		this.victoryBackground.CrossFadeAlpha(0.6f, 4f, false);
		this.victoryLine.CrossFadeAlpha(1f, 2f, false);
		this.scaleVictoryLineAction.Start();
		yield return new WaitForSeconds(2f);
		this.victoryText.CrossFadeAlpha(1f, 2f, false);
		yield break;
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x0000FACA File Offset: 0x0000DCCA
	public static void PeekVictoryGraphics()
	{
		VictoryUi.instance.StartCoroutine(VictoryUi.instance.PeekVictoryGraphicsCoroutine());
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
	private IEnumerator PeekVictoryGraphicsCoroutine()
	{
		this.victoryBackground.CrossFadeAlpha(0.2f, 4f, false);
		this.victoryLine.CrossFadeAlpha(1f, 2f, false);
		this.scaleVictoryLineAction.Start();
		yield return new WaitForSeconds(2f);
		this.victoryText.CrossFadeAlpha(1f, 2f, false);
		yield return new WaitForSeconds(4f);
		this.victoryText.CrossFadeAlpha(0f, 2f, false);
		this.victoryBackground.CrossFadeAlpha(0f, 2f, false);
		this.victoryLine.CrossFadeAlpha(0f, 2f, false);
		yield return new WaitForSeconds(2f);
		this.victoryContainer.SetActive(false);
		yield break;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x00093820 File Offset: 0x00091A20
	private void Update()
	{
		float num = this.scaleVictoryLineAction.TrueDone() ? 1f : Mathf.SmoothStep(0f, 1f, this.scaleVictoryLineAction.Ratio());
		this.victoryLine.rectTransform.anchorMin = new Vector2(0.5f - num / 2f, 0f);
		this.victoryLine.rectTransform.anchorMax = new Vector2(0.5f + num / 2f, 0f);
	}

	// Token: 0x04001509 RID: 5385
	public static VictoryUi instance;

	// Token: 0x0400150A RID: 5386
	public GameObject victoryContainer;

	// Token: 0x0400150B RID: 5387
	public RawImage victoryBackground;

	// Token: 0x0400150C RID: 5388
	public RawImage victoryLine;

	// Token: 0x0400150D RID: 5389
	public Text victoryText;

	// Token: 0x0400150E RID: 5390
	public AudioSource audio;

	// Token: 0x0400150F RID: 5391
	public AudioClip victoryClip;

	// Token: 0x04001510 RID: 5392
	public AudioClip defeatClip;

	// Token: 0x04001511 RID: 5393
	private bool gameEnded;

	// Token: 0x04001512 RID: 5394
	private TimedAction scaleVictoryLineAction = new TimedAction(4f, false);

	// Token: 0x04001513 RID: 5395
	private Canvas canvas;
}
