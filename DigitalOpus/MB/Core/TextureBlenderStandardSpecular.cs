using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000433 RID: 1075
	public class TextureBlenderStandardSpecular : TextureBlender
	{
		// Token: 0x06001AAA RID: 6826 RVA: 0x000146D1 File Offset: 0x000128D1
		public bool DoesShaderNameMatch(string shaderName)
		{
			return shaderName.Equals("Standard (Specular setup)");
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000AFAF0 File Offset: 0x000ADCF0
		public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
		{
			if (shaderTexturePropertyName.Equals("_MainTex"))
			{
				this.propertyToDo = TextureBlenderStandardSpecular.Prop.doColor;
				if (sourceMat.HasProperty("_Color"))
				{
					this.m_tintColor = sourceMat.GetColor("_Color");
					return;
				}
				this.m_tintColor = this.m_generatingTintedAtlaColor;
				return;
			}
			else
			{
				if (!shaderTexturePropertyName.Equals("_SpecGlossMap"))
				{
					if (shaderTexturePropertyName.Equals("_BumpMap"))
					{
						this.propertyToDo = TextureBlenderStandardSpecular.Prop.doBump;
						if (!sourceMat.HasProperty(shaderTexturePropertyName))
						{
							this.m_bumpScale = this.m_generatingTintedAtlaBumpScale;
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
						this.propertyToDo = TextureBlenderStandardSpecular.Prop.doEmission;
						this.m_shaderDoesEmission = sourceMat.IsKeywordEnabled("_EMISSION");
						if (sourceMat.HasProperty("_EmissionColor"))
						{
							this.m_emissionColor = sourceMat.GetColor("_EmissionColor");
							return;
						}
						this.m_generatingTintedAtlaColor = this.m_notGeneratingAtlasDefaultEmisionColor;
						return;
					}
					else
					{
						this.propertyToDo = TextureBlenderStandardSpecular.Prop.doNone;
					}
					return;
				}
				this.propertyToDo = TextureBlenderStandardSpecular.Prop.doSpecular;
				this.m_specColor = this.m_generatingTintedAtlaSpecular;
				if (sourceMat.GetTexture("_SpecGlossMap") != null)
				{
					this.m_hasSpecGlossMap = true;
				}
				else
				{
					this.m_hasSpecGlossMap = false;
				}
				if (sourceMat.HasProperty("_SpecColor"))
				{
					this.m_specColor = sourceMat.GetColor("_SpecColor");
				}
				else
				{
					this.m_specColor = new Color(0f, 0f, 0f, 1f);
				}
				if (sourceMat.HasProperty("_GlossMapScale"))
				{
					this.m_SpecGlossMapScale = sourceMat.GetFloat("_GlossMapScale");
				}
				else
				{
					this.m_SpecGlossMapScale = 1f;
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

		// Token: 0x06001AAC RID: 6828 RVA: 0x000AFCBC File Offset: 0x000ADEBC
		public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor)
		{
			if (this.propertyToDo == TextureBlenderStandardSpecular.Prop.doColor)
			{
				return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
			}
			if (this.propertyToDo == TextureBlenderStandardSpecular.Prop.doSpecular)
			{
				if (this.m_hasSpecGlossMap)
				{
					pixelColor = new Color(pixelColor.r, pixelColor.g, pixelColor.b, pixelColor.a * this.m_SpecGlossMapScale);
					return pixelColor;
				}
				Color specColor = this.m_specColor;
				specColor.a = this.m_glossiness;
				return specColor;
			}
			else
			{
				if (this.propertyToDo == TextureBlenderStandardSpecular.Prop.doBump)
				{
					return Color.Lerp(TextureBlenderStandardSpecular.NeutralNormalMap, pixelColor, this.m_bumpScale);
				}
				if (this.propertyToDo != TextureBlenderStandardSpecular.Prop.doEmission)
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

		// Token: 0x06001AAD RID: 6829 RVA: 0x000AFDF0 File Offset: 0x000ADFF0
		public bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			if (!TextureBlenderFallback._compareColor(a, b, this.m_generatingTintedAtlaColor, "_Color"))
			{
				return false;
			}
			if (!TextureBlenderFallback._compareColor(a, b, this.m_generatingTintedAtlaSpecular, "_SpecColor"))
			{
				return false;
			}
			bool flag = a.HasProperty("_SpecGlossMap") && a.GetTexture("_SpecGlossMap") != null;
			bool flag2 = b.HasProperty("_SpecGlossMap") && b.GetTexture("_SpecGlossMap") != null;
			if (flag && flag2)
			{
				if (!TextureBlenderFallback._compareFloat(a, b, this.m_generatingTintedAtlaSpecGlossMapScale, "_GlossMapScale"))
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
				if (!TextureBlenderFallback._compareFloat(a, b, this.m_generatingTintedAtlaGlossiness, "_Glossiness"))
				{
					return false;
				}
			}
			return TextureBlenderFallback._compareFloat(a, b, this.m_generatingTintedAtlaBumpScale, "_BumpScale") && a.IsKeywordEnabled("_EMISSION") == b.IsKeywordEnabled("_EMISSION") && (!a.IsKeywordEnabled("_EMISSION") || TextureBlenderFallback._compareColor(a, b, this.m_generatingTintedAtlaEmission, "_EmissionColor"));
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000AFEFC File Offset: 0x000AE0FC
		public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
		{
			if (resultMaterial.GetTexture("_MainTex") != null)
			{
				resultMaterial.SetColor("_Color", this.m_generatingTintedAtlaColor);
			}
			else
			{
				resultMaterial.SetColor("_Color", (Color)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Color", this.m_notGeneratingAtlasDefaultColor));
			}
			if (resultMaterial.GetTexture("_SpecGlossMap") != null)
			{
				resultMaterial.SetColor("_SpecColor", this.m_generatingTintedAtlaSpecular);
				resultMaterial.SetFloat("_GlossMapScale", this.m_generatingTintedAtlaSpecGlossMapScale);
				resultMaterial.SetFloat("_Glossiness", this.m_generatingTintedAtlaGlossiness);
			}
			else
			{
				resultMaterial.SetColor("_SpecColor", (Color)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_SpecColor", this.m_notGeneratingAtlasDefaultSpecularColor));
				resultMaterial.SetFloat("_Glossiness", (float)this.sourceMaterialPropertyCache.GetValueIfAllSourceAreTheSameOrDefault("_Glossiness", this.m_notGeneratingAtlasDefaultGlossiness));
			}
			if (resultMaterial.GetTexture("_BumpMap") != null)
			{
				resultMaterial.SetFloat("_BumpScale", this.m_generatingTintedAtlaBumpScale);
			}
			else
			{
				resultMaterial.SetFloat("_BumpScale", this.m_generatingTintedAtlaBumpScale);
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

		// Token: 0x06001AAF RID: 6831 RVA: 0x000B0094 File Offset: 0x000AE294
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
			else
			{
				if (texPropertyName.name.Equals("_SpecGlossMap"))
				{
					if (mat != null && mat.HasProperty("_SpecColor"))
					{
						try
						{
							Color color2 = mat.GetColor("_SpecColor");
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
							this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_SpecColor", color2);
							this.sourceMaterialPropertyCache.CacheMaterialProperty(mat, "_Glossiness", color2.a);
						}
						catch (Exception)
						{
						}
					}
					return new Color(0f, 0f, 0f, 0.5f);
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
									goto IL_21D;
								}
								catch (Exception)
								{
									goto IL_21D;
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
			IL_21D:
			return new Color(1f, 1f, 1f, 0f);
		}

		// Token: 0x04001C4E RID: 7246
		private static Color NeutralNormalMap = new Color(0.5f, 0.5f, 1f);

		// Token: 0x04001C4F RID: 7247
		private TextureBlenderMaterialPropertyCacheHelper sourceMaterialPropertyCache = new TextureBlenderMaterialPropertyCacheHelper();

		// Token: 0x04001C50 RID: 7248
		private Color m_tintColor;

		// Token: 0x04001C51 RID: 7249
		private float m_glossiness;

		// Token: 0x04001C52 RID: 7250
		private float m_SpecGlossMapScale;

		// Token: 0x04001C53 RID: 7251
		private Color m_specColor;

		// Token: 0x04001C54 RID: 7252
		private bool m_hasSpecGlossMap;

		// Token: 0x04001C55 RID: 7253
		private float m_bumpScale;

		// Token: 0x04001C56 RID: 7254
		private bool m_shaderDoesEmission;

		// Token: 0x04001C57 RID: 7255
		private Color m_emissionColor;

		// Token: 0x04001C58 RID: 7256
		private TextureBlenderStandardSpecular.Prop propertyToDo = TextureBlenderStandardSpecular.Prop.doNone;

		// Token: 0x04001C59 RID: 7257
		private Color m_generatingTintedAtlaColor = Color.white;

		// Token: 0x04001C5A RID: 7258
		private Color m_generatingTintedAtlaSpecular = Color.black;

		// Token: 0x04001C5B RID: 7259
		private float m_generatingTintedAtlaGlossiness = 1f;

		// Token: 0x04001C5C RID: 7260
		private float m_generatingTintedAtlaSpecGlossMapScale = 1f;

		// Token: 0x04001C5D RID: 7261
		private float m_generatingTintedAtlaBumpScale = 1f;

		// Token: 0x04001C5E RID: 7262
		private Color m_generatingTintedAtlaEmission = Color.white;

		// Token: 0x04001C5F RID: 7263
		private Color m_notGeneratingAtlasDefaultColor = Color.white;

		// Token: 0x04001C60 RID: 7264
		private Color m_notGeneratingAtlasDefaultSpecularColor = new Color(0f, 0f, 0f, 1f);

		// Token: 0x04001C61 RID: 7265
		private float m_notGeneratingAtlasDefaultGlossiness = 0.5f;

		// Token: 0x04001C62 RID: 7266
		private Color m_notGeneratingAtlasDefaultEmisionColor = Color.black;

		// Token: 0x02000434 RID: 1076
		private enum Prop
		{
			// Token: 0x04001C64 RID: 7268
			doColor,
			// Token: 0x04001C65 RID: 7269
			doSpecular,
			// Token: 0x04001C66 RID: 7270
			doEmission,
			// Token: 0x04001C67 RID: 7271
			doBump,
			// Token: 0x04001C68 RID: 7272
			doNone
		}
	}
}
