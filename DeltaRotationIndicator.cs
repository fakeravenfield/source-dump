using System;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class DeltaRotationIndicator : MonoBehaviour
{
	// Token: 0x0600119D RID: 4509 RVA: 0x0008C8B8 File Offset: 0x0008AAB8
	private void Update()
	{
		try
		{
			Quaternion quaternion = this.target.rotation * Quaternion.Inverse(this.relativeTo.rotation);
			base.transform.localEulerAngles = this.indicatorRotationAxis * Vector3.Dot(quaternion.eulerAngles, this.targetRotationAxis);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040012B3 RID: 4787
	public Transform target;

	// Token: 0x040012B4 RID: 4788
	public Transform relativeTo;

	// Token: 0x040012B5 RID: 4789
	public Vector3 targetRotationAxis = new Vector3(0f, 1f, 0f);

	// Token: 0x040012B6 RID: 4790
	public Vector3 indicatorRotationAxis = new Vector3(0f, 0f, 1f);
}
