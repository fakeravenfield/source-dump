using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004BC RID: 1212
	[GeneratorMenu(menu = "Map", name = "Raw Input (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class RawInput : Generator
	{
		// Token: 0x06001E65 RID: 7781 RVA: 0x000166C0 File Offset: 0x000148C0
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0000296E File Offset: 0x00000B6E
		public void ImportRaw(string path = null)
		{
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000C7D54 File Offset: 0x000C5F54
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix defaultMatrix = chunk.defaultMatrix;
			if (!this.enabled || this.textureAsset == null || this.textureAsset.matrix == null)
			{
				this.output.SetObject(chunk, defaultMatrix);
				return;
			}
			if (chunk.stop)
			{
				return;
			}
			CoordRect newRect = new CoordRect((int)this.offset.x, (int)this.offset.y, (int)((float)defaultMatrix.rect.size.x * this.scale), (int)((float)defaultMatrix.rect.size.z * this.scale));
			Matrix source = this.textureAsset.matrix.Resize(newRect, null);
			defaultMatrix.Replicate(source, this.tile);
			defaultMatrix.Multiply(this.intensity);
			if (this.scale > 1f)
			{
				Matrix src = defaultMatrix.Copy(null);
				int num = 0;
				while ((float)num < this.scale - 1f)
				{
					defaultMatrix.Blur(null, 0.666f, false, false, true, true, null);
					num++;
				}
				Matrix.SafeBorders(src, defaultMatrix, Mathf.Max(defaultMatrix.rect.size.x / 128, 4));
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultMatrix);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000C7E9C File Offset: 0x000C609C
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
			this.layout.Toggle(ref this.tile, "Tile", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null);
		}

		// Token: 0x04001F0B RID: 7947
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F0C RID: 7948
		public MatrixAsset textureAsset;

		// Token: 0x04001F0D RID: 7949
		public Matrix previewMatrix;

		// Token: 0x04001F0E RID: 7950
		[NonSerialized]
		public Texture2D preview;

		// Token: 0x04001F0F RID: 7951
		public string texturePath;

		// Token: 0x04001F10 RID: 7952
		public float intensity = 1f;

		// Token: 0x04001F11 RID: 7953
		public float scale = 1f;

		// Token: 0x04001F12 RID: 7954
		public Vector2 offset;

		// Token: 0x04001F13 RID: 7955
		public bool tile;
	}
}
