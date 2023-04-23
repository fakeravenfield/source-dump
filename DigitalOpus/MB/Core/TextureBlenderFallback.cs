using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200042A RID: 1066
	public class TextureBlenderFallback : TextureBlender
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x0000476F File Offset: 0x0000296F
		public bool DoesShaderNameMatch(string shaderName)
		{
			return true;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000AE098 File Offset: 0x000AC298
		public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
		{
			if (shaderTexturePropertyName.Equals("_MainTex"))
			{
				this.m_doTintColor = true;
				this.m_tintColor = Color.white;
				if (sourceMat.HasProperty("_Color"))
				{
					this.m_tintColor = sourceMat.GetColor("_Color");
					return;
				}
				if (sourceMat.HasProperty("_TintColor"))
				{
					this.m_tintColor = sourceMat.GetColor("_TintColor");
					return;
				}
			}
			else
			{
				this.m_doTintColor = false;
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000AE10C File Offset: 0x000AC30C
		public Color OnBlendTexturePixel(string shaderPropertyName, Color pixelColor)
		{
			if (this.m_doTintColor)
			{
				return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
			}
			return pixelColor;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000AE170 File Offset: 0x000AC370
		public bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			if (a.HasProperty("_Color"))
			{
				if (TextureBlenderFallback._compareColor(a, b, this.m_defaultColor, "_Color"))
				{
					return true;
				}
			}
			else if (a.HasProperty("_TintColor") && TextureBlenderFallback._compareColor(a, b, this.m_defaultColor, "_TintColor"))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000144E5 File Offset: 0x000126E5
		public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
		{
			if (resultMaterial.HasProperty("_Color"))
			{
				resultMaterial.SetColor("_Color", this.m_defaultColor);
				return;
			}
			if (resultMaterial.HasProperty("_TintColor"))
			{
				resultMaterial.SetColor("_TintColor", this.m_defaultColor);
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000AE1C4 File Offset: 0x000AC3C4
		public Color GetColorIfNoTexture(Material mat, ShaderTextureProperty texProperty)
		{
			if (texProperty.isNormalMap)
			{
				return new Color(0.5f, 0.5f, 1f);
			}
			if (texProperty.name.Equals("_MainTex"))
			{
				if (mat != null && mat.HasProperty("_Color"))
				{
					try
					{
						return mat.GetColor("_Color");
					}
					catch (Exception)
					{
						goto IL_2E5;
					}
				}
				if (!(mat != null) || !mat.HasProperty("_TintColor"))
				{
					goto IL_2E5;
				}
				try
				{
					return mat.GetColor("_TintColor");
				}
				catch (Exception)
				{
					goto IL_2E5;
				}
			}
			if (texProperty.name.Equals("_SpecGlossMap"))
			{
				if (!(mat != null) || !mat.HasProperty("_SpecColor"))
				{
					goto IL_2E5;
				}
				try
				{
					Color color = mat.GetColor("_SpecColor");
					if (mat.HasProperty("_Glossiness"))
					{
						try
						{
							color.a = mat.GetFloat("_Glossiness");
						}
						catch (Exception)
						{
						}
					}
					Debug.LogWarning(color);
					return color;
				}
				catch (Exception)
				{
					goto IL_2E5;
				}
			}
			if (texProperty.name.Equals("_MetallicGlossMap"))
			{
				if (!(mat != null) || !mat.HasProperty("_Metallic"))
				{
					goto IL_2E5;
				}
				try
				{
					float @float = mat.GetFloat("_Metallic");
					Color result = new Color(@float, @float, @float);
					if (mat.HasProperty("_Glossiness"))
					{
						try
						{
							result.a = mat.GetFloat("_Glossiness");
						}
						catch (Exception)
						{
						}
					}
					return result;
				}
				catch (Exception)
				{
					goto IL_2E5;
				}
			}
			if (texProperty.name.Equals("_ParallaxMap"))
			{
				return new Color(0f, 0f, 0f, 0f);
			}
			if (texProperty.name.Equals("_OcclusionMap"))
			{
				return new Color(1f, 1f, 1f, 1f);
			}
			if (texProperty.name.Equals("_EmissionMap"))
			{
				if (!(mat != null) || !mat.HasProperty("_EmissionScaleUI"))
				{
					goto IL_2E5;
				}
				if (mat.HasProperty("_EmissionColor") && mat.HasProperty("_EmissionColorUI"))
				{
					try
					{
						Color color2 = mat.GetColor("_EmissionColor");
						Color color3 = mat.GetColor("_EmissionColorUI");
						float float2 = mat.GetFloat("_EmissionScaleUI");
						if (color2 == new Color(0f, 0f, 0f, 0f) && color3 == new Color(1f, 1f, 1f, 1f))
						{
							return new Color(float2, float2, float2, float2);
						}
						return color3;
					}
					catch (Exception)
					{
						goto IL_2E5;
					}
				}
				try
				{
					float float3 = mat.GetFloat("_EmissionScaleUI");
					return new Color(float3, float3, float3, float3);
				}
				catch (Exception)
				{
					goto IL_2E5;
				}
			}
			if (texProperty.name.Equals("_DetailMask"))
			{
				return new Color(0f, 0f, 0f, 0f);
			}
			IL_2E5:
			return new Color(1f, 1f, 1f, 0f);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000AE538 File Offset: 0x000AC738
		public static bool _compareColor(Material a, Material b, Color defaultVal, string propertyName)
		{
			Color lhs = defaultVal;
			Color rhs = defaultVal;
			if (a.HasProperty(propertyName))
			{
				lhs = a.GetColor(propertyName);
			}
			if (b.HasProperty(propertyName))
			{
				rhs = b.GetColor(propertyName);
			}
			return !(lhs != rhs);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000AE578 File Offset: 0x000AC778
		public static bool _compareFloat(Material a, Material b, float defaultVal, string propertyName)
		{
			float num = defaultVal;
			float num2 = defaultVal;
			if (a.HasProperty(propertyName))
			{
				num = a.GetFloat(propertyName);
			}
			if (b.HasProperty(propertyName))
			{
				num2 = b.GetFloat(propertyName);
			}
			return num == num2;
		}

		// Token: 0x04001C0C RID: 7180
		private bool m_doTintColor;

		// Token: 0x04001C0D RID: 7181
		private Color m_tintColor;

		// Token: 0x04001C0E RID: 7182
		private Color m_defaultColor = Color.white;
	}
}
