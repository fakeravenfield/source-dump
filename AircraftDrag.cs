using System;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class AircraftDrag : MonoBehaviour
{
	// Token: 0x060014D8 RID: 5336 RVA: 0x000109FE File Offset: 0x0000EBFE
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.baseAngularDrag = this.rigidbody.angularDrag;
		this.baseDrag = this.rigidbody.drag;
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x00098D34 File Offset: 0x00096F34
	private void FixedUpdate()
	{
		Vector3 vector = base.transform.worldToLocalMatrix.MultiplyVector(this.rigidbody.velocity);
		Vector3 vector2 = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
		this.rigidbody.drag = this.baseDrag + vector2.x * this.sideDragGain + vector2.y * this.upDragGain + vector2.z * this.forwardDragGain;
		this.rigidbody.angularDrag = this.baseAngularDrag + vector2.x * this.sideAngularDragGain + vector2.y * this.upAngularDragGain + vector2.z * this.forwardAngularDragGain;
	}

	// Token: 0x0400169D RID: 5789
	public float forwardDragGain;

	// Token: 0x0400169E RID: 5790
	public float sideDragGain;

	// Token: 0x0400169F RID: 5791
	public float upDragGain;

	// Token: 0x040016A0 RID: 5792
	public float forwardAngularDragGain;

	// Token: 0x040016A1 RID: 5793
	public float sideAngularDragGain;

	// Token: 0x040016A2 RID: 5794
	public float upAngularDragGain;

	// Token: 0x040016A3 RID: 5795
	private Rigidbody rigidbody;

	// Token: 0x040016A4 RID: 5796
	private float baseAngularDrag;

	// Token: 0x040016A5 RID: 5797
	private float baseDrag;
}
