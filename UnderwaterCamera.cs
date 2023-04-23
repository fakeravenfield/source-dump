using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class UnderwaterCamera : MonoBehaviour
{
	// Token: 0x06001389 RID: 5001 RVA: 0x0000FA29 File Offset: 0x0000DC29
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.whiteTexture = new Texture2D(1, 1);
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x00093424 File Offset: 0x00091624
	private void OnGUI()
	{
		if (this.camera.enabled && WaterLevel.IsInWater(base.transform.position))
		{
			GUI.color = new Color(0.2f, 0.2f, 0.5f, 0.9f);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.whiteTexture);
		}
	}

	// Token: 0x04001505 RID: 5381
	private Camera camera;

	// Token: 0x04001506 RID: 5382
	private Texture2D whiteTexture;
}
