using System;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class BackgroundCamera : MimicCamera
{
	// Token: 0x06000EFF RID: 3839 RVA: 0x0000BF1C File Offset: 0x0000A11C
	public static void SetProjectionMatrix(Matrix4x4 projection)
	{
		BackgroundCamera.instance.hasCustomProjectionMatrix = true;
		BackgroundCamera.instance.customProjectionMatrix = projection;
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x0000BF34 File Offset: 0x0000A134
	protected override void Awake()
	{
		BackgroundCamera.instance = this;
		base.Awake();
		this.camera.allowHDR = Options.GetToggle(OptionToggle.Id.HDR);
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x0000BF53 File Offset: 0x0000A153
	private void OnPreCull()
	{
		if (this.hasCustomProjectionMatrix)
		{
			this.camera.projectionMatrix = this.customProjectionMatrix;
		}
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0000BF6E File Offset: 0x0000A16E
	private void OnPostRender()
	{
		this.camera.ResetProjectionMatrix();
		this.hasCustomProjectionMatrix = false;
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0000BF82 File Offset: 0x0000A182
	protected override void LateUpdate()
	{
		if ((this.mimicMain ? GameManager.GetMainCamera() : this.parentCamera).clearFlags != CameraClearFlags.Depth)
		{
			this.camera.enabled = false;
			return;
		}
		base.LateUpdate();
	}

	// Token: 0x04000FD3 RID: 4051
	public static BackgroundCamera instance;

	// Token: 0x04000FD4 RID: 4052
	private bool hasCustomProjectionMatrix;

	// Token: 0x04000FD5 RID: 4053
	private Matrix4x4 customProjectionMatrix;
}
