using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004EC RID: 1260
	[GeneratorMenu(menu = "Map", name = "Curve", disengageable = true)]
	[Serializable]
	public class CurveGenerator : Generator
	{
		// Token: 0x06001F8D RID: 8077 RVA: 0x00016ED2 File Offset: 0x000150D2
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00016EE2 File Offset: 0x000150E2
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000CD744 File Offset: 0x000CB944
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2 = matrix.Copy(null);
			Curve curve = new Curve(this.curve);
			for (int i = 0; i < matrix2.array.Length; i++)
			{
				matrix2.array[i] = curve.Evaluate(matrix2.array[i]);
			}
			if (chunk.stop)
			{
				return;
			}
			Matrix matrix3 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix3 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix3);
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000CD804 File Offset: 0x000CBA04
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			Rect cursor = this.layout.cursor;
			this.layout.Par(50, default(Layout.Val), 0);
			this.layout.Inset(3, default(Layout.Val), default(Layout.Val), default(Layout.Val));
			this.layout.Curve(this.curve, this.layout.Inset(80, default(Layout.Val), default(Layout.Val), 0), this.range.x, this.range.y, default(Color), null);
			this.layout.Par(3, default(Layout.Val), default(Layout.Val));
			this.layout.margin = 86;
			this.layout.cursor = cursor;
			this.layout.Label("Range:", default(Rect), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			this.layout.Field<Vector2>(ref this.range, null, default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001FF4 RID: 8180
		public AnimationCurve curve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 1f, 1f),
			new Keyframe(1f, 1f, 1f, 1f)
		});

		// Token: 0x04001FF5 RID: 8181
		public bool extended = true;

		// Token: 0x04001FF6 RID: 8182
		public Vector2 range = new Vector2(0f, 1f);

		// Token: 0x04001FF7 RID: 8183
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FF8 RID: 8184
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FF9 RID: 8185
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);
	}
}
