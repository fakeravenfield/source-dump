using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class DetectionUi : MonoBehaviour
{
	// Token: 0x060011AF RID: 4527 RVA: 0x0008CC98 File Offset: 0x0008AE98
	private void Awake()
	{
		DetectionUi.instance = this;
		this.blips = new DetectionBlip[5];
		this.indicators = new DetectionIndicator[5];
		for (int i = 0; i < 5; i++)
		{
			DetectionBlip component = UnityEngine.Object.Instantiate<GameObject>(this.blipPrefab, base.transform).GetComponent<DetectionBlip>();
			this.blips[i] = component;
			DetectionIndicator component2 = UnityEngine.Object.Instantiate<GameObject>(this.indicatorPrefab, this.indicatorPanel).GetComponent<DetectionIndicator>();
			this.indicators[i] = component2;
		}
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x0008CD10 File Offset: 0x0008AF10
	public static void StartDetection(AiActorController ai)
	{
		if (DetectionUi.instance.IsTrackingDetectionOf(ai))
		{
			return;
		}
		DetectionUi.instance.blips[DetectionUi.instance.nextIndex].Activate(ai.actor);
		DetectionUi.instance.indicators[DetectionUi.instance.nextIndex].Activate(ai);
		DetectionUi.instance.nextIndex = (DetectionUi.instance.nextIndex + 1) % 5;
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x0008CD80 File Offset: 0x0008AF80
	public bool IsTrackingDetectionOf(AiActorController ai)
	{
		return this.indicators.Any((DetectionIndicator i) => i.target == ai);
	}

	// Token: 0x040012C6 RID: 4806
	private const int POOL_SIZE = 5;

	// Token: 0x040012C7 RID: 4807
	public static DetectionUi instance;

	// Token: 0x040012C8 RID: 4808
	public GameObject blipPrefab;

	// Token: 0x040012C9 RID: 4809
	public GameObject indicatorPrefab;

	// Token: 0x040012CA RID: 4810
	public Transform indicatorPanel;

	// Token: 0x040012CB RID: 4811
	private DetectionBlip[] blips;

	// Token: 0x040012CC RID: 4812
	private DetectionIndicator[] indicators;

	// Token: 0x040012CD RID: 4813
	private int nextIndex;
}
