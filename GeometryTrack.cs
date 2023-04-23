using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class GeometryTrack : MonoBehaviour
{
	// Token: 0x06000907 RID: 2311 RVA: 0x00007EDE File Offset: 0x000060DE
	public void Awake()
	{
		this.renderer = base.GetComponent<SkinnedMeshRenderer>();
		if (this.speedSampleTransform == null)
		{
			this.speedSampleTransform = base.transform;
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00007F06 File Offset: 0x00006106
	private void Update()
	{
		this.phase = (this.phase + 10000f + this.speed * Time.deltaTime / this.linkDistance) % 1f;
		this.renderer.SetBlendShapeWeight(0, this.phase);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00069D80 File Offset: 0x00067F80
	private void FixedUpdate()
	{
		Vector3 pointVelocity = this.vehicleRigidbody.GetPointVelocity(this.speedSampleTransform.position);
		this.speed = Vector3.Dot(pointVelocity, this.speedSampleTransform.forward);
	}

	// Token: 0x040009D0 RID: 2512
	private SkinnedMeshRenderer renderer;

	// Token: 0x040009D1 RID: 2513
	public Transform speedSampleTransform;

	// Token: 0x040009D2 RID: 2514
	public float linkDistance = 0.3f;

	// Token: 0x040009D3 RID: 2515
	public Rigidbody vehicleRigidbody;

	// Token: 0x040009D4 RID: 2516
	private float speed;

	// Token: 0x040009D5 RID: 2517
	private float phase;
}
