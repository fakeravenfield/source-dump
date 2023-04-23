using System;
using MapEditor.Generators;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapEditor
{
	// Token: 0x02000666 RID: 1638
	public class BiomeContainer : MonoBehaviour
	{
		// Token: 0x0600298D RID: 10637 RVA: 0x0001C826 File Offset: 0x0001AA26
		public WaterLevel GetWaterLevel()
		{
			return this.waterLevel;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0001C82E File Offset: 0x0001AA2E
		public GameObject GetBackdrop()
		{
			return this.backdrop;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0001C836 File Offset: 0x0001AA36
		public BaseMapGenerator GetMapGenerator()
		{
			return this.mapGenerator;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0001C83E File Offset: 0x0001AA3E
		public PileTexturer GetPileTexturer()
		{
			return this.pileTexturer;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0001C846 File Offset: 0x0001AA46
		public Terrain GetTerrain()
		{
			return this.terrain;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000FD108 File Offset: 0x000FB308
		public void Populate(BiomeAsset asset, BiomeTerrainSettings settings)
		{
			settings.OverrideWithSaneDefaults();
			if (asset == BiomeAsset.empty)
			{
				throw new ArgumentException("Please supply a biome.", "asset");
			}
			bool flag = this.asset != asset || this.settings.useMapGenerator != settings.useMapGenerator;
			bool flag2 = this.settings != settings || flag;
			this.asset = asset;
			this.settings = settings;
			if (flag)
			{
				this.SetBiome(asset, settings);
			}
			if (flag2)
			{
				this.SetSize(asset, settings);
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000FD194 File Offset: 0x000FB394
		private void SetBiome(BiomeAsset asset, BiomeTerrainSettings settings)
		{
			bool flag = this.BeginUpdate();
			TerrainBiomeData biome = asset.biome;
			this.Clear();
			this.mapGenerator = null;
			this.terrain = null;
			GameObject gameObject = new GameObject("Terrain");
			gameObject.transform.parent = base.transform;
			this.terrain = gameObject.AddComponent<Terrain>();
			this.terrain.terrainData = new TerrainData();
			if (settings.useMapGenerator)
			{
				this.mapGenerator = UnityEngine.Object.Instantiate<GameObject>(biome.mapGeneratorPrefab, base.transform).GetComponent<BaseMapGenerator>();
				this.terrain.terrainData.terrainLayers = this.mapGenerator.layers;
			}
			gameObject.AddComponent<TerrainCollider>().terrainData = this.terrain.terrainData;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(biome.pileTexturerPrefab, base.transform);
			this.pileTexturer = gameObject2.GetComponent<PileTexturer>();
			this.originalWaterLevel = biome.waterLevel;
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(biome.waterLevelPrefab, base.transform);
			this.waterLevel = gameObject3.GetComponent<WaterLevel>();
			this.waterLevel.transform.position = Vector3.up * this.originalWaterLevel;
			if (biome.waterLevelScale > 0f)
			{
				this.waterLevel.transform.localScale = Vector3.one * biome.waterLevelScale;
			}
			this.backdrop = UnityEngine.Object.Instantiate<GameObject>(biome.backdropPrefab, base.transform);
			if (flag)
			{
				this.EndUpdate();
			}
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0001C84E File Offset: 0x0001AA4E
		public float GetWaterOffsetFromOriginal()
		{
			return this.waterLevel.transform.position.y - this.originalWaterLevel;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000FD304 File Offset: 0x000FB504
		private void SetSize(BiomeAsset asset, BiomeTerrainSettings settings)
		{
			bool flag = this.BeginUpdate();
			TerrainBiomeData biome = asset.biome;
			this.terrain.terrainData.heightmapResolution = settings.heightmapResolution + 1;
			this.terrain.terrainData.alphamapResolution = settings.alphamapResolution;
			this.terrain.terrainData.size = new Vector3((float)settings.size, (float)biome.terrainHeight, (float)settings.size);
			this.terrain.Flush();
			this.terrain.transform.position = -new Vector3(1f, 0f, 1f) * (float)settings.size / 2f;
			if (biome.waterLevelScale <= 0f)
			{
				this.waterLevel.transform.localScale = Vector3.one * (float)settings.size;
			}
			if (flag)
			{
				this.EndUpdate();
			}
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0001C86C File Offset: 0x0001AA6C
		public void Clear()
		{
			Utils.DestroyChildren(base.gameObject);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0001C879 File Offset: 0x0001AA79
		public bool BeginUpdate()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
				return true;
			}
			return false;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		public void EndUpdate()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x0001C897 File Offset: 0x0001AA97
		public void PersistToNextScene()
		{
			if (!this.persist)
			{
				this.persist = true;
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				SceneManager.sceneLoaded += this.SceneLoaded;
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000FD3F8 File Offset: 0x000FB5F8
		private void SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (this && base.gameObject && !this.persist)
			{
				SceneManager.sceneLoaded -= this.SceneLoaded;
				UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
			this.persist = false;
		}

		// Token: 0x0400270D RID: 9997
		private BaseMapGenerator mapGenerator;

		// Token: 0x0400270E RID: 9998
		private PileTexturer pileTexturer;

		// Token: 0x0400270F RID: 9999
		private WaterLevel waterLevel;

		// Token: 0x04002710 RID: 10000
		private GameObject backdrop;

		// Token: 0x04002711 RID: 10001
		private Terrain terrain;

		// Token: 0x04002712 RID: 10002
		private BiomeAsset asset;

		// Token: 0x04002713 RID: 10003
		private BiomeTerrainSettings settings;

		// Token: 0x04002714 RID: 10004
		private float originalWaterLevel;

		// Token: 0x04002715 RID: 10005
		private bool persist;
	}
}
