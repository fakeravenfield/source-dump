using System;
using cakeslice;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000640 RID: 1600
	public class PrefabRenderer : MonoBehaviour
	{
		// Token: 0x060028DA RID: 10458 RVA: 0x0001C20B File Offset: 0x0001A40B
		private void OnEnable()
		{
			if (this.rendering)
			{
				this.rendering.SetActive(true);
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0001C226 File Offset: 0x0001A426
		private void OnDisable()
		{
			if (this.rendering)
			{
				this.rendering.SetActive(false);
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0001C241 File Offset: 0x0001A441
		public void SetLayer(int? layer)
		{
			this.layerOverride = layer;
			this.MoveRenderingToAppropriateLayer();
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000FB658 File Offset: 0x000F9858
		public void SetRendering(GameObject rendering)
		{
			if (this.rendering)
			{
				UnityEngine.Object.Destroy(this.rendering);
				this.rendering = null;
				this.assistant = null;
			}
			this.rendering = rendering;
			if (rendering)
			{
				this.assistant = rendering.GetComponentInChildren<PrefabRendererAssistant>();
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000FB6A8 File Offset: 0x000F98A8
		public GameObject RenderPrefab(GameObject prefab, PrefabRenderer.RenderMode mode = PrefabRenderer.RenderMode.Strict)
		{
			GameObject gameObject;
			if (mode == PrefabRenderer.RenderMode.Strict)
			{
				gameObject = Utils.Instantiate(prefab, base.transform, PrefabRenderer.ALLOWED_COMPONENTS, true);
			}
			else
			{
				if (mode != PrefabRenderer.RenderMode.NoColliders)
				{
					throw new NotImplementedException();
				}
				gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, base.transform);
				Collider[] componentsInChildren = gameObject.GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					UnityEngine.Object.Destroy(componentsInChildren[i]);
				}
			}
			Utils.SetStatic(gameObject, false);
			gameObject.name = "Prefab Rendering";
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			this.SetRendering(gameObject);
			this.MoveRenderingToAppropriateLayer();
			return gameObject;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000FB758 File Offset: 0x000F9958
		public GameObject RenderPrefab(AssetId id)
		{
			GameObject prefab = MapEditorAssistant.instance.GetPrefab(id, out this.asset);
			return this.RenderPrefab(prefab, PrefabRenderer.RenderMode.Strict);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0001C250 File Offset: 0x0001A450
		public bool IsPreview()
		{
			return this.preview;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x0001C258 File Offset: 0x0001A458
		public void RenderAsPreview(bool value)
		{
			if (this.preview != value)
			{
				this.preview = value;
				this.MoveRenderingToAppropriateLayer();
			}
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000FB780 File Offset: 0x000F9980
		private void MoveRenderingToAppropriateLayer()
		{
			if (this.rendering)
			{
				if (this.preview)
				{
					Utils.MoveToLayer(this.rendering, Layers.GetPreviewLayer());
					if (this.assistant && this.assistant.meshRenderer)
					{
						if (this.assistant.previewMaterial)
						{
							this.assistant.meshRenderer.enabled = true;
							this.assistant.meshRenderer.sharedMaterial = this.assistant.previewMaterial;
							return;
						}
						this.assistant.meshRenderer.enabled = false;
						return;
					}
				}
				else
				{
					int layer = (this.layerOverride != null) ? this.layerOverride.Value : base.gameObject.layer;
					Utils.MoveToLayer(this.rendering, layer);
					if (this.assistant && this.assistant.meshRenderer)
					{
						if (this.assistant.defaultMaterial)
						{
							this.assistant.meshRenderer.enabled = true;
							this.assistant.meshRenderer.sharedMaterial = this.assistant.defaultMaterial;
							return;
						}
						this.assistant.meshRenderer.enabled = false;
					}
				}
			}
		}

		// Token: 0x040026BE RID: 9918
		private static readonly Type[] ALLOWED_COMPONENTS = new Type[]
		{
			typeof(Transform),
			typeof(MeshFilter),
			typeof(MeshRenderer),
			typeof(LODGroup),
			typeof(BillboardRenderer),
			typeof(LineRenderer),
			typeof(CubeAutoUv),
			typeof(IAllowInPrefabRenderer),
			typeof(IOutlineIgnore),
			typeof(SpawnPoint),
			typeof(VehicleSpawner),
			typeof(PathfindingBox)
		};

		// Token: 0x040026BF RID: 9919
		private PrefabRendererAssistant assistant;

		// Token: 0x040026C0 RID: 9920
		private PrefabAsset asset;

		// Token: 0x040026C1 RID: 9921
		private GameObject rendering;

		// Token: 0x040026C2 RID: 9922
		private bool preview;

		// Token: 0x040026C3 RID: 9923
		private int? layerOverride;

		// Token: 0x02000641 RID: 1601
		public enum RenderMode
		{
			// Token: 0x040026C5 RID: 9925
			Strict,
			// Token: 0x040026C6 RID: 9926
			NoColliders
		}
	}
}
