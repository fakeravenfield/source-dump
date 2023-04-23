using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000688 RID: 1672
	public class AssetBrowserUI : MonoBehaviour
	{
		// Token: 0x06002A8C RID: 10892 RVA: 0x0001D3C2 File Offset: 0x0001B5C2
		private void Awake()
		{
			AssetBrowserUI.instance = this;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000FFB10 File Offset: 0x000FDD10
		private void Start()
		{
			this.editor = MapEditor.instance;
			this.input = this.editor.GetInput();
			this.entryLookup = new Dictionary<Button, AssetBrowserUI.PrefabCategoryEntry>();
			this.renderPreviewTextureJobList = new List<AssetBrowserUI.RenderPrefabPreviewTextureJob>();
			this.orginizer = new AssetBrowserUI.PrefabOrganizer();
			this.searchCategory = this.AddSearchCategory();
			this.builtinCategory = this.AddBuiltInCategory();
			this.AddAssetCategories();
			this.GenerateListItems();
			this.searchInput.onTextChanged.AddListener(new UnityAction<string>(this.Search));
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000FFB9C File Offset: 0x000FDD9C
		private AssetBrowserUI.PrefabCategory AddSearchCategory()
		{
			AssetBrowserUI.PrefabCategory category = new AssetBrowserUI.PrefabCategory("​Search Results", new AssetBrowserUI.PrefabCategoryEntry[0]);
			return this.orginizer.AddCategory(category);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000FFBC8 File Offset: 0x000FDDC8
		private AssetBrowserUI.PrefabCategory AddBuiltInCategory()
		{
			AssetBrowserUI.PrefabCategoryEntry[] array = new AssetBrowserUI.PrefabCategoryEntry[5];
			array[0] = new AssetBrowserUI.PrefabCategoryEntry("Capture Point", () => MeoCapturePoint.Create(null));
			array[1] = new AssetBrowserUI.PrefabCategoryEntry("Spawn Point", () => MeoSpawnPoint.Create(null));
			array[2] = new AssetBrowserUI.PrefabCategoryEntry("Vehicle Spawn", () => MeoVehicleSpawner.Create(null));
			array[3] = new AssetBrowserUI.PrefabCategoryEntry("Turret Spawn", () => MeoTurretSpawner.Create(null));
			array[4] = new AssetBrowserUI.PrefabCategoryEntry("Avoidance Box", () => MeoAvoidanceBox.Create(null));
			AssetBrowserUI.PrefabCategoryEntry[] entires = array;
			AssetBrowserUI.PrefabCategory category = new AssetBrowserUI.PrefabCategory("Built-in Types", entires);
			return this.orginizer.AddCategory(category);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0001D3CA File Offset: 0x0001B5CA
		private void AddAssetCategories()
		{
			this.orginizer.AddAssetCategories();
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0001D3D7 File Offset: 0x0001B5D7
		private void Update()
		{
			if (this.input.CloseWindow())
			{
				this.Hide();
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		public void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0000969C File Offset: 0x0000789C
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		public bool PlaceAgain()
		{
			if (this.placeAction != null)
			{
				this.placeAction();
				return true;
			}
			return false;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000FFCD4 File Offset: 0x000FDED4
		public AssetBrowserUI.RenderPrefabPreviewTextureJob GetNextRenderJob()
		{
			AssetBrowserUI.RenderPrefabPreviewTextureJob result = null;
			if (this.renderPreviewTextureJobList.Count > 0)
			{
				int index = 0;
				for (int i = 0; i < this.renderPreviewTextureJobList.Count; i++)
				{
					if (this.renderPreviewTextureJobList[i].HasPriority())
					{
						index = i;
						break;
					}
				}
				result = this.renderPreviewTextureJobList[index];
				this.renderPreviewTextureJobList.RemoveAt(index);
			}
			return result;
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000FFD3C File Offset: 0x000FDF3C
		private void GenerateListItems()
		{
			this.categoryListView.Clear();
			this.assetListView.Clear();
			using (IEnumerator<AssetBrowserUI.PrefabCategory> enumerator = this.orginizer.GetCategories().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AssetBrowserUI.PrefabCategory category = enumerator.Current;
					this.categoryListView.Add(category.name, delegate
					{
						this.ShowCategory(category);
					});
				}
			}
			AssetBrowserUI.PrefabCategoryEntry[] array = (from entry in this.orginizer.GetEntries()
			orderby entry.name
			select entry).ToArray<AssetBrowserUI.PrefabCategoryEntry>();
			int num = 0;
			AssetBrowserUI.PrefabCategoryEntry[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				AssetBrowserUI.PrefabCategoryEntry entry = array2[i];
				entry.orderByName = num++;
				entry.listItem = this.assetListView.Add(entry.name, delegate
				{
					this.EntryClicked(entry);
				});
				entry.listItem.gameObject.SetActive(false);
				this.entryLookup.Add(entry.listItem, entry);
				this.renderPreviewTextureJobList.Add(new AssetBrowserUI.RenderPrefabPreviewTextureJob(entry));
			}
			this.ShowCategory(this.builtinCategory);
			this.previewRender.ProcessJobs();
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000FFEE0 File Offset: 0x000FE0E0
		private void ShowCategory(AssetBrowserUI.PrefabCategory category)
		{
			foreach (AssetBrowserUI.PrefabCategoryEntry prefabCategoryEntry in this.orginizer.GetEntries())
			{
				prefabCategoryEntry.listItem.gameObject.SetActive(false);
			}
			AssetBrowserUI.PrefabCategoryEntry[] entries = category.entries;
			for (int i = 0; i < entries.Length; i++)
			{
				entries[i].listItem.gameObject.SetActive(true);
			}
			this.assetListView.SortItems((Button item) => this.entryLookup[item].orderByScore);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x0001D404 File Offset: 0x0001B604
		private void ShowSearchResults()
		{
			this.ShowCategory(this.searchCategory);
			this.assetListView.SortItems((Button item) => this.entryLookup[item].orderByScore);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0001D429 File Offset: 0x0001B629
		private void Search(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				this.ShowCategory(this.builtinCategory);
				return;
			}
			this.UpdateSearchResults(query);
			this.ShowSearchResults();
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000FFF7C File Offset: 0x000FE17C
		private void UpdateSearchResults(string query)
		{
			this.searchCategory.entries = new AssetBrowserUI.PrefabCategoryEntry[0];
			int num = 0;
			foreach (AssetBrowserUI.PrefabCategoryEntry prefabCategoryEntry in this.orginizer.GetEntries().ToArray<AssetBrowserUI.PrefabCategoryEntry>())
			{
				prefabCategoryEntry.orderByScore = FuzzySearch.GetScore(query, prefabCategoryEntry.name);
				num = Mathf.Max(num, prefabCategoryEntry.orderByScore);
			}
			int num2 = Mathf.Max(1, num / 3);
			List<AssetBrowserUI.PrefabCategoryEntry> list = new List<AssetBrowserUI.PrefabCategoryEntry>();
			foreach (AssetBrowserUI.PrefabCategoryEntry prefabCategoryEntry2 in this.orginizer.GetEntries().ToArray<AssetBrowserUI.PrefabCategoryEntry>())
			{
				if (prefabCategoryEntry2.orderByScore > num2)
				{
					list.Add(prefabCategoryEntry2);
				}
			}
			this.searchCategory.entries = list.ToArray();
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00100044 File Offset: 0x000FE244
		private void EntryClicked(AssetBrowserUI.PrefabCategoryEntry entry)
		{
			this.placeAction = delegate()
			{
				MapEditorObject mapEditorObject = entry.createObject();
				MeTools.instance.SwitchToPlaceTool(new MapEditorObject[]
				{
					mapEditorObject
				});
				this.Hide();
			};
			this.placeAction();
		}

		// Token: 0x040027A0 RID: 10144
		public static AssetBrowserUI instance;

		// Token: 0x040027A1 RID: 10145
		public InputWithText searchInput;

		// Token: 0x040027A2 RID: 10146
		public ListView categoryListView;

		// Token: 0x040027A3 RID: 10147
		public ListView assetListView;

		// Token: 0x040027A4 RID: 10148
		public AssetPreviewTextureRenderer previewRender;

		// Token: 0x040027A5 RID: 10149
		private MapEditor editor;

		// Token: 0x040027A6 RID: 10150
		private MeInput input;

		// Token: 0x040027A7 RID: 10151
		private UnityAction placeAction;

		// Token: 0x040027A8 RID: 10152
		private AssetBrowserUI.PrefabOrganizer orginizer;

		// Token: 0x040027A9 RID: 10153
		private AssetBrowserUI.PrefabCategory builtinCategory;

		// Token: 0x040027AA RID: 10154
		private AssetBrowserUI.PrefabCategory searchCategory;

		// Token: 0x040027AB RID: 10155
		private Dictionary<Button, AssetBrowserUI.PrefabCategoryEntry> entryLookup;

		// Token: 0x040027AC RID: 10156
		private List<AssetBrowserUI.RenderPrefabPreviewTextureJob> renderPreviewTextureJobList;

		// Token: 0x040027AD RID: 10157
		private static readonly Type[] PREVIEWED_COMPONENTS = new Type[]
		{
			typeof(MeshFilter),
			typeof(MeshRenderer),
			typeof(LODGroup),
			typeof(BillboardRenderer),
			typeof(LineRenderer),
			typeof(CircleRenderer),
			typeof(ParticleSystem)
		};

		// Token: 0x02000689 RID: 1673
		private class PrefabOrganizer
		{
			// Token: 0x06002AA1 RID: 10913 RVA: 0x001000F8 File Offset: 0x000FE2F8
			public void AddAssetCategories()
			{
				Dictionary<string, List<PrefabAsset>> dictionary = new Dictionary<string, List<PrefabAsset>>();
				foreach (PrefabAsset prefabAsset in AssetTable.GetAllPrefabs())
				{
					string path = prefabAsset.path;
					string key = "";
					string[] array = path.Split(new char[]
					{
						'/'
					});
					if (array.Length > 4)
					{
						key = array.Skip(3).First<string>();
					}
					else if (array.Length > 3)
					{
						key = array.Skip(array.Length - 2).First<string>();
					}
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, new List<PrefabAsset>());
					}
					dictionary[key].Add(prefabAsset);
				}
				foreach (KeyValuePair<string, List<PrefabAsset>> keyValuePair in dictionary)
				{
					AssetBrowserUI.PrefabCategoryEntry[] entires = (from asset in keyValuePair.Value
					select new AssetBrowserUI.PrefabCategoryEntry(asset, () => MeoPrefab.Create(asset.id, null))).ToArray<AssetBrowserUI.PrefabCategoryEntry>();
					AssetBrowserUI.PrefabCategory category = new AssetBrowserUI.PrefabCategory(keyValuePair.Key, entires);
					category = this.AddCategory(category);
				}
			}

			// Token: 0x06002AA2 RID: 10914 RVA: 0x00100238 File Offset: 0x000FE438
			public AssetBrowserUI.PrefabCategory AddCategory(AssetBrowserUI.PrefabCategory category)
			{
				AssetBrowserUI.PrefabCategory prefabCategory = (from c in this.categories
				where c.name == category.name
				select c).FirstOrDefault<AssetBrowserUI.PrefabCategory>();
				if (prefabCategory != null)
				{
					prefabCategory.entries = prefabCategory.entries.Concat(category.entries).ToArray<AssetBrowserUI.PrefabCategoryEntry>();
					return prefabCategory;
				}
				this.categories.Add(category);
				this.categories.Sort((AssetBrowserUI.PrefabCategory a, AssetBrowserUI.PrefabCategory b) => string.Compare(a.name, b.name));
				return category;
			}

			// Token: 0x06002AA3 RID: 10915 RVA: 0x0001D473 File Offset: 0x0001B673
			public IEnumerable<AssetBrowserUI.PrefabCategory> GetCategories()
			{
				return this.categories;
			}

			// Token: 0x06002AA4 RID: 10916 RVA: 0x0001D47B File Offset: 0x0001B67B
			public IEnumerable<AssetBrowserUI.PrefabCategoryEntry> GetEntries()
			{
				return this.categories.SelectMany((AssetBrowserUI.PrefabCategory c) => c.entries);
			}

			// Token: 0x040027AE RID: 10158
			private List<AssetBrowserUI.PrefabCategory> categories = new List<AssetBrowserUI.PrefabCategory>();
		}

		// Token: 0x0200068D RID: 1677
		private class PrefabCategory
		{
			// Token: 0x06002AAE RID: 10926 RVA: 0x0001D4F9 File Offset: 0x0001B6F9
			public PrefabCategory(string name, AssetBrowserUI.PrefabCategoryEntry[] entires)
			{
				this.name = name;
				this.entries = entires;
			}

			// Token: 0x040027B5 RID: 10165
			public readonly string name;

			// Token: 0x040027B6 RID: 10166
			public AssetBrowserUI.PrefabCategoryEntry[] entries;
		}

		// Token: 0x0200068E RID: 1678
		public class PrefabCategoryEntry
		{
			// Token: 0x06002AAF RID: 10927 RVA: 0x0001D50F File Offset: 0x0001B70F
			public PrefabCategoryEntry(string name, Func<MapEditorObject> createObject)
			{
				this.name = FuzzySearch.GetDisplayName(name);
				this.createObject = createObject;
				this.orderByScore = 0;
				this.orderByName = 0;
				this.listItem = null;
			}

			// Token: 0x06002AB0 RID: 10928 RVA: 0x0010030C File Offset: 0x000FE50C
			public PrefabCategoryEntry(PrefabAsset asset, Func<MapEditorObject> createObject)
			{
				this.name = FuzzySearch.GetDisplayName(asset.gameObject.name);
				this.createObject = createObject;
				this.asset = asset;
				this.orderByScore = 0;
				this.orderByName = 0;
				this.listItem = null;
			}

			// Token: 0x040027B7 RID: 10167
			public readonly string name;

			// Token: 0x040027B8 RID: 10168
			public readonly Func<MapEditorObject> createObject;

			// Token: 0x040027B9 RID: 10169
			public readonly PrefabAsset asset;

			// Token: 0x040027BA RID: 10170
			public int orderByScore;

			// Token: 0x040027BB RID: 10171
			public int orderByName;

			// Token: 0x040027BC RID: 10172
			public Button listItem;
		}

		// Token: 0x0200068F RID: 1679
		public class RenderPrefabPreviewTextureJob
		{
			// Token: 0x06002AB1 RID: 10929 RVA: 0x0001D53F File Offset: 0x0001B73F
			public RenderPrefabPreviewTextureJob(AssetBrowserUI.PrefabCategoryEntry entry)
			{
				this.entry = entry;
			}

			// Token: 0x06002AB2 RID: 10930 RVA: 0x0001D54E File Offset: 0x0001B74E
			public bool HasPriority()
			{
				return this.entry.listItem.gameObject.activeInHierarchy;
			}

			// Token: 0x06002AB3 RID: 10931 RVA: 0x00100358 File Offset: 0x000FE558
			public GameObject Instantiate()
			{
				GameObject gameObject = null;
				Texture texture = null;
				PrefabAsset asset = this.entry.asset;
				if (this.entry.asset.gameObject)
				{
					GameObject source = this.entry.asset.gameObject;
					MeoPrefabAssistant componentInChildren = this.entry.asset.gameObject.GetComponentInChildren<MeoPrefabAssistant>();
					if (componentInChildren)
					{
						if (componentInChildren.inEditorRendering)
						{
							source = componentInChildren.inEditorRendering;
						}
						if (componentInChildren.previewTexture)
						{
							texture = componentInChildren.previewTexture;
						}
					}
					if (!texture)
					{
						gameObject = Utils.CloneGameObject(source, null, AssetBrowserUI.PREVIEWED_COMPONENTS);
						Utils.SetStatic(gameObject, false);
					}
				}
				else
				{
					MapEditorObject mapEditorObject = this.entry.createObject();
					gameObject = mapEditorObject.gameObject;
					UnityEngine.Object.Destroy(mapEditorObject);
				}
				if (texture)
				{
					this.SetPreview(texture);
				}
				else
				{
					this.StripGameObject(gameObject);
				}
				return gameObject;
			}

			// Token: 0x06002AB4 RID: 10932 RVA: 0x0001D565 File Offset: 0x0001B765
			public void SetPreview(Texture previewRendering)
			{
				this.entry.listItem.GetComponent<RawImage>().texture = previewRendering;
			}

			// Token: 0x06002AB5 RID: 10933 RVA: 0x0001D57D File Offset: 0x0001B77D
			private void StripGameObject(GameObject gameObject)
			{
				this.DestroyComponents<Joint>(gameObject);
				this.DestroyComponents<Weapon>(gameObject);
				this.DestroyComponents<FlareLayer>(gameObject);
				this.RecursiveStripComponents(gameObject.transform);
			}

			// Token: 0x06002AB6 RID: 10934 RVA: 0x00100438 File Offset: 0x000FE638
			private void RecursiveStripComponents(Transform transform)
			{
				if (!transform.gameObject.activeInHierarchy)
				{
					return;
				}
				Component[] components = transform.GetComponents<Component>();
				for (int i = components.Length - 1; i >= 0; i--)
				{
					Component component = components[i];
					if (!(component == null))
					{
						try
						{
							Type type = component.GetType();
							if (!(type == typeof(Transform)) && !type.IsSubclassOf(typeof(Renderer)) && !(type == typeof(MeshFilter)) && !(type == typeof(PrefabRenderer)))
							{
								if (type == typeof(Cloth))
								{
									((Cloth)component).enabled = false;
								}
								else if (type == typeof(Rigidbody))
								{
									((Rigidbody)component).isKinematic = true;
									UnityEngine.Object.Destroy(component);
								}
								else
								{
									UnityEngine.Object.Destroy(component);
								}
							}
						}
						catch (Exception exception)
						{
							Debug.LogException(exception);
						}
					}
				}
				for (int j = 0; j < transform.childCount; j++)
				{
					this.RecursiveStripComponents(transform.GetChild(j));
				}
			}

			// Token: 0x06002AB7 RID: 10935 RVA: 0x0010055C File Offset: 0x000FE75C
			private void DestroyComponents<T>(GameObject gameObject) where T : Component
			{
				T[] componentsInChildren = gameObject.GetComponentsInChildren<T>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					UnityEngine.Object.Destroy(componentsInChildren[i]);
				}
			}

			// Token: 0x040027BD RID: 10173
			private AssetBrowserUI.PrefabCategoryEntry entry;
		}
	}
}
