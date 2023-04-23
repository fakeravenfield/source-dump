using System;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class IndependentRotation : MonoBehaviour
{
	// Token: 0x060014F3 RID: 5363 RVA: 0x00010BC6 File Offset: 0x0000EDC6
	private void Awake()
	{
		if (this.onlyWhenSeatTaken == null)
		{
			this.Activate();
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00099654 File Offset: 0x00097854
	private void LateUpdate()
	{
		if (this.onlyWhenSeatTaken != null)
		{
			bool flag = this.onlyWhenSeatTaken.IsOccupied();
			if (flag && !this.active)
			{
				this.Activate();
			}
			else if (!flag && this.active)
			{
				this.Deactivate();
			}
		}
		if (this.active)
		{
			Vector3 vector = Vector3.ProjectOnPlane(this.targetDirection, base.transform.parent.up);
			base.transform.rotation = Quaternion.LookRotation(vector, base.transform.parent.up);
			Debug.DrawRay(base.transform.position, vector * 5f, Color.red);
		}
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x00099704 File Offset: 0x00097904
	private void Activate()
	{
		this.targetDirection = base.transform.forward.ToGround().normalized;
		this.active = true;
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x00010BDC File Offset: 0x0000EDDC
	private void Deactivate()
	{
		this.active = false;
	}

	// Token: 0x040016D7 RID: 5847
	private Vector3 targetDirection;

	// Token: 0x040016D8 RID: 5848
	private bool active;

	// Token: 0x040016D9 RID: 5849
	public Seat onlyWhenSeatTaken;
}
