using System;
using UnityEngine;

// Token: 0x02000345 RID: 837
public class UiElementTracker : MonoBehaviour
{
	// Token: 0x0600152F RID: 5423 RVA: 0x00010E3B File Offset: 0x0000F03B
	private void Awake()
	{
		this.canvas = base.GetComponentInParent<Canvas>();
		this.rectTransform = (RectTransform)base.transform;
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x0009A0C0 File Offset: 0x000982C0
	private void LateUpdate()
	{
		try
		{
			GameManager.GetMainCamera();
			Vector3 vector = GameManager.GetMainCamera().WorldToScreenPoint(this.target.position);
			this.rectTransform.anchoredPosition = vector;
			if (this.activateWhenInFrontOfCamera != null)
			{
				this.activateWhenInFrontOfCamera.SetActive(vector.z > 0f);
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x04001726 RID: 5926
	public Transform target;

	// Token: 0x04001727 RID: 5927
	public GameObject activateWhenInFrontOfCamera;

	// Token: 0x04001728 RID: 5928
	private Canvas canvas;

	// Token: 0x04001729 RID: 5929
	private RectTransform rectTransform;
}
