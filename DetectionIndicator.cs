using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000297 RID: 663
public class DetectionIndicator : MonoBehaviour
{
	// Token: 0x060011AA RID: 4522 RVA: 0x0000DE61 File Offset: 0x0000C061
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0000DE6F File Offset: 0x0000C06F
	public void Activate(AiActorController ai)
	{
		this.target = ai;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0008CB64 File Offset: 0x0008AD64
	private void LateUpdate()
	{
		if (this.target == null || this.target.actor.dead || !this.target.slowTargetDetection || (this.target.targetDetectionProgress == 0f && !this.target.HasTarget()))
		{
			base.gameObject.SetActive(false);
			this.target = null;
			return;
		}
		this.SetProgress(this.target.targetDetectionProgress);
		Vector3 vector = FpsActorController.instance.GetActiveCamera().transform.worldToLocalMatrix.MultiplyPoint(this.target.actor.Position());
		float z = -Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		this.rectTransform.localEulerAngles = new Vector3(0f, 0f, z);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x0008CC44 File Offset: 0x0008AE44
	private void SetProgress(float progress)
	{
		float num = 1f / Mathf.Clamp(progress, 0.01f, 1f) - 1f;
		this.mask.uvRect = new Rect(-num, 0f, num * 2f + 1f, 1f);
	}

	// Token: 0x040012C2 RID: 4802
	public RawImage mask;

	// Token: 0x040012C3 RID: 4803
	public Graphic chevronBase;

	// Token: 0x040012C4 RID: 4804
	private RectTransform rectTransform;

	// Token: 0x040012C5 RID: 4805
	[NonSerialized]
	public AiActorController target;
}
