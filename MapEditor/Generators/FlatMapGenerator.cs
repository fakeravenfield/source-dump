using System;
using System.Collections;

namespace MapEditor.Generators
{
	// Token: 0x02000739 RID: 1849
	public class FlatMapGenerator : BaseMapGenerator
	{
		// Token: 0x06002E56 RID: 11862 RVA: 0x0001FE78 File Offset: 0x0001E078
		public override IEnumerator GenerateHeights(float[,] heights, int resolution, float upscaleAmount)
		{
			int num;
			for (int i = 0; i < resolution; i = num + 1)
			{
				for (int j = 0; j < resolution; j++)
				{
					heights[i, j] = 0.05f;
				}
				if (i % 16 == 0)
				{
					this.progress = (float)i / (float)resolution;
					yield return 0;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x04002A66 RID: 10854
		private const float HEIGHT = 0.05f;
	}
}
