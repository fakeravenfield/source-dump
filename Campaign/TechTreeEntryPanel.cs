using System;
using Campaign.Tech;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Campaign
{
	// Token: 0x020003FA RID: 1018
	public class TechTreeEntryPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x00013D04 File Offset: 0x00011F04
		private void Awake()
		{
			this.background = base.GetComponent<Graphic>();
			this.background.color = this.lockedColor;
			this.parentUi = base.GetComponentInParent<TechTreeUi>();
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000A9F9C File Offset: 0x000A819C
		public void SetEntry(TechTree techTree, TechTreeEntry entry)
		{
			this.entry = entry;
			this.title.text = entry.name;
			this.description.text = entry.description;
			this.icon.texture = entry.icon;
			this.title.raycastTarget = false;
			this.icon.raycastTarget = false;
			this.unlockButton.onClick.AddListener(new UnityAction(this.OnUnlockClicked));
			this.unlockText = this.unlockButton.GetComponentInChildren<TextMeshProUGUI>();
			RectTransform rectTransform = (RectTransform)base.transform;
			Vector2 position = entry.position;
			position.y = -position.y;
			rectTransform.anchoredPosition = position;
			this.hoverPanel.gameObject.SetActive(false);
			this.unlockButton.interactable = false;
			this.icon.color = new Color(1f, 1f, 1f, 0.4f);
			string text = string.Format("UNLOCK FOR {0}{1}", entry.price, ConquestCampaign.GetResourceSpriteTag(techTree.defaultPriceResource));
			foreach (ResourceAmount resourceAmount in entry.additionalCosts)
			{
				text += string.Format("  {0}{1}", resourceAmount.amount, ConquestCampaign.GetResourceSpriteTag(resourceAmount.type));
			}
			Color color = this.unlockText.color;
			color.a = 0.3f;
			this.unlockText.color = color;
			this.unlockText.text = text;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00013D2F File Offset: 0x00011F2F
		public void OnUnlockClicked()
		{
			CampaignBase.instance.TryUnlockTechTreeEntry(CampaignBase.instance.playerTeam, this.entry);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000AA14C File Offset: 0x000A834C
		public Vector2 GetArrowOutputPosition()
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			return rectTransform.anchoredPosition - new Vector2(0f, rectTransform.sizeDelta.y / 2f);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000AA18C File Offset: 0x000A838C
		public Vector2 GetArrowInputPosition()
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			return rectTransform.anchoredPosition + new Vector2(0f, rectTransform.sizeDelta.y / 2f);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000AA1CC File Offset: 0x000A83CC
		public void OnAvailable()
		{
			if (this.background != null)
			{
				this.background.color = this.availableColor;
			}
			this.unlockButton.interactable = true;
			this.icon.color = new Color(1f, 1f, 1f, 1f);
			Color color = this.unlockText.color;
			color.a = 1f;
			this.unlockText.color = color;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000AA24C File Offset: 0x000A844C
		public void OnUnlocked()
		{
			if (this.background != null)
			{
				this.background.color = this.unlockedColor;
			}
			this.unlockButton.gameObject.SetActive(false);
			this.icon.color = new Color(1f, 1f, 1f, 1f);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00013D4C File Offset: 0x00011F4C
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.hoverPanel.gameObject.SetActive(true);
			base.transform.SetAsLastSibling();
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00013D6A File Offset: 0x00011F6A
		public void OnPointerExit(PointerEventData eventData)
		{
			this.hoverPanel.gameObject.SetActive(false);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000AA2B0 File Offset: 0x000A84B0
		public void SetupHoverPanelPosition(Canvas canvas)
		{
			if (!this.automaticHoverPanelPosition)
			{
				return;
			}
			RectTransform rectTransform = (RectTransform)base.transform;
			Rect rect = RectTransformUtility.PixelAdjustRect((RectTransform)base.transform.parent, canvas);
			Vector2 pivot = this.hoverPanel.pivot;
			Vector2 anchorMin = this.hoverPanel.anchorMin;
			if (rectTransform.anchoredPosition.x < rect.width / 2f)
			{
				this.hoverPanel.pivot = new Vector2(0f, pivot.y);
				this.hoverPanel.anchorMin = new Vector2(1f, anchorMin.y);
				this.hoverPanel.anchorMax = new Vector2(1f, anchorMin.y);
				return;
			}
			this.hoverPanel.pivot = new Vector2(1f, pivot.y);
			this.hoverPanel.anchorMin = new Vector2(0f, anchorMin.y);
			this.hoverPanel.anchorMax = new Vector2(0f, anchorMin.y);
		}

		// Token: 0x04001B59 RID: 7001
		public TextMeshProUGUI title;

		// Token: 0x04001B5A RID: 7002
		public TextMeshProUGUI description;

		// Token: 0x04001B5B RID: 7003
		public RawImage icon;

		// Token: 0x04001B5C RID: 7004
		public Button unlockButton;

		// Token: 0x04001B5D RID: 7005
		public RectTransform hoverPanel;

		// Token: 0x04001B5E RID: 7006
		public bool automaticHoverPanelPosition = true;

		// Token: 0x04001B5F RID: 7007
		public Color lockedColor = new Color(0.1f, 0.1f, 0.1f, 0.4f);

		// Token: 0x04001B60 RID: 7008
		public Color availableColor = new Color(0.1f, 0.1f, 0.1f, 1f);

		// Token: 0x04001B61 RID: 7009
		public Color unlockedColor = Color.blue;

		// Token: 0x04001B62 RID: 7010
		private TechTreeUi parentUi;

		// Token: 0x04001B63 RID: 7011
		private TextMeshProUGUI unlockText;

		// Token: 0x04001B64 RID: 7012
		private Graphic background;

		// Token: 0x04001B65 RID: 7013
		private TechTreeEntry entry;
	}
}
