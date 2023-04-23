using System;
using System.Collections.Generic;
using Lua;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000349 RID: 841
public class Vehicle : MonoBehaviour
{
	// Token: 0x06001537 RID: 5431 RVA: 0x00010E7A File Offset: 0x0000F07A
	public float GetAvoidanceCoarseRadius()
	{
		return this.avoidanceSize.magnitude;
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x00010E87 File Offset: 0x0000F087
	public void Highlight(float time)
	{
		this.highlightAction.StartLifetime(time);
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x00010E95 File Offset: 0x0000F095
	public bool IsHighlighted()
	{
		return !this.highlightAction.TrueDone();
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x00010EA5 File Offset: 0x0000F0A5
	public bool HasPlayerDriver()
	{
		return !this.Driver().aiControlled;
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x00010EB5 File Offset: 0x0000F0B5
	public bool HasDriver()
	{
		return this.seats[0].IsOccupied();
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x00010EC8 File Offset: 0x0000F0C8
	public Actor Driver()
	{
		return this.seats[0].occupant;
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x00010EDB File Offset: 0x0000F0DB
	public void MarkTakingFire()
	{
		this.takingFireAction.Start();
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0009A3E4 File Offset: 0x000985E4
	public Vector3 GetWorldCenterOfMass()
	{
		return this.rigidbody.transform.localToWorldMatrix.MultiplyPoint(this.originalCenterOfMass);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x0009A410 File Offset: 0x00098610
	protected virtual void Awake()
	{
		if (this.targetLockPoint == null)
		{
			this.targetLockPoint = base.transform;
		}
		this.isHuge = (this.avoidanceSize.x * this.avoidanceSize.y > 200f);
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.engine.SetAudioSource(base.GetComponent<AudioSource>());
		this.engine.SetWindZone(base.GetComponent<WindZone>());
		this.engine.Reset();
		if (this.rigidbody != null)
		{
			this.originalCenterOfMass = this.rigidbody.centerOfMass;
			this.rigidbody.maxDepenetrationVelocity = 10f;
		}
		this.countermeasuresCooldownAction = new TimedAction(this.countermeasuresCooldown, false);
		this.countermeasuresActiveAction = new TimedAction(this.countermeasuresActiveTime, false);
		if (this.interiorAudioSource != null)
		{
			this.interiorAudioSource.outputAudioMixerGroup = GameManager.instance.playerVehicleInteriorMixerGroup;
			this.interiorAudioSourceBaseVolume = this.interiorAudioSource.volume;
		}
		if (this.animator != null)
		{
			int num = Animator.StringToHash("velocity x");
			int num2 = Animator.StringToHash("input w");
			foreach (AnimatorControllerParameter animatorControllerParameter in this.animator.parameters)
			{
				if (animatorControllerParameter.nameHash == num && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					this.notifyAnimatorVelocity = true;
				}
				if (animatorControllerParameter.nameHash == num2 && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					this.notifyAnimatorInput = true;
				}
			}
		}
		if (this.rigidbody != null)
		{
			this.rigidbody.gameObject.AddComponent<RigidbodyFailsafe>();
		}
		this.health = this.maxHealth;
		this.colliders = base.GetComponentsInChildren<Collider>();
		if (this.HasBlockSensor())
		{
			this.blockSensorOrigin = this.blockSensor.transform.localPosition;
		}
		this.cannotRamAction.Start();
		if (GameManager.IsIngame())
		{
			this.squadOrderPoint = OrderManager.CreateOrderPoint(base.transform, SquadOrderPoint.ObjectiveType.Enter, OrderManager.instance.pointOrderVehicleTexture);
			this.squadOrderPoint.targetText = this.name;
		}
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0009A628 File Offset: 0x00098828
	protected virtual void Start()
	{
		try
		{
			this.RegisterVehicle();
			this.UpdateTeamColor();
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0009A65C File Offset: 0x0009885C
	private void RegisterVehicle()
	{
		try
		{
			ActorManager.RegisterVehicle(this);
		}
		catch (Exception)
		{
			Debug.LogError("Vehicle " + base.gameObject.name + " could not be registered with ActorManager");
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0009A6A4 File Offset: 0x000988A4
	public void CommandeerBySquad(Squad squad)
	{
		Squad squad2 = this.claimingSquad;
		if (squad2 != null)
		{
			squad2.DropSquadVehicleClaim();
		}
		this.SquadClaim(squad);
		if (squad2 != null && squad2.aiMembers[0].modifier.canJoinPlayerSquad)
		{
			squad2.JoinSquad(squad);
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x0009A6EC File Offset: 0x000988EC
	public void SquadClaim(Squad squad)
	{
		this.claimedBySquad = true;
		this.claimingSquad = squad;
		if (squad.team == GameManager.PlayerTeam())
		{
			this.squadOrderPoint.type = (this.claimingSquad.HasPlayerLeader() ? SquadOrderPoint.ObjectiveType.Exit : SquadOrderPoint.ObjectiveType.Commandeer);
			return;
		}
		this.squadOrderPoint.enabled = false;
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x00010EE8 File Offset: 0x0000F0E8
	public void KickOutSquad()
	{
		if (this.claimedBySquad && !this.claimingSquad.HasPlayerLeader())
		{
			this.claimingSquad.ExitVehicle();
		}
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x00010F0A File Offset: 0x0000F10A
	public void DropSquadClaim()
	{
		this.claimedBySquad = false;
		this.claimingSquad = null;
		this.squadOrderPoint.enabled = true;
		this.squadOrderPoint.type = SquadOrderPoint.ObjectiveType.Enter;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x00010F32 File Offset: 0x0000F132
	public bool IsUnclaimed()
	{
		return !this.claimedBySquad && !this.claimedByPlayer;
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x00010F47 File Offset: 0x0000F147
	public void AddTrackingMissile(TargetSeekingMissile missile)
	{
		this.trackingMissiles.Add(missile);
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x00010F56 File Offset: 0x0000F156
	public void DropTrackingMissile(TargetSeekingMissile missile)
	{
		this.trackingMissiles.Remove(missile);
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x00010F65 File Offset: 0x0000F165
	public bool IsBeingTrackedByMissile()
	{
		return this.trackingMissiles.Count > 0;
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x0009A740 File Offset: 0x00098940
	private void CheckRam()
	{
		Vector3 vector = this.rigidbody.velocity * Time.fixedDeltaTime;
		int num = Physics.BoxCastNonAlloc(this.cachedLocalToWorldMatrix.MultiplyPoint(this.ramOffset), this.ramSize, vector.normalized, Vehicle.ramResults, base.transform.rotation, vector.magnitude, 256);
		for (int i = 0; i < num; i++)
		{
			RaycastHit raycastHit = Vehicle.ramResults[i];
			raycastHit.collider.GetComponent<Hitbox>().VehicleHit(this, raycastHit.point);
		}
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0009A7D8 File Offset: 0x000989D8
	protected virtual void Update()
	{
		if (this.canStopSmokeInWater.TrueDone())
		{
			if (this.smokeParticles != null && WaterLevel.IsInWater(this.smokeParticles.transform.position))
			{
				this.smokeParticles.Stop();
			}
			if (this.fireParticles != null && WaterLevel.IsInWater(this.fireParticles.transform.position))
			{
				this.fireParticles.Stop();
			}
		}
		if (this.dead)
		{
			return;
		}
		if (this.interiorAudioSource != null)
		{
			this.interiorAudioSource.volume = this.engine.audio.volume * this.interiorAudioSourceBaseVolume;
		}
		this.engine.Update();
		if (!this.dead)
		{
			AudioMixerGroup soundOutputGroupVehicle = GameManager.GetSoundOutputGroupVehicle(this.playerDistance, this.playerIsInside, this.canSeePlayer);
			if (this.engine.audio != null)
			{
				this.engine.audio.outputAudioMixerGroup = soundOutputGroupVehicle;
			}
			if (this.engine.throttleAudioSource != null)
			{
				this.engine.throttleAudioSource.outputAudioMixerGroup = soundOutputGroupVehicle;
			}
		}
		if (this.notifyAnimatorVelocity)
		{
			Vector3 vector = this.LocalVelocity();
			this.animator.SetFloat("velocity x", vector.x);
			this.animator.SetFloat("velocity y", vector.y);
			this.animator.SetFloat("velocity z", vector.z);
		}
		if (this.notifyAnimatorInput)
		{
			this.animator.SetFloat("input x", this.driverInput.x);
			this.animator.SetFloat("input y", this.driverInput.y);
			this.animator.SetFloat("input z", this.driverInput.z);
			this.animator.SetFloat("input w", this.driverInput.w);
		}
		if (this.hasCountermeasures && this.CountermeasuresAreReady() && this.HasDriver() && this.Driver().controller.Countermeasures())
		{
			this.PopCountermeasures();
		}
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x00010F75 File Offset: 0x0000F175
	public bool CountermeasuresAreReady()
	{
		return this.countermeasuresCooldownAction.TrueDone();
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x0009A9F4 File Offset: 0x00098BF4
	private void PopCountermeasures()
	{
		this.countermeasuresCooldownAction.Start();
		this.isBeingLockedAction.Stop();
		if (this.countermeasureParticles != null)
		{
			this.countermeasureParticles.Play();
		}
		if (this.countermeasureAudio != null)
		{
			this.countermeasureAudio.Play();
		}
		if (this.countermeasureSpawnPrefab != null)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.countermeasureSpawnPrefab, base.transform.position, base.transform.rotation);
		}
		this.countermeasuresActiveAction.Start();
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x00010F82 File Offset: 0x0000F182
	public bool CountermeasuresAreActive()
	{
		return this.hasCountermeasures && !this.countermeasuresActiveAction.TrueDone();
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x0009AA84 File Offset: 0x00098C84
	private bool PositionIsNan()
	{
		return float.IsInfinity(base.transform.position.x) || float.IsNaN(base.transform.position.x) || float.IsInfinity(base.transform.position.y) || float.IsNaN(base.transform.position.y) || float.IsInfinity(base.transform.position.z) || float.IsNaN(base.transform.position.z);
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x0009AB1C File Offset: 0x00098D1C
	private void ResetToSafePosition()
	{
		string str = "Resetting vehicle ";
		string str2 = this.name;
		string str3 = " to safe position at ";
		Vector3 vector = this.safeResetPosition;
		Debug.Log(str + str2 + str3 + vector.ToString());
		List<Collider> list = new List<Collider>(base.GetComponentsInChildren<Collider>());
		if (base.GetComponent<Collider>() != null)
		{
			list.Add(base.GetComponent<Collider>());
		}
		List<Collider> list2 = new List<Collider>();
		foreach (Collider collider in list)
		{
			if (collider.enabled)
			{
				collider.enabled = false;
				list2.Add(collider);
			}
		}
		base.transform.position = this.safeResetPosition;
		foreach (Collider collider2 in list2)
		{
			collider2.enabled = true;
		}
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.isKinematic = true;
		this.Die(DamageInfo.Default);
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x0009AC5C File Offset: 0x00098E5C
	protected virtual void FixedUpdate()
	{
		if (this.PositionIsNan())
		{
			this.ResetToSafePosition();
		}
		else
		{
			this.safeResetPosition = base.transform.position;
		}
		if (this.dead)
		{
			return;
		}
		if (this.rigidbody != null)
		{
			if (this.rigidbody.velocity.magnitude < 3f)
			{
				this.cannotRamAction.Start();
			}
			if (this.cannotRamAction.TrueDone())
			{
				this.CheckRam();
			}
		}
		if (this.burning && !this.dead)
		{
			this.burnTime -= Time.deltaTime;
			if (this.burnTime < 0f)
			{
				this.Die(this.burningDamageSource);
			}
		}
		this.cachedLocalToWorldMatrix = base.transform.localToWorldMatrix;
		this.cachedWorldToLocalMatrix = this.cachedLocalToWorldMatrix.inverse;
		if (this.rigidbody != null)
		{
			Vector3 velocity = this.rigidbody.velocity;
			this.acceleration = (velocity - this.cachedVelocity) / Time.fixedDeltaTime;
			this.cachedVelocity = velocity;
			this.cachedLocalVelocity = this.cachedWorldToLocalMatrix.MultiplyVector(this.cachedVelocity);
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x0009AD8C File Offset: 0x00098F8C
	public void OccupantEntered(Seat seat)
	{
		if (seat == this.seats[0])
		{
			this.OnDriverEntered();
		}
		if (this.ownerTeam != seat.occupant.team)
		{
			this.ownerTeam = seat.occupant.team;
			this.UpdateTeamColor();
		}
		if (!seat.occupant.aiControlled)
		{
			Squad playerSquad = FpsActorController.instance.playerSquad;
			if (seat == this.seats[0] && this.claimedBySquad && this.claimingSquad != playerSquad)
			{
				this.claimingSquad.JoinSquad(playerSquad);
				this.SquadClaim(playerSquad);
			}
			this.claimedByPlayer = true;
			this.playerIsInside = true;
			this.ReserveSeat();
			this.OnPlayerEntered();
			if (this.burning && this.fireAlarmSound != null)
			{
				this.fireAlarmSound.Play();
			}
			if (this.interiorAudioSource != null)
			{
				this.interiorAudioSource.priority = 1;
				this.interiorAudioSource.Play();
			}
			this.engine.PrioritizeAudioSource();
		}
		base.CancelInvoke("AutoDamage");
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x0009AEA8 File Offset: 0x000990A8
	public void OccupantLeft(Seat seat, Actor leaver)
	{
		if (seat == this.seats[0])
		{
			this.DriverExited();
		}
		if (!leaver.aiControlled)
		{
			this.playerIsInside = false;
			this.DropSeatReservation();
			this.OnPlayerExited();
			if (this.fireAlarmSound != null)
			{
				this.fireAlarmSound.Stop();
			}
			if (this.interiorAudioSource != null)
			{
				this.interiorAudioSource.Stop();
			}
			this.engine.ResetAudioSourcePriority();
		}
		if (this.IsEmpty())
		{
			if (!this.isTurret && this.applyAutoDamage)
			{
				base.InvokeRepeating("AutoDamage", 50f, 2f);
			}
			this.ownerTeam = -1;
			this.UpdateTeamColor();
		}
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x0009AF64 File Offset: 0x00099164
	protected virtual void OnPlayerEntered()
	{
		foreach (Seat seat in this.seats)
		{
			bool flag = seat.IsOccupied() && !seat.occupant.aiControlled;
			foreach (MountedWeapon mountedWeapon in seat.weapons)
			{
				if (flag)
				{
					mountedWeapon.AssignFpVehicleAudioMix();
				}
				else
				{
					mountedWeapon.AssignPlayerVehicleAudioMix();
				}
			}
		}
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x0009AFFC File Offset: 0x000991FC
	protected virtual void OnPlayerExited()
	{
		foreach (Seat seat in this.seats)
		{
			MountedWeapon[] weapons = seat.weapons;
			for (int i = 0; i < weapons.Length; i++)
			{
				weapons[i].ResetAudioMix();
			}
		}
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x0009B064 File Offset: 0x00099264
	private void UpdateTeamColor()
	{
		try
		{
			Color color = Color.gray;
			if (this.ownerTeam > -1)
			{
				color = ColorScheme.TeamColor(this.ownerTeam);
			}
			MaterialTarget[] array = this.teamColorMaterials;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Get().color = color;
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x00010F9C File Offset: 0x0000F19C
	private void AutoDamage()
	{
		this.Damage(DamageInfo.DefaultDamage(this.maxHealth * 0.07f));
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x0009B0C8 File Offset: 0x000992C8
	protected virtual void OnDriverEntered()
	{
		if (!this.reportedFirstDriver)
		{
			if (this.spawner != null)
			{
				this.spawner.FirstDriverEntered(this);
			}
			ActorManager.SetVehicleTaken(this);
			this.reportedFirstDriver = true;
		}
		this.engine.enabled = true;
		this.engine.PlayIgnitionSound();
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x00010FB5 File Offset: 0x0000F1B5
	protected virtual void DriverExited()
	{
		this.engine.enabled = false;
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x0009B11C File Offset: 0x0009931C
	public void Damage(DamageInfo info)
	{
		if (this.isInvulnerable)
		{
			return;
		}
		if (!this.dead)
		{
			IngameUi.OnDamageDealt(info, new HitInfo(this));
		}
		this.health = Mathf.Clamp(this.health - info.healthDamage, 0f, this.maxHealth);
		if (info.sourceActor != null)
		{
			this.lastDamageSource = info.sourceActor;
			this.lastDamageSourceAppliesAction.Start();
			if (info.healthDamage > 400f)
			{
				info.sourceActor.MarkHighPriorityTarget(10f);
			}
		}
		if (info.healthDamage > 900f)
		{
			this.HeavyDamage();
		}
		foreach (Seat seat in this.seats)
		{
			if (seat.IsOccupied())
			{
				seat.occupant.controller.OnVehicleWasDamaged(info.sourceActor, info.healthDamage);
			}
		}
		if (this.health <= 0f && !this.dead && !this.burning)
		{
			if (this.burnTime <= 0f)
			{
				this.Die(info);
			}
			else
			{
				this.StartBurning(info);
			}
		}
		if (this.health < 0.5f * this.maxHealth && this.smokeParticles != null)
		{
			this.smokeParticles.Play();
		}
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x0009B288 File Offset: 0x00099488
	protected virtual void StartBurning(DamageInfo info)
	{
		RavenscriptManager.events.onVehicleDisabled.Invoke(this, info);
		this.burning = true;
		this.burningDamageSource = info;
		this.stopBurningRepairs = 3;
		if (this.lastDamageSource != null && !this.lastDamageSourceAppliesAction.TrueDone())
		{
			this.lastDamageSource.OnGotVehicleKill(true, this);
		}
		if (this.fireParticles != null)
		{
			this.fireParticles.Play();
		}
		if (this.fireAlarmSound != null && this.playerIsInside)
		{
			this.fireAlarmSound.Play();
		}
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x0009B320 File Offset: 0x00099520
	private void StopBurning()
	{
		RavenscriptManager.events.onVehicleExtinguished.Invoke(this);
		this.burning = false;
		this.burningDamageSource = default(DamageInfo);
		if (this.fireParticles != null)
		{
			this.fireParticles.Stop();
		}
		if (this.fireAlarmSound != null)
		{
			this.fireAlarmSound.Stop();
		}
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x0009B384 File Offset: 0x00099584
	public bool Repair(float amount)
	{
		if (this.dead && !this.canBeRepairedAfterDeath)
		{
			return false;
		}
		if (this.burning)
		{
			this.stopBurningRepairs--;
			if (this.stopBurningRepairs == 0)
			{
				this.StopBurning();
			}
		}
		bool result = this.health < this.maxHealth;
		this.health = Mathf.Min(this.health + amount, this.maxHealth);
		if (this.health >= 0.5f * this.maxHealth)
		{
			if (this.smokeParticles != null)
			{
				this.smokeParticles.Stop();
			}
			if (this.dead)
			{
				this.Ressurect();
			}
		}
		if (this.IsEmpty())
		{
			base.CancelInvoke("AutoDamage");
			if (!this.isTurret && this.applyAutoDamage)
			{
				base.InvokeRepeating("AutoDamage", 50f, 2f);
			}
		}
		return result;
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x0009B464 File Offset: 0x00099664
	protected virtual void Ressurect()
	{
		this.dead = false;
		if (this.burning)
		{
			this.StopBurning();
		}
		foreach (Seat seat in this.seats)
		{
			seat.gameObject.SetActive(true);
		}
		GameObject[] array = this.disableOnDeath;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
		array = this.activateOnDeath;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		this.squadOrderPoint.enabled = true;
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x0009B518 File Offset: 0x00099718
	public virtual void Die(DamageInfo info)
	{
		Vehicle.DelOnDestroyed delOnDestroyed = this.onDestroyed;
		if (delOnDestroyed != null)
		{
			delOnDestroyed();
		}
		RavenscriptManager.events.onVehicleDestroyed.Invoke(this, info);
		if (info.sourceActor != null)
		{
			info.sourceActor.OnGotVehicleKill(false, this);
		}
		this.dead = true;
		if (this.fireAlarmSound != null)
		{
			this.fireAlarmSound.Stop();
		}
		if (this.spawner != null)
		{
			this.spawner.VehicleDied(this);
		}
		ActorManager.DropVehicle(this);
		foreach (Seat seat in this.seats)
		{
			if (seat.IsOccupied())
			{
				Actor occupant = seat.occupant;
				occupant.LeaveSeat(false);
				if (seat.enclosed)
				{
					occupant.Kill(info);
				}
				else
				{
					DamageInfo info2 = new DamageInfo(info)
					{
						balanceDamage = 200f,
						isPiercing = true,
						point = base.transform.position,
						impactForce = new Vector3(0f, 10f, 0f)
					};
					occupant.Damage(info2);
				}
			}
			seat.gameObject.SetActive(false);
		}
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
					component.velocity = this.rigidbody.velocity;
					component.AddTorque(UnityEngine.Random.insideUnitSphere * 3f, ForceMode.VelocityChange);
				}
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
		if (this.rigidbody != null)
		{
			this.rigidbody.WakeUp();
		}
		if (!this.isTurret)
		{
			base.Invoke("Cleanup", 24f);
		}
		this.squadOrderPoint.enabled = false;
		base.Invoke("Explode", 0.3f);
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x0009B774 File Offset: 0x00099974
	private void OnCollisionEnter(Collision c)
	{
		if (this.PositionIsNan())
		{
			this.ResetToSafePosition();
		}
		float num = Mathf.Abs(Vector3.Dot(c.relativeVelocity, c.contacts[0].normal));
		if (num > this.crashDamageSpeedThrehshold && c.collider.gameObject.layer != 8 && c.collider.gameObject.layer != 10)
		{
			float amount = (num - this.crashDamageSpeedThrehshold) * this.crashDamageMultiplier;
			this.Damage(DamageInfo.DefaultDamage(amount));
			if (this.impactAudio != null)
			{
				this.PlayImpactSound(c.contacts[0].point);
			}
			if (this.burning && this.crashSkipsBurn && !this.dead)
			{
				this.Die(this.burningDamageSource);
			}
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x00010FC3 File Offset: 0x0000F1C3
	protected virtual void PlayImpactSound(Vector3 position)
	{
		this.impactAudio.transform.position = position;
		this.impactAudio.pitch *= UnityEngine.Random.Range(0.9f, 1.1f);
		this.impactAudio.Play();
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x0009B84C File Offset: 0x00099A4C
	protected virtual void Explode()
	{
		if (this.rigidbody != null)
		{
			this.rigidbody.WakeUp();
			this.rigidbody.AddForce((UnityEngine.Random.insideUnitSphere + Vector3.up) * 2000f, ForceMode.Impulse);
			this.rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 500f, ForceMode.Impulse);
		}
		if (this.deathParticles != null)
		{
			this.deathParticles.Play();
		}
		this.canStopSmokeInWater.Start();
		this.engine.Reset();
		if (this.deathSound != null)
		{
			GameManager.UpdateSoundOutputGroupCombat(this.deathSound, this.playerDistance, this.canSeePlayer);
			this.deathSound.Play();
		}
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x0009B914 File Offset: 0x00099B14
	private void Cleanup()
	{
		GameObject[] array = this.activateOnDeath;
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x00011002 File Offset: 0x0000F202
	public virtual Vector3 Velocity()
	{
		return this.cachedVelocity;
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x0001100A File Offset: 0x0000F20A
	public Vector3 LocalVelocity()
	{
		return this.cachedLocalVelocity;
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x00011012 File Offset: 0x0000F212
	public virtual Vector3 AngularVelocity()
	{
		if (this.rigidbody != null)
		{
			return this.rigidbody.angularVelocity;
		}
		return Vector3.zero;
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x00011033 File Offset: 0x0000F233
	public virtual Vector3 LocalAngularVelocity()
	{
		return base.transform.InverseTransformDirection(this.AngularVelocity());
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x00011046 File Offset: 0x0000F246
	public void SetSpawner(VehicleSpawner spawner)
	{
		this.spawner = spawner;
		this.applyAutoDamage = (this.spawner.respawnType != VehicleSpawner.RespawnType.Never);
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x00011066 File Offset: 0x0000F266
	protected static Vector2 Clamp2(Vector2 v)
	{
		return new Vector2(Mathf.Clamp(v.x, -1f, 1f), Mathf.Clamp(v.y, -1f, 1f));
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x0009B94C File Offset: 0x00099B4C
	protected static Vector4 Clamp4(Vector4 v)
	{
		return new Vector4(Mathf.Clamp(v.x, -1f, 1f), Mathf.Clamp(v.y, -1f, 1f), Mathf.Clamp(v.z, -1f, 1f), Mathf.Clamp(v.w, -1f, 1f));
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x0009B9B4 File Offset: 0x00099BB4
	public Seat GetEmptySeat(bool allowDriverSeat)
	{
		for (int i = allowDriverSeat ? 0 : 1; i < this.seats.Count; i++)
		{
			if (!this.seats[i].IsOccupied())
			{
				return this.seats[i];
			}
		}
		return null;
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x0009BA00 File Offset: 0x00099C00
	public int EmptySeats()
	{
		int num = 0;
		using (List<Seat>.Enumerator enumerator = this.seats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsOccupied())
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x0009BA5C File Offset: 0x00099C5C
	public bool IsFull()
	{
		using (List<Seat>.Enumerator enumerator = this.seats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsOccupied())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x0009BAB8 File Offset: 0x00099CB8
	public bool IsEmpty()
	{
		using (List<Seat>.Enumerator enumerator = this.seats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOccupied())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x00011097 File Offset: 0x0000F297
	public bool HasBlockSensor()
	{
		return this.blockSensor != null;
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x0009BB14 File Offset: 0x00099D14
	public int BlockTest(Collider[] outColliders, float extrapolationTime, int mask)
	{
		float num = Mathf.Max(0.1f, this.LocalVelocity().z * extrapolationTime);
		Vector3 point = this.blockSensorOrigin;
		point.z += num / 2f;
		Vector3 localScale = this.blockSensor.localScale;
		localScale.z = num;
		Vector3 vector = this.cachedLocalToWorldMatrix.MultiplyPoint(point);
		this.blockSensor.transform.position = vector;
		this.blockSensor.transform.localScale = localScale;
		return Physics.OverlapBoxNonAlloc(vector, localScale / 2f, outColliders, this.blockSensor.rotation, mask);
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x0009BBB4 File Offset: 0x00099DB4
	public bool CoarseLineOverlap(Vector3 origin, Vector3 target, float lineRadius = 0f)
	{
		Vector3 point = SMath.LineSegmentVsPointClosest(origin, target, base.transform.position);
		return this.IsCoarseOverlapping(point, lineRadius);
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x000110A5 File Offset: 0x0000F2A5
	public bool IsCoarseOverlapping(Vector3 point, float lineRadius = 0f)
	{
		return Vector3.Distance(base.transform.position, point) < this.GetAvoidanceCoarseRadius() + lineRadius;
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x0009BBDC File Offset: 0x00099DDC
	public bool IsStill()
	{
		return this.rigidbody == null || this.rigidbody.velocity.magnitude < 0.2f;
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x000110C2 File Offset: 0x0000F2C2
	public virtual bool ShouldBeAvoided()
	{
		return this.IsStill();
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000110CA File Offset: 0x0000F2CA
	public float GetHealthRatio()
	{
		return this.health / this.maxHealth;
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x0009BC14 File Offset: 0x00099E14
	protected virtual void HeavyDamage()
	{
		if (this.heavyDamageAudio != null && this.playerIsInside)
		{
			this.heavyDamageAudio.Play();
			FpsActorController.instance.Deafen();
			FpsActorController.instance.fpParent.ApplyScreenshake(15f, 3);
		}
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x000110D9 File Offset: 0x0000F2D9
	public bool AiShouldEnter()
	{
		return !this.stuck && !this.IsFull() && this.takingFireAction.TrueDone();
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x0009BA5C File Offset: 0x00099C5C
	public bool AllSeatsTaken()
	{
		using (List<Seat>.Enumerator enumerator = this.seats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsOccupied())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x0009BC64 File Offset: 0x00099E64
	public bool AnySeatsTaken()
	{
		using (List<Seat>.Enumerator enumerator = this.seats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOccupied())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000110F8 File Offset: 0x0000F2F8
	public bool AllSeatsReserved()
	{
		return this.reservedSeats >= this.seats.Count;
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x00011110 File Offset: 0x0000F310
	public void ReserveSeat()
	{
		this.reservedSeats++;
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x00011120 File Offset: 0x0000F320
	public void DropSeatReservation()
	{
		this.reservedSeats--;
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsWatercraft()
	{
		return false;
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsAmphibious()
	{
		return false;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsAircraft()
	{
		return false;
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x00011130 File Offset: 0x0000F330
	public virtual bool IsInWater()
	{
		return WaterLevel.IsInWater(base.transform.position);
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x00011142 File Offset: 0x0000F342
	public bool IsDamagedByRating(Vehicle.ArmorRating damageRating)
	{
		return damageRating >= this.armorDamagedBy;
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x00011150 File Offset: 0x0000F350
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
		return 1f;
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x0009BCC0 File Offset: 0x00099EC0
	public bool SeatsActor(Actor actor)
	{
		foreach (Seat seat in this.seats)
		{
			if (seat.IsOccupied() && seat.occupant == actor)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x0001116C File Offset: 0x0000F36C
	public void MarkAsBeingLocked()
	{
		this.isBeingLockedAction.Start();
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x00011179 File Offset: 0x0000F379
	public bool IsBeingLocked()
	{
		return !this.isBeingLockedAction.TrueDone();
	}

	// Token: 0x0400173B RID: 5947
	public const int AIR_AVOIDANCE_LAYER = 22;

	// Token: 0x0400173C RID: 5948
	private const float HEAVY_DAMAGE_THRESHOLD = 900f;

	// Token: 0x0400173D RID: 5949
	private const int HIGH_AUDIO_PRIORITY = 1;

	// Token: 0x0400173E RID: 5950
	private const float HUGE_AVOIDANCE_AREA_THRESHOLD = 200f;

	// Token: 0x0400173F RID: 5951
	private const float MAX_DEPENETRATION_VELOCITY = 10f;

	// Token: 0x04001740 RID: 5952
	private static RaycastHit[] ramResults = new RaycastHit[16];

	// Token: 0x04001741 RID: 5953
	private const int RAM_MASK = 256;

	// Token: 0x04001742 RID: 5954
	private const float EXPLODE_TIME = 0.3f;

	// Token: 0x04001743 RID: 5955
	public const float CLEANUP_TIME = 24f;

	// Token: 0x04001744 RID: 5956
	public const int LAYER = 12;

	// Token: 0x04001745 RID: 5957
	public const int FIRST_PERSON_LAYER = 18;

	// Token: 0x04001746 RID: 5958
	private const float AUTO_DAMAGE_START_TIME = 50f;

	// Token: 0x04001747 RID: 5959
	private const float AUTO_DAMAGE_PERIOD = 2f;

	// Token: 0x04001748 RID: 5960
	private const float AUTO_DAMAGE_PERCENT = 0.07f;

	// Token: 0x04001749 RID: 5961
	private const float RAM_MIN_SPEED = 3f;

	// Token: 0x0400174A RID: 5962
	public new string name = "VEHICLE";

	// Token: 0x0400174B RID: 5963
	public List<Seat> seats = new List<Seat>();

	// Token: 0x0400174C RID: 5964
	public Animator animator;

	// Token: 0x0400174D RID: 5965
	public Actor.TargetType targetType = Actor.TargetType.Unarmored;

	// Token: 0x0400174E RID: 5966
	public Vehicle.ArmorRating armorDamagedBy = Vehicle.ArmorRating.HeavyArms;

	// Token: 0x0400174F RID: 5967
	public float smallArmsMultiplier = 0.05f;

	// Token: 0x04001750 RID: 5968
	public float heavyArmsMultiplier = 1f;

	// Token: 0x04001751 RID: 5969
	[NonSerialized]
	public bool isTurret;

	// Token: 0x04001752 RID: 5970
	public bool canBeRepairedAfterDeath;

	// Token: 0x04001753 RID: 5971
	public float maxHealth = 1000f;

	// Token: 0x04001754 RID: 5972
	public float crashDamageSpeedThrehshold = 2f;

	// Token: 0x04001755 RID: 5973
	public float crashDamageMultiplier = 1f;

	// Token: 0x04001756 RID: 5974
	public float spotChanceMultiplier = 3f;

	// Token: 0x04001757 RID: 5975
	public float burnTime;

	// Token: 0x04001758 RID: 5976
	public bool crashSkipsBurn;

	// Token: 0x04001759 RID: 5977
	public bool directJavelinPath;

	// Token: 0x0400175A RID: 5978
	public float targetSize;

	// Token: 0x0400175B RID: 5979
	public bool canCapturePoints = true;

	// Token: 0x0400175C RID: 5980
	public bool canFireAtOwnVehicle;

	// Token: 0x0400175D RID: 5981
	[NonSerialized]
	public bool isLocked;

	// Token: 0x0400175E RID: 5982
	[NonSerialized]
	public bool allowPlayerSeatChange = true;

	// Token: 0x0400175F RID: 5983
	[NonSerialized]
	public bool allowPlayerSeatSwap = true;

	// Token: 0x04001760 RID: 5984
	public Transform targetLockPoint;

	// Token: 0x04001761 RID: 5985
	public Vehicle.AiType aiType;

	// Token: 0x04001762 RID: 5986
	public Vehicle.AiUseStrategy aiUseStrategy;

	// Token: 0x04001763 RID: 5987
	public bool aiUseToDefendPoint = true;

	// Token: 0x04001764 RID: 5988
	public int minCrewCount;

	// Token: 0x04001765 RID: 5989
	public float roamCompleteDistance;

	// Token: 0x04001766 RID: 5990
	public AudioSource interiorAudioSource;

	// Token: 0x04001767 RID: 5991
	private float interiorAudioSourceBaseVolume = 1f;

	// Token: 0x04001768 RID: 5992
	public ParticleSystem smokeParticles;

	// Token: 0x04001769 RID: 5993
	public ParticleSystem fireParticles;

	// Token: 0x0400176A RID: 5994
	public AudioSource fireAlarmSound;

	// Token: 0x0400176B RID: 5995
	public ParticleSystem deathParticles;

	// Token: 0x0400176C RID: 5996
	public AudioSource deathSound;

	// Token: 0x0400176D RID: 5997
	public AudioSource impactAudio;

	// Token: 0x0400176E RID: 5998
	public AudioSource heavyDamageAudio;

	// Token: 0x0400176F RID: 5999
	public Transform blockSensor;

	// Token: 0x04001770 RID: 6000
	public Texture blip;

	// Token: 0x04001771 RID: 6001
	public float blipScale = 0.5f;

	// Token: 0x04001772 RID: 6002
	public Vector2 avoidanceSize = Vector2.one;

	// Token: 0x04001773 RID: 6003
	public float pathingRadius;

	// Token: 0x04001774 RID: 6004
	public Vector3 ramSize = Vector3.one;

	// Token: 0x04001775 RID: 6005
	public Vector3 ramOffset = Vector3.zero;

	// Token: 0x04001776 RID: 6006
	public GameObject[] disableOnDeath;

	// Token: 0x04001777 RID: 6007
	public GameObject[] activateOnDeath;

	// Token: 0x04001778 RID: 6008
	public MaterialTarget[] teamColorMaterials;

	// Token: 0x04001779 RID: 6009
	public Vehicle.Engine engine;

	// Token: 0x0400177A RID: 6010
	public bool hasCountermeasures;

	// Token: 0x0400177B RID: 6011
	public float countermeasuresActiveTime = 3f;

	// Token: 0x0400177C RID: 6012
	public float countermeasuresCooldown = 20f;

	// Token: 0x0400177D RID: 6013
	public ParticleSystem countermeasureParticles;

	// Token: 0x0400177E RID: 6014
	public GameObject countermeasureSpawnPrefab;

	// Token: 0x0400177F RID: 6015
	public AudioSource countermeasureAudio;

	// Token: 0x04001780 RID: 6016
	[NonSerialized]
	public List<CarWheel> wheels = new List<CarWheel>();

	// Token: 0x04001781 RID: 6017
	[NonSerialized]
	public bool applyAutoDamage = true;

	// Token: 0x04001782 RID: 6018
	[NonSerialized]
	public int ownerTeam = -1;

	// Token: 0x04001783 RID: 6019
	[NonSerialized]
	public bool playerIsInside;

	// Token: 0x04001784 RID: 6020
	[NonSerialized]
	public bool claimedByPlayer;

	// Token: 0x04001785 RID: 6021
	[NonSerialized]
	public bool stuck;

	// Token: 0x04001786 RID: 6022
	[NonSerialized]
	public SpawnPoint closestSpawnPoint;

	// Token: 0x04001787 RID: 6023
	private TimedAction takingFireAction = new TimedAction(20f, false);

	// Token: 0x04001788 RID: 6024
	[NonSerialized]
	public float health;

	// Token: 0x04001789 RID: 6025
	[NonSerialized]
	public bool dead;

	// Token: 0x0400178A RID: 6026
	[NonSerialized]
	public Rigidbody rigidbody;

	// Token: 0x0400178B RID: 6027
	[NonSerialized]
	public VehicleSpawner spawner;

	// Token: 0x0400178C RID: 6028
	private bool reportedFirstDriver;

	// Token: 0x0400178D RID: 6029
	private bool claimedBySquad;

	// Token: 0x0400178E RID: 6030
	[NonSerialized]
	public Squad claimingSquad;

	// Token: 0x0400178F RID: 6031
	[NonSerialized]
	public Collider[] colliders;

	// Token: 0x04001790 RID: 6032
	[NonSerialized]
	public bool isHuge;

	// Token: 0x04001791 RID: 6033
	[NonSerialized]
	public bool isInvulnerable;

	// Token: 0x04001792 RID: 6034
	private Vector3 blockSensorOrigin;

	// Token: 0x04001793 RID: 6035
	private Vector3 safeResetPosition;

	// Token: 0x04001794 RID: 6036
	private TimedAction cannotRamAction = new TimedAction(0.5f, false);

	// Token: 0x04001795 RID: 6037
	private TimedAction crashDamageCooldown = new TimedAction(0.2f, false);

	// Token: 0x04001796 RID: 6038
	private TimedAction countermeasuresCooldownAction;

	// Token: 0x04001797 RID: 6039
	private TimedAction countermeasuresActiveAction;

	// Token: 0x04001798 RID: 6040
	private TimedAction isBeingLockedAction = new TimedAction(2f, false);

	// Token: 0x04001799 RID: 6041
	[NonSerialized]
	public HashSet<TargetSeekingMissile> trackingMissiles = new HashSet<TargetSeekingMissile>();

	// Token: 0x0400179A RID: 6042
	[NonSerialized]
	public bool burning;

	// Token: 0x0400179B RID: 6043
	private DamageInfo burningDamageSource;

	// Token: 0x0400179C RID: 6044
	private int stopBurningRepairs;

	// Token: 0x0400179D RID: 6045
	private int reservedSeats;

	// Token: 0x0400179E RID: 6046
	[NonSerialized]
	public SquadOrderPoint squadOrderPoint;

	// Token: 0x0400179F RID: 6047
	private bool notifyAnimatorVelocity;

	// Token: 0x040017A0 RID: 6048
	private bool notifyAnimatorInput;

	// Token: 0x040017A1 RID: 6049
	private Actor lastDamageSource;

	// Token: 0x040017A2 RID: 6050
	private TimedAction lastDamageSourceAppliesAction = new TimedAction(5f, false);

	// Token: 0x040017A3 RID: 6051
	protected Vector4 driverInput = Vector4.zero;

	// Token: 0x040017A4 RID: 6052
	[NonSerialized]
	public bool canSeePlayer;

	// Token: 0x040017A5 RID: 6053
	[NonSerialized]
	public float playerDistance;

	// Token: 0x040017A6 RID: 6054
	[NonSerialized]
	public bool canBeTakenOverByPlayerSquad = true;

	// Token: 0x040017A7 RID: 6055
	private TimedAction highlightAction = new TimedAction(4f, false);

	// Token: 0x040017A8 RID: 6056
	private TimedAction canStopSmokeInWater = new TimedAction(1f, false);

	// Token: 0x040017A9 RID: 6057
	private Vector3 cachedVelocity = Vector3.zero;

	// Token: 0x040017AA RID: 6058
	private Vector3 cachedLocalVelocity = Vector3.zero;

	// Token: 0x040017AB RID: 6059
	private Vector3 originalCenterOfMass;

	// Token: 0x040017AC RID: 6060
	[NonSerialized]
	public Vector3 acceleration;

	// Token: 0x040017AD RID: 6061
	[NonSerialized]
	public Matrix4x4 cachedLocalToWorldMatrix;

	// Token: 0x040017AE RID: 6062
	[NonSerialized]
	public Matrix4x4 cachedWorldToLocalMatrix;

	// Token: 0x040017AF RID: 6063
	public Vehicle.DelOnDestroyed onDestroyed;

	// Token: 0x0200034A RID: 842
	public enum AiType
	{
		// Token: 0x040017B1 RID: 6065
		Capture,
		// Token: 0x040017B2 RID: 6066
		Roam,
		// Token: 0x040017B3 RID: 6067
		Transport
	}

	// Token: 0x0200034B RID: 843
	public enum AiUseStrategy
	{
		// Token: 0x040017B5 RID: 6069
		Default,
		// Token: 0x040017B6 RID: 6070
		OnlyFromFrontlineSpawn,
		// Token: 0x040017B7 RID: 6071
		FromAnySpawn
	}

	// Token: 0x0200034C RID: 844
	public enum AiAttackStrategy
	{
		// Token: 0x040017B9 RID: 6073
		Default,
		// Token: 0x040017BA RID: 6074
		Random,
		// Token: 0x040017BB RID: 6075
		BehindEnemyLines
	}

	// Token: 0x0200034D RID: 845
	public enum ArmorRating
	{
		// Token: 0x040017BD RID: 6077
		SmallArms,
		// Token: 0x040017BE RID: 6078
		HeavyArms,
		// Token: 0x040017BF RID: 6079
		AntiTank
	}

	// Token: 0x0200034E RID: 846
	// (Invoke) Token: 0x06001589 RID: 5513
	public delegate void DelOnDestroyed();

	// Token: 0x0200034F RID: 847
	[Serializable]
	public class Engine
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x0009BED4 File Offset: 0x0009A0D4
		[Ignore]
		public void SetAudioSource(AudioSource audio)
		{
			this.audio = audio;
			if (audio != null)
			{
				this.baseVolume = audio.volume;
				this.basePitch = audio.pitch;
				this.baseAudioPriority = audio.priority;
			}
			if (this.throttleAudioSource != null)
			{
				this.baseThrottleVolume = this.throttleAudioSource.volume;
				this.baseThrottlePitch = this.throttleAudioSource.pitch;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00011197 File Offset: 0x0000F397
		[Ignore]
		public void SetWindZone(WindZone windZone)
		{
			this.windZone = windZone;
			if (this.windZone != null)
			{
				this.baseWind = this.windZone.windMain;
				this.baseWindTurbulence = this.windZone.windTurbulence;
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0009BF48 File Offset: 0x0009A148
		[Ignore]
		public void PrioritizeAudioSource()
		{
			if (this.audio != null)
			{
				this.audio.priority = 1;
			}
			if (this.extraAudioSource != null)
			{
				this.extraAudioSource.priority = 1;
			}
			if (this.throttleAudioSource != null)
			{
				this.throttleAudioSource.priority = 1;
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0009BFA4 File Offset: 0x0009A1A4
		[Ignore]
		public void ResetAudioSourcePriority()
		{
			if (this.audio != null)
			{
				this.audio.priority = this.baseAudioPriority;
			}
			if (this.extraAudioSource != null)
			{
				this.extraAudioSource.priority = this.baseAudioPriority + 2;
			}
			if (this.throttleAudioSource != null)
			{
				this.throttleAudioSource.priority = this.baseAudioPriority + 1;
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000111D0 File Offset: 0x0000F3D0
		public void PlayIgnitionSound()
		{
			if (this.extraAudioSource != null && this.ignitionClip != null)
			{
				this.extraAudioSource.PlayOneShot(this.ignitionClip);
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x000111FF File Offset: 0x0000F3FF
		public void PlayShiftForwardSound()
		{
			if (this.extraAudioSource != null && this.shiftForwardClip != null)
			{
				this.extraAudioSource.PlayOneShot(this.shiftForwardClip);
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0001122E File Offset: 0x0000F42E
		public void PlayShiftReverseSound()
		{
			if (this.extraAudioSource != null && this.shiftReverseClip != null)
			{
				this.extraAudioSource.PlayOneShot(this.shiftReverseClip);
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0009C014 File Offset: 0x0009A214
		[Ignore]
		public void Update()
		{
			this.power = Mathf.MoveTowards(this.power, this.enabled ? 1f : 0f, this.powerGainSpeed * Time.deltaTime);
			this.pitch = Mathf.MoveTowards(this.pitch, this.targetPitch, this.pitchGainSpeed * Time.deltaTime);
			this.throttle = Mathf.MoveTowards(this.throttle, this.targetThrottle, this.throttleGainSpeed * Time.deltaTime);
			if (this.controlAudio && this.audio != null)
			{
				this.audio.pitch = this.power * this.pitch * this.basePitch;
				this.audio.volume = this.power * this.baseVolume;
				this.audio.enabled = (this.power > 0f);
			}
			if (this.windZone != null)
			{
				this.windZone.windMain = this.power * this.baseWind;
				this.windZone.windTurbulence = this.power * this.baseWindTurbulence;
			}
			if (this.throttleAudioSource != null)
			{
				this.throttleAudioSource.volume = this.throttle * this.baseThrottleVolume;
				this.throttleAudioSource.pitch = Mathf.Lerp(0.5f, 1f, this.throttle) * this.baseThrottlePitch;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0009C188 File Offset: 0x0009A388
		public void Reset()
		{
			if (this.controlAudio && this.audio != null)
			{
				this.audio.pitch = 0f;
				this.audio.volume = 0f;
				this.audio.enabled = false;
			}
			if (this.windZone != null)
			{
				this.windZone.windMain = 0f;
			}
			this.pitch = 0f;
			this.power = 0f;
		}

		// Token: 0x040017C0 RID: 6080
		public bool controlAudio = true;

		// Token: 0x040017C1 RID: 6081
		public float powerGainSpeed = 1f;

		// Token: 0x040017C2 RID: 6082
		public float pitchGainSpeed = 1f;

		// Token: 0x040017C3 RID: 6083
		public float throttleGainSpeed = 2f;

		// Token: 0x040017C4 RID: 6084
		[Ignore]
		public AudioSource throttleAudioSource;

		// Token: 0x040017C5 RID: 6085
		[Ignore]
		public AudioSource extraAudioSource;

		// Token: 0x040017C6 RID: 6086
		public AudioClip shiftForwardClip;

		// Token: 0x040017C7 RID: 6087
		public AudioClip shiftReverseClip;

		// Token: 0x040017C8 RID: 6088
		public AudioClip ignitionClip;

		// Token: 0x040017C9 RID: 6089
		[Ignore]
		[NonSerialized]
		public AudioSource audio;

		// Token: 0x040017CA RID: 6090
		[NonSerialized]
		public float power;

		// Token: 0x040017CB RID: 6091
		[NonSerialized]
		public bool enabled;

		// Token: 0x040017CC RID: 6092
		[NonSerialized]
		public float targetPitch = 1f;

		// Token: 0x040017CD RID: 6093
		[NonSerialized]
		public float targetThrottle;

		// Token: 0x040017CE RID: 6094
		private WindZone windZone;

		// Token: 0x040017CF RID: 6095
		private int baseAudioPriority;

		// Token: 0x040017D0 RID: 6096
		private float pitch;

		// Token: 0x040017D1 RID: 6097
		private float throttle;

		// Token: 0x040017D2 RID: 6098
		private float baseVolume;

		// Token: 0x040017D3 RID: 6099
		private float basePitch;

		// Token: 0x040017D4 RID: 6100
		private float baseThrottleVolume;

		// Token: 0x040017D5 RID: 6101
		private float baseThrottlePitch;

		// Token: 0x040017D6 RID: 6102
		private float baseWind;

		// Token: 0x040017D7 RID: 6103
		private float baseWindTurbulence;
	}
}
