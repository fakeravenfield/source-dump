using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E3 RID: 483
public class GamePreview : MonoBehaviour
{
	// Token: 0x06000CE1 RID: 3297 RVA: 0x0000A88B File Offset: 0x00008A8B
	private void Awake()
	{
		GamePreview.instance = this;
		this.SetupDefaultValues();
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0007A828 File Offset: 0x00078A28
	private void SetupDefaultValues()
	{
		PreviewTeam[] array = this.team;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetupDefaultValues();
		}
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0007A854 File Offset: 0x00078A54
	public void UpdateIconCamera(float right, float up, float zoom)
	{
		this.iconCamera.transform.eulerAngles = new Vector3(-up * 25f - 2f, right * 25f, 0f);
		this.iconCamera.fieldOfView = Mathf.Lerp(45.2f, 5f, zoom);
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0000A899 File Offset: 0x00008A99
	public void SetupIconUi(bool headerEnabled, string headerText, bool footerEnabled, string footerText)
	{
		this.header.SetActive(headerEnabled);
		this.footer.SetActive(footerEnabled);
		this.headerText.text = headerText;
		this.footerText.text = footerText;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0000A8CC File Offset: 0x00008ACC
	public void SetIconCameraEnabled(bool enabled)
	{
		this.iconCamera.enabled = enabled;
		this.iconUiCamera.enabled = enabled;
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0007A8AC File Offset: 0x00078AAC
	public static void UpdatePreview()
	{
		try
		{
			GamePreview.instance.UpdatePreviewInternal();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0000A8E6 File Offset: 0x00008AE6
	public static void RenderVehiclePreview(GameObject prefab, int team, Texture2D destination)
	{
		GamePreview.UpdateVehiclePrefab(prefab, GamePreview.instance.vehiclePreviewParent, team);
		GamePreview.instance.RenderFrame(GamePreview.instance.vehiclePreviewCamera, destination);
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0007A8DC File Offset: 0x00078ADC
	private void UpdatePreviewInternal()
	{
		this.team[0].Update(GameManager.instance.gameInfo.team[0], 0);
		this.team[1].Update(GameManager.instance.gameInfo.team[1], 1);
		this.RenderFrame(this.camera);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0007A934 File Offset: 0x00078B34
	public void UpdateTeamVisibility(int eagle, int raven)
	{
		for (int i = 0; i < this.team[0].actors.Length; i++)
		{
			this.team[0].actorMeshRenderers[i].enabled = (i < eagle);
			this.team[0].weaponParents[i].gameObject.SetActive(i < eagle);
		}
		for (int j = 0; j < this.team[1].actors.Length; j++)
		{
			this.team[1].actorMeshRenderers[j].enabled = (j < raven);
			this.team[1].weaponParents[j].gameObject.SetActive(j < raven);
		}
		this.RenderFrame(this.camera);
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0000A90E File Offset: 0x00008B0E
	private void RenderFrame(Camera camera)
	{
		base.StartCoroutine(this.RenderCoroutine(camera));
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0000A91E File Offset: 0x00008B1E
	private void RenderFrame(Camera camera, Texture2D destination)
	{
		base.StartCoroutine(this.RenderCoroutine(camera, destination));
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0000A92F File Offset: 0x00008B2F
	private IEnumerator RenderCoroutine(Camera camera)
	{
		yield return new WaitForEndOfFrame();
		camera.Render();
		yield break;
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0000A93E File Offset: 0x00008B3E
	private IEnumerator RenderCoroutine(Camera camera, Texture2D destination)
	{
		yield return new WaitForEndOfFrame();
		camera.Render();
		Graphics.CopyTexture(camera.targetTexture, destination);
		yield break;
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0007A9EC File Offset: 0x00078BEC
	public static void UpdateVehiclePrefab(GameObject prefab, Transform parent, int team)
	{
		for (int i = parent.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(parent.GetChild(i).gameObject);
		}
		if (prefab != null)
		{
			try
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				GameManager.SetupRecursiveLayer(gameObject.transform, 30);
				GamePreview.StripVehiclePrefab(gameObject, team);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0007AA7C File Offset: 0x00078C7C
	private static void StripVehiclePrefab(GameObject gameObject, int team)
	{
		try
		{
			Vehicle component = gameObject.GetComponent<Vehicle>();
			foreach (MaterialTarget materialTarget in component.teamColorMaterials)
			{
				try
				{
					materialTarget.Get().color = ColorScheme.TeamColor(team);
				}
				catch (Exception)
				{
				}
			}
			foreach (Seat seat in component.seats)
			{
				if (seat.hud != null)
				{
					UnityEngine.Object.Destroy(seat.hud.gameObject);
				}
			}
			UnityEngine.Object.Destroy(component);
		}
		catch (Exception exception)
		{
			Debug.Log("Caught exception:");
			Debug.LogException(exception);
		}
		foreach (Rotor rotor in gameObject.GetComponentsInChildren<Rotor>())
		{
			try
			{
				if (rotor.blurredRotorRenderer != null)
				{
					UnityEngine.Object.Destroy(rotor.solidRotorRenderer);
					rotor.blurredRotorRenderer.enabled = true;
				}
			}
			catch (Exception exception2)
			{
				Debug.Log("Caught exception:");
				Debug.LogException(exception2);
			}
		}
		try
		{
			GamePreview.DestroyComponents<Joint>(gameObject);
			GamePreview.DestroyComponents<Weapon>(gameObject);
			GamePreview.DestroyComponents<FlareLayer>(gameObject);
			GamePreview.RecursiveStripComponents(gameObject.transform);
		}
		catch (Exception exception3)
		{
			Debug.Log("Caught exception:");
			Debug.LogException(exception3);
		}
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0007ABF4 File Offset: 0x00078DF4
	public static void RecursiveStripComponents(Transform transform)
	{
		if (!transform.gameObject.activeInHierarchy)
		{
			return;
		}
		Component[] components = transform.GetComponents<Component>();
		for (int i = components.Length - 1; i >= 0; i--)
		{
			Component component = components[i];
			if (!(component == null))
			{
				try
				{
					Type type = component.GetType();
					if (!(component is Transform) && !type.IsSubclassOf(typeof(Renderer)) && !(type == typeof(MeshFilter)))
					{
						if (type == typeof(Cloth))
						{
							((Cloth)component).enabled = false;
						}
						else if (type == typeof(Rigidbody))
						{
							((Rigidbody)component).isKinematic = true;
							UnityEngine.Object.Destroy(component);
						}
						else
						{
							UnityEngine.Object.Destroy(component);
						}
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}
		for (int j = 0; j < transform.childCount; j++)
		{
			GamePreview.RecursiveStripComponents(transform.GetChild(j));
		}
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0007ACF8 File Offset: 0x00078EF8
	public static void DestroyComponents<T>(GameObject gameObject) where T : Component
	{
		T[] componentsInChildren = gameObject.GetComponentsInChildren<T>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UnityEngine.Object.Destroy(componentsInChildren[i]);
		}
	}

	// Token: 0x04000DE4 RID: 3556
	public const int PREVIEW_LAYER = 30;

	// Token: 0x04000DE5 RID: 3557
	private const float ICON_MAX_FOV = 45.2f;

	// Token: 0x04000DE6 RID: 3558
	private const float ICON_MIN_FOV = 5f;

	// Token: 0x04000DE7 RID: 3559
	private const float ICON_MAX_PITCH = 25f;

	// Token: 0x04000DE8 RID: 3560
	private const float ICON_MAX_YAW = 25f;

	// Token: 0x04000DE9 RID: 3561
	public static GamePreview instance;

	// Token: 0x04000DEA RID: 3562
	public PreviewTeam[] team;

	// Token: 0x04000DEB RID: 3563
	public Camera camera;

	// Token: 0x04000DEC RID: 3564
	public Camera iconCamera;

	// Token: 0x04000DED RID: 3565
	public Camera iconUiCamera;

	// Token: 0x04000DEE RID: 3566
	public GameObject header;

	// Token: 0x04000DEF RID: 3567
	public GameObject footer;

	// Token: 0x04000DF0 RID: 3568
	public Text headerText;

	// Token: 0x04000DF1 RID: 3569
	public Text footerText;

	// Token: 0x04000DF2 RID: 3570
	public Camera vehiclePreviewCamera;

	// Token: 0x04000DF3 RID: 3571
	public Transform vehiclePreviewParent;
}
