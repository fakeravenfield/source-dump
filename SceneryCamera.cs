using System;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class SceneryCamera : MonoBehaviour
{
	// Token: 0x06000C17 RID: 3095 RVA: 0x00077C94 File Offset: 0x00075E94
	private void Awake()
	{
		SceneryCamera.instance = this;
		this.camera = base.GetComponent<Camera>();
		this.cullingMask = this.camera.cullingMask;
		this.camera.cullingMask = 0;
		this.camera.clearFlags = CameraClearFlags.Color;
		base.gameObject.tag = "MainCamera";
		AudioListener component = base.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00009F70 File Offset: 0x00008170
	private void Start()
	{
		PostProcessingManager.RegisterWorldCamera(this.camera);
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00009F7D File Offset: 0x0000817D
	public void EnableRender()
	{
		this.camera.cullingMask = this.cullingMask;
		this.camera.clearFlags = CameraClearFlags.Skybox;
	}

	// Token: 0x04000D26 RID: 3366
	public static SceneryCamera instance;

	// Token: 0x04000D27 RID: 3367
	[NonSerialized]
	public Camera camera;

	// Token: 0x04000D28 RID: 3368
	private int cullingMask;
}
