using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Campaign
{
	// Token: 0x020003F4 RID: 1012
	public class LevelHighlightPanel : MonoBehaviour
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x000A8F9C File Offset: 0x000A719C
		public void SetColor(Color color)
		{
			Graphic[] array = this.backgroundGraphics;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = color;
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000A8FC8 File Offset: 0x000A71C8
		public void Setup(LevelClickable level)
		{
			this.level = level;
			Color a = ColorScheme.TeamColor((int)level.defaultOwner);
			this.SetColor(Color.Lerp(a, Color.black, 0.6f));
			this.image.sprite = level.displayImage;
			this.nameText.text = level.displayName;
			this.descriptionText.text = level.description;
			if (level.highlightHoverRoot != null)
			{
				this.followTransform = level.highlightHoverRoot;
			}
			else
			{
				this.followTransform = level.transform;
			}
			this.recruitButton.onClick.AddListener(new UnityAction(level.OnPlayerWantsToRecruit));
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000A9078 File Offset: 0x000A7278
		public Button AddButton(string name, UnityAction onClickCallback)
		{
			Button componentInChildren = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.highlightContainer).GetComponentInChildren<Button>();
			componentInChildren.GetComponentInChildren<Text>().text = name;
			if (onClickCallback != null)
			{
				componentInChildren.onClick.AddListener(onClickCallback);
			}
			return componentInChildren;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000A90B8 File Offset: 0x000A72B8
		public void LateUpdate()
		{
			if (this.followTransform != null)
			{
				RectTransform rectTransform = (RectTransform)base.transform;
				Vector3 vector = CommandRoomMainCamera.instance.camera.WorldToScreenPoint(this.followTransform.position);
				if (vector.z < 0f)
				{
					vector.x = (float)(-(float)Screen.width * 100);
					vector.y = (float)(-(float)Screen.height * 100);
				}
				rectTransform.anchoredPosition = vector;
			}
			if (this.recruitButton.gameObject.activeInHierarchy)
			{
				this.recruitButton.interactable = this.level.CanRecruit();
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000A915C File Offset: 0x000A735C
		public void UpdateRecruitCount()
		{
			this.recruitText.text = string.Format("{0}<sprite index=0> at {1}{2}", this.level.remainingRecruits, this.level.recruitCost, ConquestCampaign.GetResourceSpriteTag(this.level.recruitCostType));
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00013B6E File Offset: 0x00011D6E
		public void ShowFull()
		{
			base.gameObject.SetActive(true);
			this.highlightContainer.gameObject.SetActive(true);
			this.recruitContainer.gameObject.SetActive(false);
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.highlightContainer);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00013BAE File Offset: 0x00011DAE
		public void ShowRecruit()
		{
			base.gameObject.SetActive(true);
			this.highlightContainer.gameObject.SetActive(false);
			this.recruitContainer.gameObject.SetActive(true);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0000969C File Offset: 0x0000789C
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04001B21 RID: 6945
		public Graphic[] backgroundGraphics;

		// Token: 0x04001B22 RID: 6946
		public TextMeshProUGUI nameText;

		// Token: 0x04001B23 RID: 6947
		public TextMeshProUGUI descriptionText;

		// Token: 0x04001B24 RID: 6948
		public Image image;

		// Token: 0x04001B25 RID: 6949
		public Transform highlightContainer;

		// Token: 0x04001B26 RID: 6950
		public Transform recruitContainer;

		// Token: 0x04001B27 RID: 6951
		public GameObject buttonPrefab;

		// Token: 0x04001B28 RID: 6952
		public TextMeshProUGUI recruitText;

		// Token: 0x04001B29 RID: 6953
		public Button recruitButton;

		// Token: 0x04001B2A RID: 6954
		private LevelClickable level;

		// Token: 0x04001B2B RID: 6955
		private Transform followTransform;
	}
}
