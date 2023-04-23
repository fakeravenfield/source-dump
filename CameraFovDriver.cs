using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class CameraFovDriver : MonoBehaviour
{
	// Token: 0x060008B9 RID: 2233 RVA: 0x00007B31 File Offset: 0x00005D31
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00007B3F File Offset: 0x00005D3F
	private void Update()
	{
		this.camera.fieldOfView = Mathf.Lerp(this.fov0, this.fov1, this.driver.localScale.x);
	}

	// Token: 0x04000963 RID: 2403
	public float fov0 = 30f;

	// Token: 0x04000964 RID: 2404
	public float fov1 = 60f;

	// Token: 0x04000965 RID: 2405
	public Transform driver;

	// Token: 0x04000966 RID: 2406
	private Camera camera;
}
