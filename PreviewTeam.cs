using System;
using Lua;
using UnityEngine;

// Token: 0x020001E6 RID: 486
[Serializable]
public class PreviewTeam
{
	// Token: 0x06000CFF RID: 3327 RVA: 0x0007ADDC File Offset: 0x00078FDC
	public void SetupDefaultValues()
	{
		this.actorMeshRenderers = new SkinnedMeshRenderer[this.actors.Length];
		this.actorAnimators = new Animator[this.actors.Length];
		for (int i = 0; i < this.actors.Length; i++)
		{
			this.actorMeshRenderers[i] = this.actors[i].GetComponentInChildren<SkinnedMeshRenderer>();
			this.actorAnimators[i] = this.actors[i].GetComponent<Animator>();
			this.actorAnimators[i].SetInteger("stance", i);
		}
		this.defaultActorMesh = this.actorMeshRenderers[0].sharedMesh;
		this.defaultActorMaterials = new Material[this.actorMeshRenderers[0].materials.Length];
		this.actorMeshRenderers[0].materials.CopyTo(this.defaultActorMaterials, 0);
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0000A982 File Offset: 0x00008B82
	public void Update(TeamInfo info, int team)
	{
		this.UpdateCharacterSkins(info, team);
		this.UpdateWeapons(info, team);
		this.UpdateVehicles(info, team);
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0007AEA8 File Offset: 0x000790A8
	public void UpdateCharacterSkins(TeamInfo info, int team)
	{
		if (info.skin == null || info.skin.characterSkin == null)
		{
			this.SetCharacterSkin(new ActorSkin.MeshSkin(this.defaultActorMesh, this.defaultActorMaterials, 0), team);
			return;
		}
		this.SetCharacterSkin(info.skin.characterSkin, team);
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0007AEF8 File Offset: 0x000790F8
	private void UpdateWeapons(TeamInfo info, int team)
	{
		WeaponManager.WeaponEntry[] array = new WeaponManager.WeaponEntry[3];
		float[] array2 = new float[3];
		for (int i = 0; i < 3; i++)
		{
			array[i] = null;
			array2[i] = 0f;
		}
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (info.IsWeaponEntryAvailable(weaponEntry) && weaponEntry.usableByAi)
			{
				int num;
				float num2;
				if (weaponEntry.slot == WeaponManager.WeaponSlot.Primary)
				{
					num = 0;
					num2 = 1f;
				}
				else if (weaponEntry.type != WeaponManager.WeaponEntry.LoadoutType.AntiArmor)
				{
					num = 1;
					num2 = 1f;
				}
				else
				{
					num = 2;
					num2 = 1f;
				}
				if (num != -1 && num2 > array2[num])
				{
					array2[num] = num2;
					array[num] = weaponEntry;
				}
			}
		}
		WeaponManager.WeaponEntry weaponEntry2 = null;
		for (int j = 0; j < 3; j++)
		{
			if (array[j] != null)
			{
				weaponEntry2 = array[j];
				break;
			}
		}
		for (int k = 0; k < 3; k++)
		{
			WeaponManager.WeaponEntry weaponEntry3 = array[k];
			if (weaponEntry3 == null)
			{
				weaponEntry3 = weaponEntry2;
			}
			this.UpdateWeaponEntry(weaponEntry3, this.weaponParents[k], this.actorAnimators[k]);
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0007B034 File Offset: 0x00079234
	private void CullUnsafeObjects(GameObject parentObject)
	{
		Animator[] components = parentObject.GetComponents<Animator>();
		ScriptedBehaviour[] components2 = parentObject.GetComponents<ScriptedBehaviour>();
		Canvas[] components3 = parentObject.GetComponents<Canvas>();
		Animator[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		ScriptedBehaviour[] array2 = components2;
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object.Destroy(array2[i]);
		}
		Canvas[] array3 = components3;
		for (int i = 0; i < array3.Length; i++)
		{
			UnityEngine.Object.Destroy(array3[i]);
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0007B0A4 File Offset: 0x000792A4
	private void UpdateWeaponEntry(WeaponManager.WeaponEntry entry, Transform parent, Animator animator)
	{
		for (int i = parent.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(parent.GetChild(i).gameObject);
		}
		if (entry == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(entry.prefab, parent);
		this.CullUnsafeObjects(gameObject);
		Weapon component = gameObject.GetComponent<Weapon>();
		component.CullFpsObjects();
		component.transform.localPosition = Vector3.zero;
		component.transform.localRotation = Quaternion.identity;
		component.transform.localScale = Vector3.one;
		component.thirdPersonTransform.localPosition = component.thirdPersonOffset;
		component.thirdPersonTransform.localEulerAngles = component.thirdPersonRotation;
		component.thirdPersonTransform.localScale = new Vector3(component.thirdPersonScale, component.thirdPersonScale, component.thirdPersonScale);
		animator.SetInteger("pose", component.configuration.pose);
		GameManager.SetupRecursiveLayer(component.transform, 30);
		GamePreview.DestroyComponents<Weapon>(gameObject);
		GamePreview.RecursiveStripComponents(component.transform);
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0007B1A4 File Offset: 0x000793A4
	private void UpdateVehicles(TeamInfo info, int team)
	{
		GamePreview.UpdateVehiclePrefab(info.vehiclePrefab[VehicleSpawner.VehicleSpawnType.Jeep], this.jeep, team);
		GamePreview.UpdateVehiclePrefab(info.vehiclePrefab[VehicleSpawner.VehicleSpawnType.Tank], this.tank, team);
		GamePreview.UpdateVehiclePrefab(info.vehiclePrefab[VehicleSpawner.VehicleSpawnType.AttackPlane], this.plane, team);
		GamePreview.UpdateVehiclePrefab(info.vehiclePrefab[VehicleSpawner.VehicleSpawnType.AttackHelicopter], this.helicopter, team);
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0007B214 File Offset: 0x00079414
	private void SetCharacterSkin(ActorSkin.MeshSkin skin, int team)
	{
		SkinnedMeshRenderer[] array = this.actorMeshRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			ActorManager.ApplyOverrideMeshSkin(array[i], skin, team);
		}
	}

	// Token: 0x04000DFB RID: 3579
	public Transform[] actors;

	// Token: 0x04000DFC RID: 3580
	public Transform[] weaponParents;

	// Token: 0x04000DFD RID: 3581
	public Transform jeep;

	// Token: 0x04000DFE RID: 3582
	public Transform tank;

	// Token: 0x04000DFF RID: 3583
	public Transform helicopter;

	// Token: 0x04000E00 RID: 3584
	public Transform plane;

	// Token: 0x04000E01 RID: 3585
	[NonSerialized]
	public Mesh defaultActorMesh;

	// Token: 0x04000E02 RID: 3586
	[NonSerialized]
	public Material[] defaultActorMaterials;

	// Token: 0x04000E03 RID: 3587
	[NonSerialized]
	public SkinnedMeshRenderer[] actorMeshRenderers;

	// Token: 0x04000E04 RID: 3588
	[NonSerialized]
	public Animator[] actorAnimators;
}
