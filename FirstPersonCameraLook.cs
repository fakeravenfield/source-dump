using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class FirstPersonCameraLook : MonoBehaviour
{
	// Token: 0x060014DE RID: 5342 RVA: 0x00098FB8 File Offset: 0x000971B8
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.originPosition = base.transform.localPosition;
		this.vehicle = base.GetComponentInParent<Vehicle>();
		this.cameraSpring = new Spring(50f, 5f, new Vector3(-this.maxHeadBobOffset, -this.maxHeadBobOffset, -this.maxHeadBobOffset), new Vector3(this.maxHeadBobOffset, this.maxHeadBobOffset, this.maxHeadBobOffset), 2);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00010A58 File Offset: 0x0000EC58
	private void OnEnable()
	{
		this.cameraSpring.velocity = Vector3.zero;
		this.cameraSpring.position = Vector3.zero;
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x00099038 File Offset: 0x00097238
	private void LateUpdate()
	{
		if (!this.camera.enabled)
		{
			return;
		}
		this.cameraSpring.AddVelocity(-this.vehicle.acceleration * Time.deltaTime * 0.05f);
		this.cameraSpring.Update();
		Quaternion rotation = FpsActorController.instance.fpCamera.transform.rotation;
		Vector3 b = Quaternion.Inverse(rotation) * this.cameraSpring.position * this.headBobPositionWeight;
		base.transform.localPosition = this.originPosition + b;
		Quaternion rhs = Quaternion.Euler(PlayerFpParent.instance.GetKickLocalEuler() * this.headBobRecoilKickWeight);
		base.transform.rotation = rotation * rhs;
	}

	// Token: 0x040016AD RID: 5805
	private Camera camera;

	// Token: 0x040016AE RID: 5806
	private Spring cameraSpring;

	// Token: 0x040016AF RID: 5807
	private Vector3 originPosition;

	// Token: 0x040016B0 RID: 5808
	public float maxHeadBobOffset = 0.3f;

	// Token: 0x040016B1 RID: 5809
	public float headBobPositionWeight = 0.4f;

	// Token: 0x040016B2 RID: 5810
	public float headBobRecoilKickWeight;

	// Token: 0x040016B3 RID: 5811
	private Vehicle vehicle;
}
