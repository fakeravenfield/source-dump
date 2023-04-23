using System;
using System.Collections;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005CD RID: 1485
	public class AssetPreviewTextureRenderer : MonoBehaviour
	{
		// Token: 0x0600267B RID: 9851 RVA: 0x0001AA34 File Offset: 0x00018C34
		private void Start()
		{
			this.previewCamera = base.GetComponent<Camera>();
			this.previewCamera.enabled = false;
			base.enabled = false;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0001AA55 File Offset: 0x00018C55
		public void ProcessJobs()
		{
			if (this.runner == null)
			{
				this.runner = new MonitoredCoroutine(delegate(Exception ex)
				{
					if (ex != null)
					{
						Debug.LogError("An exception occured while rendering asset preview textures");
						Debug.LogException(ex);
					}
					this.runner = null;
				});
				base.StartCoroutine(this.runner.Monitor(this.Render()));
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0001AA8E File Offset: 0x00018C8E
		private IEnumerator Render()
		{
			for (;;)
			{
				AssetBrowserUI.RenderPrefabPreviewTextureJob job = AssetBrowserUI.instance.GetNextRenderJob();
				if (job == null)
				{
					break;
				}
				GameObject preview = job.Instantiate();
				if (preview)
				{
					RenderTexture results = new RenderTexture(128, 128, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
					preview.transform.position = Vector3.zero;
					preview.transform.Rotate(new Vector3(0f, 30f, 0f), Space.World);
					preview.transform.Rotate(new Vector3(-25f, 0f, 0f), Space.World);
					GameManager.SetupRecursiveLayer(preview.transform, 30);
					yield return new WaitForEndOfFrame();
					MeshRenderer[] componentsInChildren = preview.GetComponentsInChildren<MeshRenderer>();
					if (componentsInChildren.Length != 0)
					{
						Bounds bounds = componentsInChildren[0].bounds;
						for (int i = 1; i < componentsInChildren.Length; i++)
						{
							bounds.Encapsulate(componentsInChildren[i].bounds);
						}
						this.previewCamera.transform.position = bounds.center + new Vector3(0f, 0f, -500f);
						this.previewCamera.orthographicSize = Mathf.Max(bounds.extents.x, bounds.extents.y);
						this.previewCamera.orthographicSize *= 1.1f;
						this.previewCamera.targetTexture = results;
						this.previewCamera.Render();
						job.SetPreview(results);
					}
					UnityEngine.Object.Destroy(preview);
					yield return new WaitForEndOfFrame();
					job = null;
					preview = null;
					results = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x040024D7 RID: 9431
		private const int PREVIEW_TEXTURE_DIMENSIONS = 128;

		// Token: 0x040024D8 RID: 9432
		private const int PREVIEW_RENDER_LAYER = 30;

		// Token: 0x040024D9 RID: 9433
		private Camera previewCamera;

		// Token: 0x040024DA RID: 9434
		private MonitoredCoroutine runner;
	}
}
