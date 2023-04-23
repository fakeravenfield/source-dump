using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class InheritDeltaPosition : MonoBehaviour
{
	// Token: 0x06000966 RID: 2406 RVA: 0x0006AFA8 File Offset: 0x000691A8
	private void Start()
	{
		this.localOrigin = base.transform.localPosition;
		this.deltaLocalPosition = base.transform.parent.worldToLocalMatrix.MultiplyPoint(this.target.position) - this.localOrigin;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0006AFFC File Offset: 0x000691FC
	private void LateUpdate()
	{
		try
		{
			Vector3 a = base.transform.parent.worldToLocalMatrix.MultiplyPoint(this.target.position) - this.localOrigin - this.deltaLocalPosition;
			base.transform.localPosition = this.localOrigin + Vector3.Scale(a, this.multiplier);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x04000A4D RID: 2637
	public Transform target;

	// Token: 0x04000A4E RID: 2638
	public Vector3 multiplier = Vector3.one;

	// Token: 0x04000A4F RID: 2639
	private Vector3 localOrigin;

	// Token: 0x04000A50 RID: 2640
	private Vector3 deltaLocalPosition;
}
