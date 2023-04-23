using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class PlayerRecoilCamera : MonoBehaviour
{
	// Token: 0x0600096F RID: 2415 RVA: 0x000084CE File Offset: 0x000066CE
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x000084DC File Offset: 0x000066DC
	private void LateUpdate()
	{
		if (this.camera != null && this.camera.enabled)
		{
			this.SetupProjectionMatrix();
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x000084FF File Offset: 0x000066FF
	private void OnPreCull()
	{
		this.camera.projectionMatrix = this.projectionMatrix;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0006B1C0 File Offset: 0x000693C0
	private void SetupProjectionMatrix()
	{
		if (this.activePlayerSeat != null && (!this.activePlayerSeat.IsOccupied() || this.activePlayerSeat.occupant.aiControlled))
		{
			return;
		}
		Vector3 springLocalPosition = PlayerFpParent.instance.GetSpringLocalPosition();
		Vector2 vector = springLocalPosition;
		vector += this.zAxis * springLocalPosition.z;
		vector *= (float)Screen.height * this.weight;
		if (this.camera != null)
		{
			float d = this.camera.nearClipPlane * 2f * Mathf.Tan(this.camera.fieldOfView / 2f * 0.017453292f) / (float)Screen.height;
			Vector2 perspectiveOffset = vector * d;
			this.projectionMatrix = this.SetVanishingPoint(this.camera, perspectiveOffset);
			float d2 = BackgroundCamera.instance.camera.nearClipPlane * 2f * Mathf.Tan(BackgroundCamera.instance.camera.fieldOfView / 2f * 0.017453292f) / (float)Screen.height;
			Vector2 perspectiveOffset2 = vector * d2;
			BackgroundCamera.SetProjectionMatrix(this.SetVanishingPoint(BackgroundCamera.instance.camera, perspectiveOffset2));
		}
		if (this.cameraHud != null)
		{
			this.cameraHud.anchoredPosition = vector;
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00008512 File Offset: 0x00006712
	private void OnPostRender()
	{
		this.camera.ResetProjectionMatrix();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0006B318 File Offset: 0x00069518
	private Matrix4x4 SetVanishingPoint(Camera cam, Vector2 perspectiveOffset)
	{
		Matrix4x4 matrix4x = cam.projectionMatrix;
		float num = 2f * cam.nearClipPlane / matrix4x.m00;
		float num2 = 2f * cam.nearClipPlane / matrix4x.m11;
		float num3 = -num / 2f - perspectiveOffset.x;
		float right = num3 + num;
		float num4 = -num2 / 2f - perspectiveOffset.y;
		float top = num4 + num2;
		return PlayerRecoilCamera.PerspectiveOffCenter(num3, right, num4, top, cam.nearClipPlane, cam.farClipPlane);
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0006B394 File Offset: 0x00069594
	public static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
	{
		float value = 2f * near / (right - left);
		float value2 = 2f * near / (top - bottom);
		float value3 = (right + left) / (right - left);
		float value4 = (top + bottom) / (top - bottom);
		float value5 = -(far + near) / (far - near);
		float value6 = -(2f * far * near) / (far - near);
		float value7 = -1f;
		Matrix4x4 result = default(Matrix4x4);
		result[0, 0] = value;
		result[0, 1] = 0f;
		result[0, 2] = value3;
		result[0, 3] = 0f;
		result[1, 0] = 0f;
		result[1, 1] = value2;
		result[1, 2] = value4;
		result[1, 3] = 0f;
		result[2, 0] = 0f;
		result[2, 1] = 0f;
		result[2, 2] = value5;
		result[2, 3] = value6;
		result[3, 0] = 0f;
		result[3, 1] = 0f;
		result[3, 2] = value7;
		result[3, 3] = 0f;
		return result;
	}

	// Token: 0x04000A54 RID: 2644
	public Seat activePlayerSeat;

	// Token: 0x04000A55 RID: 2645
	private Camera camera;

	// Token: 0x04000A56 RID: 2646
	public RectTransform cameraHud;

	// Token: 0x04000A57 RID: 2647
	public float weight = 1f;

	// Token: 0x04000A58 RID: 2648
	public Vector2 zAxis = Vector2.zero;

	// Token: 0x04000A59 RID: 2649
	private Matrix4x4 projectionMatrix;

	// Token: 0x04000A5A RID: 2650
	private float fieldOfView = 60f;
}
