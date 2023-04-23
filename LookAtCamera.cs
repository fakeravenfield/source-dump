using System;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x060014E2 RID: 5346 RVA: 0x00010A98 File Offset: 0x0000EC98
	private void Awake()
	{
		this.rotation = base.transform.rotation;
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x00099108 File Offset: 0x00097308
	private void LateUpdate()
	{
		try
		{
			if (this.camera.enabled)
			{
				Quaternion b = Quaternion.LookRotation(this.target.position - base.transform.position);
				this.rotation = Quaternion.Lerp(this.rotation, b, this.turnSpeed * Time.deltaTime);
				base.transform.rotation = this.rotation;
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040016B4 RID: 5812
	public Transform target;

	// Token: 0x040016B5 RID: 5813
	public float turnSpeed = 10f;

	// Token: 0x040016B6 RID: 5814
	private Camera camera;

	// Token: 0x040016B7 RID: 5815
	private Quaternion rotation;
}
