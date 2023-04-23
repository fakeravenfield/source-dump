using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class MountedWeapon : Weapon
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x00064DF8 File Offset: 0x00062FF8
	protected override void Awake()
	{
		base.Awake();
		this.configuration.forceAutoReload = true;
		if (this.overrideCamera != null)
		{
			this.overrideCamera.fieldOfView = Mathf.Max(this.overrideCamera.fieldOfView, 5f);
		}
		if (this.aimCamera != null)
		{
			this.aimCamera.fieldOfView = Mathf.Max(this.aimCamera.fieldOfView, 5f);
			this.baseFov = this.aimCamera.fieldOfView;
		}
		this.baseAudioPriority = this.audio.priority;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00064E98 File Offset: 0x00063098
	protected override void Update()
	{
		base.Update();
		if (this.aimCamera != null && this.unholstered && base.user != null)
		{
			float target = base.user.IsAiming() ? 1f : 0f;
			this.aim = Mathf.MoveTowards(this.aim, target, this.aimChangeSpeed * Time.deltaTime);
			this.aimCamera.fieldOfView = Mathf.Lerp(this.baseFov, base.GetAimFov(), this.aim);
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00007140 File Offset: 0x00005340
	public void AssignFpVehicleAudioMix()
	{
		if (!this.configuration.forceWorldAudioOutput)
		{
			this.audio.outputAudioMixerGroup = GameManager.instance.fpMixerGroup;
			this.audio.spatialBlend = 0f;
		}
		this.audio.priority = 0;
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00007180 File Offset: 0x00005380
	public void AssignPlayerVehicleAudioMix()
	{
		if (!this.configuration.forceWorldAudioOutput)
		{
			this.audio.outputAudioMixerGroup = GameManager.instance.playerVehicleMixerGroup;
		}
		this.audio.priority = 1;
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x000071B0 File Offset: 0x000053B0
	public void ResetAudioMix()
	{
		this.audio.priority = this.baseAudioPriority;
		this.audio.spatialBlend = 1f;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0000257D File Offset: 0x0000077D
	protected override bool CanUseEyeAsMuzzle()
	{
		return false;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x000071D3 File Offset: 0x000053D3
	public override bool CanFire()
	{
		return !this.seat.vehicle.burning && base.CanFire();
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x000071EF File Offset: 0x000053EF
	public override void Fire(Vector3 direction, bool useMuzzleDirection)
	{
		base.Fire(direction, true);
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00064F2C File Offset: 0x0006312C
	public override void OnWeaponFire(Vector3 muzzlePosition, Vector3 fireDirection)
	{
		base.OnWeaponFire(muzzlePosition, fireDirection);
		try
		{
			this.seat.vehicle.rigidbody.AddForceAtPosition(-fireDirection * this.vehicleRigidbodyRecoilForce, muzzlePosition, ForceMode.VelocityChange);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x000071F9 File Offset: 0x000053F9
	public override void Show()
	{
		this.isHidden = false;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00007202 File Offset: 0x00005402
	public override void Hide()
	{
		this.isHidden = true;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00064F80 File Offset: 0x00063180
	public override void Holster()
	{
		this.unholstered = false;
		base.StopFire();
		if (this.seat.activeWeapon == this)
		{
			this.seat.activeWeapon = null;
		}
		if (this.overrideCamera != null && base.UserIsPlayer() && !FpsActorController.instance.UseThirdPersonVehicleCamera())
		{
			this.seat.SwitchToDefaultCamera();
		}
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00064FE8 File Offset: 0x000631E8
	public override void Unholster()
	{
		base.Unholster();
		if (!base.HasLoadedAmmo() && this.configuration.forceAutoReload)
		{
			this.Reload(true);
		}
		this.aim = 0f;
		if (this.aimCamera != null)
		{
			this.aimCamera.fieldOfView = this.baseFov;
		}
		if (this.overrideCamera != null && base.UserIsPlayer() && !FpsActorController.instance.UseThirdPersonVehicleCamera())
		{
			FpsActorController.instance.SetOverrideCamera(this.overrideCamera);
		}
		this.seat.activeWeapon = this;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsMountedWeapon()
	{
		return true;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsClampedTurret()
	{
		return false;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0000720B File Offset: 0x0000540B
	public virtual Vector3 GetClampedTurretRandomLookDirection()
	{
		return base.transform.forward + UnityEngine.Random.insideUnitSphere * 0.7f;
	}

	// Token: 0x040007F7 RID: 2039
	public Camera overrideCamera;

	// Token: 0x040007F8 RID: 2040
	public Camera aimCamera;

	// Token: 0x040007F9 RID: 2041
	public float vehicleRigidbodyRecoilForce;

	// Token: 0x040007FA RID: 2042
	public float aimChangeSpeed = 10f;

	// Token: 0x040007FB RID: 2043
	private float aim;

	// Token: 0x040007FC RID: 2044
	private float baseFov;

	// Token: 0x040007FD RID: 2045
	private int baseAudioPriority;

	// Token: 0x040007FE RID: 2046
	[NonSerialized]
	public Seat seat;
}
