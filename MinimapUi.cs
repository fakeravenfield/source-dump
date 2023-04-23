using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B8 RID: 696
public class MinimapUi : MonoBehaviour
{
	// Token: 0x0600128D RID: 4749 RVA: 0x0008FE50 File Offset: 0x0008E050
	private void Awake()
	{
		MinimapUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.actorBlipScale = 1f;
		this.playerSquadBlipScale = 1f;
		this.playerViewConeBlipScale = 1f;
		RectTransform rectTransform = this.minimap.rectTransform;
		float num = this.minimap.rectTransform.anchorMax.x - this.minimap.rectTransform.anchorMin.x;
		this.minimapSize = num * (float)Screen.width * 1.3f;
		this.minimapTargetAnchor = new Vector2(this.minimap.rectTransform.anchorMin.x, this.minimap.rectTransform.anchorMax.y);
		this.blitMaterial = new Material(this.blitMaterial);
		this.IntializeBlipRTs();
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x0000EAA1 File Offset: 0x0000CCA1
	private void Start()
	{
		this.SetupMinimap();
		MinimapUi.UpdateSpawnPointButtons();
		base.StartCoroutine(this.UpdateActorRT());
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x0008FF28 File Offset: 0x0008E128
	private void IntializeBlipRTs()
	{
		int resolution = MinimapCamera.instance.resolution;
		this.actorBlipsRTBack = this.InitializeRenderTexture(resolution);
		this.actorBlipsRTFront = this.InitializeRenderTexture(resolution);
		this.playerBlipsRT = this.InitializeRenderTexture(resolution);
		this.UpdateFrontActorRT();
		this.actorBlipsImage.texture = this.actorBlipsRTFront;
		this.playerBlipsImage.texture = this.playerBlipsRT;
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x0008FF90 File Offset: 0x0008E190
	private RenderTexture InitializeRenderTexture(int resolution)
	{
		RenderTexture renderTexture = new RenderTexture(new RenderTextureDescriptor(resolution, resolution, RenderTextureFormat.ARGB32, 0, 0)
		{
			sRGB = false
		});
		renderTexture.Create();
		return renderTexture;
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x0008FFC0 File Offset: 0x0008E1C0
	private void Update()
	{
		float target = SteelInput.GetButton(SteelInput.KeyBinds.Map) ? 1f : 0f;
		this.minimapOpenness = Mathf.MoveTowards(this.minimapOpenness, target, Time.deltaTime * 20f);
		this.ingameParent.anchorMin = new Vector2(0f, Mathf.Lerp(-1f, 0f, this.minimapOpenness));
		this.ingameParent.anchorMax = new Vector2(1f, Mathf.Lerp(0f, 1f, this.minimapOpenness));
		this.canvas.enabled = (this.minimapOpenness > 0f);
		this.BlitPlayerBlips();
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x0000EABB File Offset: 0x0000CCBB
	private void PushGLMatrix()
	{
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.MultMatrix(MinimapUi.GRAPHICS_BLIT_MATRIX);
	}

	// Token: 0x06001293 RID: 4755 RVA: 0x0000EAD1 File Offset: 0x0000CCD1
	private void PopGLMatrix()
	{
		GL.PopMatrix();
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x00090074 File Offset: 0x0008E274
	private void BlitPlayerBlips()
	{
		if (GameManager.IsPaused())
		{
			return;
		}
		RenderTexture.active = this.playerBlipsRT;
		GL.Clear(false, true, Color.black);
		this.playerSquadVehicles.Clear();
		Actor player = ActorManager.instance.player;
		ActorData actorData = ActorManager.instance.actorData[player.actorIndex];
		if (actorData.dead)
		{
			return;
		}
		this.PushGLMatrix();
		Matrix4x4 matrix4x = MinimapUi.VIEWPORT_MATRIX * MinimapCamera.instance.camera.projectionMatrix * MinimapCamera.instance.camera.worldToCameraMatrix;
		float y = MinimapCamera.instance.camera.transform.eulerAngles.y;
		this.blitCurrentTeam = -100;
		this.blitMaterial.SetColor(MinimapUi.BLIT_TINT_COLOR_PROPERTY, Color.white);
		this.blitMaterial.SetTexture(MinimapUi.BLIT_TEXTURE_PROPERTY, this.infantryBlipTexture);
		Vector3 v;
		float angle;
		this.GetActorBlitData(ref matrix4x, y, ref actorData, out v, out angle);
		Vector3 vector = FpsActorController.instance.activeCameraLocalToWorldMatrix.MultiplyVector(Vector3.forward);
		float angle2 = y - 57.29578f * Mathf.Atan2(vector.x, vector.z);
		this.sightConeMaterial.SetPass(0);
		this.BlitBottomAnchor(v, angle2, 0.08f * this.playerViewConeBlipScale);
		this.blitMaterial.SetPass(0);
		if (actorData.visibleOnMinimap)
		{
			this.Blit(v, angle, 0.025f * this.playerSquadBlipScale);
		}
		else if (player.IsSeated())
		{
			this.playerSquadVehicles.Add(player.seat.vehicle);
		}
		Squad playerSquad = FpsActorController.instance.playerSquad;
		if (playerSquad != null)
		{
			foreach (AiActorController aiActorController in playerSquad.aiMembers)
			{
				ActorData actorData2 = ActorManager.instance.actorData[aiActorController.actor.actorIndex];
				if (actorData2.visibleOnMinimap)
				{
					this.BlitActor(ref matrix4x, y, ref actorData2, 0.025f * this.playerSquadBlipScale);
				}
				else if (aiActorController.actor.IsSeated())
				{
					this.playerSquadVehicles.Add(aiActorController.actor.seat.vehicle);
				}
			}
		}
		this.blitMaterial.SetColor(MinimapUi.BLIT_TINT_COLOR_PROPERTY, new Color(1f, 1f, 1f, 0.25f));
		foreach (Vehicle vehicle in this.playerSquadVehicles)
		{
			this.blitMaterial.SetTexture(MinimapUi.BLIT_TEXTURE_PROPERTY, vehicle.blip);
			this.blitMaterial.SetPass(0);
			this.BlitVehicle(ref matrix4x, y, vehicle, 0.06f * this.playerSquadBlipScale);
		}
		RenderTexture.active = null;
		this.PopGLMatrix();
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x00090380 File Offset: 0x0008E580
	private bool ShouldDrawVehicleBlip(Vehicle vehicle)
	{
		if (vehicle.isTurret)
		{
			return !vehicle.IsEmpty() && (vehicle.IsHighlighted() || GameManager.IsSpectating() || vehicle.ownerTeam == GameManager.PlayerTeam());
		}
		return !vehicle.dead && (vehicle.IsHighlighted() || GameManager.IsSpectating() || vehicle.ownerTeam == GameManager.PlayerTeam() || vehicle.spawner == null || (vehicle.ownerTeam == -1 && vehicle.spawner.lastSpawnedVehicleHasBeenUsed) || (vehicle.spawner.spawnPoint != null && vehicle.spawner.spawnPoint.owner == GameManager.PlayerTeam()));
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
	private IEnumerator UpdateActorRT()
	{
		for (;;)
		{
			RenderTexture.active = this.actorBlipsRTBack;
			this.PushGLMatrix();
			GL.Clear(false, true, Color.black);
			this.blitMaterial.SetTexture(MinimapUi.BLIT_TEXTURE_PROPERTY, this.infantryBlipTexture);
			this.blitMaterial.SetPass(0);
			Matrix4x4 minimapMatrix = MinimapUi.VIEWPORT_MATRIX * MinimapCamera.instance.camera.projectionMatrix * MinimapCamera.instance.camera.worldToCameraMatrix;
			float minimapBearing = MinimapCamera.instance.camera.transform.eulerAngles.y;
			int actorIndex = 0;
			int nBlitted = 0;
			int count = ActorManager.instance.actors.Count;
			int nActorsPerFrame = Mathf.Max(10, count / 2 + 1);
			while (actorIndex < ActorManager.instance.actors.Count)
			{
				ActorData actorData = ActorManager.instance.actorData[actorIndex];
				int num;
				if (actorData.visibleOnMinimap && !actorData.isOnPlayerSquad)
				{
					this.SetBlitTeamColor(actorData.team);
					this.BlitActor(ref minimapMatrix, minimapBearing, ref actorData, 0.015f * this.actorBlipScale);
					num = nBlitted;
					nBlitted = num + 1;
				}
				num = actorIndex;
				actorIndex = num + 1;
				if (actorIndex % nActorsPerFrame == 0)
				{
					this.PopGLMatrix();
					yield return 0;
					this.PushGLMatrix();
					RenderTexture.active = this.actorBlipsRTBack;
					this.blitMaterial.SetTexture(MinimapUi.BLIT_TEXTURE_PROPERTY, this.infantryBlipTexture);
					this.blitMaterial.SetPass(0);
				}
			}
			this.PopGLMatrix();
			yield return 0;
			this.PushGLMatrix();
			RenderTexture.active = this.actorBlipsRTBack;
			this.blitMaterial.SetPass(0);
			foreach (Vehicle vehicle in ActorManager.instance.vehicles)
			{
				try
				{
					if (!vehicle.claimedByPlayer && this.ShouldDrawVehicleBlip(vehicle))
					{
						this.SetBlitTeamColor(vehicle.ownerTeam);
						this.blitMaterial.SetTexture(MinimapUi.BLIT_TEXTURE_PROPERTY, vehicle.blip);
						this.blitMaterial.SetPass(0);
						this.BlitVehicle(ref minimapMatrix, minimapBearing, vehicle, 0.05f * this.actorBlipScale);
					}
				}
				catch (Exception exception)
				{
					Debug.LogErrorFormat("Could not blit vehicle {0}, exception follows" + vehicle.name, Array.Empty<object>());
					Debug.LogException(exception);
				}
			}
			this.PopGLMatrix();
			this.UpdateFrontActorRT();
			yield return 0;
			if (GameManager.IsPaused())
			{
				yield return new WaitUntil(() => !GameManager.IsPaused());
			}
		}
		yield break;
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x00090438 File Offset: 0x0008E638
	private void BlitActor(ref Matrix4x4 minimapMatrix, float minimapBearing, ref ActorData actorData, float scale)
	{
		Vector3 v;
		float angle;
		this.GetActorBlitData(ref minimapMatrix, minimapBearing, ref actorData, out v, out angle);
		this.Blit(v, angle, scale);
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x0000EAE7 File Offset: 0x0000CCE7
	private void GetActorBlitData(ref Matrix4x4 minimapMatrix, float minimapBearing, ref ActorData actorData, out Vector3 viewportPosition, out float angle)
	{
		viewportPosition = minimapMatrix.MultiplyPoint(actorData.position);
		angle = minimapBearing - 57.29578f * Mathf.Atan2(actorData.facingDirection.x, actorData.facingDirection.z);
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x00090464 File Offset: 0x0008E664
	private void BlitVehicle(ref Matrix4x4 minimapMatrix, float minimapBearing, Vehicle vehicle, float scale)
	{
		Vector3 v;
		float angle;
		this.GetVehicleBlitData(ref minimapMatrix, minimapBearing, vehicle, out v, out angle);
		this.Blit(v, angle, scale * vehicle.blipScale);
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x0000EB22 File Offset: 0x0000CD22
	private void GetVehicleBlitData(ref Matrix4x4 minimapMatrix, float minimapBearing, Vehicle vehicle, out Vector3 viewportPosition, out float angle)
	{
		viewportPosition = minimapMatrix.MultiplyPoint(vehicle.transform.position);
		angle = minimapBearing - vehicle.transform.eulerAngles.y;
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x00090494 File Offset: 0x0008E694
	private void SetBlitTeamColor(int team)
	{
		if (team != this.blitCurrentTeam)
		{
			this.blitCurrentTeam = team;
			Color value = ColorScheme.TeamColorBrighter(team);
			value.a = 0.6f;
			if (team < 0)
			{
				value.a = 0.2f;
			}
			this.blitMaterial.SetColor(MinimapUi.BLIT_TINT_COLOR_PROPERTY, value);
			this.blitMaterial.SetPass(0);
		}
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x0000EB51 File Offset: 0x0000CD51
	private Vector2 ClampViewportPosition(Vector2 position)
	{
		return new Vector2(Mathf.Clamp(position.x, 0.01f, 0.99f), Mathf.Clamp(position.y, 0.01f, 0.99f));
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x000904F4 File Offset: 0x0008E6F4
	private void Blit(Vector2 viewportPosition, float angle, float size)
	{
		GL.PushMatrix();
		GL.MultMatrix(Matrix4x4.TRS(this.ClampViewportPosition(viewportPosition), Quaternion.Euler(0f, 0f, angle), new Vector3(size, size, size)));
		GL.Begin(7);
		GL.TexCoord2(0f, 0f);
		GL.Vertex3(-0.5f, -0.5f, 0f);
		GL.TexCoord2(0f, 1f);
		GL.Vertex3(-0.5f, 0.5f, 0f);
		GL.TexCoord2(1f, 1f);
		GL.Vertex3(0.5f, 0.5f, 0f);
		GL.TexCoord2(1f, 0f);
		GL.Vertex3(0.5f, -0.5f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x000905D0 File Offset: 0x0008E7D0
	private void BlitBottomAnchor(Vector2 viewportPosition, float angle, float size)
	{
		GL.PushMatrix();
		GL.MultMatrix(Matrix4x4.TRS(this.ClampViewportPosition(viewportPosition), Quaternion.Euler(0f, 0f, angle), new Vector3(size, size, size)));
		GL.Begin(7);
		GL.TexCoord2(0f, 0f);
		GL.Vertex3(-0.5f, --0f, 0f);
		GL.TexCoord2(0f, 1f);
		GL.Vertex3(-0.5f, 1f, 0f);
		GL.TexCoord2(1f, 1f);
		GL.Vertex3(0.5f, 1f, 0f);
		GL.TexCoord2(1f, 0f);
		GL.Vertex3(0.5f, 0f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x0000EB82 File Offset: 0x0000CD82
	private void UpdateFrontActorRT()
	{
		Graphics.CopyTexture(this.actorBlipsRTBack, this.actorBlipsRTFront);
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x000906AC File Offset: 0x0008E8AC
	private void SetupMinimap()
	{
		MinimapCamera minimapCamera = UnityEngine.Object.FindObjectOfType<MinimapCamera>();
		if (minimapCamera == null)
		{
			Debug.LogWarning("No minimap camera found!");
			return;
		}
		this.minimap.texture = minimapCamera.GetTexture();
		this.minimapSpawnPointButton = new Dictionary<SpawnPoint, Button>();
		Camera component = minimapCamera.GetComponent<Camera>();
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			SpawnPoint spawnPoint = spawnPoints[i];
			Button component2 = UnityEngine.Object.Instantiate<GameObject>(this.minimapSpawnPointPrefab).GetComponent<Button>();
			RectTransform rectTransform = (RectTransform)component2.transform;
			Vector3 vector = component.WorldToViewportPoint(spawnPoint.transform.position);
			SpawnPoint anonSpawnPoint = spawnPoint;
			component2.onClick.AddListener(delegate()
			{
				MinimapUi.SelectSpawnPoint(anonSpawnPoint);
			});
			rectTransform.SetParent(this.minimap.rectTransform);
			Vector2 vector2 = new Vector2(vector.x, vector.y);
			rectTransform.anchorMin = vector2;
			rectTransform.anchorMax = vector2;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.localScale = Vector3.one;
			this.minimapSpawnPointButton.Add(spawnPoint, component2);
		}
		MinimapUi.UpdateSpawnPointButtons();
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x0000EB95 File Offset: 0x0000CD95
	public static void SelectSpawnPoint(SpawnPoint spawnPoint)
	{
		MinimapUi.instance.selectedSpawnPoint = spawnPoint;
		MinimapUi.UpdateSpawnPointButtons();
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x0000EBA7 File Offset: 0x0000CDA7
	public static bool DrawObjectiveMarkersOnMinimap()
	{
		if (MinimapUi.instance.pinnedToIngameMap)
		{
			return MinimapUi.instance.minimapOpenness > 0.99f;
		}
		return MinimapUi.instance.minimap.gameObject.activeInHierarchy;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x000907D4 File Offset: 0x0008E9D4
	public static SpawnPoint SelectedSpawnPoint()
	{
		if (LoadoutUi.IsOpen())
		{
			return null;
		}
		if (MinimapUi.instance.selectedSpawnPoint == null)
		{
			foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
			{
				if (spawnPoint.IsFrontLine() && spawnPoint.owner == GameManager.PlayerTeam())
				{
					return spawnPoint;
				}
			}
			return null;
		}
		if (MinimapUi.instance.selectedSpawnPoint.owner != GameManager.PlayerTeam() && FpsActorController.instance.actor.dead)
		{
			LoadoutUi.Show(false);
		}
		return MinimapUi.instance.selectedSpawnPoint;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0009086C File Offset: 0x0008EA6C
	public static void UpdateSpawnPointButtons()
	{
		foreach (SpawnPoint spawnPoint in MinimapUi.instance.minimapSpawnPointButton.Keys)
		{
			MinimapUi.UpdateSpawnPointButton(spawnPoint);
		}
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000908C8 File Offset: 0x0008EAC8
	public static void UpdateSpawnPointButton(SpawnPoint spawnPoint)
	{
		if (spawnPoint == null || !MinimapUi.instance.minimapSpawnPointButton.ContainsKey(spawnPoint))
		{
			return;
		}
		int owner = spawnPoint.owner;
		Button button = MinimapUi.instance.minimapSpawnPointButton[spawnPoint];
		ColorBlock colors = button.colors;
		Color color = ColorScheme.TeamColor(owner);
		colors.normalColor = color;
		colors.highlightedColor = color + new Color(0.2f, 0.2f, 0.2f);
		colors.disabledColor = color * new Color(0.6f, 0.6f, 0.6f);
		colors.pressedColor = Color.white;
		if (spawnPoint == MinimapUi.instance.selectedSpawnPoint)
		{
			button.image.sprite = (spawnPoint.isGhostSpawn ? MinimapUi.instance.spawnPointSelectedGhostSprite : MinimapUi.instance.spawnPointSelectedSprite);
		}
		else
		{
			button.image.sprite = (spawnPoint.isGhostSpawn ? MinimapUi.instance.spawnPointGhostSprite : MinimapUi.instance.spawnPointSprite);
		}
		button.colors = colors;
		button.interactable = (MinimapUi.instance.playerCanSelectSpawnPoint && owner == GameManager.PlayerTeam());
		button.gameObject.SetActive(spawnPoint.isActiveAndEnabled);
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x0000EBDB File Offset: 0x0000CDDB
	public static void SetPlayerCanSelectSpawnPoint(bool active)
	{
		MinimapUi.instance.playerCanSelectSpawnPoint = active;
		MinimapUi.UpdateSpawnPointButtons();
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x0000EBED File Offset: 0x0000CDED
	public static void OnNewSpawnPointSelected()
	{
		MinimapUi.UpdateSpawnPointButtons();
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
	public static void PinToStrategyScreen()
	{
		MinimapUi.instance.minimap.rectTransform.SetParent(MinimapUi.instance.strategyParent, false);
		MinimapUi.instance.pinnedToIngameMap = false;
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x0000EC20 File Offset: 0x0000CE20
	public static void PinToLoadoutScreen()
	{
		MinimapUi.instance.minimap.rectTransform.SetParent(MinimapUi.instance.loadoutParent, false);
		MinimapUi.instance.pinnedToIngameMap = false;
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x0000EC4C File Offset: 0x0000CE4C
	public static void PinToIngameScreen()
	{
		MinimapUi.instance.minimap.rectTransform.SetParent(MinimapUi.instance.ingameParent, false);
		MinimapUi.instance.pinnedToIngameMap = true;
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x00090A08 File Offset: 0x0008EC08
	public static RawImage AddGenericBlip(Texture2D texture, Vector2 normalizedPosition, Vector2 size)
	{
		GameObject gameObject = new GameObject("Scripted Blip");
		gameObject.transform.SetParent(MinimapUi.instance.minimap.rectTransform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		RawImage rawImage = gameObject.AddComponent<RawImage>();
		rawImage.texture = texture;
		rawImage.rectTransform.anchorMin = normalizedPosition;
		rawImage.rectTransform.anchorMax = normalizedPosition;
		rawImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
		rawImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
		rawImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
		return rawImage;
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0000EC78 File Offset: 0x0000CE78
	public static RawImage AddGenericBlipWorld(Texture2D texture, Vector3 worldPosition, Vector2 size)
	{
		return MinimapUi.AddGenericBlip(texture, MinimapCamera.WorldToNormalizedPosition(worldPosition), size);
	}

	// Token: 0x040013C0 RID: 5056
	private static readonly Matrix4x4 VIEWPORT_MATRIX = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));

	// Token: 0x040013C1 RID: 5057
	private static readonly Matrix4x4 GRAPHICS_BLIT_MATRIX = Matrix4x4.Translate(new Vector2(0.5f, 0.5f));

	// Token: 0x040013C2 RID: 5058
	private static readonly int BLIT_TINT_COLOR_PROPERTY = Shader.PropertyToID("_TintColor");

	// Token: 0x040013C3 RID: 5059
	private static readonly int BLIT_TEXTURE_PROPERTY = Shader.PropertyToID("_MainTex");

	// Token: 0x040013C4 RID: 5060
	private const float PLAYER_SQUAD_BLIP_SCALE = 0.025f;

	// Token: 0x040013C5 RID: 5061
	private const float PLAYER_VIEW_CONE_SCALE = 0.08f;

	// Token: 0x040013C6 RID: 5062
	private const float DEFAULT_BLIP_SCALE = 0.015f;

	// Token: 0x040013C7 RID: 5063
	private const float DEFAULT_VEHICLE_BLIP_SCALE = 0.05f;

	// Token: 0x040013C8 RID: 5064
	private const float PLAYER_VEHICLE_BLIP_SCALE = 0.06f;

	// Token: 0x040013C9 RID: 5065
	private const float MINIMAP_SCALE = 1.3f;

	// Token: 0x040013CA RID: 5066
	public static MinimapUi instance;

	// Token: 0x040013CB RID: 5067
	public RectTransform strategyParent;

	// Token: 0x040013CC RID: 5068
	public RectTransform loadoutParent;

	// Token: 0x040013CD RID: 5069
	public RectTransform ingameParent;

	// Token: 0x040013CE RID: 5070
	public RawImage minimap;

	// Token: 0x040013CF RID: 5071
	public GameObject minimapSpawnPointPrefab;

	// Token: 0x040013D0 RID: 5072
	public GameObject actorBlipPrefab;

	// Token: 0x040013D1 RID: 5073
	public GameObject vehicleBlipPrefab;

	// Token: 0x040013D2 RID: 5074
	public Sprite spawnPointSprite;

	// Token: 0x040013D3 RID: 5075
	public Sprite spawnPointSelectedSprite;

	// Token: 0x040013D4 RID: 5076
	public Sprite spawnPointSelectedGhostSprite;

	// Token: 0x040013D5 RID: 5077
	public Sprite spawnPointGhostSprite;

	// Token: 0x040013D6 RID: 5078
	public Texture infantryBlipTexture;

	// Token: 0x040013D7 RID: 5079
	public Texture viewconeBlipTexture;

	// Token: 0x040013D8 RID: 5080
	public Material blitMaterial;

	// Token: 0x040013D9 RID: 5081
	public Material sightConeMaterial;

	// Token: 0x040013DA RID: 5082
	public RawImage actorBlipsImage;

	// Token: 0x040013DB RID: 5083
	public RawImage playerBlipsImage;

	// Token: 0x040013DC RID: 5084
	public RenderTexture actorBlipsRTFront;

	// Token: 0x040013DD RID: 5085
	public RenderTexture playerBlipsRT;

	// Token: 0x040013DE RID: 5086
	private RenderTexture actorBlipsRTBack;

	// Token: 0x040013DF RID: 5087
	[NonSerialized]
	public Dictionary<SpawnPoint, Button> minimapSpawnPointButton = new Dictionary<SpawnPoint, Button>();

	// Token: 0x040013E0 RID: 5088
	[NonSerialized]
	public SpawnPoint selectedSpawnPoint;

	// Token: 0x040013E1 RID: 5089
	private float minimapSize;

	// Token: 0x040013E2 RID: 5090
	private float minimapOpenness;

	// Token: 0x040013E3 RID: 5091
	private Vector2 minimapTargetAnchor;

	// Token: 0x040013E4 RID: 5092
	[NonSerialized]
	public bool playerCanSelectSpawnPoint = true;

	// Token: 0x040013E5 RID: 5093
	private bool pinnedToIngameMap;

	// Token: 0x040013E6 RID: 5094
	private Canvas canvas;

	// Token: 0x040013E7 RID: 5095
	[NonSerialized]
	public float actorBlipScale = 1f;

	// Token: 0x040013E8 RID: 5096
	[NonSerialized]
	public float playerSquadBlipScale = 1f;

	// Token: 0x040013E9 RID: 5097
	[NonSerialized]
	public float playerViewConeBlipScale = 1f;

	// Token: 0x040013EA RID: 5098
	private HashSet<Vehicle> playerSquadVehicles = new HashSet<Vehicle>();

	// Token: 0x040013EB RID: 5099
	private int blitCurrentTeam = -999;

	// Token: 0x040013EC RID: 5100
	private const float CLAMP_VIEWPORT_MIN = 0.01f;

	// Token: 0x040013ED RID: 5101
	private const float CLAMP_VIEWPORT_MAX = 0.99f;

	// Token: 0x020002B9 RID: 697
	private struct VehicleBlip
	{
		// Token: 0x040013EE RID: 5102
		public bool draw;

		// Token: 0x040013EF RID: 5103
		public int team;
	}
}
