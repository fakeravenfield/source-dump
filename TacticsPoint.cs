using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002E0 RID: 736
public class TacticsPoint : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x06001381 RID: 4993 RVA: 0x0000F9C5 File Offset: 0x0000DBC5
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		BattlePlanUi.StartDragPoint(this);
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x0000F9D6 File Offset: 0x0000DBD6
	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		BattlePlanUi.EndDrag();
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x0000F9E6 File Offset: 0x0000DBE6
	public void OnPointerEnter(PointerEventData eventData)
	{
		BattlePlanUi.PointerEntered(this);
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x0000F9EE File Offset: 0x0000DBEE
	public void OnPointerExit(PointerEventData eventData)
	{
		BattlePlanUi.PointerExited(this);
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
	private void Awake()
	{
		this.graphic = base.GetComponent<Graphic>();
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x0000FA04 File Offset: 0x0000DC04
	private void Start()
	{
		this.UpdateColor();
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x0000FA0C File Offset: 0x0000DC0C
	public void UpdateColor()
	{
		this.graphic.color = ColorScheme.TeamColor(this.spawnPoint.owner);
	}

	// Token: 0x04001503 RID: 5379
	public SpawnPoint spawnPoint;

	// Token: 0x04001504 RID: 5380
	private Graphic graphic;
}
