using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lua;
using Lua.Wrapper;
using MapEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

// Token: 0x020001AE RID: 430
public class GameManager : MonoBehaviour
{
	// Token: 0x06000B6C RID: 2924 RVA: 0x0000973C File Offset: 0x0000793C
	public static void DebugVerbose(object message)
	{
		if (GameManager.verboseLogging)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0000974B File Offset: 0x0000794B
	public static void DebugVerbose(string format, params object[] args)
	{
		if (GameManager.verboseLogging)
		{
			Debug.LogFormat(format, args);
		}
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x0000975B File Offset: 0x0000795B
	public static bool IsInAssetBundleLevel()
	{
		return GameManager.instance.levelBundle != null;
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x0000976D File Offset: 0x0000796D
	public static bool IsLegitimate()
	{
		return GameManager.IsConnectedToSteam() || GameManager.instance.transform.GetChild(GameManager.instance.transform.childCount - 1).GetHashCode() != 3145580;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00074ED4 File Offset: 0x000730D4
	public static void UpdateSoundOutputGroupCombat(AudioSource audioSource, float distance, bool canSee)
	{
		bool flag = distance < 80f;
		if (canSee)
		{
			audioSource.outputAudioMixerGroup = (flag ? GameManager.instance.combatMixerGroup : GameManager.instance.combatFarMixerGroup);
			return;
		}
		audioSource.outputAudioMixerGroup = (flag ? GameManager.instance.combatMuffledMixerGroup : GameManager.instance.combatMuffledFarMixerGroup);
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00074F2C File Offset: 0x0007312C
	public static AudioMixerGroup GetSoundOutputGroupVehicle(float distance, bool playerIsInside, bool canSee)
	{
		if (playerIsInside)
		{
			if (!FpsActorController.instance.actor.seat.enclosed)
			{
				return GameManager.instance.playerVehicleMixerGroup;
			}
			return GameManager.instance.worldMixerGroup;
		}
		else
		{
			bool flag = distance < 80f;
			if (canSee)
			{
				if (!flag)
				{
					return GameManager.instance.vehiclesFarMixerGroup;
				}
				return GameManager.instance.vehiclesMixerGroup;
			}
			else
			{
				if (!flag)
				{
					return GameManager.instance.vehiclesFarMixerGroup;
				}
				return GameManager.instance.vehiclesMixerGroup;
			}
		}
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00074FA8 File Offset: 0x000731A8
	public static bool OnWin(int winner, bool continueNeverendingBattle)
	{
		RavenscriptManager.events.onMatchEnd.Invoke((WTeam)winner);
		if (RavenscriptManager.events.onMatchEnd.isConsumed)
		{
			return false;
		}
		if (!continueNeverendingBattle)
		{
			GameManager.gameOver = true;
			KillIndicatorUi.Hide();
			KillCamera.Hide();
		}
		Type type = GameModeBase.instance.GetType();
		if (GameManager.triggeredUfo && !GameManager.IsInCustomLevel() && SceneManager.GetActiveScene().name.ToLowerInvariant() == "coastline" && (type == typeof(PointMatch) || type == typeof(BattleMode)) && GameManager.PlayerTeam() == winner && GameManager.PlayerTeam() == 0)
		{
			GameManager.instance.Invoke("CoastlineHqCall", 7f);
			WeaponManager.UnlockSecretWeapon();
		}
		return true;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x00075070 File Offset: 0x00073270
	public static Camera GetMainCamera()
	{
		Camera mainManagedCamera = GameManager.GetMainManagedCamera();
		if (mainManagedCamera != null)
		{
			return mainManagedCamera;
		}
		return Camera.main;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00075094 File Offset: 0x00073294
	public static Camera GetMainManagedCamera()
	{
		if (GameManager.IsSpectating())
		{
			return SpectatorCamera.instance.camera;
		}
		Camera activeCamera = FpsActorController.instance.GetActiveCamera();
		if (activeCamera != null && activeCamera.enabled)
		{
			return activeCamera;
		}
		if (SpectatorCamera.instance != null && SpectatorCamera.instance.camera != null && SpectatorCamera.instance.camera.enabled)
		{
			return SpectatorCamera.instance.camera;
		}
		return null;
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x000097A7 File Offset: 0x000079A7
	public static Vector3 GetPlayerCameraPosition()
	{
		return GameManager.GetMainCamera().transform.position;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x000097B8 File Offset: 0x000079B8
	private void AutoStartMapWhenLoadingDone(string path)
	{
		this.autoStartMapArmed = true;
		this.autoStartMapPath = path;
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x000097C8 File Offset: 0x000079C8
	private void CoastlineHqCall()
	{
		UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.hqCallPrefab);
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x000097DA File Offset: 0x000079DA
	public static bool IsPlayingCampaign()
	{
		return GameManager.instance.isPlayingCampaign;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000097E6 File Offset: 0x000079E6
	public static bool IsTestingContentMod()
	{
		return GameManager.instance.testContentModMode;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00075110 File Offset: 0x00073310
	public static bool IsInCustomLevel()
	{
		return SceneManager.GetActiveScene().buildIndex == -1;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00075130 File Offset: 0x00073330
	public static bool IsIngame()
	{
		return !GameManager.instance.isInNoGameplayScene && (GameManager.IsInCustomLevel() || SceneManager.GetActiveScene().buildIndex > 2);
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00075164 File Offset: 0x00073364
	public static bool IsInMainMenu()
	{
		return SceneManager.GetActiveScene().buildIndex == 1;
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00075184 File Offset: 0x00073384
	public static bool IsInMapEditor()
	{
		return SceneManager.GetActiveScene().name == "MapEditor";
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x000751A8 File Offset: 0x000733A8
	public static bool IsInLoadingScreen()
	{
		return SceneManager.GetActiveScene().buildIndex == 2;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x000097F2 File Offset: 0x000079F2
	private static void OnExitGameScene()
	{
		GameManager.instance.CancelInvoke();
		ActorManager.OnExitGameScene();
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00009803 File Offset: 0x00007A03
	public static void ReturnToMenu()
	{
		if (GameManager.IsIngame())
		{
			GameManager.OnExitGameScene();
		}
		SceneManager.LoadScene(1);
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x00009817 File Offset: 0x00007A17
	public static void ReturnToCampaignLobby()
	{
		if (GameManager.IsIngame())
		{
			GameManager.OnExitGameScene();
		}
		SceneManager.LoadScene("CommandRoom");
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0000982F File Offset: 0x00007A2F
	public static void OnGameEnded()
	{
		if (GameManager.IsPlayingCampaign())
		{
			GameManager.ReturnToCampaignLobby();
			return;
		}
		GameManager.ReturnToMenu();
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00009843 File Offset: 0x00007A43
	public static void RestartLevel()
	{
		if (GameManager.IsIngame())
		{
			GameManager.instance.restartArmed = true;
			GameManager.ReturnToMenu();
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0000985C File Offset: 0x00007A5C
	public static void SetTeamColors(Color c1, Color c2)
	{
		ColorScheme.teamColors = new Color[]
		{
			c1,
			c2
		};
		DecalManager.SetBloodDecalColor(0, c1);
		DecalManager.SetBloodDecalColor(1, c2);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00009887 File Offset: 0x00007A87
	public static void CreateSplashEffect(bool large, Vector3 position)
	{
		UnityEngine.Object.Instantiate<GameObject>(large ? GameManager.instance.waterSplashLargePrefab : GameManager.instance.waterSplashPrefab, position, Quaternion.identity).AddComponent<AutoDestroy>().duration = 10f;
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x000751C8 File Offset: 0x000733C8
	public static void SetupVehiclePrefab(GameObject parentObject, ModContentInformation contentInfo)
	{
		try
		{
			GameManager.TryUpgradeVehicleComponent(parentObject);
			GameManager.SetWorldSoundMixForObject(parentObject);
			GameManager.SetupRecursiveLayer(parentObject.transform, 12);
			ModManager.PreprocessContentModPrefab(parentObject, contentInfo);
			Vehicle component = parentObject.GetComponent<Vehicle>();
			if (component.rigidbody != null && !component.rigidbody.isKinematic)
			{
				component.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
			if (component.aiUseStrategy == Vehicle.AiUseStrategy.Default)
			{
				if (component.IsAircraft() || component.aiType == Vehicle.AiType.Roam || component.aiType == Vehicle.AiType.Capture)
				{
					component.aiUseStrategy = Vehicle.AiUseStrategy.FromAnySpawn;
				}
				else
				{
					component.aiUseStrategy = Vehicle.AiUseStrategy.OnlyFromFrontlineSpawn;
				}
			}
			foreach (Seat seat in component.seats)
			{
				MountedWeapon[] weapons = seat.weapons;
				for (int i = 0; i < weapons.Length; i++)
				{
					GameManager.SetupWeaponPrefab(weapons[i], component, contentInfo);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x000752C4 File Offset: 0x000734C4
	private static void TryUpgradeVehicleComponent(GameObject prefab)
	{
		Vehicle component = prefab.GetComponent<Vehicle>();
		if (component != null && component is IAutoUpgradeComponent)
		{
			AutoUpgradeComponent.Upgrade(component);
		}
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x000752F0 File Offset: 0x000734F0
	public static void SetupWeaponPrefab(Weapon weapon, Vehicle mountedVehicle, ModContentInformation contentInfo)
	{
		ModManager.PreprocessContentModPrefab(weapon.gameObject, contentInfo);
		if (!weapon.IsMountedWeapon())
		{
			weapon.renderTextureCamera = weapon.GetComponentInChildren<Camera>();
		}
		else
		{
			MountedWeapon x = weapon as MountedWeapon;
			try
			{
				if (x != null && weapon.configuration.haltStrategy == Weapon.HaltStrategy.Auto && mountedVehicle.IsWatercraft())
				{
					weapon.configuration.haltStrategy = Weapon.HaltStrategy.Never;
				}
			}
			catch (Exception)
			{
			}
		}
		if (weapon.configuration.haltStrategy == Weapon.HaltStrategy.Auto && !weapon.configuration.auto && weapon.configuration.effectiveRange > 600f)
		{
			weapon.configuration.haltStrategy = Weapon.HaltStrategy.PreferredLongRange;
		}
		try
		{
			GameManager.UpdateWeaponDifficultyValues(weapon, mountedVehicle);
		}
		catch (Exception)
		{
		}
		AudioSource component = weapon.GetComponent<AudioSource>();
		if (component != null)
		{
			component.loop = weapon.configuration.auto;
		}
		if (weapon.configuration.projectilePrefab != null)
		{
			ModManager.PreprocessContentModPrefab(weapon.configuration.projectilePrefab, contentInfo);
			Projectile component2 = weapon.configuration.projectilePrefab.GetComponent<Projectile>();
			Renderer component3 = weapon.configuration.projectilePrefab.GetComponent<Renderer>();
			if (component3 != null && !(component2 is GrenadeProjectile) && !(component2 is RigidbodyProjectile))
			{
				component3.shadowCastingMode = ShadowCastingMode.Off;
			}
			if (weapon.configuration.haltStrategy == Weapon.HaltStrategy.Auto && component2.armorDamage == Vehicle.ArmorRating.AntiTank && weapon.configuration.effectiveRange > 100f)
			{
				weapon.configuration.haltStrategy = Weapon.HaltStrategy.PreferredLongRange;
			}
		}
		if (weapon.reflectionSound == Weapon.ReflectionSound.Auto)
		{
			weapon.AutoAssignWeaponReflectionSound();
		}
		GameManager.SetWorldSoundMixForObject(weapon.gameObject);
		if (weapon.configuration.projectilePrefab != null)
		{
			GameManager.SetWorldSoundMixForObject(weapon.configuration.projectilePrefab);
		}
		if (weapon.parentWeapon != null)
		{
			List<Weapon> list = new List<Weapon>();
			Weapon weapon2 = weapon;
			while (weapon2 != null)
			{
				if (list.Contains(weapon2))
				{
					weapon.parentWeapon = null;
					break;
				}
				list.Add(weapon2);
				weapon2 = weapon2.parentWeapon;
			}
		}
		if (contentInfo.HasPatchData())
		{
			GameManager.ApplyWeaponPatch(weapon, contentInfo);
		}
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0007550C File Offset: 0x0007370C
	private static void ApplyWeaponPatch(Weapon weapon, ModContentInformation contentInfo)
	{
		try
		{
			Animator component = weapon.GetComponent<Animator>();
			if (!weapon.IsMountedWeapon() && component != null && contentInfo.patchData.ContainsPatchedAnimations())
			{
				if (contentInfo.patchData.animationDatabase.ContainsPatchedAnimationClipsForController(component))
				{
					weapon.gameObject.AddComponent<PatchedAnimatorPlayback>().InitializePatchedPlayback(component, contentInfo.patchData.animationDatabase);
				}
				else
				{
					Debug.Log(contentInfo.bundlePath + " patch data did not contain animation clips for animator " + component.runtimeAnimatorController.name);
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x000755A8 File Offset: 0x000737A8
	public static Weapon.WeaponRole GenerateWeaponRole(Weapon weapon, Vehicle vehicle)
	{
		if (weapon.IsMeleeWeapon())
		{
			return Weapon.WeaponRole.Melee;
		}
		Projectile component = weapon.configuration.projectilePrefab.GetComponent<Projectile>();
		bool flag = weapon.configuration.auto || (weapon.configuration.cooldown < 0.7f && weapon.configuration.ammo > 2);
		bool flag2 = component is GrenadeProjectile;
		bool flag3 = component is RigidbodyProjectile;
		bool flag4 = component.configuration.speed > 150f;
		bool flag5 = component.configuration.damage >= 80f;
		bool flag6 = weapon.configuration.projectilesPerShot > 1;
		bool flag7 = component is ExplodingProjectile && (component as ExplodingProjectile).explosionConfiguration.damageRange > 1f;
		bool flag8 = component is TargetSeekingMissile;
		if (!flag7)
		{
			bool flag9 = component.armorDamage >= Vehicle.ArmorRating.HeavyArms;
		}
		bool flag10 = weapon is MountedWeapon && vehicle != null && vehicle.IsAircraft();
		bool flag11 = weapon is MountedStabilizedTurret || weapon is MountedTurret;
		bool flag12 = weapon is Mortar;
		bool flag13 = false;
		if (component is ProximityFuzeProjectile)
		{
			ProximityFuzeProjectile proximityFuzeProjectile = (ProximityFuzeProjectile)component;
			flag13 = (proximityFuzeProjectile.allowAllTargets || proximityFuzeProjectile.allowedTargetTypes.Contains(Actor.TargetType.Air) || proximityFuzeProjectile.allowedTargetTypes.Contains(Actor.TargetType.AirFastMover));
		}
		Weapon.WeaponRole result;
		if (flag2)
		{
			result = Weapon.WeaponRole.Grenade;
		}
		else if (flag3)
		{
			result = Weapon.WeaponRole.Utility;
		}
		else if (flag12)
		{
			result = Weapon.WeaponRole.Mortar;
		}
		else if (flag10 && !flag11 && !flag7)
		{
			result = Weapon.WeaponRole.DogfightGuns;
		}
		else if (!flag7)
		{
			if (flag)
			{
				result = Weapon.WeaponRole.AutoRifle;
			}
			else if (flag6)
			{
				result = Weapon.WeaponRole.Shotgun;
			}
			else if (flag5)
			{
				result = Weapon.WeaponRole.Sniper;
			}
			else
			{
				result = Weapon.WeaponRole.Handgun;
			}
		}
		else if (flag13)
		{
			result = Weapon.WeaponRole.AntiAir;
		}
		else if (flag8)
		{
			result = Weapon.WeaponRole.MissileLauncher;
		}
		else if (flag)
		{
			result = Weapon.WeaponRole.AutoCannon;
		}
		else if (flag4)
		{
			result = Weapon.WeaponRole.RocketLauncher;
		}
		else
		{
			result = Weapon.WeaponRole.GrenadeLauncher;
		}
		return result;
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0007578C File Offset: 0x0007398C
	private static void UpdateWeaponDifficultyValues(Weapon weapon, Vehicle vehicle)
	{
		if (!weapon.NeedsDifficultyGenerated())
		{
			return;
		}
		if (weapon.IsMeleeWeapon() || weapon is CarHorn)
		{
			GameManager.AssignWeaponDifficultyTrivial(weapon);
			return;
		}
		Weapon.WeaponRole weaponRole = GameManager.GenerateWeaponRole(weapon, vehicle);
		switch (weaponRole)
		{
		case Weapon.WeaponRole.AutoRifle:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Challenging);
			return;
		case Weapon.WeaponRole.Sniper:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Challenging, Weapon.Difficulty.Challenging, Weapon.Difficulty.Hard);
			return;
		case Weapon.WeaponRole.Handgun:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Challenging, Weapon.Difficulty.Hard, Weapon.Difficulty.Hard);
			return;
		case Weapon.WeaponRole.Shotgun:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Hard, Weapon.Difficulty.Hard);
			return;
		case Weapon.WeaponRole.AutoCannon:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Challenging, Weapon.Difficulty.Hard);
			return;
		case Weapon.WeaponRole.RocketLauncher:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Challenging, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Challenging, Weapon.Difficulty.Hard);
			return;
		case Weapon.WeaponRole.GrenadeLauncher:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Hard, Weapon.Difficulty.Hard);
			return;
		case Weapon.WeaponRole.MissileLauncher:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy);
			return;
		case Weapon.WeaponRole.AntiAir:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy);
			return;
		case Weapon.WeaponRole.DogfightGuns:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Hard, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy);
			return;
		case Weapon.WeaponRole.Utility:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy);
			return;
		case Weapon.WeaponRole.Grenade:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Challenging, Weapon.Difficulty.Impossible, Weapon.Difficulty.Impossible);
			return;
		case Weapon.WeaponRole.Mortar:
			GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Challenging, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Hard, Weapon.Difficulty.Hard);
			return;
		default:
			Debug.LogError("Unknown weapon role: " + weaponRole.ToString());
			return;
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x000098BC File Offset: 0x00007ABC
	private static void AssignWeaponDifficultyTrivial(Weapon weapon)
	{
		GameManager.AssignWeaponDifficulty(weapon, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy, Weapon.Difficulty.Easy);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000758B8 File Offset: 0x00073AB8
	private static void AssignWeaponDifficulty(Weapon weapon, Weapon.Difficulty infantry, Weapon.Difficulty infantryGroup, Weapon.Difficulty groundVehicle, Weapon.Difficulty air, Weapon.Difficulty airFastMover)
	{
		if (weapon.configuration.diffInfantry == Weapon.Difficulty.Auto)
		{
			weapon.configuration.diffInfantry = infantry;
		}
		if (weapon.configuration.diffInfantryGroup == Weapon.Difficulty.Auto)
		{
			weapon.configuration.diffInfantryGroup = infantryGroup;
		}
		if (weapon.configuration.diffGroundVehicles == Weapon.Difficulty.Auto)
		{
			weapon.configuration.diffGroundVehicles = groundVehicle;
		}
		if (weapon.configuration.diffAir == Weapon.Difficulty.Auto)
		{
			weapon.configuration.diffAir = air;
		}
		if (weapon.configuration.diffAirFastMover == Weapon.Difficulty.Auto)
		{
			weapon.configuration.diffAirFastMover = airFastMover;
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x000098C9 File Offset: 0x00007AC9
	public static void SetWorldSoundMixForObject(GameObject parentObject)
	{
		GameManager.SetWorldSoundMixForTransformRecursive(parentObject.transform);
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00075944 File Offset: 0x00073B44
	public static void SetWorldSoundMixForTransformRecursive(Transform transform)
	{
		AudioSource component = transform.GetComponent<AudioSource>();
		if (component != null)
		{
			component.outputAudioMixerGroup = GameManager.instance.worldMixerGroup;
		}
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameManager.SetWorldSoundMixForTransformRecursive(transform.GetChild(i));
		}
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00075990 File Offset: 0x00073B90
	public static void RecurseHierarchy(Transform root, GameManager.DelRecurseHierarchy callback)
	{
		callback(root);
		for (int i = 0; i < root.childCount; i++)
		{
			GameManager.RecurseHierarchy(root.GetChild(i), callback);
		}
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x000759C4 File Offset: 0x00073BC4
	public static void SetupRecursiveLayer(Transform transform, int layer)
	{
		transform.gameObject.layer = layer;
		for (int i = 0; i < transform.childCount; i++)
		{
			GameManager.SetupRecursiveLayer(transform.GetChild(i), layer);
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x000098D6 File Offset: 0x00007AD6
	public static GameObject GetGameModePrefab(GameModeType type)
	{
		return GameManager.instance.officialGameModePrefabs[(int)type];
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x000759FC File Offset: 0x00073BFC
	public static void StartLevel(InstantActionMaps.MapEntry entry, GameModeParameters parameters)
	{
		if (GameManager.IsTestingContentMod())
		{
			ModManager.instance.FinalizeLoadedModContent();
		}
		if (!GameManager.IsPlayingCampaign())
		{
			DateTime now = DateTime.Now;
			if (parameters.nightMode && now.Minute % 20 == 7)
			{
				parameters.halloweenMode = true;
			}
			else
			{
				parameters.halloweenMode = false;
			}
		}
		else
		{
			parameters.halloweenMode = false;
		}
		entry.nightVersion = parameters.nightMode;
		if (entry.IsRFLBundle())
		{
			GameManager.instance.levelVersionInfo = ModManager.ExtractBundleEditorVersion(entry.sceneName);
		}
		else
		{
			GameManager.instance.levelVersionInfo = ModManager.EngineVersionInfo.CurrentPlayer;
		}
		GameManager.instance.gameModeParameters = parameters;
		GameManager.instance.hasNonDefaultGameModeParameters = true;
		GameManager.instance.lastMapEntry = entry;
		SceneManager.LoadScene(2);
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x000098E4 File Offset: 0x00007AE4
	public static bool IsSpectating()
	{
		return GameManager.instance.gameModeParameters.playerTeam == -1;
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x000098F8 File Offset: 0x00007AF8
	public static GameModeParameters GameParameters()
	{
		return GameManager.instance.gameModeParameters;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00009904 File Offset: 0x00007B04
	public static int PlayerTeam()
	{
		return GameManager.instance.gameModeParameters.playerTeam;
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00009915 File Offset: 0x00007B15
	public static bool IsConnectedToSteam()
	{
		return GameManager.instance != null && GameManager.instance.steamworks != null && GameManager.instance.steamworks.isInitialized;
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00009941 File Offset: 0x00007B41
	public static void ShowConfigFlagsScreen()
	{
		UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.configureFlagPrefab);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00075AB8 File Offset: 0x00073CB8
	public static void PlayReflectionSound(bool playerWeapon, bool isAuto, Weapon.ReflectionSound reflectionType, float volume, Vector3 position)
	{
		if (reflectionType == Weapon.ReflectionSound.Auto || reflectionType == Weapon.ReflectionSound.None)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		float num3 = Vector3.Distance(position, FpsActorController.instance.actor.Position());
		float minDistance = 10f;
		float maxDistance = 200f;
		if (reflectionType == Weapon.ReflectionSound.Handgun)
		{
			num = 0;
			num2 = 1;
		}
		else if (reflectionType == Weapon.ReflectionSound.RifleSmall)
		{
			num = 1;
			num2 = 3;
		}
		else if (reflectionType == Weapon.ReflectionSound.RifleLarge)
		{
			num = 2;
			num2 = 8;
			minDistance = 20f;
			maxDistance = 300f;
		}
		else if (reflectionType == Weapon.ReflectionSound.Launcher)
		{
			num = 3;
			num2 = 15;
			minDistance = 40f;
			maxDistance = 400f;
		}
		else if (reflectionType == Weapon.ReflectionSound.Tank)
		{
			num = 4;
			num2 = 25;
			minDistance = 100f;
			maxDistance = 700f;
		}
		if (num3 < 30f)
		{
			num2 += 20;
		}
		else if (num3 < 60f)
		{
			num2 += 15;
		}
		else if (num3 < 100f)
		{
			num2 += 10;
		}
		if (isAuto)
		{
			num2 -= 8;
		}
		if (playerWeapon)
		{
			num2 = 1000;
		}
		AudioClip[] array = CoverManager.instance.IsInCqcCell(position) ? GameManager.instance.reflectionsClose : GameManager.instance.reflectionsOpen;
		for (int i = 0; i < GameManager.instance.reflectionAudioSources.Length; i++)
		{
			if (!GameManager.instance.reflectionAudioSources[i].isPlaying || num2 >= GameManager.instance.reflectionAudioPriority[i])
			{
				AudioSource audioSource = GameManager.instance.reflectionAudioSources[i];
				GameManager.instance.reflectionAudioPriority[i] = num2;
				audioSource.priority = (playerWeapon ? 50 : 255);
				audioSource.transform.position = position;
				audioSource.outputAudioMixerGroup = (playerWeapon ? GameManager.instance.worldMixerGroup : GameManager.instance.reflectionMixerGroup);
				audioSource.minDistance = minDistance;
				audioSource.maxDistance = maxDistance;
				audioSource.rolloffMode = AudioRolloffMode.Linear;
				audioSource.volume = volume;
				audioSource.Stop();
				audioSource.PlayOneShot(array[num]);
				return;
			}
		}
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00075C80 File Offset: 0x00073E80
	private void Awake()
	{
		GameManager.instance = this;
		Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.steamworks = new SteamworksWrapper();
		this.steamworks.Initialize();
		this.SetupDefaultGameParameters();
		this.reflectionAudioSources = this.reflectionAudioSourceParent.GetComponentsInChildren<AudioSource>();
		this.reflectionAudioPriority = new int[this.reflectionAudioSources.Length];
		SpriteActorDatabase[] array = this.defaultSpriteActorDatabases;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].InitializeRuntimeData();
		}
		try
		{
			this.SetupExternalFiles();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		SceneManager.sceneLoaded += this.OnLevelLoaded;
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00009953 File Offset: 0x00007B53
	private void SetupExternalFiles()
	{
		if (!Directory.Exists(SaveConfigurationPage.GetFolderPath()))
		{
			Directory.CreateDirectory(SaveConfigurationPage.GetFolderPath());
		}
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0000996C File Offset: 0x00007B6C
	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= this.OnLevelLoaded;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0000997F File Offset: 0x00007B7F
	private void OnApplicationQuit()
	{
		if (this.steamworks != null)
		{
			this.steamworks.Shutdown();
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00075D3C File Offset: 0x00073F3C
	public static string GenerateVersionString()
	{
		return string.Format("Ravenfield build {0}{1}, Engine Version {2}, BuildGUID {3}", new object[]
		{
			GameManager.instance.buildNumber,
			GameManager.instance.isBeta ? " (BETA)" : "",
			Application.unityVersion,
			Application.buildGUID
		});
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00075D98 File Offset: 0x00073F98
	private void Start()
	{
		IngameDebugGizmos.instance.enabled = this.testContentModMode;
		this.UpdateUIScale();
		this.gameInfo = GameInfoContainer.Default();
		if (EventSystem.current != null)
		{
			EventSystem.current.sendNavigationEvents = false;
		}
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			string[] array = commandLineArgs[i].Split(new char[]
			{
				' '
			});
			string text = "";
			for (int j = 1; j < array.Length; j++)
			{
				text += array[j];
				if (j < array.Length - 1)
				{
					text += " ";
				}
			}
			this.HandleArgument(array[0].ToLowerInvariant(), text);
		}
		ModManager.instance.OnGameManagerStart();
		if (this.autoStartMapArmed && ModManager.instance.contentHasFinishedLoading)
		{
			this.AutoStartArmedMap();
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00075E74 File Offset: 0x00074074
	private void HandleArgument(string argument, string parameter)
	{
		string message = string.Format("Handling argument: {0} {1}", argument, parameter);
		ScriptConsole.instance.LogInfo(message);
		Debug.Log(message);
		if (argument == "-generatenavcache")
		{
			Debug.Log("Generate cache mode enabled");
			this.generateNavCache = true;
			this.navCacheWritebackPath = parameter;
			ModManager.instance.noContentMods = true;
			return;
		}
		if (argument == "-custommap")
		{
			Debug.Log("Load custom map: " + parameter);
			InstantActionMaps.MapEntry mapEntry = new InstantActionMaps.MapEntry();
			mapEntry.isCustomMap = true;
			mapEntry.sceneName = parameter;
			this.SetupDefaultGameParameters();
			GameManager.StartLevel(mapEntry, this.gameModeParameters);
			return;
		}
		if (argument == "-editmap")
		{
			Debug.Log("Load custom map: " + parameter);
			InstantActionMaps.MapEntry entry = SceneConstructor.InstantActionMapsEntry(parameter, SceneConstructor.Mode.Edit);
			this.SetupDefaultGameParameters();
			GameManager.StartLevel(entry, this.gameModeParameters);
			return;
		}
		if (argument == "-map")
		{
			this.SetupDefaultGameParameters();
			this.AutoStartMapWhenLoadingDone(parameter);
			return;
		}
		if (argument == "-debuggizmos")
		{
			this.InitializeIngameDebugGizmos();
			return;
		}
		if (argument == "-nocontentmods")
		{
			ModManager.instance.noContentMods = true;
			return;
		}
		if (argument == "-noworkshopmods")
		{
			ModManager.instance.noWorkshopMods = true;
			return;
		}
		if (argument == "-verbose")
		{
			GameManager.verboseLogging = true;
			return;
		}
		if (argument == "-testcontentmod")
		{
			if (!this.testContentModMode)
			{
				this.testContentModMode = true;
				this.InitializeIngameDebugGizmos();
				this.InitializeTestContentModMode();
				ModManager.instance.ClearContentModData();
				this.gameInfo.LoadOfficial();
			}
			GC.Collect();
			long totalAllocatedMemoryLong = Profiler.GetTotalAllocatedMemoryLong();
			ModInformation mod = ModManager.instance.LoadSingleModContentBundle(parameter);
			GC.Collect();
			int num = (int)((Profiler.GetTotalAllocatedMemoryLong() - totalAllocatedMemoryLong) / 1000000L);
			ScriptConsole.instance.LogInfo("Loaded content mod: {0}, memory usage: {1} MB", new object[]
			{
				parameter,
				num
			});
			this.gameInfo.AdditiveLoadSingleMod(mod);
			return;
		}
		if (argument == "-benchmark")
		{
			new InstantActionMaps.MapEntry();
			if (!this.autoStartMapArmed)
			{
				this.AutoStartMapWhenLoadingDone("island");
			}
			int actorCount;
			if (!string.IsNullOrEmpty(parameter) && int.TryParse(parameter, out actorCount))
			{
				this.SetupBenchmarkGameParameters(actorCount);
			}
			else
			{
				this.SetupBenchmarkGameParameters(60);
			}
			Benchmark.isRunning = true;
			this.benchmarkMutator.isEnabled = true;
			ModManager.instance.builtInMutators.Add(this.benchmarkMutator);
			return;
		}
		if (argument == "-testsessionid")
		{
			RavenscriptManager.instance.OnStartTestSession(parameter);
			return;
		}
		if (argument == "-testsession")
		{
			RavenscriptManager.instance.OnStartTestSession(parameter);
			return;
		}
		if (argument == "-modstagingpath")
		{
			ModManager.instance.modStagingPathOverride = parameter;
			ScriptConsole.instance.LogInfo("Mod staging path set to: {0}", new object[]
			{
				ModManager.ModStagingPath()
			});
			return;
		}
		if (!(argument == "-nointro"))
		{
			if (argument == "-resetresolution")
			{
				Screen.SetResolution(1280, 720, false);
				return;
			}
			if (argument == "-strictmodversion")
			{
				ModManager.instance.strictModVersionFilter = true;
				return;
			}
			string message2 = string.Format("Unrecognized Argument {0}", argument);
			ScriptConsole.instance.LogInfo(message2);
			Debug.Log(message2);
		}
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00009994 File Offset: 0x00007B94
	private void InitializeIngameDebugGizmos()
	{
		IngameDebugGizmos.instance.enabled = true;
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x000099A1 File Offset: 0x00007BA1
	private void InitializeTestContentModMode()
	{
		ModManager.instance.noContentMods = true;
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x000099AE File Offset: 0x00007BAE
	public void OnAllContentLoaded()
	{
		if (!GameManager.IsTestingContentMod())
		{
			this.gameInfo = GameInfoContainer.Default();
		}
		if (this.autoStartMapArmed)
		{
			this.AutoStartArmedMap();
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x000099D0 File Offset: 0x00007BD0
	private void AutoStartArmedMap()
	{
		this.autoStartMapArmed = false;
		GameManager.StartLevel(new InstantActionMaps.MapEntry
		{
			isCustomMap = this.autoStartMapPath.Contains("."),
			sceneName = this.autoStartMapPath
		}, this.gameModeParameters);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00076198 File Offset: 0x00074398
	private void SetupDefaultGameParameters()
	{
		this.gameModeParameters.playerHasAllWeapons = true;
		this.gameModeParameters.noVehicles = false;
		this.gameModeParameters.nightMode = false;
		this.gameModeParameters.configFlags = false;
		this.gameModeParameters.playerTeam = 0;
		this.gameModeParameters.balance = 0.5f;
		this.gameModeParameters.gameLength = 2;
		this.gameModeParameters.noTurrets = false;
		this.gameModeParameters.respawnTime = 5;
		this.gameModeParameters.reverseMode = false;
		this.gameModeParameters.bloodExplosions = false;
		this.gameModeParameters.gameModePrefab = this.defaultGameModePrefab;
		if (this.testContentModMode)
		{
			this.gameModeParameters.actorCount = 0;
			return;
		}
		this.gameModeParameters.actorCount = 50;
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00009A0B File Offset: 0x00007C0B
	private void SetupBenchmarkGameParameters(int actorCount)
	{
		this.SetupDefaultGameParameters();
		this.gameModeParameters.playerTeam = -1;
		this.gameModeParameters.gameModePrefab = this.officialGameModePrefabs[0];
		this.gameModeParameters.actorCount = actorCount;
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00076260 File Offset: 0x00074460
	private void UpdateSceneLight()
	{
		if (RenderSettings.ambientMode == AmbientMode.Trilight)
		{
			this.sceneAmbientColor = RenderSettings.ambientSkyColor * 0.333f + RenderSettings.ambientEquatorColor * 0.333f + RenderSettings.ambientGroundColor * 0.333f;
		}
		else if (RenderSettings.ambientMode == AmbientMode.Flat)
		{
			this.sceneAmbientColor = RenderSettings.ambientLight;
		}
		else if (RenderSettings.ambientMode == AmbientMode.Skybox || RenderSettings.ambientMode == AmbientMode.Custom)
		{
			Vector3[] array = new Vector3[]
			{
				Vector3.up,
				Vector3.forward,
				Vector3.down
			};
			Color[] array2 = new Color[array.Length];
			RenderSettings.ambientProbe.Evaluate(array, array2);
			this.sceneAmbientColor = Color.black;
			for (int i = 0; i < array2.Length; i++)
			{
				this.sceneAmbientColor += array2[i] / (float)array2.Length;
			}
			this.sceneAmbientColor *= RenderSettings.ambientIntensity;
		}
		this.sceneAmbientColor.a = 1f;
		this.sceneSunlight = null;
		Light[] array3 = UnityEngine.Object.FindObjectsOfType<Light>();
		float num = 0f;
		foreach (Light light in array3)
		{
			if (light.type == LightType.Directional && light.intensity > num)
			{
				this.sceneSunlight = light;
				num = light.intensity;
			}
			if (GameManager.IsIngame())
			{
				light.cullingMask &= int.MaxValue;
			}
		}
		if (this.sceneSunlight != null)
		{
			this.sceneSunlightColor = this.sceneSunlight.color * this.sceneSunlight.intensity;
			this.sceneSunlightColor.a = 1f;
		}
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00076434 File Offset: 0x00074634
	public static bool IsInSunlight(Vector3 position)
	{
		return GameManager.instance.sceneSunlight == null || !Physics.Raycast(new Ray(position, -GameManager.instance.sceneSunlight.transform.forward), 9999f, -12947205);
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00076488 File Offset: 0x00074688
	private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name != "MapEditor-GameScene")
		{
			MapEditor.isTestingMap = false;
		}
		GameManager.UnpauseGame();
		this.OnLevelIndexLoaded(scene.buildIndex);
		if (EventSystem.current != null)
		{
			EventSystem.current.sendNavigationEvents = false;
		}
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x000764D8 File Offset: 0x000746D8
	private void OnLevelIndexLoaded(int levelIndex)
	{
		if (levelIndex < 0)
		{
			LevelPatcher.Run(this.levelVersionInfo);
		}
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		SteelInput.ignoreNumberRowBinds = false;
		KeyboardGlyphGenerator.ForceHide();
		EffectUi.Clear();
		(this.defaultNightVisionGoggles as NightVision).ForceDisable();
		PostProcessingManager.OnSceneLoaded();
		Physics.gravity = new Vector3(0f, -9.81f, 0f);
		MouseLook.paused = false;
		this.defaultSnapshot.TransitionTo(0f);
		this.isInNoGameplayScene = (UnityEngine.Object.FindObjectOfType<NoGameplayScene>() != null);
		SceneConstructor sceneConstructor = UnityEngine.Object.FindObjectOfType<SceneConstructor>();
		if (sceneConstructor)
		{
			sceneConstructor.OnSceneConstructed(new Action(this.OnLevelConstructed));
			return;
		}
		this.OnLevelConstructed();
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00009A3E File Offset: 0x00007C3E
	public static void DisableNightVision()
	{
		GameManager.instance.defaultNightVisionGoggles.ToggleDisable();
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00009A4F File Offset: 0x00007C4F
	public static void EnableNightVision()
	{
		GameManager.instance.defaultNightVisionGoggles.ToggleEnable();
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00009A60 File Offset: 0x00007C60
	public static void ToggleNightVision()
	{
		GameManager.instance.defaultNightVisionGoggles.Toggle(false);
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00009A72 File Offset: 0x00007C72
	private void UpdateIngameTargetFrameRate()
	{
		Application.targetFrameRate = -1;
		if (Options.GetToggle(OptionToggle.Id.VSync))
		{
			QualitySettings.vSyncCount = 1;
			return;
		}
		QualitySettings.vSyncCount = 0;
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00076590 File Offset: 0x00074790
	private int GetClampedScreenRefreshRate()
	{
		return Mathf.Clamp(Screen.currentResolution.refreshRate, 60, 200);
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x000765B8 File Offset: 0x000747B8
	private void OnLevelConstructed()
	{
		this.sceneName = SceneManager.GetActiveScene().name;
		DecalManager.instance.ResetDecals();
		this.ingameUiWorker.Clear();
		this.UpdateSceneLight();
		if (GameManager.IsIngame())
		{
			this.UpdateIngameTargetFrameRate();
			base.StartCoroutine(this.StartIngameLevel());
		}
		else if (GameManager.IsInLoadingScreen())
		{
			Application.targetFrameRate = this.GetClampedScreenRefreshRate();
			this.ingame = false;
		}
		else
		{
			Application.targetFrameRate = this.GetClampedScreenRefreshRate();
			this.ingame = false;
			this.generateNavCache = false;
			if (this.levelBundle != null)
			{
				Debug.Log("Unloading level bundle");
				this.levelBundle.Unload(true);
				this.levelBundleContentInfo = ModContentInformation.VanillaContent;
			}
			if (this.restartArmed)
			{
				this.restartArmed = false;
				GameManager.StartLevel(this.lastMapEntry, this.gameModeParameters);
			}
		}
		if (GameManager.IsInMainMenu() || GameManager.IsInMapEditor())
		{
			Benchmark.isRunning = false;
			this.isPlayingCampaign = false;
			VehicleSwitch.requireReload = true;
		}
		Options.instance.ApplyAtEndOfFrame();
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x00009A90 File Offset: 0x00007C90
	private IEnumerator StartIngameLevel()
	{
		GameManager.SetTeamColors(Color.blue, Color.red);
		if (GameManager.IsInCustomLevel() && GameManager.IsInAssetBundleLevel())
		{
			FixBundleShaders.ReloadAssetBundleSceneShadersIfNeeded(GameManager.instance.levelBundleContentInfo.versionInfo);
		}
		EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 0f, Color.black);
		yield return 0;
		if (this.generateNavCache)
		{
			EffectUi.Clear();
			this.GenerateNavCache();
		}
		else
		{
			if (GameManager.IsInCustomLevel())
			{
				this.SetupCustomLevelPathfinding();
			}
			this.StartGame();
		}
		yield break;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00009A9F File Offset: 0x00007C9F
	public void UfoAttack()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.ufoPrefab);
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x000766C0 File Offset: 0x000748C0
	private void StartGame()
	{
		RavenscriptManager.instance.ResetLog();
		Scene activeScene = SceneManager.GetActiveScene();
		Debug.LogFormat("Starting game on {0}, game mode prefab: {1}", new object[]
		{
			this.sceneName,
			this.gameModeParameters.gameModePrefab.name
		});
		this.ingame = true;
		GameManager.gameOver = false;
		this.hudEnabled = true;
		ChangeEmissionAtNight.tweakedMaterials = new Dictionary<Material, Material>();
		ActorManager.instance.LoadActorNameSet();
		try
		{
			foreach (AudioSource audioSource in UnityEngine.Object.FindObjectsOfType<AudioSource>())
			{
				if (audioSource.gameObject.scene == activeScene)
				{
					GameManager.SetOutputAudioMixer(audioSource, global::AudioMixer.World);
				}
			}
		}
		catch (Exception)
		{
		}
		UnityEngine.Object.Instantiate<GameObject>(this.ingameUiPrefab).SetActive(true);
		UnityEngine.Object.Instantiate<GameObject>(this.gameModeParameters.gameModePrefab, Vector3.zero, Quaternion.identity);
		if (this.testContentModMode)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.valueMonitorCanvasPrefab);
		}
		EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0.2f, Color.black);
		SceneryCamera.instance.EnableRender();
		ScoreboardUi.activeScoreboardType = ScoreboardUi.ActiveScoreboardUI.Scoreboard;
		WeaponManager.instance.SetupAiWeaponEntries(WeaponManager.instance.allWeapons);
		ActorManager.instance.StartGame();
		SpawnPointNeighborManager.instance.StartGame();
		CoverManager.instance.StartGame();
		DecalManager.instance.ResetDecals();
		ProjectilePoolManager.instance.InitializePools(this.gameInfo);
		if (this.gameModeParameters.halloweenMode)
		{
			this.ApplyHalloweenTheme();
			TimeOfDay.instance = null;
		}
		else if (TimeOfDay.instance != null)
		{
			TimeOfDay.instance.StartGame();
		}
		PostProcessingManager.StartGame();
		MinimapCamera.instance.Render();
		this.UpdateSceneLight();
		PathfindingManager.instance.SetupGraphMasks();
		PathfindingManager.instance.ApplyAvoidanceBoxes();
		PathfindingManager.instance.FindClosestNavmeshesToSpawnPoints();
		PathfindingManager.instance.GenerateLandingZones();
		this.waterSplashLargePrefab = this.defaultWaterSplashLargePrefab;
		this.waterSplashPrefab = this.defaultWaterSplashPrefab;
		LevelGravity levelGravity = UnityEngine.Object.FindObjectOfType<LevelGravity>();
		if (levelGravity != null)
		{
			Physics.gravity = levelGravity.gravity;
		}
		Vehicle[] array2 = UnityEngine.Object.FindObjectsOfType<Vehicle>();
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object.Destroy(array2[i].gameObject);
		}
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			spawnPoints[i].FindNearbyStuff();
		}
		this.InstantiatePlayerActor();
		FpsActorController.instance.SetOverrideCamera(SceneryCamera.instance.camera);
		try
		{
			GameModeBase.instance.StartGame();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		this.SpawnAllTurrets();
		OrderManager.instance.StartGame();
		try
		{
			GameModeBase.instance.SetupOrders();
		}
		catch (Exception exception2)
		{
			Debug.LogException(exception2);
		}
		OrderManager.RefreshAllOrders();
		this.gameStartTime = Time.time;
		if (GameManager.IsSpectating())
		{
			base.StartCoroutine(this.StartSpectatorModeCoroutine());
		}
		ModManager.SpawnAllEnabledMutatorPrefabs();
		this.SpawnAllVehicles();
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x000769A8 File Offset: 0x00074BA8
	private void InstantiatePlayerActor()
	{
		Actor componentInChildren = UnityEngine.Object.Instantiate<GameObject>(this.playerPrefab, new Vector3(0f, 10000f, 0f), Quaternion.identity).GetComponentInChildren<Actor>();
		componentInChildren.SetTeam(GameManager.PlayerTeam());
		ActorManager.Register(componentInChildren);
		this.defaultNightVisionGoggles.Equip(componentInChildren);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x000769FC File Offset: 0x00074BFC
	private void ApplyHalloweenTheme()
	{
		if (TimeOfDay.instance != null)
		{
			TimeOfDay.instance.ApplyNight();
		}
		UnityEngine.Object.Instantiate<GameObject>(this.halloweenPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
		TimeOfDay.ApplyAtmosphere(this.halloweenAtmosphere);
		RenderSettings.fogMode = FogMode.Exponential;
		if (ReflectionProber.instance != null)
		{
			ReflectionProber.instance.SetupProbes();
		}
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00009AAD File Offset: 0x00007CAD
	private IEnumerator StartSpectatorModeCoroutine()
	{
		this.StartSpectatorMode();
		yield return new WaitForEndOfFrame();
		GameModeBase.instance.PlayerAcceptedLoadoutFirstTime();
		yield break;
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x00076A70 File Offset: 0x00074C70
	private void SpawnAllVehicles()
	{
		if (!this.gameModeParameters.noVehicles)
		{
			foreach (VehicleSpawner vehicleSpawner in UnityEngine.Object.FindObjectsOfType<VehicleSpawner>())
			{
				try
				{
					if (vehicleSpawner.enabled)
					{
						vehicleSpawner.SpawnVehicleWhenClear();
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x00076ACC File Offset: 0x00074CCC
	private void SpawnAllTurrets()
	{
		if (this.gameModeParameters.noTurrets)
		{
			return;
		}
		try
		{
			foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
			{
				foreach (TurretSpawner turretSpawner in spawnPoint.turretSpawners)
				{
					if (turretSpawner.enabled)
					{
						turretSpawner.SpawnTurrets();
						turretSpawner.ActivateTurret(spawnPoint.owner);
					}
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x00009ABC File Offset: 0x00007CBC
	private void SetupCustomLevelPathfinding()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.pathfindingPrefab).GetComponent<PathfindingManager>().SetupCustomLevelPathfinding();
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x00076B78 File Offset: 0x00074D78
	private void GenerateNavCache()
	{
		PathfindingManager component = UnityEngine.Object.Instantiate<GameObject>(this.pathfindingPrefab).GetComponent<PathfindingManager>();
		component.ScanCustomLevel();
		component.GenerateNavmeshCache(this.navCacheWritebackPath, false);
		string filePath = this.navCacheWritebackPath.Substring(0, this.navCacheWritebackPath.Length - 6) + "_coverpoints.bytes";
		component.GenerateCoverPointList(filePath);
		component.ApplyAvoidanceBoxes();
		component.DisplayNavmeshes();
		this.StartSpectatorMode();
		SpectatorCamera.instance.camera.gameObject.AddComponent<AudioListener>();
		SpectatorCamera.instance.camera.clearFlags = CameraClearFlags.Skybox;
		SpectatorCamera.instance.camera.cullingMask = -1;
		SpectatorCamera.instance.camera.nearClipPlane = 0.2f;
		SpectatorCamera.instance.camera.farClipPlane = 4000f;
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x00076C44 File Offset: 0x00074E44
	private void StartSpectatorMode()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.spectatorCameraPrefab, SceneryCamera.instance.transform.position, SceneryCamera.instance.transform.rotation);
		if (FpsActorController.instance != null)
		{
			FpsActorController.instance.DisableCameras();
		}
		SceneryCamera.instance.camera.enabled = false;
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x00009AD3 File Offset: 0x00007CD3
	public float ElapsedGameTime()
	{
		return Time.time - this.gameStartTime;
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00009AE1 File Offset: 0x00007CE1
	public string GetTeamName(int team)
	{
		if (team == 0)
		{
			return "EAGLE";
		}
		if (team == 1)
		{
			return "RAVEN";
		}
		return "UNKNOWN TEAM";
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00009AFB File Offset: 0x00007CFB
	public string GetRichTextColorTeamName(int team)
	{
		return ColorScheme.RichTextColorTagOfTeam(team) + this.GetTeamName(team) + "</color>";
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00076CA4 File Offset: 0x00074EA4
	private void Update()
	{
		if (GameManager.IsIngame())
		{
			try
			{
				Camera mainManagedCamera = GameManager.GetMainManagedCamera();
				if (mainManagedCamera != null)
				{
					GameManager.cachedIngameCameraPosition = mainManagedCamera.transform.position;
				}
			}
			catch
			{
			}
		}
		if (GameManager.IsConnectedToSteam())
		{
			this.steamworks.Update();
		}
		this.PollSteamInput();
		if (SteelInput.IsInitialized())
		{
			SteelInput.Update();
			SteelInput.SetScrollEnabled(!SteelInput.GetButton(SteelInput.KeyBinds.ScopeModifier) && (!(FpsActorController.instance != null) || !FpsActorController.instance.IsCursorFree()));
		}
		if (!GameManager.gameOver && Input.GetKeyDown(KeyCode.Home))
		{
			this.hudEnabled = !this.hudEnabled;
		}
		if (Input.GetKeyDown(KeyCode.F11))
		{
			this.showFps = !this.showFps;
		}
		if (Input.GetKeyDown(KeyCode.Escape) && this.generateNavCache && GameManager.IsIngame())
		{
			Application.Quit();
		}
		if (GameModeBase.instance != null && GameModeBase.instance.canvas != null)
		{
			GameModeBase.instance.canvas.enabled = this.hudEnabled;
		}
		if (this.showFps && Time.timeScale > 0f)
		{
			this.framerate = (float)Mathf.RoundToInt(10f * Time.timeScale / Time.smoothDeltaTime) * 0.1f;
			if (this.framerate < this.minFramerate)
			{
				this.minFramerate = this.framerate;
				this.minFramerateStayAction.Start();
				return;
			}
			if (this.minFramerateStayAction.TrueDone())
			{
				this.minFramerate = this.framerate;
			}
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00076E44 File Offset: 0x00075044
	private void PollSteamInput()
	{
		if (!this.steamworks.input.isInitialized)
		{
			return;
		}
		if (GameManager.IsIngame() && FpsActorController.instance != null && FpsActorController.instance.lockCursor)
		{
			if (ActorManager.instance.player.IsSeated())
			{
				Vehicle vehicle = ActorManager.instance.player.seat.vehicle;
				if (!vehicle.isTurret && ActorManager.instance.player.IsDriver())
				{
					if (vehicle is Airplane)
					{
						this.steamworks.input.UpdateGameState(SteamInputWrapper.ActionSet.Vehicle, SteamInputWrapper.VehicleLayer.Airplane, false);
					}
					else if (vehicle is Helicopter)
					{
						this.steamworks.input.UpdateGameState(SteamInputWrapper.ActionSet.Vehicle, SteamInputWrapper.VehicleLayer.Helicopter, false);
					}
					else
					{
						this.steamworks.input.UpdateGameState(SteamInputWrapper.ActionSet.Vehicle, SteamInputWrapper.VehicleLayer.Car, false);
					}
				}
				else
				{
					this.steamworks.input.UpdateGameState(SteamInputWrapper.ActionSet.Vehicle, SteamInputWrapper.VehicleLayer.None, false);
				}
			}
			else
			{
				this.steamworks.input.UpdateGameState(SteamInputWrapper.ActionSet.Infantry, SteamInputWrapper.VehicleLayer.None, false);
			}
		}
		else
		{
			this.steamworks.input.UpdateGameState(SteamInputWrapper.ActionSet.Menu, SteamInputWrapper.VehicleLayer.None, false);
		}
		this.steamworks.input.PollInputStatus();
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00076F68 File Offset: 0x00075168
	private void OnGUI()
	{
		if (this.showFps)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.Scale(new Vector3(GameManager.UI_SCALE, GameManager.UI_SCALE, GameManager.UI_SCALE));
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			this.Label(3f, 0f, this.framerate.ToString(), Color.white);
			this.Label(3f, 18f, this.minFramerate.ToString(), Color.red);
			if (ActorManager.instance != null && this.ingame)
			{
				Color c = ActorManager.AITickIsThrottled() ? Color.red : Color.white;
				this.Label(3f, 40f, ActorManager.GetAITickStatusString(), c);
			}
			GUI.matrix = matrix;
		}
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00077038 File Offset: 0x00075238
	private void Label(float x, float y, string text, Color c)
	{
		GUI.color = Color.black;
		GUI.Label(new Rect(x + 1f, y + 1f, 400f, 20f), text);
		GUI.color = c;
		GUI.Label(new Rect(x, y, 400f, 20f), text);
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00009B14 File Offset: 0x00007D14
	public static void PauseGame()
	{
		MouseLook.paused = true;
		Time.timeScale = 0f;
		GameManager.instance.sfxMixer.SetFloat("pitch", Time.timeScale);
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00009B40 File Offset: 0x00007D40
	public static void UnpauseGame()
	{
		MouseLook.paused = false;
		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.timeScale / 60f;
		GameManager.instance.sfxMixer.SetFloat("pitch", Time.timeScale);
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00009B7C File Offset: 0x00007D7C
	public static bool IsPaused()
	{
		return Time.timeScale == 0f;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00077090 File Offset: 0x00075290
	public static void SetOutputAudioMixer(AudioSource audio, global::AudioMixer mixer)
	{
		AudioMixerGroup outputAudioMixerGroup = GameManager.instance.ingameMixerGroup;
		switch (mixer)
		{
		case global::AudioMixer.Master:
			outputAudioMixerGroup = GameManager.instance.masterMixerGroup;
			break;
		case global::AudioMixer.Ingame:
			outputAudioMixerGroup = GameManager.instance.ingameMixerGroup;
			break;
		case global::AudioMixer.Important:
			outputAudioMixerGroup = GameManager.instance.importantMixerGroup;
			break;
		case global::AudioMixer.FirstPerson:
			outputAudioMixerGroup = GameManager.instance.fpMixerGroup;
			break;
		case global::AudioMixer.PlayerVehicle:
			outputAudioMixerGroup = GameManager.instance.playerVehicleMixerGroup;
			break;
		case global::AudioMixer.World:
			outputAudioMixerGroup = GameManager.instance.worldMixerGroup;
			break;
		case global::AudioMixer.Music:
			outputAudioMixerGroup = GameManager.instance.musicMixerGroup;
			break;
		case global::AudioMixer.MusicSting:
			outputAudioMixerGroup = GameManager.instance.musicMixerGroup;
			break;
		}
		audio.outputAudioMixerGroup = outputAudioMixerGroup;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00009B8A File Offset: 0x00007D8A
	public void OnOptionsApplied()
	{
		PostProcessingManager.OnOptionsApplied();
		IngameUiWorker.OnResolutionChanged();
		if (this.ingame)
		{
			this.UpdateIngameTargetFrameRate();
		}
		this.UpdateUIScale();
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00009BAA File Offset: 0x00007DAA
	private void UpdateUIScale()
	{
		GameManager.UI_SCALE = Mathf.Max(1f, (float)Screen.height / 1080f);
	}

	// Token: 0x04000C8D RID: 3213
	private const float SCREEN_DEFAULT_HEIGHT = 1080f;

	// Token: 0x04000C8E RID: 3214
	public const int MENU_LEVEL_INDEX = 1;

	// Token: 0x04000C8F RID: 3215
	public const int LOADING_SCREEN_LEVEL_INDEX = 2;

	// Token: 0x04000C90 RID: 3216
	public const int SPECTATOR_TEAM = -1;

	// Token: 0x04000C91 RID: 3217
	private const int FLAG_CHECK = 3145580;

	// Token: 0x04000C92 RID: 3218
	public static float UI_SCALE = 1f;

	// Token: 0x04000C93 RID: 3219
	private const int SUNLIGHT_RAY_MASK = -12947205;

	// Token: 0x04000C94 RID: 3220
	private const float USE_FARAWAY_MIX_DISTANCE = 80f;

	// Token: 0x04000C95 RID: 3221
	public static bool gameOver = false;

	// Token: 0x04000C96 RID: 3222
	public static bool verboseLogging = false;

	// Token: 0x04000C97 RID: 3223
	public static GameManager instance;

	// Token: 0x04000C98 RID: 3224
	[NonSerialized]
	public bool ingame;

	// Token: 0x04000C99 RID: 3225
	public int buildNumber = 20;

	// Token: 0x04000C9A RID: 3226
	public bool isBeta;

	// Token: 0x04000C9B RID: 3227
	public GameObject ingameUiPrefab;

	// Token: 0x04000C9C RID: 3228
	public GameObject playerPrefab;

	// Token: 0x04000C9D RID: 3229
	public UnityEngine.Audio.AudioMixer sfxMixer;

	// Token: 0x04000C9E RID: 3230
	public AudioMixerGroup fpMixerGroup;

	// Token: 0x04000C9F RID: 3231
	public AudioMixerGroup playerVehicleMixerGroup;

	// Token: 0x04000CA0 RID: 3232
	public AudioMixerGroup playerVehicleInteriorMixerGroup;

	// Token: 0x04000CA1 RID: 3233
	public AudioMixerGroup reflectionMixerGroup;

	// Token: 0x04000CA2 RID: 3234
	public AudioMixerGroup masterMixerGroup;

	// Token: 0x04000CA3 RID: 3235
	public AudioMixerGroup ingameMixerGroup;

	// Token: 0x04000CA4 RID: 3236
	public AudioMixerGroup importantMixerGroup;

	// Token: 0x04000CA5 RID: 3237
	public AudioMixerGroup worldMixerGroup;

	// Token: 0x04000CA6 RID: 3238
	public AudioMixerGroup combatMixerGroup;

	// Token: 0x04000CA7 RID: 3239
	public AudioMixerGroup combatFarMixerGroup;

	// Token: 0x04000CA8 RID: 3240
	public AudioMixerGroup combatMuffledMixerGroup;

	// Token: 0x04000CA9 RID: 3241
	public AudioMixerGroup combatMuffledFarMixerGroup;

	// Token: 0x04000CAA RID: 3242
	public AudioMixerGroup vehiclesMixerGroup;

	// Token: 0x04000CAB RID: 3243
	public AudioMixerGroup vehiclesFarMixerGroup;

	// Token: 0x04000CAC RID: 3244
	public AudioMixerGroup musicMixerGroup;

	// Token: 0x04000CAD RID: 3245
	public AudioMixerSnapshot defaultSnapshot;

	// Token: 0x04000CAE RID: 3246
	public GameObject spectatorCameraPrefab;

	// Token: 0x04000CAF RID: 3247
	public GameObject configureFlagPrefab;

	// Token: 0x04000CB0 RID: 3248
	public GameObject defaultGameModePrefab;

	// Token: 0x04000CB1 RID: 3249
	public GameObject editorTestGameModePrefab;

	// Token: 0x04000CB2 RID: 3250
	public GameObject[] officialGameModePrefabs;

	// Token: 0x04000CB3 RID: 3251
	public bool hudEnabled = true;

	// Token: 0x04000CB4 RID: 3252
	public TimeOfDay.Atmosphere halloweenAtmosphere;

	// Token: 0x04000CB5 RID: 3253
	public GameObject halloweenPrefab;

	// Token: 0x04000CB6 RID: 3254
	public GameObject sswt;

	// Token: 0x04000CB7 RID: 3255
	public GameObject ufoPrefab;

	// Token: 0x04000CB8 RID: 3256
	public GameObject hqCallPrefab;

	// Token: 0x04000CB9 RID: 3257
	public static bool triggeredUfo = false;

	// Token: 0x04000CBA RID: 3258
	public GameObject defaultWaterSplashLargePrefab;

	// Token: 0x04000CBB RID: 3259
	public GameObject defaultWaterSplashPrefab;

	// Token: 0x04000CBC RID: 3260
	private GameObject waterSplashLargePrefab;

	// Token: 0x04000CBD RID: 3261
	private GameObject waterSplashPrefab;

	// Token: 0x04000CBE RID: 3262
	public GameModeParameters gameModeParameters;

	// Token: 0x04000CBF RID: 3263
	[NonSerialized]
	public bool hasNonDefaultGameModeParameters;

	// Token: 0x04000CC0 RID: 3264
	[NonSerialized]
	public bool generateNavCache;

	// Token: 0x04000CC1 RID: 3265
	[NonSerialized]
	public string navCacheWritebackPath = "";

	// Token: 0x04000CC2 RID: 3266
	public GameObject pathfindingPrefab;

	// Token: 0x04000CC3 RID: 3267
	public AssetBundle levelBundle;

	// Token: 0x04000CC4 RID: 3268
	public ModContentInformation levelBundleContentInfo;

	// Token: 0x04000CC5 RID: 3269
	public AudioClip[] reflectionsOpen;

	// Token: 0x04000CC6 RID: 3270
	public AudioClip[] reflectionsClose;

	// Token: 0x04000CC7 RID: 3271
	public Transform reflectionAudioSourceParent;

	// Token: 0x04000CC8 RID: 3272
	public GameInfoContainer gameInfo;

	// Token: 0x04000CC9 RID: 3273
	public SpriteActorDatabase[] defaultSpriteActorDatabases;

	// Token: 0x04000CCA RID: 3274
	public ToggleableItem defaultNightVisionGoggles;

	// Token: 0x04000CCB RID: 3275
	public GameObject valueMonitorCanvasPrefab;

	// Token: 0x04000CCC RID: 3276
	public MutatorEntry benchmarkMutator;

	// Token: 0x04000CCD RID: 3277
	public IngameUiWorker ingameUiWorker;

	// Token: 0x04000CCE RID: 3278
	public static Vector3 cachedIngameCameraPosition;

	// Token: 0x04000CCF RID: 3279
	[NonSerialized]
	public bool isPlayingCampaign;

	// Token: 0x04000CD0 RID: 3280
	[NonSerialized]
	public Color sceneAmbientColor;

	// Token: 0x04000CD1 RID: 3281
	[NonSerialized]
	public Color sceneSunlightColor;

	// Token: 0x04000CD2 RID: 3282
	[NonSerialized]
	public Light sceneSunlight;

	// Token: 0x04000CD3 RID: 3283
	[NonSerialized]
	public string sceneName;

	// Token: 0x04000CD4 RID: 3284
	private int sunFpMask;

	// Token: 0x04000CD5 RID: 3285
	private int sunNoFpMask;

	// Token: 0x04000CD6 RID: 3286
	private static bool isFlagged;

	// Token: 0x04000CD7 RID: 3287
	private bool showFps;

	// Token: 0x04000CD8 RID: 3288
	private float framerate;

	// Token: 0x04000CD9 RID: 3289
	private float minFramerate;

	// Token: 0x04000CDA RID: 3290
	private TimedAction minFramerateStayAction = new TimedAction(2f, false);

	// Token: 0x04000CDB RID: 3291
	private float gameStartTime;

	// Token: 0x04000CDC RID: 3292
	private bool restartArmed;

	// Token: 0x04000CDD RID: 3293
	[NonSerialized]
	public bool testContentModMode;

	// Token: 0x04000CDE RID: 3294
	[NonSerialized]
	public InstantActionMaps.MapEntry lastMapEntry;

	// Token: 0x04000CDF RID: 3295
	[NonSerialized]
	public ModManager.EngineVersionInfo levelVersionInfo;

	// Token: 0x04000CE0 RID: 3296
	private bool autoStartMapArmed;

	// Token: 0x04000CE1 RID: 3297
	private string autoStartMapPath;

	// Token: 0x04000CE2 RID: 3298
	public SteamworksWrapper steamworks;

	// Token: 0x04000CE3 RID: 3299
	private bool isInNoGameplayScene;

	// Token: 0x04000CE4 RID: 3300
	private AudioSource[] reflectionAudioSources;

	// Token: 0x04000CE5 RID: 3301
	private int[] reflectionAudioPriority;

	// Token: 0x04000CE6 RID: 3302
	public AudioSource secretTapeSound;

	// Token: 0x020001AF RID: 431
	// (Invoke) Token: 0x06000BCC RID: 3020
	public delegate void DelRecurseHierarchy(Transform t);
}
