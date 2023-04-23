using System;
using UnityEngine;

// Token: 0x02000330 RID: 816
[RequireComponent(typeof(Rigidbody))]
public class FloatingRigidbody : MonoBehaviour
{
	// Token: 0x060014EF RID: 5359 RVA: 0x00010B62 File Offset: 0x0000ED62
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.airDrag = this.rigidbody.drag;
		this.airAngularDrag = this.rigidbody.angularDrag;
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x00099534 File Offset: 0x00097734
	private void FixedUpdate()
	{
		this.samplersInWater = FloatingRigidbody.ApplyFloatingPhysics(this.floatingSamplers, this.rigidbody, this.floatDepth, this.floatAcceleration);
		bool flag = this.samplersInWater >= Mathf.Min(2, this.floatingSamplers.Length);
		if (flag && !this.waterDragApplied)
		{
			this.rigidbody.drag = this.waterDrag;
			this.rigidbody.angularDrag = this.waterAngularDrag;
		}
		else if (!flag && this.waterDragApplied)
		{
			this.rigidbody.drag = this.airDrag;
			this.rigidbody.angularDrag = this.airAngularDrag;
		}
		this.waterDragApplied = flag;
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000995E4 File Offset: 0x000977E4
	public static int ApplyFloatingPhysics(Transform[] floatingSamplers, Rigidbody rigidbody, float floatDepth, float floatAcceleration)
	{
		int num = 0;
		foreach (Transform transform in floatingSamplers)
		{
			float num2;
			if (WaterLevel.IsInWater(transform.position, out num2))
			{
				float d = Mathf.Clamp01(num2 / floatDepth) / (float)floatingSamplers.Length;
				rigidbody.AddForceAtPosition(Vector3.up * floatAcceleration * d, transform.position + Vector3.up, ForceMode.Acceleration);
				num++;
			}
		}
		return num;
	}

	// Token: 0x040016CC RID: 5836
	private const int MIN_SAMPLERS_UNDERWATER_APPLY_DRAG = 2;

	// Token: 0x040016CD RID: 5837
	public Transform[] floatingSamplers;

	// Token: 0x040016CE RID: 5838
	public float floatAcceleration = 10f;

	// Token: 0x040016CF RID: 5839
	public float floatDepth = 0.5f;

	// Token: 0x040016D0 RID: 5840
	public float waterDrag = 2f;

	// Token: 0x040016D1 RID: 5841
	public float waterAngularDrag = 2f;

	// Token: 0x040016D2 RID: 5842
	private float airDrag;

	// Token: 0x040016D3 RID: 5843
	private float airAngularDrag;

	// Token: 0x040016D4 RID: 5844
	private Rigidbody rigidbody;

	// Token: 0x040016D5 RID: 5845
	[NonSerialized]
	public int samplersInWater;

	// Token: 0x040016D6 RID: 5846
	[NonSerialized]
	public bool waterDragApplied;
}
