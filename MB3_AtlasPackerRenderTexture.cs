using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000058 RID: 88
[ExecuteInEditMode]
public class MB3_AtlasPackerRenderTexture : MonoBehaviour
{
	// Token: 0x060001A7 RID: 423 RVA: 0x000448E0 File Offset: 0x00042AE0
	public Texture2D OnRenderAtlas(MB3_TextureCombiner combiner)
	{
		this.fastRenderer = new MB_TextureCombinerRenderTexture();
		this._doRenderAtlas = true;
		Texture2D result = this.fastRenderer.DoRenderAtlas(base.gameObject, this.width, this.height, this.padding, this.rects, this.textureSets, this.indexOfTexSetToRender, this.texPropertyName, this.resultMaterialTextureBlender, this.isNormalMap, this.fixOutOfBoundsUVs, this.considerNonTextureProperties, combiner, this.LOG_LEVEL);
		this._doRenderAtlas = false;
		return result;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00003258 File Offset: 0x00001458
	private void OnRenderObject()
	{
		if (this._doRenderAtlas)
		{
			this.fastRenderer.OnRenderObject();
			this._doRenderAtlas = false;
		}
	}

	// Token: 0x04000120 RID: 288
	private MB_TextureCombinerRenderTexture fastRenderer;

	// Token: 0x04000121 RID: 289
	private bool _doRenderAtlas;

	// Token: 0x04000122 RID: 290
	public int width;

	// Token: 0x04000123 RID: 291
	public int height;

	// Token: 0x04000124 RID: 292
	public int padding;

	// Token: 0x04000125 RID: 293
	public bool isNormalMap;

	// Token: 0x04000126 RID: 294
	public bool fixOutOfBoundsUVs;

	// Token: 0x04000127 RID: 295
	public bool considerNonTextureProperties;

	// Token: 0x04000128 RID: 296
	public MB3_TextureCombinerNonTextureProperties resultMaterialTextureBlender;

	// Token: 0x04000129 RID: 297
	public Rect[] rects;

	// Token: 0x0400012A RID: 298
	public Texture2D tex1;

	// Token: 0x0400012B RID: 299
	public List<MB_TexSet> textureSets;

	// Token: 0x0400012C RID: 300
	public int indexOfTexSetToRender;

	// Token: 0x0400012D RID: 301
	public ShaderTextureProperty texPropertyName;

	// Token: 0x0400012E RID: 302
	public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

	// Token: 0x0400012F RID: 303
	public Texture2D testTex;

	// Token: 0x04000130 RID: 304
	public Material testMat;
}
