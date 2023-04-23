using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005C4 RID: 1476
	[Serializable]
	public class AssetTableData : ScriptableObject
	{
		// Token: 0x0600264E RID: 9806 RVA: 0x0001A746 File Offset: 0x00018946
		public override string ToString()
		{
			return base.name;
		}

		// Token: 0x040024B6 RID: 9398
		public string comment;

		// Token: 0x040024B7 RID: 9399
		public AssetTableData.Entry[] entries;

		// Token: 0x020005C5 RID: 1477
		[Serializable]
		public class Entry
		{
			// Token: 0x06002650 RID: 9808 RVA: 0x000F4510 File Offset: 0x000F2710
			public Entry(string guid, string path, UnityEngine.Object asset)
			{
				this.guid = guid;
				this.path = path;
				if (asset is Material)
				{
					this.material = (Material)asset;
					return;
				}
				if (asset is GameObject)
				{
					this.gameObject = (GameObject)asset;
					return;
				}
				if (asset is TerrainBiomeData)
				{
					this.biome = (TerrainBiomeData)asset;
					return;
				}
				if (asset is Texture)
				{
					this.texture = (Texture2D)asset;
					return;
				}
				if (asset is AudioClip)
				{
					this.audioClip = (AudioClip)asset;
					return;
				}
				throw new Exception("Cannot create entry for Unity.Object: " + ((asset != null) ? asset.ToString() : null));
			}

			// Token: 0x040024B8 RID: 9400
			public string path;

			// Token: 0x040024B9 RID: 9401
			public string guid;

			// Token: 0x040024BA RID: 9402
			public GameObject gameObject;

			// Token: 0x040024BB RID: 9403
			public Material material;

			// Token: 0x040024BC RID: 9404
			public TerrainBiomeData biome;

			// Token: 0x040024BD RID: 9405
			public Texture2D texture;

			// Token: 0x040024BE RID: 9406
			public AudioClip audioClip;
		}
	}
}
