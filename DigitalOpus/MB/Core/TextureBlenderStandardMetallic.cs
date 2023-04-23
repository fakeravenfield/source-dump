using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200042F RID: 1071
	public class TextureBlenderStandardMetallic : TextureBlender
	{
		// Token: 0x06001A9A RID: 6810 RVA: 0x00014681 File Offset: 0x00012881
		public bool DoesShaderNameMatch(string shaderName)
		{
			return shaderName.Equals("Standard");
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000AE8E4 File Offset: 0x000ACAE4
		public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
		{
			if (shaderTexturePropertyName.Equals("_MainTex"))
			{
				this.propertyToDo = TextureBlenderStandardMetallic.Prop.doColor;
				if (sourceMat.HasProperty("_Color"))
				{
					this.m_tintColor = sourceMat.GetColor("_Color");
					return;
				}
				this.m_tintColor = this.m_generatingTintedAtlasColor;
				return;
			}
			else
			{
				if (!shaderTexturePropertyName.Equals("_MetallicGlossMap"))
				{
					if (shaderTexturePropertyName.Equals("_BumpMap"))
					{
						this.propertyToDo = TextureBlenderStandardMetallic.Prop.doBump;
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
						this.propertyToDo = TextureBlenderStandardMetallic.Prop.doEmission;
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
						this.propertyToDo = TextureBlenderStandardMetallic.Prop.doNone;
					}
					return;
				}
				this.propertyToDo = TextureBlenderStandardMetallic.Prop.doMetallic;
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
				}
				else
				{
					this.m_metallic = 0f;
				}
				if (sourceMat.HasProperty("_GlossMapScale"))
				{
					this.m_glossMapScale = sourceMat.GetFloat("_GlossMapScale");
				}
				else
				{
					this.m_glossMapScale = 1f;
				}
				if (sourceMat.HasProperty("_Glossiness"))
				{
					this.m_glossiness = sourceMat.GetFloat("_Glossiness");
					return;
				}
				this.m_glossiness = 0f;
				return;
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000AEA9C File Offset: 0x000ACC9C
		public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor)
		{
			if (this.propertyToDo == TextureBlenderStandardMetallic.Prop.doColor)
			{
				return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
			}
			if (this.propertyToDo == TextureBlenderStandardMetallic.Prop.doMetallic)
			{
				if (this.m_hasMetallicGlossMap)
				{
					pixelColor = new Color(pixelColor.r, pixelColor.g, pixelColor.b, pixelColor.a * this.m_glossMapScale);
					return pixelColor;
				}
				return new Color(this.m_metallic, 0f, 0f, this.m_glossiness);
			}
			else
			{
				if (this.propertyToDo == TextureBlenderStandardMetallic.Prop.doBump)
				{
					return Color.Lerp(TextureBlenderStandardMetallic.NeutralNormalMap, pixelColor, this.m_bumpScale);
				}
				if (this.propertyToDo != TextureBlenderStandardMetallic.Prop.doEmission)
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

		// Token: 0x06001A9D RID: 6813 RVA: 0x000AEBD8 File Offset: 0x000ACDD8
		public bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			if (!TextureBlenderFallback._compareColor(a, b, this.m_notGeneratingAtlasDefaultColor, "_Color"))
			{
				return false;
			}
			if (!TextureBlenderFallback._compareFloat(a, b, this.m_notGeneratingAtlasDefaultGlossiness, "_Glossiness"))
			{
				return false;
			}
			bool flag = a.HasProperty("_MetallicGlossMap") && a.GetTexture("_MetallicGlossMap") != null;
			bool flag2 = b.HasProperty("_MetallicGlossMap") && b.GetTexture("_MetallicGlossMap") != null;
			if (flag && flag2)
			{
				if (!TextureBlenderFallback._compareFloat(a, b, this.m_notGeneratingAtlasDefaultMetallic, "_GlossMapScale"))
				{
					return false;
				}
			}
			else
			{
				if (flag || flag2)
				{
					return false;
				}
				if (!TextureBlenderFallback._compareFloat(a, b, this.m_notGeneratingAtlasDefaultMetallic, "_Metallic"))
				{
					return false;
				}
			}
			return a.IsKeywordEnabled("_EMISSION") == b.IsKeywordEnabled("_EMISSION") && (!a.IsKeywordEnabled("_EMISSION") || TextureBlenderFallback._compareColor(a, b, this.m_notGeneratingAtlasDefaultEmisionColor, "_EmissionColor"));
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000AECCC File Offset: 0x000ACECC
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
				resultMaterial.SetFloat("_GlossMapScale", this.m_generatingTintedAtlasGlossMapScale);
				resultMaterial.SetFloat("_Glossiness", this.m_generatingTintedAtlasGlossiness);
			}
			else
			{
				resultMaterial.SetFloat("_Metallic", (float)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Metallic", this.m_notGeneratingAtlasDefaultMetallic));
				resultMaterial.SetFloat("_Glossiness", (float)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Glossiness", this.m_notGeneratingAtlasDefaultGlossiness));
			}
			if (resultMaterial.GetTexture("_BumpMap") != null)
			{
				resultMaterial.SetFloat("_BumpScale", this.m_generatingTintedAtlasBumpScale);
			}
			if (resultMaterial.GetTexture("_EmissionMap") != null)
			{
				resultMaterial.EnableKeyword("_EMISSION");
				resultMaterial.SetColor("_EmissionColor", this.m_generatingTintedAtlasEmission);
				return;
			}
			resultMaterial.DisableKeyword("_EMISSION");
			resultMaterial.SetColor("_EmissionColor", (Color)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_EmissionColor", this.m_notGeneratingAtlasDefaultEmisionColor));
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000AEE54 File Offset: 0x000AD054
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
						Color color2 = new Color(@float, @float, @float);
						if (mat.HasProperty("_Glossiness"))
						{
							try
							{
								color2.a = mat.GetFloat("_Glossiness");
							}
							catch (Exception)
							{
							}
						}
						this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_Metallic", @float);
						this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_Glossiness", color2.a);
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
									goto IL_247;
								}
								catch (Exception)
								{
									goto IL_247;
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
			IL_247:
			return new Color(1f, 1f, 1f, 0f);
		}

		// Token: 0x04001C18 RID: 7192
		private static Color NeutralNormalMap = new Color(0.5f, 0.5f, 1f);

		// Token: 0x04001C19 RID: 7193
		private TextureBlenderMaterialPropertyCacheHelper sourceMaterialPropertyCache = new TextureBlenderMaterialPropertyCacheHelper();

		// Token: 0x04001C1A RID: 7194
		private Color m_tintColor;

		// Token: 0x04001C1B RID: 7195
		private float m_glossiness;

		// Token: 0x04001C1C RID: 7196
		private float m_glossMapScale;

		// Token: 0x04001C1D RID: 7197
		private float m_metallic;

		// Token: 0x04001C1E RID: 7198
		private bool m_hasMetallicGlossMap;

		// Token: 0x04001C1F RID: 7199
		private float m_bumpScale;

		// Token: 0x04001C20 RID: 7200
		private bool m_shaderDoesEmission;

		// Token: 0x04001C21 RID: 7201
		private Color m_emissionColor;

		// Token: 0x04001C22 RID: 7202
		private TextureBlenderStandardMetallic.Prop propertyToDo = TextureBlenderStandardMetallic.Prop.doNone;

		// Token: 0x04001C23 RID: 7203
		private Color m_generatingTintedAtlasColor = Color.white;

		// Token: 0x04001C24 RID: 7204
		private float m_generatingTintedAtlasMetallic;

		// Token: 0x04001C25 RID: 7205
		private float m_generatingTintedAtlasGlossiness = 1f;

		// Token: 0x04001C26 RID: 7206
		private float m_generatingTintedAtlasGlossMapScale = 1f;

		// Token: 0x04001C27 RID: 7207
		private float m_generatingTintedAtlasBumpScale = 1f;

		// Token: 0x04001C28 RID: 7208
		private Color m_generatingTintedAtlasEmission = Color.white;

		// Token: 0x04001C29 RID: 7209
		private Color m_notGeneratingAtlasDefaultColor = Color.white;

		// Token: 0x04001C2A RID: 7210
		private float m_notGeneratingAtlasDefaultMetallic;

		// Token: 0x04001C2B RID: 7211
		private float m_notGeneratingAtlasDefaultGlossiness = 0.5f;

		// Token: 0x04001C2C RID: 7212
		private Color m_notGeneratingAtlasDefaultEmisionColor = Color.black;

		// Token: 0x02000430 RID: 1072
		private enum Prop
		{
			// Token: 0x04001C2E RID: 7214
			doColor,
			// Token: 0x04001C2F RID: 7215
			doMetallic,
			// Token: 0x04001C30 RID: 7216
			doEmission,
			// Token: 0x04001C31 RID: 7217
			doBump,
			// Token: 0x04001C32 RID: 7218
			doNone
		}
	}
}
