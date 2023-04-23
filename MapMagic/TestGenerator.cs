using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004D4 RID: 1236
	[GeneratorMenu(menu = "Map", name = "Test", disengageable = true, disabled = true)]
	[Serializable]
	public class TestGenerator : Generator
	{
		// Token: 0x06001EF9 RID: 7929 RVA: 0x00016AA6 File Offset: 0x00014CA6
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000CA818 File Offset: 0x000C8A18
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix defaultMatrix = chunk.defaultMatrix;
			if (!this.enabled)
			{
				this.output.SetObject(chunk, defaultMatrix);
				return;
			}
			for (int i = 0; i < defaultMatrix.rect.size.x; i++)
			{
				for (int j = 0; j < defaultMatrix.rect.size.z; j++)
				{
					defaultMatrix[i + defaultMatrix.rect.offset.x, j + defaultMatrix.rect.offset.z] = 0.3f * (float)i / (float)defaultMatrix.rect.size.x * 5f;
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultMatrix);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00016AB6 File Offset: 0x00014CB6
		public float InlineFn(float input)
		{
			return input + 0.01f;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000CA8D8 File Offset: 0x000C8AD8
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.55f;
			this.layout.Field<int>(ref this.iterations, "K Iterations", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.result, "Result", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F7C RID: 8060
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F7D RID: 8061
		public int iterations = 100000;

		// Token: 0x04001F7E RID: 8062
		public float result;
	}
}
