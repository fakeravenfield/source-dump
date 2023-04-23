using System;
using System.Collections;
using System.Collections.Generic;
using Lua;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000060 RID: 96
public class Actor : Hurtable
{
	// Token: 0x060001B5 RID: 437 RVA: 0x00044FF4 File Offset: 0x000431F4
	public virtual void Awake()
	{
		this.animator.enabled = false;
		this.movementPosition = ActorManager.ACTOR_INSTANTIATE_POSITION;
		this.accessoryRenderer = new List<Renderer>();
		this.accessoryRagdollRenderer = new List<Renderer>();
		this.ragdoll.ragdollObject.SetActive(true);
		this.skinnedRenderer = this.animator.GetComponentInChildren<SkinnedMeshRenderer>();
		this.skinnedRendererRagdoll = this.ragdoll.ragdollObject.GetComponentInChildren<SkinnedMeshRenderer>();
		this.ragdoll.ragdollObject.SetActive(false);
		this.ragdoll.OnStartRagdoll = new ActiveRaggy.OnStatechange(this.OnStartRagdoll);
		this.ragdoll.OnEndRagdoll = new ActiveRaggy.OnStatechange(this.OnEndRagdoll);
		this.animatedParentObject = this.ragdoll.animatedObject.transform;
		this.ik = this.animator.GetComponent<ActorIk>();
		this.parentOffset = base.transform.localPosition;
		this.originalParent = base.transform.parent;
		this.aiControlled = (this.controller.GetType() == typeof(AiActorController));
		this.dead = true;
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.hitboxColliders = this.ragdoll.animatedColliders;
		this.ragdollColliders = this.ragdoll.ragdollObject.GetComponentsInChildren<Collider>();
		this.animatedBones = ActorManager.GetRecursiveBones(this.skinnedRenderer.rootBone);
		this.ragdollBones = ActorManager.GetRecursiveBones(this.skinnedRendererRagdoll.rootBone);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000032BF File Offset: 0x000014BF
	public void Start()
	{
		if (this.name.ToLowerInvariant() == "rick")
		{
			this.AddAccessory(ActorManager.instance.walkmanAccessory);
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000032E8 File Offset: 0x000014E8
	public void ShiftClosestEnemyDistance()
	{
		this.closestEnemyDistance = this.closestEnemyDistancePreShift;
		this.closestEnemyDistancePreShift = 9999f;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00045174 File Offset: 0x00043374
	private void OnStartRagdoll()
	{
		foreach (Renderer renderer in this.accessoryRenderer)
		{
			renderer.enabled = false;
		}
		foreach (Renderer renderer2 in this.accessoryRagdollRenderer)
		{
			renderer2.enabled = true;
		}
		this.isFallingAsRagdoll = false;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0004520C File Offset: 0x0004340C
	private void OnEndRagdoll()
	{
		foreach (Renderer renderer in this.accessoryRenderer)
		{
			renderer.enabled = true;
		}
		foreach (Renderer renderer2 in this.accessoryRagdollRenderer)
		{
			renderer2.enabled = false;
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00003301 File Offset: 0x00001501
	public bool IsAiming()
	{
		return this.aiming;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00003309 File Offset: 0x00001509
	public void ForceStance(Actor.Stance stance)
	{
		this.stance = stance;
		this.controller.ForceChangeStance(stance);
	}

	// Token: 0x060001BC RID: 444 RVA: 0x000452A0 File Offset: 0x000434A0
	public void SpawnAt(Vector3 position, Quaternion rotation, WeaponManager.LoadoutSet forcedLoadout = null)
	{
		Vector3 eulerAngles = rotation.eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.z = 0f;
		rotation = Quaternion.Euler(eulerAngles);
		this.animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		this.EnableHitboxColliders();
		this.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		this.ragdoll.SetDrive(700f, 3f);
		this.ragdoll.SetControl(true);
		this.ragdoll.InstantAnimate();
		this.hasSpawnedAmmoReserve = false;
		this.hasHeroArmor = false;
		this.isInvulnerable = false;
		this.makesProximityMovementNoise = false;
		this.visibilityDistanceModifier = 0f;
		this.isScheduledToSpawn = false;
		this.animatedSoldierHeightOffset = 0f;
		this.immersedInWater = false;
		this.feetAreInWater = false;
		this.swimmingAction.Stop();
		this.CancelClimbObstacle();
		this.climbObstacleCooldownAction.Start();
		this.bonusSprintSpeedAction.StartLifetime(3f);
		this.ForceStance(Actor.Stance.Stand);
		if (this.autoMoveActor)
		{
			this.SetPositionAndRotation(position, rotation);
		}
		this.weaponImposterRenderers = new Dictionary<Weapon, Renderer[]>();
		WeaponManager.LoadoutSet loadoutSet = forcedLoadout;
		if (loadoutSet == null)
		{
			loadoutSet = GameModeBase.instance.GetLoadout(this);
		}
		if (!this.aiControlled)
		{
			RavenscriptManager.events.onActorSelectedLoadout.Invoke(this, loadoutSet, null);
		}
		else
		{
			RavenscriptManager.events.onActorSelectedLoadout.Invoke(this, loadoutSet, ((AiActorController)this.controller).loadoutStrategy);
		}
		this.SpawnLoadoutWeapons(loadoutSet);
		if (this.seat != null)
		{
			this.seat.OccupantLeft();
			this.seat = null;
		}
		this.CutParachutes();
		this.ladder = null;
		this.moving = false;
		this.ik.turnBody = true;
		this.ik.weight = 1f;
		this.ik.SetHandIkEnabled(false, false);
		this.animator.enabled = true;
		this.animator.SetLayerWeight(2, 0f);
		this.animator.SetTrigger(Actor.ANIM_PAR_RESET);
		this.balance = this.maxBalance;
		this.health = this.maxHealth;
		this.dead = false;
		this.controller.EnableInput();
		this.controller.SpawnAt(position, rotation);
		this.fallenOver = false;
		this.getupAction.Stop();
		this.getupActionWasStarted = false;
		this.needsResupply = false;
		this.aimAnimationLayerWeight = 1f;
		this.parachuteDeployed = false;
		this.parachuteDeployAction.Stop();
		this.controller.EnableMovement();
		this.fallStartHeight = position.y;
		this.parachuteDeployStunAction.Stop();
		this.ResetParachuteCountdown();
		this.animator.SetBool(Actor.ANIM_PAR_DEAD, false);
		this.animator.SetBool(Actor.ANIM_PAR_SEATED, false);
		this.animator.SetBool(Actor.ANIM_PAR_RELOADING, false);
		this.animator.SetBool(Actor.ANIM_PAR_CLIMBING, false);
		this.Show();
		this.Unfreeze();
		this.EnableHitboxColliders();
		if (!this.aiControlled)
		{
			IngameUi.instance.Show();
			IngameUi.instance.SetHealth(Mathf.Max(0f, this.health));
		}
		ActorManager.SetAlive(this);
		this.UpdateCachedValues();
		RavenscriptManager.events.onActorSpawn.Invoke(this);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000331E File Offset: 0x0000151E
	public bool CanSpawnAmmoReserve()
	{
		return this.dropsAmmoOnKick && !this.hasSpawnedAmmoReserve;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x000455D0 File Offset: 0x000437D0
	private void SpawnLoadoutWeapons(WeaponManager.LoadoutSet loadout)
	{
		this.hasAmmoBox = false;
		this.hasMedipack = false;
		this.hasRepairTool = false;
		this.hasSmokeScreen = false;
		this.SpawnWeapon(loadout.primary, 0);
		this.SpawnWeapon(loadout.secondary, 1);
		this.SpawnWeapon(loadout.gear1, 2);
		this.SpawnWeapon(loadout.gear2, 3);
		this.SpawnWeapon(loadout.gear3, 4);
		this.SwitchToFirstAvailableWeapon();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00045648 File Offset: 0x00043848
	public Weapon EquipNewWeaponEntry(WeaponManager.WeaponEntry entry, int slotNumber, bool autoEquip)
	{
		bool flag = this.CanUnholsterWeapon() && (autoEquip || slotNumber == this.activeWeaponSlot);
		if (flag)
		{
			this.HolsterActiveWeapon();
		}
		this.DropWeapon(slotNumber);
		Weapon result = this.SpawnWeapon(entry, slotNumber);
		if (flag)
		{
			this.Unholster(this.weapons[slotNumber]);
		}
		return result;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0004569C File Offset: 0x0004389C
	private void SwitchToFirstAvailableWeapon()
	{
		for (int i = 0; i < 5; i++)
		{
			if (this.HasWeaponInSlot(i))
			{
				this.activeWeaponSlot = i;
				this.Unholster(this.weapons[i]);
				return;
			}
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x000456D4 File Offset: 0x000438D4
	private Weapon SpawnWeapon(WeaponManager.WeaponEntry entry, int slotNumber)
	{
		if (entry == null || entry.prefab == null)
		{
			this.weapons[slotNumber] = null;
			return null;
		}
		Weapon component = UnityEngine.Object.Instantiate<GameObject>(entry.prefab).GetComponent<Weapon>();
		component.gameObject.name = entry.name;
		component.weaponEntry = entry;
		if (this.aiControlled)
		{
			component.CullFpsObjects();
		}
		Transform thirdPersonTransform = component.thirdPersonTransform;
		if (thirdPersonTransform != null)
		{
			if (!this.aiControlled)
			{
				GameObject gameObject = component.CreateTpImposter(out thirdPersonTransform);
				gameObject.transform.parent = this.controller.TpWeaponParent();
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
				Collider[] componentsInChildren2 = gameObject.GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					componentsInChildren2[i].enabled = false;
				}
				Renderer[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].shadowCastingMode = ShadowCastingMode.ShadowsOnly;
				}
				gameObject.SetActive(false);
				this.weaponImposterRenderers.Add(component, componentsInChildren);
			}
			thirdPersonTransform.localEulerAngles = component.thirdPersonRotation;
			thirdPersonTransform.localPosition = component.thirdPersonOffset;
			thirdPersonTransform.localScale = new Vector3(component.thirdPersonScale, component.thirdPersonScale, component.thirdPersonScale);
		}
		if (!this.aiControlled)
		{
			component.SetupFirstPerson();
		}
		component.FindRenderers(this.aiControlled);
		component.Equip(this);
		component.transform.parent = this.controller.WeaponParent();
		component.transform.localPosition = Vector3.zero;
		component.transform.localRotation = Quaternion.identity;
		component.transform.localScale = Vector3.one;
		component.slot = slotNumber;
		component.ammo = component.configuration.ammo;
		this.weapons[slotNumber] = component;
		if (component.IsToggleable())
		{
			return component;
		}
		component.gameObject.SetActive(false);
		if (entry.type == WeaponManager.WeaponEntry.LoadoutType.ResupplyAmmo)
		{
			this.hasAmmoBox = true;
			this.ammoBoxSlot = slotNumber;
		}
		if (entry.type == WeaponManager.WeaponEntry.LoadoutType.ResupplyHealth)
		{
			this.hasMedipack = true;
			this.medipackSlot = slotNumber;
		}
		if (entry.type == WeaponManager.WeaponEntry.LoadoutType.Repair)
		{
			this.hasRepairTool = true;
			this.repairToolSlot = slotNumber;
		}
		if (entry.type == WeaponManager.WeaponEntry.LoadoutType.SmokeScreen)
		{
			this.hasSmokeScreen = true;
			this.smokeScreenSlot = slotNumber;
		}
		return component;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00003333 File Offset: 0x00001533
	public void EmoteSwing()
	{
		if (!this.dead && !this.ragdoll.IsRagdoll() && this.animator.enabled)
		{
			this.animator.SetTrigger(Actor.ANIM_PAR_MELEE);
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00003367 File Offset: 0x00001567
	public void EmoteHail()
	{
		if (!this.dead && !this.ragdoll.IsRagdoll() && this.animator.enabled)
		{
			this.animator.SetTrigger(Actor.ANIM_PAR_HAIL);
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000339B File Offset: 0x0000159B
	public void EmoteRegroup()
	{
		if (!this.dead && !this.ragdoll.IsRagdoll() && this.animator.enabled)
		{
			this.animator.SetTrigger(Actor.ANIM_PAR_REGROUP);
		}
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000033CF File Offset: 0x000015CF
	public void EmoteMove()
	{
		if (!this.dead && !this.ragdoll.IsRagdoll() && this.animator.enabled)
		{
			this.animator.SetTrigger(Actor.ANIM_PAR_MOVE);
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00003403 File Offset: 0x00001603
	public void EmoteHalt()
	{
		if (!this.dead && !this.ragdoll.IsRagdoll() && this.animator.enabled)
		{
			this.animator.SetTrigger(Actor.ANIM_PAR_HALT);
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00003437 File Offset: 0x00001637
	public bool IsSwimming()
	{
		return !this.swimmingAction.TrueDone();
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00045934 File Offset: 0x00043B34
	protected virtual void FixedUpdate()
	{
		if (this.isFrozen)
		{
			return;
		}
		if (this.ragdoll.ragdollObject.activeInHierarchy)
		{
			if (this.dead && !this.hipRigidbody.IsSleeping() && this.disableForceSleepAction.TrueDone() && this.hipRigidbody.velocity.magnitude < 0.01f)
			{
				Rigidbody[] rigidbodies = this.ragdoll.rigidbodies;
				for (int i = 0; i < rigidbodies.Length; i++)
				{
					rigidbodies[i].Sleep();
				}
			}
			if (this.immersedInWater)
			{
				this.ResetParachuteCountdown();
				this.fallStartHeight = this.Position().y;
				this.hipRigidbody.AddForce(-Physics.gravity * 3f, ForceMode.Acceleration);
				this.hipRigidbody.drag = 8f;
				this.hipRigidbody.angularDrag = 30f;
				if (!this.dead)
				{
					this.headRigidbody.AddForce(-Physics.gravity * 3.5f, ForceMode.Acceleration);
					this.headRigidbody.angularDrag = 3f;
					this.headRigidbody.drag = 10f;
				}
			}
			else
			{
				this.hipRigidbody.drag = 0f;
				this.headRigidbody.drag = 0f;
				this.hipRigidbody.angularDrag = 0.05f;
				this.headRigidbody.angularDrag = 0.05f;
			}
			if (!this.dead && WaterLevel.IsInWater(this.CenterPosition()))
			{
				Vector3 a = this.controller.SwimInput() * 15f;
				this.hipRigidbody.AddForce(-Physics.gravity * 3f + a * 0.2f, ForceMode.Acceleration);
				this.headRigidbody.AddForce(-Physics.gravity * 3f + a * 0.8f, ForceMode.Acceleration);
			}
			if (this.parachuteDeployed)
			{
				this.headRigidbody.AddForce(Physics.gravity * this.hipRigidbody.velocity.y, ForceMode.Acceleration);
			}
		}
		if (this.parachuteDeployed && !this.fallenOver)
		{
			this.ControlParachute();
		}
		if (this.IsOnLadder())
		{
			this.movementPosition = this.ladder.GetPositionAtHeight(this.ladderHeight);
			base.transform.position = this.movementPosition;
		}
		if (!this.IsSeated())
		{
			if (this.autoMoveActor)
			{
				try
				{
					this.rigidbody.MovePosition(this.movementPosition);
					return;
				}
				catch (Exception ex)
				{
					Debug.LogError("Handling exception, moving actor to origin. Exception: " + ex.Message);
					this.rigidbody.position = Vector3.up;
					if (!this.dead)
					{
						this.Die(new DamageInfo(DamageInfo.DamageSourceType.Exception, null, null), true);
					}
					return;
				}
			}
			this.movementPosition = this.rigidbody.position;
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00045C34 File Offset: 0x00043E34
	private void UpdateCachedValues()
	{
		this.cachedPosition = this.GenerateCachedPosition();
		this.cachedTargetType = this.GenerateCachedTargetType();
		this.cachedVelocity = this.GenerateCachedVelocity();
		if (this.IsSeated() && this.seat.enclosed)
		{
			this.cachedArmorRating = this.seat.vehicle.armorDamagedBy;
		}
		else
		{
			this.cachedArmorRating = Vehicle.ArmorRating.SmallArms;
		}
		this.cachedIsHighlighted = !this.highlightAction.TrueDone();
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00045CB0 File Offset: 0x00043EB0
	private void LateUpdate()
	{
		if (this.dead)
		{
			return;
		}
		if (this.parachuteDeployed)
		{
			this.parachuteParent.position = this.ragdoll.HumanBoneTransformActive(HumanBodyBones.Chest).position;
			Vector3 vector = -this.parachuteVelocity.normalized * 0.5f + new Vector3(Mathf.PerlinNoise((float)base.GetInstanceID(), Time.time * 0.33f) - 0.5f, 1f, Mathf.PerlinNoise(Time.time * 0.37f, (float)base.GetInstanceID()) - 0.5f);
			Vector3 forward = Vector3.ProjectOnPlane(Vector3.forward, vector);
			this.parachuteParent.rotation = Quaternion.LookRotation(forward, vector);
		}
		if (this.autoMoveActor)
		{
			if (this.IsSeated())
			{
				this.animatedSoldierHeightOffset = 0f;
			}
			else
			{
				float y = this.Position().y;
				float num = y - this.lastSoldierHeight;
				this.lastSoldierHeight = y;
				if (!this.IsClimbingObstacle())
				{
					this.animatedSoldierHeightOffset = Mathf.Clamp(this.animatedSoldierHeightOffset - num, -0.5f, 1.5f);
				}
				this.animatedSoldierHeightOffset = Mathf.MoveTowards(this.animatedSoldierHeightOffset, 0f, 5f * Time.deltaTime);
			}
			this.animatedParentObject.localPosition = new Vector3(0f, this.animatedSoldierHeightOffset, 0f);
		}
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00003447 File Offset: 0x00001647
	public bool WasRecentlyInWater()
	{
		return this.immersedInWater || !this.recentlyInWaterAction.TrueDone();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00045E14 File Offset: 0x00044014
	public virtual void Update()
	{
		this.UpdateDistanceCull();
		this.UpdateCachedValues();
		if (this.hasHeroArmor)
		{
			this.accumulatedHeroArmorDamage = Mathf.MoveTowards(this.accumulatedHeroArmorDamage, 0f, Time.deltaTime * 40f * 1f);
		}
		if (GameManager.IsPaused() || this.isFrozen)
		{
			return;
		}
		bool flag = this.immersedInWater;
		this.feetAreInWater = WaterLevel.IsInWater(this.Position(), out this.waterDepth);
		this.waterHeight = this.Position().y + this.waterDepth;
		if (this.feetAreInWater)
		{
			Vector3 vector = this.WaterSamplePosition();
			float num = this.waterDepth;
			this.immersedInWater = (WaterLevel.IsInWater(vector, out this.waterDepth) && (!this.IsSeated() || !this.seat.allowUnderwater));
			if (this.immersedInWater)
			{
				this.waterHeight = vector.y + this.waterDepth;
			}
		}
		else
		{
			this.immersedInWater = false;
		}
		ActorData actorData = ActorManager.instance.actorData[this.actorIndex];
		actorData.position = this.Position();
		actorData.dead = this.dead;
		actorData.canCaptureFlag = (!this.dead && this.CanCapturePoint());
		actorData.team = this.team;
		actorData.isOnPlayerSquad = this.controller.IsOnPlayerSquad();
		actorData.facingDirection = this.controller.FacingDirection();
		actorData.visibleOnMinimap = (!this.dead && !this.IsSeated() && (this.IsHighlighted() || this.team == GameManager.PlayerTeam()));
		if (actorData.facingDirection.x == 0f && actorData.facingDirection.z == 0f)
		{
			actorData.facingDirection.z = 1f;
		}
		ActorManager.instance.actorData[this.actorIndex] = actorData;
		if (this.dead)
		{
			return;
		}
		if (this.immersedInWater)
		{
			this.recentlyInWaterAction.Start();
			if (this.stance == Actor.Stance.Stand && this.HasUnholsteredWeapon())
			{
				this.HolsterActiveWeapon();
			}
			this.swimmingAction.Start();
			if (this.fallenOver && !this.getupActionWasStarted && Mathf.Abs(this.Velocity().y) < 1f && Mathf.Abs(this.waterDepth) < 0.3f)
			{
				this.preventWaterRagdollAction.Start();
				Vector3 position = this.ragdoll.Position();
				position.y = this.waterHeight;
				if (this.aiControlled)
				{
					position.y -= 2f;
				}
				else
				{
					position.y -= 1.77f;
				}
				this.InstantGetUpAtPosition(position);
				this.ForceStance(Actor.Stance.Stand);
			}
		}
		if (flag && !this.immersedInWater && !this.fallenOver && this.CanUnholsterWeapon())
		{
			this.SwitchWeapon(this.activeWeaponSlot);
		}
		if (this.parachuteDeployed && this.parachuteDeployAction.TrueDone() && this.fallenOver)
		{
			this.InstantGetUpAtPosition(this.ragdoll.Position());
		}
		if (this.CanDeployParachute() && this.controller.DeployParachute())
		{
			this.DeployParachute();
		}
		if (this.IsOnLadder())
		{
			this.ladderHeight = Mathf.Clamp(this.ladderHeight + this.controller.LadderInput() * 3f * Time.deltaTime, 0f, this.ladder.height);
		}
		float target = 1f;
		if ((!this.controller.IdlePose() && this.controller.UseSprintingAnimation()) || (this.IsSeated() && !this.seat.CanUsePersonalWeapons()) || this.IsOnLadder() || this.IsSwimming() || !this.climbObstacleAnimationAction.TrueDone() || this.stance == Actor.Stance.Prone)
		{
			target = 0f;
		}
		this.aimAnimationLayerWeight = Mathf.MoveTowards(this.aimAnimationLayerWeight, target, 3f * Time.deltaTime);
		this.animator.SetLayerWeight(1, this.aimAnimationLayerWeight);
		if (this.immersedInWater && this.parachuteDeployed)
		{
			this.CutParachutes();
		}
		if (this.immersedInWater)
		{
			if (this.IsSeated())
			{
				this.LeaveSeat(true);
			}
			float y = this.Velocity().y;
			if (y < -7f && this.preventWaterRagdollAction.TrueDone() && !this.fallenOver)
			{
				this.FallOver();
				this.justFellNaturallyAction.Start();
			}
			if (!flag && y < -4f && this.splashCooldownAction.TrueDone())
			{
				Vector3 position2 = this.CenterPosition();
				position2.y = this.waterHeight;
				GameManager.CreateSplashEffect(true, position2);
				this.splashCooldownAction.Start();
			}
		}
		if (!this.hurtAction.Done() && !this.fallenOver && !this.dead)
		{
			float num2 = this.hurtAction.Ratio();
			if (num2 < 0.2f)
			{
				this.ik.weight = 0.5f - 2.5f * num2 + 0.5f;
			}
			else
			{
				this.ik.weight = 0.625f * (num2 - 0.2f) + 0.5f;
			}
		}
		if (!this.getupAction.TrueDone())
		{
			this.UpdateGetup();
		}
		else if (this.getupActionWasStarted && this.fallenOver)
		{
			this.FinishGettingUp();
		}
		if (!this.fallenOver)
		{
			this.UpdateFacing();
			if (!this.IsSeated() && !this.IsOnLadder())
			{
				this.UpdateMovement(Time.deltaTime);
				this.ResetAnimatedActorOffset();
			}
			else
			{
				this.moving = false;
			}
			if (this.aiControlled)
			{
				this.ResetParachuteCountdown();
			}
		}
		else
		{
			this.moving = false;
			this.UpdateRagdollStates();
		}
		this.aiming = ((this.controller.Aiming() || !this.aimingAction.TrueDone()) && !this.fallenOver && (this.activeWeapon == null || this.activeWeapon.CanBeAimed()));
		if (this.activeWeapon != null)
		{
			this.UpdateWeapon();
		}
		else
		{
			this.animator.SetBool(Actor.ANIM_PAR_RELOADING, false);
		}
		this.animator.SetBool(Actor.ANIM_PAR_SEATED, this.IsSeated());
		this.animator.SetBool(Actor.ANIM_PAR_IDLE, this.controller.IdlePose());
		this.animator.SetBool(Actor.ANIM_PAR_SWIM, this.IsSwimming());
		this.animator.SetBool(Actor.ANIM_PAR_CLIMBING, this.IsOnLadder());
		bool flag2 = (this.IsSeated() && !this.seat.CanUsePersonalWeapons()) || !this.controller.IsAlert() || this.IsOnLadder() || this.IsSwimming() || (this.moving && this.stance == Actor.Stance.Prone);
		this.ik.turnBody = !flag2;
		if (this.IsOnLadder())
		{
			this.smoothLadderClimbInput = Mathf.MoveTowards(this.smoothLadderClimbInput, this.controller.LadderInput(), Time.deltaTime * 3f);
			this.animator.SetFloat(Actor.ANIM_PAR_LADDER_Y, this.smoothLadderClimbInput);
			this.ResetParachuteCountdown();
		}
		if (this.IsSeated())
		{
			this.ResetParachuteCountdown();
			if (this.seat.CanUsePersonalWeapons())
			{
				Vector3 vector2 = this.seat.transform.worldToLocalMatrix.MultiplyVector(this.controller.FacingDirection());
				float num3 = Mathf.Atan2(vector2.z, vector2.x);
				int value;
				if (num3 < 0.7853982f && num3 > -0.7853982f)
				{
					value = 3;
				}
				else if (num3 <= -0.7853982f && num3 > -2.3561945f)
				{
					value = 2;
				}
				else if (num3 <= 2.3561945f && num3 > 0.7853982f)
				{
					value = 0;
				}
				else
				{
					value = 1;
				}
				this.animator.SetInteger(Actor.ANIM_PAR_SEATED_DIRECTION, value);
			}
			else
			{
				this.animator.SetInteger(Actor.ANIM_PAR_SEATED_DIRECTION, 0);
			}
		}
		this.balance = Mathf.Min(this.balance + Time.deltaTime * 10f, this.maxBalance);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00046620 File Offset: 0x00044820
	private void ControlParachute()
	{
		Vector2 vector = this.controller.ParachuteInput();
		if (!this.aiControlled)
		{
			Vector3 normalized = this.controller.FacingDirection().ToGround().normalized;
			Vector3 a = Vector3.Cross(normalized, Vector3.up);
			Vector3 target = (vector.x * a + vector.y * normalized) * 7f;
			target.y = -13f;
			this.parachuteVelocity = Vector3.MoveTowards(this.parachuteVelocity, target, 10f * Time.deltaTime);
			this.controller.Move(this.parachuteVelocity * Time.deltaTime);
			if (this.controller.OnGround())
			{
				this.CutParachutes();
			}
			return;
		}
		Vector3 target2 = new Vector3(vector.x, 0f, vector.y).normalized * 7f;
		target2.y = -13f;
		this.parachuteVelocity = Vector3.MoveTowards(this.parachuteVelocity, target2, 10f * Time.deltaTime);
		Vector3 vector2 = this.parachuteVelocity * Time.deltaTime;
		float magnitude = vector2.magnitude;
		Vector3 a2 = vector2 / magnitude;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(this.movementPosition - a2 * 2f, vector2), out raycastHit, magnitude + 2f, 2232321))
		{
			this.movementPosition = raycastHit.point;
			this.CutParachutes();
			return;
		}
		this.movementPosition += vector2;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00003461 File Offset: 0x00001661
	private bool CanFireWeapon()
	{
		return !this.fallenOver && !this.IsOnLadder() && (!this.IsSeated() || this.seat.CanUsePersonalWeapons() || this.seat.HasAnyMountedWeapons());
	}

	// Token: 0x060001CF RID: 463 RVA: 0x000467C0 File Offset: 0x000449C0
	private void SwitchFireMode()
	{
		UnityEngine.Object activeSubWeapon = this.activeWeapon.GetActiveSubWeapon();
		this.activeWeapon.SwitchFireMode();
		Weapon activeSubWeapon2 = this.activeWeapon.GetActiveSubWeapon();
		if (activeSubWeapon != activeSubWeapon2)
		{
			this.controller.SwitchedToWeapon(activeSubWeapon2);
			if (!this.aiControlled)
			{
				this.UpdateScopeUi();
			}
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00046814 File Offset: 0x00044A14
	private void UpdateWeapon()
	{
		bool flag = this.CanFireWeapon() && this.controller.Fire();
		if (flag)
		{
			this.activeWeapon.Fire(this.controller.FacingDirection(), this.controller.UseMuzzleDirection());
			this.bonusSprintSpeedAction.StartLifetime(25f);
		}
		else if (this.wasFiring)
		{
			this.activeWeapon.StopFire();
		}
		if (this.controller.SwitchFireMode())
		{
			this.SwitchFireMode();
		}
		this.wasFiring = flag;
		Weapon activeSubWeapon = this.activeWeapon.GetActiveSubWeapon();
		if (this.controller.NextSightMode())
		{
			if (activeSubWeapon.NextSightMode() && !this.aiControlled)
			{
				this.UpdateScopeUi();
				this.controller.ChangeAimFieldOfView(activeSubWeapon.GetAimFov());
			}
		}
		else if (this.controller.PreviousSightMode() && activeSubWeapon.PreviousSightMode() && !this.aiControlled)
		{
			this.UpdateScopeUi();
			this.controller.ChangeAimFieldOfView(activeSubWeapon.GetAimFov());
		}
		if (!activeSubWeapon.aiming && this.aiming)
		{
			activeSubWeapon.SetAiming(true);
			this.aimingAction.Start();
		}
		else if (activeSubWeapon.aiming && !this.aiming)
		{
			activeSubWeapon.SetAiming(false);
		}
		if (this.controller.Reload() && this.CanReload())
		{
			this.activeWeapon.Reload(false);
		}
		this.animator.SetBool(Actor.ANIM_PAR_RELOADING, this.activeWeapon.reloading);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00003497 File Offset: 0x00001697
	private bool CanReload()
	{
		return !this.dead && !this.fallenOver;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00046988 File Offset: 0x00044B88
	private void UpdateGetup()
	{
		float num = this.getupAction.Ratio();
		this.animator.SetLayerWeight(2, Mathf.Clamp01(2f * (1f - num)));
		this.ik.weight = num;
		if (num > 0.5f)
		{
			this.controller.EnableInput();
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x000034AC File Offset: 0x000016AC
	public float GetupProgress()
	{
		if (this.ragdoll.IsRagdoll())
		{
			return 0f;
		}
		return this.getupAction.Ratio();
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x000469E0 File Offset: 0x00044BE0
	private void UpdateFacing()
	{
		this.animator.SetFloat(Actor.ANIM_PAR_LEAN, this.controller.Lean());
		Vector3 vector;
		Quaternion quaternion;
		if (this.IsOnLadder())
		{
			vector = this.ladder.transform.forward;
			quaternion = Quaternion.LookRotation(vector, this.ladder.transform.up);
		}
		else if (!this.climbObstacleAnimationAction.TrueDone())
		{
			vector = this.climbObstacleFlatDelta.normalized;
			quaternion = Quaternion.LookRotation(vector, Vector3.up);
		}
		else
		{
			vector = this.controller.FacingDirection();
			quaternion = Quaternion.LookRotation(vector, Vector3.up);
		}
		if (this.IsOnLadder())
		{
			base.transform.rotation = this.ladder.transform.rotation;
		}
		else if (!this.IsSeated())
		{
			Quaternion to = Quaternion.Euler(Vector3.Scale(quaternion.eulerAngles, Actor.removePitchEuler));
			float f = Mathf.DeltaAngle(base.transform.eulerAngles.y, to.eulerAngles.y);
			float num = 50f;
			if (this.stance == Actor.Stance.Crouch && this.controller.IsAlert())
			{
				num = 20f;
			}
			else if (this.stance == Actor.Stance.Prone)
			{
				num = 10f;
			}
			if (Mathf.Abs(f) > num)
			{
				Quaternion rotation = Quaternion.RotateTowards(base.transform.rotation, to, Mathf.Abs(f) - num);
				rotation.eulerAngles.Scale(new Vector3(0f, 1f, 1f));
				base.transform.rotation = rotation;
			}
		}
		this.ik.aimPoint = base.transform.position + vector * 100f;
		if (this.stance == Actor.Stance.Prone)
		{
			ActorIk actorIk = this.ik;
			actorIk.aimPoint.y = actorIk.aimPoint.y - 40f;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x000034CC File Offset: 0x000016CC
	public bool JustChangedProneStance()
	{
		return !this.justChangedProneStanceAction.TrueDone();
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00046BBC File Offset: 0x00044DBC
	private void UpdateMovement(float dt)
	{
		if (this.aiControlled || !IngameMenuUi.IsOpen())
		{
			if (this.immersedInWater || this.parachuteDeployed)
			{
				this.wantedStance = Actor.Stance.Stand;
			}
			else if (this.controller.Prone())
			{
				this.wantedStance = Actor.Stance.Prone;
			}
			else if (this.controller.Crouch())
			{
				this.wantedStance = Actor.Stance.Crouch;
			}
			else
			{
				this.wantedStance = Actor.Stance.Stand;
			}
			if (this.wantedStance != this.stance && this.controller.ChangeStance(this.wantedStance))
			{
				if (this.stance == Actor.Stance.Prone || this.wantedStance == Actor.Stance.Prone)
				{
					this.justChangedProneStanceAction.Start();
				}
				this.stance = this.wantedStance;
			}
			this.animator.SetBool(Actor.ANIM_PAR_CROUCHED, this.stance == Actor.Stance.Crouch);
			this.animator.SetBool(Actor.ANIM_PAR_PRONE, this.stance == Actor.Stance.Prone);
		}
		if (!this.controller.OnGround())
		{
			return;
		}
		Vector3 vector = this.cachedVelocity;
		Vector3 vector2 = vector.ToGround();
		Debug.DrawRay(this.CenterPosition(), vector.normalized * 2f, Color.red);
		this.moving = this.controller.IsMoving();
		this.animator.SetBool(Actor.ANIM_PAR_MOVING, this.moving);
		this.animator.SetBool(Actor.ANIM_PAR_SPRINTING, this.controller.IsSprinting());
		Vector2 zero = Vector2.zero;
		if (this.moving)
		{
			bool flag = Vector3.Dot(vector, base.transform.forward) < 0f;
			Vector3 vector3 = base.transform.worldToLocalMatrix.MultiplyVector(vector2);
			zero = new Vector2(vector3.x, vector3.z);
			if (vector2 != Vector3.zero)
			{
				Quaternion b;
				if (!flag)
				{
					b = Quaternion.LookRotation(vector2);
				}
				else
				{
					b = Quaternion.LookRotation(-vector2);
				}
				this.rigidbody.MoveRotation(Quaternion.Slerp(base.transform.rotation, b, dt * 2f));
			}
		}
		float magnitude = vector2.magnitude;
		float num = magnitude * dt;
		this.movement = Vector2.Lerp(this.movement, zero, 5f * dt);
		this.animator.SetFloat(Actor.ANIM_PAR_MOVEMENT_X, this.movement.x);
		this.animator.SetFloat(Actor.ANIM_PAR_MOVEMENT_Y, this.movement.y);
		this.makesProximityMovementNoise = (this.stance == Actor.Stance.Stand && magnitude > 2f);
		Vector3 vector4 = this.movementPosition + vector * dt;
		Vector3 vector5;
		if (this.controller.CurrentWaypoint(out vector5) && (this.movementPosition - vector5).ToGround().magnitude < num)
		{
			vector4.x = vector5.x;
			vector4.z = vector5.z;
			this.controller.MarkReachedWaypoint();
		}
		float y = vector4.y;
		if (this.controller.ProjectToGround() && !this.IsOnLadder() && !this.IsClimbingObstacle() && !this.parachuteDeployed)
		{
			Ray ray = new Ray(vector4 + Vector3.up * 1.5f, Vector3.down);
			bool flag2 = false;
			float num2 = vector4.y;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 15f, 2232321) && raycastHit.distance < 4f)
			{
				num2 = raycastHit.point.y;
				flag2 = true;
			}
			else if (Physics.SphereCast(ray, 0.3f, out raycastHit, 15f, 2232321) && raycastHit.distance < 4f)
			{
				num2 = raycastHit.point.y;
				flag2 = true;
			}
			else if (!this.WasRecentlyInWater() && this.justExitedLadderAction.TrueDone())
			{
				this.FallOver();
				this.justFellNaturallyAction.Start();
			}
			if (flag2)
			{
				this.fallStartHeight = num2;
				bool onTerrain = raycastHit.collider != null && raycastHit.collider.GetType() == typeof(TerrainCollider);
				this.footstepPhase = (this.footstepPhase + magnitude * Time.deltaTime * 0.2f) % 1f;
				bool flag3 = this.footstepPhase < 0.5f;
				if (flag3 != this.footstepPhaseUnderHalf)
				{
					this.PlayFootstep(onTerrain);
				}
				this.footstepPhaseUnderHalf = flag3;
			}
			if (this.immersedInWater)
			{
				num2 = Mathf.Max(this.waterHeight - 1.1f, num2);
			}
			vector4.y = num2;
			if (this.immersedInWater || flag2)
			{
				this.ResetParachuteCountdown();
			}
			if (this.moving)
			{
				Vector3 vector6 = vector4 - this.movementPosition;
				Vector3 vector7 = vector6.ToGround();
				float y2 = vector6.y;
				float magnitude2 = vector7.magnitude;
				if (this.climbObstacleCooldownAction.TrueDone() && raycastHit.normal.y > 0.8f && num2 > y + 0.6f && Time.deltaTime < 0.1f)
				{
					this.climbObstacleStartPosition = this.movementPosition;
					this.climbObstacleDeltaHeight = y2;
					this.climbObstacleFlatDelta = vector7.normalized;
					this.climbObstacleStartPosition -= this.climbObstacleFlatDelta * 0.2f;
					this.StartObstacleClimbingAction();
				}
			}
		}
		if (!this.controller.ProjectToGround() && this.controller.OnGround())
		{
			this.fallStartHeight = this.Position().y;
		}
		if (!this.aiControlled && this.controller.OnGround())
		{
			this.ResetParachuteCountdown();
		}
		if (this.autoMoveActor && this.IsClimbingObstacle())
		{
			Vector3 b2 = Vector3.Lerp(Vector3.zero, this.climbObstacleFlatDelta, Mathf.Pow(this.climbObstacleAction.Ratio(), 2f));
			b2.y = Mathf.Lerp(0f, this.climbObstacleDeltaHeight, 1f - Mathf.Pow(1f - this.climbObstacleAction.Ratio(), 2f));
			vector4 = this.climbObstacleStartPosition + b2;
		}
		if (this.autoMoveActor && !this.IsOnLadder() && !this.parachuteDeployed)
		{
			this.movementPosition = vector4;
		}
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000034DC File Offset: 0x000016DC
	private void CancelClimbObstacle()
	{
		this.climbObstacleAction.Stop();
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000034E9 File Offset: 0x000016E9
	private bool IsClimbingObstacle()
	{
		return !this.climbObstacleAction.TrueDone();
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x000034F9 File Offset: 0x000016F9
	private void ResetParachuteCountdown()
	{
		if (this.aiControlled)
		{
			this.canUseParachuteAction.StartLifetime(UnityEngine.Random.Range(1f, 2f));
			return;
		}
		this.canUseParachuteAction.Start();
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00003529 File Offset: 0x00001729
	private void ResetAnimatedActorOffset()
	{
		if (base.transform.parent != null)
		{
			base.transform.localPosition = this.parentOffset;
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x000471F8 File Offset: 0x000453F8
	private void UpdateRagdollStates()
	{
		float magnitude = this.Velocity().magnitude;
		bool flag = magnitude > 2f;
		this.animator.SetBool(Actor.ANIM_PAR_FALLING, flag);
		this.animator.SetBool(Actor.ANIM_PAR_ON_BACK, this.ragdoll.OnBack());
		this.animator.SetBool(Actor.ANIM_PAR_SWIM_FORWARD, this.IsSwimming() && this.controller.SwimInput() != Vector3.zero);
		this.makesProximityMovementNoise = (magnitude > 2f);
		if (this.fallAction.TrueDone() && !flag && this.ragdoll.IsRagdoll() && !this.WasRecentlyInWater())
		{
			if (this.stopFallAction.TrueDone())
			{
				this.GetUp();
			}
		}
		else
		{
			this.stopFallAction.Start();
		}
		bool flag2 = this.isFallingAsRagdoll;
		this.isFallingAsRagdoll = (this.Velocity().y < -1f);
		if (!this.isFallingAsRagdoll)
		{
			this.ResetParachuteCountdown();
			if (flag2 && !this.getupActionWasStarted)
			{
				this.DealFallDamage();
				return;
			}
			this.fallStartHeight = this.Position().y;
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0004731C File Offset: 0x0004551C
	private void DealFallDamage()
	{
		float num = this.fallStartHeight - this.Position().y;
		if (num > 7f)
		{
			float num2 = (num - 7f) / 23f * 100f;
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.FallDamage, null, null)
			{
				healthDamage = num2,
				balanceDamage = num2,
				isPiercing = true,
				point = this.Position(),
				direction = -this.controller.FacingDirection()
			};
			this.Damage(info);
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000354F File Offset: 0x0000174F
	public void KnockOver(Vector3 force)
	{
		if (!this.ragdoll.IsRagdoll())
		{
			this.FallOver();
			this.ApplyRigidbodyForce(force);
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000356B File Offset: 0x0000176B
	public void ApplyRigidbodyForce(Vector3 force)
	{
		this.ragdoll.MainRigidbody().AddForce(force, ForceMode.Impulse);
	}

	// Token: 0x060001DF RID: 479 RVA: 0x000473A8 File Offset: 0x000455A8
	public void FallOver()
	{
		if (this.IsSeated())
		{
			this.LeaveSeat(true);
		}
		if (this.IsOnLadder())
		{
			this.ExitLadder();
		}
		this.animator.SetBool(Actor.ANIM_PAR_RAGDOLLED, true);
		this.ragdoll.Ragdoll(this.cachedVelocity);
		this.controller.DisableInput();
		this.controller.StartRagdoll();
		this.ik.weight = 0f;
		this.animator.SetLayerWeight(2, 1f);
		this.fallAction.Start();
		this.fallenOver = true;
		this.getupAction.Stop();
		this.getupActionWasStarted = false;
		if (this.HasUnholsteredWeapon())
		{
			this.activeWeapon.CancelReload();
			this.activeWeapon.SetAiming(false);
			this.activeWeapon.gameObject.SetActive(false);
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00047480 File Offset: 0x00045680
	private void GetUp()
	{
		this.ragdoll.Animate();
		Transform transform = this.ragdoll.ragdollObject.transform;
		Transform parent = transform.parent;
		transform.parent = null;
		this.controller.GettingUp();
		this.getupAction.Start();
		this.getupActionWasStarted = true;
		this.animator.SetBool(Actor.ANIM_PAR_RAGDOLLED, false);
		if (this.autoMoveActor)
		{
			Vector3 position = this.ragdoll.Position();
			this.SetPositionAndRotation(position, base.transform.rotation);
		}
		transform.parent = parent;
		this.bonusSprintSpeedAction.StartLifetime(25f);
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00047520 File Offset: 0x00045720
	private void FinishGettingUp()
	{
		this.fallenOver = false;
		this.getupAction.Stop();
		this.getupActionWasStarted = false;
		this.climbObstacleCooldownAction.Start();
		this.controller.EndRagdoll();
		if (this.HasUnholsteredWeapon())
		{
			this.activeWeapon.gameObject.SetActive(true);
			this.activeWeapon.OnWeaponAnimatorActivated();
		}
		this.bonusSprintSpeedAction.StartLifetime(25f);
		this.animatedSoldierHeightOffset = 0f;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0004759C File Offset: 0x0004579C
	private void InstantGetUp()
	{
		this.ragdoll.InstantAnimate();
		this.controller.GettingUp();
		this.controller.EnableInput();
		this.animator.SetLayerWeight(2, 0f);
		this.ik.weight = 1f;
		this.FinishGettingUp();
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000357F File Offset: 0x0000177F
	private void InstantGetUpAtPosition(Vector3 position)
	{
		this.movementPosition = position;
		this.controller.transform.position = position;
		this.InstantGetUp();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000359F File Offset: 0x0000179F
	public void Kill(DamageInfo info)
	{
		this.Die(info, false);
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x000035A9 File Offset: 0x000017A9
	public void KillSilently()
	{
		this.ragdoll.DisableAnimatedAndRagdollObjects();
		this.Die(DamageInfo.Default, true);
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000475F4 File Offset: 0x000457F4
	private void Die(DamageInfo info, bool isSilentKill)
	{
		Vector3 vector = this.Position();
		if (!isSilentKill)
		{
			PathfindingManager.RegisterDeath(vector);
			this.disableForceSleepAction.Start();
			this.ragdoll.SetDrive(50f, 1f);
			this.ragdoll.SetControl(false);
			this.ragdoll.Ragdoll(this.cachedVelocity);
			this.ApplyRigidbodyForce(info.impactForce);
			if (info.sourceActor != null && info.sourceActor != this)
			{
				info.sourceActor.OnGotKill(this);
			}
			if (this.HasScoreboardEntry())
			{
				this.scoreboardEntry.AddDeath();
			}
		}
		this.CancelClimbObstacle();
		this.controller.Die(info.sourceActor);
		this.animator.SetBool(Actor.ANIM_PAR_DEAD, true);
		RavenscriptManager.events.onActorDied.Invoke(this, info.sourceActor, isSilentKill);
		RavenscriptManager.events.onActorDiedInfo.Invoke(this, info, isSilentKill);
		for (int i = 0; i < 5; i++)
		{
			this.DropWeapon(i);
		}
		if (this.IsSeated())
		{
			this.LeaveSeat(true);
		}
		if (this.IsOnLadder())
		{
			this.ExitLadder();
		}
		this.CutParachutes();
		if (!this.aiControlled)
		{
			IngameUi.instance.Hide();
		}
		this.deathTimestamp = Time.time;
		this.fallAction.Stop();
		this.isHighPriotityTargetAction.Stop();
		this.fallenOver = false;
		this.getupAction.Stop();
		this.getupActionWasStarted = false;
		this.controller.DisableInput();
		this.animator.enabled = false;
		this.dead = true;
		this.makesProximityMovementNoise = false;
		ActorManager.SetDead(this);
		GameModeBase.instance.ActorDied(this, vector, isSilentKill);
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x000035C2 File Offset: 0x000017C2
	public bool IsReadyToSpawn()
	{
		return this.dead && !this.isDeactivated && !this.isScheduledToSpawn;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x000477A4 File Offset: 0x000459A4
	public void OnGotKill(Actor target)
	{
		if (!this.aiControlled && target.aiControlled && ((AiActorController)target.controller).modifier.showKillMessage)
		{
			KillIndicatorUi.ShowActorMessage("KILLED", target);
			KillIndicatorUi.PlayKillChime();
		}
		if (this.HasScoreboardEntry())
		{
			if (target.team == this.team)
			{
				this.scoreboardEntry.SubtractKill();
				return;
			}
			this.scoreboardEntry.AddKill();
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00047818 File Offset: 0x00045A18
	public void OnGotVehicleKill(bool disabled, Vehicle vehicle)
	{
		if (!this.aiControlled)
		{
			if (disabled)
			{
				KillIndicatorUi.ShowMessage("DISABLED " + vehicle.name);
			}
			else
			{
				KillIndicatorUi.ShowMessage("DESTROYED " + vehicle.name);
			}
		}
		if (this.HasScoreboardEntry() && !disabled)
		{
			this.scoreboardEntry.AddVehicle();
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x000035DF File Offset: 0x000017DF
	public void OnGotFlagCapture()
	{
		if (this.HasScoreboardEntry())
		{
			this.scoreboardEntry.AddFlag();
		}
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00047874 File Offset: 0x00045A74
	private bool CanDeployParachute()
	{
		return this.canDeployParachute && (!this.aiControlled || (this.fallenOver && this.parachuteDeployStunAction.TrueDone())) && !this.IsSeated() && this.canUseParachuteAction.Done() && !this.parachuteDeployed;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x000478C8 File Offset: 0x00045AC8
	public void DeployParachute()
	{
		if (this.parachuteDeployed)
		{
			return;
		}
		this.CancelClimbObstacle();
		this.parachuteVelocity = this.Velocity();
		this.parachuteAnimation.Stop();
		this.parachuteAnimation.Play(PlayMode.StopAll);
		if (this.isRendered)
		{
			this.parachuteRenderer.enabled = true;
		}
		this.parachuteDeployed = true;
		this.parachuteDeployAction.Start();
		this.animator.SetBool(Actor.ANIM_PAR_PARACHUTE, true);
		this.ForceStance(Actor.Stance.Stand);
		this.controller.DisableMovement();
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00047954 File Offset: 0x00045B54
	public void CutParachutes()
	{
		if (!this.parachuteDeployed)
		{
			return;
		}
		this.parachuteRenderer.enabled = false;
		this.parachuteDeployed = false;
		this.animator.SetBool(Actor.ANIM_PAR_PARACHUTE, false);
		this.controller.EnableMovement();
		this.controller.OnCancelParachute();
	}

	// Token: 0x060001EE RID: 494 RVA: 0x000035F4 File Offset: 0x000017F4
	public virtual Vector3 Position()
	{
		return this.cachedPosition;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x000035FC File Offset: 0x000017FC
	private Vector3 GenerateCachedPosition()
	{
		if (this.ragdoll.IsRagdoll())
		{
			return this.ragdoll.Position();
		}
		if (this.IsSeated())
		{
			return base.transform.position;
		}
		return this.movementPosition;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000479A4 File Offset: 0x00045BA4
	private Vector3 WaterSamplePosition()
	{
		if (this.ragdoll.IsRagdoll())
		{
			return this.CenterPosition();
		}
		if (this.IsSwimming())
		{
			Vector3 position = base.transform.position;
			position.y += 0.5f;
			Debug.DrawRay(position, Vector3.up + Vector3.forward * 0.1f, Color.red);
			return position;
		}
		if (this.stance == Actor.Stance.Stand)
		{
			return base.transform.position + new Vector3(0f, 1f, 0f);
		}
		if (this.stance == Actor.Stance.Crouch)
		{
			return base.transform.position + new Vector3(0f, 0.6f, 0f);
		}
		return base.transform.position + new Vector3(0f, 0.2f, 0f);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00003631 File Offset: 0x00001831
	public virtual Vector3 CenterPosition()
	{
		if (this.ragdoll.IsRenderingRagdollTransforms())
		{
			return this.ragdoll.HumanBoneTransformRagdoll(HumanBodyBones.Spine).position;
		}
		return this.ragdoll.HumanBoneTransformAnimated(HumanBodyBones.Spine).position;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00003663 File Offset: 0x00001863
	public virtual Vector3 Velocity()
	{
		return this.cachedVelocity;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000366B File Offset: 0x0000186B
	public Vector3 GenerateCachedVelocity()
	{
		if (this.IsSeated())
		{
			return this.seat.vehicle.Velocity();
		}
		if (this.ragdoll.IsRagdoll())
		{
			return this.ragdoll.Velocity();
		}
		return this.controller.Velocity();
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000036AA File Offset: 0x000018AA
	public float GetBonusSprintAmount()
	{
		if (this.bonusSprintSpeedAction.TrueDone())
		{
			return 1f;
		}
		return Mathf.Clamp01(4f * (this.bonusSprintSpeedAction.Ratio() - 0.75f));
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00047A90 File Offset: 0x00045C90
	public override bool Damage(DamageInfo info)
	{
		if (this.isInvulnerable)
		{
			return false;
		}
		this.onTakeDamage.Invoke(this, info.sourceActor, info);
		if (this.onTakeDamage.isConsumed)
		{
			return false;
		}
		if (!this.dead)
		{
			IngameUi.OnDamageDealt(info, new HitInfo(this));
		}
		float num = info.healthDamage;
		float num2 = info.balanceDamage;
		if (this.hasHeroArmor && !info.isScripted)
		{
			if (this.HeroArmorIgnoresDamage())
			{
				num = 0f;
			}
			else
			{
				float num3 = 40f - this.accumulatedHeroArmorDamage;
				bool flag = num3 < 0f;
				num3 = Mathf.Max(0f, num3);
				num = Mathf.Min(num, num3);
				this.accumulatedHeroArmorDamage += num;
				if (flag)
				{
					this.heroArmorIgnoreDamageAction.Start();
					this.accumulatedHeroArmorDamage = 0f;
				}
				this.accumulatedHeroArmorDamage = Mathf.Min(this.accumulatedHeroArmorDamage, 40f);
			}
			num2 = Mathf.Min(num2, 45f);
		}
		this.disableForceSleepAction.Start();
		bool flag2 = this.IsSeated();
		bool flag3 = flag2 && this.seat.enclosed;
		bool flag4 = flag2 && this.seat.enclosedDamagedByDirectFire;
		if (!info.isPiercing && flag3 && (!flag4 || info.isSplashDamage))
		{
			return false;
		}
		if (this.dead)
		{
			return false;
		}
		bool friendlyFire = info.sourceActor != null && info.sourceActor.team == this.team;
		if (num > 5f)
		{
			this.bonusSprintSpeedAction.StartLifetime(25f);
		}
		this.controller.ReceivedDamage(friendlyFire, info.healthDamage, info.balanceDamage, info.point, info.direction, info.impactForce);
		this.health -= num;
		if (!flag3)
		{
			this.balance = Mathf.Max(this.balance - num2, -100f);
		}
		int num4 = Mathf.Min(Mathf.CeilToInt(num / 10f), 16);
		float d = 0.1f;
		switch (BloodParticle.BLOOD_PARTICLE_SETTING)
		{
		case BloodParticle.BloodParticleType.Reduced:
			num4 /= 2;
			break;
		case BloodParticle.BloodParticleType.BloodExplosions:
			d = 0.3f;
			break;
		}
		Vector3 baseVelocity = Vector3.ClampMagnitude(info.impactForce * d, 5f);
		if (BloodParticle.BLOOD_PARTICLE_SETTING != BloodParticle.BloodParticleType.None)
		{
			for (int i = 0; i < num4; i++)
			{
				DecalManager.CreateBloodDrop(info.point, baseVelocity, this.team);
			}
		}
		int num5 = Mathf.Clamp(num4, 1, 2);
		for (int j = 0; j < num5; j++)
		{
			DecalManager.EmitBloodEffect(info.point, baseVelocity, this.team);
		}
		bool flag5 = info.isSplashDamage && !this.dead && !this.fallenOver && num2 > 200f;
		if (this.health <= 0f)
		{
			this.Die(info, false);
		}
		else if (this.ragdoll.IsRagdoll())
		{
			this.ApplyRigidbodyForce(info.impactForce);
		}
		else if (this.balance < 0f)
		{
			this.KnockOver(Vector3.up * 100f + info.impactForce);
		}
		else
		{
			this.Hurt(UnityEngine.Random.Range(-2f, 2f));
		}
		if (this.ragdoll.IsRagdoll() && flag5)
		{
			Vector3 b = UnityEngine.Random.insideUnitSphere.ToGround() * 5f;
			Vector3 a = this.Position();
			Rigidbody[] rigidbodies = this.ragdoll.rigidbodies;
			for (int k = 0; k < rigidbodies.Length; k++)
			{
				rigidbodies[k].AddForceAtPosition(new Vector3(0f, 2f, 0f), a + b, ForceMode.VelocityChange);
			}
		}
		if (!this.aiControlled)
		{
			IngameUi.instance.SetHealth(Mathf.Max(0f, this.health));
			float intensity = Mathf.Clamp01(0.3f + (1f - this.health / this.maxHealth));
			IngameUi.instance.ShowVignette(intensity, 6f);
		}
		else if (num2 > 20f)
		{
			this.parachuteDeployStunAction.Start();
		}
		return true;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x000036DB File Offset: 0x000018DB
	public void OnBalanceSetManually()
	{
		if (this.balance < 0f)
		{
			this.balance = 0f;
			this.FallOver();
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x000036FB File Offset: 0x000018FB
	private void StartObstacleClimbingAction()
	{
		this.climbObstacleAction.Start();
		this.climbObstacleCooldownAction.Start();
		this.PlayClimbObstacleAnimation();
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00003719 File Offset: 0x00001919
	private void PlayClimbObstacleAnimation()
	{
		if (this.climbObstacleAnimationCooldown.TrueDone())
		{
			this.climbObstacleAnimationCooldown.Start();
			this.climbObstacleAnimationAction.Start();
			this.animator.SetTrigger(Actor.ANIM_PAR_CLIMB_OBSTACLE);
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000374E File Offset: 0x0000194E
	public void SetHealth(float health)
	{
		if (this.dead)
		{
			return;
		}
		this.health = health;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00003760 File Offset: 0x00001960
	public virtual void ApplyRecoil(Vector3 impulse)
	{
		this.controller.ApplyRecoil(impulse);
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00047EB0 File Offset: 0x000460B0
	public Vector3 WeaponMuzzlePosition()
	{
		Vector3 result;
		try
		{
			result = this.activeWeapon.CurrentMuzzle().position;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
			result = Vector3.zero;
		}
		return result;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000376E File Offset: 0x0000196E
	public void ForceApplyAnimatorForTime(float time)
	{
		base.StartCoroutine(this.ForceApplyAnimatorCoroutine(time));
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000377E File Offset: 0x0000197E
	private IEnumerator ForceApplyAnimatorCoroutine(float time)
	{
		AnimatorCullingMode cullMode = this.animator.cullingMode;
		this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		this.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
		yield return new WaitForSecondsRealtime(time);
		this.animator.cullingMode = cullMode;
		this.animator.updateMode = AnimatorUpdateMode.Normal;
		yield break;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00047EF0 File Offset: 0x000460F0
	private void Unholster(Weapon weapon)
	{
		weapon.gameObject.SetActive(true);
		this.activeWeapon = weapon;
		this.activeWeapon.Unholster();
		this.controller.SwitchedToWeapon(this.activeWeapon);
		if (!this.isRendered)
		{
			this.activeWeapon.Hide();
		}
		this.animator.SetFloat(Actor.ANIM_PAR_POSE_TYPE, (float)this.activeWeapon.configuration.pose);
		this.animator.SetTrigger(Actor.ANIM_PAR_UNHOLSTER);
		if (this.aiControlled)
		{
			if (this.distanceCull)
			{
				this.activeWeapon.Hide();
				return;
			}
		}
		else
		{
			IngameUi.instance.SetWeapon(weapon);
			this.UpdateAmmoUi();
			this.UpdateScopeUi();
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00003794 File Offset: 0x00001994
	private void HolsterActiveWeapon()
	{
		if (this.activeWeapon == null)
		{
			return;
		}
		this.activeWeapon.Holster();
		this.activeWeapon = null;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00047FA4 File Offset: 0x000461A4
	public void DropWeapon(int slot)
	{
		Weapon weapon = this.weapons[slot];
		if (weapon == null)
		{
			return;
		}
		weapon.transform.parent = null;
		if (!this.aiControlled)
		{
			this.weaponImposterRenderers.Remove(weapon);
		}
		weapon.Drop();
		if (weapon == this.activeWeapon)
		{
			this.activeWeapon = null;
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000037B7 File Offset: 0x000019B7
	public bool HasWeaponInSlot(int i)
	{
		return this.weapons[i] != null;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x000037C7 File Offset: 0x000019C7
	public bool HasUnholsteredWeapon()
	{
		return this.activeWeapon != null;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x000037D5 File Offset: 0x000019D5
	public void Highlight(float time = 4f)
	{
		this.highlightAction.StartLifetime(time);
		if (this.IsSeated())
		{
			this.seat.vehicle.Highlight(time);
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x000037FC File Offset: 0x000019FC
	public bool IsHighlighted()
	{
		return this.cachedIsHighlighted;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00003804 File Offset: 0x00001A04
	public void MarkHighPriorityTarget(float duration)
	{
		this.isHighPriotityTargetAction.StartLifetime(duration);
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00003812 File Offset: 0x00001A12
	public bool IsHighPriorityTarget()
	{
		return !this.isHighPriotityTargetAction.TrueDone();
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00003822 File Offset: 0x00001A22
	public bool IsInBurningVehicle()
	{
		return this.seat != null && this.seat.vehicle.burning;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00003844 File Offset: 0x00001A44
	public bool IsSeated()
	{
		return this.seat != null;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00003852 File Offset: 0x00001A52
	public bool IsPassenger()
	{
		return this.IsSeated() && !this.seat.IsDriverSeat();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000386C File Offset: 0x00001A6C
	public bool IsDriver()
	{
		return this.IsSeated() && this.seat.IsDriverSeat();
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00048000 File Offset: 0x00046200
	public bool CanEnterSeat()
	{
		return !this.dead && !this.IsOnLadder() && !this.IsSeated() && this.cannotEnterVehicleAction.TrueDone() && (!this.fallenOver || !this.justFellNaturallyAction.TrueDone() || this.IsSwimming());
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00048054 File Offset: 0x00046254
	public bool EnterVehicle(Vehicle vehicle)
	{
		if (vehicle.isLocked)
		{
			return false;
		}
		if (this.IsSeated())
		{
			if (this.seat.vehicle == vehicle)
			{
				return true;
			}
			this.LeaveSeat(false);
		}
		if (!this.CanEnterSeat())
		{
			return false;
		}
		foreach (Seat seat in vehicle.seats)
		{
			if (!seat.IsOccupied())
			{
				this.EnterSeat(seat, false);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x000480F0 File Offset: 0x000462F0
	public bool EnterSeat(Seat seat, bool kickOutOccupant)
	{
		if (seat == null || !seat.gameObject.activeInHierarchy || seat.vehicle.isLocked || (seat.vehicle.ownerTeam >= 0 && seat.vehicle.ownerTeam != this.team))
		{
			return false;
		}
		this.CancelClimbObstacle();
		if (this.fallenOver)
		{
			this.InstantGetUp();
		}
		if (this.parachuteDeployed)
		{
			this.CutParachutes();
		}
		if (seat.vehicle.dead)
		{
			return false;
		}
		Actor actor = null;
		if (seat.IsOccupied())
		{
			if (!kickOutOccupant || !seat.vehicle.allowPlayerSeatSwap)
			{
				return false;
			}
			actor = seat.occupant;
			actor.LeaveSeat(false);
		}
		this.animator.SetInteger(Actor.ANIM_PAR_SEATED_TYPE, (int)seat.animation);
		seat.SetOccupant(this);
		base.transform.parent = seat.transform;
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = Quaternion.identity;
		this.seat = seat;
		this.controller.StartSeated(seat);
		this.rigidbody.interpolation = RigidbodyInterpolation.None;
		if (!seat.CanUsePersonalWeapons() && this.activeWeapon != null)
		{
			this.HolsterActiveWeapon();
		}
		else if (seat.CanUsePersonalWeapons() && this.activeWeapon == null)
		{
			this.SwitchToFirstAvailableWeapon();
		}
		if (seat.HasAnyMountedWeapons())
		{
			this.Unholster(seat.weapons[0]);
		}
		if (seat.HasHandIkTargets())
		{
			this.ik.handTargetL = seat.handTargetL;
			this.ik.handTargetR = seat.handTargetR;
		}
		this.ik.SetHandIkEnabled(seat.handTargetL != null, seat.handTargetR != null);
		Collider[] array = this.hitboxColliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.layer = 16;
		}
		array = this.ragdollColliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.layer = 16;
		}
		this.ForceStance(Actor.Stance.Stand);
		if (actor != null && !actor.EnterVehicle(seat.vehicle))
		{
			AiActorController aiActorController = actor.controller as AiActorController;
			if (aiActorController != null)
			{
				aiActorController.CreateRougeSquad();
			}
		}
		return true;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0004832C File Offset: 0x0004652C
	public void LeaveSeat(bool forcedByFallingOver)
	{
		this.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		Vector3 forward = this.seat.transform.forward;
		Vehicle vehicle = this.seat.vehicle;
		if (this.seat.HasAnyMountedWeapons() && this.activeWeapon == this.seat.activeWeapon)
		{
			this.HolsterActiveWeapon();
		}
		Seat seat = this.seat;
		this.seat.OccupantLeft();
		this.seat = null;
		this.ForceStance(Actor.Stance.Stand);
		base.transform.parent = this.originalParent;
		base.transform.localScale = Vector3.one;
		if (this.originalParent != null)
		{
			base.transform.localRotation = Quaternion.identity;
		}
		Vector3 raycastExitPosition = seat.GetRaycastExitPosition();
		Quaternion quaternion = Quaternion.LookRotation(forward.ToGround());
		this.controller.EndSeated(seat, raycastExitPosition, quaternion, forcedByFallingOver);
		this.movementPosition = raycastExitPosition;
		this.rigidbody.position = raycastExitPosition;
		this.rigidbody.rotation = quaternion;
		this.ik.SetHandIkEnabled(false, false);
		if (this.activeWeapon == null)
		{
			if (this.activeWeaponSlot < this.weapons.Length && this.weapons[this.activeWeaponSlot] != null)
			{
				this.SwitchWeapon(this.activeWeaponSlot);
			}
			else
			{
				this.SwitchToFirstAvailableWeapon();
			}
		}
		this.cannotEnterVehicleAction.Start();
		base.StartCoroutine(this.ReactivateCollisionsWith(!seat.enclosed, vehicle));
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00003883 File Offset: 0x00001A83
	private IEnumerator ReactivateCollisionsWith(bool forceEnableRagdollCollisionInstantly, Vehicle vehicle)
	{
		bool enableRagdollCollisionInstantly = forceEnableRagdollCollisionInstantly || vehicle.Velocity().sqrMagnitude < 1f;
		if (enableRagdollCollisionInstantly)
		{
			Collider[] array = this.ragdollColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.layer = 10;
			}
		}
		yield return new WaitForSeconds(0.5f);
		bool flag = this.IsSeated() && this.seat.vehicle == vehicle;
		if (vehicle != null && !flag)
		{
			Collider[] array = this.hitboxColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.layer = 8;
			}
			if (!enableRagdollCollisionInstantly)
			{
				array = this.ragdollColliders;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].gameObject.layer = 10;
				}
			}
		}
		yield break;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x000038A0 File Offset: 0x00001AA0
	private bool CanUnholsterWeapon()
	{
		return !this.dead && !this.fallenOver && !this.IsOnLadder() && (!this.IsSeated() || this.seat.CanUsePersonalWeapons()) && !this.immersedInWater;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x000484A4 File Offset: 0x000466A4
	public void InstantlyReloadCarriedWeapons()
	{
		foreach (Weapon weapon in this.weapons)
		{
			if (weapon != null)
			{
				weapon.InstantlyReload();
			}
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000484DC File Offset: 0x000466DC
	public void NextWeapon()
	{
		if (this.IsSeated() && this.seat.weapons.Length != 0)
		{
			for (int i = 1; i <= this.seat.weapons.Length - 1; i++)
			{
				int num = (this.seat.ActiveWeaponSlot() + i) % this.seat.weapons.Length;
				if (!this.seat.weapons[num].IsToggleable())
				{
					this.SwitchWeapon(num);
					return;
				}
			}
			return;
		}
		if (this.CanUnholsterWeapon())
		{
			for (int j = 1; j <= 4; j++)
			{
				int num2 = (this.activeWeaponSlot + j) % 5;
				if (this.weapons[num2] != null && !this.weapons[num2].IsToggleable())
				{
					this.SwitchWeapon(num2);
					return;
				}
			}
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00048598 File Offset: 0x00046798
	public void PreviousWeapon()
	{
		if (this.IsSeated() && this.seat.weapons.Length != 0)
		{
			for (int i = 1; i <= this.seat.weapons.Length - 1; i++)
			{
				int num = (this.seat.ActiveWeaponSlot() - i + this.seat.weapons.Length) % this.seat.weapons.Length;
				if (!this.seat.weapons[num].IsToggleable())
				{
					this.SwitchWeapon(num);
					return;
				}
			}
			return;
		}
		if (this.CanUnholsterWeapon())
		{
			for (int j = 1; j <= 4; j++)
			{
				int num2 = (this.activeWeaponSlot - j + 5) % 5;
				if (this.weapons[num2] != null && !this.weapons[num2].IsToggleable())
				{
					this.SwitchWeapon(num2);
					return;
				}
			}
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00048664 File Offset: 0x00046864
	public void SwitchWeapon(int slot)
	{
		if (!this.IsSeated() || this.seat.weapons.Length <= slot)
		{
			if (this.CanUnholsterWeapon())
			{
				Weapon weapon = this.weapons[slot];
				if (weapon != null)
				{
					if (weapon.IsToggleable())
					{
						((ToggleableItem)weapon).Toggle(false);
						return;
					}
					if (weapon != this.activeWeapon)
					{
						if (this.activeWeapon != null)
						{
							this.HolsterActiveWeapon();
						}
						this.activeWeaponSlot = slot;
						this.Unholster(weapon);
						return;
					}
					this.SwitchFireMode();
				}
			}
			return;
		}
		Weapon weapon2 = this.seat.weapons[slot];
		if (weapon2.IsToggleable())
		{
			((ToggleableItem)weapon2).Toggle(false);
			return;
		}
		if (weapon2 != this.activeWeapon)
		{
			if (this.activeWeapon != null)
			{
				this.HolsterActiveWeapon();
			}
			this.Unholster(weapon2);
			return;
		}
		this.SwitchFireMode();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00048744 File Offset: 0x00046944
	public bool SwitchSeat(int seatIndex, bool swapIfOccupied)
	{
		if (this.IsSeated())
		{
			Vehicle vehicle = this.seat.vehicle;
			if (!vehicle.allowPlayerSeatChange)
			{
				return false;
			}
			if (!vehicle.allowPlayerSeatSwap)
			{
				swapIfOccupied = false;
			}
			if (seatIndex >= 0 && seatIndex < vehicle.seats.Count)
			{
				Seat seat = vehicle.seats[seatIndex];
				if (seat == this.seat)
				{
					return true;
				}
				bool flag = seat.IsOccupied();
				if (swapIfOccupied || !flag)
				{
					Seat seat2 = this.seat;
					this.LeaveSeatForSwap();
					if (swapIfOccupied && flag)
					{
						Actor occupant = seat.occupant;
						if (occupant != null)
						{
							occupant.LeaveSeatForSwap();
							occupant.EnterSeat(seat2, false);
						}
					}
					this.EnterSeat(seat, false);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000487FC File Offset: 0x000469FC
	private void LeaveSeatForSwap()
	{
		if (this.seat.HasAnyMountedWeapons() && this.activeWeapon == this.seat.activeWeapon)
		{
			this.HolsterActiveWeapon();
		}
		Seat leftSeat = this.seat;
		this.seat.OccupantLeft();
		this.seat = null;
		this.controller.EndSeatedSwap(leftSeat);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x000038DA File Offset: 0x00001ADA
	public void Hurt(float x)
	{
		if (this.fallenOver || this.dead)
		{
			return;
		}
		this.animator.SetFloat(Actor.ANIM_PAR_HURT_X, x);
		this.animator.SetTrigger(Actor.ANIM_PAR_HURT);
		this.hurtAction.Start();
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00003919 File Offset: 0x00001B19
	public void OnJoinPlayerSquad()
	{
		this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00003927 File Offset: 0x00001B27
	public void OnDroppedFromSquad()
	{
		this.animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00003935 File Offset: 0x00001B35
	public void SetTeam(int team)
	{
		this.team = team;
		ActorManager.instance.OnSetTeam(this);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00003949 File Offset: 0x00001B49
	private bool ControllingVehicle()
	{
		return this.IsSeated() && !this.seat.CanUsePersonalWeapons();
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0004885C File Offset: 0x00046A5C
	public void UpdateAmmoUi()
	{
		if (this.activeWeapon != null)
		{
			Weapon activeSubWeapon = this.activeWeapon.GetActiveSubWeapon();
			IngameUi.instance.SetAmmoText(activeSubWeapon.ammo, activeSubWeapon.spareAmmo);
		}
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00003963 File Offset: 0x00001B63
	public void UpdateHealthUi()
	{
		IngameUi.instance.SetHealth(this.health);
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0004889C File Offset: 0x00046A9C
	public void UpdateScopeUi()
	{
		Weapon weapon = this.activeWeapon.GetActiveSubWeapon();
		if (weapon.HasParentWeapon() && weapon.useParentWeaponSightModes)
		{
			weapon = weapon.parentWeapon;
		}
		if (weapon.configuration.sightModes != null && weapon.configuration.sightModes.Length != 0)
		{
			IngameUi.instance.SetSightText(weapon.configuration.sightModes[weapon.activeSightModeIndex].name);
			return;
		}
		IngameUi.instance.SetSightText("");
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0004891C File Offset: 0x00046B1C
	public void AmmoChanged()
	{
		if (this.activeWeapon != null && this.activeWeapon.AllowsResupply() && this.activeWeapon.spareAmmo <= this.activeWeapon.configuration.spareAmmo / 2)
		{
			this.needsResupply = true;
		}
		if (!this.aiControlled)
		{
			this.UpdateAmmoUi();
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00003975 File Offset: 0x00001B75
	public void MarkAtResupplyCrate()
	{
		if (this.dead)
		{
			return;
		}
		this.justResuppliedAtCrateAction.Start();
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00048978 File Offset: 0x00046B78
	public bool ResupplyAmmo()
	{
		if (this.dead)
		{
			return false;
		}
		this.justResuppliedAmmoAction.Start();
		bool flag = false;
		this.needsResupply = false;
		for (int i = 0; i < 5; i++)
		{
			Weapon weapon = this.weapons[i];
			if (!(weapon == null))
			{
				flag |= weapon.Resupply();
				if (weapon.spareAmmo <= weapon.configuration.spareAmmo / 2)
				{
					this.needsResupply = true;
				}
			}
		}
		if (flag)
		{
			this.AmmoChanged();
			if (!this.aiControlled)
			{
				IngameUi.instance.Resupply();
			}
		}
		return flag;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00048A04 File Offset: 0x00046C04
	public bool ResupplyHealth()
	{
		if (this.dead)
		{
			return false;
		}
		this.justResuppliedHealthAction.Start();
		float num = this.health;
		this.health = Mathf.Min(this.health + 30f, this.maxHealth);
		if (!this.aiControlled && num != this.health)
		{
			this.UpdateHealthUi();
			IngameUi.instance.Heal();
		}
		return num != this.health;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000398B File Offset: 0x00001B8B
	public bool CanCapturePoint()
	{
		return !this.IsSeated() || this.seat.vehicle.canCapturePoints;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00048A78 File Offset: 0x00046C78
	public void Freeze()
	{
		this.isFrozen = true;
		if (this.activeWeapon != null && this.wasFiring)
		{
			this.activeWeapon.StopFire();
			this.wasFiring = false;
			this.animator.speed = 0f;
		}
	}

	// Token: 0x06000225 RID: 549 RVA: 0x000039A7 File Offset: 0x00001BA7
	public void Unfreeze()
	{
		this.isFrozen = false;
		this.animator.speed = 1f;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000039C0 File Offset: 0x00001BC0
	public bool HitboxCollidersAreEnabled()
	{
		return this.hitboxColliders[0].enabled;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00048AC4 File Offset: 0x00046CC4
	public bool DisableHitboxColliders()
	{
		bool result = this.HitboxCollidersAreEnabled();
		this.ragdoll.autoDisableColliders = false;
		Collider[] array = this.hitboxColliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		return result;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00048B04 File Offset: 0x00046D04
	public void EnableHitboxColliders()
	{
		this.ragdoll.autoDisableColliders = true;
		if (this.ragdoll.state == ActiveRaggy.State.Animate)
		{
			Collider[] array = this.hitboxColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x000039CF File Offset: 0x00001BCF
	public void Deactivate()
	{
		this.Hide();
		this.Freeze();
		this.DisableHitboxColliders();
		this.isDeactivated = true;
		this.dead = true;
		ActorManager.SetDead(this);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x000039F8 File Offset: 0x00001BF8
	public void Activate()
	{
		this.Show();
		this.Unfreeze();
		this.EnableHitboxColliders();
		this.isDeactivated = false;
		this.dead = false;
		ActorManager.SetAlive(this);
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00048B48 File Offset: 0x00046D48
	public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
	{
		if (!this.aiControlled)
		{
			FpsActorController.instance.SetPositionAndRotation(position, rotation);
			return;
		}
		Collider[] array = this.hitboxColliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		base.transform.position = position;
		base.transform.rotation = rotation;
		this.movementPosition = position;
		this.lastSoldierHeight = position.y;
		array = this.hitboxColliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00048BD4 File Offset: 0x00046DD4
	public void Hide()
	{
		this.skinnedRenderer.enabled = false;
		this.skinnedRendererRagdoll.enabled = false;
		this.parachuteRenderer.enabled = false;
		this.ragdoll.controlRenderers = false;
		this.isRendered = false;
		if (this.activeWeapon != null)
		{
			this.activeWeapon.Hide();
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00048C34 File Offset: 0x00046E34
	public void Show()
	{
		bool flag = this.ragdoll.state > ActiveRaggy.State.Animate;
		this.skinnedRenderer.enabled = !flag;
		this.skinnedRendererRagdoll.enabled = true;
		this.parachuteRenderer.enabled = this.parachuteDeployed;
		this.ragdoll.controlRenderers = true;
		this.isRendered = true;
		if (this.activeWeapon != null)
		{
			this.activeWeapon.Show();
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00048CA8 File Offset: 0x00046EA8
	private void UpdateDistanceCull()
	{
		if (!this.isRendered)
		{
			return;
		}
		this.distanceCull = this.ShouldDistanceCull();
		if (!this.aiControlled)
		{
			return;
		}
		if (this.activeWeapon != null)
		{
			if (this.distanceCull && !this.activeWeapon.isHidden)
			{
				this.activeWeapon.Hide();
				return;
			}
			if (!this.distanceCull && this.activeWeapon.isHidden)
			{
				this.activeWeapon.Show();
			}
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00048D24 File Offset: 0x00046F24
	private bool ShouldDistanceCull()
	{
		if (!this.aiControlled || FpsActorController.instance.attractMode || !FpsActorController.instance.allowPlayerControlledTpCamera)
		{
			return false;
		}
		if (GameManager.IsSpectating() || FpsActorController.instance.inPhotoMode)
		{
			return false;
		}
		float z = FpsActorController.instance.activeCameraWorldToLocalMatrix.MultiplyPoint(this.cachedPosition).z;
		return z > FpsActorController.instance.lqDistance || z < -5f;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00003A20 File Offset: 0x00001C20
	public virtual Actor.TargetType GetTargetType()
	{
		return this.cachedTargetType;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00003A28 File Offset: 0x00001C28
	private Actor.TargetType GenerateCachedTargetType()
	{
		if (this.IsSeated())
		{
			return this.seat.vehicle.targetType;
		}
		if (this.controller.IsGroupedUp())
		{
			return Actor.TargetType.InfantryGroup;
		}
		return Actor.TargetType.Infantry;
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00003A53 File Offset: 0x00001C53
	public virtual bool CanBeDamagedBy(Weapon weapon)
	{
		return weapon.projectileArmorRating >= this.cachedArmorRating;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00003A66 File Offset: 0x00001C66
	public void SetModelSkin(ActorSkin skin)
	{
		ActorManager.ApplyOverrideActorSkin(this, skin, this.team);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00003A75 File Offset: 0x00001C75
	public void AddAccessory(ActorAccessory accessory)
	{
		this.accessoryRenderer.Add(this.CreateAccessoryObject(accessory, this.skinnedRenderer));
		this.accessoryRagdollRenderer.Add(this.CreateAccessoryObject(accessory, this.skinnedRendererRagdoll));
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00048D9C File Offset: 0x00046F9C
	public void RemoveAccessories()
	{
		foreach (Renderer obj in this.accessoryRenderer)
		{
			UnityEngine.Object.Destroy(obj);
		}
		foreach (Renderer obj2 in this.accessoryRagdollRenderer)
		{
			UnityEngine.Object.Destroy(obj2);
		}
		this.accessoryRenderer.Clear();
		this.accessoryRagdollRenderer.Clear();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00048E44 File Offset: 0x00047044
	public void SetImposterShadowMode(ShadowCastingMode mode)
	{
		if (this.weaponImposterRenderers == null)
		{
			return;
		}
		foreach (Renderer[] array in this.weaponImposterRenderers.Values)
		{
			foreach (Renderer renderer in array)
			{
				if (renderer != null)
				{
					renderer.shadowCastingMode = mode;
				}
			}
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x00048EC0 File Offset: 0x000470C0
	public void SetImposterRenderingEnabled(bool enabled)
	{
		if (this.weaponImposterRenderers == null)
		{
			return;
		}
		foreach (Renderer[] array in this.weaponImposterRenderers.Values)
		{
			foreach (Renderer renderer in array)
			{
				if (renderer != null)
				{
					renderer.enabled = enabled;
				}
			}
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00048F3C File Offset: 0x0004713C
	private Renderer CreateAccessoryObject(ActorAccessory accessory, SkinnedMeshRenderer siblingRenderer)
	{
		SkinnedMeshRenderer component = UnityEngine.Object.Instantiate<GameObject>(siblingRenderer.gameObject, siblingRenderer.transform.parent).GetComponent<SkinnedMeshRenderer>();
		component.sharedMesh = accessory.mesh;
		component.materials = accessory.materials;
		component.transform.parent = siblingRenderer.transform.parent;
		component.rootBone = siblingRenderer.rootBone;
		return component;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x00003AA7 File Offset: 0x00001CA7
	public void GetOnLadder(Ladder ladder)
	{
		this.GetOnLadderAtHeight(ladder, ladder.GetProjectedHeightAtPosition(this.Position()));
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00003ABC File Offset: 0x00001CBC
	public void GetOnLadderAtHeight(Ladder ladder, float height)
	{
		this.ladder = ladder;
		this.ladderHeight = height;
		this.controller.StartLadder(ladder);
		if (this.parachuteDeployed)
		{
			this.CutParachutes();
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00003AE6 File Offset: 0x00001CE6
	public bool CanExitLadderAtBottom()
	{
		return this.ladderHeight < (this.aiControlled ? 0.5f : 1f);
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00003B04 File Offset: 0x00001D04
	public bool CanExitLadderAtTop()
	{
		return this.ladderHeight > this.ladder.height - (this.aiControlled ? 0.5f : 1f);
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00048FA0 File Offset: 0x000471A0
	public void ExitLadder()
	{
		Vector3 vector = this.ladder.GetPositionAtHeight(this.ladderHeight) - this.ladder.transform.forward * 1f;
		if (this.CanExitLadderAtBottom())
		{
			vector = this.ladder.GetBottomExitPosition();
			this.justExitedLadderAction.Start();
		}
		else if (this.CanExitLadderAtTop())
		{
			vector = this.ladder.GetTopExitPosition();
			this.justExitedLadderAction.Start();
		}
		Vector3 forward = this.ladder.transform.forward.ToGround();
		this.movementPosition = vector;
		this.controller.EndLadder(vector, Quaternion.LookRotation(forward));
		this.fallStartHeight = vector.y;
		this.ladder = null;
		this.cannotEnterVehicleAction.Start();
		this.smoothLadderClimbInput = 0f;
		this.animator.SetFloat(Actor.ANIM_PAR_LADDER_Y, 0f);
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00003B2E File Offset: 0x00001D2E
	public float GetTargetSize()
	{
		if (this.IsSeated())
		{
			return this.seat.vehicle.targetSize;
		}
		return 0f;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00003B4E File Offset: 0x00001D4E
	public bool IsOnLadder()
	{
		return this.ladder != null;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00003B5C File Offset: 0x00001D5C
	private bool HasScoreboardEntry()
	{
		return this.scoreboardEntry != null;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00003B6A File Offset: 0x00001D6A
	public AiActorController.FocusType GetFocusType()
	{
		if (!this.IsSeated())
		{
			return AiActorController.FocusType.Infantry;
		}
		if (this.seat.vehicle.isHuge)
		{
			return AiActorController.FocusType.HugeVehicle;
		}
		return AiActorController.FocusType.Vehicle;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0004908C File Offset: 0x0004728C
	private void PlayFootstep(bool onTerrain)
	{
		if (!this.muteFootsteps && !this.IsSwimming())
		{
			FootstepAudio.FootstepType footstepType = FootstepAudio.FootstepType.Terrain;
			if (this.feetAreInWater)
			{
				footstepType = FootstepAudio.FootstepType.Water;
			}
			else if (!onTerrain)
			{
				footstepType = FootstepAudio.FootstepType.Indoor;
			}
			FootstepAudio.Play(this.cachedPosition, footstepType, this.stance == Actor.Stance.Prone, ActorManager.ActorCanSeePlayer(this));
			if (footstepType == FootstepAudio.FootstepType.Water)
			{
				Vector3 position = this.Position();
				position.y = this.waterHeight;
				GameManager.CreateSplashEffect(false, position);
			}
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00003B8B File Offset: 0x00001D8B
	public bool HeroArmorIgnoresDamage()
	{
		return this.hasHeroArmor && !this.heroArmorIgnoreDamageAction.TrueDone();
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00003BA5 File Offset: 0x00001DA5
	public bool GetIsResupplyingAmmo()
	{
		return !this.justResuppliedAmmoAction.TrueDone();
	}

	// Token: 0x06000245 RID: 581 RVA: 0x00003BB5 File Offset: 0x00001DB5
	public bool GetIsResupplyingHealth()
	{
		return !this.justResuppliedHealthAction.TrueDone();
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00003BC5 File Offset: 0x00001DC5
	public bool GetIsAtResupplyCrate()
	{
		return !this.justResuppliedAtCrateAction.TrueDone();
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000490F8 File Offset: 0x000472F8
	public CapturePoint GetCurrentCapturePoint()
	{
		return ActorManager.instance.actorData[this.actorIndex].GetCurrentCapturePoint();
	}

	// Token: 0x04000147 RID: 327
	private const float FOOTSTEP_PHASE_PER_SPEED = 0.2f;

	// Token: 0x04000148 RID: 328
	private const float HERO_ARMOR_MAX_DAMAGE = 40f;

	// Token: 0x04000149 RID: 329
	private const float HERO_ARMOR_MAX_BALANCE_DAMAGE = 45f;

	// Token: 0x0400014A RID: 330
	private const float HERO_ARMOR_IGNORE_PROJECTILES_TIME = 1f;

	// Token: 0x0400014B RID: 331
	private const float HERO_ARMOR_ACCUMULATED_DAMAGE_DRAIN_RATE = 1f;

	// Token: 0x0400014C RID: 332
	public const int PROJECT_TO_GROUND_MASK = 2232321;

	// Token: 0x0400014D RID: 333
	public const float PARACHUTE_FALL_SPEED = 13f;

	// Token: 0x0400014E RID: 334
	public const float PARACHUTE_MOVE_SPEED = 7f;

	// Token: 0x0400014F RID: 335
	public const float PARACHUTE_ACCELERATION = 10f;

	// Token: 0x04000150 RID: 336
	public const int ACTOR_COLLISION_LAYER = 17;

	// Token: 0x04000151 RID: 337
	private const float MOVEMENT_LERP_SPEED = 5f;

	// Token: 0x04000152 RID: 338
	private const float TURN_MOVEMENT_FORWARD_SLERP = 2f;

	// Token: 0x04000153 RID: 339
	private const float CLAMP_FACING_ANGLE = 50f;

	// Token: 0x04000154 RID: 340
	private const float CLAMP_FACING_ANGLE_CROUCH_ALERTED = 20f;

	// Token: 0x04000155 RID: 341
	private const float CLAMP_FACING_ANGLE_PRONE = 10f;

	// Token: 0x04000156 RID: 342
	private const float OFFSET_RETURN_SPEED = 2f;

	// Token: 0x04000157 RID: 343
	private const float GETUP_HEIGHT_PADDING = 0.5f;

	// Token: 0x04000158 RID: 344
	private const float RAGDOLL_GETUP_SPEED_THRESHOLD = 2f;

	// Token: 0x04000159 RID: 345
	private const float RAGDOLL_TOSS_BALANCE_DAMAGE_THRESHOLD = 200f;

	// Token: 0x0400015A RID: 346
	private const float RAGDOLL_TOSS_FORCE_MAGNITUDE = 2f;

	// Token: 0x0400015B RID: 347
	private const float HEIGHT_OFFSET_RETURN_SPEED = 5f;

	// Token: 0x0400015C RID: 348
	private const float PROJECT_TO_GROUND_THRESHOLD = 1.5f;

	// Token: 0x0400015D RID: 349
	public const float PROJECT_TO_GROUND_SPHERECAST_RADIUS = 0.3f;

	// Token: 0x0400015E RID: 350
	private const float AUTO_MOVE_FALL_PROJECT_DISTANCE = 4f;

	// Token: 0x0400015F RID: 351
	private const float RAGDOLL_DRIVE_SPRING = 700f;

	// Token: 0x04000160 RID: 352
	private const float RAGDOLL_DRIVE_DAMPING = 3f;

	// Token: 0x04000161 RID: 353
	private const float RAGDOLL_DEAD_DRIVE_SPRING = 50f;

	// Token: 0x04000162 RID: 354
	private const float RAGDOLL_DEAD_DRIVE_DAMPING = 1f;

	// Token: 0x04000163 RID: 355
	private const float RAGDOLL_IN_WATER_SPEED_THRESHOLD = 7f;

	// Token: 0x04000164 RID: 356
	private const float STOP_RAGDOLL_IN_WATER_SPEED_THRESHOLD = 1f;

	// Token: 0x04000165 RID: 357
	private const float SPLASH_IN_WATER_SPEED_THRESHOLD = 4f;

	// Token: 0x04000166 RID: 358
	private const float BALANCE_GAIN_PER_SECOND = 10f;

	// Token: 0x04000167 RID: 359
	private const int AIM_ANIMATION_LAYER = 1;

	// Token: 0x04000168 RID: 360
	private const int RAGDOLL_ANIMATION_LAYER = 2;

	// Token: 0x04000169 RID: 361
	private const float RAGDOLL_SWIM_MOVE_ACCELERATION = 15f;

	// Token: 0x0400016A RID: 362
	private const float RAGDOLL_SWIM_FLOAT_ANTI_GRAVITY_MULTIPLIER = 3f;

	// Token: 0x0400016B RID: 363
	private const float LADDER_SPEED = 3f;

	// Token: 0x0400016C RID: 364
	private const float FALL_DAMAGE_DAMAGE_THRESHOLD = 7f;

	// Token: 0x0400016D RID: 365
	private const float FALL_DAMAGE_LETHAL_THRESHOLD = 30f;

	// Token: 0x0400016E RID: 366
	private const int MAX_BLOOD_DROPS_PER_DAMAGE = 16;

	// Token: 0x0400016F RID: 367
	private const float BONUS_SPRINT_SPEED_CHARGE_TIME = 25f;

	// Token: 0x04000170 RID: 368
	private const float BONUS_SPRINT_SPEED_CHARGE_TIME_SPAWNED = 3f;

	// Token: 0x04000171 RID: 369
	private const float DEFAULT_MAX_HEALTH = 100f;

	// Token: 0x04000172 RID: 370
	private const float DEFAULT_MAX_BALANCE = 100f;

	// Token: 0x04000173 RID: 371
	private const float MAKE_PROXIMITY_NOISE_MOVEMENT_SPEED = 2f;

	// Token: 0x04000174 RID: 372
	public const float DEFAULT_HIGHLIGHT_TIME = 4f;

	// Token: 0x04000175 RID: 373
	private static readonly int ANIM_PAR_RESET = Animator.StringToHash("reset");

	// Token: 0x04000176 RID: 374
	private static readonly int ANIM_PAR_RAGDOLLED = Animator.StringToHash("ragdolled");

	// Token: 0x04000177 RID: 375
	private static readonly int ANIM_PAR_FALLING = Animator.StringToHash("falling");

	// Token: 0x04000178 RID: 376
	private static readonly int ANIM_PAR_ON_BACK = Animator.StringToHash("onBack");

	// Token: 0x04000179 RID: 377
	private static readonly int ANIM_PAR_MOVING = Animator.StringToHash("moving");

	// Token: 0x0400017A RID: 378
	private static readonly int ANIM_PAR_MOVEMENT_X = Animator.StringToHash("movement x");

	// Token: 0x0400017B RID: 379
	private static readonly int ANIM_PAR_MOVEMENT_Y = Animator.StringToHash("movement y");

	// Token: 0x0400017C RID: 380
	private static readonly int ANIM_PAR_DEAD = Animator.StringToHash("dead");

	// Token: 0x0400017D RID: 381
	private static readonly int ANIM_PAR_SEATED = Animator.StringToHash("seated");

	// Token: 0x0400017E RID: 382
	private static readonly int ANIM_PAR_LEAN = Animator.StringToHash("lean");

	// Token: 0x0400017F RID: 383
	private static readonly int ANIM_PAR_HAIL = Animator.StringToHash("hail");

	// Token: 0x04000180 RID: 384
	private static readonly int ANIM_PAR_REGROUP = Animator.StringToHash("regroup");

	// Token: 0x04000181 RID: 385
	private static readonly int ANIM_PAR_MOVE = Animator.StringToHash("move");

	// Token: 0x04000182 RID: 386
	private static readonly int ANIM_PAR_HALT = Animator.StringToHash("halt");

	// Token: 0x04000183 RID: 387
	private static readonly int ANIM_PAR_HURT = Animator.StringToHash("hurt");

	// Token: 0x04000184 RID: 388
	private static readonly int ANIM_PAR_HURT_X = Animator.StringToHash("hurt x");

	// Token: 0x04000185 RID: 389
	private static readonly int ANIM_PAR_CROUCHED = Animator.StringToHash("crouched");

	// Token: 0x04000186 RID: 390
	private static readonly int ANIM_PAR_PRONE = Animator.StringToHash("prone");

	// Token: 0x04000187 RID: 391
	private static readonly int ANIM_PAR_SWIM = Animator.StringToHash("swim");

	// Token: 0x04000188 RID: 392
	private static readonly int ANIM_PAR_SWIM_FORWARD = Animator.StringToHash("swim forward");

	// Token: 0x04000189 RID: 393
	private static readonly int ANIM_PAR_SPRINTING = Animator.StringToHash("sprinting");

	// Token: 0x0400018A RID: 394
	private static readonly int ANIM_PAR_SEATED_TYPE = Animator.StringToHash("seated type");

	// Token: 0x0400018B RID: 395
	private static readonly int ANIM_PAR_POSE_TYPE = Animator.StringToHash("pose type");

	// Token: 0x0400018C RID: 396
	private static readonly int ANIM_PAR_IDLE = Animator.StringToHash("idle");

	// Token: 0x0400018D RID: 397
	private static readonly int ANIM_PAR_MELEE = Animator.StringToHash("melee");

	// Token: 0x0400018E RID: 398
	private static readonly int ANIM_PAR_RELOADING = Animator.StringToHash("reloading");

	// Token: 0x0400018F RID: 399
	private static readonly int ANIM_PAR_SEATED_DIRECTION = Animator.StringToHash("seated direction");

	// Token: 0x04000190 RID: 400
	private static readonly int ANIM_PAR_CLIMBING = Animator.StringToHash("climbing");

	// Token: 0x04000191 RID: 401
	private static readonly int ANIM_PAR_LADDER_Y = Animator.StringToHash("ladder y");

	// Token: 0x04000192 RID: 402
	private static readonly int ANIM_PAR_PARACHUTE = Animator.StringToHash("parachute");

	// Token: 0x04000193 RID: 403
	private static readonly int ANIM_PAR_CLIMB_OBSTACLE = Animator.StringToHash("climb obstacle");

	// Token: 0x04000194 RID: 404
	private static readonly int ANIM_PAR_UNHOLSTER = Animator.StringToHash("unholster");

	// Token: 0x04000195 RID: 405
	public ActorController controller;

	// Token: 0x04000196 RID: 406
	public ActiveRaggy ragdoll;

	// Token: 0x04000197 RID: 407
	public Animator animator;

	// Token: 0x04000198 RID: 408
	public bool autoMoveActor;

	// Token: 0x04000199 RID: 409
	public Rigidbody hipRigidbody;

	// Token: 0x0400019A RID: 410
	public Rigidbody headRigidbody;

	// Token: 0x0400019B RID: 411
	public Transform parachuteParent;

	// Token: 0x0400019C RID: 412
	public Animation parachuteAnimation;

	// Token: 0x0400019D RID: 413
	public MeshRenderer parachuteRenderer;

	// Token: 0x0400019E RID: 414
	[NonSerialized]
	public float closestEnemyDistancePreShift = 9999f;

	// Token: 0x0400019F RID: 415
	[NonSerialized]
	public float closestEnemyDistance = 9999f;

	// Token: 0x040001A0 RID: 416
	[NonSerialized]
	public int actorIndex;

	// Token: 0x040001A1 RID: 417
	[NonSerialized]
	public int teamActorIndex;

	// Token: 0x040001A2 RID: 418
	private List<Renderer> accessoryRenderer;

	// Token: 0x040001A3 RID: 419
	private List<Renderer> accessoryRagdollRenderer;

	// Token: 0x040001A4 RID: 420
	[NonSerialized]
	public float deathTimestamp;

	// Token: 0x040001A5 RID: 421
	[NonSerialized]
	public float health = 100f;

	// Token: 0x040001A6 RID: 422
	[NonSerialized]
	public float maxHealth = 100f;

	// Token: 0x040001A7 RID: 423
	[NonSerialized]
	public float balance = 100f;

	// Token: 0x040001A8 RID: 424
	[NonSerialized]
	public float maxBalance = 100f;

	// Token: 0x040001A9 RID: 425
	[NonSerialized]
	public bool dead;

	// Token: 0x040001AA RID: 426
	[NonSerialized]
	public bool fallenOver;

	// Token: 0x040001AB RID: 427
	[NonSerialized]
	public bool makesProximityMovementNoise;

	// Token: 0x040001AC RID: 428
	private bool getupActionWasStarted;

	// Token: 0x040001AD RID: 429
	private Collider[] hitboxColliders;

	// Token: 0x040001AE RID: 430
	private Collider[] ragdollColliders;

	// Token: 0x040001AF RID: 431
	private ActorIk ik;

	// Token: 0x040001B0 RID: 432
	private Vector2 movement;

	// Token: 0x040001B1 RID: 433
	private Vector3 parentOffset = Vector3.zero;

	// Token: 0x040001B2 RID: 434
	private Transform originalParent;

	// Token: 0x040001B3 RID: 435
	private Transform animatedParentObject;

	// Token: 0x040001B4 RID: 436
	private TimedAction fallAction = new TimedAction(2f, false);

	// Token: 0x040001B5 RID: 437
	private TimedAction stopFallAction = new TimedAction(0.3f, false);

	// Token: 0x040001B6 RID: 438
	private TimedAction getupAction = new TimedAction(2f, false);

	// Token: 0x040001B7 RID: 439
	private TimedAction preventWaterRagdollAction = new TimedAction(1f, false);

	// Token: 0x040001B8 RID: 440
	private TimedAction highlightAction = new TimedAction(4f, false);

	// Token: 0x040001B9 RID: 441
	private TimedAction isHighPriotityTargetAction = new TimedAction(10f, false);

	// Token: 0x040001BA RID: 442
	private TimedAction hurtAction = new TimedAction(0.6f, false);

	// Token: 0x040001BB RID: 443
	private TimedAction justExitedLadderAction = new TimedAction(0.5f, false);

	// Token: 0x040001BC RID: 444
	[NonSerialized]
	public bool immersedInWater;

	// Token: 0x040001BD RID: 445
	[NonSerialized]
	public bool feetAreInWater;

	// Token: 0x040001BE RID: 446
	[NonSerialized]
	public float waterDepth;

	// Token: 0x040001BF RID: 447
	[NonSerialized]
	public float waterHeight;

	// Token: 0x040001C0 RID: 448
	private TimedAction swimmingAction = new TimedAction(0.15f, false);

	// Token: 0x040001C1 RID: 449
	private float aimAnimationLayerWeight;

	// Token: 0x040001C2 RID: 450
	[NonSerialized]
	public bool parachuteDeployed;

	// Token: 0x040001C3 RID: 451
	[NonSerialized]
	public bool canDeployParachute = true;

	// Token: 0x040001C4 RID: 452
	private TimedAction parachuteDeployAction = new TimedAction(1f, false);

	// Token: 0x040001C5 RID: 453
	private TimedAction parachuteDeployStunAction = new TimedAction(4f, false);

	// Token: 0x040001C6 RID: 454
	private Vector3 parachuteVelocity = Vector3.zero;

	// Token: 0x040001C7 RID: 455
	private TimedAction canUseParachuteAction = new TimedAction(1f, false);

	// Token: 0x040001C8 RID: 456
	private TimedAction splashCooldownAction = new TimedAction(1f, false);

	// Token: 0x040001C9 RID: 457
	private TimedAction recentlyInWaterAction = new TimedAction(1f, false);

	// Token: 0x040001CA RID: 458
	private TimedAction climbObstacleAction = new TimedAction(0.6f, false);

	// Token: 0x040001CB RID: 459
	private TimedAction climbObstacleCooldownAction = new TimedAction(0.8f, false);

	// Token: 0x040001CC RID: 460
	private TimedAction climbObstacleAnimationAction = new TimedAction(0.9f, false);

	// Token: 0x040001CD RID: 461
	private TimedAction climbObstacleAnimationCooldown = new TimedAction(0.7f, false);

	// Token: 0x040001CE RID: 462
	private TimedAction justChangedProneStanceAction = new TimedAction(0.5f, false);

	// Token: 0x040001CF RID: 463
	private bool isFallingAsRagdoll;

	// Token: 0x040001D0 RID: 464
	private float fallStartHeight;

	// Token: 0x040001D1 RID: 465
	private Vector3 climbObstacleStartPosition;

	// Token: 0x040001D2 RID: 466
	private Vector3 climbObstacleFlatDelta;

	// Token: 0x040001D3 RID: 467
	private float climbObstacleDeltaHeight;

	// Token: 0x040001D4 RID: 468
	private bool isMantlingWall;

	// Token: 0x040001D5 RID: 469
	[NonSerialized]
	public Actor.Stance stance;

	// Token: 0x040001D6 RID: 470
	private Actor.Stance wantedStance;

	// Token: 0x040001D7 RID: 471
	[NonSerialized]
	public bool moving;

	// Token: 0x040001D8 RID: 472
	private TimedAction bonusSprintSpeedAction = new TimedAction(10f, false);

	// Token: 0x040001D9 RID: 473
	private TimedAction justFellNaturallyAction = new TimedAction(2f, false);

	// Token: 0x040001DA RID: 474
	[NonSerialized]
	public bool isFrozen;

	// Token: 0x040001DB RID: 475
	[NonSerialized]
	public bool isRendered;

	// Token: 0x040001DC RID: 476
	[NonSerialized]
	public bool isDeactivated;

	// Token: 0x040001DD RID: 477
	[NonSerialized]
	public bool isIgnored;

	// Token: 0x040001DE RID: 478
	[NonSerialized]
	public float visibilityDistanceModifier;

	// Token: 0x040001DF RID: 479
	private TimedAction justResuppliedAmmoAction = new TimedAction(3.5f, false);

	// Token: 0x040001E0 RID: 480
	private TimedAction justResuppliedHealthAction = new TimedAction(3.5f, false);

	// Token: 0x040001E1 RID: 481
	private TimedAction justResuppliedAtCrateAction = new TimedAction(3.5f, false);

	// Token: 0x040001E2 RID: 482
	private TimedAction disableForceSleepAction = new TimedAction(5f, false);

	// Token: 0x040001E3 RID: 483
	[NonSerialized]
	public Ladder ladder;

	// Token: 0x040001E4 RID: 484
	[NonSerialized]
	public float ladderHeight;

	// Token: 0x040001E5 RID: 485
	private float smoothLadderClimbInput;

	// Token: 0x040001E6 RID: 486
	private Actor.TargetType cachedTargetType;

	// Token: 0x040001E7 RID: 487
	private Vector3 cachedPosition = Vector3.zero;

	// Token: 0x040001E8 RID: 488
	private Vector3 cachedVelocity = Vector3.zero;

	// Token: 0x040001E9 RID: 489
	private Vehicle.ArmorRating cachedArmorRating;

	// Token: 0x040001EA RID: 490
	private bool cachedIsHighlighted;

	// Token: 0x040001EB RID: 491
	[NonSerialized]
	public Weapon activeWeapon;

	// Token: 0x040001EC RID: 492
	[NonSerialized]
	public Weapon[] weapons = new Weapon[5];

	// Token: 0x040001ED RID: 493
	private int activeWeaponSlot;

	// Token: 0x040001EE RID: 494
	private bool wasFiring;

	// Token: 0x040001EF RID: 495
	private bool aiming;

	// Token: 0x040001F0 RID: 496
	private TimedAction aimingAction = new TimedAction(0.2f, false);

	// Token: 0x040001F1 RID: 497
	[NonSerialized]
	public bool hasAmmoBox;

	// Token: 0x040001F2 RID: 498
	[NonSerialized]
	public int ammoBoxSlot;

	// Token: 0x040001F3 RID: 499
	[NonSerialized]
	public bool hasMedipack;

	// Token: 0x040001F4 RID: 500
	[NonSerialized]
	public int medipackSlot;

	// Token: 0x040001F5 RID: 501
	[NonSerialized]
	public bool hasRepairTool;

	// Token: 0x040001F6 RID: 502
	[NonSerialized]
	public int repairToolSlot;

	// Token: 0x040001F7 RID: 503
	[NonSerialized]
	public bool hasSmokeScreen;

	// Token: 0x040001F8 RID: 504
	[NonSerialized]
	public int smokeScreenSlot;

	// Token: 0x040001F9 RID: 505
	[NonSerialized]
	public bool aiControlled;

	// Token: 0x040001FA RID: 506
	[NonSerialized]
	public bool needsResupply;

	// Token: 0x040001FB RID: 507
	[NonSerialized]
	public Seat seat;

	// Token: 0x040001FC RID: 508
	private TimedAction cannotEnterVehicleAction = new TimedAction(1f, false);

	// Token: 0x040001FD RID: 509
	[NonSerialized]
	public SkinnedMeshRenderer skinnedRenderer;

	// Token: 0x040001FE RID: 510
	[NonSerialized]
	public SkinnedMeshRenderer skinnedRendererRagdoll;

	// Token: 0x040001FF RID: 511
	public Dictionary<Weapon, Renderer[]> weaponImposterRenderers;

	// Token: 0x04000200 RID: 512
	[NonSerialized]
	public bool distanceCull;

	// Token: 0x04000201 RID: 513
	[NonSerialized]
	public bool dropsAmmoOnKick = true;

	// Token: 0x04000202 RID: 514
	[NonSerialized]
	public bool hasSpawnedAmmoReserve;

	// Token: 0x04000203 RID: 515
	[NonSerialized]
	public bool isInvulnerable;

	// Token: 0x04000204 RID: 516
	[NonSerialized]
	public bool isScheduledToSpawn;

	// Token: 0x04000205 RID: 517
	[NonSerialized]
	public Rigidbody rigidbody;

	// Token: 0x04000206 RID: 518
	[NonSerialized]
	public new string name = "Actor";

	// Token: 0x04000207 RID: 519
	[NonSerialized]
	public ScoreboardActorEntry scoreboardEntry;

	// Token: 0x04000208 RID: 520
	private bool footstepPhaseUnderHalf = true;

	// Token: 0x04000209 RID: 521
	private float footstepPhase;

	// Token: 0x0400020A RID: 522
	[NonSerialized]
	public bool muteFootsteps;

	// Token: 0x0400020B RID: 523
	public float speedMultiplier = 1f;

	// Token: 0x0400020C RID: 524
	[NonSerialized]
	public bool hasHeroArmor;

	// Token: 0x0400020D RID: 525
	private TimedAction heroArmorIgnoreDamageAction = new TimedAction(1f, false);

	// Token: 0x0400020E RID: 526
	private float accumulatedHeroArmorDamage;

	// Token: 0x0400020F RID: 527
	private float lastSoldierHeight;

	// Token: 0x04000210 RID: 528
	private float animatedSoldierHeightOffset;

	// Token: 0x04000211 RID: 529
	private Vector3 movementPosition = Vector3.zero;

	// Token: 0x04000212 RID: 530
	[NonSerialized]
	public Transform[] animatedBones;

	// Token: 0x04000213 RID: 531
	[NonSerialized]
	public Transform[] ragdollBones;

	// Token: 0x04000214 RID: 532
	[Consumable("Consuming this event stops the actor from taking damage.")]
	[Doc("Invoked when an actor takes damage.")]
	public ScriptEvent<Actor, Actor, DamageInfo> onTakeDamage = new ScriptEvent<Actor, Actor, DamageInfo>();

	// Token: 0x04000215 RID: 533
	private const float PARACHUTE_RAYCAST_OFFSET = 2f;

	// Token: 0x04000216 RID: 534
	private static Vector3 removePitchEuler = new Vector3(0f, 1f, 1f);

	// Token: 0x02000061 RID: 97
	public enum TargetType
	{
		// Token: 0x04000218 RID: 536
		Infantry,
		// Token: 0x04000219 RID: 537
		InfantryGroup,
		// Token: 0x0400021A RID: 538
		Unarmored,
		// Token: 0x0400021B RID: 539
		Armored,
		// Token: 0x0400021C RID: 540
		Air,
		// Token: 0x0400021D RID: 541
		AirFastMover
	}

	// Token: 0x02000062 RID: 98
	public enum Stance
	{
		// Token: 0x0400021F RID: 543
		Stand,
		// Token: 0x04000220 RID: 544
		Crouch,
		// Token: 0x04000221 RID: 545
		Prone
	}
}
