using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003EB RID: 1003
	public class ClickableManager : MonoBehaviour
	{
		// Token: 0x06001919 RID: 6425 RVA: 0x00013774 File Offset: 0x00011974
		public static void RegisterClickable(CommandRoomClickable clickable)
		{
			ClickableManager.instance.clickables.Add(clickable);
			ClickableManager.instance.UpdateClickableState(clickable);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000A8694 File Offset: 0x000A6894
		public static void OnClickableGroupsChanged(int[] newClickableGroups)
		{
			ClickableManager.instance.currentClickableGroups = newClickableGroups;
			foreach (CommandRoomClickable clickable in ClickableManager.instance.clickables)
			{
				ClickableManager.instance.UpdateClickableState(clickable);
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00013791 File Offset: 0x00011991
		private void Awake()
		{
			ClickableManager.instance = this;
			this.clickables = new List<CommandRoomClickable>();
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000A86FC File Offset: 0x000A68FC
		private void UpdateClickableState(CommandRoomClickable clickable)
		{
			for (int i = 0; i < this.currentClickableGroups.Length; i++)
			{
				if (clickable.clickableGroup == this.currentClickableGroups[i])
				{
					clickable.ActivateColliders();
					return;
				}
			}
			clickable.DeactivateColliders();
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000A873C File Offset: 0x000A693C
		public static bool IsClickableGroupActive(int group)
		{
			int[] array = ClickableManager.instance.currentClickableGroups;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == group)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000A876C File Offset: 0x000A696C
		public static bool IsAnyClickableGroupActive(IEnumerable<int> groups)
		{
			using (IEnumerator<int> enumerator = groups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ClickableManager.IsClickableGroupActive(enumerator.Current))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04001AEE RID: 6894
		public static ClickableManager instance;

		// Token: 0x04001AEF RID: 6895
		[NonSerialized]
		public List<CommandRoomClickable> clickables;

		// Token: 0x04001AF0 RID: 6896
		[NonSerialized]
		public int[] currentClickableGroups = new int[1];
	}
}
