using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200042D RID: 1069
	public class TextureBlenderMaterialPropertyCacheHelper
	{
		// Token: 0x06001A93 RID: 6803 RVA: 0x000AE788 File Offset: 0x000AC988
		private bool AllNonTexturePropertyValuesAreEqual(string prop)
		{
			bool flag = false;
			object obj = null;
			foreach (TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair materialPropertyPair in this.nonTexturePropertyValuesForSourceMaterials.Keys)
			{
				if (materialPropertyPair.property.Equals(prop))
				{
					if (!flag)
					{
						obj = this.nonTexturePropertyValuesForSourceMaterials[materialPropertyPair];
						flag = true;
					}
					else if (!obj.Equals(this.nonTexturePropertyValuesForSourceMaterials[materialPropertyPair]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00014637 File Offset: 0x00012837
		public void CacheMaterialProperty(Material m, string property, object value)
		{
			this.nonTexturePropertyValuesForSourceMaterials[new TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair(m, property)] = value;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000AE81C File Offset: 0x000ACA1C
		public object GetValueIfAllSourceAreTheSameOrDefault(string property, object defaultValue)
		{
			if (this.AllNonTexturePropertyValuesAreEqual(property))
			{
				foreach (TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair materialPropertyPair in this.nonTexturePropertyValuesForSourceMaterials.Keys)
				{
					if (materialPropertyPair.property.Equals(property))
					{
						return this.nonTexturePropertyValuesForSourceMaterials[materialPropertyPair];
					}
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x04001C15 RID: 7189
		private Dictionary<TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair, object> nonTexturePropertyValuesForSourceMaterials = new Dictionary<TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair, object>();

		// Token: 0x0200042E RID: 1070
		private struct MaterialPropertyPair
		{
			// Token: 0x06001A97 RID: 6807 RVA: 0x0001465F File Offset: 0x0001285F
			public MaterialPropertyPair(Material m, string prop)
			{
				this.material = m;
				this.property = prop;
			}

			// Token: 0x06001A98 RID: 6808 RVA: 0x000AE898 File Offset: 0x000ACA98
			public override bool Equals(object obj)
			{
				if (!(obj is TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair))
				{
					return false;
				}
				TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair materialPropertyPair = (TextureBlenderMaterialPropertyCacheHelper.MaterialPropertyPair)obj;
				return this.material.Equals(materialPropertyPair.material) && !(this.property != materialPropertyPair.property);
			}

			// Token: 0x06001A99 RID: 6809 RVA: 0x0001466F File Offset: 0x0001286F
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x04001C16 RID: 7190
			public Material material;

			// Token: 0x04001C17 RID: 7191
			public string property;
		}
	}
}
