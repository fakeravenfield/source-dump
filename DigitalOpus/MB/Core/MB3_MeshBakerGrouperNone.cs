using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000423 RID: 1059
	[Serializable]
	public class MB3_MeshBakerGrouperNone : MB3_MeshBakerGrouperCore
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x0001445A File Offset: 0x0001265A
		public MB3_MeshBakerGrouperNone(GrouperData d)
		{
			this.d = d;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000AD0E8 File Offset: 0x000AB2E8
		public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
		{
			Debug.Log("Filtering into groups none");
			Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
			List<Renderer> list = new List<Renderer>();
			for (int i = 0; i < selection.Count; i++)
			{
				if (selection[i] != null)
				{
					list.Add(selection[i].GetComponent<Renderer>());
				}
			}
			dictionary.Add("MeshBaker", list);
			return dictionary;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void DrawGizmos(Bounds sourceObjectBounds)
		{
		}
	}
}
