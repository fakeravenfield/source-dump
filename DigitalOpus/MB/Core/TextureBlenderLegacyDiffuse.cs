using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200042C RID: 1068
	public class TextureBlenderLegacyDiffuse : TextureBlender
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x000145C0 File Offset: 0x000127C0
		public bool DoesShaderNameMatch(string shaderName)
		{
			return shaderName.Equals("Legacy Shaders/Diffuse") || shaderName.Equals("Diffuse");
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000145E1 File Offset: 0x000127E1
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

		// Token: 0x06001A8E RID: 6798 RVA: 0x000AE6B0 File Offset: 0x000AC8B0
		public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor)
		{
			if (this.doColor)
			{
				return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
			}
			return pixelColor;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00014610 File Offset: 0x00012810
		public bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			return TextureBlenderFallback._compareColor(a, b, this.m_defaultTintColor, "_Color");
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0001459B File Offset: 0x0001279B
		public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
		{
			resultMaterial.SetColor("_Color", Color.white);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000AE714 File Offset: 0x000AC914
		public Color GetColorIfNoTexture(Material m, ShaderTextureProperty texPropertyName)
		{
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

		// Token: 0x04001C12 RID: 7186
		private bool doColor;

		// Token: 0x04001C13 RID: 7187
		private Color m_tintColor;

		// Token: 0x04001C14 RID: 7188
		private Color m_defaultTintColor = Color.white;
	}
}
