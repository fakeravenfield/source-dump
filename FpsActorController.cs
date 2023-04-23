using System;
using System.Collections;
using System.Collections.Generic;
using LevelSystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityStandardAssets.Characters.FirstPerson;

// Token: 0x02000234 RID: 564
[RequireComponent(typeof(FirstPersonController))]
public class FpsActorController : ActorController
{
	// Token: 0x06000F08 RID: 3848 RVA: 0x00082018 File Offset: 0x00080218
	private void Awake()
	{
		FpsActorController.instance = this;
		PostProcessingManager.RegisterFirstPersonViewModelCamera(this.fpViewModelCamera);
		PostProcessingManager.RegisterWorldCamera(this.fpCamera);
		PostProcessingManager.RegisterWorldCamera(this.tpCamera);
		this.unlockCursorRavenscriptOverride = false;
		this.allowMouseLookRavenscriptOverride = true;
		this.controller = base.GetComponent<FirstPersonController>();
		this.characterController = base.GetComponent<CharacterController>();
		this.thirdpersonRenderers = this.actor.ragdoll.AnimatedRenderers();
		this.controller.onJump = new FirstPersonController.DelOnActionCallback(this.OnJump);
		this.controller.onLand = new FirstPersonController.DelOnActionCallback(this.OnLand);
		this.fpCameraRoot = this.fpParent.fpCameraRoot;
		this.fpCameraParentOffset = this.fpCameraRoot.transform.localPosition;
		this.kickRenderer = this.kickAnimation.GetComponentInChildren<SkinnedMeshRenderer>();
		this.kickSound = this.kickAnimation.GetComponent<AudioSource>();
		this.aimInput = new PlayerActionInput(SteelInput.KeyBinds.Aim, OptionToggle.Id.ToggleAim);
		this.crouchInput = new PlayerActionInput(SteelInput.KeyBinds.Crouch, OptionToggle.Id.ToggleCrouch);
		this.proneInput = new PlayerActionInput(SteelInput.KeyBinds.Prone);
		this.sprintInput = new PlayerActionInput(SteelInput.KeyBinds.Sprint, OptionToggle.Id.ToggleSprint);
		this.fpCamera.enabled = false;
		this.fpCamera.farClipPlane = Options.maxDrawDistance;
		this.tpCamera.farClipPlane = Options.maxDrawDistance;
		this.blackoutTexture = new Texture2D(8, 8);
		this.ForceStandStance();
		this.DisableInput();
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0000BFFA File Offset: 0x0000A1FA
	public override bool IsAirborne()
	{
		return !this.controller.OnGround();
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x0008217C File Offset: 0x0008037C
	private void OnLand()
	{
		if (this.actor.IsSeated())
		{
			return;
		}
		float num = Mathf.Clamp((-this.actor.Velocity().y - 2f) * 0.3f, 0f, 2f);
		if (this.IsSprinting())
		{
			num *= 2f;
		}
		this.fpParent.ApplyRecoil(new Vector3(0f, -num * 0.3f, 0f), true);
		this.fpParent.KickCamera(new Vector3(num, 0f, 0f));
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x0000C00A File Offset: 0x0000A20A
	public override Squad GetSquad()
	{
		return this.playerSquad;
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x00082214 File Offset: 0x00080414
	private void OnJump()
	{
		if (this.actor.IsSeated())
		{
			return;
		}
		Vector3 vector = new Vector3(0f, 0.2f, 0f);
		if (this.IsSprinting())
		{
			Vector3 vector2 = this.fpCamera.transform.worldToLocalMatrix.MultiplyVector(this.Velocity());
			vector -= Vector3.ClampMagnitude(vector2, 1f);
		}
		this.fpParent.ApplyRecoil(vector, false);
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0008228C File Offset: 0x0008048C
	private void Start()
	{
		this.actorLocalOrigin = this.actor.transform.localPosition;
		this.defaultMix.TransitionTo(0f);
		if (GameManager.IsConnectedToSteam())
		{
			this.actor.name = GameManager.instance.steamworks.GetSteamNick();
		}
		else
		{
			this.actor.name = "Unknown Player";
		}
		if (!GameManager.IsSpectating())
		{
			ActorSkin actorSkin = ActorManager.instance.actorSkin[GameManager.PlayerTeam()];
			if (actorSkin != null && actorSkin.kickLegSkin.mesh != null)
			{
				ActorManager.ApplyOverrideMeshSkin(this.kickRenderer, actorSkin.kickLegSkin, GameManager.PlayerTeam());
			}
			else
			{
				this.kickRenderer.material = ColorScheme.GetActorMaterial(this.actor.team);
			}
			ScoreboardUi.AddEntryForActor(this.actor);
		}
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0000C012 File Offset: 0x0000A212
	public bool UseThirdPersonVehicleCamera()
	{
		return this.vehicleThirdPersonCamera;
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0000C01A File Offset: 0x0000A21A
	public override bool Fire()
	{
		return !this.IsSprinting() && this.sprintCannotFireAction.TrueDone() && !GameManager.gameOver && (SteelInput.GetButton(SteelInput.KeyBinds.Fire) && !this.IsCursorFree()) && !this.IsKicking();
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x00082360 File Offset: 0x00080560
	public override bool Aiming()
	{
		return this.aimInput.GetInput() && !this.IsCursorFree() && !this.IsKicking() && !this.actor.IsSwimming() && !this.actor.parachuteDeployed && !GameManager.gameOver;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0000C055 File Offset: 0x0000A255
	public override bool Reload()
	{
		return SteelInput.GetButton(SteelInput.KeyBinds.Reload) && !this.IsCursorFree() && !this.IsKicking() && !GameManager.gameOver;
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x0000C079 File Offset: 0x0000A279
	public override bool OnGround()
	{
		return this.controller.OnGround();
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool ProjectToGround()
	{
		return false;
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x0000C086 File Offset: 0x0000A286
	public override Vector3 Velocity()
	{
		return this.controller.Velocity();
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x000823B0 File Offset: 0x000805B0
	public override Vector3 SwimInput()
	{
		return this.tpCamera.transform.forward * SteelInput.GetAxis(SteelInput.KeyBinds.Vertical) + -this.tpCamera.transform.right * SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal);
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x0000C093 File Offset: 0x0000A293
	public override Vector3 FacingDirection()
	{
		return this.fpCamera.transform.forward;
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x0000C0A5 File Offset: 0x0000A2A5
	public override Vector2 BoatInput()
	{
		return this.CarInput();
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool ForceStopVehicle()
	{
		return false;
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0000C0AD File Offset: 0x0000A2AD
	public override Vector2 CarInput()
	{
		if (GameManager.gameOver)
		{
			return Vector2.zero;
		}
		return new Vector2(-SteelInput.GetAxis(SteelInput.KeyBinds.CarSteer), SteelInput.GetAxis(SteelInput.KeyBinds.CarThrottle));
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x00082400 File Offset: 0x00080600
	private Vector4 GetHelicopterAutoHoverInput()
	{
		Vehicle vehicle = this.actor.seat.vehicle;
		Vector3 vector = vehicle.LocalVelocity();
		Vector3 eulerAngles = vehicle.transform.eulerAngles;
		Vector3 vector2 = vehicle.LocalAngularVelocity();
		eulerAngles.x = Mathf.DeltaAngle(eulerAngles.x, 0f);
		eulerAngles.z = Mathf.DeltaAngle(eulerAngles.z, 0f);
		float z = -1.5f * eulerAngles.z + 20f * vector2.z;
		float w = 1.5f * eulerAngles.x - 20f * vector2.x;
		float y = 0.05f - 0.3f * vector.y;
		return new Vector4(-2f * vector2.y, y, z, w);
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x000824C4 File Offset: 0x000806C4
	public override Vector4 HelicopterInput()
	{
		if (GameManager.gameOver)
		{
			return new Vector4(0f, 1f, 0f, 0f);
		}
		float sensitivity = Options.GetScaledMouseSensitivity(OptionSlider.Id.HelicopterSensitivity);
		if (this.actor.IsAiming() || this.IsCursorFree())
		{
			sensitivity = 0f;
		}
		Vector4 vector = new Vector4(-this.ScaleAxisIfMouse(SteelInput.KeyBinds.HeliYaw, sensitivity), this.ScaleAxisIfMouse(SteelInput.KeyBinds.HeliThrottle, sensitivity), -this.ScaleAxisIfMouse(SteelInput.KeyBinds.HeliRoll, sensitivity), this.ScaleAxisIfMouse(SteelInput.KeyBinds.HeliPitch, sensitivity));
		if (this.helicopterAutoHoverEnabled)
		{
			return this.GetHelicopterAutoHoverInput() + vector * 2f;
		}
		return vector;
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x00082564 File Offset: 0x00080764
	public override Vector4 AirplaneInput()
	{
		if (GameManager.gameOver)
		{
			return new Vector4(0f, 1f, 0f, 0.2f);
		}
		float sensitivity = Options.GetScaledMouseSensitivity(OptionSlider.Id.PlaneSensitivity);
		if (this.actor.IsAiming() || this.IsCursorFree())
		{
			sensitivity = 0f;
		}
		return new Vector4(-this.ScaleAxisIfMouse(SteelInput.KeyBinds.PlaneYaw, sensitivity), this.ScaleAxisIfMouse(SteelInput.KeyBinds.PlaneThrottle, sensitivity), -this.ScaleAxisIfMouse(SteelInput.KeyBinds.PlaneRoll, sensitivity), this.ScaleAxisIfMouse(SteelInput.KeyBinds.PlanePitch, sensitivity));
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
	private float ScaleAxisIfMouse(SteelInput.KeyBinds input, float sensitivity)
	{
		if (SteelInput.GetInput(input).isMouseAxis)
		{
			return SteelInput.GetAxis(input) * sensitivity;
		}
		return SteelInput.GetAxis(input);
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool UseMuzzleDirection()
	{
		return true;
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x000825E0 File Offset: 0x000807E0
	public override void OnVehicleWasDamaged(Actor source, float damage)
	{
		if (source != null && damage > 5f)
		{
			Vector3 a;
			if (this.actor.seat.vehicle.rigidbody != null)
			{
				a = this.actor.seat.vehicle.rigidbody.position - source.Position();
			}
			else
			{
				a = this.actor.CenterPosition() - source.Position();
			}
			Vector3 vector = this.GetActiveCamera().transform.worldToLocalMatrix.MultiplyVector(-a);
			float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
			IngameUi.instance.ShowDamageIndicator(angle, false);
		}
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000826AC File Offset: 0x000808AC
	public override void ReceivedDamage(bool friendlyFire, float damage, float balanceDamage, Vector3 point, Vector3 direction, Vector3 force)
	{
		if (balanceDamage > 5f)
		{
			this.fpParent.ApplyScreenshake(balanceDamage / 6f, Mathf.CeilToInt(balanceDamage / 20f));
		}
		if (damage > 5f)
		{
			this.fpParent.KickCamera(new Vector3(UnityEngine.Random.Range(5f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-5f, 5f)));
		}
		if (balanceDamage > 50f)
		{
			this.Deafen();
		}
		Vector3 vector = this.GetActiveCamera().transform.worldToLocalMatrix.MultiplyVector(-direction);
		float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
		IngameUi.instance.ShowDamageIndicator(angle, damage < 2f && balanceDamage > damage);
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0000C0EE File Offset: 0x0000A2EE
	private bool IsInEnclosedVehicle()
	{
		return this.actor.IsSeated() && this.actor.seat.enclosed;
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0000C10F File Offset: 0x0000A30F
	public void Deafen()
	{
		if (GameManager.gameOver)
		{
			return;
		}
		this.deafMix.TransitionTo(0.7f);
		base.CancelInvoke("Undeafen");
		base.Invoke("Undeafen", 5f);
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x0000C144 File Offset: 0x0000A344
	private void Undeafen()
	{
		if (GameManager.gameOver)
		{
			return;
		}
		if (this.IsInEnclosedVehicle())
		{
			this.enclosedMix.TransitionTo(8f);
			return;
		}
		this.defaultMix.TransitionTo(8f);
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0000C177 File Offset: 0x0000A377
	public override void DisableInput()
	{
		this.characterController.enabled = false;
		this.controller.inputEnabled = false;
		this.inputEnabled = false;
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x0000C198 File Offset: 0x0000A398
	public override void EnableInput()
	{
		this.characterController.enabled = true;
		this.controller.inputEnabled = true;
		this.inputEnabled = true;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x0000C1B9 File Offset: 0x0000A3B9
	public override void StartLadder(Ladder ladder)
	{
		this.DisableCharacterController();
		this.HideFpModel();
		this.SetFirstPersonCameraParent(this.actor.transform, new Vector3(0f, 1.6f, -0.5f));
		this.cannotLeaveAction.Start();
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0008278C File Offset: 0x0008098C
	public override void EndLadder(Vector3 exitPosition, Quaternion flatFacing)
	{
		base.transform.position = exitPosition + 0.8f * Vector3.up;
		base.transform.rotation = flatFacing;
		this.actor.transform.position = exitPosition;
		this.EnableCharacterController();
		this.ShowFpModel();
		this.ResetFirstPersonCameraParent();
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x0000C1F7 File Offset: 0x0000A3F7
	private void DisableCharacterController()
	{
		this.controller.DisableCharacterController();
		this.controller.ResetSliding();
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0000C20F File Offset: 0x0000A40F
	private void EnableCharacterController()
	{
		this.controller.EnableCharacterController();
		this.controller.ResetSliding();
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x0000C227 File Offset: 0x0000A427
	private void SetFirstPersonCameraParent(Transform parent, Vector3 localOffset)
	{
		this.fpCameraRoot.parent = parent;
		this.fpCameraRoot.localPosition = localOffset;
		this.fpCameraRoot.localRotation = Quaternion.identity;
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x000827E8 File Offset: 0x000809E8
	private void ResetFirstPersonCameraParent()
	{
		this.fpCameraRoot.parent = base.transform;
		this.fpCameraRoot.localPosition = new Vector3(0f, this.cameraHeight, 0f);
		this.fpCameraRoot.localRotation = Quaternion.identity;
		this.fpCameraRoot.localScale = Vector3.one;
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0000C251 File Offset: 0x0000A451
	public void OnSeatCameraUpdated(Seat seat)
	{
		this.mouseViewLocked = (seat.type == Seat.Type.LockedAllowFreelookUnarmed || seat.type == Seat.Type.AlwaysLockedUnarmed);
		this.allowMouseViewWhileAiming = (seat.type != Seat.Type.AlwaysLockedUnarmed);
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00082848 File Offset: 0x00080A48
	public override void StartSeated(Seat seat)
	{
		this.DisableCharacterController();
		this.OnSeatCameraUpdated(seat);
		this.cameraHeight = 0.85f;
		this.SetFirstPersonCameraParent(seat.transform, new Vector3(0f, 0.85f, 0.2f));
		this.helicopterAutoHoverEnabled = false;
		if (!seat.CanUsePersonalWeapons())
		{
			if (seat.vehicle.GetType() == typeof(Helicopter))
			{
				this.fpParent.SetFov(75f, 50f);
			}
			else
			{
				this.fpParent.SetAimFov(45f, true);
			}
		}
		if (!seat.CanUsePersonalWeapons())
		{
			this.HideFpModel();
		}
		this.vehicleThirdPersonCamera = false;
		if (this.playerSquad.members.Count > 1)
		{
			this.playerSquad.PlayerSquadTakeOverSquadVehicle(seat.vehicle);
			this.playerSquad.PlayerOrderEnterVehicle(seat.vehicle);
		}
		IngameUi.instance.ShowVehicleBar(seat.vehicle.GetHealthRatio(), true);
		this.aimInput.Reset();
		this.crouchInput.Reset();
		this.proneInput.Reset();
		this.sprintInput.Reset();
		this.UpdateSeatedMixGroup();
		this.fpParent.ResetRecoil();
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x0000C280 File Offset: 0x0000A480
	private void UpdateSeatedMixGroup()
	{
		if (!this.vehicleThirdPersonCamera && this.actor.seat.UseMuffledSoundMix())
		{
			this.enclosedMix.TransitionTo(0.4f);
			return;
		}
		this.defaultMix.TransitionTo(0.4f);
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00082980 File Offset: 0x00080B80
	private void BaseEndSeated(Seat leftSeat)
	{
		this.mouseViewLocked = false;
		this.audioListenerAutoParent.ForceDetatch();
		this.ResetFirstPersonCameraParent();
		this.helicopterAutoHoverEnabled = false;
		if (this.IsUsingOverrideCamera())
		{
			this.CancelOverrideCamera();
		}
		this.SetupWeaponFov(this.actor.activeWeapon);
		this.ShowFpModel();
		IngameUi.instance.HideVehicleBar(true);
		this.aimInput.Reset();
		this.crouchInput.Reset();
		this.proneInput.Reset();
		this.sprintInput.Reset();
		if (leftSeat.enclosed)
		{
			this.defaultMix.TransitionTo(1f);
		}
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
	public override void EndSeatedSwap(Seat leftSeat)
	{
		this.BaseEndSeated(leftSeat);
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x0000C2C9 File Offset: 0x0000A4C9
	public override void EndSeated(Seat leftSeat, Vector3 exitPosition, Quaternion flatFacing, bool forcedByFallingOver)
	{
		this.EnableCharacterController();
		this.TeleportTo(exitPosition + new Vector3(0f, 0.8f, 0f));
		base.transform.rotation = flatFacing;
		this.BaseEndSeated(leftSeat);
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x0000C304 File Offset: 0x0000A504
	private void ResetActorPosition()
	{
		this.actor.transform.localPosition = this.actorLocalOrigin;
		this.FreezeNextCharacterControllerTick();
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x0000C322 File Offset: 0x0000A522
	private void FreezeNextCharacterControllerTick()
	{
		if (this.freezeCharacterControllerCoroutine != null)
		{
			base.StopCoroutine(this.freezeCharacterControllerCoroutine);
		}
		this.freezeCharacterControllerCoroutine = base.StartCoroutine(this.FreezeNextCharacterControllerTickCoroutine());
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x0000C34A File Offset: 0x0000A54A
	private IEnumerator FreezeNextCharacterControllerTickCoroutine()
	{
		this.characterController.enabled = false;
		yield return new WaitForFixedUpdate();
		this.characterController.enabled = true;
		this.freezeCharacterControllerCoroutine = null;
		yield break;
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x0000C359 File Offset: 0x0000A559
	private void TeleportTo(Vector3 position)
	{
		base.transform.position = position;
		this.actor.rigidbody.position = position;
		this.ResetActorPosition();
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x0000C37E File Offset: 0x0000A57E
	public override void StartRagdoll()
	{
		this.aimInput.Reset();
		this.crouchInput.Reset();
		this.proneInput.Reset();
		this.sprintInput.Reset();
		this.actor.SetImposterRenderingEnabled(false);
		this.ThirdPersonCamera();
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00082A20 File Offset: 0x00080C20
	public override void GettingUp()
	{
		base.transform.position = this.actor.ragdoll.Position();
		this.ResetActorPosition();
		Debug.DrawRay(base.transform.position, Vector3.up * 100f, Color.green, 100f);
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0000C3BE File Offset: 0x0000A5BE
	public override void EndRagdoll()
	{
		this.actor.SetImposterRenderingEnabled(true);
		this.hasNotBeenGroundedAction.Start();
		this.FirstPersonCamera();
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00082A78 File Offset: 0x00080C78
	public override void Die(Actor killer)
	{
		GameManager.DisableNightVision();
		this.goggles = null;
		this.hasAcceptedLoadoutAfterDeath = false;
		this.readyToRespawnCooldownAction.Start();
		StrategyUi.HideSquadOrderMarker();
		this.playerSquad.DropMember(this);
		SquadPointUi.Hide(false);
		this.ThirdPersonCamera();
		this.UpdateThirdPersonCamera(true);
		if (KillCamera.instance.enabled)
		{
			if (killer != null && killer != this.actor && !GameManager.gameOver)
			{
				KillCamera.RenderKillCamera(killer);
				KillCamera.Show(killer, "Info about stuff");
				this.faceKillerOrigin = this.fpCamera.transform.rotation;
				this.killer = killer;
				this.faceKillerAction.Start();
			}
			else
			{
				KillCamera.ShowSuicide();
			}
		}
		SpawnCountdownUi.OnDeath();
		this.defaultMix.TransitionTo(4f);
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x0000C3DD File Offset: 0x0000A5DD
	public void OpenLoadout()
	{
		if (GameManager.gameOver)
		{
			return;
		}
		LoadoutUi.Show(!this.actor.dead);
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x0000C3FA File Offset: 0x0000A5FA
	public void CloseLoadout()
	{
		LoadoutUi.Hide(false);
		this.hasAcceptedLoadoutAfterDeath = true;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00082B48 File Offset: 0x00080D48
	public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
	{
		this.controller.transform.position = position + Vector3.up * (this.characterController.height / 2f);
		this.controller.transform.rotation = rotation;
		this.actor.animator.transform.localPosition = Vector3.zero;
		this.controller.ResetVelocity();
		this.controller.ResetSliding();
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00082BC8 File Offset: 0x00080DC8
	public override void SpawnAt(Vector3 position, Quaternion rotation)
	{
		KillCamera.Hide();
		this.faceKillerAction.Stop();
		EffectUi.FadeIn(EffectUi.FadeType.EyeAndFullScreen, 0.3f, Color.black);
		if (this.IsUsingOverrideCamera())
		{
			this.CancelOverrideCamera();
		}
		SceneryCamera.instance.camera.enabled = false;
		this.SetPositionAndRotation(position, rotation);
		this.kickCooldownAction.Start();
		this.hasNotBeenGroundedAction.Start();
		this.allowPlayerControlledTpCamera = true;
		this.FirstPersonCamera();
		this.ForceStandStance();
		this.attractMode = false;
		this.allowExitVehicle = true;
		this.fpCameraRoot.transform.localEulerAngles = Vector3.zero;
		this.fpParent.ResetRecoil();
		this.fpParent.ResetCameraOffset();
		this.aimInput.Reset();
		this.helicopterAutoHoverEnabled = false;
		this.playerSquad = new Squad(new List<ActorController>(1)
		{
			this
		}, null, null, null, 0f);
		this.goggles = null;
		this.targetCameraHeight = 1.5300001f;
		this.cameraHeight = this.targetCameraHeight;
		this.fpCameraRoot.localPosition = new Vector3(0f, this.cameraHeight, 0f);
		foreach (Weapon weapon in this.actor.weapons)
		{
			if (weapon != null && weapon.GetType() == typeof(NightVision))
			{
				this.goggles = (NightVision)weapon;
			}
		}
		this.ResetActorPosition();
		this.defaultMix.TransitionTo(1f);
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00082D50 File Offset: 0x00080F50
	public override void ApplyRecoil(Vector3 impulse)
	{
		this.fpParent.ApplyRecoil(impulse, true);
		Weapon activeWeapon = this.actor.activeWeapon;
		this.fpParent.ApplyWeaponSnap(activeWeapon.configuration.snapMagnitude, activeWeapon.configuration.snapDuration, activeWeapon.configuration.snapFrequency);
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x0000C409 File Offset: 0x0000A609
	public override float Lean()
	{
		if (this.IsSprinting() || (this.actor.IsSeated() && !this.actor.seat.allowLean))
		{
			return 0f;
		}
		return -SteelInput.GetAxis(SteelInput.KeyBinds.Lean);
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x0000C43F File Offset: 0x0000A63F
	private void HideFpModel()
	{
		if (this.actor.HasUnholsteredWeapon())
		{
			this.actor.activeWeapon.Hide();
		}
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x0000C45E File Offset: 0x0000A65E
	private void ShowFpModel()
	{
		if (this.actor.HasUnholsteredWeapon())
		{
			this.actor.activeWeapon.Show();
		}
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x0000C47D File Offset: 0x0000A67D
	public bool IsUsingOverrideCamera()
	{
		return this.overrideCamera != null;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0000C48B File Offset: 0x0000A68B
	public Camera GetActiveCamera()
	{
		if (this.IsUsingOverrideCamera())
		{
			return this.overrideCamera;
		}
		if (this.tpCamera.enabled)
		{
			return this.tpCamera;
		}
		return this.fpCamera;
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00082DA4 File Offset: 0x00080FA4
	public void SetOverrideCamera(Camera camera)
	{
		PostProcessingManager.RegisterWorldCamera(camera);
		this.fpCamera.enabled = false;
		this.tpCamera.enabled = false;
		SceneryCamera.instance.camera.enabled = false;
		camera.cullingMask &= -786433;
		camera.clearFlags = CameraClearFlags.Depth;
		if (this.overrideCamera != null)
		{
			this.overrideCamera.enabled = false;
		}
		this.overrideCamera = camera;
		this.overrideCamera.enabled = true;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
	public void CancelOverrideCamera()
	{
		if (this.overrideCamera != null)
		{
			this.overrideCamera.enabled = false;
		}
		this.overrideCamera = null;
		if (this.firstPerson)
		{
			this.FirstPersonCamera();
			return;
		}
		this.ThirdPersonCamera();
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0000C4EE File Offset: 0x0000A6EE
	public void ThirdPersonCamera()
	{
		this.firstPerson = false;
		if (this.IsUsingOverrideCamera())
		{
			return;
		}
		this.fpCamera.enabled = false;
		this.tpCamera.enabled = true;
		this.EnableThirdPersonRenderingMode();
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0000C51E File Offset: 0x0000A71E
	public void FirstPersonCamera()
	{
		this.firstPerson = true;
		if (this.IsUsingOverrideCamera())
		{
			return;
		}
		this.fpCamera.enabled = true;
		this.tpCamera.enabled = false;
		this.EnableFirstPersonRenderingMode();
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00082E28 File Offset: 0x00081028
	public void EnableThirdPersonRenderingMode()
	{
		Renderer[] array = this.thirdpersonRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].shadowCastingMode = ShadowCastingMode.On;
		}
		this.actor.SetImposterShadowMode(ShadowCastingMode.On);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x00082E60 File Offset: 0x00081060
	public void EnableFirstPersonRenderingMode()
	{
		Renderer[] array = this.thirdpersonRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].shadowCastingMode = ShadowCastingMode.ShadowsOnly;
		}
		this.actor.SetImposterShadowMode(ShadowCastingMode.ShadowsOnly);
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x00082E98 File Offset: 0x00081098
	private void FixedUpdate()
	{
		if (!this.characterController.enabled || this.characterController.isGrounded || this.actor.fallenOver || this.actor.dead || this.actor.IsSeated() || this.actor.immersedInWater)
		{
			this.hasNotBeenGroundedAction.Start();
		}
		if (!this.actor.parachuteDeployed && this.hasNotBeenGroundedAction.TrueDone() && !this.actor.fallenOver && this.actor.Velocity().y < 0f)
		{
			this.actor.balance -= 200f * Time.deltaTime;
			this.actor.OnBalanceSetManually();
		}
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x0000C54E File Offset: 0x0000A74E
	public bool IsCursorFree()
	{
		return this.unlockCursorRavenscriptOverride || IngameMenuUi.IsOpen() || LoadoutUi.IsOpen() || StrategyUi.IsOpen() || ConfigFlagsUi.IsOpen() || ScoreboardUi.IsOpenAndFocused();
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x0000C57B File Offset: 0x0000A77B
	private bool IsMouseLookEnabled()
	{
		return this.allowMouseLookRavenscriptOverride && this.faceKillerAction.TrueDone() && (!this.mouseViewLocked || (this.allowMouseViewWhileAiming && this.actor.IsAiming()));
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00082F68 File Offset: 0x00081168
	private void Update()
	{
		bool flag = this.inputEnabled && Time.timeScale > 0f;
		if (flag)
		{
			this.UpdateToggledInputs();
		}
		if (flag && SteelInput.GetButtonDown(SteelInput.KeyBinds.AutoHover))
		{
			this.helicopterAutoHoverEnabled = !this.helicopterAutoHoverEnabled;
		}
		if (this.controller.playFootStepAudio)
		{
			this.controller.playFootStepAudio = false;
			if (this.actor.feetAreInWater)
			{
				this.controller.m_AudioSource.pitch = 0.5f;
				this.controller.m_AudioSource.volume = 0.5f;
				this.controller.m_AudioSource.PlayOneShot(FootstepAudio.GetWaterClip());
			}
			else if (this.actor.stance == Actor.Stance.Prone)
			{
				this.controller.m_AudioSource.pitch = 0.5f;
				this.controller.m_AudioSource.volume = 0.15f;
				this.controller.m_AudioSource.PlayOneShot(FootstepAudio.GetOutdoorClip());
			}
			else
			{
				this.controller.m_AudioSource.pitch = 1f;
				this.controller.m_AudioSource.volume = 0.4f;
				this.controller.m_AudioSource.PlayOneShot(this.controller.onTerrain ? FootstepAudio.GetOutdoorClip() : FootstepAudio.GetIndoorClip());
			}
		}
		if (!Benchmark.isRunning && this.disallowTogglePhotoModeAction.TrueDone() && this.TogglePauseMenuInput() && (!GameManager.IsPlayingCampaign() || !GameManager.gameOver))
		{
			if (IngameMenuUi.instance.canvas.enabled)
			{
				IngameMenuUi.Hide();
			}
			else
			{
				this.DisablePhotoMode();
				IngameMenuUi.Show();
			}
		}
		if (this.actor.dead && this.faceKillerAction.TrueDone() && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
		{
			this.attractMode = true;
		}
		CapturePoint currentCapturePoint = this.actor.GetCurrentCapturePoint();
		if (!this.actor.dead && currentCapturePoint != null)
		{
			IngameUi.instance.ShowFlagIndicator();
			IngameUi.instance.SetFlagIndicator(currentCapturePoint.control, currentCapturePoint.owner, currentCapturePoint.pendingOwner);
		}
		else
		{
			IngameUi.instance.HideFlagIndicator();
		}
		if (!this.actor.dead)
		{
			this.controller.m_RunSpeed = Mathf.Lerp(6.5f, 8f, this.actor.GetBonusSprintAmount());
		}
		Vector3 zero = Vector3.zero;
		this.controller.canJump = (this.actor.stance != Actor.Stance.Prone);
		if (this.actor.stance == Actor.Stance.Stand)
		{
			this.controller.m_WalkSpeed = 3.7f * this.actor.speedMultiplier;
		}
		else if (this.actor.stance == Actor.Stance.Crouch)
		{
			this.controller.m_WalkSpeed = 2.5f * this.actor.speedMultiplier;
		}
		else
		{
			this.controller.m_WalkSpeed = 1.5f * this.actor.speedMultiplier;
			zero.z += -0.6f;
		}
		this.controller.m_RunSpeed = 6.5f * this.actor.speedMultiplier;
		this.actor.animator.transform.localPosition = Vector3.MoveTowards(this.actor.animator.transform.localPosition, zero, 2f * Time.deltaTime);
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Crouch) && this.actor.stance == Actor.Stance.Prone)
		{
			this.proneInput.Reset();
		}
		if (this.HoldingSprint() && this.sprintInput.justToggled)
		{
			this.crouchInput.Reset();
		}
		if (this.playerSquad != null)
		{
			this.playerSquad.Update();
		}
		this.lockCursor = !this.IsCursorFree();
		bool flag2 = this.lockCursor && this.IsMouseLookEnabled();
		this.controller.SetMouseEnabled(flag2);
		this.controller.inWater = this.actor.immersedInWater;
		this.controller.waterHeight = this.actor.waterHeight;
		if (this.lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		bool flag3 = this.actor.IsAiming();
		if (this.actor.stance != Actor.Stance.Prone || SteelInput.GetButton(SteelInput.KeyBinds.Fire) || flag3 || this.IsReloading() || this.WeaponIsOnCooldown())
		{
			this.fpParent.proneCrawlAction.Start();
		}
		if (this.mouseViewLocked && !flag2)
		{
			this.fpCameraRoot.transform.localRotation = Quaternion.RotateTowards(this.fpCameraRoot.transform.localRotation, Quaternion.identity, Time.deltaTime * 400f);
		}
		this.fpLightIntensity.isInShadow = !GameManager.IsInSunlight(this.fpCamera.transform.position + this.fpCamera.transform.forward * 0.5f);
		this.controller.sprinting = this.IsSprinting();
		if (this.IsSprinting())
		{
			this.sprintCannotFireAction.Start();
		}
		this.fpParent.lean = Mathf.MoveTowards(this.fpParent.lean, this.Lean(), Time.deltaTime * 8f);
		if (this.actor.activeWeapon != null)
		{
			Weapon activeSubWeapon = this.actor.activeWeapon.GetActiveSubWeapon();
			if (this.actor.HasUnholsteredWeapon() && activeSubWeapon.ShouldUseFineAim())
			{
				float fov = activeSubWeapon.GetAimFov();
				if (activeSubWeapon.renderTextureCamera != null)
				{
					fov = activeSubWeapon.renderTextureCamera.fieldOfView * 2f;
				}
				float num = FpsActorController.GetSensitivityMultiplierFromFov(fov);
				if (activeSubWeapon.aiming)
				{
					num *= activeSubWeapon.configuration.aimSensitivityMultiplier;
				}
				this.SetMouseSensitivity(4f * num * Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity));
			}
			else
			{
				this.SetMouseSensitivity(4f * Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity));
			}
		}
		else
		{
			this.SetMouseSensitivity(4f * Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity));
		}
		if (flag3)
		{
			this.fpParent.Aim();
		}
		else
		{
			this.fpParent.StopAim();
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Fire) && GameModeBase.instance.allowDefaultRespawn && this.actor.dead && this.readyToRespawnCooldownAction.TrueDone())
		{
			this.hasAcceptedLoadoutAfterDeath = true;
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.OpenLoadout))
		{
			if (LoadoutUi.IsOpen())
			{
				this.CloseLoadout();
			}
			else
			{
				this.OpenLoadout();
			}
		}
		if (!IngameMenuUi.IsOpen() && !this.actor.dead)
		{
			if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Goggles))
			{
				if (this.goggles != null)
				{
					this.goggles.Toggle(false);
				}
				else
				{
					GameManager.ToggleNightVision();
				}
			}
			if (SteelInput.GetButtonDown(SteelInput.KeyBinds.SquadLeaderKit))
			{
				if (StrategyUi.IsOpen())
				{
					StrategyUi.Hide();
				}
				else if (!LoadoutUi.IsOpen())
				{
					StrategyUi.Show();
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.F10))
		{
			ActorManager.instance.debug = !ActorManager.instance.debug;
		}
		if (Input.GetKeyDown(KeyCode.F8) && !IngameMenuUi.IsOpen())
		{
			this.TogglePhotoMode();
		}
		if (!this.actor.IsSeated() && !this.actor.IsOnLadder())
		{
			this.cameraHeight = Mathf.MoveTowards(this.cameraHeight, this.targetCameraHeight, 4f * Time.deltaTime);
			this.fpCameraRoot.localPosition = new Vector3(0f, this.cameraHeight, 0f);
		}
		if (!Benchmark.isRunning && !GameManager.IsPlayingCampaign() && SteelInput.GetButtonDown(SteelInput.KeyBinds.Slowmotion) && !IngameMenuUi.IsOpen() && !this.inPhotoMode)
		{
			if (Time.timeScale < 1f)
			{
				Time.timeScale = 1f;
			}
			else
			{
				Time.timeScale = 0.2f;
			}
			Time.fixedDeltaTime = Time.timeScale / 60f;
			this.mixer.SetFloat("pitch", Time.timeScale);
		}
		if (flag)
		{
			this.UpdateInput();
		}
		Camera activeCamera = this.GetActiveCamera();
		if (activeCamera != null)
		{
			this.lqDistance = 12000f / activeCamera.fieldOfView;
			this.activeCameraWorldToLocalMatrix = activeCamera.transform.worldToLocalMatrix;
			this.activeCameraLocalToWorldMatrix = activeCamera.transform.localToWorldMatrix;
		}
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
	public bool TogglePauseMenuInput()
	{
		return Input.GetKeyDown(KeyCode.Escape) || SteelInput.GetButtonDown(SteelInput.KeyBinds.TogglePauseMenu);
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0000C5C7 File Offset: 0x0000A7C7
	private void SetMouseSensitivity(float sensitivity)
	{
		this.mouseSensitivity = sensitivity;
		this.controller.SetMouseSensitivity(sensitivity);
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000837A4 File Offset: 0x000819A4
	private void UpdateVictoryAnimation()
	{
		float t = Mathf.Pow(this.victoryCameraAnimationAction.Ratio(), 2f);
		float t2 = Mathf.SmoothStep(0f, 1f, t);
		Vector3 a = this.VictoryCameraOriginPosition();
		Vector3 vector = Vector3.Lerp(a, this.victoryCameraTargetPosition, t2);
		Vector3 a2 = Vector3.Lerp(a + this.victoryCameraDeltaOriginLook, this.victoryCameraTargetLook, t2);
		this.tpCamera.transform.position = vector;
		Quaternion rotation = Quaternion.LookRotation(a2 - vector);
		this.tpCamera.transform.rotation = rotation;
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00083834 File Offset: 0x00081A34
	private void UpdateAttractMode()
	{
		if (this.attractModeShotAction.TrueDone())
		{
			this.NewAttractModeShot();
		}
		if (this.attractModeTarget != null && this.attractModeTarget.dead)
		{
			this.attractModeTarget = null;
			this.attractModeShotAction.StartLifetime(Mathf.Min(this.attractModeShotAction.Remaining(), 3f));
		}
		if (this.attractModeTarget != null)
		{
			this.targetAttractRotation = Quaternion.LookRotation(this.attractModeTarget.Position() - this.attractModePosition + new Vector3(0f, 3f, 0f));
			this.targetAttractPosition = this.attractModeTarget.Position() + this.attractModeOffset;
		}
		this.attractModePosition = Vector3.Lerp(this.attractModePosition, this.targetAttractPosition, Time.deltaTime * 0.5f);
		this.attractModeRotation = Quaternion.Slerp(this.attractModeRotation, this.targetAttractRotation, Time.deltaTime * 1f);
		this.tpCamera.transform.position = this.attractModePosition;
		this.tpCamera.transform.rotation = this.attractModeRotation;
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x0008396C File Offset: 0x00081B6C
	private void NewAttractModeShot()
	{
		List<Actor> list = ActorManager.AliveActorsOnTeam(UnityEngine.Random.Range(0, 2));
		if (list.Count > 0)
		{
			int num = UnityEngine.Random.Range(0, list.Count);
			this.attractModeTarget = list[num];
			for (int i = 0; i < list.Count; i++)
			{
				int index = (num + i) % list.Count;
				if (list[index].aiControlled && ((AiActorController)list[index].controller).HasSpottedTarget())
				{
					this.attractModeTarget = list[index];
					break;
				}
			}
			if (this.attractModeTarget == null)
			{
				return;
			}
			this.attractModeOffset = new Vector3(UnityEngine.Random.Range(-2f, 2f), 5f, -8f);
			this.attractModeShotAction.StartLifetime(UnityEngine.Random.Range(7f, 15f));
			this.attractModeRotation = Quaternion.LookRotation(this.attractModeTarget.controller.FacingDirection().ToGround());
			this.attractModePosition = Matrix4x4.TRS(this.attractModeTarget.CenterPosition(), this.attractModeRotation, Vector3.one).MultiplyPoint(this.attractModeOffset);
		}
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x0000C5DC File Offset: 0x0000A7DC
	private void LeaveCurrentSeat()
	{
		if (this.playerSquad.shouldFollow)
		{
			this.playerSquad.PlayerOrderExitVehicle(this.actor.seat.vehicle);
		}
		this.actor.LeaveSeat(false);
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x00083AA0 File Offset: 0x00081CA0
	private void UpdateToggledInputs()
	{
		this.crouchInput.Update();
		this.proneInput.Update();
		this.aimInput.Update();
		this.sprintInput.Update();
		if (this.sprintInput.useToggle && !this.IsMoving())
		{
			this.sprintInput.Reset();
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00083AFC File Offset: 0x00081CFC
	private void UpdateInput()
	{
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Weapon1))
		{
			this.actor.SwitchWeapon(0);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Weapon2))
		{
			this.actor.SwitchWeapon(1);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Weapon3))
		{
			this.actor.SwitchWeapon(2);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Weapon4))
		{
			this.actor.SwitchWeapon(3);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Weapon5))
		{
			this.actor.SwitchWeapon(4);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat1))
		{
			this.actor.SwitchSeat(0, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat2))
		{
			this.actor.SwitchSeat(1, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat3))
		{
			this.actor.SwitchSeat(2, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat4))
		{
			this.actor.SwitchSeat(3, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat5))
		{
			this.actor.SwitchSeat(4, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat6))
		{
			this.actor.SwitchSeat(5, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Seat7))
		{
			this.actor.SwitchSeat(6, true);
		}
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.NextWeapon))
		{
			this.actor.NextWeapon();
		}
		else if (SteelInput.GetButtonDown(SteelInput.KeyBinds.PreviousWeapon))
		{
			this.actor.PreviousWeapon();
		}
		if (SteelInput.GetButton(SteelInput.KeyBinds.Kick) && !this.actor.IsSeated() && !this.actor.IsSwimming() && this.kickCooldownAction.TrueDone() && !this.IsReloading() && this.actor.stance != Actor.Stance.Prone)
		{
			base.StartCoroutine(this.Kick());
		}
		bool input = this.aimInput.GetInput();
		if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Call))
		{
			this.StartPointSquadOrder(input ? SquadPointUi.SquadPointOrderBaseType.TargetOrder : SquadPointUi.SquadPointOrderBaseType.PointOrder);
		}
		if (!this.pointOrderCanceled)
		{
			if (SteelInput.GetButton(SteelInput.KeyBinds.Call))
			{
				if (this.squadKeyTapAction.TrueDone())
				{
					SquadPointUi.Show(this.pointOrderBaseType);
				}
				this.readyToIssueRegroupOrder = (GameManager.GetMainCamera().transform.forward.y < -0.7f);
				SquadPointUi.SetDefaultText(this.readyToIssueRegroupOrder ? "REGROUP" : "GO TO");
				if (this.pointOrderBaseType == SquadPointUi.SquadPointOrderBaseType.TargetOrder != input)
				{
					this.CancelPointSquadOrder();
				}
			}
			if (SteelInput.GetButtonUp(SteelInput.KeyBinds.Call) && !this.pointOrderCanceled)
			{
				this.IssueSquadPointOrder();
				SquadPointUi.Hide(true);
			}
		}
		if (this.actor.IsSeated() && SteelInput.GetButtonDown(SteelInput.KeyBinds.ThirdPersonToggle))
		{
			this.vehicleThirdPersonCamera = !this.vehicleThirdPersonCamera;
			if (this.vehicleThirdPersonCamera)
			{
				this.actor.seat.SwitchToThirdPersonCamera();
			}
			else
			{
				this.actor.seat.SwitchToDefaultCamera();
			}
			this.UpdateSeatedMixGroup();
		}
		if (SteelInput.GetButton(SteelInput.KeyBinds.Use))
		{
			if (!this.actor.IsSeated() && !this.actor.IsOnLadder())
			{
				if (this.actor.CanEnterSeat())
				{
					this.SampleUseRay();
					return;
				}
			}
			else if (this.cannotLeaveAction.TrueDone())
			{
				if (this.actor.IsSeated() && this.allowExitVehicle)
				{
					this.LeaveCurrentSeat();
					return;
				}
				if (this.actor.IsOnLadder())
				{
					this.actor.ExitLadder();
				}
			}
		}
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x0000C612 File Offset: 0x0000A812
	private void StartPointSquadOrder(SquadPointUi.SquadPointOrderBaseType type)
	{
		this.squadKeyTapAction.Start();
		this.pointOrderBaseType = type;
		this.pointOrderCanceled = false;
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0000C62D File Offset: 0x0000A82D
	private void CancelPointSquadOrder()
	{
		SquadPointUi.Hide(false);
		SquadPointUi.PlayCancelOrderUi();
		this.pointOrderCanceled = true;
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00083E14 File Offset: 0x00082014
	private void TogglePhotoMode()
	{
		if (!this.disallowTogglePhotoModeAction.TrueDone() || GameManager.IsSpectating())
		{
			return;
		}
		if (SpectatorCamera.instance == null)
		{
			UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.spectatorCameraPrefab, this.GetActiveCamera().transform.position, this.GetActiveCamera().transform.rotation);
			this.EnablePhotoMode();
			return;
		}
		if (!SpectatorCamera.instance.gameObject.activeInHierarchy)
		{
			SpectatorCamera.instance.gameObject.SetActive(true);
			this.EnablePhotoMode();
			return;
		}
		this.DisablePhotoMode();
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00083EA8 File Offset: 0x000820A8
	private void EnablePhotoMode()
	{
		if (this.inPhotoMode)
		{
			return;
		}
		this.disallowTogglePhotoModeAction.Start();
		if (this.actor.activeWeapon != null)
		{
			this.actor.activeWeapon.Hide();
		}
		this.EnableThirdPersonRenderingMode();
		this.photoModeOverridenCamera = this.GetActiveCamera();
		if (this.photoModeOverridenCamera != null)
		{
			SpectatorCamera.instance.transform.position = this.photoModeOverridenCamera.transform.position;
			Vector3 eulerAngles = this.photoModeOverridenCamera.transform.eulerAngles;
			eulerAngles.z = 0f;
			SpectatorCamera.instance.transform.eulerAngles = eulerAngles;
			this.photoModeOverridenCamera.enabled = false;
		}
		foreach (Actor actor in ActorManager.instance.actors)
		{
			actor.ForceApplyAnimatorForTime(0.3f);
		}
		GameManager.PauseGame();
		this.inPhotoMode = true;
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00083FBC File Offset: 0x000821BC
	private void DisablePhotoMode()
	{
		if (GameManager.IsSpectating() || !this.inPhotoMode)
		{
			return;
		}
		if (this.actor.activeWeapon != null)
		{
			this.actor.activeWeapon.Show();
		}
		if (this.firstPerson && !this.IsUsingOverrideCamera())
		{
			this.EnableFirstPersonRenderingMode();
		}
		if (this.photoModeOverridenCamera != null)
		{
			this.photoModeOverridenCamera.enabled = true;
		}
		if (SpectatorCamera.instance != null)
		{
			this.audioListenerAutoParent.ForceDetatch();
			SpectatorCamera.instance.gameObject.SetActive(false);
		}
		GameManager.UnpauseGame();
		this.inPhotoMode = false;
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00084060 File Offset: 0x00082260
	private bool TryGetOnLadder()
	{
		foreach (Ladder ladder in ActorManager.instance.ladders)
		{
			Vector3 closestPositionTo = ladder.GetClosestPositionTo(this.fpCamera.transform.position);
			Debug.DrawLine(closestPositionTo, this.fpCamera.transform.position, Color.red, 20f);
			if (Vector3.Distance(this.fpCamera.transform.position, closestPositionTo) < 3f)
			{
				this.actor.GetOnLadder(ladder);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00084118 File Offset: 0x00082318
	private void SampleUseRay()
	{
		if (this.TryGetOnLadder())
		{
			return;
		}
		Ray ray;
		if (this.actor.fallenOver)
		{
			ray = new Ray(this.actor.CenterPosition(), this.tpCamera.transform.forward + this.tpCamera.transform.up * 0.2f);
		}
		else
		{
			ray = new Ray(this.fpCamera.transform.position, this.fpCamera.transform.forward);
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 3f, 2048) && raycastHit.collider.gameObject.layer == 11)
		{
			Seat component = raycastHit.collider.GetComponent<Seat>();
			this.actor.EnterSeat(component, true);
			this.cannotLeaveAction.Start();
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x000841F4 File Offset: 0x000823F4
	private void LateUpdate()
	{
		Vector3 localEulerAngles = this.fpCameraRoot.localEulerAngles;
		float num = Mathf.DeltaAngle(0f, localEulerAngles.x);
		if (num < -87f)
		{
			localEulerAngles.x = -87f;
			this.fpCameraRoot.localEulerAngles = localEulerAngles;
		}
		else if (num > 87f)
		{
			localEulerAngles.x = 87f;
			this.fpCameraRoot.localEulerAngles = localEulerAngles;
		}
		if (Mathf.Abs(Mathf.DeltaAngle(localEulerAngles.z, 0f)) > 90f)
		{
			localEulerAngles.z = 0f;
			this.fpCameraRoot.localEulerAngles = localEulerAngles;
		}
		this.kickRenderer.enabled = (this.fpCamera.enabled && !this.kickAction.TrueDone());
		if (!this.faceKillerAction.TrueDone() && this.allowPlayerControlledTpCamera)
		{
			this.UpdateFaceKiller();
		}
		else
		{
			this.tpCamera.fieldOfView = 60f;
		}
		if (this.tpCamera.enabled)
		{
			this.UpdateThirdPersonCamera(false);
		}
		if (this.playingVictoryAnimation)
		{
			this.UpdateVictoryAnimation();
			return;
		}
		if (this.attractMode)
		{
			this.UpdateAttractMode();
		}
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0008431C File Offset: 0x0008251C
	private void UpdateFaceKiller()
	{
		Vector3 forward = this.killer.Position() - this.tpCamera.transform.position;
		forward.y += 1f;
		float b = Mathf.Min(60f, Mathf.Atan2(10f, forward.magnitude) * 57.29578f);
		float t = Mathf.Clamp01(6f * (this.faceKillerAction.Ratio() - 0.1f));
		float num = Mathf.Clamp01(1.1f * (this.faceKillerAction.Ratio() - 0.2f));
		this.fpCameraRoot.rotation = Quaternion.Lerp(this.faceKillerOrigin, Quaternion.LookRotation(forward), Mathf.SmoothStep(0f, 1f, t));
		this.tpCamera.fieldOfView = Mathf.Lerp(60f, b, Mathf.SmoothStep(0f, 1f, 2f * Mathf.Sin(num * 3.1415927f * 2f)));
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00084420 File Offset: 0x00082620
	private void UpdateThirdPersonCamera(bool forceUseActorPosition = false)
	{
		if (this.allowPlayerControlledTpCamera)
		{
			this.tpCamera.transform.rotation = this.fpCamera.transform.rotation;
			if (!this.actor.dead || forceUseActorPosition)
			{
				Vector3 vector = -this.tpCamera.transform.forward * 3f;
				Ray ray = new Ray(this.actor.CenterPosition() + Vector3.up * 0.5f, vector);
				Debug.DrawRay(ray.origin, Vector3.up, Color.red, 0.1f);
				RaycastHit raycastHit;
				if (Physics.SphereCast(ray, 0.3f, out raycastHit, vector.magnitude, 4097))
				{
					this.tpCamera.transform.position = raycastHit.point + raycastHit.normal * 0.15f;
					return;
				}
				this.tpCamera.transform.position = ray.origin + vector;
			}
		}
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00084538 File Offset: 0x00082738
	public override SpawnPoint SelectedSpawnPoint()
	{
		if (GameManager.IsSpectating() || !LoadoutUi.HasBeenOpen() || !this.hasAcceptedLoadoutAfterDeath)
		{
			return null;
		}
		SpawnPoint spawnPoint = MinimapUi.SelectedSpawnPoint();
		if (spawnPoint == null || spawnPoint.owner != this.actor.team)
		{
			return null;
		}
		return spawnPoint;
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x0000C641 File Offset: 0x0000A841
	public override Transform WeaponParent()
	{
		return this.weaponParent;
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x0000C649 File Offset: 0x0000A849
	public override Transform TpWeaponParent()
	{
		return this.tpWeaponParent;
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00084584 File Offset: 0x00082784
	public override void SwitchedToWeapon(Weapon weapon)
	{
		this.SetupWeaponFov(weapon);
		if (weapon == null)
		{
			return;
		}
		if (KeyboardGlyphGenerator.IsHidden() && Options.GetToggle(OptionToggle.Id.ControlHints))
		{
			if (weapon.targetTracker != null && FpsActorController.IsLaserGuidedTargetTracker(weapon.targetTracker) && FpsActorController.controlHints.laserGuidedTarget.TryConsume())
			{
				KeyboardGlyphGenerator.instance.ShowCombinationBind(SteelInput.KeyBinds.Aim, SteelInput.KeyBinds.AimX, "Move Missile Target");
				return;
			}
			if (weapon.HasAnySubWeapons() && FpsActorController.controlHints.fireMode.TryConsume())
			{
				KeyboardGlyphGenerator.instance.ShowBind(SteelInput.KeyBinds.FireMode, "Change Fire Mode");
				return;
			}
			if (weapon.HasAnyAltSightModes() && FpsActorController.controlHints.aimMode.TryConsume())
			{
				KeyboardGlyphGenerator.instance.ShowCompoundBind(SteelInput.KeyBinds.PreviousScope, SteelInput.KeyBinds.NextScope, "Change Scope");
			}
		}
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x0000C651 File Offset: 0x0000A851
	private static bool IsLaserGuidedTargetTracker(TargetTracker targetTracker)
	{
		return targetTracker.requireAim && targetTracker.usePointTarget;
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0000C663 File Offset: 0x0000A863
	public override void HolsteredActiveWeapon()
	{
		this.SetupWeaponFov(null);
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0000C66C File Offset: 0x0000A86C
	public override void ChangeAimFieldOfView(float fov)
	{
		this.fpParent.SetAimFov(fov, false);
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x0000C67B File Offset: 0x0000A87B
	public void SetupWeaponFov(Weapon weapon)
	{
		if (weapon != null)
		{
			this.fpParent.SetAimFov(weapon.GetActiveSubWeapon().GetAimFov(), true);
			return;
		}
		this.fpParent.SetAimFov(45f, true);
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x0000C6AF File Offset: 0x0000A8AF
	public override WeaponManager.LoadoutSet GetLoadout()
	{
		return LoadoutUi.instance.loadout;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x0000C6BB File Offset: 0x0000A8BB
	public override bool Crouch()
	{
		return this.crouchInput.GetInput();
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
	public override bool Prone()
	{
		return this.proneInput.GetInput();
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x0000C6D5 File Offset: 0x0000A8D5
	public override bool Jump()
	{
		return SteelInput.GetButton(SteelInput.KeyBinds.Jump);
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x0000C6DE File Offset: 0x0000A8DE
	private void SetCharacterControllerHeight(float height, float cameraHeight)
	{
		this.characterController.height = height;
		this.characterController.center = new Vector3(0f, height / 2f, 0f);
		this.targetCameraHeight = cameraHeight + -0.17f;
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00084650 File Offset: 0x00082850
	public override bool ChangeStance(Actor.Stance stance)
	{
		float stanceHeight = this.GetStanceHeight(stance);
		float stanceHeight2 = this.GetStanceHeight(this.actor.stance);
		if (stanceHeight < stanceHeight2)
		{
			if (stance == Actor.Stance.Prone)
			{
				this.SetCharacterControllerHeight(stanceHeight, 0.6f);
			}
			else
			{
				this.SetCharacterControllerHeight(stanceHeight, stanceHeight);
			}
			this.OnStanceChanged(stance);
			return true;
		}
		if (!Physics.SphereCast(new Ray(this.actor.Position(), Vector3.up), 0.4f, stanceHeight - 0.35999998f, 4097))
		{
			if (stance == Actor.Stance.Prone)
			{
				this.SetCharacterControllerHeight(stanceHeight, 0.6f);
			}
			else
			{
				this.SetCharacterControllerHeight(stanceHeight, stanceHeight);
			}
			this.OnStanceChanged(stance);
			return true;
		}
		return false;
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x000846F0 File Offset: 0x000828F0
	public override void ForceChangeStance(Actor.Stance stance)
	{
		float stanceHeight = this.GetStanceHeight(stance);
		if (stance == Actor.Stance.Prone)
		{
			this.SetCharacterControllerHeight(stanceHeight, 0.6f);
		}
		else
		{
			this.SetCharacterControllerHeight(stanceHeight, stanceHeight);
		}
		this.OnStanceChanged(stance);
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00084728 File Offset: 0x00082928
	private void OnStanceChanged(Actor.Stance stance)
	{
		if (!this.sprintInput.justToggled)
		{
			this.sprintInput.Reset();
		}
		if (stance == Actor.Stance.Prone)
		{
			this.controller.SetMouseVerticalPadding(50f);
			this.crouchInput.Reset();
			return;
		}
		if (stance == Actor.Stance.Crouch)
		{
			this.controller.SetMouseVerticalPadding(3f);
			this.proneInput.Reset();
			return;
		}
		this.controller.SetMouseVerticalPadding(3f);
		this.proneInput.Reset();
		this.crouchInput.Reset();
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x0000C71A File Offset: 0x0000A91A
	private float GetStanceHeight(Actor.Stance stance)
	{
		if (stance == Actor.Stance.Crouch)
		{
			return 1.1f;
		}
		if (stance == Actor.Stance.Prone)
		{
			return 0.8f;
		}
		return 1.7f;
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x0000C735 File Offset: 0x0000A935
	private void ForceStandStance()
	{
		this.SetCharacterControllerHeight(1.7f, 1.7f);
		this.crouchInput.Reset();
		this.proneInput.Reset();
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool IsGroupedUp()
	{
		return false;
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00004694 File Offset: 0x00002894
	private bool IsReloading()
	{
		return this.actor.HasUnholsteredWeapon() && this.actor.activeWeapon.reloading;
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x0000C75D File Offset: 0x0000A95D
	private bool WeaponIsOnCooldown()
	{
		return this.actor.HasUnholsteredWeapon() && this.actor.activeWeapon.CoolingDown();
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0000C77E File Offset: 0x0000A97E
	public override bool IsSprinting()
	{
		return this.actor.stance == Actor.Stance.Stand && !this.Aiming() && this.HoldingSprint() && !this.actor.IsSeated() && !this.IsKicking();
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
	public override bool UseSprintingAnimation()
	{
		return this.IsSprinting();
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x0000C7BD File Offset: 0x0000A9BD
	public override bool HoldingSprint()
	{
		return this.sprintInput.GetInput();
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0000C7CA File Offset: 0x0000A9CA
	public void DisableCameras()
	{
		this.CancelOverrideCamera();
		this.fpCamera.enabled = false;
		this.tpCamera.enabled = false;
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0000C7EA File Offset: 0x0000A9EA
	public void EnableCameras()
	{
		this.FirstPersonCamera();
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0000C7F2 File Offset: 0x0000A9F2
	public void BulletFlyby(Vector3 position, float pitch)
	{
		this.bulletFlybySoundbank.transform.position = position;
		this.bulletFlybySoundbank.audioSource.pitch = pitch;
		this.bulletFlybySoundbank.PlayRandom();
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0000C821 File Offset: 0x0000AA21
	private bool IsKicking()
	{
		return !this.kickAction.TrueDone();
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0000C831 File Offset: 0x0000AA31
	private IEnumerator Kick()
	{
		this.kickCooldownAction.Start();
		this.kickAction.Start();
		this.kickAnimation.Stop();
		this.kickAnimation.Play();
		this.kickSound.Play();
		if (this.actor.activeWeapon != null)
		{
			this.actor.activeWeapon.animator.SetTrigger("kick");
		}
		PlayerFpParent.instance.KickCamera(Vector3.right * 3f);
		yield return new WaitForSeconds(0.15f);
		PlayerFpParent.instance.KickCamera(Vector3.left * 8f);
		yield return new WaitForSeconds(0.1f);
		RaycastHit raycastHit;
		if (!this.actor.fallenOver && !this.actor.dead && Physics.SphereCast(new Ray(this.fpCamera.transform.position, this.fpCamera.transform.forward), 0.6f, out raycastHit, 2.4f, 16848129))
		{
			this.kickSound.PlayOneShot(this.kickHitSound);
			if (raycastHit.collider.gameObject.layer == 24)
			{
				raycastHit.collider.gameObject.GetComponentInParent<KickActivator>().Trigger();
			}
			if (Hitbox.IsHitboxLayer(raycastHit.collider.gameObject.layer))
			{
				Hitbox component = raycastHit.collider.GetComponent<Hitbox>();
				Actor actor = component.parent as Actor;
				if (actor != null && actor.dead)
				{
					Rigidbody attachedRigidbody = raycastHit.collider.attachedRigidbody;
					if (attachedRigidbody != null)
					{
						attachedRigidbody.AddForceAtPosition(this.fpCamera.transform.forward * 300f, raycastHit.point, ForceMode.Impulse);
					}
					if (actor.CanSpawnAmmoReserve())
					{
						ActorManager.SpawnAmmoReserveOnActor(actor);
					}
				}
				if (component.parent != this.actor)
				{
					DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Melee, this.actor, null)
					{
						healthDamage = 30f,
						balanceDamage = 120f,
						point = raycastHit.point,
						direction = this.fpCamera.transform.forward,
						impactForce = this.fpCamera.transform.forward * 300f
					};
					component.parent.Damage(info);
				}
			}
			else
			{
				Rigidbody attachedRigidbody2 = raycastHit.collider.attachedRigidbody;
				if (attachedRigidbody2 != null)
				{
					attachedRigidbody2.AddForceAtPosition(this.fpCamera.transform.forward * 300f, raycastHit.point, ForceMode.Impulse);
				}
			}
		}
		yield break;
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x0000C840 File Offset: 0x0000AA40
	public void SquadOrderRegroup()
	{
		this.playerSquad.Regroup();
		StrategyUi.HideSquadOrderMarker();
		this.PlayCallAnimation();
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x0000C858 File Offset: 0x0000AA58
	public void SquadOrderEnterVehicle(Vehicle vehicle)
	{
		if (this.playerSquad.PlayerOrderEnterVehicle(vehicle))
		{
			this.playerSquad.PlayerSquadTakeOverSquadVehicle(vehicle);
		}
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0000C874 File Offset: 0x0000AA74
	public void SquadOrderCommandeerVehicle(Vehicle vehicle)
	{
		if (vehicle.claimingSquad == null)
		{
			this.SquadOrderEnterVehicle(vehicle);
			return;
		}
		if (vehicle.canBeTakenOverByPlayerSquad)
		{
			vehicle.CommandeerBySquad(this.playerSquad);
		}
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x0000C89A File Offset: 0x0000AA9A
	public void SquadOrderExitVehicle(Vehicle vehicle)
	{
		this.playerSquad.PlayerOrderExitVehicle(vehicle);
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
	public void SquadOrderAttack(SpawnPoint point)
	{
		this.playerSquad.Ungroup();
		this.playerSquad.PlayerOrderMoveTo(point.transform.position);
		StrategyUi.SetSquadOrderMarker(point.transform.position, StrategyUi.MarkerType.Attack);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0000C8DC File Offset: 0x0000AADC
	public void SquadOrderDefend(SpawnPoint point)
	{
		this.playerSquad.Ungroup();
		this.playerSquad.PlayerOrderMoveTo(point.transform.position);
		StrategyUi.SetSquadOrderMarker(point.transform.position, StrategyUi.MarkerType.Defend);
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0000C910 File Offset: 0x0000AB10
	public void SquadOrderGoto(Vector3 point)
	{
		this.playerSquad.Ungroup();
		this.playerSquad.PlayerOrderMoveTo(point);
		StrategyUi.SetSquadOrderMarker(point, StrategyUi.MarkerType.Goto);
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000847B4 File Offset: 0x000829B4
	private void IssueSquadPointOrder()
	{
		if (this.squadKeyTapAction.TrueDone())
		{
			SquadPointUi.PlayCommitOrder();
		}
		else
		{
			SquadPointUi.PlayCommitOrderQuick();
		}
		if (this.pointOrderBaseType != SquadPointUi.SquadPointOrderBaseType.PointOrder)
		{
			if (this.pointOrderBaseType == SquadPointUi.SquadPointOrderBaseType.TargetOrder)
			{
				Actor attackTarget = SquadPointUi.GetAttackTarget();
				if (attackTarget != null)
				{
					if (this.playerSquad.members.Count > 1)
					{
						this.playerSquad.attackTarget = attackTarget;
						return;
					}
					attackTarget.Highlight(4f);
					attackTarget.MarkHighPriorityTarget(60f);
				}
			}
			return;
		}
		SquadOrderPoint aimingAtPoint = SquadPointUi.GetAimingAtPoint();
		if (aimingAtPoint != null && this.squadKeyTapAction.TrueDone())
		{
			this.IssuePointOrder(aimingAtPoint);
			return;
		}
		if (this.CallToSquad())
		{
			this.PlayCallAnimation();
			return;
		}
		if (this.readyToIssueRegroupOrder)
		{
			this.SquadOrderRegroup();
			return;
		}
		this.IssueSquadRaycastOrder();
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x0008487C File Offset: 0x00082A7C
	private void IssuePointOrder(SquadOrderPoint point)
	{
		if (point.HasOnIssueDelegate())
		{
			point.OnIssue();
			return;
		}
		if (point.type == SquadOrderPoint.ObjectiveType.Attack)
		{
			this.SquadOrderAttack(point.GetComponentInParent<SpawnPoint>());
			this.PlayDirectAnimation();
			return;
		}
		if (point.type == SquadOrderPoint.ObjectiveType.Defend)
		{
			this.SquadOrderDefend(point.GetComponentInParent<SpawnPoint>());
			this.PlayDirectAnimation();
			return;
		}
		if (point.type == SquadOrderPoint.ObjectiveType.Enter)
		{
			this.SquadOrderEnterVehicle(point.GetComponentInParent<Vehicle>());
			this.PlayDirectAnimation();
			return;
		}
		if (point.type == SquadOrderPoint.ObjectiveType.Commandeer)
		{
			this.SquadOrderCommandeerVehicle(point.GetComponentInParent<Vehicle>());
			this.PlayCallAnimation();
			return;
		}
		this.SquadOrderExitVehicle(point.GetComponentInParent<Vehicle>());
		this.PlayCallAnimation();
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00084920 File Offset: 0x00082B20
	private void IssueSquadRaycastOrder()
	{
		Ray ray = new Ray(GameManager.GetMainCamera().transform.position, GameManager.GetMainCamera().transform.forward);
		RaycastHit raycastHit;
		bool flag = WaterLevel.Raycast(ray, out raycastHit);
		RaycastHit raycastHit2;
		bool flag2 = Physics.Raycast(ray, out raycastHit2, 999999f, 1);
		if (flag && (!flag2 || raycastHit.distance < raycastHit2.distance))
		{
			raycastHit2 = raycastHit;
		}
		if (flag2)
		{
			this.PlayDirectAnimation();
			this.SquadOrderGoto(raycastHit2.point);
		}
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00084998 File Offset: 0x00082B98
	private bool CallToSquad()
	{
		List<Actor> list = ActorManager.AliveActorsOnTeam(this.actor.team);
		float num = 60f;
		Actor actor = null;
		Camera mainCamera = GameManager.GetMainCamera();
		foreach (Actor actor2 in list)
		{
			if (!(actor2 == this.actor) && !this.playerSquad.members.Contains(actor2.controller))
			{
				AiActorController aiActorController = actor2.controller as AiActorController;
				if (!(aiActorController != null) || aiActorController.modifier.canJoinPlayerSquad)
				{
					float num2 = SMath.LineVsPointClosestT(mainCamera.transform.position, mainCamera.transform.forward, actor2.CenterPosition());
					if (Vector3.Distance(mainCamera.transform.position + mainCamera.transform.forward * num2, actor2.CenterPosition()) < 2f && num2 > 0.5f && num2 < num && !Physics.Linecast(mainCamera.transform.position, actor2.CenterPosition(), 1))
					{
						num = num2;
						actor = actor2;
					}
				}
			}
		}
		if (actor != null)
		{
			actor.controller.ChangeToSquad(this.playerSquad);
		}
		return actor != null;
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0000C930 File Offset: 0x0000AB30
	private void PlayCallAnimation()
	{
		base.StartCoroutine(this.PlayCallAnimationCoroutine());
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0000C93F File Offset: 0x0000AB3F
	private void PlayDirectAnimation()
	{
		base.StartCoroutine(this.PlayDirectAnimationCoroutine());
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0000C94E File Offset: 0x0000AB4E
	private IEnumerator PlayCallAnimationCoroutine()
	{
		if (this.actor.activeWeapon != null && this.actor.activeWeapon.animator != null)
		{
			this.actor.activeWeapon.animator.SetTrigger("call");
			yield return new WaitForSeconds(0.2f);
			this.actor.activeWeapon.animator.ResetTrigger("call");
		}
		yield break;
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x0000C95D File Offset: 0x0000AB5D
	private IEnumerator PlayDirectAnimationCoroutine()
	{
		if (this.actor.activeWeapon != null && this.actor.activeWeapon.animator != null)
		{
			this.actor.activeWeapon.animator.SetTrigger("direct");
			yield return new WaitForSeconds(0.2f);
			this.actor.activeWeapon.animator.ResetTrigger("direct");
		}
		yield break;
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x0000C96C File Offset: 0x0000AB6C
	public override Vector2 AimInput()
	{
		return new Vector2(SteelInput.GetAxis(SteelInput.KeyBinds.AimX), SteelInput.GetAxis(SteelInput.KeyBinds.AimY));
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x0000C981 File Offset: 0x0000AB81
	public override float RangeInput()
	{
		return Input.mouseScrollDelta.y * 5f;
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0000C993 File Offset: 0x0000AB93
	public void ScopeBlackout()
	{
		this.blackoutAction.Start();
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
	public override bool Countermeasures()
	{
		return SteelInput.GetButtonDown(SteelInput.KeyBinds.Countermeasures);
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x0000C9A9 File Offset: 0x0000ABA9
	public override float LadderInput()
	{
		return SteelInput.GetAxis(SteelInput.KeyBinds.Vertical);
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x0000C6D5 File Offset: 0x0000A8D5
	public override bool DeployParachute()
	{
		return SteelInput.GetButton(SteelInput.KeyBinds.Jump);
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x0000C9B1 File Offset: 0x0000ABB1
	public override void DisableMovement()
	{
		this.controller.movementEnabled = false;
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x0000C9BF File Offset: 0x0000ABBF
	public override void EnableMovement()
	{
		this.controller.movementEnabled = true;
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x0000C9CD File Offset: 0x0000ABCD
	public override void Move(Vector3 movement)
	{
		this.characterController.Move(movement);
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x0000C9DC File Offset: 0x0000ABDC
	public override bool IsMovementEnabled()
	{
		return this.controller.movementEnabled;
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0000C9E9 File Offset: 0x0000ABE9
	public override bool IsMoving()
	{
		return !GameManager.gameOver && (SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal) != 0f || SteelInput.GetAxis(SteelInput.KeyBinds.Vertical) != 0f);
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x0000CA13 File Offset: 0x0000AC13
	public override Vector2 ParachuteInput()
	{
		return new Vector2(SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal), SteelInput.GetAxis(SteelInput.KeyBinds.Vertical));
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool UseEyeMuzzle()
	{
		return false;
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00084B04 File Offset: 0x00082D04
	private void OnGUI()
	{
		if (!this.blackoutAction.TrueDone())
		{
			Color black = Color.black;
			black.a = Mathf.Clamp01(1f - this.blackoutAction.Ratio());
			GUI.color = black;
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.blackoutTexture);
		}
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void OnCancelParachute()
	{
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsOnPlayerSquad()
	{
		return true;
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0000CA26 File Offset: 0x0000AC26
	public static float GetSensitivityMultiplierFromFov(float fov)
	{
		return Mathf.Clamp01(fov / 60f);
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0000CA34 File Offset: 0x0000AC34
	public override bool SwitchFireMode()
	{
		return SteelInput.GetButtonDown(SteelInput.KeyBinds.FireMode);
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0000CA3D File Offset: 0x0000AC3D
	public override bool NextSightMode()
	{
		return SteelInput.GetButtonDown(SteelInput.KeyBinds.NextScope) || (SteelInput.GetButton(SteelInput.KeyBinds.ScopeModifier) && Input.mouseScrollDelta.y > 0f);
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x0000CA66 File Offset: 0x0000AC66
	public override bool PreviousSightMode()
	{
		return SteelInput.GetButtonDown(SteelInput.KeyBinds.PreviousScope) || (SteelInput.GetButton(SteelInput.KeyBinds.ScopeModifier) && Input.mouseScrollDelta.y < 0f);
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x00084B70 File Offset: 0x00082D70
	public void PlayVictoryCameraAnimation()
	{
		FpsActorController.TakeThirdPersonCameraControl();
		this.CancelOverrideCamera();
		this.ThirdPersonCamera();
		LoadoutUi.Hide(false);
		this.endMix.TransitionTo(2f);
		if (this.actor.IsSeated() && this.actor.seat.hud != null)
		{
			this.actor.seat.hud.SetActive(false);
		}
		EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 1f, Color.black);
		this.playingVictoryAnimation = true;
		this.inputEnabled = false;
		this.DisableMovement();
		MouseLook.paused = true;
		this.victoryCameraAnimationAction.Start();
		this.victoryCameraSequence.Start();
		IngameUi.instance.Hide();
		GameManager.instance.hudEnabled = false;
		List<Vector3> list = new List<Vector3>();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			list.Add(spawnPoint.transform.position);
		}
		if (ActorManager.instance.actors.Count > 20)
		{
			foreach (Actor actor in ActorManager.instance.actors)
			{
				list.Add(actor.Position());
			}
		}
		this.victoryCameraTargetLook = SMath.Median(list);
		Vector3 v = Vector3.Lerp(this.tpCamera.transform.forward, (this.victoryCameraTargetLook - this.actor.CenterPosition()).normalized, 0.5f);
		this.victoryCameraForwardFlat = v.ToGround().normalized;
		Vector3 a = this.actor.CenterPosition() + this.actor.transform.forward * 5f;
		this.victoryCameraTargetPosition = (a - this.victoryCameraTargetLook).ToGround().normalized * 120f + this.VictoryCameraOriginPosition();
		this.victoryCameraTargetPosition.y = this.VictoryCameraOriginPosition().y + 90f;
		this.victoryCameraDeltaOriginLook = a - this.VictoryCameraOriginPosition();
		base.StartCoroutine(this.VictoryAnimation(5f, 14f));
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0000CA8F File Offset: 0x0000AC8F
	private Vector3 VictoryCameraOriginPosition()
	{
		return this.actor.CenterPosition() - this.victoryCameraForwardFlat * 10f + new Vector3(0f, 5f, 0f);
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0000CACA File Offset: 0x0000ACCA
	private IEnumerator VictoryAnimation(float graphicsTime, float fadeOutTime)
	{
		yield return new WaitForSeconds(graphicsTime);
		VictoryUi.FadeInVictoryGraphics();
		this.zeroMix.TransitionTo(fadeOutTime - graphicsTime);
		yield return new WaitForSeconds(fadeOutTime - graphicsTime);
		EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 3f, Color.black);
		yield return new WaitForSeconds(4.5f);
		GameManager.OnGameEnded();
		yield break;
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0000CAE7 File Offset: 0x0000ACE7
	public static void TakeThirdPersonCameraControl()
	{
		FpsActorController.instance.allowPlayerControlledTpCamera = false;
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
	public static void ReleaseThirdPersonCameraControl()
	{
		FpsActorController.instance.allowPlayerControlledTpCamera = true;
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x0000CB01 File Offset: 0x0000AD01
	public float GetStepPhase()
	{
		return this.controller.StepCycle();
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0000CB0E File Offset: 0x0000AD0E
	public float GetStepSpeed()
	{
		return this.controller.StepSpeed();
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void OnAssignedToSquad(Squad squad)
	{
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void OnDroppedFromSquad()
	{
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool EnterCover(CoverPoint coverPoint)
	{
		return false;
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool FindCover()
	{
		return false;
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool FindCoverAtPoint(Vector3 point)
	{
		return false;
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool FindCoverAwayFrom(Vector3 point)
	{
		return false;
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool FindCoverTowards(Vector3 direction)
	{
		return false;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00002FD8 File Offset: 0x000011D8
	public override Actor GetTarget()
	{
		return null;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void FillDriverSeat()
	{
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool HasSpottedTarget()
	{
		return false;
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool IsTakingFire()
	{
		return false;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void LeaveCover()
	{
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void LookAt(Vector3 position)
	{
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool HasPath()
	{
		return true;
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsReadyToPickUpPassengers()
	{
		return true;
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool IdlePose()
	{
		return false;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0000CB1B File Offset: 0x0000AD1B
	public override Vector3 PathEndPoint()
	{
		return Vector3.zero;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void ChangeToSquad(Squad squad)
	{
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void StartClimbingSlope()
	{
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsAlert()
	{
		return true;
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0000CB22 File Offset: 0x0000AD22
	public override bool CurrentWaypoint(out Vector3 waypoint)
	{
		waypoint = Vector3.zero;
		return false;
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void MarkReachedWaypoint()
	{
	}

	// Token: 0x04000FD7 RID: 4055
	public static FpsActorController instance;

	// Token: 0x04000FD8 RID: 4056
	public static FpsActorController.ControlHints controlHints;

	// Token: 0x04000FD9 RID: 4057
	public const int BACKGROUND_LAYER = 19;

	// Token: 0x04000FDA RID: 4058
	public const int FIRST_PERSON_LAYER = 18;

	// Token: 0x04000FDB RID: 4059
	public const int PLAYER_LAYER = 9;

	// Token: 0x04000FDC RID: 4060
	public const float BASE_SENSITIVITY = 4f;

	// Token: 0x04000FDD RID: 4061
	private const int OVERRIDE_CAMERA_LAYER_CULLING_MASK = -786433;

	// Token: 0x04000FDE RID: 4062
	private const float KICK_TIME = 0.25f;

	// Token: 0x04000FDF RID: 4063
	private const float KICK_RADIUS = 0.6f;

	// Token: 0x04000FE0 RID: 4064
	private const float KICK_RANGE = 2.4f;

	// Token: 0x04000FE1 RID: 4065
	private const int KICK_MASK = 16848129;

	// Token: 0x04000FE2 RID: 4066
	private const float KICK_DAMAGE = 30f;

	// Token: 0x04000FE3 RID: 4067
	private const float KICK_BALANCE_DAMAGE = 120f;

	// Token: 0x04000FE4 RID: 4068
	private const float KICK_FORCE = 300f;

	// Token: 0x04000FE5 RID: 4069
	public const float STAND_WALK_SPEED = 3.7f;

	// Token: 0x04000FE6 RID: 4070
	public const float CROUCH_WALK_SPEED = 2.5f;

	// Token: 0x04000FE7 RID: 4071
	public const float PRONE_WALK_SPEED = 1.5f;

	// Token: 0x04000FE8 RID: 4072
	private const float FALLING_BALANCE_DRAIN_SPEED = 200f;

	// Token: 0x04000FE9 RID: 4073
	public const float BASE_SPRINT_SPEED = 6.5f;

	// Token: 0x04000FEA RID: 4074
	public const float BONUS_SPRINT_SPEED = 8f;

	// Token: 0x04000FEB RID: 4075
	private const float PRONE_ACTOR_MODEL_LOCAL_OFFSET_Z = -0.6f;

	// Token: 0x04000FEC RID: 4076
	private const float PRONE_MOUSE_X_PAD = 50f;

	// Token: 0x04000FED RID: 4077
	private const float STAND_MOUSE_X_PAD = 3f;

	// Token: 0x04000FEE RID: 4078
	private const float CHANGE_ACTOR_MODEL_OFFSET_SPEED = 2f;

	// Token: 0x04000FEF RID: 4079
	private const float LEAN_SPEED = 8f;

	// Token: 0x04000FF0 RID: 4080
	private const float DEATH_TO_LOADOUT_TIME = 2f;

	// Token: 0x04000FF1 RID: 4081
	private const int USE_LAYER_MASK = 2048;

	// Token: 0x04000FF2 RID: 4082
	private const float MAX_USE_DISTANCE = 3f;

	// Token: 0x04000FF3 RID: 4083
	private const float MAX_CALL_DISTANCE = 60f;

	// Token: 0x04000FF4 RID: 4084
	private const float CALL_MAX_OFFSET = 2f;

	// Token: 0x04000FF5 RID: 4085
	private const int CALL_BLOCK_MASK = 1;

	// Token: 0x04000FF6 RID: 4086
	private const float SEAT_CAMERA_OFFSET_UP = 0.85f;

	// Token: 0x04000FF7 RID: 4087
	private const float SEAT_CAMERA_OFFSET_FORWARD = 0.2f;

	// Token: 0x04000FF8 RID: 4088
	private const float LADDER_CAMERA_OFFSET_UP = 1.6f;

	// Token: 0x04000FF9 RID: 4089
	private const float LADDER_CAMERA_OFFSET_FORWARD = -0.5f;

	// Token: 0x04000FFA RID: 4090
	private const float EXIT_VEHICLE_PAD_UP = 0.8f;

	// Token: 0x04000FFB RID: 4091
	public const float HELICOPTER_FOV = 75f;

	// Token: 0x04000FFC RID: 4092
	public const float HELICOPTER_ZOOM_FOV = 50f;

	// Token: 0x04000FFD RID: 4093
	public const float DEFAULT_FOV = 60f;

	// Token: 0x04000FFE RID: 4094
	public const float DEFAULT_ZOOM_FOV = 45f;

	// Token: 0x04000FFF RID: 4095
	private const float CAMERA_RETURN_SPEED = 400f;

	// Token: 0x04001000 RID: 4096
	public const float CAMERA_CHANGE_HEIGHT_SPEED = 4f;

	// Token: 0x04001001 RID: 4097
	public const float CAMERA_STANCE_HEIGHT_OFFSET = -0.17f;

	// Token: 0x04001002 RID: 4098
	public const float STAND_HEIGHT = 1.7f;

	// Token: 0x04001003 RID: 4099
	public const float CROUCH_HEIGHT = 1.1f;

	// Token: 0x04001004 RID: 4100
	public const float PRONE_HEIGHT = 0.8f;

	// Token: 0x04001005 RID: 4101
	public const float PRONE_CAMERA_HEIGHT = 0.6f;

	// Token: 0x04001006 RID: 4102
	private const float STANCE_CHANGE_SPHERECAST_RADIUS = 0.4f;

	// Token: 0x04001007 RID: 4103
	private const int STANCE_CHANGE_SPHERECAST_MASK = 4097;

	// Token: 0x04001008 RID: 4104
	private const int CAMERA_LAYER_MASK = 4097;

	// Token: 0x04001009 RID: 4105
	private const float LQ_DISTANCE = 12000f;

	// Token: 0x0400100A RID: 4106
	public Camera fpCamera;

	// Token: 0x0400100B RID: 4107
	public Camera fpViewModelCamera;

	// Token: 0x0400100C RID: 4108
	private Transform fpCameraRoot;

	// Token: 0x0400100D RID: 4109
	public Camera tpCamera;

	// Token: 0x0400100E RID: 4110
	public PlayerFpParent fpParent;

	// Token: 0x0400100F RID: 4111
	public Transform weaponParent;

	// Token: 0x04001010 RID: 4112
	public Transform tpWeaponParent;

	// Token: 0x04001011 RID: 4113
	public SoundBank bulletFlybySoundbank;

	// Token: 0x04001012 RID: 4114
	public UnityEngine.Audio.AudioMixer mixer;

	// Token: 0x04001013 RID: 4115
	public AudioMixerSnapshot defaultMix;

	// Token: 0x04001014 RID: 4116
	public AudioMixerSnapshot enclosedMix;

	// Token: 0x04001015 RID: 4117
	public AudioMixerSnapshot deafMix;

	// Token: 0x04001016 RID: 4118
	public AudioMixerSnapshot endMix;

	// Token: 0x04001017 RID: 4119
	public AudioMixerSnapshot zeroMix;

	// Token: 0x04001018 RID: 4120
	public ParentToMainCamera audioListenerAutoParent;

	// Token: 0x04001019 RID: 4121
	public FPLightIntensity fpLightIntensity;

	// Token: 0x0400101A RID: 4122
	public Matrix4x4 activeCameraWorldToLocalMatrix;

	// Token: 0x0400101B RID: 4123
	public Matrix4x4 activeCameraLocalToWorldMatrix;

	// Token: 0x0400101C RID: 4124
	public Animation kickAnimation;

	// Token: 0x0400101D RID: 4125
	private SkinnedMeshRenderer kickRenderer;

	// Token: 0x0400101E RID: 4126
	private AudioSource kickSound;

	// Token: 0x0400101F RID: 4127
	public AudioClip kickHitSound;

	// Token: 0x04001020 RID: 4128
	[NonSerialized]
	public float lqDistance = 200f;

	// Token: 0x04001021 RID: 4129
	[NonSerialized]
	public bool firstPerson = true;

	// Token: 0x04001022 RID: 4130
	private bool hasAcceptedLoadoutAfterDeath = true;

	// Token: 0x04001023 RID: 4131
	private CharacterController characterController;

	// Token: 0x04001024 RID: 4132
	[NonSerialized]
	public FirstPersonController controller;

	// Token: 0x04001025 RID: 4133
	private Renderer[] thirdpersonRenderers;

	// Token: 0x04001026 RID: 4134
	private Vector3 fpCameraParentOffset;

	// Token: 0x04001027 RID: 4135
	private Vector3 actorLocalOrigin;

	// Token: 0x04001028 RID: 4136
	private Camera overrideCamera;

	// Token: 0x04001029 RID: 4137
	private bool inputEnabled = true;

	// Token: 0x0400102A RID: 4138
	[NonSerialized]
	public bool crouching;

	// Token: 0x0400102B RID: 4139
	private bool mouseViewLocked;

	// Token: 0x0400102C RID: 4140
	private bool allowMouseViewWhileAiming;

	// Token: 0x0400102D RID: 4141
	[NonSerialized]
	public bool inPhotoMode;

	// Token: 0x0400102E RID: 4142
	private Camera photoModeOverridenCamera;

	// Token: 0x0400102F RID: 4143
	private TimedAction cannotLeaveAction = new TimedAction(1f, false);

	// Token: 0x04001030 RID: 4144
	private TimedAction hasNotBeenGroundedAction = new TimedAction(1f, false);

	// Token: 0x04001031 RID: 4145
	private TimedAction sprintCannotFireAction = new TimedAction(0.2f, false);

	// Token: 0x04001032 RID: 4146
	private TimedAction kickAction = new TimedAction(0.7f, false);

	// Token: 0x04001033 RID: 4147
	private TimedAction kickCooldownAction = new TimedAction(1f, false);

	// Token: 0x04001034 RID: 4148
	private TimedAction readyToRespawnCooldownAction = new TimedAction(1f, false);

	// Token: 0x04001035 RID: 4149
	private TimedAction blackoutAction = new TimedAction(0.2f, false);

	// Token: 0x04001036 RID: 4150
	private Texture2D blackoutTexture;

	// Token: 0x04001037 RID: 4151
	private bool playingVictoryAnimation;

	// Token: 0x04001038 RID: 4152
	private TimedAction victoryCameraAnimationAction = new TimedAction(10f, false);

	// Token: 0x04001039 RID: 4153
	private TimedAction victoryCameraSequence = new TimedAction(16f, false);

	// Token: 0x0400103A RID: 4154
	private Vector3 victoryCameraForwardFlat;

	// Token: 0x0400103B RID: 4155
	private Vector3 victoryCameraTargetPosition;

	// Token: 0x0400103C RID: 4156
	private Vector3 victoryCameraDeltaOriginLook;

	// Token: 0x0400103D RID: 4157
	private Vector3 victoryCameraTargetLook;

	// Token: 0x0400103E RID: 4158
	private NightVision goggles;

	// Token: 0x0400103F RID: 4159
	public Squad playerSquad;

	// Token: 0x04001040 RID: 4160
	private TimedAction squadKeyTapAction = new TimedAction(0.2f, false);

	// Token: 0x04001041 RID: 4161
	private SquadPointUi.SquadPointOrderBaseType pointOrderBaseType;

	// Token: 0x04001042 RID: 4162
	private bool pointOrderCanceled;

	// Token: 0x04001043 RID: 4163
	private PlayerActionInput aimInput;

	// Token: 0x04001044 RID: 4164
	private PlayerActionInput crouchInput;

	// Token: 0x04001045 RID: 4165
	private PlayerActionInput proneInput;

	// Token: 0x04001046 RID: 4166
	private PlayerActionInput sprintInput;

	// Token: 0x04001047 RID: 4167
	private bool readyToIssueRegroupOrder;

	// Token: 0x04001048 RID: 4168
	private bool vehicleThirdPersonCamera;

	// Token: 0x04001049 RID: 4169
	[NonSerialized]
	public bool helicopterAutoHoverEnabled;

	// Token: 0x0400104A RID: 4170
	[NonSerialized]
	public bool attractMode;

	// Token: 0x0400104B RID: 4171
	private TimedAction attractModeShotAction = new TimedAction(5f, false);

	// Token: 0x0400104C RID: 4172
	private Vector3 attractModeOffset = Vector3.zero;

	// Token: 0x0400104D RID: 4173
	private Vector3 attractModePosition = Vector3.zero;

	// Token: 0x0400104E RID: 4174
	private Quaternion attractModeRotation = Quaternion.identity;

	// Token: 0x0400104F RID: 4175
	private Vector3 targetAttractPosition = Vector3.zero;

	// Token: 0x04001050 RID: 4176
	private Quaternion targetAttractRotation = Quaternion.identity;

	// Token: 0x04001051 RID: 4177
	private Actor attractModeTarget;

	// Token: 0x04001052 RID: 4178
	private TimedAction disallowTogglePhotoModeAction = new TimedAction(0.5f, true);

	// Token: 0x04001053 RID: 4179
	private Quaternion faceKillerOrigin;

	// Token: 0x04001054 RID: 4180
	private Actor killer;

	// Token: 0x04001055 RID: 4181
	private TimedAction faceKillerAction = new TimedAction(6f, false);

	// Token: 0x04001056 RID: 4182
	private float cameraHeight = 1.5300001f;

	// Token: 0x04001057 RID: 4183
	private float targetCameraHeight = 1.5300001f;

	// Token: 0x04001058 RID: 4184
	[NonSerialized]
	public bool allowPlayerControlledTpCamera = true;

	// Token: 0x04001059 RID: 4185
	[NonSerialized]
	public bool unlockCursorRavenscriptOverride;

	// Token: 0x0400105A RID: 4186
	[NonSerialized]
	public bool allowMouseLookRavenscriptOverride = true;

	// Token: 0x0400105B RID: 4187
	[NonSerialized]
	public bool allowExitVehicle = true;

	// Token: 0x0400105C RID: 4188
	[NonSerialized]
	public float mouseSensitivity;

	// Token: 0x0400105D RID: 4189
	private Coroutine freezeCharacterControllerCoroutine;

	// Token: 0x0400105E RID: 4190
	[NonSerialized]
	public bool lockCursor;

	// Token: 0x02000235 RID: 565
	public struct ControlHints
	{
		// Token: 0x0400105F RID: 4191
		public FpsActorController.ControlHints.ConsumableFlag aimMode;

		// Token: 0x04001060 RID: 4192
		public FpsActorController.ControlHints.ConsumableFlag fireMode;

		// Token: 0x04001061 RID: 4193
		public FpsActorController.ControlHints.ConsumableFlag loadout;

		// Token: 0x04001062 RID: 4194
		public FpsActorController.ControlHints.ConsumableFlag respawn;

		// Token: 0x04001063 RID: 4195
		public FpsActorController.ControlHints.ConsumableFlag laserGuidedTarget;

		// Token: 0x02000236 RID: 566
		public struct ConsumableFlag
		{
			// Token: 0x06000FBE RID: 4030 RVA: 0x0000CB30 File Offset: 0x0000AD30
			public bool TryConsume()
			{
				if (this.consumed)
				{
					return false;
				}
				this.consumed = true;
				return true;
			}

			// Token: 0x06000FBF RID: 4031 RVA: 0x0000CB44 File Offset: 0x0000AD44
			public void Reset()
			{
				this.consumed = false;
			}

			// Token: 0x04001064 RID: 4196
			private bool consumed;
		}
	}
}
