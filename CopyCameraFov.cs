using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class CopyCameraFov : MonoBehaviour
{
	// Token: 0x060006A2 RID: 1698 RVA: 0x000063C6 File Offset: 0x000045C6
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x000063D4 File Offset: 0x000045D4
	private void LateUpdate()
	{
		this.camera.fieldOfView = this.fovTransform.localScale.x;
	}

	// Token: 0x04000689 RID: 1673
	private Camera camera;

	// Token: 0x0400068A RID: 1674
	public Transform fovTransform;
}
