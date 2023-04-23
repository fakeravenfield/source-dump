using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D6 RID: 726
public class SpawnCountdownUi : MonoBehaviour
{
	// Token: 0x0600133E RID: 4926 RVA: 0x0000F4FC File Offset: 0x0000D6FC
	public static void OnDeath()
	{
		SpawnCountdownUi.instance.respawnHintAction.Start();
		SpawnCountdownUi.instance.uiIsActive = true;
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x0000F518 File Offset: 0x0000D718
	private void Awake()
	{
		SpawnCountdownUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.uiIsActive = false;
		this.showHints = Options.GetToggle(OptionToggle.Id.ControlHints);
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x00092480 File Offset: 0x00090680
	private void LateUpdate()
	{
		if (!GameModeBase.instance.allowDefaultRespawn || FpsActorController.instance.actor.isDeactivated)
		{
			this.canvas.enabled = false;
			return;
		}
		this.canvas.enabled = (this.uiIsActive && FpsActorController.instance.actor.dead && !LoadoutUi.IsOpen() && !GameManager.IsSpectating());
		if (!this.uiIsActive)
		{
			return;
		}
		bool flag = FpsActorController.instance.SelectedSpawnPoint() != null && FpsActorController.instance.SelectedSpawnPoint().owner == GameManager.PlayerTeam();
		this.respawnTimeText.enabled = (flag && GameModeBase.instance.ShowRemainingTimeToPlayerSpawn());
		if (this.showHints)
		{
			bool flag2 = flag || LoadoutUi.IsOpen() || !FpsActorController.instance.actor.dead;
			if (this.respawnHintAction.TrueDone() && !flag2)
			{
				this.ShowRespawnHint();
			}
			else if (flag2)
			{
				KeyboardGlyphGenerator.Hide(this.hintID);
			}
		}
		if (this.respawnTimeText.enabled)
		{
			this.respawnTimeText.text = string.Format("SPAWNING AT {0} IN {1}", FpsActorController.instance.SelectedSpawnPoint().shortName, GameModeBase.instance.TimeToPlayerRespawn());
		}
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000925CC File Offset: 0x000907CC
	private void ShowRespawnHint()
	{
		if (FpsActorController.controlHints.respawn.TryConsume())
		{
			this.hintID = KeyboardGlyphGenerator.instance.ShowBind(SteelInput.KeyBinds.Fire, "Respawn");
		}
		else if (FpsActorController.controlHints.loadout.TryConsume())
		{
			this.hintID = KeyboardGlyphGenerator.instance.ShowBind(SteelInput.KeyBinds.OpenLoadout, "Change Loadout");
		}
		this.respawnHintAction.StartLifetime(15f);
	}

	// Token: 0x040014B5 RID: 5301
	public static SpawnCountdownUi instance;

	// Token: 0x040014B6 RID: 5302
	public Text respawnTimeText;

	// Token: 0x040014B7 RID: 5303
	private Canvas canvas;

	// Token: 0x040014B8 RID: 5304
	private TimedAction respawnHintAction = new TimedAction(5f, false);

	// Token: 0x040014B9 RID: 5305
	private int hintID = -1;

	// Token: 0x040014BA RID: 5306
	private bool uiIsActive;

	// Token: 0x040014BB RID: 5307
	private bool showHints;
}
