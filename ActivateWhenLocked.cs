using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class ActivateWhenLocked : MonoBehaviour
{
	// Token: 0x06001506 RID: 5382 RVA: 0x00010C61 File Offset: 0x0000EE61
	private void Awake()
	{
		this.vehicle = base.GetComponentInParent<Vehicle>();
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x00099950 File Offset: 0x00097B50
	private void LateUpdate()
	{
		try
		{
			bool flag = this.vehicle.IsBeingTrackedByMissile();
			this.isTrackedByMissileObject.SetActive(flag);
			this.isBeingLockedObject.SetActive(!flag && this.vehicle.IsBeingLocked());
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040016E9 RID: 5865
	public GameObject isBeingLockedObject;

	// Token: 0x040016EA RID: 5866
	public GameObject isTrackedByMissileObject;

	// Token: 0x040016EB RID: 5867
	private Vehicle vehicle;
}
