using System;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class Shifter : MonoBehaviour
{
	// Token: 0x06001528 RID: 5416 RVA: 0x00010DB3 File Offset: 0x0000EFB3
	private void Awake()
	{
		this.originalEuler = base.transform.localEulerAngles;
		this.car = base.GetComponentInParent<ArcadeCar>();
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x00099F58 File Offset: 0x00098158
	private void Update()
	{
		this.shiftAmount = Mathf.MoveTowards(this.shiftAmount, this.car.inReverseGear ? 0f : 1f, this.shiftSpeed * Time.deltaTime);
		base.transform.localEulerAngles = this.originalEuler + this.shiftForwardRotation * this.shiftAmount;
	}

	// Token: 0x04001718 RID: 5912
	public Vector3 shiftForwardRotation = new Vector3(30f, 0f, 0f);

	// Token: 0x04001719 RID: 5913
	public float shiftSpeed = 5f;

	// Token: 0x0400171A RID: 5914
	private Vector3 originalEuler;

	// Token: 0x0400171B RID: 5915
	private float shiftAmount;

	// Token: 0x0400171C RID: 5916
	private ArcadeCar car;
}
