using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DF RID: 479
public class CustomMapEntry : MonoBehaviour
{
	// Token: 0x06000CCA RID: 3274 RVA: 0x0000A756 File Offset: 0x00008956
	public void SetTitle(string title)
	{
		base.GetComponentInChildren<Text>().text = title;
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0000A764 File Offset: 0x00008964
	private void Awake()
	{
		this.rectTransform = (RectTransform)base.transform;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0007A1D4 File Offset: 0x000783D4
	private void Update()
	{
		if (!this.queuedLoadImageAndData)
		{
			Vector3 vector = this.rectTransform.localToWorldMatrix.MultiplyPoint(this.rectTransform.rect.position);
			if (vector.x > -this.rectTransform.rect.width && vector.x < (float)Screen.width)
			{
				this.queuedLoadImageAndData = true;
				CustomMapsBrowser.QueueLoadData(this);
			}
		}
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0000A777 File Offset: 0x00008977
	public void LoadImageAndData()
	{
		this.LoadMetaData();
		if (!this.hasImage)
		{
			this.SetPreviewImageToModIcon();
		}
		this.entry.image = this.image.sprite;
		this.hasLoadedImageAndData = true;
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0007A24C File Offset: 0x0007844C
	public void LoadMetaData()
	{
		this.entry.hasLoadedMetaData = true;
		try
		{
			string text = this.entry.sceneName + ".png";
			string path = this.entry.sceneName + ".json";
			bool flag = File.Exists(text);
			GameManager.DebugVerbose(string.Format("Map image file path = {0}, exists = {1}", text, flag), Array.Empty<object>());
			if (flag)
			{
				Texture2D texture2D = new Texture2D(485, 300, TextureFormat.RGB24, false);
				texture2D.LoadImage(File.ReadAllBytes(text));
				this.image.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, 485f, 300f), Vector2.zero, 100f);
				this.hasImage = true;
			}
			if (File.Exists(path))
			{
				CustomMapEntry.MapMetaData mapMetaData = JsonUtility.FromJson<CustomMapEntry.MapMetaData>(File.ReadAllText(path));
				this.entry.suggestedBots = mapMetaData.suggestedBots;
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0007A350 File Offset: 0x00078550
	public void SetPreviewImageToModIcon()
	{
		if (!this.mod.HasLoadedContent() || !this.mod.content.HasIconImage())
		{
			return;
		}
		Sprite sprite = Sprite.Create(this.mod.iconTexture, new Rect(0f, 0f, (float)this.mod.iconTexture.width, (float)this.mod.iconTexture.height), Vector2.zero, 100f);
		this.image.sprite = sprite;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0000A7AA File Offset: 0x000089AA
	public void Select()
	{
		if (!this.hasLoadedImageAndData)
		{
			this.LoadImageAndData();
		}
		InstantActionMaps.SelectedCustomMapEntry(this.entry);
		MainMenu.instance.OpenPageIndex(3);
	}

	// Token: 0x04000DC6 RID: 3526
	public Image image;

	// Token: 0x04000DC7 RID: 3527
	[NonSerialized]
	public InstantActionMaps.MapEntry entry;

	// Token: 0x04000DC8 RID: 3528
	[NonSerialized]
	public ModInformation mod;

	// Token: 0x04000DC9 RID: 3529
	private RectTransform rectTransform;

	// Token: 0x04000DCA RID: 3530
	private bool queuedLoadImageAndData;

	// Token: 0x04000DCB RID: 3531
	private bool hasLoadedImageAndData;

	// Token: 0x04000DCC RID: 3532
	private bool hasImage;

	// Token: 0x020001E0 RID: 480
	[Serializable]
	public class MapMetaData
	{
		// Token: 0x04000DCD RID: 3533
		public int suggestedBots = 50;
	}
}
