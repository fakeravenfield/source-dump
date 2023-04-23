using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign.Tech
{
	// Token: 0x02000418 RID: 1048
	[CreateAssetMenu(fileName = "New Tech Tree", menuName = "Campaign/Tech Tree")]
	public class TechTree : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x000AC49C File Offset: 0x000AA69C
		public void UpdateViewportSize()
		{
			if (this.entries.Count == 0)
			{
				this.viewportSize = new Vector2(100f, 100f);
				return;
			}
			Vector2 vector = Vector2.zero;
			foreach (TechTreeEntry techTreeEntry in this.entries)
			{
				vector = Vector2.Max(vector, techTreeEntry.position);
			}
			this.viewportSize = new Vector2(vector.x + 300f, vector.y + 300f);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000AC544 File Offset: 0x000AA744
		public bool IsAvailableForUnlock(int team, TechTreeEntry entry)
		{
			if (TechManager.IsEntryUnlocked(team, entry))
			{
				return false;
			}
			if (entry.alwaysAvailable)
			{
				return true;
			}
			if (!this.entries.Contains(entry))
			{
				return false;
			}
			foreach (int index in entry.parents)
			{
				if (!TechManager.IsEntryUnlocked(team, this.entries[index]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000AC5D0 File Offset: 0x000AA7D0
		public TechTreeEntry GetRandomAvailableEntry(int team)
		{
			List<int> list = new List<int>(this.entries.Count);
			for (int i = 0; i < this.entries.Count; i++)
			{
				list.Add(i);
			}
			while (list.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				if (this.IsAvailableForUnlock(team, this.entries[index]))
				{
					return this.entries[index];
				}
				list.RemoveAt(index);
			}
			return null;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0000296E File Offset: 0x00000B6E
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000AC650 File Offset: 0x000AA850
		public void OnAfterDeserialize()
		{
			foreach (TechTreeEntry techTreeEntry in this.entries)
			{
				techTreeEntry.techTree = this;
			}
		}

		// Token: 0x04001BD2 RID: 7122
		private const float WINDOW_PADDING = 300f;

		// Token: 0x04001BD3 RID: 7123
		private const float WINDOW_WIDTH = 1000f;

		// Token: 0x04001BD4 RID: 7124
		public ResourceType defaultPriceResource = ResourceType.Research;

		// Token: 0x04001BD5 RID: 7125
		public List<TechTreeEntry> entries;

		// Token: 0x04001BD6 RID: 7126
		[NonSerialized]
		public Vector2 viewportSize;
	}
}
