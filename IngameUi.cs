using System;
using System.Collections;
using Lua;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A8 RID: 680
public class IngameUi : MonoBehaviour
{
	// Token: 0x060011F6 RID: 4598 RVA: 0x0008D9BC File Offset: 0x0008BBBC
	public static void OnDamageDealt(DamageInfo info, HitInfo hit)
	{
		try
		{
			if (info.isPlayerSource)
			{
				RavenscriptManager.events.onPlayerDealtDamage.Invoke(info, hit);
				if (IngameUi.showHitmarkers)
				{
					IngameUi.instance.ShowHitmarker();
				}
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x0008DA10 File Offset: 0x0008BC10
	private void Awake()
	{
		IngameUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.minimapCamera = UnityEngine.Object.FindObjectOfType<MinimapCamera>();
		this.hitmarkerSound = this.hitmarker.GetComponent<AudioSource>();
		this.damageVignette.color = Color.clear;
		this.HideVehicleBar(true);
		this.Hide();
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x0008DA68 File Offset: 0x0008BC68
	public void SetAmmoText(int current, int spare)
	{
		this.currentAmmo.text = ((current != -1) ? current.ToString() : "");
		if (spare >= 0)
		{
			this.spareAmmo.text = "/" + spare.ToString();
			return;
		}
		if (spare == -1)
		{
			this.spareAmmo.text = "";
			return;
		}
		if (spare == -2)
		{
			this.spareAmmo.text = "/∞";
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0000E186 File Offset: 0x0000C386
	public static void ToggleVisibility()
	{
		IngameUi.instance.forceHide = !IngameUi.instance.forceHide;
		IngameUi.instance.canvas.enabled = !IngameUi.instance.forceHide;
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x0000E1BB File Offset: 0x0000C3BB
	public static void SetVisibility(bool visible)
	{
		IngameUi.instance.forceHide = !visible;
		IngameUi.instance.canvas.enabled = !IngameUi.instance.forceHide;
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x0008DAE0 File Offset: 0x0008BCE0
	private void Update()
	{
		this.resupplyHealthIndicator.enabled = !this.resupplyHealthAction.TrueDone();
		this.resupplyAmmoIndicator.enabled = !this.resupplyAmmoAction.TrueDone();
		this.resupplyHealthIndicator.rectTransform.anchoredPosition = new Vector2(0f, this.resupplyHealthAction.Ratio() * 30f);
		this.resupplyAmmoIndicator.rectTransform.anchoredPosition = new Vector2(0f, this.resupplyAmmoAction.Ratio() * 30f);
		Color white = Color.white;
		white.a = Mathf.Clamp01(2f - 2f * this.resupplyHealthAction.Ratio());
		this.resupplyHealthIndicator.color = white;
		white.a = Mathf.Clamp01(2f - 2f * this.resupplyAmmoAction.Ratio());
		this.resupplyAmmoIndicator.color = white;
		if (!GameManager.gameOver && Input.GetKeyDown(KeyCode.End))
		{
			IngameUi.ToggleVisibility();
		}
		Actor actor = FpsActorController.instance.actor;
		if (actor.IsSeated())
		{
			this.SetVehicleBarAmount(actor.seat.vehicle.GetHealthRatio());
		}
		Squad playerSquad = FpsActorController.instance.playerSquad;
		if (playerSquad != null)
		{
			int num = playerSquad.members.Count - 1;
			if (num == 0)
			{
				this.squadText.text = "NO SQUAD";
			}
			else if (playerSquad.shouldFollow)
			{
				this.squadText.text = "SQUAD (" + num.ToString() + "): FOLLOWING";
			}
			else
			{
				this.squadText.text = (playerSquad.NoAiMemberHasPath() ? ("SQUAD (" + num.ToString() + "): HOLDING") : ("SQUAD (" + num.ToString() + "): MOVING"));
			}
		}
		this.squadText.enabled = (playerSquad != null);
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x0008DCCC File Offset: 0x0008BECC
	private void LateUpdate()
	{
		this.minimapCamera.camera.WorldToViewportPoint(FpsActorController.instance.actor.Position());
		this.hitmarker.enabled = !this.hitmarkerAction.Done();
		Color white = Color.white;
		if (this.vignetteAction.Done())
		{
			white.a = 0f;
		}
		else
		{
			float num = Mathf.Lerp(0.5f, 0f, Mathf.Clamp01(this.vignetteAction.Ratio() * 10f));
			white.g -= num;
			white.b -= num;
			white.a = Mathf.Lerp(0f, this.vignetteIntensity, this.vignetteIntensityCurve.Evaluate(this.vignetteAction.Ratio()));
		}
		this.damageVignette.color = white;
		white = this.damageIndicatorColor;
		white.a = Mathf.Clamp01(3f - 3f * this.damageIndicatorAction.Ratio());
		this.damageIndicator.color = white;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x0000E1E7 File Offset: 0x0000C3E7
	public void SetWeapon(Weapon weapon)
	{
		this.weapon.sprite = weapon.uiSprite;
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x0008DDE8 File Offset: 0x0008BFE8
	public void SetHealth(float health)
	{
		this.health.text = Mathf.CeilToInt(health).ToString();
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x0000E1FA File Offset: 0x0000C3FA
	public void SetSightText(string text)
	{
		this.sight.text = text;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x0000E208 File Offset: 0x0000C408
	public void Hide()
	{
		this.canvas.enabled = false;
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0000E216 File Offset: 0x0000C416
	public void Show()
	{
		this.canvas.enabled = !this.forceHide;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x0000E22C File Offset: 0x0000C42C
	private void ShowHitmarker()
	{
		if (this.hitmarkerAction.Done())
		{
			this.hitmarkerAction.Start();
			this.hitmarkerSound.Play();
		}
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x0000E251 File Offset: 0x0000C451
	public void FlashVehicleBar(float amount)
	{
		if (this.flashVehicleBarCoroutine != null)
		{
			base.StopCoroutine(this.flashVehicleBarCoroutine);
		}
		this.flashVehicleBarCoroutine = base.StartCoroutine(this.FlashVehicleBarCoroutine(amount));
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x0000E27A File Offset: 0x0000C47A
	private IEnumerator FlashVehicleBarCoroutine(float amount)
	{
		this.ShowVehicleBar(amount, false);
		yield return new WaitForSeconds(2f);
		this.HideVehicleBar(false);
		yield break;
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0000E290 File Offset: 0x0000C490
	public void ShowVehicleBar(float amount, bool cancelCoroutine = true)
	{
		if (cancelCoroutine && this.flashVehicleBarCoroutine != null)
		{
			base.StopCoroutine(this.flashVehicleBarCoroutine);
		}
		this.vehicleHealth.enabled = true;
		this.vehicleHealthBackground.enabled = true;
		this.SetVehicleBarAmount(amount);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
	public void HideVehicleBar(bool cancelCoroutine = true)
	{
		if (cancelCoroutine && this.flashVehicleBarCoroutine != null)
		{
			base.StopCoroutine(this.flashVehicleBarCoroutine);
		}
		this.vehicleHealth.enabled = false;
		this.vehicleHealthBackground.enabled = false;
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x0008DE10 File Offset: 0x0008C010
	public void SetVehicleBarAmount(float amount)
	{
		this.vehicleHealth.rectTransform.anchorMax = new Vector2(amount, 1f);
		this.vehicleHealth.uvRect = new Rect(0f, 0f, 9f * amount, 1f);
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x0000E2F9 File Offset: 0x0000C4F9
	public void ShowVignette(float intensity, float duration)
	{
		this.vignetteIntensity = intensity;
		this.vignetteAction.StartLifetime(duration);
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0008DE60 File Offset: 0x0008C060
	public void ShowDamageIndicator(float angle, bool onlyBalanceDamage)
	{
		this.damageIndicatorAction.Start();
		this.damageIndicator.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
		this.damageIndicatorColor = (onlyBalanceDamage ? Color.yellow : Color.red);
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0000E30E File Offset: 0x0000C50E
	public void Heal()
	{
		this.healSounds.PlayRandom();
		this.resupplyHealthAction.Start();
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x0000E326 File Offset: 0x0000C526
	public void Resupply()
	{
		this.resupplySounds.PlayRandom();
		this.resupplyAmmoAction.Start();
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x0000E33E File Offset: 0x0000C53E
	public void ShowFlagIndicator()
	{
		this.flagIndicatorParent.gameObject.SetActive(true);
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0008DEB0 File Offset: 0x0008C0B0
	public void SetFlagIndicator(float amount, int owner, int pendingOwner)
	{
		if (!this.flagIndicatorParent.gameObject.activeInHierarchy)
		{
			return;
		}
		this.flagIndicatorBackground.rectTransform.anchorMax = new Vector2(1f, amount);
		this.flagIndicatorBackground.uvRect = new Rect(0f, 0f, 1f, amount);
		this.flagIndicatorBackground.color = ColorScheme.TeamColor(pendingOwner);
		this.flagIndicator.color = ColorScheme.TeamColor(owner);
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x0000E351 File Offset: 0x0000C551
	public void HideFlagIndicator()
	{
		this.flagIndicatorParent.gameObject.SetActive(false);
	}

	// Token: 0x0400131E RID: 4894
	public static IngameUi instance;

	// Token: 0x0400131F RID: 4895
	public static bool showHitmarkers = true;

	// Token: 0x04001320 RID: 4896
	[NonSerialized]
	public Canvas canvas;

	// Token: 0x04001321 RID: 4897
	public Text currentAmmo;

	// Token: 0x04001322 RID: 4898
	public Text spareAmmo;

	// Token: 0x04001323 RID: 4899
	public Text health;

	// Token: 0x04001324 RID: 4900
	public Text sight;

	// Token: 0x04001325 RID: 4901
	public Image weapon;

	// Token: 0x04001326 RID: 4902
	public RawImage hitmarker;

	// Token: 0x04001327 RID: 4903
	public RawImage damageVignette;

	// Token: 0x04001328 RID: 4904
	public RawImage damageIndicator;

	// Token: 0x04001329 RID: 4905
	public AnimationCurve vignetteIntensityCurve;

	// Token: 0x0400132A RID: 4906
	public SoundBank healSounds;

	// Token: 0x0400132B RID: 4907
	public SoundBank resupplySounds;

	// Token: 0x0400132C RID: 4908
	public RawImage resupplyHealthIndicator;

	// Token: 0x0400132D RID: 4909
	public RawImage resupplyAmmoIndicator;

	// Token: 0x0400132E RID: 4910
	public RawImage vehicleHealthBackground;

	// Token: 0x0400132F RID: 4911
	public RawImage vehicleHealth;

	// Token: 0x04001330 RID: 4912
	public RawImage flagIndicatorParent;

	// Token: 0x04001331 RID: 4913
	public RawImage flagIndicatorBackground;

	// Token: 0x04001332 RID: 4914
	public RawImage flagIndicator;

	// Token: 0x04001333 RID: 4915
	public Text squadText;

	// Token: 0x04001334 RID: 4916
	private AudioSource hitmarkerSound;

	// Token: 0x04001335 RID: 4917
	private MinimapCamera minimapCamera;

	// Token: 0x04001336 RID: 4918
	private TimedAction hitmarkerAction = new TimedAction(0.15f, false);

	// Token: 0x04001337 RID: 4919
	private TimedAction damageIndicatorAction = new TimedAction(1.5f, false);

	// Token: 0x04001338 RID: 4920
	private TimedAction resupplyHealthAction = new TimedAction(1.5f, false);

	// Token: 0x04001339 RID: 4921
	private TimedAction resupplyAmmoAction = new TimedAction(1.5f, false);

	// Token: 0x0400133A RID: 4922
	private Color damageIndicatorColor = Color.red;

	// Token: 0x0400133B RID: 4923
	private TimedAction vignetteAction = new TimedAction(1f, false);

	// Token: 0x0400133C RID: 4924
	private float vignetteIntensity;

	// Token: 0x0400133D RID: 4925
	[NonSerialized]
	public bool forceHide;

	// Token: 0x0400133E RID: 4926
	private Coroutine flashVehicleBarCoroutine;
}
