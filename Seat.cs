using System;
using System.Collections.Generic;
using Lua;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class Seat : MonoBehaviour
{
	// Token: 0x060014AE RID: 5294 RVA: 0x00098480 File Offset: 0x00096680
	private void Awake()
	{
		this.vehicle = base.GetComponentInParent<Vehicle>();
		base.gameObject.layer = 11;
		if (this.camera != null)
		{
			this.camera.farClipPlane = Mathf.Max(this.camera.farClipPlane, Options.maxDrawDistance);
		}
		if (this.thirdPersonCamera != null)
		{
			this.thirdPersonCamera.farClipPlane = Mathf.Max(this.thirdPersonCamera.farClipPlane, Options.maxDrawDistance);
		}
		MountedWeapon[] array = this.weapons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].seat = this;
		}
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x00010783 File Offset: 0x0000E983
	private void Start()
	{
		if (this.hud != null)
		{
			this.hud.SetActive(false);
		}
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x0001079F File Offset: 0x0000E99F
	public bool UseMuffledSoundMix()
	{
		if (this.soundMuffle == Seat.SoundMuffle.Auto)
		{
			this.soundMuffle = ((this.enclosed && !this.enclosedDamagedByDirectFire) ? Seat.SoundMuffle.On : Seat.SoundMuffle.Off);
		}
		return this.soundMuffle == Seat.SoundMuffle.On;
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000107CC File Offset: 0x0000E9CC
	public bool IsDriverSeat()
	{
		return this.vehicle.seats[0] == this;
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000107E5 File Offset: 0x0000E9E5
	public bool IsOccupied()
	{
		return this.occupant != null;
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x00098520 File Offset: 0x00096720
	public void SetOccupant(Actor actor)
	{
		this.occupant = actor;
		MountedWeapon[] array = this.weapons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].user = this.occupant;
		}
		if (!this.occupant.aiControlled)
		{
			if (this.camera != null)
			{
				FpsActorController.instance.SetOverrideCamera(this.camera);
			}
			if (this.hud != null)
			{
				this.hud.SetActive(true);
				if (this.hudRenderers == null)
				{
					this.FindHudElements();
				}
				this.SetHudRenderersEnabled(true);
			}
		}
		this.vehicle.OccupantEntered(this);
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000107F3 File Offset: 0x0000E9F3
	private void FindHudElements()
	{
		this.hudRenderers = new List<Renderer>();
		this.hudCanvas = new List<Canvas>();
		this.FindHudElementsRecursive(this.hud.transform);
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000985C0 File Offset: 0x000967C0
	private void FindHudElementsRecursive(Transform t)
	{
		this.hudRenderers.AddRange(t.GetComponents<Renderer>());
		this.hudCanvas.AddRange(t.GetComponents<Canvas>());
		for (int i = 0; i < t.childCount; i++)
		{
			this.FindHudElementsRecursive(t.GetChild(i));
		}
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x00098610 File Offset: 0x00096810
	public void OccupantLeft()
	{
		Actor leaver = this.occupant;
		this.occupant = null;
		if (this.HasActiveWeapon())
		{
			this.activeWeapon.Holster();
		}
		MountedWeapon[] array = this.weapons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].user = null;
		}
		if (this.hud != null)
		{
			this.hud.SetActive(false);
		}
		this.vehicle.OccupantLeft(this, leaver);
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x0001081C File Offset: 0x0000EA1C
	private void Update()
	{
		if (this.IsOccupied())
		{
			this.occupant.balance = Mathf.Min(this.occupant.balance, this.maxOccupantBalance);
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x00010847 File Offset: 0x0000EA47
	public bool CanUsePersonalWeapons()
	{
		return this.type == Seat.Type.FreelookArmed;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x00010852 File Offset: 0x0000EA52
	public bool HasAnyMountedWeapons()
	{
		return this.weapons.Length != 0;
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x0001085E File Offset: 0x0000EA5E
	public bool HasActiveWeapon()
	{
		return this.activeWeapon != null;
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x0001086C File Offset: 0x0000EA6C
	public bool HasActiveWeaponNonHorn()
	{
		return this.HasActiveWeapon() && this.activeWeapon.GetType() != typeof(CarHorn);
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x00098684 File Offset: 0x00096884
	public int ActiveWeaponSlot()
	{
		for (int i = 0; i < this.weapons.Length; i++)
		{
			if (this.weapons[i] == this.activeWeapon)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x00010892 File Offset: 0x0000EA92
	public bool HasHandIkTargets()
	{
		return this.handTargetL != null || this.handTargetR != null;
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000108B0 File Offset: 0x0000EAB0
	public void SwitchToThirdPersonCamera()
	{
		if (this.thirdPersonCamera != null)
		{
			FpsActorController.instance.SetOverrideCamera(this.thirdPersonCamera);
			FpsActorController.instance.EnableThirdPersonRenderingMode();
		}
		if (this.hud != null)
		{
			this.SetHudRenderersEnabled(false);
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000986BC File Offset: 0x000968BC
	public void SwitchToDefaultCamera()
	{
		FpsActorController.instance.EnableFirstPersonRenderingMode();
		if (this.activeWeapon != null && this.activeWeapon.overrideCamera != null)
		{
			FpsActorController.instance.SetOverrideCamera(this.activeWeapon.overrideCamera);
		}
		else if (this.camera != null)
		{
			FpsActorController.instance.SetOverrideCamera(this.camera);
		}
		else
		{
			FpsActorController.instance.CancelOverrideCamera();
		}
		if (this.hud != null)
		{
			this.SetHudRenderersEnabled(true);
		}
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x0009874C File Offset: 0x0009694C
	public Vector3 GetRaycastExitPosition()
	{
		Vector3 targetExitPosition = this.GetTargetExitPosition();
		Vector3 position = base.transform.position;
		Vector3 vector = targetExitPosition - position;
		Vector3 vector2 = new Vector3(0f, Mathf.Sign(vector.y), 0f);
		float num = Mathf.Abs(vector.y);
		vector.y = 0f;
		Vector3 vector3 = position + vector;
		float magnitude = vector.magnitude;
		Vector3 vector4 = vector / magnitude;
		Ray ray = new Ray(position, vector4);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, magnitude, 2228225))
		{
			vector3 = raycastHit.point - vector4 * 0.3f;
		}
		ray.origin = vector3;
		ray.direction = vector2;
		if (Physics.Raycast(ray, out raycastHit, num, 2232321))
		{
			return raycastHit.point;
		}
		return vector3 + vector2 * num;
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x00098830 File Offset: 0x00096A30
	public Vector3 GetTargetExitPosition()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(this.exitOffset);
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x00098858 File Offset: 0x00096A58
	private void SetHudRenderersEnabled(bool enabled)
	{
		if (this.hudRenderers == null)
		{
			return;
		}
		foreach (Renderer renderer in this.hudRenderers)
		{
			renderer.enabled = enabled;
		}
		foreach (Canvas canvas in this.hudCanvas)
		{
			canvas.enabled = enabled;
		}
	}

	// Token: 0x04001664 RID: 5732
	public const int LAYER = 11;

	// Token: 0x04001665 RID: 5733
	[NonSerialized]
	public Vehicle vehicle;

	// Token: 0x04001666 RID: 5734
	public Seat.Type type;

	// Token: 0x04001667 RID: 5735
	public Seat.SitAnimation animation;

	// Token: 0x04001668 RID: 5736
	public bool enclosed;

	// Token: 0x04001669 RID: 5737
	public bool enclosedDamagedByDirectFire;

	// Token: 0x0400166A RID: 5738
	public Seat.SoundMuffle soundMuffle;

	// Token: 0x0400166B RID: 5739
	public bool allowLean;

	// Token: 0x0400166C RID: 5740
	public bool allowUnderwater;

	// Token: 0x0400166D RID: 5741
	public Vector3 exitOffset = Vector3.zero;

	// Token: 0x0400166E RID: 5742
	public MountedWeapon[] weapons;

	// Token: 0x0400166F RID: 5743
	public Transform handTargetL;

	// Token: 0x04001670 RID: 5744
	public Transform handTargetR;

	// Token: 0x04001671 RID: 5745
	public Camera camera;

	// Token: 0x04001672 RID: 5746
	public Camera thirdPersonCamera;

	// Token: 0x04001673 RID: 5747
	[NonSerialized]
	public MountedWeapon activeWeapon;

	// Token: 0x04001674 RID: 5748
	[NonSerialized]
	public Actor occupant;

	// Token: 0x04001675 RID: 5749
	public GameObject hud;

	// Token: 0x04001676 RID: 5750
	private List<Renderer> hudRenderers;

	// Token: 0x04001677 RID: 5751
	private List<Canvas> hudCanvas;

	// Token: 0x04001678 RID: 5752
	public float maxOccupantBalance = 200f;

	// Token: 0x04001679 RID: 5753
	private const int EXIT_RAY_HIT_MASK = 2228225;

	// Token: 0x02000323 RID: 803
	public enum SitAnimation
	{
		// Token: 0x0400167B RID: 5755
		Chair,
		// Token: 0x0400167C RID: 5756
		Quad,
		// Token: 0x0400167D RID: 5757
		Standing,
		// Token: 0x0400167E RID: 5758
		ChairRelaxed
	}

	// Token: 0x02000324 RID: 804
	public enum SoundMuffle
	{
		// Token: 0x04001680 RID: 5760
		Auto,
		// Token: 0x04001681 RID: 5761
		On,
		// Token: 0x04001682 RID: 5762
		Off
	}

	// Token: 0x02000325 RID: 805
	[Name("SeatCameraType")]
	public enum Type
	{
		// Token: 0x04001684 RID: 5764
		FreelookUnarmed,
		// Token: 0x04001685 RID: 5765
		LockedAllowFreelookUnarmed,
		// Token: 0x04001686 RID: 5766
		AlwaysLockedUnarmed,
		// Token: 0x04001687 RID: 5767
		FreelookArmed
	}
}
