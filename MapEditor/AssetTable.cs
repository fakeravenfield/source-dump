using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005C1 RID: 1473
	public static class AssetTable
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x0001A580 File Offset: 0x00018780
		private static AssetTable.AssetTableImplementation GetInstance()
		{
			if (AssetTable.instance == null)
			{
				AssetTable.instance = new AssetTable.AssetTableImplementation();
			}
			return AssetTable.instance;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0001A598 File Offset: 0x00018798
		public static PrefabAsset GetPrefab(AssetId id)
		{
			return AssetTable.GetInstance().GetPrefab(id);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0001A5A5 File Offset: 0x000187A5
		public static MaterialAsset GetMaterial(AssetId id)
		{
			return AssetTable.GetInstance().GetMaterial(id);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0001A5B2 File Offset: 0x000187B2
		public static BiomeAsset GetBiome(AssetId id)
		{
			return AssetTable.GetInstance().GetBiome(id);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0001A5BF File Offset: 0x000187BF
		public static TextureAsset GetTexture(AssetId id)
		{
			return AssetTable.GetInstance().GetTexture(id);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0001A5CC File Offset: 0x000187CC
		public static TextureAsset FindTexture(Texture2D texture)
		{
			return AssetTable.GetInstance().FindTexture(texture);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0001A5D9 File Offset: 0x000187D9
		public static AudioAsset GetAudioClip(AssetId id)
		{
			return AssetTable.GetInstance().GetAudioClip(id);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0001A5E6 File Offset: 0x000187E6
		public static IEnumerable<PrefabAsset> GetAllPrefabs()
		{
			return AssetTable.GetInstance().GetAllPrefabs();
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0001A5F2 File Offset: 0x000187F2
		public static IEnumerable<MaterialAsset> GetAllMaterials()
		{
			return AssetTable.GetInstance().GetAllMaterials();
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0001A5FE File Offset: 0x000187FE
		public static IEnumerable<BiomeAsset> GetAllBiomes()
		{
			return AssetTable.GetInstance().GetAllBiomes();
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0001A60A File Offset: 0x0001880A
		public static IEnumerable<TextureAsset> GetAllTextures()
		{
			return AssetTable.GetInstance().GetAllTextures();
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0001A616 File Offset: 0x00018816
		public static IEnumerable<AudioAsset> GetAllAudioClips()
		{
			return AssetTable.GetInstance().GetAllAudioClips();
		}

		// Token: 0x040024A6 RID: 9382
		public const string TABLE_RESOURCE_NAME = "MapEditor/AssetTable";

		// Token: 0x040024A7 RID: 9383
		public const string TABLE_RESOURCE_PATH = "Assets/Resources/MapEditor/AssetTable.asset";

		// Token: 0x040024A8 RID: 9384
		public const string SNAPSHOT_FOLDER = "Assets/Resources/MapEditor/";

		// Token: 0x040024A9 RID: 9385
		public const string SNAPSHOT_PREFIX = "Snapshot";

		// Token: 0x040024AA RID: 9386
		public const string REPLACEMENT_RESOURCE_NAME = "MapEditor/ReplacementTable";

		// Token: 0x040024AB RID: 9387
		public const string REPLACEMENT_RESOURCE_PATH = "Assets/Resources/MapEditor/ReplacementTable.json";

		// Token: 0x040024AC RID: 9388
		private static AssetTable.AssetTableImplementation instance;

		// Token: 0x020005C2 RID: 1474
		private class AssetTableImplementation
		{
			// Token: 0x06002638 RID: 9784 RVA: 0x000F4098 File Offset: 0x000F2298
			public AssetTableImplementation()
			{
				this.table = AssetTable.AssetTableImplementation.LoadAssetTable();
				this.replacements = AssetTable.AssetTableImplementation.LoadReplacementTable();
				this.entries = new Dictionary<AssetId, AssetTableData.Entry>();
				this.prefabs = new Dictionary<AssetId, PrefabAsset>();
				this.materials = new Dictionary<AssetId, MaterialAsset>();
				this.biomes = new Dictionary<AssetId, BiomeAsset>();
				this.textures = new Dictionary<AssetId, TextureAsset>();
				this.audioClips = new Dictionary<AssetId, AudioAsset>();
				this.PopulateEntries();
				this.PopulatePrefabs();
				this.PopulateMaterials();
				this.PopulateBiomes();
				this.PopulateTextures();
				this.PopulateAudioClips();
			}

			// Token: 0x06002639 RID: 9785 RVA: 0x000F4128 File Offset: 0x000F2328
			private void PopulateEntries()
			{
				foreach (AssetTableData.Entry entry in this.table.entries)
				{
					AssetId key = new AssetId(entry.guid);
					this.entries.Add(key, entry);
				}
				foreach (ReplacementTableData.Entry entry2 in this.replacements.entries)
				{
					AssetId key2 = new AssetId(entry2.old);
					AssetId key3 = new AssetId(entry2.@new);
					if (this.entries.ContainsKey(key3))
					{
						this.entries.Add(key2, this.entries[key3]);
					}
				}
			}

			// Token: 0x0600263A RID: 9786 RVA: 0x000F41D8 File Offset: 0x000F23D8
			private void PopulatePrefabs()
			{
				foreach (AssetId assetId in this.entries.Keys)
				{
					AssetTableData.Entry entry = this.entries[assetId];
					if (entry.gameObject)
					{
						PrefabAsset value = new PrefabAsset(assetId, entry.path, entry.gameObject);
						this.prefabs.Add(assetId, value);
					}
				}
			}

			// Token: 0x0600263B RID: 9787 RVA: 0x000F4264 File Offset: 0x000F2464
			private void PopulateMaterials()
			{
				foreach (AssetId assetId in this.entries.Keys)
				{
					AssetTableData.Entry entry = this.entries[assetId];
					if (entry.material)
					{
						MaterialAsset value = new MaterialAsset(assetId, entry.path, entry.material);
						this.materials.Add(assetId, value);
					}
				}
			}

			// Token: 0x0600263C RID: 9788 RVA: 0x000F42F0 File Offset: 0x000F24F0
			private void PopulateBiomes()
			{
				foreach (AssetId assetId in this.entries.Keys)
				{
					AssetTableData.Entry entry = this.entries[assetId];
					if (entry.biome)
					{
						BiomeAsset value = new BiomeAsset(assetId, entry.path, entry.biome);
						this.biomes.Add(assetId, value);
					}
				}
			}

			// Token: 0x0600263D RID: 9789 RVA: 0x000F437C File Offset: 0x000F257C
			private void PopulateTextures()
			{
				foreach (AssetId assetId in this.entries.Keys)
				{
					AssetTableData.Entry entry = this.entries[assetId];
					if (entry.texture)
					{
						TextureAsset value = new TextureAsset(assetId, entry.path, entry.texture);
						this.textures.Add(assetId, value);
					}
				}
			}

			// Token: 0x0600263E RID: 9790 RVA: 0x000F4408 File Offset: 0x000F2608
			private void PopulateAudioClips()
			{
				foreach (AssetId assetId in this.entries.Keys)
				{
					AssetTableData.Entry entry = this.entries[assetId];
					if (entry.audioClip)
					{
						AudioAsset value = new AudioAsset(assetId, entry.path, entry.audioClip);
						this.audioClips.Add(assetId, value);
					}
				}
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x0001A622 File Offset: 0x00018822
			private static AssetTableData LoadAssetTable()
			{
				AssetTableData assetTableData = Resources.Load<AssetTableData>("MapEditor/AssetTable");
				if (assetTableData == null)
				{
					throw new Exception("Unable to load asset table from resource: MapEditor/AssetTable");
				}
				return assetTableData;
			}

			// Token: 0x06002640 RID: 9792 RVA: 0x000F4494 File Offset: 0x000F2694
			private static ReplacementTableData LoadReplacementTable()
			{
				ReplacementTableData replacementTableData = JsonUtility.FromJson<ReplacementTableData>(Resources.Load<TextAsset>("MapEditor/ReplacementTable").text);
				if (replacementTableData == null)
				{
					Debug.LogWarning("Unable to load asset replacement table from resource: MapEditor/ReplacementTable");
					replacementTableData = new ReplacementTableData();
				}
				return replacementTableData;
			}

			// Token: 0x06002641 RID: 9793 RVA: 0x0001A642 File Offset: 0x00018842
			public PrefabAsset GetPrefab(AssetId id)
			{
				if (this.prefabs.ContainsKey(id))
				{
					return this.prefabs[id];
				}
				return PrefabAsset.empty;
			}

			// Token: 0x06002642 RID: 9794 RVA: 0x0001A664 File Offset: 0x00018864
			public MaterialAsset GetMaterial(AssetId id)
			{
				if (this.materials.ContainsKey(id))
				{
					return this.materials[id];
				}
				return MaterialAsset.empty;
			}

			// Token: 0x06002643 RID: 9795 RVA: 0x0001A686 File Offset: 0x00018886
			public BiomeAsset GetBiome(AssetId id)
			{
				if (this.biomes.ContainsKey(id))
				{
					return this.biomes[id];
				}
				return BiomeAsset.empty;
			}

			// Token: 0x06002644 RID: 9796 RVA: 0x0001A6A8 File Offset: 0x000188A8
			public TextureAsset GetTexture(AssetId id)
			{
				if (this.textures.ContainsKey(id))
				{
					return this.textures[id];
				}
				return TextureAsset.empty;
			}

			// Token: 0x06002645 RID: 9797 RVA: 0x000F44CC File Offset: 0x000F26CC
			public TextureAsset FindTexture(Texture2D texture)
			{
				KeyValuePair<AssetId, TextureAsset> keyValuePair = this.textures.FirstOrDefault((KeyValuePair<AssetId, TextureAsset> kv) => kv.Value.texture == texture);
				TextureAsset value = keyValuePair.Value;
				return keyValuePair.Value;
			}

			// Token: 0x06002646 RID: 9798 RVA: 0x0001A6CA File Offset: 0x000188CA
			public AudioAsset GetAudioClip(AssetId id)
			{
				if (this.audioClips.ContainsKey(id))
				{
					return this.audioClips[id];
				}
				return AudioAsset.empty;
			}

			// Token: 0x06002647 RID: 9799 RVA: 0x0001A6EC File Offset: 0x000188EC
			public IEnumerable<PrefabAsset> GetAllPrefabs()
			{
				return this.prefabs.Values;
			}

			// Token: 0x06002648 RID: 9800 RVA: 0x0001A6F9 File Offset: 0x000188F9
			public IEnumerable<MaterialAsset> GetAllMaterials()
			{
				return this.materials.Values;
			}

			// Token: 0x06002649 RID: 9801 RVA: 0x0001A706 File Offset: 0x00018906
			public IEnumerable<BiomeAsset> GetAllBiomes()
			{
				return this.biomes.Values;
			}

			// Token: 0x0600264A RID: 9802 RVA: 0x0001A713 File Offset: 0x00018913
			public IEnumerable<TextureAsset> GetAllTextures()
			{
				return this.textures.Values;
			}

			// Token: 0x0600264B RID: 9803 RVA: 0x0001A720 File Offset: 0x00018920
			public IEnumerable<AudioAsset> GetAllAudioClips()
			{
				return this.audioClips.Values;
			}

			// Token: 0x040024AD RID: 9389
			private readonly AssetTableData table;

			// Token: 0x040024AE RID: 9390
			private readonly ReplacementTableData replacements;

			// Token: 0x040024AF RID: 9391
			private readonly Dictionary<AssetId, AssetTableData.Entry> entries;

			// Token: 0x040024B0 RID: 9392
			private readonly Dictionary<AssetId, PrefabAsset> prefabs;

			// Token: 0x040024B1 RID: 9393
			private readonly Dictionary<AssetId, MaterialAsset> materials;

			// Token: 0x040024B2 RID: 9394
			private readonly Dictionary<AssetId, BiomeAsset> biomes;

			// Token: 0x040024B3 RID: 9395
			private readonly Dictionary<AssetId, TextureAsset> textures;

			// Token: 0x040024B4 RID: 9396
			private readonly Dictionary<AssetId, AudioAsset> audioClips;
		}
	}
}
