using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200042B RID: 1067
	public class TextureBlenderLegacyBumpDiffuse : TextureBlender
	{
		// Token: 0x06001A85 RID: 6789 RVA: 0x00014537 File Offset: 0x00012737
		public bool DoesShaderNameMatch(string shaderName)
		{
			return shaderName.Equals("Legacy Shaders/Bumped Diffuse") || shaderName.Equals("Bumped Diffuse");
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00014558 File Offset: 0x00012758
		public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
		{
			if (shaderTexturePropertyName.EndsWith("_MainTex"))
			{
				this.doColor = true;
				this.m_tintColor = sourceMat.GetColor("_Color");
				return;
			}
			this.doColor = false;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000AE5B4 File Offset: 0x000AC7B4
		public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor)
		{
			if (this.doColor)
			{
				return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
			}
			return pixelColor;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00014587 File Offset: 0x00012787
		public bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			return TextureBlenderFallback._compareColor(a, b, this.m_defaultTintColor, "_Color");
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0001459B File Offset: 0x0001279B
		public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
		{
			resultMaterial.SetColor("_Color", Color.white);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000AE618 File Offset: 0x000AC818
		public Color GetColorIfNoTexture(Material m, ShaderTextureProperty texPropertyName)
		{
			if (texPropertyName.name.Equals("_BumpMap"))
			{
				return new Color(0.5f, 0.5f, 1f);
			}
			if (texPropertyName.name.Equals("_MainTex") && m != null && m.HasProperty("_Color"))
			{
				try
				{
					return m.GetColor("_Color");
				}
				catch (Exception)
				{
				}
			}
			return new Color(1f, 1f, 1f, 0f);
		}

		// Token: 0x04001C0F RID: 7183
		private bool doColor;

		// Token: 0x04001C10 RID: 7184
		private Color m_tintColor;

		// Token: 0x04001C11 RID: 7185
		private Color m_defaultTintColor = Color.white;
	}
}
