using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class WeaponPickup : MonoBehaviour
{
	// Token: 0x06000CBA RID: 3258 RVA: 0x00079E4C File Offset: 0x0007804C
	private void Start()
	{
		if (this.modelParent != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.entry.prefab, this.modelParent);
			Weapon component = gameObject.GetComponent<Weapon>();
			component.CullFpsObjects();
			Transform thirdPersonTransform = component.thirdPersonTransform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			thirdPersonTransform.localPosition = Vector3.zero;
			thirdPersonTransform.localEulerAngles = component.thirdPersonRotation;
			thirdPersonTransform.transform.localScale = new Vector3(component.thirdPersonScale, component.thirdPersonScale, component.thirdPersonScale);
			UnityEngine.Object.Destroy(component);
			foreach (Renderer renderer in this.modelParent.GetComponentsInChildren<Renderer>())
			{
				Material[] array = new Material[renderer.materials.Length];
				for (int j = 0; j < renderer.materials.Length; j++)
				{
					array[j] = this.blueprintMaterial;
				}
				renderer.materials = array;
			}
		}
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00079F60 File Offset: 0x00078160
	private void Update()
	{
		Vector3 vector = base.transform.position - FpsActorController.instance.actor.Position();
		float magnitude = vector.ToGround().magnitude;
		if (Mathf.Abs(vector.y) < 2f && magnitude < 1f)
		{
			this.PickupEntry();
			if (this.audio != null)
			{
				this.audio.Play();
			}
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00079FE0 File Offset: 0x000781E0
	private void PickupEntry()
	{
		if (this.affectSquadmates && FpsActorController.instance.playerSquad != null)
		{
			using (List<ActorController>.Enumerator enumerator = FpsActorController.instance.playerSquad.members.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActorController actorController = enumerator.Current;
					this.GiveEntryToActor(actorController.actor);
				}
				return;
			}
		}
		this.GiveEntryToActor(FpsActorController.instance.actor);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0007A068 File Offset: 0x00078268
	private void GiveEntryToActor(Actor actor)
	{
		Weapon weapon = actor.EquipNewWeaponEntry(this.entry, this.slot, this.autoEquip);
		if (weapon.configuration.spareAmmo > 0 && weapon.configuration.ammo > 0)
		{
			int spareAmmo = Mathf.RoundToInt((float)weapon.configuration.spareAmmo * this.spareAmmoMultiplier / (float)weapon.configuration.ammo) * weapon.configuration.ammo;
			weapon.configuration.spareAmmo = spareAmmo;
			if (!actor.aiControlled)
			{
				FpsActorController.instance.actor.UpdateAmmoUi();
			}
		}
	}

	// Token: 0x04000DB8 RID: 3512
	private const int PLAYER_LAYER = 9;

	// Token: 0x04000DB9 RID: 3513
	public Material blueprintMaterial;

	// Token: 0x04000DBA RID: 3514
	public WeaponManager.WeaponEntry entry;

	// Token: 0x04000DBB RID: 3515
	public int slot;

	// Token: 0x04000DBC RID: 3516
	public bool autoEquip;

	// Token: 0x04000DBD RID: 3517
	public bool affectSquadmates;

	// Token: 0x04000DBE RID: 3518
	public Transform modelParent;

	// Token: 0x04000DBF RID: 3519
	public AudioSource audio;

	// Token: 0x04000DC0 RID: 3520
	public float spareAmmoMultiplier = 1f;
}
