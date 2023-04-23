using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004E4 RID: 1252
	[GeneratorMenu(menu = "Map", name = "Texture Input", disengageable = true)]
	[Serializable]
	public class TextureInput : Generator
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x00016D0D File Offset: 0x00014F0D
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000CC968 File Offset: 0x000CAB68
		public void CheckLoadTexture()
		{
			object obj = this.matrixLocker;
			lock (obj)
			{
				if (!(this.texture == null))
				{
					if (this.textureMatrix == null || this.loadEachGen)
					{
						this.ReloadTexture();
					}
				}
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000CC9C8 File Offset: 0x000CABC8
		public void ReloadTexture()
		{
			this.textureMatrix = new Matrix(new CoordRect(0, 0, this.texture.width, this.texture.height), null);
			try
			{
				this.textureMatrix.FromTexture(this.texture);
			}
			catch (UnityException message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000CCA28 File Offset: 0x000CAC28
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			if (chunk.stop || !this.enabled || this.texture == null)
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

		// Token: 0x06001F59 RID: 8025 RVA: 0x000CCBFC File Offset: 0x000CADFC
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.62f;
			this.layout.Field<Texture2D>(ref this.texture, "Texture", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.layout.Button("Reload", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null))
			{
				this.ReloadTexture();
			}
			this.layout.Toggle(ref this.loadEachGen, "Reload Each Generate", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null);
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

		// Token: 0x04001FCD RID: 8141
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001FCE RID: 8142
		public Texture2D texture;

		// Token: 0x04001FCF RID: 8143
		public bool loadEachGen;

		// Token: 0x04001FD0 RID: 8144
		[NonSerialized]
		public Matrix textureMatrix;

		// Token: 0x04001FD1 RID: 8145
		[NonSerialized]
		public object matrixLocker = new object();

		// Token: 0x04001FD2 RID: 8146
		public float intensity = 1f;

		// Token: 0x04001FD3 RID: 8147
		public float scale = 1f;

		// Token: 0x04001FD4 RID: 8148
		public Vector2 offset;

		// Token: 0x04001FD5 RID: 8149
		public Matrix.WrapMode wrapMode;

		// Token: 0x04001FD6 RID: 8150
		public bool tile;
	}
}
