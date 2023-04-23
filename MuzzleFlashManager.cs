using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class MuzzleFlashManager : MonoBehaviour
{
	// Token: 0x06000925 RID: 2341 RVA: 0x0006A320 File Offset: 0x00068520
	private void Awake()
	{
		MuzzleFlashManager.instance = this;
		this.light = base.GetComponent<Light>();
		int num = Mathf.Max(60, Screen.currentResolution.refreshRate);
		this.lightStayOnAction = new TimedAction(1f / (float)num, true);
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0000812A File Offset: 0x0000632A
	private void Update()
	{
		if (this.lightStayOnAction.TrueDone())
		{
			this.isPlayer = false;
			this.closestDistance = 300f;
		}
		this.light.enabled = !this.lightStayOnAction.TrueDone();
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0006A368 File Offset: 0x00068568
	public static void UpdateIntensity()
	{
		if (MuzzleFlashManager.instance == null)
		{
			return;
		}
		int dropdown = Options.GetDropdown(OptionDropdown.Id.MuzzleFlashLight);
		MuzzleFlashManager.instance.isEnabled = (dropdown > 0);
		MuzzleFlashManager.instance.light.intensity = ((dropdown == 1) ? 0.5f : 1.05f);
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0006A3B8 File Offset: 0x000685B8
	public static void RegisterMuzzleFlash(Vector3 position, Actor source, bool isMountedWeapon)
	{
		if (!MuzzleFlashManager.instance.isEnabled || source.distanceCull)
		{
			return;
		}
		float distance;
		if (GameManager.IsSpectating())
		{
			distance = Vector3.Distance(position, SpectatorCamera.instance.transform.position);
		}
		else
		{
			distance = ActorManager.ActorDistanceToPlayer(source);
		}
		float range = isMountedWeapon ? 20f : 10f;
		bool flag = !source.aiControlled;
		if (flag || !isMountedWeapon || !source.seat.vehicle.playerIsInside || !FpsActorController.instance.firstPerson)
		{
			MuzzleFlashManager.RegisterLightFlash(position, distance, flag, range);
		}
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0006A44C File Offset: 0x0006864C
	public static void RegisterLightFlash(Vector3 position, float distance, bool isPlayerSource, float range)
	{
		if (!isPlayerSource)
		{
			MuzzleFlashManager.instance.isPlayer = true;
		}
		else if (MuzzleFlashManager.instance.isPlayer)
		{
			return;
		}
		if (MuzzleFlashManager.instance.isPlayer || distance < MuzzleFlashManager.instance.closestDistance)
		{
			MuzzleFlashManager.instance.closestDistance = distance;
			if (MuzzleFlashManager.instance.isPlayer && FpsActorController.instance.firstPerson)
			{
				Camera fpCamera = FpsActorController.instance.fpCamera;
				MuzzleFlashManager.instance.light.transform.position = position + FpsActorController.instance.activeCameraLocalToWorldMatrix.MultiplyVector(new Vector3(-0.25f, 0.25f, 0f));
			}
			else
			{
				MuzzleFlashManager.instance.light.transform.position = position;
			}
			MuzzleFlashManager.instance.light.enabled = true;
			MuzzleFlashManager.instance.lightStayOnAction.Start();
			MuzzleFlashManager.instance.light.range = range;
		}
	}

	// Token: 0x040009F9 RID: 2553
	public static MuzzleFlashManager instance;

	// Token: 0x040009FA RID: 2554
	private const float MAX_DISTANCE = 300f;

	// Token: 0x040009FB RID: 2555
	private const float RANGE_INFANTRY = 10f;

	// Token: 0x040009FC RID: 2556
	private const float RANGE_MOUNTED = 20f;

	// Token: 0x040009FD RID: 2557
	public const float RANGE_EXPLOSION = 20f;

	// Token: 0x040009FE RID: 2558
	private const float INTENSITY_REDUCED = 0.5f;

	// Token: 0x040009FF RID: 2559
	private const float INTENSITY_FULL = 1.05f;

	// Token: 0x04000A00 RID: 2560
	private Light light;

	// Token: 0x04000A01 RID: 2561
	private float closestDistance;

	// Token: 0x04000A02 RID: 2562
	private bool isPlayer;

	// Token: 0x04000A03 RID: 2563
	private TimedAction lightStayOnAction;

	// Token: 0x04000A04 RID: 2564
	private bool isEnabled = true;
}
