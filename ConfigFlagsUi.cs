using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000291 RID: 657
public class ConfigFlagsUi : MonoBehaviour
{
	// Token: 0x06001192 RID: 4498 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
	public static bool IsOpen()
	{
		return ConfigFlagsUi.isVisible;
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x0008C4A8 File Offset: 0x0008A6A8
	private void Start()
	{
		this.spawnPoints = UnityEngine.Object.FindObjectsOfType<SpawnPoint>();
		this.minimapImage.texture = MinimapCamera.instance.minimapRenderTexture;
		this.spawnMode = new Dictionary<SpawnPoint, ConfigFlagsUi.FlagMode>(this.spawnPoints.Length);
		this.spawnPointButton = new Dictionary<SpawnPoint, Button>(this.spawnPoints.Length);
		for (int i = 0; i < this.spawnPoints.Length; i++)
		{
			SpawnPoint spawnPoint = this.spawnPoints[i];
			RectTransform rectTransform = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.panel).transform;
			Vector2 vector = MinimapCamera.instance.camera.WorldToViewportPoint(spawnPoint.transform.position);
			rectTransform.anchorMin = vector;
			rectTransform.anchorMax = vector;
			Button component = rectTransform.GetComponent<Button>();
			rectTransform.GetComponentInChildren<Text>().text = spawnPoint.shortName;
			this.spawnPointButton.Add(spawnPoint, component);
			this.spawnMode.Add(spawnPoint, ConfigFlagsUi.FlagMode.Neutral);
			if (!spawnPoint.isActiveAndEnabled)
			{
				this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Disabled;
			}
			else if (spawnPoint.defaultOwner == SpawnPoint.Team.Blue)
			{
				this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Blue;
			}
			else if (spawnPoint.defaultOwner == SpawnPoint.Team.Red)
			{
				this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Red;
			}
			component.onClick.AddListener(delegate()
			{
				if (this.spawnMode[spawnPoint] == ConfigFlagsUi.FlagMode.Neutral)
				{
					this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Blue;
				}
				else if (this.spawnMode[spawnPoint] == ConfigFlagsUi.FlagMode.Blue)
				{
					this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Red;
				}
				else if (this.spawnMode[spawnPoint] == ConfigFlagsUi.FlagMode.Red)
				{
					this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Disabled;
				}
				else
				{
					this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Neutral;
				}
				this.UpdateButtonGraphic(spawnPoint);
			});
			this.UpdateButtonGraphic(spawnPoint);
		}
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
	private void OnEnable()
	{
		ConfigFlagsUi.isVisible = true;
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x0000DDBF File Offset: 0x0000BFBF
	private void OnDisable()
	{
		ConfigFlagsUi.isVisible = false;
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x0008C644 File Offset: 0x0008A844
	public void Play()
	{
		foreach (SpawnPoint spawnPoint in this.spawnPoints)
		{
			if (this.spawnMode[spawnPoint] == ConfigFlagsUi.FlagMode.Blue)
			{
				spawnPoint.SetOwner(0, true);
			}
			else if (this.spawnMode[spawnPoint] == ConfigFlagsUi.FlagMode.Red)
			{
				spawnPoint.SetOwner(1, true);
			}
			else if (this.spawnMode[spawnPoint] == ConfigFlagsUi.FlagMode.Neutral)
			{
				spawnPoint.SetOwner(-1, true);
			}
			else
			{
				spawnPoint.gameObject.SetActive(false);
			}
		}
		OrderManager.RefreshAllOrders();
		MinimapUi.UpdateSpawnPointButtons();
		if (!GameManager.IsSpectating())
		{
			LoadoutUi.Show(false);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0008C6E4 File Offset: 0x0008A8E4
	public void Reset()
	{
		Debug.Log("Reset!");
		foreach (SpawnPoint spawnPoint in this.spawnPoints)
		{
			this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Neutral;
			if (!spawnPoint.isActiveAndEnabled)
			{
				this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Disabled;
			}
			else if (spawnPoint.defaultOwner == SpawnPoint.Team.Blue)
			{
				this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Blue;
			}
			else if (spawnPoint.defaultOwner == SpawnPoint.Team.Red)
			{
				this.spawnMode[spawnPoint] = ConfigFlagsUi.FlagMode.Red;
			}
			this.UpdateButtonGraphic(spawnPoint);
		}
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0008C76C File Offset: 0x0008A96C
	private void UpdateButtonGraphic(SpawnPoint spawnPoint)
	{
		ConfigFlagsUi.FlagMode flagMode = this.spawnMode[spawnPoint];
		Image component = this.spawnPointButton[spawnPoint].GetComponent<Image>();
		if (flagMode == ConfigFlagsUi.FlagMode.Neutral)
		{
			component.color = ColorScheme.TeamColor(-1);
			return;
		}
		if (flagMode == ConfigFlagsUi.FlagMode.Blue)
		{
			component.color = ColorScheme.TeamColor(0);
			return;
		}
		if (flagMode == ConfigFlagsUi.FlagMode.Red)
		{
			component.color = ColorScheme.TeamColor(1);
			return;
		}
		component.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
	}

	// Token: 0x040012A5 RID: 4773
	public RectTransform panel;

	// Token: 0x040012A6 RID: 4774
	public RawImage minimapImage;

	// Token: 0x040012A7 RID: 4775
	public GameObject buttonPrefab;

	// Token: 0x040012A8 RID: 4776
	private static bool isVisible;

	// Token: 0x040012A9 RID: 4777
	private SpawnPoint[] spawnPoints;

	// Token: 0x040012AA RID: 4778
	private Dictionary<SpawnPoint, ConfigFlagsUi.FlagMode> spawnMode;

	// Token: 0x040012AB RID: 4779
	private Dictionary<SpawnPoint, Button> spawnPointButton;

	// Token: 0x02000292 RID: 658
	private enum FlagMode
	{
		// Token: 0x040012AD RID: 4781
		Neutral,
		// Token: 0x040012AE RID: 4782
		Blue,
		// Token: 0x040012AF RID: 4783
		Red,
		// Token: 0x040012B0 RID: 4784
		Disabled
	}
}
