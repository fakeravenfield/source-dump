using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200052A RID: 1322
	[GeneratorMenu(menu = "Objects", name = "Flatten", disengageable = true)]
	[Serializable]
	public class FlattenGenerator : Generator
	{
		// Token: 0x0600212A RID: 8490 RVA: 0x00017949 File Offset: 0x00015B49
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.objectsIn;
			yield return this.canvasIn;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00017959 File Offset: 0x00015B59
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000D4E9C File Offset: 0x000D309C
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.objectsIn.GetObject(chunk);
			Matrix matrix = (Matrix)this.canvasIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || spatialHash == null)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2;
			if (matrix != null)
			{
				matrix2 = matrix.Copy(null);
			}
			else
			{
				matrix2 = chunk.defaultMatrix;
			}
			foreach (SpatialObject spatialObject in spatialHash.AllObjs())
			{
				float num = this.radius * (1f - this.sizeFactor) + this.radius * spatialObject.size * this.sizeFactor;
				num = num / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
				BlobGenerator.DrawBlob(matrix2, spatialObject.pos, spatialObject.height, num, this.fallof, this.noiseAmount, this.noiseSize);
			}
			Matrix matrix3 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix3 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix3);
			}
			if (this.safeBorders != 0)
			{
				Matrix.SafeBorders(matrix, matrix2, this.safeBorders);
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000D4FFC File Offset: 0x000D31FC
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.objectsIn.DrawIcon(this.layout, "Objects", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.canvasIn.DrawIcon(this.layout, "Canvas", false);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.margin = 5;
			this.layout.Field<float>(ref this.radius, "Radius", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.safeBorders, "Safe Borders", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Label("Fallof:", default(Rect), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			Rect cursor = this.layout.cursor;
			this.layout.Par(53, default(Layout.Val), default(Layout.Val));
			this.layout.Curve(this.fallof, this.layout.Inset(80, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Color), null);
			this.layout.cursor = cursor;
			this.layout.margin = 86;
			this.layout.fieldSize = 0.8f;
			this.layout.Label("Noise", default(Rect), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			this.layout.Field<float>(ref this.noiseAmount, "A", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.noiseSize, "S", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x0400213C RID: 8508
		public Generator.Input objectsIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x0400213D RID: 8509
		public Generator.Input canvasIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400213E RID: 8510
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400213F RID: 8511
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002140 RID: 8512
		public float radius = 10f;

		// Token: 0x04002141 RID: 8513
		public float sizeFactor;

		// Token: 0x04002142 RID: 8514
		public AnimationCurve fallof = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 1f, 1f),
			new Keyframe(1f, 1f, 1f, 1f)
		});

		// Token: 0x04002143 RID: 8515
		public float noiseAmount = 0.1f;

		// Token: 0x04002144 RID: 8516
		public float noiseSize = 100f;

		// Token: 0x04002145 RID: 8517
		public int safeBorders;
	}
}
