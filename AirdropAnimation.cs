using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class AirdropAnimation : MonoBehaviour
{
	// Token: 0x060009A8 RID: 2472 RVA: 0x000087A5 File Offset: 0x000069A5
	private void Start()
	{
		this.flightAction.Start();
		this.arriveAction.Start();
		base.StartCoroutine(this.SpawnSmokePuffs());
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x000087CA File Offset: 0x000069CA
	public void ActivateCamera()
	{
		FpsActorController.instance.SetOverrideCamera(this.camera);
		this.cameraAction.Start();
		this.controlCamera = true;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
	private void Update()
	{
		if (this.flightAction.TrueDone())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		float num = Mathf.Clamp01(this.flightAction.Elapsed() / 2f) * Mathf.Clamp01(this.flightAction.Remaining() / 2f);
		this.flightParent.localScale = new Vector3(num, num, num);
		float num2 = Mathf.SmoothStep(130f, 0f, Mathf.Clamp01(this.flightAction.Elapsed() / 10f) * Mathf.Clamp01(this.flightAction.Remaining() / 10f));
		this.flightParent.localPosition = new Vector3(0f, 100f + num2, Mathf.Lerp(-1000f, 2000f, this.flightAction.Ratio()));
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0006C09C File Offset: 0x0006A29C
	private void LateUpdate()
	{
		float t = this.controlCamera ? 1f : 0f;
		if (!this.cameraAction.TrueDone())
		{
			t = Mathf.SmoothStep(0f, 1f, this.cameraAction.Ratio());
		}
		this.camera.fieldOfView = Mathf.Lerp(50f, 30f, t);
		Vector3 localEulerAngles = Vector3.Lerp(new Vector3(40f, 110f, 0f), new Vector3(-20f, 150f, 0f), t);
		localEulerAngles.y += 35f * this.arriveAction.Ratio();
		this.cameraParent.localEulerAngles = localEulerAngles;
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000087EE File Offset: 0x000069EE
	private IEnumerator SpawnSmokePuffs()
	{
		yield return new WaitForSeconds(7.5f);
		this.fireSmokeParticles.Play();
		this.smokeDeployAudio.Play();
		yield return new WaitForSeconds(1f);
		this.smokePuffParticles.Play();
		int num;
		for (int i = 0; i < 32; i = num + 1)
		{
			yield return 0;
			Vector3 vector = base.transform.position + UnityEngine.Random.insideUnitSphere.ToGround().normalized * 25f;
			vector.y += 100f * Mathf.Clamp01(0.3f + 0.6f * (1f - (float)i / 32f));
			vector - this.flightParent.position;
			UnityEngine.Object.Instantiate<GameObject>(this.smokePuffPrefab, vector, Quaternion.identity, base.transform);
			num = i;
		}
		yield break;
	}

	// Token: 0x04000A8D RID: 2701
	public const float FLIGHT_HEIGHT = 100f;

	// Token: 0x04000A8E RID: 2702
	public const float FLIGHT_START_END_EXTRA_HEIGHT = 130f;

	// Token: 0x04000A8F RID: 2703
	public const float FLIGHT_SPEED = 100f;

	// Token: 0x04000A90 RID: 2704
	public const float FLIGHT_ETA = 10f;

	// Token: 0x04000A91 RID: 2705
	public const float FLIGHT_LEAVE_TIME = 20f;

	// Token: 0x04000A92 RID: 2706
	private const float SMOKE_FIRE_BEFORE_APPEAR_TIME = 1.5f;

	// Token: 0x04000A93 RID: 2707
	private const float SMOKE_APPEAR_BEFORE_DEPLOY_TIME = 1f;

	// Token: 0x04000A94 RID: 2708
	private const float DISAPPEAR_TIME = 2f;

	// Token: 0x04000A95 RID: 2709
	private const float START_POSITION = -1000f;

	// Token: 0x04000A96 RID: 2710
	private const float LEAVE_POSITION = 2000f;

	// Token: 0x04000A97 RID: 2711
	private const int SMOKE_PUFF_COUNT = 32;

	// Token: 0x04000A98 RID: 2712
	public Transform flightParent;

	// Token: 0x04000A99 RID: 2713
	public Transform cameraParent;

	// Token: 0x04000A9A RID: 2714
	public GameObject smokePuffPrefab;

	// Token: 0x04000A9B RID: 2715
	public ParticleSystem fireSmokeParticles;

	// Token: 0x04000A9C RID: 2716
	public ParticleSystem smokePuffParticles;

	// Token: 0x04000A9D RID: 2717
	public AudioSource smokeDeployAudio;

	// Token: 0x04000A9E RID: 2718
	private TimedAction flightAction = new TimedAction(30f, false);

	// Token: 0x04000A9F RID: 2719
	private TimedAction arriveAction = new TimedAction(10f, false);

	// Token: 0x04000AA0 RID: 2720
	private TimedAction cameraAction = new TimedAction(3f, false);

	// Token: 0x04000AA1 RID: 2721
	public Camera camera;

	// Token: 0x04000AA2 RID: 2722
	private bool controlCamera;
}
