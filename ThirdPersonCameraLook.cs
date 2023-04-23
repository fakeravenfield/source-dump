using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class ThirdPersonCameraLook : MonoBehaviour
{
	// Token: 0x060014E5 RID: 5349 RVA: 0x00099190 File Offset: 0x00097390
	private void Awake()
	{
		Vector3 v = base.transform.position - base.transform.parent.position;
		this.localOffset = Matrix4x4.TRS(Vector3.zero, base.transform.rotation, Vector3.one).inverse * v;
		this.camera = base.GetComponent<Camera>();
		this.vehicle = base.GetComponentInParent<Vehicle>();
		this.bobDistanceMultiplier = this.localOffset.magnitude * 0.05f;
		float num = this.bobDistanceMultiplier * 5f;
		this.cameraSpring = new Spring(20f, 5f, -new Vector3(num, num, num), new Vector3(num, num, num), 1);
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x00010ACA File Offset: 0x0000ECCA
	private void OnEnable()
	{
		this.cameraSpring.velocity = Vector3.zero;
		this.cameraSpring.position = Vector3.zero;
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x00099260 File Offset: 0x00097460
	private void LateUpdate()
	{
		if (!this.camera.enabled)
		{
			return;
		}
		this.cameraSpring.AddVelocity(this.vehicle.acceleration * Time.deltaTime);
		this.cameraSpring.Update();
		Quaternion rotation;
		if (this.useWorldUpDirection)
		{
			rotation = Quaternion.LookRotation(FpsActorController.instance.actor.controller.FacingDirection());
		}
		else
		{
			rotation = Quaternion.LookRotation(FpsActorController.instance.actor.controller.FacingDirection(), base.transform.parent.up);
		}
		Vector3 b = rotation * this.localOffset + this.cameraSpring.position * this.positionBobWeight * this.bobDistanceMultiplier;
		base.transform.rotation = rotation;
		base.transform.position = base.transform.parent.position + b;
	}

	// Token: 0x040016B8 RID: 5816
	private const float BOB_PER_DISTANCE = 0.05f;

	// Token: 0x040016B9 RID: 5817
	private const float MAX_SPRING_OFFSET = 5f;

	// Token: 0x040016BA RID: 5818
	private Vehicle vehicle;

	// Token: 0x040016BB RID: 5819
	private Vector3 localOffset;

	// Token: 0x040016BC RID: 5820
	private Camera camera;

	// Token: 0x040016BD RID: 5821
	public bool useWorldUpDirection = true;

	// Token: 0x040016BE RID: 5822
	public float positionBobWeight = 1f;

	// Token: 0x040016BF RID: 5823
	private Spring cameraSpring;

	// Token: 0x040016C0 RID: 5824
	private float bobDistanceMultiplier = 1f;
}
