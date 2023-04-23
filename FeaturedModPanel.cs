using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029D RID: 669
public class FeaturedModPanel : MonoBehaviour
{
	// Token: 0x060011C0 RID: 4544 RVA: 0x0008D044 File Offset: 0x0008B244
	public void Setup(SteamworksWrapper.UGCQueryResult item)
	{
		this.titleText.text = item.details.m_rgchTitle;
		this.descriptionText.text = item.details.m_rgchDescription;
		this.url = item.previewImageURL;
		this.fileId = item.details.m_nPublishedFileId;
		if ((GameManager.instance.steamworks.GetItemState(this.fileId) & 1U) != 0U)
		{
			this.MarkSubscribed();
		}
		else
		{
			this.MarkUnsubscribed();
		}
		this.HideDescription();
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0000DF2D File Offset: 0x0000C12D
	private void OnEnable()
	{
		if (!this.hasLoadedImage && !string.IsNullOrEmpty(this.url))
		{
			base.StartCoroutine(this.UpdateImage(this.url));
		}
		this.subscribeButton.gameObject.SetActive(GameManager.IsConnectedToSteam());
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x0008D0CC File Offset: 0x0008B2CC
	private void Update()
	{
		RectTransform rectTransform = (RectTransform)base.transform.parent;
		Vector3[] array = new Vector3[4];
		rectTransform.GetWorldCorners(array);
		bool flag = Input.mousePosition.x > array[0].x && Input.mousePosition.y > array[0].y && Input.mousePosition.x < array[2].x && Input.mousePosition.y < array[2].y;
		if (flag && !this.lastMouseOver)
		{
			this.ShowDescription();
		}
		else if (!flag && this.lastMouseOver)
		{
			this.HideDescription();
		}
		this.lastMouseOver = flag;
		this.scale = Mathf.MoveTowards(this.scale, this.targetScale, 1.6f * Time.deltaTime);
		this.image.rectTransform.localScale = new Vector3(this.scale, this.scale, this.scale);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0008D1D0 File Offset: 0x0008B3D0
	private void ShowDescription()
	{
		this.descriptionText.enabled = true;
		this.image.CrossFadeColor(new Color(0.2f, 0.2f, 0.2f, 1f), 0.2f, false, false);
		this.targetScale = 1.2f;
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x0000DF6C File Offset: 0x0000C16C
	private void HideDescription()
	{
		this.descriptionText.enabled = false;
		this.image.CrossFadeColor(Color.white, 0.3f, false, false);
		this.targetScale = 1f;
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0000DF9C File Offset: 0x0000C19C
	private IEnumerator UpdateImage(string url)
	{
		using (WWW request = new WWW(url))
		{
			yield return request;
			Sprite sprite = Sprite.Create(request.texture, new Rect(0f, 0f, (float)request.texture.width, (float)request.texture.height), Vector2.zero);
			this.image.sprite = sprite;
			this.hasLoadedImage = true;
		}
		WWW request = null;
		yield break;
		yield break;
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
	public void ChangeButtonSprite(Sprite sprite)
	{
		this.subscribeImage.sprite = sprite;
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
	public void MarkSubscribed()
	{
		this.ChangeButtonSprite(this.isSubscribedSprite);
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0000DFCE File Offset: 0x0000C1CE
	public void MarkUnsubscribed()
	{
		this.ChangeButtonSprite(this.canSubscribeSprite);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x0008D220 File Offset: 0x0008B420
	public void Subscribe()
	{
		this.MarkSubscribed();
		using (List<ModInformation>.Enumerator enumerator = ModManager.instance.mods.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.workshopItemId == this.fileId)
				{
					return;
				}
			}
		}
		GameManager.instance.steamworks.SubscribeToItem(this.fileId);
	}

	// Token: 0x040012DE RID: 4830
	public Image image;

	// Token: 0x040012DF RID: 4831
	public Text titleText;

	// Token: 0x040012E0 RID: 4832
	public Text descriptionText;

	// Token: 0x040012E1 RID: 4833
	public string url;

	// Token: 0x040012E2 RID: 4834
	public Button subscribeButton;

	// Token: 0x040012E3 RID: 4835
	public Image subscribeImage;

	// Token: 0x040012E4 RID: 4836
	private bool lastMouseOver;

	// Token: 0x040012E5 RID: 4837
	private bool hasLoadedImage;

	// Token: 0x040012E6 RID: 4838
	public Sprite isSubscribedSprite;

	// Token: 0x040012E7 RID: 4839
	public Sprite canSubscribeSprite;

	// Token: 0x040012E8 RID: 4840
	private PublishedFileId_t fileId;

	// Token: 0x040012E9 RID: 4841
	private float scale = 1f;

	// Token: 0x040012EA RID: 4842
	private float targetScale = 1f;
}
