using System;
using System.Collections.Generic;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000474 RID: 1140
	[Serializable]
	public class ShaderTextureProperty
	{
		// Token: 0x06001C98 RID: 7320 RVA: 0x0001579E File Offset: 0x0001399E
		public ShaderTextureProperty(string n, bool norm)
		{
			this.name = n;
			this.isNormalMap = norm;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000BCAAC File Offset: 0x000BACAC
		public override bool Equals(object obj)
		{
			if (!(obj is ShaderTextureProperty))
			{
				return false;
			}
			ShaderTextureProperty shaderTextureProperty = (ShaderTextureProperty)obj;
			return this.name.Equals(shaderTextureProperty.name) && this.isNormalMap == shaderTextureProperty.isNormalMap;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000157B4 File Offset: 0x000139B4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000BCAF0 File Offset: 0x000BACF0
		public static string[] GetNames(List<ShaderTextureProperty> props)
		{
			string[] array = new string[props.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = props[i].name;
			}
			return array;
		}

		// Token: 0x04001D6C RID: 7532
		public string name;

		// Token: 0x04001D6D RID: 7533
		public bool isNormalMap;
	}
}
