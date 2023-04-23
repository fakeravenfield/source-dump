using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class MB3_BatchPrefabBaker : MonoBehaviour
{
	// Token: 0x040000B2 RID: 178
	public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

	// Token: 0x040000B3 RID: 179
	public MB3_BatchPrefabBaker.MB3_PrefabBakerRow[] prefabRows;

	// Token: 0x040000B4 RID: 180
	public string outputPrefabFolder;

	// Token: 0x0200003E RID: 62
	[Serializable]
	public class MB3_PrefabBakerRow
	{
		// Token: 0x040000B5 RID: 181
		public GameObject sourcePrefab;

		// Token: 0x040000B6 RID: 182
		public GameObject resultPrefab;
	}
}
