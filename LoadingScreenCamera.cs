using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class LoadingScreenCamera : MonoBehaviour
{
	// Token: 0x06000D52 RID: 3410 RVA: 0x0007C5BC File Offset: 0x0007A7BC
	public void Start()
	{
		this.parent = base.transform.parent;
		base.transform.localPosition = this.staticOffset - this.pivotOffset;
		this.parent.position = this.pivotOffset;
		this.startTime = Time.time;
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0007C614 File Offset: 0x0007A814
	public void Update()
	{
		float num = Time.time - this.startTime;
		float t = Mathf.Pow(1f - Mathf.Clamp01(num / 6f), 2f);
		this.parent.position = this.pivotOffset + Vector3.Lerp(Vector3.zero, LoadingScreenCamera.CAMERA_TRAVEL_OFFSET, t);
		this.parent.rotation = Quaternion.Euler(0f, 3f * Mathf.Cos(num * 0.1f), 0f);
	}

	// Token: 0x04000E54 RID: 3668
	private const float ANGLE_AMPLITUDE = 3f;

	// Token: 0x04000E55 RID: 3669
	private const float ANGLE_FREQUENCY = 0.1f;

	// Token: 0x04000E56 RID: 3670
	private static readonly Vector3 CAMERA_TRAVEL_OFFSET = new Vector3(0f, 0.5f, 0f);

	// Token: 0x04000E57 RID: 3671
	private const float CAMERA_TRAVEL_TIME = 6f;

	// Token: 0x04000E58 RID: 3672
	public Vector3 staticOffset = new Vector3(0f, 1.6f, -10f);

	// Token: 0x04000E59 RID: 3673
	public Vector3 pivotOffset;

	// Token: 0x04000E5A RID: 3674
	private Transform parent;

	// Token: 0x04000E5B RID: 3675
	private float startTime;
}
