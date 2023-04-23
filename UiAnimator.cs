using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class UiAnimator : MonoBehaviour
{
	// Token: 0x0600152D RID: 5421 RVA: 0x0009A064 File Offset: 0x00098264
	private void Update()
	{
		float time = Time.time % this.periodTime / this.periodTime;
		float d = this.curve.Evaluate(time);
		base.transform.localScale = d * this.scale;
		base.transform.localEulerAngles = d * this.rotation;
	}

	// Token: 0x04001721 RID: 5921
	public Vector3 scale = Vector3.one;

	// Token: 0x04001722 RID: 5922
	public Vector3 rotation = Vector3.zero;

	// Token: 0x04001723 RID: 5923
	public AnimationCurve curve;

	// Token: 0x04001724 RID: 5924
	public float periodTime;

	// Token: 0x04001725 RID: 5925
	private TimedAction action;
}
