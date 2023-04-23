using System;
using System.Collections.Generic;
using System.IO;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class MB3_TestRenderTextureTestHarness : MonoBehaviour
{
	// Token: 0x06000192 RID: 402 RVA: 0x00043094 File Offset: 0x00041294
	public Texture2D Create3x3Tex()
	{
		Texture2D texture2D = new Texture2D(3, 3, TextureFormat.ARGB32, false);
		Color32[] array = new Color32[texture2D.width * texture2D.height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.color;
		}
		texture2D.SetPixels32(array);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000430E8 File Offset: 0x000412E8
	public Texture2D Create3x3Clone()
	{
		Texture2D texture2D = new Texture2D(3, 3, TextureFormat.ARGB32, false);
		Color32[] pixels = new Color32[]
		{
			new Color32(54, 54, 201, byte.MaxValue),
			new Color32(128, 37, 218, byte.MaxValue),
			new Color32(201, 54, 201, byte.MaxValue),
			new Color32(37, 128, 218, byte.MaxValue),
			new Color32(128, 128, byte.MaxValue, byte.MaxValue),
			new Color32(218, 128, 218, byte.MaxValue),
			new Color32(54, 201, 201, byte.MaxValue),
			new Color32(128, 218, 218, byte.MaxValue),
			new Color32(201, 201, 201, byte.MaxValue)
		};
		texture2D.SetPixels32(pixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00043224 File Offset: 0x00041424
	public static void TestRender(Texture2D input, Texture2D output)
	{
		int num = 1;
		ShaderTextureProperty[] array = new ShaderTextureProperty[]
		{
			new ShaderTextureProperty("_BumpMap", false)
		};
		int width = input.width;
		int height = input.height;
		int padding = 0;
		Rect[] rects = new Rect[]
		{
			new Rect(0f, 0f, 1f, 1f)
		};
		List<MB_TexSet> list = new List<MB_TexSet>();
		MB_TexSet item = new MB_TexSet(new MeshBakerMaterialTexture[]
		{
			new MeshBakerMaterialTexture(input)
		}, Vector2.zero, Vector2.one, MB_TextureTilingTreatment.considerUVs);
		list.Add(item);
		GameObject gameObject = new GameObject("MBrenderAtlasesGO");
		MB3_AtlasPackerRenderTexture mb3_AtlasPackerRenderTexture = gameObject.AddComponent<MB3_AtlasPackerRenderTexture>();
		gameObject.AddComponent<Camera>();
		for (int i = 0; i < num; i++)
		{
			Debug.Log("About to render " + array[i].name + " isNormal=" + array[i].isNormalMap.ToString());
			mb3_AtlasPackerRenderTexture.LOG_LEVEL = MB2_LogLevel.trace;
			mb3_AtlasPackerRenderTexture.width = width;
			mb3_AtlasPackerRenderTexture.height = height;
			mb3_AtlasPackerRenderTexture.padding = padding;
			mb3_AtlasPackerRenderTexture.rects = rects;
			mb3_AtlasPackerRenderTexture.textureSets = list;
			mb3_AtlasPackerRenderTexture.indexOfTexSetToRender = i;
			mb3_AtlasPackerRenderTexture.isNormalMap = array[i].isNormalMap;
			Texture2D texture2D = mb3_AtlasPackerRenderTexture.OnRenderAtlas(null);
			Debug.Log(string.Concat(new string[]
			{
				"Created atlas ",
				array[i].name,
				" w=",
				texture2D.width.ToString(),
				" h=",
				texture2D.height.ToString(),
				" id=",
				texture2D.GetInstanceID().ToString()
			}));
			Debug.Log("Color " + texture2D.GetPixel(5, 5).ToString() + " " + Color.red.ToString());
			byte[] bytes = texture2D.EncodeToPNG();
			File.WriteAllBytes(Application.dataPath + "/_Experiment/red.png", bytes);
		}
	}

	// Token: 0x04000109 RID: 265
	public Texture2D input;

	// Token: 0x0400010A RID: 266
	public bool doColor;

	// Token: 0x0400010B RID: 267
	public Color32 color;
}
