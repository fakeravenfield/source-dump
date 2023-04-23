using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class DirectionalDragForce : MonoBehaviour
{
	// Token: 0x060014E9 RID: 5353 RVA: 0x00010B11 File Offset: 0x0000ED11
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		if (this.directionMatrixOverride == null)
		{
			this.directionMatrixOverride = base.transform;
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x00099354 File Offset: 0x00097554
	private void FixedUpdate()
	{
		try
		{
			Matrix4x4 worldToLocalMatrix = this.directionMatrixOverride.worldToLocalMatrix;
			Matrix4x4 inverse = worldToLocalMatrix.inverse;
			Vector3 vector = worldToLocalMatrix.MultiplyVector(this.rigidbody.velocity);
			Vector3 force = inverse.MultiplyVector(new Vector3(vector.x * -this.sideDrag, vector.y * -this.upwardsDrag, vector.z * -this.forwardDrag));
			if (this.forceApplyPoint != null)
			{
				this.rigidbody.AddForceAtPosition(force, this.forceApplyPoint.position, ForceMode.Acceleration);
			}
			else
			{
				this.rigidbody.AddForce(force, ForceMode.Acceleration);
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040016C1 RID: 5825
	private Rigidbody rigidbody;

	// Token: 0x040016C2 RID: 5826
	public float forwardDrag = 0.1f;

	// Token: 0x040016C3 RID: 5827
	public float sideDrag = 0.5f;

	// Token: 0x040016C4 RID: 5828
	public float upwardsDrag = 0.1f;

	// Token: 0x040016C5 RID: 5829
	public Transform directionMatrixOverride;

	// Token: 0x040016C6 RID: 5830
	public Transform forceApplyPoint;
}
