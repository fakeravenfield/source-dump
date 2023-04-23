using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class MimicCamera : MonoBehaviour
{
	// Token: 0x06000FDE RID: 4062 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
	protected virtual void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x000854FC File Offset: 0x000836FC
	protected virtual void LateUpdate()
	{
		Camera camera = this.mimicMain ? GameManager.GetMainCamera() : this.parentCamera;
		this.camera.fieldOfView = camera.fieldOfView;
		this.camera.enabled = camera.enabled;
		if (this.mimicMain)
		{
			base.transform.position = camera.transform.position;
			base.transform.rotation = camera.transform.rotation;
		}
	}

	// Token: 0x04001076 RID: 4214
	public bool mimicMain;

	// Token: 0x04001077 RID: 4215
	[NonSerialized]
	public Camera camera;

	// Token: 0x04001078 RID: 4216
	public Camera parentCamera;
}
