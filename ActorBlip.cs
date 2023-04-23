using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028C RID: 652
public class ActorBlip : MinimapBlip
{
	// Token: 0x06001168 RID: 4456 RVA: 0x0000DA90 File Offset: 0x0000BC90
	protected override Vector3 GetWorldPosition()
	{
		return this.actor.Position();
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0008BD70 File Offset: 0x00089F70
	protected override float GetYAngle()
	{
		if (this.actor.aiControlled)
		{
			return Quaternion.LookRotation(this.actor.controller.FacingDirection()).eulerAngles.y;
		}
		return FpsActorController.instance.GetActiveCamera().transform.eulerAngles.y;
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0008BDC8 File Offset: 0x00089FC8
	public void SetActor(Actor actor, bool useSightCone)
	{
		if (actor.GetType() == typeof(ForcedAiTarget))
		{
			base.enabled = false;
			return;
		}
		this.actor = actor;
		this.image.color = Color.Lerp(ColorScheme.TeamColor(actor.team), Color.white, actor.controller.IsOnPlayerSquad() ? 0.8f : 0.2f);
		this.useSightCone = useSightCone;
		this.updateTransformWhenInvisible = useSightCone;
		if (this.useSightCone)
		{
			this.sightCone = UnityEngine.Object.Instantiate<GameObject>(this.sightConePrefab, base.transform).GetComponent<RawImage>();
			Vector2 vector = new Vector2(0.5f, 0.5f);
			this.sightCone.rectTransform.anchorMin = vector;
			this.sightCone.rectTransform.anchorMax = vector;
			this.sightCone.rectTransform.anchoredPosition = Vector2.zero;
		}
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0008BEB0 File Offset: 0x0008A0B0
	protected override bool IsVisible()
	{
		return !this.actor.dead && !this.actor.IsSeated() && (GameManager.IsSpectating() || this.actor.team == GameManager.PlayerTeam() || this.actor.IsHighlighted());
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0000DA9D File Offset: 0x0000BC9D
	protected override void LateUpdate()
	{
		if (this.useSightCone)
		{
			this.sightCone.enabled = (this.actor.IsSeated() || this.IsVisible());
		}
		base.LateUpdate();
	}

	// Token: 0x04001285 RID: 4741
	public GameObject sightConePrefab;

	// Token: 0x04001286 RID: 4742
	private Actor actor;

	// Token: 0x04001287 RID: 4743
	private RawImage sightCone;

	// Token: 0x04001288 RID: 4744
	private bool useSightCone;
}
