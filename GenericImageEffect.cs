using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000144 RID: 324
[ExecuteInEditMode]
public class GenericImageEffect : ImageEffectBase
{
	// Token: 0x06000903 RID: 2307 RVA: 0x00007EB1 File Offset: 0x000060B1
	private void OnEnable()
	{
		this.ApplyParameters();
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00069C30 File Offset: 0x00067E30
	private void ApplyParameters()
	{
		if (this.textures != null)
		{
			foreach (GenericImageEffect.TextureMap textureMap in this.textures)
			{
				base.material.SetTexture(textureMap.name, textureMap.value);
			}
		}
		if (this.floats != null)
		{
			foreach (GenericImageEffect.FloatMap floatMap in this.floats)
			{
				base.material.SetFloat(floatMap.name, floatMap.value);
			}
		}
		if (this.integers != null)
		{
			foreach (GenericImageEffect.IntMap intMap in this.integers)
			{
				base.material.SetInt(intMap.name, intMap.value);
			}
		}
		if (this.colors != null)
		{
			foreach (GenericImageEffect.ColorMap colorMap in this.colors)
			{
				base.material.SetColor(colorMap.name, colorMap.value);
			}
		}
		if (this.vectors != null)
		{
			foreach (GenericImageEffect.VectorMap vectorMap in this.vectors)
			{
				base.material.SetVector(vectorMap.name, vectorMap.value);
			}
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00007EB9 File Offset: 0x000060B9
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.updateParametersEveryFrame)
		{
			this.ApplyParameters();
		}
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040009C0 RID: 2496
	public GenericImageEffect.TextureMap[] textures;

	// Token: 0x040009C1 RID: 2497
	public GenericImageEffect.FloatMap[] floats;

	// Token: 0x040009C2 RID: 2498
	public GenericImageEffect.IntMap[] integers;

	// Token: 0x040009C3 RID: 2499
	public GenericImageEffect.ColorMap[] colors;

	// Token: 0x040009C4 RID: 2500
	public GenericImageEffect.VectorMap[] vectors;

	// Token: 0x040009C5 RID: 2501
	public bool updateParametersEveryFrame;

	// Token: 0x02000145 RID: 325
	[Serializable]
	public struct FloatMap
	{
		// Token: 0x040009C6 RID: 2502
		public string name;

		// Token: 0x040009C7 RID: 2503
		public float value;
	}

	// Token: 0x02000146 RID: 326
	[Serializable]
	public struct IntMap
	{
		// Token: 0x040009C8 RID: 2504
		public string name;

		// Token: 0x040009C9 RID: 2505
		public int value;
	}

	// Token: 0x02000147 RID: 327
	[Serializable]
	public struct TextureMap
	{
		// Token: 0x040009CA RID: 2506
		public string name;

		// Token: 0x040009CB RID: 2507
		public Texture value;
	}

	// Token: 0x02000148 RID: 328
	[Serializable]
	public struct ColorMap
	{
		// Token: 0x040009CC RID: 2508
		public string name;

		// Token: 0x040009CD RID: 2509
		public Color value;
	}

	// Token: 0x02000149 RID: 329
	[Serializable]
	public struct VectorMap
	{
		// Token: 0x040009CE RID: 2510
		public string name;

		// Token: 0x040009CF RID: 2511
		public Vector4 value;
	}
}
