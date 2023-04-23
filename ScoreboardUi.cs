using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class ScoreboardUi : MonoBehaviour
{
	// Token: 0x06001330 RID: 4912 RVA: 0x000922C0 File Offset: 0x000904C0
	public static void AddEntryForActor(Actor actor)
	{
		ScoreboardActorEntry component = UnityEngine.Object.Instantiate<GameObject>(ScoreboardUi.instance.scoreboardEntryPrefab, (actor.team == 1) ? ScoreboardUi.instance.team1Panel : ScoreboardUi.instance.team0Panel).GetComponent<ScoreboardActorEntry>();
		component.SetActor(actor);
		if (!actor.aiControlled)
		{
			component.EnableHighlight();
		}
		actor.scoreboardEntry = component;
		ScoreboardUi.instance.entriesOfTeam[actor.team].Add(component);
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x00092338 File Offset: 0x00090538
	public static void Sort(ScoreboardActorEntry entry)
	{
		ScoreboardUi.instance.entriesOfTeam[entry.actor.team].Sort((ScoreboardActorEntry x, ScoreboardActorEntry y) => y.sortScore.CompareTo(x.sortScore));
		entry.transform.SetSiblingIndex(ScoreboardUi.instance.entriesOfTeam[entry.actor.team].IndexOf(entry));
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0000F47E File Offset: 0x0000D67E
	public static bool IsOpenAndFocused()
	{
		return ScoreboardUi.instance.canvas.enabled && ScoreboardUi.instance.isFocused;
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000923B0 File Offset: 0x000905B0
	private void Awake()
	{
		ScoreboardUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.entriesOfTeam = new Dictionary<int, List<ScoreboardActorEntry>>();
		this.entriesOfTeam.Add(0, new List<ScoreboardActorEntry>());
		this.entriesOfTeam.Add(1, new List<ScoreboardActorEntry>());
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x0000F49D File Offset: 0x0000D69D
	public static bool GetOpenInput(out bool isFocused)
	{
		isFocused = SteelInput.GetButton(SteelInput.KeyBinds.Scoreboard);
		return isFocused || SteelInput.GetButton(SteelInput.KeyBinds.PeekScoreboard);
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x000923FC File Offset: 0x000905FC
	private void Update()
	{
		bool flag;
		bool openInput = ScoreboardUi.GetOpenInput(out flag);
		this.isFocused = flag;
		this.canvas.enabled = (openInput && ScoreboardUi.activeScoreboardType == ScoreboardUi.ActiveScoreboardUI.Scoreboard);
	}

	// Token: 0x040014A3 RID: 5283
	public static ScoreboardUi instance;

	// Token: 0x040014A4 RID: 5284
	public static ScoreboardUi.ActiveScoreboardUI activeScoreboardType;

	// Token: 0x040014A5 RID: 5285
	public GameObject scoreboardEntryPrefab;

	// Token: 0x040014A6 RID: 5286
	public Transform team0Panel;

	// Token: 0x040014A7 RID: 5287
	public Transform team1Panel;

	// Token: 0x040014A8 RID: 5288
	private Canvas canvas;

	// Token: 0x040014A9 RID: 5289
	private Dictionary<int, List<ScoreboardActorEntry>> entriesOfTeam;

	// Token: 0x040014AA RID: 5290
	private bool isFocused;

	// Token: 0x020002D3 RID: 723
	public enum ActiveScoreboardUI
	{
		// Token: 0x040014AC RID: 5292
		Scoreboard,
		// Token: 0x040014AD RID: 5293
		Objective,
		// Token: 0x040014AE RID: 5294
		None
	}
}
