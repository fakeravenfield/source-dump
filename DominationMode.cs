using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017C RID: 380
public class DominationMode : GameModeBase
{
	// Token: 0x060009DB RID: 2523 RVA: 0x0006C880 File Offset: 0x0006AA80
	protected override void Awake()
	{
		base.Awake();
		this.infoText.gameObject.SetActive(false);
		GameModeParameters gameModeParameters = GameManager.GameParameters();
		if (gameModeParameters.useConquestRules)
		{
			this.remainingBattalions[0] = Mathf.Clamp(gameModeParameters.conquestBattalions[0], 0, 3);
			this.remainingBattalions[1] = Mathf.Clamp(gameModeParameters.conquestBattalions[1], 0, 3);
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0006C8E0 File Offset: 0x0006AAE0
	public override void StartGame()
	{
		base.StartGame();
		ActorManager.instance.CreateDefaultAiActors(false);
		this.indicatorBlips = new RawImage[3];
		for (int i = 0; i < 3; i++)
		{
			this.indicatorBlips[i] = MinimapUi.AddGenericBlip(this.minimapIndicatorBlipTexture, Vector2.zero, new Vector2(32f, 32f));
			this.indicatorBlips[i].color = this.blipColor;
		}
		this.UpdateBattalionsUI();
		this.InitializeFlagSets();
		this.GenerateNewRound(-1);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0006C964 File Offset: 0x0006AB64
	private void InitializeFlagSets()
	{
		IEnumerable<CapturePoint> enumerable = from s in ActorManager.instance.spawnPoints
		where s is CapturePoint && !s.isGhostSpawn
		select s as CapturePoint;
		HashSet<DominationMode.FlagSet> hashSet = new HashSet<DominationMode.FlagSet>();
		foreach (CapturePoint capturePoint in enumerable)
		{
			foreach (CapturePoint capturePoint2 in enumerable)
			{
				if (!(capturePoint == capturePoint2) && capturePoint.IsNeighborTo(capturePoint2))
				{
					foreach (CapturePoint capturePoint3 in enumerable)
					{
						if (!(capturePoint == capturePoint3) && !(capturePoint2 == capturePoint3) && capturePoint.IsNeighborTo(capturePoint3))
						{
							capturePoint2.IsNeighborTo(capturePoint3);
							DominationMode.FlagSet item = new DominationMode.FlagSet(new CapturePoint[]
							{
								capturePoint,
								capturePoint2,
								capturePoint3
							});
							hashSet.Add(item);
						}
					}
				}
			}
		}
		this.allFlagSets = new List<DominationMode.FlagSet>(hashSet);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0006CAE8 File Offset: 0x0006ACE8
	public void GenerateNewRound(int favorTeam)
	{
		DominationMode.FlagSet flagSet = this.allFlagSets[0];
		float num = float.MinValue;
		for (int i = 0; i < 2; i++)
		{
			this.dominationRatio[i] = 0f;
		}
		this.barsHaveMet = false;
		foreach (DominationMode.FlagSet flagSet2 in this.allFlagSets)
		{
			float num2 = flagSet2.GetScore(favorTeam);
			if (flagSet2.Equals(this.activeFlagSet))
			{
				num2 = float.MinValue;
			}
			if (num2 > num)
			{
				num = num2;
				flagSet = flagSet2;
			}
		}
		this.SetActiveFlagSet(flagSet);
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x000089F1 File Offset: 0x00006BF1
	private void SetActiveFlagSet(DominationMode.FlagSet set)
	{
		this.activeFlagSet = set;
		this.ResetFlagUI();
		this.animateFlagContainerAction.Start();
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0006CBA8 File Offset: 0x0006ADA8
	private void ResetFlagUI()
	{
		for (int i = this.flagUIContainer.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.flagUIContainer.GetChild(i).gameObject);
		}
		foreach (CapturePoint flag in this.activeFlagSet.flags)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.flagUIEntry, this.flagUIContainer).GetComponent<DominationFlagEntry>().Initialize(flag);
		}
		for (int k = 0; k < this.activeFlagSet.flags.Length; k++)
		{
			Vector2 vector = MinimapCamera.WorldToNormalizedPosition(this.activeFlagSet.flags[k].transform.position);
			this.indicatorBlips[k].rectTransform.anchorMin = vector;
			this.indicatorBlips[k].rectTransform.anchorMax = vector;
		}
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0006CC88 File Offset: 0x0006AE88
	public override int ModifyOrderPriority(Order order, int currentPriority)
	{
		CapturePoint capturePoint = order.target as CapturePoint;
		if (order.type != Order.OrderType.Attack && order.type != Order.OrderType.Defend && order.type != Order.OrderType.Move)
		{
			return currentPriority;
		}
		if (capturePoint == null || !this.activeFlagSet.Contains(capturePoint))
		{
			return currentPriority - 6;
		}
		return currentPriority;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00008A0B File Offset: 0x00006C0B
	public override void PlayerAcceptedLoadoutFirstTime()
	{
		base.PlayerAcceptedLoadoutFirstTime();
		this.StartDominationRoundDelayed();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00008A19 File Offset: 0x00006C19
	private void StartDominationRoundDelayed()
	{
		base.StartCoroutine(this.StartDominationRoundRoutine());
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00008A28 File Offset: 0x00006C28
	private IEnumerator StartDominationRoundRoutine()
	{
		this.startDominationAction.Start();
		this.infoText.gameObject.SetActive(true);
		yield return new WaitForSeconds(90f);
		this.infoText.gameObject.SetActive(false);
		this.runDomination = true;
		this.dominationStartTime = Time.time;
		yield break;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00008A37 File Offset: 0x00006C37
	private void LateUpdate()
	{
		if (this.runDomination)
		{
			this.UpdateDomination();
		}
		this.UpdateBarUI();
		this.UpdateFlagUI();
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0006CCE0 File Offset: 0x0006AEE0
	private void UpdateFlagUI()
	{
		if (!this.animateFlagContainerAction.Done())
		{
			float num = Mathf.SmoothStep(0.2f, 0f, this.animateFlagContainerAction.Ratio());
			Vector2 anchorMin = this.flagUIContainer.anchorMin;
			Vector2 anchorMax = this.flagUIContainer.anchorMax;
			anchorMin.x = num;
			anchorMax.x = 1f - num;
			this.flagUIContainer.anchorMin = anchorMin;
			this.flagUIContainer.anchorMax = anchorMax;
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0006CD5C File Offset: 0x0006AF5C
	private void UpdateDomination()
	{
		int[] array = new int[2];
		foreach (CapturePoint capturePoint in this.activeFlagSet.flags)
		{
			if (capturePoint.owner >= 0)
			{
				array[capturePoint.owner]++;
			}
		}
		int num = -1;
		if (array[0] > array[1])
		{
			num = 0;
		}
		else if (array[0] < array[1])
		{
			num = 1;
		}
		float t = Mathf.Clamp01((Time.time - this.dominationStartTime - 60f) / 240f);
		float num2 = Mathf.Lerp(1f, 3f, t);
		if (!this.barsHaveMet || num > -1)
		{
			for (int j = 0; j < 2; j++)
			{
				if (array[j] > 0)
				{
					this.dominationRatio[j] += DominationMode.DOMINATION_SPEED[array[j] - 1] * num2 * Time.deltaTime;
					if (this.dominationRatio[j] >= 1f)
					{
						this.EndDominationRound(j);
					}
				}
			}
		}
		bool[] array2 = new bool[]
		{
			array[0] > 0,
			array[1] > 0
		};
		if (this.dominationRatio[0] + this.dominationRatio[1] > 1f)
		{
			this.barsHaveMet = true;
			if (num == 0)
			{
				this.dominationRatio[1] = 1f - this.dominationRatio[0];
				array2[1] = false;
			}
			else if (num == 1)
			{
				this.dominationRatio[0] = 1f - this.dominationRatio[1];
				array2[0] = false;
			}
		}
		for (int k = 0; k < 2; k++)
		{
			this.arrow[k].SetActive(array2[k]);
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0006CEF8 File Offset: 0x0006B0F8
	private void EndDominationRound(int winner)
	{
		int num = 1 - winner;
		this.remainingBattalions[num]--;
		if (this.remainingBattalions[num] == 0)
		{
			this.Win(winner);
			return;
		}
		this.UpdateBattalionsUI();
		this.runDomination = false;
		this.GenerateNewRound(num);
		this.StartDominationRoundDelayed();
		for (int i = 0; i < 2; i++)
		{
			this.arrow[i].SetActive(false);
		}
		OverlayUi.ShowOverlayText(GameManager.instance.GetRichTextColorTeamName(num) + " LOST A BATTALION", 3.5f);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00008A53 File Offset: 0x00006C53
	public override void Win(int winningTeam)
	{
		VictoryUi.EndGame(winningTeam, true);
		BattleResult.SetWinner(winningTeam);
		BattleResult.AppendBattalionResult(this.remainingBattalions[0], this.remainingBattalions[1]);
		BattleResult.latest.remainingBattalions[1 - winningTeam] = 0;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0006CF80 File Offset: 0x0006B180
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		this.Win(1 - num);
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0006CF9C File Offset: 0x0006B19C
	private void UpdateBattalionsUI()
	{
		for (int i = 0; i < this.battalionIndicatorsBlue.Length; i++)
		{
			this.battalionIndicatorsBlue[i].SetActive(i < this.remainingBattalions[0]);
			this.battalionIndicatorsRed[i].SetActive(i < this.remainingBattalions[1]);
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0006CFEC File Offset: 0x0006B1EC
	private void UpdateBarUI()
	{
		this.dominationBar[0].anchorMin = new Vector2(0f, 0f);
		this.dominationBar[0].anchorMax = new Vector2(this.dominationRatio[0], 1f);
		this.dominationBar[1].anchorMin = new Vector2(1f - this.dominationRatio[1], 0f);
		this.dominationBar[1].anchorMax = new Vector2(1f, 1f);
		float x = 2f * Mathf.Sin(Time.time * 5f);
		(this.arrow[0].transform as RectTransform).anchoredPosition = new Vector2(x, 0f);
		(this.arrow[1].transform as RectTransform).anchoredPosition = new Vector2(x, 0f);
		for (int i = 0; i < 3; i++)
		{
			this.indicatorBlips[i].rectTransform.localEulerAngles = new Vector3(0f, 0f, Time.time * 10f);
		}
		if (!this.startDominationAction.TrueDone())
		{
			this.infoText.text = "DOMINATION STARTS IN " + Mathf.CeilToInt(this.startDominationAction.Remaining()).ToString();
		}
	}

	// Token: 0x04000AC5 RID: 2757
	private const float ALL_NEIGHBORS_SET_SCORE = 1f;

	// Token: 0x04000AC6 RID: 2758
	private const float NOT_ALL_NEIGHBORS_SET_SCORE = 0f;

	// Token: 0x04000AC7 RID: 2759
	private const float ALL_NEUTRAL_SCORE = 10f;

	// Token: 0x04000AC8 RID: 2760
	private const float NEW_ROUND_START_TIME = 90f;

	// Token: 0x04000AC9 RID: 2761
	private const float SPEEDUP_CAPTURE_MULTIPLIER = 3f;

	// Token: 0x04000ACA RID: 2762
	private const float SPEEDUP_CAPTURE_START_TIME = 60f;

	// Token: 0x04000ACB RID: 2763
	private const float SPEEDUP_CAPTURE_MAX_TIME = 300f;

	// Token: 0x04000ACC RID: 2764
	private static readonly float[] DOMINATION_SPEED = new float[]
	{
		0.004761905f,
		0.0055555557f,
		0.01f
	};

	// Token: 0x04000ACD RID: 2765
	public GameObject flagUIEntry;

	// Token: 0x04000ACE RID: 2766
	public RectTransform flagUIContainer;

	// Token: 0x04000ACF RID: 2767
	public RectTransform[] dominationBar;

	// Token: 0x04000AD0 RID: 2768
	public GameObject[] arrow;

	// Token: 0x04000AD1 RID: 2769
	public GameObject[] battalionIndicatorsBlue;

	// Token: 0x04000AD2 RID: 2770
	public GameObject[] battalionIndicatorsRed;

	// Token: 0x04000AD3 RID: 2771
	public Texture2D minimapIndicatorBlipTexture;

	// Token: 0x04000AD4 RID: 2772
	public Color blipColor;

	// Token: 0x04000AD5 RID: 2773
	public Text infoText;

	// Token: 0x04000AD6 RID: 2774
	private RawImage[] indicatorBlips;

	// Token: 0x04000AD7 RID: 2775
	private List<DominationMode.FlagSet> allFlagSets;

	// Token: 0x04000AD8 RID: 2776
	private DominationMode.FlagSet activeFlagSet;

	// Token: 0x04000AD9 RID: 2777
	private int[] remainingBattalions = new int[]
	{
		3,
		3
	};

	// Token: 0x04000ADA RID: 2778
	private float[] dominationRatio = new float[2];

	// Token: 0x04000ADB RID: 2779
	private bool runDomination;

	// Token: 0x04000ADC RID: 2780
	private bool barsHaveMet;

	// Token: 0x04000ADD RID: 2781
	private float dominationStartTime;

	// Token: 0x04000ADE RID: 2782
	private TimedAction animateFlagContainerAction = new TimedAction(0.5f, false);

	// Token: 0x04000ADF RID: 2783
	private TimedAction startDominationAction = new TimedAction(90f, false);

	// Token: 0x0200017D RID: 381
	public struct FlagSet
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x0006D19C File Offset: 0x0006B39C
		public FlagSet(params CapturePoint[] flags)
		{
			this.flags = flags;
			this.baseScore = 1f;
			for (int i = 0; i < flags.Length; i++)
			{
				int num = (i + 1) % flags.Length;
				if (!this.flags[i].IsNeighborTo(this.flags[num]))
				{
					this.baseScore = 0f;
					return;
				}
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0006D1F4 File Offset: 0x0006B3F4
		public override int GetHashCode()
		{
			int num = 0;
			if (this.flags == null)
			{
				return 0;
			}
			foreach (CapturePoint y in this.flags)
			{
				for (int j = 0; j < ActorManager.instance.spawnPoints.Length; j++)
				{
					if (ActorManager.instance.spawnPoints[j] == y)
					{
						num |= 1 << j;
					}
				}
			}
			return num;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0006D264 File Offset: 0x0006B464
		public override bool Equals(object obj)
		{
			return obj is DominationMode.FlagSet && ((DominationMode.FlagSet)obj).GetHashCode() == this.GetHashCode();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0006D2A0 File Offset: 0x0006B4A0
		public bool Contains(CapturePoint flag)
		{
			CapturePoint[] array = this.flags;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0006D2D0 File Offset: 0x0006B4D0
		public float GetScore(int favorTeam)
		{
			float num = 0f;
			if (favorTeam >= 0)
			{
				if (favorTeam == 0)
				{
					num = -1.5f;
				}
				else
				{
					num = 1.5f;
				}
			}
			float num2 = this.baseScore;
			bool flag = true;
			foreach (CapturePoint capturePoint in this.flags)
			{
				if (!capturePoint.isActiveAndEnabled)
				{
					return float.MinValue;
				}
				if (capturePoint.owner == 0)
				{
					num += 1f;
					flag = false;
				}
				else if (capturePoint.owner == 1)
				{
					num -= 1f;
					flag = false;
				}
				foreach (SpawnPoint spawnPoint in capturePoint.allNeighbors)
				{
					if (spawnPoint.owner == 0)
					{
						num += 0.2f;
					}
					else if (spawnPoint.owner == 1)
					{
						num -= 0.2f;
					}
				}
				if (!capturePoint.IsSafe())
				{
					num2 += 1f;
				}
			}
			if (flag)
			{
				num2 += 10f;
			}
			num2 -= Mathf.Abs(num);
			return num2 + UnityEngine.Random.Range(0f, 0.5f);
		}

		// Token: 0x04000AE0 RID: 2784
		public CapturePoint[] flags;

		// Token: 0x04000AE1 RID: 2785
		private float baseScore;
	}
}
