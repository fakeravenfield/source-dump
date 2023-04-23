using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class UiAnimatorExtended : MonoBehaviour
{
	// Token: 0x060000AF RID: 175 RVA: 0x0003FA64 File Offset: 0x0003DC64
	private void Update()
	{
		float time = (Time.time + this.phaseOffset) % this.periodTime / this.periodTime;
		float t = this.curve.Evaluate(time);
		base.transform.localScale = Vector3.Lerp(this.startScale, this.endScale, t);
		base.transform.localEulerAngles = Vector3.Lerp(this.startRotation, this.endRotation, t);
	}

	// Token: 0x0400006A RID: 106
	public Vector3 startScale = Vector3.one;

	// Token: 0x0400006B RID: 107
	public Vector3 endScale = Vector3.one;

	// Token: 0x0400006C RID: 108
	public Vector3 startRotation = Vector3.zero;

	// Token: 0x0400006D RID: 109
	public Vector3 endRotation = Vector3.zero;

	// Token: 0x0400006E RID: 110
	public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400006F RID: 111
	public float periodTime = 1f;

	// Token: 0x04000070 RID: 112
	public float phaseOffset;
}
