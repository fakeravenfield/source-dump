using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapEditor.Generators;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006C4 RID: 1732
	public class GenerateMapUI : MonoBehaviour
	{
		// Token: 0x06002BB3 RID: 11187 RVA: 0x0001E0A6 File Offset: 0x0001C2A6
		private void Awake()
		{
			this.onCancel = new UnityEvent();
			this.busyIndicator.SetActive(false);
			this.placeholder.SetActive(true);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x00102138 File Offset: 0x00100338
		private void Start()
		{
			this.SetupBiomesDropdown();
			this.SetupSizeToggles();
			this.SetupButtons();
			this.selectedAsset = this.biomes.First<KeyValuePair<string, BiomeAsset>>().Value;
			this.selectedSize = GenerateMapUI.Size.Small;
			this.displayedAsset = BiomeAsset.empty;
			this.displayedSize = GenerateMapUI.Size.None;
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x0010218C File Offset: 0x0010038C
		private void Update()
		{
			this.isBusy = false;
			this.showPlaceholder = true;
			BaseMapGenerator mapGenerator = this.biomeContainer.GetMapGenerator();
			WaterLevel waterLevel = this.biomeContainer.GetWaterLevel();
			GameObject backdrop = this.biomeContainer.GetBackdrop();
			if (mapGenerator)
			{
				this.isBusy = !mapGenerator.isDone;
				this.showPlaceholder = this.isBusy;
			}
			this.busyIndicator.SetActive(this.isBusy);
			this.placeholder.SetActive(this.showPlaceholder);
			if (waterLevel && waterLevel.gameObject)
			{
				waterLevel.gameObject.SetActive(!this.showPlaceholder);
			}
			if (backdrop)
			{
				backdrop.SetActive(!this.showPlaceholder);
			}
			if (this.IsTerrainGenerated())
			{
				if (this.CanGenerateTexture() && this.generateTextureWhenDone)
				{
					this.generateTextureWhenDone = false;
					this.GenerateTexture();
				}
				if (this.CanOpenEditor() && this.openEditorWhenDone)
				{
					this.openEditorWhenDone = false;
					this.OpenEditor();
				}
			}
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0001E0CB File Offset: 0x0001C2CB
		private bool IsTerrainGenerated()
		{
			return !this.isBusy && !this.showPlaceholder;
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		private bool CanGenerateTexture()
		{
			return this.IsTerrainGenerated();
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		private bool CanOpenEditor()
		{
			return this.IsTerrainGenerated() && !this.generateTextureWhenDone;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00102294 File Offset: 0x00100494
		private void SetupBiomesDropdown()
		{
			this.biomes = new Dictionary<string, BiomeAsset>();
			foreach (BiomeAsset biomeAsset in AssetTable.GetAllBiomes())
			{
				if (biomeAsset.biome.allowNewMaps)
				{
					this.biomes.Add(biomeAsset.biome.GetDisplayName(), biomeAsset);
				}
			}
			string[] options = (from kv in this.biomes
			select kv.Key).ToArray<string>();
			this.dropdownBiomes.SetOptions(options);
			this.dropdownBiomes.onSelectedValueChanged.AddListener(new UnityAction<string>(this.SelectBiome));
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0001E0FD File Offset: 0x0001C2FD
		private void SelectBiome(string name)
		{
			this.selectedAsset = this.biomes[name];
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00102360 File Offset: 0x00100560
		private void SetupSizeToggles()
		{
			this.toggleSmall.onValueChanged.AddListener(delegate(bool on)
			{
				this.SetSize(on, GenerateMapUI.Size.Small);
			});
			this.toggleMedium.onValueChanged.AddListener(delegate(bool on)
			{
				this.SetSize(on, GenerateMapUI.Size.Medium);
			});
			this.toggleLarge.onValueChanged.AddListener(delegate(bool on)
			{
				this.SetSize(on, GenerateMapUI.Size.Large);
			});
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0001E111 File Offset: 0x0001C311
		private void SetSize(bool doIt, GenerateMapUI.Size size)
		{
			if (doIt)
			{
				this.selectedSize = size;
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x001023C4 File Offset: 0x001005C4
		private void SetupButtons()
		{
			this.buttonGenerate.onClick.AddListener(new UnityAction(this.GenerateButtonClicked));
			this.buttonOK.onClick.AddListener(new UnityAction(this.OpenButtonClicked));
			this.buttonCancel.onClick.AddListener(new UnityAction(this.CancelButtonClicked));
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00102428 File Offset: 0x00100628
		private void GenerateButtonClicked()
		{
			this.displayedAsset != this.selectedAsset;
			GenerateMapUI.Size size = this.displayedSize;
			GenerateMapUI.Size size2 = this.selectedSize;
			this.displayedAsset = this.selectedAsset;
			this.displayedSize = this.selectedSize;
			BiomeAsset biomeAsset = this.selectedAsset;
			int num = this.TerrainSize(this.selectedSize);
			BiomeTerrainSettings settings = new BiomeTerrainSettings(num, num, num, true);
			this.biomeContainer.BeginUpdate();
			this.biomeContainer.Populate(this.selectedAsset, settings);
			this.biomeContainer.EndUpdate();
			int seed = this.random.Next(int.MinValue, int.MaxValue);
			Debug.Log("MapMagic seed: " + seed.ToString());
			BaseMapGenerator mapGenerator = this.biomeContainer.GetMapGenerator();
			mapGenerator.seed = seed;
			base.StartCoroutine(this.GenerateTerrainRoutine(mapGenerator));
			this.generateTextureWhenDone = true;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0001E11D File Offset: 0x0001C31D
		private IEnumerator GenerateTerrainRoutine(BaseMapGenerator mapGenerator)
		{
			this.progressWindow.Show();
			this.progressWindow.SetTitle("GENERATING MAP");
			this.progressWindow.SetStatus("Crunching numbers...");
			this.progressWindow.SetProgress(0f);
			Debug.LogFormat("Generating map using biome {0}", new object[]
			{
				this.selectedAsset.biome.name
			});
			Terrain terrain = this.biomeContainer.GetTerrain();
			Debug.Log(terrain);
			base.StartCoroutine(mapGenerator.Generate(terrain));
			while (!mapGenerator.isDone)
			{
				this.progressWindow.SetProgress(mapGenerator.progress);
				yield return null;
			}
			FixBundleShaders.ApplyTerrainFallbackMaterial(terrain);
			this.progressWindow.Hide();
			yield break;
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0010250C File Offset: 0x0010070C
		private void GenerateTexture()
		{
			PileTexturer pileTexturer = this.biomeContainer.GetPileTexturer();
			Terrain[] terrains = new Terrain[]
			{
				this.biomeContainer.GetTerrain()
			};
			pileTexturer.ApplyTexture(terrains);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0001E133 File Offset: 0x0001C333
		private int TerrainSize(GenerateMapUI.Size size)
		{
			if (size == GenerateMapUI.Size.Small)
			{
				return 512;
			}
			if (size != GenerateMapUI.Size.Medium)
			{
				return 2048;
			}
			return 1024;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0001E150 File Offset: 0x0001C350
		private void OpenButtonClicked()
		{
			if (!this.isBusy && this.showPlaceholder)
			{
				this.openEditorWhenDone = true;
				this.GenerateButtonClicked();
			}
			if (this.CanOpenEditor())
			{
				this.OpenEditor();
			}
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0001E17D File Offset: 0x0001C37D
		private void OpenEditor()
		{
			base.StartCoroutine(this.OpenEditorRoutine());
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0001E18C File Offset: 0x0001C38C
		private IEnumerator OpenEditorRoutine()
		{
			base.enabled = false;
			this.biomeContainer.PersistToNextScene();
			GeneratedSceneConstructor constructor = new GeneratedSceneConstructor(this.biomeContainer, this.displayedAsset);
			AsyncOperation loadScene = SceneConstructor.ReplaceSceneAsync(true, constructor);
			while (!loadScene.isDone)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0001E19B File Offset: 0x0001C39B
		private void CancelButtonClicked()
		{
			this.onCancel.Invoke();
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x0000969C File Offset: 0x0000789C
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		public void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		public bool IsVisible()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x04002859 RID: 10329
		public GameObject generatorContainer;

		// Token: 0x0400285A RID: 10330
		public BiomeContainer biomeContainer;

		// Token: 0x0400285B RID: 10331
		public GameObject busyIndicator;

		// Token: 0x0400285C RID: 10332
		public GameObject placeholder;

		// Token: 0x0400285D RID: 10333
		public Toggle toggleSmall;

		// Token: 0x0400285E RID: 10334
		public Toggle toggleMedium;

		// Token: 0x0400285F RID: 10335
		public Toggle toggleLarge;

		// Token: 0x04002860 RID: 10336
		public DropdownWithText dropdownBiomes;

		// Token: 0x04002861 RID: 10337
		public Button buttonGenerate;

		// Token: 0x04002862 RID: 10338
		public Button buttonOK;

		// Token: 0x04002863 RID: 10339
		public Button buttonCancel;

		// Token: 0x04002864 RID: 10340
		public ProgressUI progressWindow;

		// Token: 0x04002865 RID: 10341
		public UnityEvent onCancel;

		// Token: 0x04002866 RID: 10342
		private Dictionary<string, BiomeAsset> biomes;

		// Token: 0x04002867 RID: 10343
		private BiomeAsset displayedAsset;

		// Token: 0x04002868 RID: 10344
		private GenerateMapUI.Size displayedSize;

		// Token: 0x04002869 RID: 10345
		private BiomeAsset selectedAsset;

		// Token: 0x0400286A RID: 10346
		private GenerateMapUI.Size selectedSize;

		// Token: 0x0400286B RID: 10347
		private bool isBusy;

		// Token: 0x0400286C RID: 10348
		private bool showPlaceholder = true;

		// Token: 0x0400286D RID: 10349
		private bool generateTextureWhenDone;

		// Token: 0x0400286E RID: 10350
		private bool openEditorWhenDone;

		// Token: 0x0400286F RID: 10351
		private System.Random random = new System.Random();

		// Token: 0x020006C5 RID: 1733
		private enum Size
		{
			// Token: 0x04002871 RID: 10353
			None,
			// Token: 0x04002872 RID: 10354
			Small,
			// Token: 0x04002873 RID: 10355
			Medium,
			// Token: 0x04002874 RID: 10356
			Large
		}
	}
}
