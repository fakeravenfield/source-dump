using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class TargetSeekingMissile : Rocket
{
	// Token: 0x06000798 RID: 1944 RVA: 0x00006E5E File Offset: 0x0000505E
	public void ClearTrackers()
	{
		this.SetPointTargetProvider(null);
		this.SetTrackerTarget(null);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00006E6E File Offset: 0x0000506E
	public void SetTrackerTarget(Vehicle target)
	{
		this.currentTarget = target;
		this.isTrackingTarget = (this.currentTarget != null);
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00006E89 File Offset: 0x00005089
	public void SetPointTargetProvider(TargetTracker targetTracker)
	{
		this.pointTargetProvider = targetTracker;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00006E92 File Offset: 0x00005092
	private bool IsTrackingPoint()
	{
		return this.pointTargetProvider != null;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00063C5C File Offset: 0x00061E5C
	public override void StartTravelling()
	{
		try
		{
			base.StartTravelling();
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
		try
		{
			this.velocity = base.transform.forward * this.ejectSpeed + this.killCredit.Velocity() * 0.9f;
			this.noTargetVelocity = base.transform.forward * this.configuration.speed;
			this.thrustStartAction.StartLifetime(this.thrusterStartTime);
			this.inaccurateDiveAction.Start();
			this.cannotMissDiveAction.Stop();
			this.diving = false;
			this.missing = false;
			this.thrustEnabled = false;
			Vector3 vector;
			bool flag = this.HasTargetPoint(out vector);
			if (this.isTrackingTarget)
			{
				this.targetAltitude = vector.y + 200f;
				this.currentTarget.AddTrackingMissile(this);
			}
			else
			{
				this.targetAltitude = base.transform.position.y + 200f;
			}
			if (flag)
			{
				this.phaseThroughCollisionsAtStart = (this.killCredit != null && this.killCredit.aiControlled && Vector3.Distance(vector, base.transform.position) > 30f);
			}
			if (this.light != null)
			{
				this.light.enabled = false;
			}
			if (this.trailParticles != null)
			{
				this.trailParticles.Stop(true);
			}
			this.driftPhase = UnityEngine.Random.Range(0f, 10000f);
			if (this.alwaysTakeDirectPath || (this.isTrackingTarget && this.currentTarget.directJavelinPath))
			{
				this.ForceDirectMode();
			}
		}
		catch (Exception e2)
		{
			ModManager.HandleModException(e2);
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00063E40 File Offset: 0x00062040
	private bool HasTargetPoint(out Vector3 targetPoint)
	{
		targetPoint = Vector3.zero;
		if (this.isTrackingTarget)
		{
			targetPoint = this.currentTarget.targetLockPoint.position;
			return true;
		}
		if (this.IsTrackingPoint())
		{
			targetPoint = this.pointTargetProvider.pointTargetLos;
			return true;
		}
		return false;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00063E94 File Offset: 0x00062094
	protected override void Update()
	{
		if (this.isTrackingTarget && this.currentTarget.CountermeasuresAreActive())
		{
			this.missing = true;
			this.DropTracking();
		}
		if (this.thrustStartAction.TrueDone())
		{
			if (!this.thrustEnabled)
			{
				if (this.light != null)
				{
					this.light.enabled = true;
				}
				if (this.trailParticles != null)
				{
					this.trailParticles.Play(true);
				}
				this.thrustEnabled = true;
				if (!this.isTrackingTarget)
				{
					this.velocity = this.noTargetVelocity;
				}
			}
			Vector3 a;
			if (this.HasTargetPoint(out a))
			{
				Vector3 position = base.transform.position;
				Vector3 rhs = a - position + new Vector3(Mathf.Sin(this.driftPhase + 0.93f * Time.time * this.driftSpeed), 0f, Mathf.Sin(this.driftPhase + 0.87f * Time.time * this.driftSpeed)) * this.maxDrift;
				Vector3 target = Vector3.zero;
				if (!this.diving)
				{
					rhs.y = 0f;
					float value = this.targetAltitude - position.y;
					target = (rhs.normalized + Vector3.up * Mathf.Clamp(value, 0f, 3f)).normalized * this.configuration.speed;
					if (rhs.magnitude < 50f)
					{
						this.StartDiving();
					}
				}
				else if (!this.missing)
				{
					target = (rhs.normalized - this.velocity.normalized * 0.3f).normalized * this.configuration.speed;
					if ((this.isTrackingTarget || this.IsTrackingPoint()) && this.cannotMissDiveAction.Done() && Vector3.Dot(this.velocity, rhs) < 0f)
					{
						this.missing = true;
						this.DropTracking();
					}
				}
				if (!this.missing)
				{
					float num = (this.diving && this.inaccurateDiveAction.TrueDone()) ? this.correctionAcceleration : (this.correctionAcceleration * 0.7f);
					this.velocity = Vector3.MoveTowards(this.velocity, target, num * Time.deltaTime);
				}
				else
				{
					this.velocity += Physics.gravity * Time.deltaTime;
				}
				bool flag = this.diving && Vector3.Dot(base.transform.forward, Vector3.down) > 0.8f;
				this.configuration.damage = (flag ? this.divingDamage : this.damage);
				base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.LookRotation(this.velocity), 700f * Time.deltaTime);
			}
		}
		base.Update();
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00064194 File Offset: 0x00062394
	protected override Projectile.HitType Hit(Ray ray, RaycastHit hitInfo, out bool showHitIndicator)
	{
		if (this.isTrackingTarget && !this.missing && this.phaseThroughCollisionsAtStart && this.travelDistance < 15f)
		{
			showHitIndicator = false;
			return Projectile.HitType.None;
		}
		Projectile.HitType hitType = base.Hit(ray, hitInfo, out showHitIndicator);
		if (hitType == Projectile.HitType.Stop || hitType == Projectile.HitType.StopNoDecal)
		{
			this.DropTracking();
		}
		return hitType;
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00006EA0 File Offset: 0x000050A0
	private void DropTracking()
	{
		if (this.isTrackingTarget)
		{
			this.currentTarget.DropTrackingMissile(this);
			this.isTrackingTarget = false;
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000641E4 File Offset: 0x000623E4
	private void OnDisable()
	{
		try
		{
			this.DropTracking();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00006EBD File Offset: 0x000050BD
	protected override bool Explode(Vector3 position, Vector3 up)
	{
		this.DropTracking();
		return base.Explode(position, up);
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00006ECD File Offset: 0x000050CD
	public override void OnReturnedToPool()
	{
		this.DropTracking();
		base.OnReturnedToPool();
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00006EDB File Offset: 0x000050DB
	public void ForceDirectMode()
	{
		this.diving = true;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00006EE4 File Offset: 0x000050E4
	private void StartDiving()
	{
		this.diving = true;
		this.cannotMissDiveAction.Start();
	}

	// Token: 0x040007A8 RID: 1960
	private const float TURN_SPEED = 700f;

	// Token: 0x040007A9 RID: 1961
	private const float TARGET_ALTITUDE_GAIN = 200f;

	// Token: 0x040007AA RID: 1962
	private const float INACCURATE_ACCELERATION_MULTIPLIER = 0.7f;

	// Token: 0x040007AB RID: 1963
	private const float DIVE_DISTANCE = 50f;

	// Token: 0x040007AC RID: 1964
	private const float VELOCITY_COMPENSATION = 0.3f;

	// Token: 0x040007AD RID: 1965
	private const float AI_PHASE_THROUGH_COLLIDER_DISTANCE = 15f;

	// Token: 0x040007AE RID: 1966
	public float ejectSpeed = 10f;

	// Token: 0x040007AF RID: 1967
	private bool diving;

	// Token: 0x040007B0 RID: 1968
	private bool thrustEnabled;

	// Token: 0x040007B1 RID: 1969
	private bool missing;

	// Token: 0x040007B2 RID: 1970
	private TimedAction thrustStartAction = new TimedAction(0.5f, false);

	// Token: 0x040007B3 RID: 1971
	private TimedAction cannotMissDiveAction = new TimedAction(1f, false);

	// Token: 0x040007B4 RID: 1972
	private TimedAction inaccurateDiveAction = new TimedAction(3f, false);

	// Token: 0x040007B5 RID: 1973
	public float thrusterStartTime = 0.5f;

	// Token: 0x040007B6 RID: 1974
	public bool alwaysTakeDirectPath;

	// Token: 0x040007B7 RID: 1975
	public float damage = 800f;

	// Token: 0x040007B8 RID: 1976
	public float divingDamage = 1500f;

	// Token: 0x040007B9 RID: 1977
	public float correctionAcceleration = 200f;

	// Token: 0x040007BA RID: 1978
	public float maxDrift = 0.5f;

	// Token: 0x040007BB RID: 1979
	public float driftSpeed = 1f;

	// Token: 0x040007BC RID: 1980
	private Vector3 noTargetVelocity;

	// Token: 0x040007BD RID: 1981
	private float driftPhase;

	// Token: 0x040007BE RID: 1982
	private float targetAltitude;

	// Token: 0x040007BF RID: 1983
	private Vehicle currentTarget;

	// Token: 0x040007C0 RID: 1984
	private bool isTrackingTarget;

	// Token: 0x040007C1 RID: 1985
	private bool phaseThroughCollisionsAtStart;

	// Token: 0x040007C2 RID: 1986
	private TargetTracker pointTargetProvider;
}
