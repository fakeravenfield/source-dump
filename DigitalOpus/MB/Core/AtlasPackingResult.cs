using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000463 RID: 1123
	[Serializable]
	public class AtlasPackingResult
	{
		// Token: 0x06001C55 RID: 7253 RVA: 0x000155C5 File Offset: 0x000137C5
		public AtlasPackingResult(AtlasPadding[] pds)
		{
			this.padding = pds;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000B9DA0 File Offset: 0x000B7FA0
		public void CalcUsedWidthAndHeight()
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i < this.rects.Length; i++)
			{
				num3 += (float)this.padding[i].leftRight * 2f;
				num4 += (float)this.padding[i].topBottom * 2f;
				num = Mathf.Max(num, this.rects[i].x + this.rects[i].width);
				num2 = Mathf.Max(num2, this.rects[i].y + this.rects[i].height);
			}
			this.usedW = Mathf.CeilToInt(num * (float)this.atlasX + num3);
			this.usedH = Mathf.CeilToInt(num2 * (float)this.atlasY + num4);
			if (this.usedW > this.atlasX)
			{
				this.usedW = this.atlasX;
			}
			if (this.usedH > this.atlasY)
			{
				this.usedH = this.atlasY;
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000B9ED4 File Offset: 0x000B80D4
		public override string ToString()
		{
			return string.Format("numRects: {0}, atlasX: {1} atlasY: {2} usedW: {3} usedH: {4}", new object[]
			{
				this.rects.Length,
				this.atlasX,
				this.atlasY,
				this.usedW,
				this.usedH
			});
		}

		// Token: 0x04001D3A RID: 7482
		public int atlasX;

		// Token: 0x04001D3B RID: 7483
		public int atlasY;

		// Token: 0x04001D3C RID: 7484
		public int usedW;

		// Token: 0x04001D3D RID: 7485
		public int usedH;

		// Token: 0x04001D3E RID: 7486
		public Rect[] rects;

		// Token: 0x04001D3F RID: 7487
		public AtlasPadding[] padding;

		// Token: 0x04001D40 RID: 7488
		public int[] srcImgIdxs;

		// Token: 0x04001D41 RID: 7489
		public object data;
	}
}
