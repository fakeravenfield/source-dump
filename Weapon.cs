using System;
using System.Collections;
using System.Collections.Generic;
using Lua;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

// Token: 0x02000110 RID: 272
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00007271 File Offset: 0x00005471
	// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00065288 File Offset: 0x00063488
	public Actor user
	{
		get
		{
			return this._user;
		}
		set
		{
			this._user = value;
			this.killCredit = value;
			foreach (Weapon weapon in this.alternativeWeapons)
			{
				weapon.user = this._user;
			}
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x000652EC File Offset: 0x000634EC
	public void OnProjectilePrefabChanged()
	{
		if (this.configuration.projectilePrefab != null)
		{
			Projectile component = this.configuration.projectilePrefab.GetComponent<Projectile>();
			this.projectileSpeed = component.configuration.speed;
			this.projectileGravity = Physics.gravity * component.configuration.gravityMultiplier;
			this.projectileArmorRating = component.armorDamage;
			this.isWireGuided = (component.GetType() == typeof(WireGuidedMissile));
			this.isBomb = (component.configuration.inheritVelocity && component.configuration.speed < 20f);
			this.SetupTeammateDangerRange(component);
			return;
		}
		this.projectileSpeed = 100f;
		this.projectileGravity = Physics.gravity;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000653BC File Offset: 0x000635BC
	protected virtual void Awake()
	{
		try
		{
			this.onFireScriptable = RavenscriptManager.events.WrapUnityEvent(this, this.onFire);
		}
		catch (Exception)
		{
		}
		try
		{
			this.OnProjectilePrefabChanged();
			this.animator = base.GetComponent<Animator>();
			this.audio = base.GetComponent<AudioSource>();
			if (this.animator != null && this.animator.runtimeAnimatorController == null)
			{
				this.animator = null;
			}
			if (this.animator == null && this.parentWeapon != null)
			{
				this.animator = this.parentWeapon.GetComponent<Animator>();
			}
			if (this.reloadAudio != null)
			{
				GameManager.SetOutputAudioMixer(this.reloadAudio, this.IsMountedWeapon() ? AudioMixer.PlayerVehicle : AudioMixer.FirstPerson);
			}
			this.SetupAnimatorNotifications();
			this.ammo = this.configuration.ammo;
			this.spareAmmo = this.configuration.spareAmmo;
			this.configuration.followupSpreadDissipateTime = Mathf.Max(this.configuration.followupSpreadDissipateTime, 0.01f);
			this.followupSpreadDissipateRate = this.configuration.followupMaxSpreadHip / this.configuration.followupSpreadDissipateTime;
			this.heatStayAction = new TimedAction(this.configuration.cooldown * 1.1f, false);
			this.OnChargeTimeChanged();
			this.smoothNoisePhase = UnityEngine.Random.Range(0f, 6.2831855f);
			this.hasAlternateSightModes = (this.configuration.sightModes != null && this.configuration.sightModes.Length != 0);
			this.FindMuzzleFlashParticles();
			this.audio.outputAudioMixerGroup = GameManager.instance.worldMixerGroup;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00007279 File Offset: 0x00005479
	public void OnChargeTimeChanged()
	{
		this.chargeAction = new TimedAction(this.configuration.chargeTime, false);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00065598 File Offset: 0x00063798
	public void SetAutoFire()
	{
		this.StopFire();
		this.StopFireLoop();
		this.stopFireLoop.Stop();
		this.fireLoopPlaying = false;
		this.audio.Stop();
		this.audio.volume = this.weaponVolume;
		this.configuration.auto = true;
		this.audio.loop = true;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x000655F8 File Offset: 0x000637F8
	public void SetSingleFire()
	{
		this.StopFire();
		this.StopFireLoop();
		this.stopFireLoop.Stop();
		this.fireLoopPlaying = false;
		this.audio.Stop();
		this.audio.volume = this.weaponVolume;
		this.configuration.auto = false;
		this.audio.loop = false;
		this.hasFiredSingleRoundThisTrigger = false;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00007292 File Offset: 0x00005492
	public bool HasParentWeapon()
	{
		return this.parentWeapon != null;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00065660 File Offset: 0x00063860
	protected virtual void SetupTeammateDangerRange(Projectile projectile)
	{
		ExplodingProjectile explodingProjectile = projectile as ExplodingProjectile;
		if (explodingProjectile != null)
		{
			this.teammateDangerRange = Mathf.Max(explodingProjectile.explosionConfiguration.damageRange * 0.8f, 20f);
		}
		GrenadeProjectile grenadeProjectile = projectile as GrenadeProjectile;
		if (grenadeProjectile != null)
		{
			this.teammateDangerRange = Mathf.Max(grenadeProjectile.explosionConfiguration.damageRange, 20f);
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x000656CC File Offset: 0x000638CC
	private void FindMuzzleFlashParticles()
	{
		this.muzzleFlash = new Dictionary<Transform, ParticleSystem>(this.configuration.muzzles.Length);
		foreach (Transform transform in this.configuration.muzzles)
		{
			ParticleSystem particleSystem = transform.GetComponent<ParticleSystem>();
			if (particleSystem == null)
			{
				particleSystem = transform.GetComponentInChildren<ParticleSystem>();
			}
			if (particleSystem != null)
			{
				this.muzzleFlash.Add(transform, particleSystem);
			}
		}
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0006573C File Offset: 0x0006393C
	private void SetupAnimatorNotifications()
	{
		if (this.HasActiveAnimator())
		{
			foreach (AnimatorControllerParameter animatorControllerParameter in this.animator.parameters)
			{
				if (animatorControllerParameter.nameHash == Weapon.LOADED_AMMO_PARAMETER_HASH && animatorControllerParameter.type == AnimatorControllerParameterType.Int)
				{
					this.notifyAnimatorAmmo = true;
				}
				else if (animatorControllerParameter.nameHash == Weapon.NO_AMMO_BLEND_PARAMETER_HASH && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					this.notifyAnimatorNoAmmoBlend = true;
				}
				else if (animatorControllerParameter.nameHash == Weapon.RANDOM_PARAMETER_HASH && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					this.notifyAnimatorRandom = true;
				}
				else if (animatorControllerParameter.nameHash == Weapon.CHARGING_PARAMETER_HASH && animatorControllerParameter.type == AnimatorControllerParameterType.Bool)
				{
					this.notifyAnimatorCharge = true;
				}
				else if (animatorControllerParameter.nameHash == Weapon.SMOOTH_SIGHT_MODE_PARAMETER_HASH && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					this.notifySmoothSightMode = true;
				}
			}
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x000072A0 File Offset: 0x000054A0
	public bool HasAnySubWeapons()
	{
		return this.alternativeWeapons != null && this.alternativeWeapons.Count > 0;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x000072BA File Offset: 0x000054BA
	public bool HasAnyAltSightModes()
	{
		return this.configuration.sightModes != null && this.configuration.sightModes.Length != 0;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00065814 File Offset: 0x00063A14
	protected virtual void Start()
	{
		if (this.targetTracker != null)
		{
			this.allowProne = false;
		}
		if (this.HasParentWeapon())
		{
			this.parentWeapon.AddSubWeapon(this);
		}
		this.weaponVolume = this.audio.volume;
		if (this.configuration.auto)
		{
			this.SetAutoFire();
		}
		if (this.user != null)
		{
			if (this.user.aiControlled)
			{
				this.audio.pitch *= UnityEngine.Random.Range(0.97f, 1.02f);
				return;
			}
			if (this.animator != null)
			{
				this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			}
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x000658C8 File Offset: 0x00063AC8
	public virtual void FindRenderers(bool thirdperson)
	{
		if (thirdperson)
		{
			this.renderers = new List<Renderer>(this.thirdPersonTransform.GetComponentsInChildren<Renderer>());
			return;
		}
		this.renderers = new List<Renderer>(base.GetComponentsInChildren<Renderer>());
		foreach (Renderer renderer in this.renderers)
		{
			renderer.shadowCastingMode = ShadowCastingMode.Off;
			renderer.receiveShadows = false;
		}
		if (this.arms != null)
		{
			this.arms.updateWhenOffscreen = true;
		}
		if (this.HasScopeObject())
		{
			this.nonScopeRenderers = new List<Renderer>(this.renderers);
			foreach (Renderer item in this.configuration.scopeAimObject.GetComponentsInChildren<Renderer>())
			{
				this.nonScopeRenderers.Remove(item);
			}
		}
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000659B0 File Offset: 0x00063BB0
	public bool NeedsDifficultyGenerated()
	{
		return this.configuration.diffAir == Weapon.Difficulty.Auto || this.configuration.diffAirFastMover == Weapon.Difficulty.Auto || this.configuration.diffGroundVehicles == Weapon.Difficulty.Auto || this.configuration.diffInfantry == Weapon.Difficulty.Auto || this.configuration.diffInfantryGroup == Weapon.Difficulty.Auto;
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00065A04 File Offset: 0x00063C04
	protected virtual void Update()
	{
		if (!this.stopFireLoop.Done())
		{
			float num = 1f - this.stopFireLoop.Ratio();
			this.audio.volume = num * this.weaponVolume;
			if (this.stopFireLoop.TrueDone())
			{
				this.audio.Stop();
			}
		}
		if (this.HasActiveAnimator())
		{
			if (this.notifyAnimatorRandom)
			{
				this.animator.SetFloat(Weapon.RANDOM_PARAMETER_HASH, UnityEngine.Random.Range(0f, 100f));
			}
			this.UpdateSightModeAnimation();
			if (this.unholstered)
			{
				if (this.notifyAnimatorCharge)
				{
					this.animator.SetBool(Weapon.CHARGING_PARAMETER_HASH, !this.chargeAction.TrueDone());
				}
				if (this.user != null)
				{
					this.animator.SetBool(Weapon.TUCK_PARAMETER_HASH, this.user.controller.IsSprinting() && !this.reloading);
				}
			}
		}
		if (this.UserIsPlayer() && this.HasScopeObject())
		{
			this.aimAmount = Mathf.MoveTowards(this.aimAmount, this.aiming ? 1f : 0f, Time.deltaTime * 6f);
			bool flag = this.showScopeObject;
			this.showScopeObject = (this.aimAmount >= 0.95f);
			this.configuration.scopeAimObject.SetActive(this.showScopeObject);
			try
			{
				if (!flag && this.showScopeObject)
				{
					foreach (Renderer renderer in this.nonScopeRenderers)
					{
						renderer.enabled = false;
					}
					FpsActorController.instance.ScopeBlackout();
				}
				else if (flag && !this.showScopeObject)
				{
					foreach (Renderer renderer2 in this.nonScopeRenderers)
					{
						renderer2.enabled = true;
					}
				}
			}
			catch (Exception e)
			{
				ModManager.HandleModException(e);
			}
		}
		if (this.followupSpreadStayAction.TrueDone())
		{
			this.followupSpreadMagnitude = Mathf.MoveTowards(this.followupSpreadMagnitude, 0f, this.followupSpreadDissipateRate * Time.deltaTime);
		}
		if (this.configuration.applyHeat)
		{
			this.UpdateHeat();
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x000072DA File Offset: 0x000054DA
	private void UpdateHasAmmoFlag()
	{
		this.cachedHasAnyAmmo = (this.HasLoadedAmmo() || this.HasSpareAmmo());
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00065C74 File Offset: 0x00063E74
	public void OnWeaponAnimatorActivated()
	{
		this.UpdateHasAmmoFlag();
		if (this.HasActiveAnimator())
		{
			this.animator.SetBool(Weapon.NO_AMMO_PARAMETER_HASH, !this.HasAnyAmmo());
			if (this.notifyAnimatorNoAmmoBlend)
			{
				this.animator.SetFloat(Weapon.NO_AMMO_BLEND_PARAMETER_HASH, this.HasAnyAmmo() ? 0f : 1f);
			}
			if (this.notifyAnimatorAmmo)
			{
				this.animator.SetInteger(Weapon.LOADED_AMMO_PARAMETER_HASH, this.ammo);
			}
			if (this.configuration.scopeAimObject != null)
			{
				this.configuration.scopeAimObject.SetActive(false);
			}
			this.animator.SetInteger(Weapon.ALT_WEAPON_PARAMETER_HASH, this.activeSubWeaponIndex + 1);
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00065D34 File Offset: 0x00063F34
	private void UpdateSightModeAnimation()
	{
		if (this.HasParentWeapon() && this.useParentWeaponSightModes)
		{
			this.parentWeapon.UpdateSightModeAnimation();
			return;
		}
		if (this.configuration.sightModes == null || this.configuration.sightModes.Length == 0)
		{
			return;
		}
		this.animator.SetInteger(Weapon.SIGHT_MODE_PARAMETER_HASH, this.activeSightModeIndex);
		if (this.notifySmoothSightMode)
		{
			this.smoothSightMode = Mathf.MoveTowards(Mathf.Lerp(this.smoothSightMode, (float)this.activeSightModeIndex, 15f * Time.deltaTime), (float)this.activeSightModeIndex, Time.deltaTime);
			this.animator.SetFloat(Weapon.SMOOTH_SIGHT_MODE_PARAMETER_HASH, this.smoothSightMode);
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00065DE4 File Offset: 0x00063FE4
	private void UpdateSoundOutputGroup()
	{
		if (GameManager.IsSpectating())
		{
			return;
		}
		if (this.user != null && !this.UserIsPlayer())
		{
			GameManager.UpdateSoundOutputGroupCombat(this.audio, ActorManager.ActorDistanceToPlayer(this.user), ActorManager.ActorCanSeePlayer(this.user));
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00065E30 File Offset: 0x00064030
	private void UpdateHeat()
	{
		if (this.heatStayAction.TrueDone())
		{
			this.heat = Mathf.Clamp01(this.heat - this.configuration.heatDrainRate * Time.deltaTime);
		}
		if (this.isOverheating)
		{
			this.isOverheating = (this.heat > 0f);
		}
		if (this.HasActiveAnimator() && this.user != null)
		{
			this.animator.SetBool(Weapon.OVERHEAT_PARAMETER_HASH, this.isOverheating);
		}
		if (this.configuration.heatMaterial.HasTarget())
		{
			Material material = this.configuration.heatMaterial.Get();
			if (material != null)
			{
				material.SetColor("_EmissionColor", Color.Lerp(Color.black, this.configuration.heatColor, this.configuration.heatColorGain.Evaluate(this.heat)));
			}
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00065F18 File Offset: 0x00064118
	public virtual void Fire(Vector3 direction, bool useMuzzleDirection)
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.Fire(direction, useMuzzleDirection);
			return;
		}
		if (!this.unholstered || this.isOverheating || this.isLocked)
		{
			return;
		}
		if (this.CanFire())
		{
			if (this.configuration.auto && (!this.audio.isPlaying || !this.stopFireLoop.Done()))
			{
				this.StartFireLoop();
			}
			this.Shoot(direction, useMuzzleDirection);
			if (this.configuration.applyHeat)
			{
				this.heat = Mathf.Clamp01(this.heat + this.configuration.heatGainPerShot);
				this.heatStayAction.Start();
				this.isOverheating = (this.heat == 1f);
				if (this.isOverheating)
				{
					this.StopFire();
					if (this.configuration.overheatParticles != null)
					{
						this.configuration.overheatParticles.Play();
					}
					if (this.configuration.overheatSound != null)
					{
						this.configuration.overheatSound.Play();
					}
				}
			}
		}
		if ((this.ammo == -1 || this.ammo > 0) && !this.reloading)
		{
			if (!this.holdingFire)
			{
				if (this.configuration.useChargeTime)
				{
					this.chargeAction.Start();
				}
				if (this.configuration.chargeSound != null)
				{
					this.configuration.chargeSound.Play();
				}
			}
			this.holdingFire = true;
		}
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000072F3 File Offset: 0x000054F3
	private void StartFireLoop()
	{
		this.UpdateSoundOutputGroup();
		this.audio.volume = this.weaponVolume;
		this.audio.Play();
		this.stopFireLoop.Stop();
		this.fireLoopPlaying = true;
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00007329 File Offset: 0x00005529
	private void StopFireLoop()
	{
		if (this.fireLoopPlaying)
		{
			this.stopFireLoop.Start();
			this.fireLoopPlaying = false;
		}
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0006609C File Offset: 0x0006429C
	public void StopFire()
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.StopFire();
			return;
		}
		if (this.configuration.auto)
		{
			this.StopFireLoop();
		}
		this.hasFiredSingleRoundThisTrigger = false;
		this.holdingFire = false;
		this.chargeAction.Stop();
		if (this.configuration.chargeSound != null)
		{
			this.configuration.chargeSound.Stop();
		}
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00066110 File Offset: 0x00064310
	public virtual void SetAiming(bool aiming)
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.SetAiming(aiming);
			return;
		}
		this.aiming = aiming;
		if (this.HasActiveAnimator())
		{
			this.animator.SetBool(Weapon.AIM_PARAMETER_HASH, aiming);
		}
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00007345 File Offset: 0x00005545
	protected void AutoReload()
	{
		this.Reload(false);
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00066158 File Offset: 0x00064358
	public virtual void Reload(bool overrideHolstered = false)
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.Reload(overrideHolstered);
			return;
		}
		if (this.AmmoFull() || !this.HasSpareAmmo())
		{
			return;
		}
		if ((!this.unholstered && !overrideHolstered) || this.reloading || this.isLocked)
		{
			return;
		}
		if (this.fireLoopPlaying)
		{
			this.StopFireLoop();
		}
		if (this.HasActiveAnimator())
		{
			this.animator.SetTrigger(Weapon.RELOAD_PARAMETER_HASH);
		}
		this.DisableOverrideLayer();
		if (this.UserIsPlayer() && this.reloadAudio != null)
		{
			this.reloadAudio.Play();
		}
		this.holdingFire = false;
		this.reloading = true;
		this.chargeAction.Stop();
		if (this.configuration.dropAmmoWhenReloading)
		{
			int num = Mathf.Min(this.ammo, this.configuration.maxRemainingAmmoAfterDrop);
			int num2 = Mathf.Max(0, this.ammo - num);
			this.ammo = num;
			this.RemoveSpareAmmo(-num2);
		}
		if (this.UserIsPlayer() && this.configuration.advancedReload)
		{
			this.StartAdvancedReload();
			return;
		}
		if (base.gameObject.activeInHierarchy)
		{
			this.standardReload = base.StartCoroutine(this.StandardReloadRoutine());
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00066290 File Offset: 0x00064490
	public void CancelReload()
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.CancelReload();
			return;
		}
		this.reloading = false;
		if (this.standardReload != null)
		{
			base.StopCoroutine(this.standardReload);
			this.standardReload = null;
		}
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0000734E File Offset: 0x0000554E
	private void StartAdvancedReload()
	{
		this.animator.SetBool(Weapon.RELOADING_PARAMETER_HASH, true);
		this.AdvancedReloadNextMotion();
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x000662D8 File Offset: 0x000644D8
	private void AdvancedReloadNextMotion()
	{
		int num = Mathf.Min(this.configuration.ammo - this.ammo, this.spareAmmo);
		if (num == 0)
		{
			this.EndAdvancedReload();
			return;
		}
		this.currentReloadMotion = 0;
		foreach (int num2 in this.configuration.allowedReloads)
		{
			if (num2 <= num && num2 >= this.currentReloadMotion)
			{
				this.currentReloadMotion = num2;
			}
		}
		if (this.currentReloadMotion == 0)
		{
			this.EndAdvancedReload();
			return;
		}
		this.animator.SetInteger(Weapon.RELOAD_MOTION_PARAMETER_HASH, this.currentReloadMotion);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00007367 File Offset: 0x00005567
	private void EndAdvancedReload()
	{
		this.animator.SetBool(Weapon.RELOADING_PARAMETER_HASH, false);
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0006636C File Offset: 0x0006456C
	public void MotionDone()
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.MotionDone();
			return;
		}
		if (!this.configuration.advancedReload)
		{
			return;
		}
		int num = this.RemoveSpareAmmo(this.currentReloadMotion);
		this.ammo += num;
		this.OnAmmoChanged();
		this.AdvancedReloadNextMotion();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0000737A File Offset: 0x0000557A
	private IEnumerator StandardReloadRoutine()
	{
		yield return new WaitForSeconds(this.configuration.reloadTime);
		this.ReloadDone();
		yield break;
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x000663C8 File Offset: 0x000645C8
	public void ReloadDone()
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			activeSubWeapon.ReloadDone();
			return;
		}
		this.CancelReload();
		this.EnableOverrideLayer();
		if (!this.UserIsPlayer() || !this.configuration.advancedReload)
		{
			int num = this.configuration.ammo - this.ammo;
			if (this.configuration.useMaxAmmoPerReload)
			{
				num = Mathf.Min(num, this.configuration.maxAmmoPerReload);
			}
			int num2 = this.RemoveSpareAmmo(num);
			this.ammo += num2;
		}
		this.OnAmmoChanged();
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00007389 File Offset: 0x00005589
	public void InstantlyReload()
	{
		this.ReloadDone();
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0006645C File Offset: 0x0006465C
	protected virtual int RemoveSpareAmmo(int count)
	{
		if (this.HasInfiniteSpareAmmo())
		{
			return count;
		}
		int num = Mathf.Max(0, this.spareAmmo - count);
		int result = this.spareAmmo - num;
		this.spareAmmo = num;
		return result;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00066494 File Offset: 0x00064694
	public void OnAmmoChanged()
	{
		this.UpdateHasAmmoFlag();
		if (this.user != null)
		{
			this.user.AmmoChanged();
		}
		if (this.HasActiveAnimator() && this.GetActiveSubWeapon() == this)
		{
			this.animator.SetBool(Weapon.NO_AMMO_PARAMETER_HASH, !this.HasAnyAmmo());
			if (this.notifyAnimatorNoAmmoBlend)
			{
				this.animator.SetFloat(Weapon.NO_AMMO_BLEND_PARAMETER_HASH, this.HasAnyAmmo() ? 0f : 1f);
			}
			if (this.notifyAnimatorAmmo)
			{
				this.animator.SetInteger(Weapon.LOADED_AMMO_PARAMETER_HASH, this.ammo);
			}
		}
		if (!this.HasLoadedAmmo() && this.HasSpareAmmo() && !this.reloading && (this.configuration.forceAutoReload || Options.GetToggle(OptionToggle.Id.AutoReload)))
		{
			base.Invoke("AutoReload", this.configuration.autoReloadDelay);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00007391 File Offset: 0x00005591
	private void DisableOverrideLayer()
	{
		if (this.HasActiveAnimator() && this.animator.layerCount > 1)
		{
			this.animator.SetLayerWeight(1, 0f);
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x000073BA File Offset: 0x000055BA
	private void EnableOverrideLayer()
	{
		if (this.HasActiveAnimator() && this.animator.layerCount > 1)
		{
			this.animator.SetLayerWeight(1, 1f);
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0006657C File Offset: 0x0006477C
	public virtual bool CanFire()
	{
		return !this.reloading && !this.isLocked && this.HasLoadedAmmo() && (this.configuration.auto || !this.hasFiredSingleRoundThisTrigger) && !this.CoolingDown() && (!this.isOverheating || !this.configuration.applyHeat) && (this.targetTracker == null || this.targetTracker.CanFire()) && (!this.configuration.useChargeTime || this.IsCharged());
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x000073E3 File Offset: 0x000055E3
	public bool IsCharged()
	{
		return this.chargeAction.TrueDone() && this.holdingFire;
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x000073FA File Offset: 0x000055FA
	public bool CoolingDown()
	{
		return Time.time - this.lastFiredTimestamp < this.configuration.cooldown;
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00007415 File Offset: 0x00005615
	public bool AmmoFull()
	{
		return this.ammo >= this.configuration.ammo;
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00066608 File Offset: 0x00064808
	public void Shoot(bool force)
	{
		if (force || this.CanFire())
		{
			bool flag = this.user == null || !this.user.aiControlled;
			Vector3 direction = flag ? this.CurrentMuzzle().forward : this.user.controller.FacingDirection();
			this.Shoot(direction, flag);
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x0006666C File Offset: 0x0006486C
	protected virtual bool Shoot(Vector3 direction, bool useMuzzleDirection)
	{
		this.onFire.Invoke();
		if (this.onFireScriptable != null && this.onFireScriptable.isConsumed)
		{
			return false;
		}
		bool flag = this.user != null;
		float num = this.aiming ? this.configuration.followupMaxSpreadAim : this.configuration.followupMaxSpreadHip;
		if (flag && this.user.stance == Actor.Stance.Prone)
		{
			num *= this.configuration.followupSpreadProneMultiplier;
		}
		this.followupSpreadMagnitude = Mathf.Clamp(this.followupSpreadMagnitude, 0f, num);
		if (flag && this.configuration.loud)
		{
			this.user.Highlight(4f);
		}
		this.lastFiredTimestamp = Time.time;
		if (this.HasActiveAnimator())
		{
			if (!this.configuration.fireFromAllMuzzles && this.configuration.muzzles.Length > 1)
			{
				this.animator.SetInteger(Weapon.MUZZLE_PARAMETER_HASH, (int)this.currentMuzzleIndex);
			}
			this.animator.SetTrigger(Weapon.FIRE_PARAMETER_HASH);
		}
		if (this.configuration.fireFromAllMuzzles)
		{
			for (int i = 0; i < this.configuration.muzzles.Length; i++)
			{
				this.FireFromMuzzle(i, direction, useMuzzleDirection, flag);
			}
		}
		else if (this.configuration.muzzles.Length != 0)
		{
			this.FireFromMuzzle((int)this.currentMuzzleIndex, direction, useMuzzleDirection, flag);
		}
		if (this.ammo != -1)
		{
			this.ammo--;
		}
		if (flag)
		{
			this.ApplyRecoil();
		}
		this.OnAmmoChanged();
		if (this.reflectionVolume > 0f && this.reflectionSound != Weapon.ReflectionSound.None)
		{
			GameManager.PlayReflectionSound(this.UserIsPlayer(), this.configuration.auto, this.reflectionSound, this.reflectionVolume, base.transform.position + direction * 30f);
		}
		this.UpdateSoundOutputGroup();
		if (!this.configuration.auto)
		{
			this.audio.Play();
			this.hasFiredSingleRoundThisTrigger = true;
		}
		else if (this.ammo == 0)
		{
			this.StopFireLoop();
		}
		this.followupSpreadMagnitude = Mathf.Clamp(this.followupSpreadMagnitude + this.configuration.followupSpreadGain, 0f, num);
		this.followupSpreadStayAction.StartLifetime(this.configuration.followupSpreadStayTime);
		if (flag && this.configuration.loud && !this.IsMeleeWeapon())
		{
			try
			{
				MuzzleFlashManager.RegisterMuzzleFlash(this.configuration.muzzles[(int)this.currentMuzzleIndex].position, this.user, this.IsMountedWeapon());
			}
			catch
			{
			}
		}
		this.NextMuzzle();
		return true;
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00066908 File Offset: 0x00064B08
	protected virtual void ApplyRecoil()
	{
		if (this.UserIsPlayer() || !this.IsMountedWeapon())
		{
			Vector3 vector = this.configuration.kickback * Vector3.back + UnityEngine.Random.insideUnitSphere * this.configuration.randomKick;
			if (this.user.stance == Actor.Stance.Prone)
			{
				vector *= this.configuration.kickbackProneMultiplier;
			}
			this.user.ApplyRecoil(vector);
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00066984 File Offset: 0x00064B84
	private void FireFromMuzzle(int muzzleIndex, Vector3 direction, bool useMuzzleDirection, bool hasUser)
	{
		Transform transform = null;
		try
		{
			transform = this.configuration.muzzles[muzzleIndex];
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
			transform = base.transform;
		}
		if (transform == null)
		{
			transform = base.transform;
		}
		if (useMuzzleDirection)
		{
			direction = transform.forward;
		}
		Vector3 muzzlePosition = transform.position;
		if (hasUser && this.CanUseEyeAsMuzzle() && this.user.aiControlled && this.user.controller.UseEyeMuzzle())
		{
			muzzlePosition = ((AiActorController)this.user.controller).eyeTransform.localToWorldMatrix.MultiplyPoint(new Vector3(-0.25f, 0f, 1f));
		}
		Projectile[] array = new Projectile[this.configuration.projectilesPerShot];
		for (int i = 0; i < this.configuration.projectilesPerShot; i++)
		{
			array[i] = this.SpawnProjectile(direction, muzzlePosition, hasUser);
		}
		this.onSpawnProjectiles.Invoke(array);
		try
		{
			if (this.muzzleFlash.ContainsKey(transform))
			{
				this.muzzleFlash[transform].Play(true);
			}
		}
		catch (Exception e2)
		{
			ModManager.HandleModException(e2);
		}
		try
		{
			if (hasUser && !this.user.aiControlled && this.configuration.casingParticles.Length != 0)
			{
				this.configuration.casingParticles[muzzleIndex % this.configuration.casingParticles.Length].Play(false);
			}
		}
		catch (Exception e3)
		{
			ModManager.HandleModException(e3);
		}
		this.OnWeaponFire(muzzlePosition, direction);
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0000296E File Offset: 0x00000B6E
	public virtual void OnWeaponFire(Vector3 muzzlePosition, Vector3 fireDirection)
	{
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0000476F File Offset: 0x0000296F
	protected virtual bool CanUseEyeAsMuzzle()
	{
		return true;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00066B20 File Offset: 0x00064D20
	public virtual Transform CurrentMuzzle()
	{
		Transform result;
		try
		{
			if (this.configuration.fireFromAllMuzzles)
			{
				result = this.configuration.muzzles[0];
			}
			else
			{
				result = this.configuration.muzzles[(int)this.currentMuzzleIndex];
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
			result = base.transform;
		}
		return result;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00066B80 File Offset: 0x00064D80
	private void NextMuzzle()
	{
		try
		{
			this.currentMuzzleIndex = (byte)((int)(this.currentMuzzleIndex + 1) % this.configuration.muzzles.Length);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
			this.currentMuzzleIndex = 0;
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0000742D File Offset: 0x0000562D
	private void OnDestroy()
	{
		if (this.thirdPersonImposter != null)
		{
			UnityEngine.Object.Destroy(this.thirdPersonImposter);
		}
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00007448 File Offset: 0x00005648
	protected bool HasActiveAnimator()
	{
		return this.animator != null && this.animator.isActiveAndEnabled;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00007465 File Offset: 0x00005665
	public float GetCurrentSpreadMagnitude()
	{
		return this.configuration.spread + this.followupSpreadMagnitude;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00007479 File Offset: 0x00005679
	public float GetCurrentSpreadMaxAngleRadians()
	{
		return Mathf.Atan2(this.GetCurrentSpreadMagnitude() * 2f, 1f);
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00066BCC File Offset: 0x00064DCC
	protected virtual Projectile SpawnProjectile(Vector3 direction, Vector3 muzzlePosition, bool hasUser)
	{
		float currentSpreadMagnitude = this.GetCurrentSpreadMagnitude();
		Quaternion rotation = Quaternion.LookRotation(direction + UnityEngine.Random.insideUnitSphere * currentSpreadMagnitude);
		Projectile projectile = ProjectilePoolManager.InstantiateProjectile(this.configuration.projectilePrefab, muzzlePosition, rotation);
		projectile.killCredit = this.killCredit;
		projectile.sourceWeapon = this;
		if (this.targetTracker != null)
		{
			TargetSeekingMissile targetSeekingMissile = projectile as TargetSeekingMissile;
			if (targetSeekingMissile != null)
			{
				targetSeekingMissile.ClearTrackers();
				if (this.targetTracker.HasVehicleTarget() && this.targetTracker.TargetIsLocked())
				{
					targetSeekingMissile.SetTrackerTarget(this.targetTracker.vehicleTarget);
				}
				else if (this.targetTracker.HasPointTarget())
				{
					targetSeekingMissile.SetPointTargetProvider(this.targetTracker);
				}
			}
		}
		if (hasUser && !this.IsMountedWeapon() && (this.UserIsPlayer() || !this.hasAnyAttachedColliders))
		{
			projectile.performInfantryInitialMuzzleTravel = true;
			Vector3 lhs = muzzlePosition - this.user.controller.WeaponParent().position;
			projectile.initialMuzzleTravelDistance = Vector3.Dot(lhs, direction);
		}
		else
		{
			projectile.performInfantryInitialMuzzleTravel = false;
			projectile.initialMuzzleTravelDistance = 0f;
		}
		projectile.StartTravelling();
		return projectile;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00066CF0 File Offset: 0x00064EF0
	public virtual void Hide()
	{
		this.isHidden = true;
		foreach (Renderer renderer in this.renderers)
		{
			if (renderer != null)
			{
				renderer.enabled = false;
			}
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00066D54 File Offset: 0x00064F54
	public virtual void Show()
	{
		this.isHidden = false;
		foreach (Renderer renderer in this.renderers)
		{
			if (renderer != null)
			{
				renderer.enabled = true;
			}
		}
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x00066DB8 File Offset: 0x00064FB8
	public virtual void CullFpsObjects()
	{
		try
		{
			if (this.animator != null)
			{
				PatchedAnimatorPlayback component = this.animator.GetComponent<PatchedAnimatorPlayback>();
				if (component != null)
				{
					component.Cleanup();
					UnityEngine.Object.Destroy(component);
				}
				UnityEngine.Object.Destroy(this.animator);
				this.animator = null;
			}
			if (this.cullInThirdPerson != null)
			{
				GameObject[] array = this.cullInThirdPerson;
				for (int i = 0; i < array.Length; i++)
				{
					UnityEngine.Object.Destroy(array[i]);
				}
			}
			if (this.configuration.optionalThirdPersonMuzzles != null && this.configuration.optionalThirdPersonMuzzles.Length != 0)
			{
				this.configuration.muzzles = this.configuration.optionalThirdPersonMuzzles;
				this.FindMuzzleFlashParticles();
			}
			Transform[] muzzles = this.configuration.muzzles;
			for (int i = 0; i < muzzles.Length; i++)
			{
				muzzles[i].parent = this.thirdPersonTransform;
			}
			this.thirdPersonTransform.parent = base.transform;
			for (int j = 0; j < base.transform.childCount; j++)
			{
				Transform child = base.transform.GetChild(j);
				if (child != this.thirdPersonTransform)
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
			if (!this.keepScriptsOnThirdPersonImposter)
			{
				ScriptedBehaviour.DestroyAll(base.gameObject);
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x00066F20 File Offset: 0x00065120
	public GameObject CreateTpImposter(out Transform imposterThirdPersonTransform)
	{
		imposterThirdPersonTransform = null;
		if (this.thirdPersonTransform != null)
		{
			this.thirdPersonImposter = UnityEngine.Object.Instantiate<GameObject>(base.gameObject);
			Weapon component = this.thirdPersonImposter.GetComponent<Weapon>();
			component.CullFpsObjects();
			imposterThirdPersonTransform = component.thirdPersonTransform;
			UnityEngine.Object.Destroy(component);
			return this.thirdPersonImposter;
		}
		return null;
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00066F78 File Offset: 0x00065178
	protected bool IsMuzzleTransform(Transform t)
	{
		for (int i = 0; i < this.configuration.muzzles.Length; i++)
		{
			if (this.configuration.muzzles[i] == t)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00007491 File Offset: 0x00005691
	public bool IsEmpty()
	{
		return this.ammo == 0;
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00066FB8 File Offset: 0x000651B8
	public virtual void Equip(Actor user)
	{
		this.user = user;
		if (this.targetTracker != null)
		{
			this.targetTracker.OnEquip();
		}
		if (this.arms != null && this.UserIsPlayer())
		{
			Material actorMaterial = ColorScheme.GetActorMaterial(GameManager.PlayerTeam());
			if (this.allowArmMeshReplacement)
			{
				ActorSkin actorSkin = ActorManager.instance.actorSkin[GameManager.PlayerTeam()];
				if (actorSkin != null && actorSkin.armSkin.mesh != null)
				{
					ActorManager.ApplyOverrideMeshSkin(this.arms, actorSkin.armSkin, GameManager.PlayerTeam());
					return;
				}
				this.arms.sharedMesh = WeaponManager.instance.defaultArms;
				this.arms.materials = new Material[]
				{
					actorMaterial
				};
			}
		}
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0000749C File Offset: 0x0000569C
	public virtual void Drop()
	{
		this.user = null;
		this.holdingFire = false;
		this.reloading = false;
		base.CancelInvoke();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0006707C File Offset: 0x0006527C
	public virtual void UnholsterSubWeapon(bool switchedFromSubWeapon)
	{
		this.unholstered = false;
		this.holdingFire = false;
		this.hasFiredSingleRoundThisTrigger = false;
		this.isLocked = false;
		this.switchedFromSubWeapon = switchedFromSubWeapon;
		if (this.user != null)
		{
			this.user.AmmoChanged();
		}
		if (switchedFromSubWeapon && this.changeFireModeAudio != null)
		{
			this.changeFireModeAudio.Play();
		}
		this.DisableOverrideLayer();
		base.Invoke("UnholsterDone", switchedFromSubWeapon ? this.configuration.changeFireModeTime : this.configuration.unholsterTime);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00067110 File Offset: 0x00065310
	public virtual void Unholster()
	{
		if (this.UserIsPlayer() && this.unholsterAudio != null)
		{
			this.unholsterAudio.Play();
		}
		this.UnholsterSubWeapon(false);
		this.aiming = false;
		this.showScopeObject = false;
		this.activeSubWeaponIndex = -1;
		if (this.configuration.unholsterIsReload)
		{
			this.InstantlyReload();
		}
		if (this.HasActiveAnimator())
		{
			if (this.notifyAnimatorRandom)
			{
				this.animator.SetFloat(Weapon.RANDOM_PARAMETER_HASH, UnityEngine.Random.Range(0f, 100f));
			}
			this.animator.SetTrigger(Weapon.UNHOLSTER_PARAMETER_HASH);
		}
		this.OnWeaponAnimatorActivated();
		if (this.thirdPersonImposter != null)
		{
			this.thirdPersonImposter.SetActive(true);
		}
		this.Show();
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000074C4 File Offset: 0x000056C4
	public void UnholsterDone()
	{
		this.EnableOverrideLayer();
		this.unholstered = true;
		if (this.switchedFromSubWeapon && this.configuration.changeFireModeIsReload)
		{
			this.ReloadDone();
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x000671D4 File Offset: 0x000653D4
	public virtual void HolsterSubWeapon()
	{
		this.unholstered = false;
		this.reloading = false;
		this.holdingFire = false;
		this.chargeAction.Stop();
		if (this.fireLoopPlaying)
		{
			this.StopFireLoop();
		}
		if (this.reloadAudio != null && this.reloadAudio.isPlaying)
		{
			this.reloadAudio.Stop();
		}
		base.CancelInvoke();
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000074EE File Offset: 0x000056EE
	public virtual void Holster()
	{
		this.GetActiveSubWeapon().HolsterSubWeapon();
		this.aiming = false;
		if (this.thirdPersonImposter != null)
		{
			this.thirdPersonImposter.SetActive(false);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00007528 File Offset: 0x00005728
	public void LockWeapon()
	{
		this.isLocked = true;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00007531 File Offset: 0x00005731
	public void UnlockWeapon()
	{
		this.isLocked = false;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0006723C File Offset: 0x0006543C
	public Weapon.Effectiveness EffectivenessAgainst(Actor.TargetType targetType)
	{
		switch (targetType)
		{
		case Actor.TargetType.InfantryGroup:
			return this.configuration.effInfantryGroup;
		case Actor.TargetType.Unarmored:
			return this.configuration.effUnarmored;
		case Actor.TargetType.Armored:
			return this.configuration.effArmored;
		case Actor.TargetType.Air:
			return this.configuration.effAir;
		case Actor.TargetType.AirFastMover:
			return this.configuration.effAirFastMover;
		default:
			return this.configuration.effInfantry;
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x000672B0 File Offset: 0x000654B0
	public Weapon.Difficulty EvaluateDifficulty(float distance, Actor.TargetType targetType)
	{
		Weapon.Difficulty difficulty;
		switch (targetType)
		{
		case Actor.TargetType.Infantry:
			difficulty = this.configuration.diffInfantry;
			goto IL_66;
		case Actor.TargetType.InfantryGroup:
			difficulty = this.configuration.diffInfantryGroup;
			goto IL_66;
		case Actor.TargetType.Air:
			difficulty = this.configuration.diffAir;
			goto IL_66;
		case Actor.TargetType.AirFastMover:
			difficulty = this.configuration.diffAirFastMover;
			goto IL_66;
		}
		difficulty = this.configuration.diffGroundVehicles;
		IL_66:
		if (distance > this.configuration.effectiveRange)
		{
			difficulty++;
		}
		return difficulty;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0000753A File Offset: 0x0000573A
	public bool AllowsResupply()
	{
		return this.configuration.spareAmmo >= 0;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00067338 File Offset: 0x00065538
	public virtual bool Resupply()
	{
		bool flag = false;
		if (this.AllowsResupply())
		{
			int num = this.spareAmmo;
			this.spareAmmo = Mathf.Min(this.configuration.spareAmmo, this.spareAmmo + this.configuration.resupplyNumber);
			flag = (num != this.spareAmmo);
		}
		foreach (Weapon weapon in this.alternativeWeapons)
		{
			flag |= weapon.Resupply();
		}
		return flag;
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0000754D File Offset: 0x0000574D
	public bool HasSpareAmmo()
	{
		return this.HasInfiniteSpareAmmo() || this.spareAmmo > 0;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00007562 File Offset: 0x00005762
	public bool HasLoadedAmmo()
	{
		return this.ammo > 0 || this.configuration.ammo == -1;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0000757D File Offset: 0x0000577D
	public bool HasAnyAmmo()
	{
		return this.cachedHasAnyAmmo;
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00007585 File Offset: 0x00005785
	public bool HasInfiniteSpareAmmo()
	{
		return this.configuration.spareAmmo == -2;
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00007596 File Offset: 0x00005796
	public void SetupFirstPerson()
	{
		this.MoveToFirstPersonLayer(base.transform);
		this.AssignFpAudioMix();
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x000673D4 File Offset: 0x000655D4
	private void MoveToFirstPersonLayer(Transform t)
	{
		if (t.GetComponent<Projector>() == null)
		{
			t.gameObject.layer = 18;
		}
		for (int i = 0; i < t.childCount; i++)
		{
			this.MoveToFirstPersonLayer(t.GetChild(i));
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0006741C File Offset: 0x0006561C
	public void AssignFpAudioMix()
	{
		if (this.audio == null)
		{
			this.audio = base.GetComponent<AudioSource>();
		}
		this.audio.spatialBlend = 0.4f;
		if (!this.configuration.forceWorldAudioOutput)
		{
			this.audio.outputAudioMixerGroup = GameManager.instance.fpMixerGroup;
			this.audio.priority = 0;
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsToggleable()
	{
		return false;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsMeleeWeapon()
	{
		return false;
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsMountedWeapon()
	{
		return false;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00067484 File Offset: 0x00065684
	public virtual bool CanBeAimed()
	{
		Weapon activeSubWeapon = this.GetActiveSubWeapon();
		if (activeSubWeapon != this)
		{
			return activeSubWeapon.CanBeAimed();
		}
		return (this.configuration.canBeAimedWhileReloading || !this.reloading) && this.unholstered;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool CanRepair()
	{
		return false;
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x000075AA File Offset: 0x000057AA
	public bool UserIsPlayer()
	{
		return this.user != null && !this.user.aiControlled;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x000075CA File Offset: 0x000057CA
	public bool UserIsAI()
	{
		return this.user != null && this.user.aiControlled;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x000075E7 File Offset: 0x000057E7
	public bool ShouldUseFineAim()
	{
		return this.aiming || this.configuration.alwaysUseFineAim;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x000674C8 File Offset: 0x000656C8
	public Vector3 GetLead(Vector3 origin, Vector3 target, Vector3 targetVelocity)
	{
		Vector3 a = target - origin;
		float num = a.magnitude / this.projectileSpeed;
		Vector3 normalized = a.normalized;
		float num2 = 1f + AiActorController.PARAMETERS.LEAD_NOISE_MAGNITUDE * this.SmoothNoise(0.2f).x;
		num *= num2;
		Vector3 a2 = Vector3.zero;
		if (this.targetTracker != null)
		{
			a2 = Vector3.zero;
		}
		else if (this.isWireGuided)
		{
			float num3 = Time.time - this.lastFiredTimestamp;
			float d = 0f;
			if (num3 < num)
			{
				d = Mathf.Clamp01(0.9f - num3 / num);
			}
			a2 = targetVelocity * num * d;
		}
		else
		{
			if (this.isBomb)
			{
				return Vector3.zero;
			}
			float num4 = num;
			for (int i = 0; i < 1; i++)
			{
				Vector3 b = this.projectileGravity * Mathf.Pow(num4, 2f) / 2f;
				num4 = num / Vector3.Dot((a - b).normalized, normalized);
			}
			a2 = targetVelocity * num4 - this.projectileGravity * Mathf.Pow(num4, 2f) / 2f;
		}
		return a2 + this.SmoothNoise(0.23333f) * AiActorController.PARAMETERS.LEAD_SWAY_MAGNITUDE;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00067638 File Offset: 0x00065838
	public float DropBombTravelTime(Vector3 target)
	{
		float num = this.CurrentMuzzle().position.y - target.y;
		float y = Physics.gravity.y;
		float y2 = (this.user.Velocity() + this.CurrentMuzzle().forward * this.projectileSpeed).y;
		return -(y2 / y) + Mathf.Sqrt(Mathf.Pow(y2 / y, 2f) - 2f * num / y);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x000676B8 File Offset: 0x000658B8
	public bool ShouldDropBombs(Vector3 target, Vector3 targetVelocity)
	{
		Vector3 position = this.CurrentMuzzle().position;
		float y = position.y - target.y;
		Vector3 a = this.user.Velocity() + this.CurrentMuzzle().forward * this.projectileSpeed;
		float d = this.DropBombTravelTime(target);
		Vector3 b = target + targetVelocity * d;
		Vector3 a2 = position + a * d;
		Debug.DrawLine(this.CurrentMuzzle().position, a2 - new Vector3(0f, y, 0f), Color.red, 1f);
		return (a2 - b).ToGround().magnitude < this.configuration.aiAllowedAimSpread;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00067780 File Offset: 0x00065980
	private Vector3 SmoothNoise(float frequency)
	{
		float num = frequency * Time.time;
		return new Vector3(Mathf.Sin(num * 7.9f + this.smoothNoisePhase), Mathf.Sin(num * 8.3f + this.smoothNoisePhase), Mathf.Sin(num * 8.9f + this.smoothNoisePhase));
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000075FE File Offset: 0x000057FE
	private bool HasScopeObject()
	{
		return this.configuration.scopeAimObject != null;
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000677D4 File Offset: 0x000659D4
	public virtual void AutoAssignWeaponReflectionSound()
	{
		if (this.configuration.projectilePrefab == null)
		{
			this.reflectionSound = Weapon.ReflectionSound.None;
			return;
		}
		Projectile component = this.configuration.projectilePrefab.GetComponent<Projectile>();
		if (!this.configuration.loud || component == null)
		{
			this.reflectionSound = Weapon.ReflectionSound.None;
			return;
		}
		Type type = component.GetType();
		if (type == typeof(ExplodingProjectile) || type == typeof(Rocket) || type == typeof(WireGuidedMissile) || type == typeof(TargetSeekingMissile))
		{
			this.reflectionSound = Weapon.ReflectionSound.Launcher;
			return;
		}
		if (component.armorDamage > Vehicle.ArmorRating.HeavyArms)
		{
			this.reflectionSound = Weapon.ReflectionSound.RifleLarge;
			return;
		}
		if (this.configuration.auto || component.configuration.damage > 30f)
		{
			this.reflectionSound = Weapon.ReflectionSound.RifleSmall;
			return;
		}
		this.reflectionSound = Weapon.ReflectionSound.Handgun;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0000476F File Offset: 0x0000296F
	public virtual bool CanAimAt(Vector3 position)
	{
		return true;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000678C4 File Offset: 0x00065AC4
	public int AddSubWeapon(Weapon subWeapon)
	{
		int count = this.alternativeWeapons.Count;
		this.alternativeWeapons.Add(subWeapon);
		subWeapon.parentWeapon = this;
		subWeapon.user = this.user;
		subWeapon.animator = this.animator;
		subWeapon.weaponEntry = this.weaponEntry;
		return count;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00067914 File Offset: 0x00065B14
	public void RemoveSubWeapon(Weapon weapon)
	{
		int subWeaponIndex = this.alternativeWeapons.IndexOf(weapon);
		this.RemoveSubWeapon(subWeaponIndex);
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00007611 File Offset: 0x00005811
	public void RemoveSubWeapon(int subWeaponIndex)
	{
		if (this.activeSubWeaponIndex == subWeaponIndex)
		{
			this.EquipSubWeapon(-1);
		}
		Weapon weapon = this.alternativeWeapons[subWeaponIndex];
		weapon.parentWeapon = null;
		weapon.user = null;
		weapon.animator = null;
		this.alternativeWeapons.RemoveAt(subWeaponIndex);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0000764F File Offset: 0x0000584F
	public void SwitchFireMode()
	{
		if (this.GetActiveSubWeapon().reloading)
		{
			return;
		}
		this.NextSubWeapon();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00007665 File Offset: 0x00005865
	public virtual Weapon GetActiveSubWeapon()
	{
		if (this.HasParentWeapon())
		{
			return this;
		}
		return this.GetSubWeapon(this.activeSubWeaponIndex);
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0000767D File Offset: 0x0000587D
	public Weapon GetSubWeapon(int index)
	{
		if (index == -1)
		{
			return this;
		}
		return this.alternativeWeapons[index];
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00067938 File Offset: 0x00065B38
	public void NextSubWeapon()
	{
		int num = this.activeSubWeaponIndex + 1;
		if (num >= this.alternativeWeapons.Count)
		{
			num = -1;
		}
		this.EquipSubWeapon(num);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00067968 File Offset: 0x00065B68
	public void EquipSubWeapon(int subWeaponIndex)
	{
		if (this.activeSubWeaponIndex != subWeaponIndex)
		{
			this.GetSubWeapon(this.activeSubWeaponIndex).HolsterSubWeapon();
			this.activeSubWeaponIndex = subWeaponIndex;
			this.GetActiveSubWeapon().UnholsterSubWeapon(true);
			if (this.animator != null)
			{
				this.animator.SetInteger(Weapon.ALT_WEAPON_PARAMETER_HASH, this.activeSubWeaponIndex + 1);
			}
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x000679C8 File Offset: 0x00065BC8
	public virtual bool NextSightMode()
	{
		if (this.HasParentWeapon() && this.useParentWeaponSightModes)
		{
			return this.parentWeapon.NextSightMode();
		}
		if (this.configuration.sightModes != null && this.configuration.sightModes.Length != 0)
		{
			this.activeSightModeIndex = (this.activeSightModeIndex + 1) % this.configuration.sightModes.Length;
			return true;
		}
		return false;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00067A2C File Offset: 0x00065C2C
	public virtual bool PreviousSightMode()
	{
		if (this.HasParentWeapon() && this.useParentWeaponSightModes)
		{
			return this.parentWeapon.PreviousSightMode();
		}
		if (this.configuration.sightModes != null && this.configuration.sightModes.Length != 0)
		{
			this.activeSightModeIndex = (this.activeSightModeIndex - 1 + this.configuration.sightModes.Length) % this.configuration.sightModes.Length;
			return true;
		}
		return false;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00067AA0 File Offset: 0x00065CA0
	public float GetAimFov()
	{
		if (this.HasParentWeapon() && this.useParentWeaponSightModes)
		{
			return this.parentWeapon.GetAimFov();
		}
		if (!this.hasAlternateSightModes)
		{
			return this.configuration.aimFov;
		}
		if (!this.configuration.sightModes[this.activeSightModeIndex].overrideFov)
		{
			return this.configuration.aimFov;
		}
		return this.configuration.sightModes[this.activeSightModeIndex].fov;
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x000659B0 File Offset: 0x00063BB0
	private bool HasAutoDifficultyValues()
	{
		return this.configuration.diffAir == Weapon.Difficulty.Auto || this.configuration.diffAirFastMover == Weapon.Difficulty.Auto || this.configuration.diffGroundVehicles == Weapon.Difficulty.Auto || this.configuration.diffInfantry == Weapon.Difficulty.Auto || this.configuration.diffInfantryGroup == Weapon.Difficulty.Auto;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00067B24 File Offset: 0x00065D24
	public Weapon.WeaponRole GenerateWeaponRoleFromStats()
	{
		Vehicle vehicle = null;
		if (this is MountedWeapon)
		{
			vehicle = base.GetComponentInParent<Vehicle>();
		}
		return GameManager.GenerateWeaponRole(this, vehicle);
	}

	// Token: 0x04000806 RID: 2054
	private const float FINE_AIM_FOV = 30f;

	// Token: 0x04000807 RID: 2055
	private const float AIM_AMOUNT_SPEED = 6f;

	// Token: 0x04000808 RID: 2056
	public const int LAYER_THIRD_PERSON = 15;

	// Token: 0x04000809 RID: 2057
	public const int LAYER_FIRST_PERSON = 18;

	// Token: 0x0400080A RID: 2058
	public const int HIGHEST_AUDIO_PRIORITY = 0;

	// Token: 0x0400080B RID: 2059
	public static readonly int RANDOM_PARAMETER_HASH = Animator.StringToHash("random");

	// Token: 0x0400080C RID: 2060
	public static readonly int CHARGING_PARAMETER_HASH = Animator.StringToHash("charging");

	// Token: 0x0400080D RID: 2061
	public static readonly int OVERHEAT_PARAMETER_HASH = Animator.StringToHash("overheat");

	// Token: 0x0400080E RID: 2062
	public static readonly int SIGHT_MODE_PARAMETER_HASH = Animator.StringToHash("sight mode");

	// Token: 0x0400080F RID: 2063
	public static readonly int SMOOTH_SIGHT_MODE_PARAMETER_HASH = Animator.StringToHash("smooth sight mode");

	// Token: 0x04000810 RID: 2064
	public static readonly int FIRE_PARAMETER_HASH = Animator.StringToHash("fire");

	// Token: 0x04000811 RID: 2065
	public static readonly int AIM_PARAMETER_HASH = Animator.StringToHash("aim");

	// Token: 0x04000812 RID: 2066
	public static readonly int RELOAD_PARAMETER_HASH = Animator.StringToHash("reload");

	// Token: 0x04000813 RID: 2067
	public static readonly int RELOADING_PARAMETER_HASH = Animator.StringToHash("reloading");

	// Token: 0x04000814 RID: 2068
	public static readonly int RELOAD_MOTION_PARAMETER_HASH = Animator.StringToHash("reload motion");

	// Token: 0x04000815 RID: 2069
	public static readonly int NO_AMMO_PARAMETER_HASH = Animator.StringToHash("no ammo");

	// Token: 0x04000816 RID: 2070
	public static readonly int NO_AMMO_BLEND_PARAMETER_HASH = Animator.StringToHash("no ammo blend");

	// Token: 0x04000817 RID: 2071
	public static readonly int LOADED_AMMO_PARAMETER_HASH = Animator.StringToHash("loaded ammo");

	// Token: 0x04000818 RID: 2072
	public static readonly int UNHOLSTER_PARAMETER_HASH = Animator.StringToHash("unholster");

	// Token: 0x04000819 RID: 2073
	public static readonly int TUCK_PARAMETER_HASH = Animator.StringToHash("tuck");

	// Token: 0x0400081A RID: 2074
	public static readonly int MUZZLE_PARAMETER_HASH = Animator.StringToHash("muzzle");

	// Token: 0x0400081B RID: 2075
	public static readonly int ALT_WEAPON_PARAMETER_HASH = Animator.StringToHash("alt weapon");

	// Token: 0x0400081C RID: 2076
	[NonSerialized]
	public Actor killCredit;

	// Token: 0x0400081D RID: 2077
	private Actor _user;

	// Token: 0x0400081E RID: 2078
	public Transform thirdPersonTransform;

	// Token: 0x0400081F RID: 2079
	public Vector3 thirdPersonOffset = Vector3.zero;

	// Token: 0x04000820 RID: 2080
	public Vector3 thirdPersonRotation = new Vector3(0f, 0f, -90f);

	// Token: 0x04000821 RID: 2081
	public float thirdPersonScale = 1f;

	// Token: 0x04000822 RID: 2082
	public GameObject[] cullInThirdPerson;

	// Token: 0x04000823 RID: 2083
	public Weapon.Configuration configuration;

	// Token: 0x04000824 RID: 2084
	public AudioSource reverbAudio;

	// Token: 0x04000825 RID: 2085
	public AudioSource reloadAudio;

	// Token: 0x04000826 RID: 2086
	public AudioSource changeFireModeAudio;

	// Token: 0x04000827 RID: 2087
	public AudioSource unholsterAudio;

	// Token: 0x04000828 RID: 2088
	public Weapon.ReflectionSound reflectionSound;

	// Token: 0x04000829 RID: 2089
	public float reflectionVolume = 0.35f;

	// Token: 0x0400082A RID: 2090
	public bool hasAnyAttachedColliders;

	// Token: 0x0400082B RID: 2091
	public float walkBobMultiplier = 1f;

	// Token: 0x0400082C RID: 2092
	public float sprintBobMultiplier = 1f;

	// Token: 0x0400082D RID: 2093
	public float proneBobMultiplier = 1f;

	// Token: 0x0400082E RID: 2094
	public Sprite uiSprite;

	// Token: 0x0400082F RID: 2095
	public bool keepScriptsOnThirdPersonImposter;

	// Token: 0x04000830 RID: 2096
	[NonSerialized]
	public int ammo;

	// Token: 0x04000831 RID: 2097
	[NonSerialized]
	public bool reloading;

	// Token: 0x04000832 RID: 2098
	protected float lastFiredTimestamp;

	// Token: 0x04000833 RID: 2099
	[NonSerialized]
	public bool holdingFire;

	// Token: 0x04000834 RID: 2100
	[NonSerialized]
	public bool unholstered;

	// Token: 0x04000835 RID: 2101
	private bool switchedFromSubWeapon;

	// Token: 0x04000836 RID: 2102
	protected AudioSource audio;

	// Token: 0x04000837 RID: 2103
	protected float weaponVolume = 1f;

	// Token: 0x04000838 RID: 2104
	protected TimedAction stopFireLoop = new TimedAction(0.12f, false);

	// Token: 0x04000839 RID: 2105
	[NonSerialized]
	public float projectileSpeed;

	// Token: 0x0400083A RID: 2106
	[NonSerialized]
	public Vector3 projectileGravity;

	// Token: 0x0400083B RID: 2107
	[NonSerialized]
	public Vehicle.ArmorRating projectileArmorRating;

	// Token: 0x0400083C RID: 2108
	[NonSerialized]
	public Animator animator;

	// Token: 0x0400083D RID: 2109
	[NonSerialized]
	public int slot = -1;

	// Token: 0x0400083E RID: 2110
	[NonSerialized]
	public bool aiming;

	// Token: 0x0400083F RID: 2111
	[NonSerialized]
	public TargetTracker targetTracker;

	// Token: 0x04000840 RID: 2112
	[NonSerialized]
	public WeaponManager.WeaponEntry weaponEntry;

	// Token: 0x04000841 RID: 2113
	[NonSerialized]
	public float teammateDangerRange = -1f;

	// Token: 0x04000842 RID: 2114
	public Camera renderTextureCamera;

	// Token: 0x04000843 RID: 2115
	private bool fireLoopPlaying;

	// Token: 0x04000844 RID: 2116
	public SkinnedMeshRenderer arms;

	// Token: 0x04000845 RID: 2117
	public bool allowArmMeshReplacement = true;

	// Token: 0x04000846 RID: 2118
	[NonSerialized]
	public bool isLocked;

	// Token: 0x04000847 RID: 2119
	public Weapon parentWeapon;

	// Token: 0x04000848 RID: 2120
	public bool useParentWeaponSightModes;

	// Token: 0x04000849 RID: 2121
	protected List<Renderer> renderers;

	// Token: 0x0400084A RID: 2122
	protected List<Renderer> nonScopeRenderers;

	// Token: 0x0400084B RID: 2123
	protected bool isWireGuided;

	// Token: 0x0400084C RID: 2124
	[NonSerialized]
	public bool isBomb;

	// Token: 0x0400084D RID: 2125
	[NonSerialized]
	public bool isHidden;

	// Token: 0x0400084E RID: 2126
	private TimedAction followupSpreadStayAction = new TimedAction(0.2f, false);

	// Token: 0x0400084F RID: 2127
	private float followupSpreadMagnitude;

	// Token: 0x04000850 RID: 2128
	private float followupSpreadDissipateRate = 1f;

	// Token: 0x04000851 RID: 2129
	private float smoothNoisePhase;

	// Token: 0x04000852 RID: 2130
	private float aimAmount;

	// Token: 0x04000853 RID: 2131
	protected bool showScopeObject;

	// Token: 0x04000854 RID: 2132
	[NonSerialized]
	public bool isOverheating;

	// Token: 0x04000855 RID: 2133
	[NonSerialized]
	public float heat;

	// Token: 0x04000856 RID: 2134
	private TimedAction heatStayAction;

	// Token: 0x04000857 RID: 2135
	private TimedAction chargeAction;

	// Token: 0x04000858 RID: 2136
	private int currentReloadMotion;

	// Token: 0x04000859 RID: 2137
	private bool notifyAnimatorAmmo;

	// Token: 0x0400085A RID: 2138
	private bool notifyAnimatorNoAmmoBlend;

	// Token: 0x0400085B RID: 2139
	private bool notifyAnimatorRandom;

	// Token: 0x0400085C RID: 2140
	private bool notifyAnimatorCharge;

	// Token: 0x0400085D RID: 2141
	private bool notifySmoothSightMode;

	// Token: 0x0400085E RID: 2142
	private float smoothSightMode;

	// Token: 0x0400085F RID: 2143
	private bool hasAlternateSightModes;

	// Token: 0x04000860 RID: 2144
	private Dictionary<Transform, ParticleSystem> muzzleFlash;

	// Token: 0x04000861 RID: 2145
	[NonSerialized]
	public List<Weapon> alternativeWeapons = new List<Weapon>();

	// Token: 0x04000862 RID: 2146
	private GameObject thirdPersonImposter;

	// Token: 0x04000863 RID: 2147
	private bool cachedHasAnyAmmo = true;

	// Token: 0x04000864 RID: 2148
	protected bool hasFiredSingleRoundThisTrigger;

	// Token: 0x04000865 RID: 2149
	[NonSerialized]
	public int activeSubWeaponIndex = -1;

	// Token: 0x04000866 RID: 2150
	[NonSerialized]
	public int activeSightModeIndex;

	// Token: 0x04000867 RID: 2151
	[NonSerialized]
	public int spareAmmo;

	// Token: 0x04000868 RID: 2152
	private Coroutine standardReload;

	// Token: 0x04000869 RID: 2153
	[NonSerialized]
	public UnityEvent onFire = new UnityEvent();

	// Token: 0x0400086A RID: 2154
	[NonSerialized]
	public ScriptEvent onFireScriptable;

	// Token: 0x0400086B RID: 2155
	[NonSerialized]
	public Weapon.OnSpawnProjectilesEvent onSpawnProjectiles = new Weapon.OnSpawnProjectilesEvent();

	// Token: 0x0400086C RID: 2156
	[NonSerialized]
	public bool allowProne = true;

	// Token: 0x0400086D RID: 2157
	private byte currentMuzzleIndex;

	// Token: 0x02000111 RID: 273
	public enum HaltStrategy
	{
		// Token: 0x0400086F RID: 2159
		Auto,
		// Token: 0x04000870 RID: 2160
		Never,
		// Token: 0x04000871 RID: 2161
		PreferredLongRange,
		// Token: 0x04000872 RID: 2162
		PreferredAnyRange,
		// Token: 0x04000873 RID: 2163
		AlwaysLongRange,
		// Token: 0x04000874 RID: 2164
		Always
	}

	// Token: 0x02000112 RID: 274
	public enum Effectiveness
	{
		// Token: 0x04000876 RID: 2166
		No,
		// Token: 0x04000877 RID: 2167
		Yes,
		// Token: 0x04000878 RID: 2168
		Preferred
	}

	// Token: 0x02000113 RID: 275
	public enum Difficulty
	{
		// Token: 0x0400087A RID: 2170
		Auto,
		// Token: 0x0400087B RID: 2171
		Easy,
		// Token: 0x0400087C RID: 2172
		Challenging,
		// Token: 0x0400087D RID: 2173
		Hard,
		// Token: 0x0400087E RID: 2174
		Impossible
	}

	// Token: 0x02000114 RID: 276
	public enum ReflectionSound
	{
		// Token: 0x04000880 RID: 2176
		Auto,
		// Token: 0x04000881 RID: 2177
		None,
		// Token: 0x04000882 RID: 2178
		Handgun,
		// Token: 0x04000883 RID: 2179
		RifleSmall,
		// Token: 0x04000884 RID: 2180
		RifleLarge,
		// Token: 0x04000885 RID: 2181
		Launcher,
		// Token: 0x04000886 RID: 2182
		Tank
	}

	// Token: 0x02000115 RID: 277
	public class OnSpawnProjectilesEvent : UnityEvent<Projectile[]>
	{
	}

	// Token: 0x02000116 RID: 278
	public enum WeaponRole
	{
		// Token: 0x04000888 RID: 2184
		AutoRifle,
		// Token: 0x04000889 RID: 2185
		Sniper,
		// Token: 0x0400088A RID: 2186
		Handgun,
		// Token: 0x0400088B RID: 2187
		Shotgun,
		// Token: 0x0400088C RID: 2188
		AutoCannon,
		// Token: 0x0400088D RID: 2189
		RocketLauncher,
		// Token: 0x0400088E RID: 2190
		GrenadeLauncher,
		// Token: 0x0400088F RID: 2191
		MissileLauncher,
		// Token: 0x04000890 RID: 2192
		AntiAir,
		// Token: 0x04000891 RID: 2193
		DogfightGuns,
		// Token: 0x04000892 RID: 2194
		Utility,
		// Token: 0x04000893 RID: 2195
		Grenade,
		// Token: 0x04000894 RID: 2196
		Mortar,
		// Token: 0x04000895 RID: 2197
		Melee
	}

	// Token: 0x02000117 RID: 279
	[Serializable]
	public class Configuration
	{
		// Token: 0x04000896 RID: 2198
		public bool auto;

		// Token: 0x04000897 RID: 2199
		public int ammo = 10;

		// Token: 0x04000898 RID: 2200
		public int spareAmmo = 50;

		// Token: 0x04000899 RID: 2201
		public int resupplyNumber = 10;

		// Token: 0x0400089A RID: 2202
		public float reloadTime = 2f;

		// Token: 0x0400089B RID: 2203
		public float cooldown = 0.2f;

		// Token: 0x0400089C RID: 2204
		public float unholsterTime = 1.2f;

		// Token: 0x0400089D RID: 2205
		public bool unholsterIsReload;

		// Token: 0x0400089E RID: 2206
		public float changeFireModeTime = 0.3f;

		// Token: 0x0400089F RID: 2207
		public bool changeFireModeIsReload;

		// Token: 0x040008A0 RID: 2208
		public float aimFov = 50f;

		// Token: 0x040008A1 RID: 2209
		public bool alwaysUseFineAim;

		// Token: 0x040008A2 RID: 2210
		public bool forceSniperAimSensitivity;

		// Token: 0x040008A3 RID: 2211
		public float aimSensitivityMultiplier = 1f;

		// Token: 0x040008A4 RID: 2212
		public float autoReloadDelay;

		// Token: 0x040008A5 RID: 2213
		public bool canBeAimedWhileReloading;

		// Token: 0x040008A6 RID: 2214
		public bool forceAutoReload;

		// Token: 0x040008A7 RID: 2215
		public bool loud = true;

		// Token: 0x040008A8 RID: 2216
		public bool forceWorldAudioOutput;

		// Token: 0x040008A9 RID: 2217
		public Transform[] muzzles;

		// Token: 0x040008AA RID: 2218
		public Transform[] optionalThirdPersonMuzzles;

		// Token: 0x040008AB RID: 2219
		public ParticleSystem[] casingParticles;

		// Token: 0x040008AC RID: 2220
		public bool fireFromAllMuzzles;

		// Token: 0x040008AD RID: 2221
		public int projectilesPerShot = 1;

		// Token: 0x040008AE RID: 2222
		public GameObject projectilePrefab;

		// Token: 0x040008AF RID: 2223
		public GameObject scopeAimObject;

		// Token: 0x040008B0 RID: 2224
		public float kickback = 2f;

		// Token: 0x040008B1 RID: 2225
		public float randomKick = 0.2f;

		// Token: 0x040008B2 RID: 2226
		public float spread;

		// Token: 0x040008B3 RID: 2227
		public float followupSpreadGain;

		// Token: 0x040008B4 RID: 2228
		public float followupMaxSpreadHip;

		// Token: 0x040008B5 RID: 2229
		public float followupMaxSpreadAim;

		// Token: 0x040008B6 RID: 2230
		public float followupSpreadStayTime;

		// Token: 0x040008B7 RID: 2231
		public float followupSpreadDissipateTime = 1f;

		// Token: 0x040008B8 RID: 2232
		public float snapMagnitude = 0.3f;

		// Token: 0x040008B9 RID: 2233
		public float snapDuration = 0.4f;

		// Token: 0x040008BA RID: 2234
		public float snapFrequency = 4f;

		// Token: 0x040008BB RID: 2235
		public float kickbackProneMultiplier = 0.6f;

		// Token: 0x040008BC RID: 2236
		public float spreadProneMultiplier = 1f;

		// Token: 0x040008BD RID: 2237
		public float followupSpreadProneMultiplier = 0.5f;

		// Token: 0x040008BE RID: 2238
		public float snapProneMultiplier = 0.5f;

		// Token: 0x040008BF RID: 2239
		public float aiAllowedAimSpread = 1f;

		// Token: 0x040008C0 RID: 2240
		public float aiAimSwing;

		// Token: 0x040008C1 RID: 2241
		public Weapon.Effectiveness effInfantry = Weapon.Effectiveness.Yes;

		// Token: 0x040008C2 RID: 2242
		public Weapon.Effectiveness effInfantryGroup;

		// Token: 0x040008C3 RID: 2243
		public Weapon.Effectiveness effUnarmored = Weapon.Effectiveness.Yes;

		// Token: 0x040008C4 RID: 2244
		public Weapon.Effectiveness effArmored;

		// Token: 0x040008C5 RID: 2245
		public Weapon.Effectiveness effAir;

		// Token: 0x040008C6 RID: 2246
		public Weapon.Effectiveness effAirFastMover;

		// Token: 0x040008C7 RID: 2247
		public Weapon.Difficulty diffInfantry;

		// Token: 0x040008C8 RID: 2248
		public Weapon.Difficulty diffInfantryGroup;

		// Token: 0x040008C9 RID: 2249
		public Weapon.Difficulty diffGroundVehicles;

		// Token: 0x040008CA RID: 2250
		public Weapon.Difficulty diffAir;

		// Token: 0x040008CB RID: 2251
		public Weapon.Difficulty diffAirFastMover;

		// Token: 0x040008CC RID: 2252
		public float effectiveRange = 600f;

		// Token: 0x040008CD RID: 2253
		public Weapon.HaltStrategy haltStrategy;

		// Token: 0x040008CE RID: 2254
		public int pose;

		// Token: 0x040008CF RID: 2255
		public bool applyHeat;

		// Token: 0x040008D0 RID: 2256
		public MaterialTarget heatMaterial;

		// Token: 0x040008D1 RID: 2257
		public float heatGainPerShot;

		// Token: 0x040008D2 RID: 2258
		public float heatDrainRate = 0.25f;

		// Token: 0x040008D3 RID: 2259
		public Color heatColor;

		// Token: 0x040008D4 RID: 2260
		public AnimationCurve heatColorGain;

		// Token: 0x040008D5 RID: 2261
		public ParticleSystem overheatParticles;

		// Token: 0x040008D6 RID: 2262
		public AudioSource overheatSound;

		// Token: 0x040008D7 RID: 2263
		public bool useChargeTime;

		// Token: 0x040008D8 RID: 2264
		public float chargeTime = 0.5f;

		// Token: 0x040008D9 RID: 2265
		public AudioSource chargeSound;

		// Token: 0x040008DA RID: 2266
		public bool dropAmmoWhenReloading;

		// Token: 0x040008DB RID: 2267
		public int maxRemainingAmmoAfterDrop;

		// Token: 0x040008DC RID: 2268
		public bool useMaxAmmoPerReload;

		// Token: 0x040008DD RID: 2269
		public int maxAmmoPerReload = 30;

		// Token: 0x040008DE RID: 2270
		public bool advancedReload;

		// Token: 0x040008DF RID: 2271
		public int[] allowedReloads;

		// Token: 0x040008E0 RID: 2272
		public Weapon.SightMode[] sightModes;
	}

	// Token: 0x02000118 RID: 280
	[Serializable]
	public struct SightMode
	{
		// Token: 0x040008E1 RID: 2273
		public bool overrideFov;

		// Token: 0x040008E2 RID: 2274
		public float fov;

		// Token: 0x040008E3 RID: 2275
		public string name;
	}
}
