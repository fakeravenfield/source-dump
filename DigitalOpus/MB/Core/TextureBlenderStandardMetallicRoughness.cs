using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000431 RID: 1073
	public class TextureBlenderStandardMetallicRoughness : TextureBlender
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x000146A9 File Offset: 0x000128A9
		public bool DoesShaderNameMatch(string shaderName)
		{
			return shaderName.Equals("Standard (Roughness setup)");
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000AF178 File Offset: 0x000AD378
		public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
		{
			if (shaderTexturePropertyName.Equals("_MainTex"))
			{
				this.propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doColor;
				if (sourceMat.HasProperty("_Color"))
				{
					this.m_tintColor = sourceMat.GetColor("_Color");
					return;
				}
				this.m_tintColor = this.m_generatingTintedAtlasColor;
				return;
			}
			else if (shaderTexturePropertyName.Equals("_MetallicGlossMap"))
			{
				this.propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doMetallic;
				this.m_metallic = this.m_generatingTintedAtlasMetallic;
				if (sourceMat.GetTexture("_MetallicGlossMap") != null)
				{
					this.m_hasMetallicGlossMap = true;
				}
				else
				{
					this.m_hasMetallicGlossMap = false;
				}
				if (sourceMat.HasProperty("_Metallic"))
				{
					this.m_metallic = sourceMat.GetFloat("_Metallic");
					return;
				}
				this.m_metallic = 0f;
				return;
			}
			else
			{
				if (!shaderTexturePropertyName.Equals("_SpecGlossMap"))
				{
					if (shaderTexturePropertyName.Equals("_BumpMap"))
					{
						this.propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doBump;
						if (!sourceMat.HasProperty(shaderTexturePropertyName))
						{
							this.m_bumpScale = this.m_generatingTintedAtlasBumpScale;
							return;
						}
						if (sourceMat.HasProperty("_BumpScale"))
						{
							this.m_bumpScale = sourceMat.GetFloat("_BumpScale");
							return;
						}
					}
					else if (shaderTexturePropertyName.Equals("_EmissionMap"))
					{
						this.propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doEmission;
						this.m_shaderDoesEmission = sourceMat.IsKeywordEnabled("_EMISSION");
						if (sourceMat.HasProperty("_EmissionColor"))
						{
							this.m_emissionColor = sourceMat.GetColor("_EmissionColor");
							return;
						}
						this.m_emissionColor = this.m_notGeneratingAtlasDefaultEmisionColor;
						return;
					}
					else
					{
						this.propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doNone;
					}
					return;
				}
				this.propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doRoughness;
				this.m_roughness = this.m_generatingTintedAtlasRoughness;
				if (sourceMat.GetTexture("_SpecGlossMap") != null)
				{
					this.m_hasSpecGlossMap = true;
				}
				else
				{
					this.m_hasSpecGlossMap = false;
				}
				if (sourceMat.HasProperty("_Glossiness"))
				{
					this.m_roughness = sourceMat.GetFloat("_Glossiness");
					return;
				}
				this.m_roughness = 1f;
				return;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000AF344 File Offset: 0x000AD544
		public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor)
		{
			if (this.propertyToDo == TextureBlenderStandardMetallicRoughness.Prop.doColor)
			{
				return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
			}
			if (this.propertyToDo == TextureBlenderStandardMetallicRoughness.Prop.doMetallic)
			{
				if (this.m_hasMetallicGlossMap)
				{
					return pixelColor;
				}
				return new Color(this.m_metallic, 0f, 0f, this.m_roughness);
			}
			else if (this.propertyToDo == TextureBlenderStandardMetallicRoughness.Prop.doRoughness)
			{
				if (this.m_hasSpecGlossMap)
				{
					return pixelColor;
				}
				return new Color(this.m_roughness, 0f, 0f, 0f);
			}
			else
			{
				if (this.propertyToDo == TextureBlenderStandardMetallicRoughness.Prop.doBump)
				{
					return Color.Lerp(TextureBlenderStandardMetallicRoughness.NeutralNormalMap, pixelColor, this.m_bumpScale);
				}
				if (this.propertyToDo != TextureBlenderStandardMetallicRoughness.Prop.doEmission)
				{
					return pixelColor;
				}
				if (this.m_shaderDoesEmission)
				{
					return new Color(pixelColor.r * this.m_emissionColor.r, pixelColor.g * this.m_emissionColor.g, pixelColor.b * this.m_emissionColor.b, pixelColor.a * this.m_emissionColor.a);
				}
				return Color.black;
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000AF488 File Offset: 0x000AD688
		public bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			if (!TextureBlenderFallback._compareColor(a, b, this.m_notGeneratingAtlasDefaultColor, "_Color"))
			{
				return false;
			}
			bool flag = a.HasProperty("_MetallicGlossMap") && a.GetTexture("_MetallicGlossMap") != null;
			bool flag2 = b.HasProperty("_MetallicGlossMap") && b.GetTexture("_MetallicGlossMap") != null;
			if (flag || flag2)
			{
				return false;
			}
			if (!TextureBlenderFallback._compareFloat(a, b, this.m_notGeneratingAtlasDefaultMetallic, "_Metallic"))
			{
				return false;
			}
			bool flag3 = a.HasProperty("_SpecGlossMap") && a.GetTexture("_SpecGlossMap") != null;
			bool flag4 = b.HasProperty("_SpecGlossMap") && b.GetTexture("_SpecGlossMap") != null;
			return !flag3 && !flag4 && TextureBlenderFallback._compareFloat(a, b, this.m_generatingTintedAtlasRoughness, "_Glossiness") && TextureBlenderFallback._compareFloat(a, b, this.m_generatingTintedAtlasBumpScale, "_bumpScale") && TextureBlenderFallback._compareFloat(a, b, this.m_generatingTintedAtlasRoughness, "_Glossiness") && a.IsKeywordEnabled("_EMISSION") == b.IsKeywordEnabled("_EMISSION") && (!a.IsKeywordEnabled("_EMISSION") || TextureBlenderFallback._compareColor(a, b, this.m_generatingTintedAtlasEmission, "_EmissionColor"));
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000AF5D8 File Offset: 0x000AD7D8
		public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
		{
			if (resultMaterial.GetTexture("_MainTex") != null)
			{
				resultMaterial.SetColor("_Color", this.m_generatingTintedAtlasColor);
			}
			else
			{
				resultMaterial.SetColor("_Color", (Color)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Color", this.m_notGeneratingAtlasDefaultColor));
			}
			if (resultMaterial.GetTexture("_MetallicGlossMap") != null)
			{
				resultMaterial.SetFloat("_Metallic", this.m_generatingTintedAtlasMetallic);
			}
			else
			{
				resultMaterial.SetFloat("_Metallic", (float)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Metallic", this.m_notGeneratingAtlasDefaultMetallic));
			}
			if (!(resultMaterial.GetTexture("_SpecGlossMap") != null))
			{
				resultMaterial.SetFloat("_Glossiness", (float)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Glossiness", this.m_notGeneratingAtlasDefaultGlossiness));
			}
			if (resultMaterial.GetTexture("_BumpMap") != null)
			{
				resultMaterial.SetFloat("_BumpScale", this.m_generatingTintedAtlasBumpScale);
			}
			else
			{
				resultMaterial.SetFloat("_BumpScale", this.m_generatingTintedAtlasBumpScale);
			}
			if (resultMaterial.GetTexture("_EmissionMap") != null)
			{
				resultMaterial.EnableKeyword("_EMISSION");
				resultMaterial.SetColor("_EmissionColor", Color.white);
				return;
			}
			resultMaterial.DisableKeyword("_EMISSION");
			resultMaterial.SetColor("_EmissionColor", (Color)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_EmissionColor", this.m_notGeneratingAtlasDefaultEmisionColor));
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000AF764 File Offset: 0x000AD964
		public Color GetColorIfNoTexture(Material mat, ShaderTextureProperty texPropertyName)
		{
			if (texPropertyName.name.Equals("_BumpMap"))
			{
				return new Color(0.5f, 0.5f, 1f);
			}
			if (texPropertyName.name.Equals("_MainTex"))
			{
				if (mat != null && mat.HasProperty("_Color"))
				{
					try
					{
						Color color = mat.GetColor("_Color");
						this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_Color", color);
					}
					catch (Exception)
					{
					}
					return Color.white;
				}
			}
			else if (texPropertyName.name.Equals("_MetallicGlossMap"))
			{
				if (mat != null && mat.HasProperty("_Metallic"))
				{
					try
					{
						float @float = mat.GetFloat("_Metallic");
						this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_Metallic", @float);
					}
					catch (Exception)
					{
					}
					return new Color(0f, 0f, 0f, 0.5f);
				}
				return new Color(0f, 0f, 0f, 0.5f);
			}
			else
			{
				if (texPropertyName.name.Equals("_SpecGlossMap"))
				{
					bool flag = false;
					try
					{
						Color color2 = new Color(0f, 0f, 0f, 0.5f);
						if (mat.HasProperty("_Glossiness"))
						{
							try
							{
								flag = true;
								color2.a = mat.GetFloat("_Glossiness");
							}
							catch (Exception)
							{
							}
						}
						this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_Glossiness", color2.a);
						return new Color(0f, 0f, 0f, 0.5f);
					}
					catch (Exception)
					{
					}
					if (!flag)
					{
						return new Color(0f, 0f, 0f, 0.5f);
					}
					goto IL_2AD;
				}
				if (texPropertyName.name.Equals("_ParallaxMap"))
				{
					return new Color(0f, 0f, 0f, 0f);
				}
				if (texPropertyName.name.Equals("_OcclusionMap"))
				{
					return new Color(1f, 1f, 1f, 1f);
				}
				if (texPropertyName.name.Equals("_EmissionMap"))
				{
					if (mat != null)
					{
						if (mat.IsKeywordEnabled("_EMISSION"))
						{
							if (mat.HasProperty("_EmissionColor"))
							{
								try
								{
									Color color3 = mat.GetColor("_EmissionColor");
									this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_EmissionColor", color3);
									goto IL_2AD;
								}
								catch (Exception)
								{
									goto IL_2AD;
								}
							}
							return Color.black;
						}
						return Color.black;
					}
				}
				else if (texPropertyName.name.Equals("_DetailMask"))
				{
					return new Color(0f, 0f, 0f, 0f);
				}
			}
			IL_2AD:
			return new Color(1f, 1f, 1f, 0f);
		}

		// Token: 0x04001C33 RID: 7219
		private static Color NeutralNormalMap = new Color(0.5f, 0.5f, 1f);

		// Token: 0x04001C34 RID: 7220
		private TextureBlenderMaterialPropertyCacheHelper sourceMaterialPropertyCache = new TextureBlenderMaterialPropertyCacheHelper();

		// Token: 0x04001C35 RID: 7221
		private Color m_tintColor;

		// Token: 0x04001C36 RID: 7222
		private float m_roughness;

		// Token: 0x04001C37 RID: 7223
		private float m_metallic;

		// Token: 0x04001C38 RID: 7224
		private bool m_hasMetallicGlossMap;

		// Token: 0x04001C39 RID: 7225
		private bool m_hasSpecGlossMap;

		// Token: 0x04001C3A RID: 7226
		private float m_bumpScale;

		// Token: 0x04001C3B RID: 7227
		private bool m_shaderDoesEmission;

		// Token: 0x04001C3C RID: 7228
		private Color m_emissionColor;

		// Token: 0x04001C3D RID: 7229
		private TextureBlenderStandardMetallicRoughness.Prop propertyToDo = TextureBlenderStandardMetallicRoughness.Prop.doNone;

		// Token: 0x04001C3E RID: 7230
		private Color m_generatingTintedAtlasColor = Color.white;

		// Token: 0x04001C3F RID: 7231
		private float m_generatingTintedAtlasMetallic;

		// Token: 0x04001C40 RID: 7232
		private float m_generatingTintedAtlasRoughness = 0.5f;

		// Token: 0x04001C41 RID: 7233
		private float m_generatingTintedAtlasBumpScale = 1f;

		// Token: 0x04001C42 RID: 7234
		private Color m_generatingTintedAtlasEmission = Color.white;

		// Token: 0x04001C43 RID: 7235
		private Color m_notGeneratingAtlasDefaultColor = Color.white;

		// Token: 0x04001C44 RID: 7236
		private float m_notGeneratingAtlasDefaultMetallic;

		// Token: 0x04001C45 RID: 7237
		private float m_notGeneratingAtlasDefaultGlossiness = 0.5f;

		// Token: 0x04001C46 RID: 7238
		private Color m_notGeneratingAtlasDefaultEmisionColor = Color.black;

		// Token: 0x02000432 RID: 1074
		private enum Prop
		{
			// Token: 0x04001C48 RID: 7240
			doColor,
			// Token: 0x04001C49 RID: 7241
			doMetallic,
			// Token: 0x04001C4A RID: 7242
			doRoughness,
			// Token: 0x04001C4B RID: 7243
			doEmission,
			// Token: 0x04001C4C RID: 7244
			doBump,
			// Token: 0x04001C4D RID: 7245
			doNone
		}
	}
}
