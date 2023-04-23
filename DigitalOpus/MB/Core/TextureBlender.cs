using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000429 RID: 1065
	public interface TextureBlender
	{
		// Token: 0x06001A76 RID: 6774
		bool DoesShaderNameMatch(string shaderName);

		// Token: 0x06001A77 RID: 6775
		void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName);

		// Token: 0x06001A78 RID: 6776
		Color OnBlendTexturePixel(string shaderPropertyName, Color pixelColor);

		// Token: 0x06001A79 RID: 6777
		bool NonTexturePropertiesAreEqual(Material a, Material b);

		// Token: 0x06001A7A RID: 6778
		void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial);

		// Token: 0x06001A7B RID: 6779
		Color GetColorIfNoTexture(Material m, ShaderTextureProperty texPropertyName);
	}
}
