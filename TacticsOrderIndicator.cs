using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002DF RID: 735
public class TacticsOrderIndicator : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x0600137E RID: 4990 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
	private void Update()
	{
		this.graphic.raycastTarget = Input.GetMouseButton(1);
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
	public void OnPointerEnter(PointerEventData eventData)
	{
		BattlePlanUi.RemoveOrder(this.order);
	}

	// Token: 0x04001501 RID: 5377
	public Graphic graphic;

	// Token: 0x04001502 RID: 5378
	public Order order;
}
