using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DC RID: 732
public class StrategyUi : MonoBehaviour
{
	// Token: 0x0600135E RID: 4958 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
	public static void Show()
	{
		StrategyUi.instance.canvas.enabled = true;
		StrategyUi.instance.hasPoint = false;
		MinimapUi.PinToStrategyScreen();
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x0000F71A File Offset: 0x0000D91A
	public static void Hide()
	{
		StrategyUi.instance.canvas.enabled = false;
		MinimapUi.PinToIngameScreen();
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x0000F731 File Offset: 0x0000D931
	public static bool IsOpen()
	{
		return StrategyUi.instance.canvas.enabled;
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x00093010 File Offset: 0x00091210
	public static Vector2 LocalPositionWorldPixel(Vector2 worldPixel)
	{
		StrategyUi.instance.strategyPanel.GetWorldCorners(StrategyUi.corners);
		Vector2 vector = StrategyUi.corners[2] - StrategyUi.corners[0];
		return Vector2.Scale(worldPixel - StrategyUi.corners[0], new Vector2(1f / vector.x, 1f / vector.y));
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x0000F742 File Offset: 0x0000D942
	public static Vector2 LocalPositionFromWorldPosition(Vector3 worldPosition)
	{
		return MinimapCamera.instance.camera.WorldToViewportPoint(worldPosition);
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x0000F759 File Offset: 0x0000D959
	public static void ClickedPoint(Vector2 mousePosition)
	{
		StrategyUi.instance.ClearPoint();
		if (FpsActorController.instance.actor.dead)
		{
			return;
		}
		if (StrategyUi.FindWorldPoint(mousePosition))
		{
			StrategyUi.instance.IssueOrderGoto();
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x0009308C File Offset: 0x0009128C
	public static void OpenContextMenuAtWorldPoint(Vector3 worldPosition)
	{
		StrategyUi.instance.ClearPoint();
		if (FpsActorController.instance.actor.dead)
		{
			return;
		}
		StrategyUi.instance.clickedPoint = MinimapCamera.instance.camera.WorldToViewportPoint(worldPosition);
		if (StrategyUi.instance.clickedPoint.x > 0f && StrategyUi.instance.clickedPoint.x < 1f && StrategyUi.instance.clickedPoint.y > 0f && StrategyUi.instance.clickedPoint.y < 1f)
		{
			StrategyUi.instance.clickedWorldPoint = worldPosition;
			StrategyUi.instance.hasPoint = true;
			StrategyUi.FindNearbyThings();
			StrategyUi.instance.SetPointPosition(StrategyUi.instance.clickedPoint);
			StrategyUi.instance.ShowPointDropdown();
		}
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x00093168 File Offset: 0x00091368
	public static void OpenContextMenu(Vector2 mousePosition)
	{
		StrategyUi.instance.ClearPoint();
		if (FpsActorController.instance.actor.dead)
		{
			return;
		}
		StrategyUi.instance.hasPoint = StrategyUi.FindWorldPoint(mousePosition);
		if (StrategyUi.instance.hasPoint)
		{
			StrategyUi.instance.ShowPointDropdown();
		}
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x000931B8 File Offset: 0x000913B8
	public static bool FindWorldPoint(Vector2 mousePosition)
	{
		Vector2 vector = StrategyUi.LocalPositionWorldPixel(mousePosition);
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(MinimapCamera.instance.camera.ViewportPointToRay(vector), out raycastHit, 9999999f, 1);
		if (flag)
		{
			StrategyUi.instance.clickedPoint = vector;
			StrategyUi.instance.clickedWorldPoint = raycastHit.point;
			StrategyUi.instance.SetPointPosition(vector);
			StrategyUi.FindNearbyThings();
		}
		return flag;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00093224 File Offset: 0x00091424
	private static void FindNearbyThings()
	{
		StrategyUi.instance.clickedSpawnPoint = null;
		StrategyUi.instance.clickedVehicle = null;
		float num = float.MaxValue;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.IsInsideProtectRange(StrategyUi.instance.clickedWorldPoint))
			{
				float magnitude = (spawnPoint.transform.position - StrategyUi.instance.clickedWorldPoint).ToGround().magnitude;
				if (magnitude < num)
				{
					StrategyUi.instance.clickedSpawnPoint = spawnPoint;
					num = magnitude;
				}
			}
		}
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x000932BC File Offset: 0x000914BC
	public static void SetSquadOrderMarker(Vector3 worldPosition, StrategyUi.MarkerType markerType)
	{
		Vector3 v = StrategyUi.LocalPositionFromWorldPosition(worldPosition);
		Texture2D texture;
		if (markerType == StrategyUi.MarkerType.Attack)
		{
			texture = StrategyUi.instance.attackOrderMarkerTexture;
		}
		else if (markerType == StrategyUi.MarkerType.Defend)
		{
			texture = StrategyUi.instance.defendOrderMarkerTexture;
		}
		else if (markerType == StrategyUi.MarkerType.EnterVehicle)
		{
			texture = StrategyUi.instance.enterVehicleOrderMarkerTexture;
		}
		else
		{
			texture = StrategyUi.instance.gotoOrderMarkerTexture;
		}
		StrategyUi.instance.squadOrderMarker.texture = texture;
		StrategyUi.instance.squadOrderMarker.rectTransform.anchorMin = v;
		StrategyUi.instance.squadOrderMarker.rectTransform.anchorMax = v;
		StrategyUi.instance.squadOrderMarker.rectTransform.anchoredPosition = Vector2.zero;
		StrategyUi.instance.squadOrderMarker.enabled = true;
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x0000F789 File Offset: 0x0000D989
	public static void HideSquadOrderMarker()
	{
		StrategyUi.instance.squadOrderMarker.enabled = false;
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x0000F79B File Offset: 0x0000D99B
	private void Awake()
	{
		StrategyUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		StrategyUi.HideSquadOrderMarker();
		StrategyUi.Hide();
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x0000F7B9 File Offset: 0x0000D9B9
	private void Update()
	{
		this.pointGraphic.enabled = this.hasPoint;
		this.pointDropdown.SetActive(this.hasPoint);
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x0000F7DD File Offset: 0x0000D9DD
	public void RegroupClicked()
	{
		if (FpsActorController.instance.actor.dead)
		{
			return;
		}
		FpsActorController.instance.SquadOrderRegroup();
		StrategyUi.instance.ClearPoint();
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x0000F805 File Offset: 0x0000DA05
	public void CloseClicked()
	{
		StrategyUi.Hide();
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x0000F80C File Offset: 0x0000DA0C
	private void SetPointPosition(Vector2 normalizedPosition)
	{
		this.pointGraphic.rectTransform.anchorMin = normalizedPosition;
		this.pointGraphic.rectTransform.anchorMax = normalizedPosition;
		this.pointGraphic.rectTransform.anchoredPosition = Vector2.zero;
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x00093380 File Offset: 0x00091580
	public void ShowPointDropdown()
	{
		this.pointDropdown.SetActive(true);
		this.dropdownButtons[1].gameObject.SetActive(this.clickedSpawnPoint != null && this.clickedSpawnPoint.owner != GameManager.PlayerTeam());
		this.dropdownButtons[2].gameObject.SetActive(this.clickedSpawnPoint != null && this.clickedSpawnPoint.owner == GameManager.PlayerTeam());
		this.dropdownButtons[3].gameObject.SetActive(this.clickedVehicle != null);
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x0000F845 File Offset: 0x0000DA45
	public void HidePointDropdown()
	{
		this.pointDropdown.SetActive(false);
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x0000F853 File Offset: 0x0000DA53
	public void ClearPoint()
	{
		this.hasPoint = false;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x0000F85C File Offset: 0x0000DA5C
	public void MoveClicked()
	{
		if (StrategyUi.instance.hasPoint && !FpsActorController.instance.actor.dead)
		{
			this.IssueOrderGoto();
		}
		StrategyUi.instance.ClearPoint();
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x0000F88B File Offset: 0x0000DA8B
	public void AttackClicked()
	{
		if (StrategyUi.instance.hasPoint && !FpsActorController.instance.actor.dead)
		{
			this.IssueOrderAttackPoint(this.clickedSpawnPoint);
		}
		StrategyUi.instance.ClearPoint();
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
	public void DefendClicked()
	{
		if (StrategyUi.instance.hasPoint && !FpsActorController.instance.actor.dead)
		{
			this.IssueOrderDefendPoint(this.clickedSpawnPoint);
		}
		StrategyUi.instance.ClearPoint();
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x0000F8F5 File Offset: 0x0000DAF5
	public void EnterClicked()
	{
		if (StrategyUi.instance.hasPoint && !FpsActorController.instance.actor.dead)
		{
			this.IssueOrderEnterVehicle(this.clickedVehicle);
		}
		StrategyUi.instance.ClearPoint();
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0000F92A File Offset: 0x0000DB2A
	private void IssueOrderGoto()
	{
		SquadPointUi.PlayCommitOrderQuick();
		FpsActorController.instance.SquadOrderGoto(this.clickedWorldPoint);
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x0000F941 File Offset: 0x0000DB41
	private void IssueOrderAttackPoint(SpawnPoint point)
	{
		SquadPointUi.PlayCommitOrderQuick();
		FpsActorController.instance.SquadOrderAttack(point);
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x0000F953 File Offset: 0x0000DB53
	private void IssueOrderDefendPoint(SpawnPoint point)
	{
		SquadPointUi.PlayCommitOrderQuick();
		FpsActorController.instance.SquadOrderDefend(point);
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0000F965 File Offset: 0x0000DB65
	private void IssueOrderEnterVehicle(Vehicle vehicle)
	{
		SquadPointUi.PlayCommitOrderQuick();
		FpsActorController.instance.SquadOrderEnterVehicle(vehicle);
	}

	// Token: 0x040014E7 RID: 5351
	public static StrategyUi instance;

	// Token: 0x040014E8 RID: 5352
	private static Vector3[] corners = new Vector3[4];

	// Token: 0x040014E9 RID: 5353
	private const int DROPDOWN_BUTTON_GOTO = 0;

	// Token: 0x040014EA RID: 5354
	private const int DROPDOWN_BUTTON_ATTACK = 1;

	// Token: 0x040014EB RID: 5355
	private const int DROPDOWN_BUTTON_DEFEND = 2;

	// Token: 0x040014EC RID: 5356
	private const int DROPDOWN_BUTTON_ENTER_VEHICLE = 3;

	// Token: 0x040014ED RID: 5357
	public RectTransform strategyPanel;

	// Token: 0x040014EE RID: 5358
	public Graphic pointGraphic;

	// Token: 0x040014EF RID: 5359
	public GameObject pointDropdown;

	// Token: 0x040014F0 RID: 5360
	public RawImage squadOrderMarker;

	// Token: 0x040014F1 RID: 5361
	public Button[] dropdownButtons;

	// Token: 0x040014F2 RID: 5362
	public Texture2D gotoOrderMarkerTexture;

	// Token: 0x040014F3 RID: 5363
	public Texture2D attackOrderMarkerTexture;

	// Token: 0x040014F4 RID: 5364
	public Texture2D defendOrderMarkerTexture;

	// Token: 0x040014F5 RID: 5365
	public Texture2D enterVehicleOrderMarkerTexture;

	// Token: 0x040014F6 RID: 5366
	private Canvas canvas;

	// Token: 0x040014F7 RID: 5367
	private Vector3 clickedPoint;

	// Token: 0x040014F8 RID: 5368
	private Vector3 clickedWorldPoint;

	// Token: 0x040014F9 RID: 5369
	private SpawnPoint clickedSpawnPoint;

	// Token: 0x040014FA RID: 5370
	private Vehicle clickedVehicle;

	// Token: 0x040014FB RID: 5371
	private bool hasPoint;

	// Token: 0x020002DD RID: 733
	public enum MarkerType
	{
		// Token: 0x040014FD RID: 5373
		Attack,
		// Token: 0x040014FE RID: 5374
		Defend,
		// Token: 0x040014FF RID: 5375
		Goto,
		// Token: 0x04001500 RID: 5376
		EnterVehicle
	}
}
