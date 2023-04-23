using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000626 RID: 1574
	public abstract class MapEditorObject : MonoBehaviour, IPropertyContainer
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x0001A746 File Offset: 0x00018946
		// (set) Token: 0x06002862 RID: 10338 RVA: 0x0001BD90 File Offset: 0x00019F90
		[ShowInMapEditor(1)]
		public new string name
		{
			get
			{
				return base.name;
			}
			set
			{
				base.name = value;
			}
		}

		// Token: 0x06002863 RID: 10339
		public abstract string GetCategoryName();

		// Token: 0x06002864 RID: 10340 RVA: 0x0001BD99 File Offset: 0x00019F99
		public bool ShouldSave()
		{
			return this && base.gameObject && !this.IsDeleted() && !this.IsPreview();
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0001BDC3 File Offset: 0x00019FC3
		public bool IsPreview()
		{
			return this.prefabRenderer.IsPreview();
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		public virtual void RenderAsPreview(bool value)
		{
			this.selectableObject.enabled = !value;
			this.prefabRenderer.RenderAsPreview(value);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0001BDED File Offset: 0x00019FED
		public bool IsDeleted()
		{
			return this.selectableObject.IsDeleted();
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0001BDFA File Offset: 0x00019FFA
		public virtual void Delete()
		{
			this.selectableObject.Delete();
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0001BE07 File Offset: 0x0001A007
		public virtual void Undelete()
		{
			this.selectableObject.Undelete();
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0001BE14 File Offset: 0x0001A014
		public virtual void Destroy()
		{
			this.selectableObject.Destroy();
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0001BE21 File Offset: 0x0001A021
		public SelectableObject GetSelectableObject()
		{
			return this.selectableObject;
		}

		// Token: 0x0600286C RID: 10348
		public abstract MapEditorObject Clone();

		// Token: 0x0600286D RID: 10349 RVA: 0x000FA610 File Offset: 0x000F8810
		protected static T Create<T>(GameObject renderingPrefab, string name = null, Transform parent = null, bool addPathfindingColliders = true) where T : MapEditorObject
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.parent = parent;
			gameObject.transform.localRotation = renderingPrefab.transform.localRotation;
			gameObject.transform.localScale = renderingPrefab.transform.localScale;
			T t = gameObject.AddComponent<T>();
			PrefabRenderer prefabRenderer = gameObject.AddComponent<PrefabRenderer>();
			prefabRenderer.RenderPrefab(renderingPrefab, PrefabRenderer.RenderMode.Strict);
			SelectableObject selectableObject = gameObject.AddComponent<SelectableObject>();
			selectableObject.GenerateColliders(renderingPrefab);
			PathfindingColliders pathfindingColliders = gameObject.AddComponent<PathfindingColliders>();
			if (addPathfindingColliders)
			{
				pathfindingColliders.CopyColliders(renderingPrefab);
			}
			t.name = (string.IsNullOrEmpty(name) ? t.GetCategoryName() : name);
			t.prefabRenderer = prefabRenderer;
			t.selectableObject = selectableObject;
			t.pathfindingColliders = pathfindingColliders;
			selectableObject.Scan();
			return t;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000FA6E0 File Offset: 0x000F88E0
		protected static T Instantiate<T>(GameObject instancePrefab, GameObject renderingPrefab, string name = null, Transform parent = null) where T : MapEditorObject
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(instancePrefab, parent);
			T orCreateComponent = gameObject.GetOrCreateComponent<T>();
			PrefabRenderer orCreateComponent2 = orCreateComponent.GetOrCreateComponent<PrefabRenderer>();
			SelectableObject orCreateComponent3 = gameObject.GetOrCreateComponent<SelectableObject>();
			if (renderingPrefab)
			{
				orCreateComponent2.RenderPrefab(renderingPrefab, PrefabRenderer.RenderMode.NoColliders);
				orCreateComponent3.GenerateColliders(renderingPrefab);
			}
			else
			{
				orCreateComponent2.SetRendering(gameObject);
				orCreateComponent3.GenerateColliders(gameObject);
			}
			orCreateComponent.name = name;
			orCreateComponent.prefabRenderer = orCreateComponent2;
			orCreateComponent.selectableObject = orCreateComponent3;
			orCreateComponent3.Scan();
			return orCreateComponent;
		}

		// Token: 0x0400266A RID: 9834
		protected PrefabRenderer prefabRenderer;

		// Token: 0x0400266B RID: 9835
		protected SelectableObject selectableObject;

		// Token: 0x0400266C RID: 9836
		protected PathfindingColliders pathfindingColliders;
	}
}
