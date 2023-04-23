using System;
using System.Collections.Generic;
using System.Diagnostics;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class MB_TextureCombinerRenderTexture
{
	// Token: 0x060001A0 RID: 416 RVA: 0x00043908 File Offset: 0x00041B08
	public Texture2D DoRenderAtlas(GameObject gameObject, int width, int height, int padding, Rect[] rss, List<MB_TexSet> textureSetss, int indexOfTexSetToRenders, ShaderTextureProperty texPropertyname, MB3_TextureCombinerNonTextureProperties resultMaterialTextureBlender, bool isNormalMap, bool fixOutOfBoundsUVs, bool considerNonTextureProperties, MB3_TextureCombiner texCombiner, MB2_LogLevel LOG_LEV)
	{
		this.LOG_LEVEL = LOG_LEV;
		this.textureSets = textureSetss;
		this.indexOfTexSetToRender = indexOfTexSetToRenders;
		this._texPropertyName = texPropertyname;
		this._padding = padding;
		this._isNormalMap = isNormalMap;
		this._fixOutOfBoundsUVs = fixOutOfBoundsUVs;
		this._resultMaterialTextureBlender = resultMaterialTextureBlender;
		this.rs = rss;
		Shader shader;
		if (this._isNormalMap)
		{
			shader = Shader.Find("MeshBaker/NormalMapShader");
		}
		else
		{
			shader = Shader.Find("MeshBaker/AlbedoShader");
		}
		if (shader == null)
		{
			Debug.LogError("Could not find shader for RenderTexture. Try reimporting mesh baker");
			return null;
		}
		this.mat = new Material(shader);
		this._destinationTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
		this._destinationTexture.filterMode = FilterMode.Point;
		this.myCamera = gameObject.GetComponent<Camera>();
		this.myCamera.orthographic = true;
		this.myCamera.orthographicSize = (float)(height >> 1);
		this.myCamera.aspect = (float)width / (float)height;
		this.myCamera.targetTexture = this._destinationTexture;
		this.myCamera.clearFlags = CameraClearFlags.Color;
		Transform component = this.myCamera.GetComponent<Transform>();
		component.localPosition = new Vector3((float)width / 2f, (float)height / 2f, 3f);
		component.localRotation = Quaternion.Euler(0f, 180f, 180f);
		this._doRenderAtlas = true;
		if (this.LOG_LEVEL >= MB2_LogLevel.debug)
		{
			Debug.Log(string.Format("Begin Camera.Render destTex w={0} h={1} camPos={2} camSize={3} camAspect={4}", new object[]
			{
				width,
				height,
				component.localPosition,
				this.myCamera.orthographicSize,
				this.myCamera.aspect.ToString("f5")
			}));
		}
		this.myCamera.Render();
		this._doRenderAtlas = false;
		MB_Utility.Destroy(this.mat);
		MB_Utility.Destroy(this._destinationTexture);
		if (this.LOG_LEVEL >= MB2_LogLevel.debug)
		{
			Debug.Log("Finished Camera.Render ");
		}
		Texture2D result = this.targTex;
		this.targTex = null;
		return result;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00043B10 File Offset: 0x00041D10
	public void OnRenderObject()
	{
		if (this._doRenderAtlas)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool flag = this.YisFlipped();
			for (int i = 0; i < this.rs.Length; i++)
			{
				MeshBakerMaterialTexture meshBakerMaterialTexture = this.textureSets[i].ts[this.indexOfTexSetToRender];
				Texture2D texture2D = meshBakerMaterialTexture.GetTexture2D();
				if (this.LOG_LEVEL >= MB2_LogLevel.trace && texture2D != null)
				{
					string[] array = new string[14];
					array[0] = "Added ";
					int num = 1;
					Texture2D texture2D2 = texture2D;
					array[num] = ((texture2D2 != null) ? texture2D2.ToString() : null);
					array[2] = " to atlas w=";
					array[3] = texture2D.width.ToString();
					array[4] = " h=";
					array[5] = texture2D.height.ToString();
					array[6] = " offset=";
					array[7] = meshBakerMaterialTexture.matTilingRect.min.ToString();
					array[8] = " scale=";
					array[9] = meshBakerMaterialTexture.matTilingRect.size.ToString();
					array[10] = " rect=";
					int num2 = 11;
					Rect rect = this.rs[i];
					array[num2] = rect.ToString();
					array[12] = " padding=";
					array[13] = this._padding.ToString();
					Debug.Log(string.Concat(array));
				}
				this.CopyScaledAndTiledToAtlas(this.textureSets[i], meshBakerMaterialTexture, this.textureSets[i].obUVoffset, this.textureSets[i].obUVscale, this.rs[i], this._texPropertyName, this._resultMaterialTextureBlender, flag);
			}
			stopwatch.Stop();
			stopwatch.Start();
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Total time for Graphics.DrawTexture calls " + stopwatch.ElapsedMilliseconds.ToString("f5"));
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Copying RenderTexture to Texture2D. destW" + this._destinationTexture.width.ToString() + " destH" + this._destinationTexture.height.ToString());
			}
			Texture2D texture2D3 = new Texture2D(this._destinationTexture.width, this._destinationTexture.height, TextureFormat.ARGB32, true);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = this._destinationTexture;
			int num3 = Mathf.CeilToInt((float)this._destinationTexture.width / 512f);
			int num4 = Mathf.CeilToInt((float)this._destinationTexture.height / 512f);
			if (num3 == 0 || num4 == 0)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("Copying all in one shot");
				}
				texture2D3.ReadPixels(new Rect(0f, 0f, (float)this._destinationTexture.width, (float)this._destinationTexture.height), 0, 0, true);
			}
			else if (!flag)
			{
				for (int j = 0; j < num3; j++)
				{
					for (int k = 0; k < num4; k++)
					{
						int num5 = j * 512;
						int num6 = k * 512;
						Rect source = new Rect((float)num5, (float)num6, 512f, 512f);
						texture2D3.ReadPixels(source, j * 512, k * 512, true);
					}
				}
			}
			else
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("Not OpenGL copying blocks");
				}
				for (int l = 0; l < num3; l++)
				{
					for (int m = 0; m < num4; m++)
					{
						int num7 = l * 512;
						int num8 = this._destinationTexture.height - 512 - m * 512;
						Rect source2 = new Rect((float)num7, (float)num8, 512f, 512f);
						texture2D3.ReadPixels(source2, l * 512, m * 512, true);
					}
				}
			}
			RenderTexture.active = active;
			texture2D3.Apply();
			if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log("TempTexture ");
				if (texture2D3.height <= 16 && texture2D3.width <= 16)
				{
					this._printTexture(texture2D3);
				}
			}
			this.myCamera.targetTexture = null;
			RenderTexture.active = null;
			this.targTex = texture2D3;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Total time to copy RenderTexture to Texture2D " + stopwatch.ElapsedMilliseconds.ToString("f5"));
			}
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00043F84 File Offset: 0x00042184
	private Color32 ConvertNormalFormatFromUnity_ToStandard(Color32 c)
	{
		Vector3 zero = Vector3.zero;
		zero.x = (float)c.a * 2f - 1f;
		zero.y = (float)c.g * 2f - 1f;
		zero.z = Mathf.Sqrt(1f - zero.x * zero.x - zero.y * zero.y);
		return new Color32
		{
			a = 1,
			r = (byte)((zero.x + 1f) * 0.5f),
			g = (byte)((zero.y + 1f) * 0.5f),
			b = (byte)((zero.z + 1f) * 0.5f)
		};
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00044058 File Offset: 0x00042258
	public bool YisFlipped()
	{
		string str = SystemInfo.graphicsDeviceVersion.ToLower();
		bool result = MBVersion.GraphicsUVStartsAtTop();
		if (this.LOG_LEVEL == MB2_LogLevel.debug)
		{
			Debug.Log("Graphics device version is: " + str + " flipY:" + result.ToString());
		}
		return result;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000440A4 File Offset: 0x000422A4
	private void CopyScaledAndTiledToAtlas(MB_TexSet texSet, MeshBakerMaterialTexture source, Vector2 obUVoffset, Vector2 obUVscale, Rect rec, ShaderTextureProperty texturePropertyName, MB3_TextureCombinerNonTextureProperties resultMatTexBlender, bool yIsFlipped)
	{
		Rect rect = rec;
		this.myCamera.backgroundColor = resultMatTexBlender.GetColorForTemporaryTexture(texSet.matsAndGOs.mats[0].mat, texturePropertyName);
		rect.y = 1f - (rect.y + rect.height);
		rect.x *= (float)this._destinationTexture.width;
		rect.y *= (float)this._destinationTexture.height;
		rect.width *= (float)this._destinationTexture.width;
		rect.height *= (float)this._destinationTexture.height;
		Rect rect2 = rect;
		rect2.x -= (float)this._padding;
		rect2.y -= (float)this._padding;
		rect2.width += (float)(this._padding * 2);
		rect2.height += (float)(this._padding * 2);
		Rect screenRect = default(Rect);
		Rect rect3 = texSet.ts[this.indexOfTexSetToRender].GetEncapsulatingSamplingRect().GetRect();
		bool fixOutOfBoundsUVs = this._fixOutOfBoundsUVs;
		Texture2D texture2D = source.GetTexture2D();
		TextureWrapMode wrapMode = texture2D.wrapMode;
		if (rect3.width == 1f && rect3.height == 1f && rect3.x == 0f && rect3.y == 0f)
		{
			texture2D.wrapMode = TextureWrapMode.Clamp;
		}
		else
		{
			texture2D.wrapMode = TextureWrapMode.Repeat;
		}
		if (this.LOG_LEVEL >= MB2_LogLevel.trace)
		{
			string[] array = new string[8];
			array[0] = "DrawTexture tex=";
			array[1] = texture2D.name;
			array[2] = " destRect=";
			int num = 3;
			Rect rect4 = rect;
			array[num] = rect4.ToString();
			array[4] = " srcRect=";
			int num2 = 5;
			rect4 = rect3;
			array[num2] = rect4.ToString();
			array[6] = " Mat=";
			int num3 = 7;
			Material material = this.mat;
			array[num3] = ((material != null) ? material.ToString() : null);
			Debug.Log(string.Concat(array));
		}
		Rect sourceRect = default(Rect);
		sourceRect.x = rect3.x;
		sourceRect.y = rect3.y + 1f - 1f / (float)texture2D.height;
		sourceRect.width = rect3.width;
		sourceRect.height = 1f / (float)texture2D.height;
		screenRect.x = rect.x;
		screenRect.y = rect2.y;
		screenRect.width = rect.width;
		screenRect.height = (float)this._padding;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = this._destinationTexture;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x;
		sourceRect.y = rect3.y;
		sourceRect.width = rect3.width;
		sourceRect.height = 1f / (float)texture2D.height;
		screenRect.x = rect.x;
		screenRect.y = rect.y + rect.height;
		screenRect.width = rect.width;
		screenRect.height = (float)this._padding;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x;
		sourceRect.y = rect3.y;
		sourceRect.width = 1f / (float)texture2D.width;
		sourceRect.height = rect3.height;
		screenRect.x = rect2.x;
		screenRect.y = rect.y;
		screenRect.width = (float)this._padding;
		screenRect.height = rect.height;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x + 1f - 1f / (float)texture2D.width;
		sourceRect.y = rect3.y;
		sourceRect.width = 1f / (float)texture2D.width;
		sourceRect.height = rect3.height;
		screenRect.x = rect.x + rect.width;
		screenRect.y = rect.y;
		screenRect.width = (float)this._padding;
		screenRect.height = rect.height;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x;
		sourceRect.y = rect3.y + 1f - 1f / (float)texture2D.height;
		sourceRect.width = 1f / (float)texture2D.width;
		sourceRect.height = 1f / (float)texture2D.height;
		screenRect.x = rect2.x;
		screenRect.y = rect2.y;
		screenRect.width = (float)this._padding;
		screenRect.height = (float)this._padding;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x + 1f - 1f / (float)texture2D.width;
		sourceRect.y = rect3.y + 1f - 1f / (float)texture2D.height;
		sourceRect.width = 1f / (float)texture2D.width;
		sourceRect.height = 1f / (float)texture2D.height;
		screenRect.x = rect.x + rect.width;
		screenRect.y = rect2.y;
		screenRect.width = (float)this._padding;
		screenRect.height = (float)this._padding;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x;
		sourceRect.y = rect3.y;
		sourceRect.width = 1f / (float)texture2D.width;
		sourceRect.height = 1f / (float)texture2D.height;
		screenRect.x = rect2.x;
		screenRect.y = rect.y + rect.height;
		screenRect.width = (float)this._padding;
		screenRect.height = (float)this._padding;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		sourceRect.x = rect3.x + 1f - 1f / (float)texture2D.width;
		sourceRect.y = rect3.y;
		sourceRect.width = 1f / (float)texture2D.width;
		sourceRect.height = 1f / (float)texture2D.height;
		screenRect.x = rect.x + rect.width;
		screenRect.y = rect.y + rect.height;
		screenRect.width = (float)this._padding;
		screenRect.height = (float)this._padding;
		Graphics.DrawTexture(screenRect, texture2D, sourceRect, 0, 0, 0, 0, this.mat);
		Graphics.DrawTexture(rect, texture2D, rect3, 0, 0, 0, 0, this.mat);
		RenderTexture.active = active;
		texture2D.wrapMode = wrapMode;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0004481C File Offset: 0x00042A1C
	private void _printTexture(Texture2D t)
	{
		if (t.width * t.height > 100)
		{
			Debug.Log("Not printing texture too large.");
			return;
		}
		try
		{
			Color32[] pixels = t.GetPixels32();
			string text = "";
			for (int i = 0; i < t.height; i++)
			{
				for (int j = 0; j < t.width; j++)
				{
					string str = text;
					Color32 color = pixels[i * t.width + j];
					text = str + color.ToString() + ", ";
				}
				text += "\n";
			}
			Debug.Log(text);
		}
		catch (Exception ex)
		{
			Debug.Log("Could not print texture. texture may not be readable." + ex.ToString());
		}
	}

	// Token: 0x04000112 RID: 274
	public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

	// Token: 0x04000113 RID: 275
	private Material mat;

	// Token: 0x04000114 RID: 276
	private RenderTexture _destinationTexture;

	// Token: 0x04000115 RID: 277
	private Camera myCamera;

	// Token: 0x04000116 RID: 278
	private int _padding;

	// Token: 0x04000117 RID: 279
	private bool _isNormalMap;

	// Token: 0x04000118 RID: 280
	private bool _fixOutOfBoundsUVs;

	// Token: 0x04000119 RID: 281
	private bool _doRenderAtlas;

	// Token: 0x0400011A RID: 282
	private Rect[] rs;

	// Token: 0x0400011B RID: 283
	private List<MB_TexSet> textureSets;

	// Token: 0x0400011C RID: 284
	private int indexOfTexSetToRender;

	// Token: 0x0400011D RID: 285
	private ShaderTextureProperty _texPropertyName;

	// Token: 0x0400011E RID: 286
	private MB3_TextureCombinerNonTextureProperties _resultMaterialTextureBlender;

	// Token: 0x0400011F RID: 287
	private Texture2D targTex;
}
