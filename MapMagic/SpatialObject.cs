using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020005A9 RID: 1449
	[Serializable]
	public class SpatialObject
	{
		// Token: 0x06002579 RID: 9593 RVA: 0x000F1064 File Offset: 0x000EF264
		public SpatialObject Copy()
		{
			return new SpatialObject
			{
				pos = this.pos,
				height = this.height,
				rotation = this.rotation,
				size = this.size,
				type = this.type,
				id = this.id
			};
		}

		// Token: 0x04002417 RID: 9239
		public Vector2 pos;

		// Token: 0x04002418 RID: 9240
		public float height;

		// Token: 0x04002419 RID: 9241
		public float rotation;

		// Token: 0x0400241A RID: 9242
		public float size;

		// Token: 0x0400241B RID: 9243
		public int type;

		// Token: 0x0400241C RID: 9244
		public int id;
	}
}
