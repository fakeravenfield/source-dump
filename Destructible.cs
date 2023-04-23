using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000139 RID: 313
public class Destructible : MonoBehaviour
{
	// Token: 0x060008D3 RID: 2259 RVA: 0x00007C62 File Offset: 0x00005E62
	private void Start()
	{
		if (this.takeSplashDamage)
		{
			ActorManager.instance.OnExplosion.AddListener(new UnityAction<ExplosionInfo>(this.OnExplosion));
		}
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00069734 File Offset: 0x00067934
	private void OnExplosion(ExplosionInfo info)
	{
		if (!this.IsDamagedByRating(info.damageRating))
		{
			return;
		}
		DamageInfo info2 = ActorManager.EvaluateLastExplosionDamage(base.transform.position, true);
		info2.healthDamage *= this.GetDamageMultiplier(info.damageRating);
		this.Damage(info2);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00007C87 File Offset: 0x00005E87
	public bool IsDamagedByRating(Vehicle.ArmorRating damageRating)
	{
		return damageRating >= this.armorDamagedBy;
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00007C95 File Offset: 0x00005E95
	public virtual void Damage(DamageInfo info)
	{
		this.health -= info.healthDamage;
		if (!this.isDead)
		{
			IngameUi.OnDamageDealt(info, new HitInfo(this));
			if (this.health <= 0f)
			{
				this.Die();
			}
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00007CD1 File Offset: 0x00005ED1
	public float GetDamageMultiplier(Vehicle.ArmorRating sourceDamageRating)
	{
		if (sourceDamageRating == Vehicle.ArmorRating.SmallArms)
		{
			return this.smallArmsMultiplier;
		}
		if (sourceDamageRating == Vehicle.ArmorRating.HeavyArms)
		{
			return this.heavyArmsMultiplier;
		}
		return this.antiTankMultiplier;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00007CEE File Offset: 0x00005EEE
	public bool IsShatteredByRating(Vehicle.ArmorRating damageRating)
	{
		return this.canInstantShatter && damageRating >= this.armorInstantShatteredBy;
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00007D06 File Offset: 0x00005F06
	public void Shatter()
	{
		this.Die();
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00069780 File Offset: 0x00067980
	private void Die()
	{
		ActorManager.instance.OnExplosion.RemoveListener(new UnityAction<ExplosionInfo>(this.OnExplosion));
		this.isDead = true;
		try
		{
			GameObject[] array = this.disableOnDeath;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			foreach (GameObject gameObject in this.activateOnDeath)
			{
				gameObject.SetActive(true);
				Rigidbody component = gameObject.GetComponent<Rigidbody>();
				if (component != null && !component.isKinematic)
				{
					gameObject.transform.parent = null;
					this.SetupActivatedRigidbody(component);
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00007D0E File Offset: 0x00005F0E
	protected virtual void SetupActivatedRigidbody(Rigidbody rigidbody)
	{
		rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 3f, ForceMode.VelocityChange);
	}

	// Token: 0x04000989 RID: 2441
	public float smallArmsMultiplier = 1f;

	// Token: 0x0400098A RID: 2442
	public float heavyArmsMultiplier = 1f;

	// Token: 0x0400098B RID: 2443
	public float antiTankMultiplier = 1f;

	// Token: 0x0400098C RID: 2444
	public Vehicle.ArmorRating armorDamagedBy = Vehicle.ArmorRating.HeavyArms;

	// Token: 0x0400098D RID: 2445
	public bool canInstantShatter;

	// Token: 0x0400098E RID: 2446
	public Vehicle.ArmorRating armorInstantShatteredBy = Vehicle.ArmorRating.HeavyArms;

	// Token: 0x0400098F RID: 2447
	public GameObject[] activateOnDeath;

	// Token: 0x04000990 RID: 2448
	public GameObject[] disableOnDeath;

	// Token: 0x04000991 RID: 2449
	public bool showHitIndicator = true;

	// Token: 0x04000992 RID: 2450
	public bool takeSplashDamage;

	// Token: 0x04000993 RID: 2451
	public float health = 300f;

	// Token: 0x04000994 RID: 2452
	[NonSerialized]
	public bool isDead;
}
