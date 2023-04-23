using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020002BF RID: 703
public class MoveOnHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060012C1 RID: 4801 RVA: 0x0000ED39 File Offset: 0x0000CF39
	private void Awake()
	{
		this.origin = this.target.anchoredPosition;
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x0000ED4C File Offset: 0x0000CF4C
	public void OnPointerEnter(PointerEventData data)
	{
		this.highlight = true;
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x0000ED55 File Offset: 0x0000CF55
	public void OnPointerExit(PointerEventData data)
	{
		this.highlight = false;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x00090FF8 File Offset: 0x0008F1F8
	private void Update()
	{
		this.delta = Mathf.MoveTowards(this.delta, this.highlight ? 1f : 0f, Time.deltaTime * this.changeSpeed);
		this.target.anchoredPosition = this.origin + this.offset * this.delta;
	}

	// Token: 0x040013FF RID: 5119
	public RectTransform target;

	// Token: 0x04001400 RID: 5120
	public Vector2 offset;

	// Token: 0x04001401 RID: 5121
	public float changeSpeed = 1f;

	// Token: 0x04001402 RID: 5122
	private Vector2 origin;

	// Token: 0x04001403 RID: 5123
	private bool highlight;

	// Token: 0x04001404 RID: 5124
	private float delta;
}
