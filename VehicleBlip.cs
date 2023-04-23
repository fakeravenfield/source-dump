using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E2 RID: 738
public class VehicleBlip : MinimapBlip
{
	// Token: 0x0600138C RID: 5004 RVA: 0x0000FA44 File Offset: 0x0000DC44
	protected override Vector3 GetWorldPosition()
	{
		return this.vehicle.transform.position;
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x0000FA56 File Offset: 0x0000DC56
	protected override float GetYAngle()
	{
		return this.vehicle.transform.eulerAngles.y;
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x00093494 File Offset: 0x00091694
	public void SetVehicle(Vehicle vehicle)
	{
		if (this.image == null)
		{
			this.image = base.GetComponentInChildren<RawImage>();
		}
		this.vehicle = vehicle;
		this.image.texture = vehicle.blip;
		this.image.transform.localScale *= 2f * vehicle.blipScale;
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000934FC File Offset: 0x000916FC
	protected override bool IsVisible()
	{
		if (this.vehicle.isTurret)
		{
			return !this.vehicle.IsEmpty() && (this.vehicle.IsHighlighted() || GameManager.IsSpectating() || this.vehicle.ownerTeam == GameManager.PlayerTeam());
		}
		return !this.vehicle.dead && (this.vehicle.IsHighlighted() || GameManager.IsSpectating() || this.vehicle.ownerTeam == GameManager.PlayerTeam() || this.vehicle.spawner == null || (this.vehicle.ownerTeam == -1 && this.vehicle.spawner.lastSpawnedVehicleHasBeenUsed) || (this.vehicle.spawner.spawnPoint != null && this.vehicle.spawner.spawnPoint.owner == GameManager.PlayerTeam()));
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000935F4 File Offset: 0x000917F4
	protected override void LateUpdate()
	{
		if (this.vehicle == null || this.vehicle.dead)
		{
			base.gameObject.transform.SetParent(null, false);
			UnityEngine.Object.Destroy(base.gameObject);
			base.enabled = false;
			return;
		}
		if (this.vehicle.ownerTeam == -1)
		{
			this.image.color = Color.gray;
		}
		else
		{
			this.image.color = Color.Lerp(ColorScheme.TeamColor(this.vehicle.ownerTeam), Color.white, (this.vehicle.claimingSquad != null && this.vehicle.claimingSquad.HasPlayerLeader()) ? 0.8f : 0.2f);
		}
		base.LateUpdate();
	}

	// Token: 0x04001507 RID: 5383
	private const float BASE_BLIP_SCALE = 2f;

	// Token: 0x04001508 RID: 5384
	private Vehicle vehicle;
}
