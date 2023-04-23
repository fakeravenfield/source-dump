using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class BattlePlanUi : MonoBehaviour
{
	// Token: 0x06001173 RID: 4467 RVA: 0x0000DB32 File Offset: 0x0000BD32
	public static void StartDragPoint(TacticsPoint point)
	{
		BattlePlanUi.instance.dragSource = point;
		BattlePlanUi.instance.isDragging = true;
		BattlePlanUi.instance.attackArrowPreviewTransform.SetAsLastSibling();
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0008BF58 File Offset: 0x0008A158
	public static void EndDrag()
	{
		if (BattlePlanUi.instance.dragSource != null && BattlePlanUi.instance.dragDestination != null)
		{
			BattlePlanUi.instance.DragCompleted(BattlePlanUi.instance.dragSource, BattlePlanUi.instance.dragDestination);
		}
		BattlePlanUi.instance.isDragging = false;
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x0000DB59 File Offset: 0x0000BD59
	public static void PointerEntered(TacticsPoint point)
	{
		BattlePlanUi.instance.dragDestination = point;
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0000DB66 File Offset: 0x0000BD66
	public static void PointerExited(TacticsPoint point)
	{
		if (BattlePlanUi.instance.dragDestination == point)
		{
			BattlePlanUi.instance.dragDestination = null;
		}
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0008BFB4 File Offset: 0x0008A1B4
	public static void RemoveOrder(Order order)
	{
		OrderManager.RemovePlayerOrder(order);
		if (BattlePlanUi.instance.orderIndicator.ContainsKey(order))
		{
			UnityEngine.Object.Destroy(BattlePlanUi.instance.orderIndicator[order].gameObject);
			BattlePlanUi.instance.orderIndicator.Remove(order);
		}
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0000DB85 File Offset: 0x0000BD85
	public static void UpdateTacticsPointColor(SpawnPoint spawn)
	{
		if (BattlePlanUi.instance.tacticsPoints.ContainsKey(spawn))
		{
			BattlePlanUi.instance.tacticsPoints[spawn].UpdateColor();
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0000DBAE File Offset: 0x0000BDAE
	private void ShowCanvas()
	{
		if (this.uiCanvas.enabled)
		{
			return;
		}
		MinimapUi.PinToStrategyScreen();
		this.uiCanvas.enabled = true;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0000DBCF File Offset: 0x0000BDCF
	private void HideCanvas()
	{
		MinimapUi.PinToIngameScreen();
		this.uiCanvas.enabled = false;
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0000DBE2 File Offset: 0x0000BDE2
	private void Awake()
	{
		BattlePlanUi.instance = this;
		this.uiCanvas = base.GetComponent<Canvas>();
		this.orderIndicator = new Dictionary<Order, TacticsOrderIndicator>();
		this.tacticsPoints = new Dictionary<SpawnPoint, TacticsPoint>();
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0000DC0C File Offset: 0x0000BE0C
	public void Setup()
	{
		this.PlaceTacticsPoints();
		this.attackArrowPreview = UnityEngine.Object.Instantiate<GameObject>(this.attackArrowPrefab, this.tacticsPanel);
		this.attackArrowPreviewTransform = (RectTransform)this.attackArrowPreview.transform;
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0008C004 File Offset: 0x0008A204
	private void Update()
	{
		Vector2 destination = this.LocalPositionWorldPixel(Input.mousePosition);
		bool flag = this.isDragging && this.dragSource != this.dragDestination;
		this.attackArrowPreview.SetActive(flag);
		if (flag)
		{
			if (this.dragDestination != null)
			{
				this.SetArrowTransform(this.attackArrowPreviewTransform, ((RectTransform)this.dragSource.transform).anchorMin, ((RectTransform)this.dragDestination.transform).anchorMin);
				return;
			}
			this.SetArrowTransform(this.attackArrowPreviewTransform, ((RectTransform)this.dragSource.transform).anchorMin, destination);
		}
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x0008C0B8 File Offset: 0x0008A2B8
	public Vector2 LocalPositionWorldPixel(Vector2 worldPixel)
	{
		this.tacticsPanel.GetWorldCorners(this.corners);
		Vector2 vector = this.corners[2] - this.corners[0];
		return Vector2.Scale(worldPixel - this.corners[0], new Vector2(1f / vector.x, 1f / vector.y));
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x0008C134 File Offset: 0x0008A334
	private void SetArrowTransform(RectTransform arrowTransform, Vector2 origin, Vector2 destination)
	{
		arrowTransform.anchorMin = origin;
		arrowTransform.anchorMax = origin;
		arrowTransform.anchoredPosition = Vector2.zero;
		Vector2 vector = Vector2.Scale(origin - destination, this.tacticsPanel.rect.size);
		arrowTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector.magnitude);
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		arrowTransform.localEulerAngles = new Vector3(0f, 0f, num + 180f);
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0008C1BC File Offset: 0x0008A3BC
	private void PlaceTacticsPoints()
	{
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			RectTransform rectTransform = (RectTransform)MinimapUi.instance.minimapSpawnPointButton[spawnPoint].transform;
			TacticsPoint component = UnityEngine.Object.Instantiate<GameObject>(this.tacticsPointPrefab, this.tacticsPanel).GetComponent<TacticsPoint>();
			RectTransform rectTransform2 = (RectTransform)component.transform;
			rectTransform2.anchorMin = rectTransform.anchorMin;
			rectTransform2.anchorMax = rectTransform.anchorMax;
			rectTransform2.anchoredPosition = Vector2.zero;
			component.spawnPoint = spawnPoint;
			this.tacticsPoints.Add(spawnPoint, component);
		}
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0008C260 File Offset: 0x0008A460
	public void DragCompleted(TacticsPoint source, TacticsPoint destination)
	{
		Order order = new Order((source != destination) ? Order.OrderType.Attack : Order.OrderType.Defend, source.spawnPoint, destination.spawnPoint, true);
		order.basePriority = 10;
		OrderManager.AddPlayerOrder(order);
		TacticsOrderIndicator component;
		if (order.type == Order.OrderType.Attack)
		{
			component = UnityEngine.Object.Instantiate<GameObject>(this.attackArrowPrefab, this.tacticsPanel).GetComponent<TacticsOrderIndicator>();
			this.SetArrowTransform((RectTransform)component.transform, ((RectTransform)source.transform).anchorMin, ((RectTransform)destination.transform).anchorMin);
		}
		else
		{
			component = UnityEngine.Object.Instantiate<GameObject>(this.defendShieldPrefab, this.tacticsPanel).GetComponent<TacticsOrderIndicator>();
			RectTransform rectTransform = (RectTransform)component.transform;
			rectTransform.anchorMin = ((RectTransform)source.transform).anchorMin;
			rectTransform.anchorMax = rectTransform.anchorMin;
			rectTransform.anchoredPosition = Vector2.zero;
		}
		component.order = order;
		this.orderIndicator.Add(order, component);
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0000DC41 File Offset: 0x0000BE41
	public void ForcePlayerOrdersChanged(bool value)
	{
		OrderManager.SetForcePlayerOrders(value);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0000DC49 File Offset: 0x0000BE49
	public void Regroup()
	{
		if (!FpsActorController.instance.actor.dead && FpsActorController.instance.playerSquad != null)
		{
			FpsActorController.instance.playerSquad.Regroup();
		}
	}

	// Token: 0x0400128C RID: 4748
	public static BattlePlanUi instance;

	// Token: 0x0400128D RID: 4749
	public GameObject tacticsPointPrefab;

	// Token: 0x0400128E RID: 4750
	public GameObject attackArrowPrefab;

	// Token: 0x0400128F RID: 4751
	public GameObject defendShieldPrefab;

	// Token: 0x04001290 RID: 4752
	public RectTransform tacticsPanel;

	// Token: 0x04001291 RID: 4753
	private GameObject attackArrowPreview;

	// Token: 0x04001292 RID: 4754
	private RectTransform attackArrowPreviewTransform;

	// Token: 0x04001293 RID: 4755
	private Canvas uiCanvas;

	// Token: 0x04001294 RID: 4756
	private TacticsPoint dragSource;

	// Token: 0x04001295 RID: 4757
	private TacticsPoint dragDestination;

	// Token: 0x04001296 RID: 4758
	private Vector3[] corners = new Vector3[4];

	// Token: 0x04001297 RID: 4759
	private Dictionary<Order, TacticsOrderIndicator> orderIndicator;

	// Token: 0x04001298 RID: 4760
	private Dictionary<SpawnPoint, TacticsPoint> tacticsPoints;

	// Token: 0x04001299 RID: 4761
	private bool isDragging;
}
