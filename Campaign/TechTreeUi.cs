using System;
using System.Collections;
using System.Collections.Generic;
using Campaign.Tech;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003FB RID: 1019
	public class TechTreeUi : MonoBehaviour
	{
		// Token: 0x060019A0 RID: 6560 RVA: 0x000AA420 File Offset: 0x000A8620
		private void Start()
		{
			this.canvas = base.GetComponent<Canvas>();
			this.techTree.UpdateViewportSize();
			this.panelContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.techTree.viewportSize.x);
			this.panelContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.techTree.viewportSize.y);
			this.entryPanel = new Dictionary<TechTreeEntry, TechTreeEntryPanel>();
			foreach (TechTreeEntry entry in this.techTree.entries)
			{
				try
				{
					this.CreatePanelForEntry(entry);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			base.StartCoroutine(this.SetupGraphicalElements());
			this.UpdateState();
			this.Hide();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000AA500 File Offset: 0x000A8700
		public void UpdateState()
		{
			foreach (TechTreeEntry techTreeEntry in this.techTree.entries)
			{
				if (TechManager.IsEntryUnlocked(CampaignBase.instance.playerTeam, techTreeEntry))
				{
					this.entryPanel[techTreeEntry].OnUnlocked();
				}
				else if (this.techTree.IsAvailableForUnlock(CampaignBase.instance.playerTeam, techTreeEntry))
				{
					this.entryPanel[techTreeEntry].OnAvailable();
				}
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00013D7D File Offset: 0x00011F7D
		private IEnumerator SetupGraphicalElements()
		{
			yield return 0;
			foreach (TechTreeEntry key in this.techTree.entries)
			{
				this.entryPanel[key].SetupHoverPanelPosition(this.canvas);
			}
			using (List<TechTreeEntry>.Enumerator enumerator = this.techTree.entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TechTreeEntry techTreeEntry = enumerator.Current;
					foreach (int index in techTreeEntry.parents)
					{
						this.CreateArrow(this.techTree.entries[index], techTreeEntry);
					}
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000AA5A0 File Offset: 0x000A87A0
		private void CreateArrow(TechTreeEntry fromEntry, TechTreeEntry toEntry)
		{
			Vector2 arrowOutputPosition = this.entryPanel[fromEntry].GetArrowOutputPosition();
			Vector2 vector = this.entryPanel[toEntry].GetArrowInputPosition() - arrowOutputPosition;
			RectTransform rectTransform = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.arrowPrefab, this.panelContainer).transform;
			rectTransform.anchoredPosition = arrowOutputPosition;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			rectTransform.localEulerAngles = new Vector3(0f, 0f, z);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector.magnitude);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00013D8C File Offset: 0x00011F8C
		public void Show()
		{
			this.canvas.enabled = true;
			CommandRoomMainCamera.instance.isLocked = true;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00013DA5 File Offset: 0x00011FA5
		public void Hide()
		{
			this.canvas.enabled = false;
			CommandRoomMainCamera.instance.isLocked = false;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000AA634 File Offset: 0x000A8834
		private void CreatePanelForEntry(TechTreeEntry entry)
		{
			TechTreeEntryPanel component = UnityEngine.Object.Instantiate<GameObject>(this.entryPanelPrefab, this.panelContainer).GetComponent<TechTreeEntryPanel>();
			component.SetEntry(this.techTree, entry);
			this.entryPanel.Add(entry, component);
		}

		// Token: 0x04001B66 RID: 7014
		public TechTree techTree;

		// Token: 0x04001B67 RID: 7015
		public RectTransform panelContainer;

		// Token: 0x04001B68 RID: 7016
		public GameObject entryPanelPrefab;

		// Token: 0x04001B69 RID: 7017
		public GameObject arrowPrefab;

		// Token: 0x04001B6A RID: 7018
		private Dictionary<TechTreeEntry, TechTreeEntryPanel> entryPanel;

		// Token: 0x04001B6B RID: 7019
		private Canvas canvas;
	}
}
