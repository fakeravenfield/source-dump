using System;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class TestPostProcessing : MonoBehaviour
{
	// Token: 0x06001159 RID: 4441 RVA: 0x0000DA3C File Offset: 0x0000BC3C
	public void Start()
	{
		PostProcessingManager.RegisterFirstPersonViewModelCamera(this.fpModelCamera);
		PostProcessingManager.RegisterWorldCamera(this.worldCamera);
	}

	// Token: 0x0400127F RID: 4735
	public Camera fpModelCamera;

	// Token: 0x04001280 RID: 4736
	public Camera worldCamera;
}
