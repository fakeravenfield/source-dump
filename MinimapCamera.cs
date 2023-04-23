using System;
using UnityEngine;

// Token: 0x020001BC RID: 444
public class MinimapCamera : MonoBehaviour
{
	// Token: 0x06000BFE RID: 3070 RVA: 0x00077994 File Offset: 0x00075B94
	private void Awake()
	{
		MinimapCamera.instance = this;
		this.resolution = 1024;
		if (GameManager.UI_SCALE > 1.1f)
		{
			this.resolution = 2048;
		}
		this.camera = base.GetComponent<Camera>();
		this.minimapRenderTexture = new RenderTexture(this.resolution, this.resolution, 16);
		this.camera.targetTexture = this.minimapRenderTexture;
		this.camera.enabled = false;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00077A0C File Offset: 0x00075C0C
	public void Render()
	{
		bool fog = RenderSettings.fog;
		RenderSettings.fog = false;
		Terrain[] array = UnityEngine.Object.FindObjectsOfType<Terrain>();
		float[] array2 = new float[array.Length];
		float[] array3 = new float[array.Length];
		float[] array4 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].treeBillboardDistance;
			array3[i] = array[i].treeCrossFadeLength;
			array4[i] = array[i].treeDistance;
			array[i].treeBillboardDistance = 0f;
			array[i].treeCrossFadeLength = 0f;
			array[i].treeDistance = 999999f;
		}
		this.camera.Render();
		RenderSettings.fog = fog;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].treeBillboardDistance = array2[j];
			array[j].treeCrossFadeLength = array3[j];
			array[j].treeDistance = array4[j];
		}
		this.camera.enabled = false;
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00009E61 File Offset: 0x00008061
	public Texture GetTexture()
	{
		return this.minimapRenderTexture;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00009E69 File Offset: 0x00008069
	public static Vector3 WorldToNormalizedPosition(Vector3 worldPosition)
	{
		return MinimapCamera.instance.camera.WorldToViewportPoint(worldPosition);
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00077B04 File Offset: 0x00075D04
	public static Vector3 WorldToMinimapScreenPosition(Vector3 worldPosition)
	{
		RectTransform rectTransform = MinimapUi.instance.minimap.rectTransform;
		Rect rect = rectTransform.rect;
		float x = rectTransform.lossyScale.x;
		Vector3 a = rect.size * x;
		return rectTransform.position + Vector3.Scale(a, MinimapCamera.WorldToNormalizedPosition(worldPosition));
	}

	// Token: 0x04000D16 RID: 3350
	private const int RESOLUTION = 1024;

	// Token: 0x04000D17 RID: 3351
	private const int RESOLUTION_EXTRA = 2048;

	// Token: 0x04000D18 RID: 3352
	public static MinimapCamera instance;

	// Token: 0x04000D19 RID: 3353
	[NonSerialized]
	public Camera camera;

	// Token: 0x04000D1A RID: 3354
	[NonSerialized]
	public RenderTexture minimapRenderTexture;

	// Token: 0x04000D1B RID: 3355
	[NonSerialized]
	public int resolution;
}
