using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004E2 RID: 1250
	[GeneratorMenu(menu = "Map", name = "Raw Input", disengageable = true, disabled = false)]
	[Serializable]
	public class RawInput1 : Generator
	{
		// Token: 0x06001F48 RID: 8008 RVA: 0x00016CA9 File Offset: 0x00014EA9
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0000296E File Offset: 0x00000B6E
		public void ImportRaw(string path = null)
		{
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000CC474 File Offset: 0x000CA674
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			if (!this.enabled || this.textureMatrix == null)
			{
				this.output.SetObject(chunk, null);
				return;
			}
			if (chunk.stop)
			{
				return;
			}
			Matrix defaultMatrix = chunk.defaultMatrix;
			Coord min = defaultMatrix.rect.Min;
			Coord max = defaultMatrix.rect.Max;
			float num = 1f * (float)MapMagic.instance.terrainSize / (float)defaultMatrix.rect.size.x;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float x = ((float)i - this.offset.x / num) / this.scale * (float)this.textureMatrix.rect.size.x / (float)defaultMatrix.rect.size.x;
					float z = ((float)j - this.offset.y / num) / this.scale * (float)this.textureMatrix.rect.size.z / (float)defaultMatrix.rect.size.z;
					defaultMatrix[i, j] = this.textureMatrix.GetInterpolated(x, z, this.wrapMode);
				}
			}
			if (this.scale >= 2f)
			{
				Matrix src = defaultMatrix.Copy(null);
				int num2 = 1;
				while ((float)num2 < this.scale - 1f)
				{
					defaultMatrix.Blur(null, 0.666f, false, false, true, true, null);
					num2 += 2;
				}
				Matrix.SafeBorders(src, defaultMatrix, Mathf.Max(defaultMatrix.rect.size.x / 128, 4));
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultMatrix);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000CC650 File Offset: 0x000CA850
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.margin = 4;
			this.layout.fieldSize = 0.62f;
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.scale, "Scale", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.offset, "Offset", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.tile)
			{
				this.wrapMode = Matrix.WrapMode.Tile;
			}
			this.tile = false;
			this.layout.Field<Matrix.WrapMode>(ref this.wrapMode, "Wrap Mode", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001FBF RID: 8127
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001FC0 RID: 8128
		public Matrix textureMatrix;

		// Token: 0x04001FC1 RID: 8129
		public Matrix previewMatrix;

		// Token: 0x04001FC2 RID: 8130
		[NonSerialized]
		public Texture2D preview;

		// Token: 0x04001FC3 RID: 8131
		public string texturePath;

		// Token: 0x04001FC4 RID: 8132
		public float intensity = 1f;

		// Token: 0x04001FC5 RID: 8133
		public float scale = 1f;

		// Token: 0x04001FC6 RID: 8134
		public Vector2 offset;

		// Token: 0x04001FC7 RID: 8135
		public bool tile;

		// Token: 0x04001FC8 RID: 8136
		public Matrix.WrapMode wrapMode;
	}
}
