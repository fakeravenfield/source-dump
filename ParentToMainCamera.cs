using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class ParentToMainCamera : MonoBehaviour
{
	// Token: 0x0600096C RID: 2412 RVA: 0x0006B0D4 File Offset: 0x000692D4
	private void LateUpdate()
	{
		Camera camera = null;
		if (SpectatorCamera.instance != null && SpectatorCamera.instance.camera.enabled && SpectatorCamera.instance.camera.gameObject.activeInHierarchy)
		{
			camera = SpectatorCamera.instance.camera;
		}
		else if (FpsActorController.instance != null && FpsActorController.instance.GetActiveCamera() != null)
		{
			camera = FpsActorController.instance.GetActiveCamera();
		}
		else
		{
			Camera mainCamera = GameManager.GetMainCamera();
			if (mainCamera != null)
			{
				camera = mainCamera;
			}
		}
		if (camera != null && camera.gameObject.activeInHierarchy && camera.transform != base.transform.parent)
		{
			base.transform.parent = camera.transform;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
		}
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x000084C0 File Offset: 0x000066C0
	public void ForceDetatch()
	{
		base.transform.parent = null;
	}
}
