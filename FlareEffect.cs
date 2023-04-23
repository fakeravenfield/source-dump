using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class FlareEffect : MonoBehaviour
{
	// Token: 0x060008F1 RID: 2289 RVA: 0x00007E13 File Offset: 0x00006013
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x000699E0 File Offset: 0x00067BE0
	private void Start()
	{
		this.rigidbody.velocity = new Vector3(0f, this.launchSpeed, 0f) + UnityEngine.Random.insideUnitSphere.ToGround().normalized * this.launchRandomHorizontalSpeed;
		base.StartCoroutine(this.Deploy());
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00007E21 File Offset: 0x00006021
	private IEnumerator Deploy()
	{
		this.rigidbody.useGravity = true;
		yield return new WaitForSeconds(this.activationTime);
		this.isActivated = true;
		this.activatedObject.SetActive(true);
		this.rigidbody.drag = 0.5f;
		yield break;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00069A3C File Offset: 0x00067C3C
	private void FixedUpdate()
	{
		if (this.isActivated)
		{
			this.rigidbody.AddForce(-Physics.gravity * 0.92f, ForceMode.Acceleration);
		}
		if (this.rigidbody.velocity.y < 0f)
		{
			this.rigidbody.drag = 2f;
		}
	}

	// Token: 0x040009A9 RID: 2473
	private const float DRAG_AIR = 0.5f;

	// Token: 0x040009AA RID: 2474
	private const float DRAG_FALLING = 2f;

	// Token: 0x040009AB RID: 2475
	private const float COUNTERACT_GRAVITY_AMOUNT = 0.92f;

	// Token: 0x040009AC RID: 2476
	public float launchSpeed = 20f;

	// Token: 0x040009AD RID: 2477
	public float launchRandomHorizontalSpeed = 5f;

	// Token: 0x040009AE RID: 2478
	public float activationTime = 0.3f;

	// Token: 0x040009AF RID: 2479
	public GameObject activatedObject;

	// Token: 0x040009B0 RID: 2480
	private bool isActivated;

	// Token: 0x040009B1 RID: 2481
	private Rigidbody rigidbody;
}
