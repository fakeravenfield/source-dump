using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class CustomMapsBrowser : MonoBehaviour
{
	// Token: 0x06000CD3 RID: 3283 RVA: 0x0000A7E0 File Offset: 0x000089E0
	public static void QueueLoadData(CustomMapEntry entry)
	{
		CustomMapsBrowser.instance.dataLoadQueue.Enqueue(entry);
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0000A7F2 File Offset: 0x000089F2
	private void Awake()
	{
		CustomMapsBrowser.instance = this;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0000A7FA File Offset: 0x000089FA
	private void Start()
	{
		this.workshopButton.SetActive(GameManager.IsConnectedToSteam());
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0000A80C File Offset: 0x00008A0C
	private void Update()
	{
		if (this.dataLoadQueue.Count > 0)
		{
			this.dataLoadQueue.Dequeue().LoadImageAndData();
		}
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0007A3D8 File Offset: 0x000785D8
	public void SetupCustomMapEntries()
	{
		this.ClearEntries();
		foreach (ModInformation modInformation in ModManager.instance.GetActiveMods())
		{
			if (modInformation.HasLoadedContent())
			{
				foreach (FileInfo mapFile in modInformation.content.GetMaps())
				{
					this.AddEntry(modInformation, mapFile);
				}
			}
		}
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0007A480 File Offset: 0x00078680
	private void ClearEntries()
	{
		this.dataLoadQueue = new Queue<CustomMapEntry>();
		this.column = 0;
		this.row = 0;
		float num = 0.7f * (float)Screen.height;
		this.rows = Mathf.FloorToInt(num / 220f);
		this.entryHeight = 1f / (float)this.rows;
		this.noMapsPanel.SetActive(true);
		int childCount = this.contentPanel.childCount;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < childCount; i++)
		{
			list.Add(this.contentPanel.GetChild(i));
		}
		foreach (Transform transform in list)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0007A558 File Offset: 0x00078758
	private void AddEntry(ModInformation mod, FileInfo mapFile)
	{
		this.noMapsPanel.SetActive(false);
		InstantActionMaps.MapEntry mapEntry = new InstantActionMaps.MapEntry();
		string[] array = mapFile.Name.Split(new char[]
		{
			'.'
		});
		mapEntry.name = "";
		for (int i = 0; i < array.Length - 1; i++)
		{
			InstantActionMaps.MapEntry mapEntry2 = mapEntry;
			mapEntry2.name += array[i];
		}
		mapEntry.sceneName = mapFile.FullName;
		mapEntry.isCustomMap = true;
		mapEntry.hasLoadedMetaData = false;
		CustomMapEntry component = UnityEngine.Object.Instantiate<GameObject>(this.customMapEntryPrefab, this.contentPanel).GetComponent<CustomMapEntry>();
		RectTransform rectTransform = (RectTransform)component.transform;
		component.entry = mapEntry;
		component.mod = mod;
		Vector2 vector = new Vector2(0f, ((float)this.row + 0.5f) * this.entryHeight);
		rectTransform.anchorMin = vector;
		rectTransform.anchorMax = vector;
		rectTransform.anchoredPosition = new Vector2(((float)this.column + 0.5f) * 360f, 0f);
		component.SetTitle(mapEntry.name);
		this.contentPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)(this.column + 1) * 360f);
		this.row++;
		if (this.row >= this.rows)
		{
			this.row = 0;
			this.column++;
		}
	}

	// Token: 0x04000DCE RID: 3534
	public static CustomMapsBrowser instance;

	// Token: 0x04000DCF RID: 3535
	private const float SPACING_X = 360f;

	// Token: 0x04000DD0 RID: 3536
	private const float MIN_SPACING_Y = 220f;

	// Token: 0x04000DD1 RID: 3537
	public GameObject customMapEntryPrefab;

	// Token: 0x04000DD2 RID: 3538
	public RectTransform contentPanel;

	// Token: 0x04000DD3 RID: 3539
	public GameObject noMapsPanel;

	// Token: 0x04000DD4 RID: 3540
	public GameObject workshopButton;

	// Token: 0x04000DD5 RID: 3541
	private int column;

	// Token: 0x04000DD6 RID: 3542
	private int row;

	// Token: 0x04000DD7 RID: 3543
	private int rows = 3;

	// Token: 0x04000DD8 RID: 3544
	private float entryWidth;

	// Token: 0x04000DD9 RID: 3545
	private float entryHeight;

	// Token: 0x04000DDA RID: 3546
	private Queue<CustomMapEntry> dataLoadQueue;
}
