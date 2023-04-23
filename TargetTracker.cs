using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class TargetTracker : MonoBehaviour
{
	// Token: 0x060006BE RID: 1726 RVA: 0x0006001C File Offset: 0x0005E21C
	private void Awake()
	{
		this.dotFov = Mathf.Cos(this.fieldOfView * 0.017453292f * 0.5f);
		this.maxPointTargetDot = Mathf.Cos(this.maxPointTargetAngle * 0.017453292f * 0.5f);
		this.scanAction = new TimedAction(1f / this.scanFrequency, false);
		this.lockTargetAction = new TimedAction(this.lockTime, false);
		this.weapon = base.GetComponentInParent<Weapon>();
		if (this.weapon == null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		this.weapon.targetTracker = this;
		this.botDotFov = ((!(this.weapon is MountedWeapon)) ? 0f : Mathf.Min(this.dotFov, 0.95f));
		this.useVehicleLock = (this.lockType != TargetTracker.LockType.Point);
		this.usePointTarget = (this.lockType > TargetTracker.LockType.Vehicle);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00006567 File Offset: 0x00004767
	public void OnEquip()
	{
		this.useDefaultForwardUserLook = true;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00060110 File Offset: 0x0005E310
	private void Update()
	{
		if (this.weapon != null && this.weapon.user != null && this.scanAction.TrueDone())
		{
			bool flag = this.weapon.UserIsAI() || (this.weapon.user.activeWeapon == this.weapon && this.weapon.user.IsAiming());
			if (!this.wasAiming && flag)
			{
				this.tapAimingAction.Start();
			}
			else if (this.wasAiming && !flag)
			{
				this.useDefaultForwardUserLook = !this.tapAimingAction.TrueDone();
			}
			else if (flag)
			{
				this.useDefaultForwardUserLook = false;
			}
			this.wasAiming = flag;
			this.scanAction.Start();
			this.Scan();
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x000601F4 File Offset: 0x0005E3F4
	private void UpdateTrackingPositionIndicator()
	{
		if (this.trackingPositionIndicator != null && this.HasVehicleTarget())
		{
			this.trackingPositionIndicator.position = this.vehicleTarget.targetLockPoint.position;
		}
		if (this.pointTargetPositionIndicator != null && !this.HasVehicleTarget() && this.HasPointTarget())
		{
			this.pointTargetPositionIndicator.position = this.pointTargetLos;
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00060264 File Offset: 0x0005E464
	private void LateUpdate()
	{
		if (this.updatePositionIndicatorEveryFrame)
		{
			this.UpdateTrackingPositionIndicator();
		}
		if (this.activateWhenLocking != null)
		{
			this.activateWhenLocking.SetActive(this.HasVehicleTarget() && !this.TargetIsLocked());
		}
		if (this.activateWhenLocked != null)
		{
			this.activateWhenLocked.SetActive(this.HasVehicleTarget() && this.TargetIsLocked());
		}
		if (this.activateWhenPointTargetInRange != null)
		{
			this.activateWhenPointTargetInRange.SetActive(this.HasPointTarget() && this.PointTargetIsAvailable());
		}
		if (this.activateWhenPointTargetOutOfRange != null)
		{
			this.activateWhenPointTargetOutOfRange.SetActive(this.HasPointTarget() && !this.PointTargetIsAvailable());
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00006570 File Offset: 0x00004770
	public bool HasVehicleTarget()
	{
		return this.vehicleTarget != null;
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0000657E File Offset: 0x0000477E
	public bool TargetIsLocked()
	{
		return this.HasVehicleTarget() && this.lockTargetAction.TrueDone();
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00006595 File Offset: 0x00004795
	public bool CanFire()
	{
		return !this.requireLockToFire || (this.useVehicleLock && this.TargetIsLocked()) || (this.usePointTarget && this.PointTargetIsAvailable());
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x000065C1 File Offset: 0x000047C1
	public bool HasPointTarget()
	{
		return this.usePointTarget;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x000065C9 File Offset: 0x000047C9
	private bool VehicleTrackerIsEnabled()
	{
		return this.useVehicleLock && (!this.onlyLockWhenWeaponIsEquipped || this.weapon.unholstered) && this.TrackerIsEnabled();
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00060330 File Offset: 0x0005E530
	private bool TrackerIsEnabled()
	{
		if (this.weapon.UserIsPlayer())
		{
			return !this.requireAim || this.weapon.aiming || (this.useMountedWeaponUserLook && this.useDefaultForwardUserLook);
		}
		return this.weapon.user.controller.HasSpottedTarget();
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00060388 File Offset: 0x0005E588
	private Vector3 ClampToMaxAngle(Vector3 direction, float maxAngle)
	{
		Quaternion rotation = base.transform.rotation;
		float num;
		Vector3 axis;
		Quaternion.FromToRotation(rotation * Vector3.forward, direction).ToAngleAxis(out num, out axis);
		num = Mathf.Min(num, this.maxPointTargetAngle);
		Quaternion lhs = Quaternion.AngleAxis(num, axis);
		return lhs * rotation * Vector3.forward;
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x000603E4 File Offset: 0x0005E5E4
	private void Scan()
	{
		if ((!this.weapon.IsMountedWeapon() || (this.useMountedWeaponUserLook && !this.useDefaultForwardUserLook)) && this.weapon.user != null)
		{
			this.trackerOrigin = base.transform.position;
			this.trackerDirection = this.weapon.user.controller.FacingDirection();
		}
		else
		{
			this.trackerOrigin = base.transform.position;
			this.trackerDirection = base.transform.forward;
		}
		if (!this.VehicleTrackerIsEnabled())
		{
			this.vehicleTarget = null;
			this.lockTargetAction.Start();
		}
		else if (!this.HasVehicleTarget() || !this.CanSeeCurrentTarget())
		{
			this.vehicleTarget = this.FindNewTarget();
			this.lockTargetAction.Start();
		}
		if (this.HasVehicleTarget())
		{
			this.vehicleTarget.MarkAsBeingLocked();
		}
		if (this.usePointTarget)
		{
			if (this.TrackerIsEnabled())
			{
				this.ScanPointTarget();
			}
			this.UpdatePointTargetLOS();
		}
		if (!this.updatePositionIndicatorEveryFrame)
		{
			this.UpdateTrackingPositionIndicator();
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x000604F4 File Offset: 0x0005E6F4
	private void ScanPointTarget()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(this.trackerOrigin, this.trackerDirection), out raycastHit, 99999f, 8392705))
		{
			this.pointTarget = raycastHit.point;
			return;
		}
		this.pointTarget = this.trackerOrigin + this.trackerDirection * 99999f;
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x000065F0 File Offset: 0x000047F0
	public bool PointTargetIsAvailable()
	{
		return this.usePointTarget && this.pointTargetIsInDotRange;
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00060554 File Offset: 0x0005E754
	private void UpdatePointTargetLOS()
	{
		Vector3 normalized = (this.pointTarget - this.trackerOrigin).normalized;
		this.pointTargetIsInDotRange = (Vector3.Dot(normalized, base.transform.forward) > this.maxPointTargetDot);
		if (!this.pointTargetIsInDotRange)
		{
			this.pointTargetLos = this.pointTarget;
			return;
		}
		Ray ray = new Ray(this.trackerOrigin, normalized);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 99999f, 8392705))
		{
			this.pointTargetLos = raycastHit.point;
			return;
		}
		this.pointTargetLos = ray.origin + ray.direction * 99999f;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00060604 File Offset: 0x0005E804
	private Vehicle FindNewTarget()
	{
		List<Vehicle> sortedTargets = this.GetSortedTargets();
		if (this.weapon.user != null && this.weapon.user.aiControlled)
		{
			AiActorController aiActorController = this.weapon.user.controller as AiActorController;
			if (aiActorController.HasTarget() && aiActorController.target.IsSeated())
			{
				sortedTargets.Insert(0, aiActorController.target.seat.vehicle);
			}
		}
		foreach (Vehicle vehicle in sortedTargets)
		{
			if (this.CanSeeVehicle(vehicle))
			{
				return vehicle;
			}
		}
		return null;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00006602 File Offset: 0x00004802
	private bool CanSeeCurrentTarget()
	{
		return this.CanSeeVehicle(this.vehicleTarget);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x000606D0 File Offset: 0x0005E8D0
	private bool CanSeeVehicle(Vehicle vehicle)
	{
		if (vehicle.CountermeasuresAreActive())
		{
			return false;
		}
		float num = this.weapon.UserIsAI() ? this.botDotFov : this.dotFov;
		Vector3 vector = vehicle.targetLockPoint.position - base.transform.position;
		Ray ray = new Ray(this.trackerOrigin, vector);
		float magnitude = vector.magnitude;
		return Vector3.Dot(vector / magnitude, this.trackerDirection) > num && !Physics.Raycast(ray, vector.magnitude, 8388609);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00060764 File Offset: 0x0005E964
	private List<Vehicle> GetSortedTargets()
	{
		List<Vehicle> list = new List<Vehicle>(ActorManager.instance.vehicles);
		list.RemoveAll((Vehicle obj) => (!this.lockOntoEmptyVehicles && obj.IsEmpty()) || obj.ownerTeam == this.weapon.user.team || this.weapon.EffectivenessAgainst(obj.targetType) == Weapon.Effectiveness.No);
		Dictionary<Vehicle, float> distanceToVehicle = new Dictionary<Vehicle, float>(list.Count);
		foreach (Vehicle vehicle in list)
		{
			float num = Vector3.Distance(base.transform.position, vehicle.transform.position);
			if (this.weapon.EffectivenessAgainst(vehicle.targetType) == Weapon.Effectiveness.Preferred)
			{
				num -= 500f;
			}
			distanceToVehicle.Add(vehicle, num);
		}
		list.Sort((Vehicle x, Vehicle y) => distanceToVehicle[x].CompareTo(distanceToVehicle[y]));
		return list;
	}

	// Token: 0x040006A2 RID: 1698
	private const int TARGET_LOS_MASK = 8388609;

	// Token: 0x040006A3 RID: 1699
	private const int TARGET_POINT_LOS_MASK = 8392705;

	// Token: 0x040006A4 RID: 1700
	private const float BOTS_MAX_DOT_FOV = 0.95f;

	// Token: 0x040006A5 RID: 1701
	private const float POINT_TARGET_MAX_DISTANCE = 99999f;

	// Token: 0x040006A6 RID: 1702
	public float fieldOfView = 20f;

	// Token: 0x040006A7 RID: 1703
	public float scanFrequency = 5f;

	// Token: 0x040006A8 RID: 1704
	public float lockTime = 2f;

	// Token: 0x040006A9 RID: 1705
	public bool requireAim;

	// Token: 0x040006AA RID: 1706
	public bool requireLockToFire;

	// Token: 0x040006AB RID: 1707
	public bool onlyLockWhenWeaponIsEquipped = true;

	// Token: 0x040006AC RID: 1708
	public bool useMountedWeaponUserLook;

	// Token: 0x040006AD RID: 1709
	public bool lockOntoEmptyVehicles;

	// Token: 0x040006AE RID: 1710
	public float maxPointTargetAngle = 20f;

	// Token: 0x040006AF RID: 1711
	public Transform trackingPositionIndicator;

	// Token: 0x040006B0 RID: 1712
	public Transform pointTargetPositionIndicator;

	// Token: 0x040006B1 RID: 1713
	public GameObject activateWhenLocking;

	// Token: 0x040006B2 RID: 1714
	public GameObject activateWhenLocked;

	// Token: 0x040006B3 RID: 1715
	public GameObject activateWhenPointTargetInRange;

	// Token: 0x040006B4 RID: 1716
	public GameObject activateWhenPointTargetOutOfRange;

	// Token: 0x040006B5 RID: 1717
	private float maxPointTargetDot;

	// Token: 0x040006B6 RID: 1718
	public bool updatePositionIndicatorEveryFrame = true;

	// Token: 0x040006B7 RID: 1719
	public TargetTracker.LockType lockType;

	// Token: 0x040006B8 RID: 1720
	private float dotFov;

	// Token: 0x040006B9 RID: 1721
	private float botDotFov;

	// Token: 0x040006BA RID: 1722
	private bool useDefaultForwardUserLook = true;

	// Token: 0x040006BB RID: 1723
	private bool pointTargetIsInDotRange;

	// Token: 0x040006BC RID: 1724
	[NonSerialized]
	public Vehicle vehicleTarget;

	// Token: 0x040006BD RID: 1725
	private Weapon weapon;

	// Token: 0x040006BE RID: 1726
	private TimedAction scanAction = new TimedAction(0.2f, false);

	// Token: 0x040006BF RID: 1727
	private TimedAction lockTargetAction = new TimedAction(2f, false);

	// Token: 0x040006C0 RID: 1728
	[NonSerialized]
	public bool useVehicleLock;

	// Token: 0x040006C1 RID: 1729
	[NonSerialized]
	public bool usePointTarget;

	// Token: 0x040006C2 RID: 1730
	private Vector3 pointTarget;

	// Token: 0x040006C3 RID: 1731
	[NonSerialized]
	public Vector3 pointTargetLos;

	// Token: 0x040006C4 RID: 1732
	private Vector3 trackerOrigin;

	// Token: 0x040006C5 RID: 1733
	private Vector3 trackerDirection;

	// Token: 0x040006C6 RID: 1734
	private bool wasAiming;

	// Token: 0x040006C7 RID: 1735
	private TimedAction tapAimingAction = new TimedAction(0.3f, false);

	// Token: 0x020000E2 RID: 226
	public enum LockType
	{
		// Token: 0x040006C9 RID: 1737
		Vehicle,
		// Token: 0x040006CA RID: 1738
		Point,
		// Token: 0x040006CB RID: 1739
		VehicleOrPoint
	}
}
