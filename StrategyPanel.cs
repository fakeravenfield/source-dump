using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020002DE RID: 734
public class StrategyPanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x0600137C RID: 4988 RVA: 0x0000F984 File Offset: 0x0000DB84
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			StrategyUi.ClickedPoint(eventData.position);
			return;
		}
		StrategyUi.OpenContextMenu(eventData.position);
	}
}
