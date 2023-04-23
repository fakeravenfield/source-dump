using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D1 RID: 721
public class ScoreboardActorEntry : MonoBehaviour
{
	// Token: 0x06001326 RID: 4902 RVA: 0x0000F34B File Offset: 0x0000D54B
	public void SetActor(Actor actor)
	{
		this.actor = actor;
		this.UpdateNameLabel();
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x0000F35A File Offset: 0x0000D55A
	public void UpdateNameLabel()
	{
		this.nameText.text = this.actor.name;
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x0000F372 File Offset: 0x0000D572
	public void EnableHighlight()
	{
		base.GetComponent<Image>().enabled = true;
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x0000F380 File Offset: 0x0000D580
	public void AddKill()
	{
		this.kills++;
		this.kText.text = this.kills.ToString();
		this.Sort();
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0000F3AC File Offset: 0x0000D5AC
	public void SubtractKill()
	{
		this.kills--;
		this.kText.text = this.kills.ToString();
		this.Sort();
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
	public void AddDeath()
	{
		this.deaths++;
		this.dText.text = this.deaths.ToString();
		this.Sort();
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x0000F404 File Offset: 0x0000D604
	public void AddFlag()
	{
		this.flagCaptures++;
		this.fText.text = this.flagCaptures.ToString();
		this.Sort();
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x0000F430 File Offset: 0x0000D630
	public void AddVehicle()
	{
		this.vehicleKills++;
		this.vText.text = this.vehicleKills.ToString();
		this.Sort();
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x0000F45C File Offset: 0x0000D65C
	public void Sort()
	{
		this.sortScore = this.kills + this.flagCaptures + this.vehicleKills;
		ScoreboardUi.Sort(this);
	}

	// Token: 0x04001498 RID: 5272
	public Text nameText;

	// Token: 0x04001499 RID: 5273
	public Text kText;

	// Token: 0x0400149A RID: 5274
	public Text dText;

	// Token: 0x0400149B RID: 5275
	public Text fText;

	// Token: 0x0400149C RID: 5276
	public Text vText;

	// Token: 0x0400149D RID: 5277
	[NonSerialized]
	public int kills;

	// Token: 0x0400149E RID: 5278
	[NonSerialized]
	public int deaths;

	// Token: 0x0400149F RID: 5279
	[NonSerialized]
	public int flagCaptures;

	// Token: 0x040014A0 RID: 5280
	[NonSerialized]
	public int vehicleKills;

	// Token: 0x040014A1 RID: 5281
	[NonSerialized]
	public int sortScore;

	// Token: 0x040014A2 RID: 5282
	[NonSerialized]
	public Actor actor;
}
