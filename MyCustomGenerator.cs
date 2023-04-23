using System;
using System.Collections.Generic;
using MapMagic;
using UnityEngine;

// Token: 0x02000027 RID: 39
[Serializable]
public class MyCustomGenerator : Generator
{
	// Token: 0x060000B1 RID: 177 RVA: 0x00002B36 File Offset: 0x00000D36
	public override IEnumerable<Generator.Input> Inputs()
	{
		yield return this.input;
		yield break;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00002B46 File Offset: 0x00000D46
	public override IEnumerable<Generator.Output> Outputs()
	{
		yield return this.output;
		yield break;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0003FB40 File Offset: 0x0003DD40
	public override void Generate(Chunk chunk, Biome biome = null)
	{
		Matrix matrix = (Matrix)this.input.GetObject(chunk);
		if (matrix == null || chunk.stop)
		{
			return;
		}
		if (!this.enabled)
		{
			this.output.SetObject(chunk, matrix);
			return;
		}
		Matrix matrix2 = new Matrix(matrix.rect, null);
		Coord min = matrix.rect.Min;
		Coord max = matrix.rect.Max;
		for (int i = min.x; i < max.x; i++)
		{
			for (int j = min.z; j < max.z; j++)
			{
				float num = this.level - matrix[i, j];
				matrix2[i, j] = ((num > 0f) ? num : 0f);
			}
		}
		if (chunk.stop)
		{
			return;
		}
		this.output.SetObject(chunk, matrix2);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0003FC20 File Offset: 0x0003DE20
	public override void OnGUI()
	{
		this.layout.Par(20, default(Layout.Val), default(Layout.Val));
		this.input.DrawIcon(this.layout, "Input", true);
		this.output.DrawIcon(this.layout, "Output");
		this.layout.Field<float>(ref this.level, "Level", default(Rect), 0f, 2f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
	}

	// Token: 0x04000071 RID: 113
	public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

	// Token: 0x04000072 RID: 114
	public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

	// Token: 0x04000073 RID: 115
	public float level = 1f;
}
