using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class InheritDeltaRotation : MonoBehaviour
{
	// Token: 0x06000969 RID: 2409 RVA: 0x0000849A File Offset: 0x0000669A
	private void Start()
	{
		this.initialLocalEuler = base.transform.localEulerAngles;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0006B080 File Offset: 0x00069280
	private void LateUpdate()
	{
		try
		{
			base.transform.localEulerAngles = Vector3.Scale(this.target.localEulerAngles, this.multiplier) + this.initialLocalEuler;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x04000A51 RID: 2641
	public Transform target;

	// Token: 0x04000A52 RID: 2642
	public Vector3 multiplier = Vector3.one;

	// Token: 0x04000A53 RID: 2643
	private Vector3 initialLocalEuler;
}
