using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000188 RID: 392
public class PointMatch : GameModeBase
{
	// Token: 0x06000A32 RID: 2610 RVA: 0x0006E0B4 File Offset: 0x0006C2B4
	private void AddScore(int blue, int red)
	{
		if (Benchmark.isRunning)
		{
			return;
		}
		this.blueScore += blue * PointMatch.ScoreMultiplier(this.blueFlags);
		this.redScore += red * PointMatch.ScoreMultiplier(this.redFlags);
		if (blue > 0)
		{
			this.bluePulse.Start();
		}
		if (red > 0)
		{
			this.redPulse.Start();
		}
		this.UpdateUi();
		if (!VictoryUi.GameHasEnded())
		{
			if (this.blueScore >= this.redScore + this.victoryPoints)
			{
				this.Win(0);
				return;
			}
			if (this.redScore >= this.blueScore + this.victoryPoints)
			{
				this.Win(1);
			}
		}
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0006E164 File Offset: 0x0006C364
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		BattleResult.SetWinner(1 - num);
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0006E180 File Offset: 0x0006C380
	private void UpdateFlags(bool initialOwner)
	{
		if (Benchmark.isRunning)
		{
			return;
		}
		this.blueFlags = 0;
		this.redFlags = 0;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (!spawnPoint.isGhostSpawn)
			{
				if (spawnPoint.owner == 0)
				{
					this.blueFlags++;
				}
				else if (spawnPoint.owner == 1)
				{
					this.redFlags++;
				}
			}
		}
		this.UpdateUi();
		if (!initialOwner && !VictoryUi.GameHasEnded())
		{
			if (!ActorManager.TeamHasAnySpawnPoint(0))
			{
				VictoryUi.EndGame(1, true);
				return;
			}
			if (!ActorManager.TeamHasAnySpawnPoint(1))
			{
				VictoryUi.EndGame(0, true);
			}
		}
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00008D0C File Offset: 0x00006F0C
	public static int ScoreMultiplier(int flags)
	{
		return flags;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0006E228 File Offset: 0x0006C428
	protected override void Awake()
	{
		base.Awake();
		this.blueScore = 0;
		this.redScore = 0;
		this.blueFlags = 0;
		this.redFlags = 0;
		int gameLength = GameManager.GameParameters().gameLength;
		if (gameLength == 0)
		{
			this.victoryPoints = 150;
		}
		else if (gameLength == 1)
		{
			this.victoryPoints = 300;
		}
		else if (gameLength == 2)
		{
			this.victoryPoints = 600;
		}
		else
		{
			this.victoryPoints = 2000;
		}
		this.blue = this.blueBar.color;
		this.red = this.redBar.color;
		this.UpdateUi();
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00008D0F File Offset: 0x00006F0F
	public override void StartGame()
	{
		base.StartGame();
		ActorManager.instance.CreateDefaultAiActors(false);
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00008D22 File Offset: 0x00006F22
	public override void PlayerAcceptedLoadoutFirstTime()
	{
		base.PlayerAcceptedLoadoutFirstTime();
		base.Invoke("Tooltip", 4f);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00008D3A File Offset: 0x00006F3A
	private void Tooltip()
	{
		OverlayUi.ShowOverlayText("SCORE " + this.victoryPoints.ToString() + " POINTS MORE THAN THE ENEMY TO WIN", 5f);
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0006E2C8 File Offset: 0x0006C4C8
	private void UpdateUi()
	{
		this.blueScoreText.text = this.blueScore.ToString();
		this.redScoreText.text = this.redScore.ToString();
		this.blueFlagsText.text = this.blueFlags.ToString();
		this.redFlagsText.text = this.redFlags.ToString();
		bool flag = this.blueScore + this.redScore >= this.victoryPoints;
		this.intercept.enabled = flag;
		if (!flag)
		{
			float x = (float)this.blueScore / (float)this.victoryPoints;
			float x2 = 1f - (float)this.redScore / (float)this.victoryPoints;
			this.blueBar.rectTransform.anchorMax = new Vector2(x, 1f);
			this.redBar.rectTransform.anchorMin = new Vector2(x2, 0f);
			return;
		}
		float x3 = Mathf.Clamp01((float)(this.blueScore - this.redScore + this.victoryPoints) / (float)(2 * this.victoryPoints));
		this.blueBar.rectTransform.anchorMax = new Vector2(x3, 1f);
		this.redBar.rectTransform.anchorMin = new Vector2(x3, 0f);
		this.intercept.rectTransform.anchorMin = new Vector2(x3, 0f);
		this.intercept.rectTransform.anchorMax = new Vector2(x3, 1f);
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0006E444 File Offset: 0x0006C644
	private void Update()
	{
		if (!this.bluePulse.Done())
		{
			this.blueBar.color = Color.Lerp(Color.white, this.blue, this.bluePulse.Ratio());
		}
		if (!this.redPulse.Done())
		{
			this.redBar.color = Color.Lerp(Color.white, this.red, this.redPulse.Ratio());
		}
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00008D60 File Offset: 0x00006F60
	public override void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
	{
		base.ActorDied(actor, position, wasSilentKill);
		if (!wasSilentKill)
		{
			this.AddScore((actor.team == 1) ? 1 : 0, (actor.team == 0) ? 1 : 0);
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00008D8D File Offset: 0x00006F8D
	public override void CapturePointChangedOwner(CapturePoint capturePoint, int oldOwner, bool initialOwner)
	{
		base.CapturePointChangedOwner(capturePoint, oldOwner, initialOwner);
		this.UpdateFlags(initialOwner);
	}

	// Token: 0x04000B26 RID: 2854
	public Text blueScoreText;

	// Token: 0x04000B27 RID: 2855
	public Text redScoreText;

	// Token: 0x04000B28 RID: 2856
	public Text blueFlagsText;

	// Token: 0x04000B29 RID: 2857
	public Text redFlagsText;

	// Token: 0x04000B2A RID: 2858
	public Image blueBar;

	// Token: 0x04000B2B RID: 2859
	public Image redBar;

	// Token: 0x04000B2C RID: 2860
	public Image intercept;

	// Token: 0x04000B2D RID: 2861
	private int blueScore;

	// Token: 0x04000B2E RID: 2862
	private int redScore;

	// Token: 0x04000B2F RID: 2863
	private int blueFlags;

	// Token: 0x04000B30 RID: 2864
	private int redFlags;

	// Token: 0x04000B31 RID: 2865
	private int victoryPoints;

	// Token: 0x04000B32 RID: 2866
	private Color blue;

	// Token: 0x04000B33 RID: 2867
	private Color red;

	// Token: 0x04000B34 RID: 2868
	private TimedAction bluePulse = new TimedAction(0.5f, false);

	// Token: 0x04000B35 RID: 2869
	private TimedAction redPulse = new TimedAction(0.5f, false);
}
