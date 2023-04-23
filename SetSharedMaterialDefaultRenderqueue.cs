using System;
using UnityEngine;

// Token: 0x0200030B RID: 779
[ExecuteInEditMode]
public class SetSharedMaterialDefaultRenderqueue : MonoBehaviour
{
	// Token: 0x06001440 RID: 5184 RVA: 0x00010252 File Offset: 0x0000E452
	[ExecuteInEditMode]
	private void OnEnable()
	{
		base.GetComponent<Renderer>().sharedMaterial.renderQueue = -1;
		Debug.Log("Reset render queue");
	}
}
