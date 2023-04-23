using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class RemoteDetonatorWeapon : ThrowableWeapon
{
	// Token: 0x0600077F RID: 1919 RVA: 0x00006CBF File Offset: 0x00004EBF
	protected override void Start()
	{
		base.Start();
		this.detonationDelayAction = new TimedAction(this.detonateDelay, false);
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00006CD9 File Offset: 0x00004ED9
	public void RegisterProjectile(RemoteDetonatedProjectile projectile)
	{
		this.registeredProjectiles.Add(projectile);
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00006CE7 File Offset: 0x00004EE7
	public void DeregisterProjectile(RemoteDetonatedProjectile projectile)
	{
		this.registeredProjectiles.Remove(projectile);
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00063980 File Offset: 0x00061B80
	public override void Fire(Vector3 direction, bool useMuzzleDirection)
	{
		if (this.aiming && !this.hasFiredSingleRoundThisTrigger && !base.CoolingDown() && !this.reloading && this.unholstered)
		{
			this.animator.SetTrigger(RemoteDetonatorWeapon.DETONATE_PARAMETER_HASH);
			this.hasFiredSingleRoundThisTrigger = true;
			if (!this.detonationTriggered)
			{
				this.detonationTriggered = true;
				this.detonationDelayAction.Start();
			}
			return;
		}
		base.Fire(direction, useMuzzleDirection);
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00006CF6 File Offset: 0x00004EF6
	protected override void Update()
	{
		base.Update();
		if (this.detonationTriggered && this.detonationDelayAction.TrueDone())
		{
			this.Detonate();
			this.detonationTriggered = false;
		}
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x000639F0 File Offset: 0x00061BF0
	private void Detonate()
	{
		foreach (RemoteDetonatedProjectile remoteDetonatedProjectile in this.registeredProjectiles.ToArray())
		{
			if (remoteDetonatedProjectile != null)
			{
				remoteDetonatedProjectile.Detonate();
			}
		}
		this.registeredProjectiles.Clear();
	}

	// Token: 0x0400079C RID: 1948
	public static readonly int DETONATE_PARAMETER_HASH = Animator.StringToHash("detonate");

	// Token: 0x0400079D RID: 1949
	public float detonateDelay = 0.3f;

	// Token: 0x0400079E RID: 1950
	private List<RemoteDetonatedProjectile> registeredProjectiles = new List<RemoteDetonatedProjectile>();

	// Token: 0x0400079F RID: 1951
	private bool detonationTriggered;

	// Token: 0x040007A0 RID: 1952
	private TimedAction detonationDelayAction;
}
