using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B7 RID: 695
public class MinimapBlip : MonoBehaviour
{
	// Token: 0x06001287 RID: 4743 RVA: 0x0000EA73 File Offset: 0x0000CC73
	private void Awake()
	{
		this.image = base.GetComponent<RawImage>();
		this.image.rectTransform.anchoredPosition = Vector2.zero;
		base.transform.SetAsFirstSibling();
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x0000CB1B File Offset: 0x0000AD1B
	protected virtual Vector3 GetWorldPosition()
	{
		return Vector3.zero;
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x00004B4B File Offset: 0x00002D4B
	protected virtual float GetYAngle()
	{
		return 0f;
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x0000257D File Offset: 0x0000077D
	protected virtual bool IsVisible()
	{
		return false;
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x0008FE40 File Offset: 0x0008E040
	protected virtual void LateUpdate()
	{
	}

	// Token: 0x040013BD RID: 5053
	private const float CLAMP_PERCENTAGE = 0.03f;

	// Token: 0x040013BE RID: 5054
	protected RawImage image;

	// Token: 0x040013BF RID: 5055
	protected bool updateTransformWhenInvisible;
}
