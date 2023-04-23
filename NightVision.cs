using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F3 RID: 243
public class NightVision : ToggleableItem
{
	// Token: 0x06000730 RID: 1840 RVA: 0x000069BB File Offset: 0x00004BBB
	protected override void Awake()
	{
		base.Awake();
		this.canvas = base.GetComponentInChildren<Canvas>();
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x000069CF File Offset: 0x00004BCF
	public override void Toggle(bool ignoreCooldown = false)
	{
		if (GameManager.IsSpectating() || FpsActorController.instance == null || FpsActorController.instance.inPhotoMode)
		{
			ignoreCooldown = true;
		}
		base.Toggle(ignoreCooldown);
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00061CF4 File Offset: 0x0005FEF4
	public override void ToggleEnable()
	{
		base.ToggleEnable();
		if (!base.UserIsAI())
		{
			NightVision.isEnabled = true;
			IRFlaresRenderer.SetFlareRenderingEnabled(true);
			ActorManager.EnableNightVisionGlow();
			if (base.UserIsPlayer())
			{
				this.audio.PlayOneShot(this.enableClip);
				this.vignette.enabled = true;
			}
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00061D48 File Offset: 0x0005FF48
	public override void ToggleDisable()
	{
		base.ToggleDisable();
		if (!base.UserIsAI())
		{
			NightVision.isEnabled = false;
			ActorManager.DisableNightVisionGlow();
			IRFlaresRenderer.SetFlareRenderingEnabled(false);
			if (base.UserIsPlayer())
			{
				this.audio.PlayOneShot(this.disableClip);
				this.vignette.enabled = false;
			}
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00061D9C File Offset: 0x0005FF9C
	public void ForceDisable()
	{
		base.ToggleDisable();
		NightVision.isEnabled = false;
		this.vignette.enabled = false;
		try
		{
			ActorManager.DisableNightVisionGlow();
		}
		catch (Exception)
		{
		}
		try
		{
			IRFlaresRenderer.SetFlareRenderingEnabled(false);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00061DF4 File Offset: 0x0005FFF4
	private void LateUpdate()
	{
		if (base.UserIsPlayer())
		{
			this.canvas.transform.parent.position = Vector3.zero;
			this.canvas.transform.parent.rotation = Quaternion.identity;
			if (this.toggleAction.TrueDone())
			{
				this.blackOverlay.enabled = false;
				return;
			}
			this.blackOverlay.enabled = !GameManager.IsPaused();
			Color black = Color.black;
			black.a = Mathf.Clamp01((1f - this.toggleAction.Ratio()) * 2f);
			this.blackOverlay.color = black;
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x000069FB File Offset: 0x00004BFB
	public override void Equip(Actor user)
	{
		base.Equip(user);
		this.canvas.enabled = base.UserIsPlayer();
	}

	// Token: 0x04000738 RID: 1848
	private const float ACTOR_GLOW_AMOUNT = 0.5f;

	// Token: 0x04000739 RID: 1849
	private const float ACTOR_GLOW_MULTIPLIER_NO_EMISSION_MAP = 0.2f;

	// Token: 0x0400073A RID: 1850
	public static readonly Color GLOW_EMISSION_MAP_COLOR = new Color(0.5f, 0.5f, 0.5f, 1f);

	// Token: 0x0400073B RID: 1851
	public static readonly Color GLOW_SOLID_COLOR = NightVision.GLOW_EMISSION_MAP_COLOR * 0.2f;

	// Token: 0x0400073C RID: 1852
	public static bool isEnabled = false;

	// Token: 0x0400073D RID: 1853
	public RawImage blackOverlay;

	// Token: 0x0400073E RID: 1854
	public RawImage vignette;

	// Token: 0x0400073F RID: 1855
	public AudioClip enableClip;

	// Token: 0x04000740 RID: 1856
	public AudioClip disableClip;

	// Token: 0x04000741 RID: 1857
	private Canvas canvas;
}
