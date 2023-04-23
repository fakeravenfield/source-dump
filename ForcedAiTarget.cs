using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class ForcedAiTarget : Actor
{
	// Token: 0x06000499 RID: 1177 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void Awake()
	{
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00004E79 File Offset: 0x00003079
	private new void Start()
	{
		base.Invoke("SetAlive", 0.1f);
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00004E8B File Offset: 0x0000308B
	private void SetAlive()
	{
		ActorManager.SetAlive(this);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0000296E File Offset: 0x00000B6E
	protected override void FixedUpdate()
	{
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00004E93 File Offset: 0x00003093
	public override void Update()
	{
		base.Highlight(4f);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00004EA0 File Offset: 0x000030A0
	public override Vector3 CenterPosition()
	{
		return base.transform.position;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00004EA0 File Offset: 0x000030A0
	public override Vector3 Position()
	{
		return base.transform.position;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00004EAD File Offset: 0x000030AD
	public override Vector3 Velocity()
	{
		return this.velocity;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00004EB5 File Offset: 0x000030B5
	public override Actor.TargetType GetTargetType()
	{
		return this.type;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00004EBD File Offset: 0x000030BD
	public override bool CanBeDamagedBy(Weapon weapon)
	{
		return weapon.projectileArmorRating >= this.armorRating;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x000561B4 File Offset: 0x000543B4
	public override bool Damage(DamageInfo info)
	{
		this.hits++;
		Debug.Log(string.Concat(new string[]
		{
			"Took damage: ",
			info.healthDamage.ToString(),
			" (balance: ",
			info.balanceDamage.ToString(),
			" Hits: ",
			this.hits.ToString()
		}));
		return true;
	}

	// Token: 0x0400048F RID: 1167
	public Vehicle.ArmorRating armorRating;

	// Token: 0x04000490 RID: 1168
	public Actor.TargetType type;

	// Token: 0x04000491 RID: 1169
	public Vector3 velocity = Vector3.zero;

	// Token: 0x04000492 RID: 1170
	private int hits;
}
