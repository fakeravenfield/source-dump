using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200075B RID: 1883
	[Serializable]
	public struct ColorDataV1
	{
		// Token: 0x06002EB0 RID: 11952 RVA: 0x000201A7 File Offset: 0x0001E3A7
		public ColorDataV1(Color c)
		{
			this.r = c.r;
			this.g = c.g;
			this.b = c.b;
			this.a = c.a;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000201D9 File Offset: 0x0001E3D9
		public Color ToColor()
		{
			return new Color(Mathf.Clamp01(this.r), Mathf.Clamp01(this.g), Mathf.Clamp01(this.b), Mathf.Clamp01(this.a));
		}

		// Token: 0x04002ACB RID: 10955
		public float r;

		// Token: 0x04002ACC RID: 10956
		public float g;

		// Token: 0x04002ACD RID: 10957
		public float b;

		// Token: 0x04002ACE RID: 10958
		public float a;
	}
}
