using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000548 RID: 1352
	[GeneratorMenu(menu = "Output", name = "Grass", disengageable = true)]
	[Serializable]
	public class GrassOutput : Generator, Generator.IOutput, Layout.ILayered
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000D9EE8 File Offset: 0x000D80E8
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x00017FA6 File Offset: 0x000161A6
		public Layout.ILayer[] layers
		{
			get
			{
				return this.baseLayers;
			}
			set
			{
				this.baseLayers = ArrayTools.Convert<GrassOutput.Layer, Layout.ILayer>(value);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00017FB4 File Offset: 0x000161B4
		// (set) Token: 0x0600220E RID: 8718 RVA: 0x00017FBC File Offset: 0x000161BC
		public int selected { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x00017FC5 File Offset: 0x000161C5
		// (set) Token: 0x06002210 RID: 8720 RVA: 0x00017FCD File Offset: 0x000161CD
		public int collapsedHeight { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00017FD6 File Offset: 0x000161D6
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x00017FDE File Offset: 0x000161DE
		public int extendedHeight { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x00017FE7 File Offset: 0x000161E7
		public Layout.ILayer def
		{
			get
			{
				return new GrassOutput.Layer
				{
					name = "Grass"
				};
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00017FF9 File Offset: 0x000161F9
		public override IEnumerable<Generator.Input> Inputs()
		{
			if (this.maskIn == null)
			{
				this.maskIn = new Generator.Input(Generator.InoutType.Map);
			}
			yield return this.maskIn;
			if (this.baseLayers == null)
			{
				this.baseLayers = new GrassOutput.Layer[0];
			}
			int num;
			for (int i = 0; i < this.baseLayers.Length; i = num + 1)
			{
				if (this.baseLayers[i].input != null)
				{
					yield return this.baseLayers[i].input;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000D9F00 File Offset: 0x000D8100
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix[] array = new Matrix[this.baseLayers.Length];
			for (int i = 0; i < this.baseLayers.Length; i++)
			{
				if (this.baseLayers[i].input != null)
				{
					array[i] = (Matrix)this.baseLayers[i].input.GetObject(chunk);
					if (array[i] != null)
					{
						array[i] = array[i].Copy(null);
					}
				}
				if (array[i] == null)
				{
					array[i] = chunk.defaultMatrix;
				}
			}
			if (this.obscureLayers)
			{
				Matrix.BlendLayers(array, null);
			}
			Matrix matrix = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix != null)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j].Multiply(matrix);
				}
			}
			for (int k = 0; k < this.baseLayers.Length; k++)
			{
				if (chunk.stop)
				{
					return;
				}
				if (this.baseLayers[k].output == null)
				{
					this.baseLayers[k].output = new Generator.Output(Generator.InoutType.Map);
				}
				this.baseLayers[k].output.SetObject(chunk, array[k]);
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000DA010 File Offset: 0x000D8210
		public static void Process(Chunk chunk)
		{
			if (chunk.stop)
			{
				return;
			}
			if (MapMagic.instance.guiDebug)
			{
				if (chunk.timer == null)
				{
					chunk.timer = new Stopwatch();
				}
				else
				{
					chunk.timer.Reset();
				}
				chunk.timer.Start();
			}
			float num = 1f * (float)MapMagic.instance.terrainSize / (float)MapMagic.instance.resolution;
			float num2 = num * num;
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + chunk.coord.x * 1000 + chunk.coord.z, 100);
			int num3 = 0;
			foreach (GrassOutput grassOutput in MapMagic.instance.gens.GeneratorsOfType<GrassOutput>(true, true))
			{
				num3 += grassOutput.baseLayers.Length;
			}
			List<int[,]> list = new List<int[,]>();
			List<DetailPrototype> list2 = new List<DetailPrototype>();
			foreach (GrassOutput grassOutput2 in MapMagic.instance.gens.GeneratorsOfType<GrassOutput>(true, true))
			{
				Matrix matrix = null;
				if (grassOutput2.biome != null)
				{
					matrix = (Matrix)grassOutput2.biome.mask.GetObject(chunk);
					if (matrix == null)
					{
						continue;
					}
				}
				for (int i = 0; i < grassOutput2.baseLayers.Length; i++)
				{
					if (chunk.stop)
					{
						return;
					}
					Generator.Output output = grassOutput2.baseLayers[i].output;
					if (chunk.stop)
					{
						return;
					}
					Matrix matrix2 = (Matrix)chunk.results[output];
					int[,] array = new int[matrix2.rect.size.x, matrix2.rect.size.z];
					for (int j = 0; j < matrix2.rect.size.x; j++)
					{
						for (int k = 0; k < matrix2.rect.size.z; k++)
						{
							float num4 = matrix2[j + matrix2.rect.offset.x, k + matrix2.rect.offset.z];
							float num5 = 1f;
							if (grassOutput2.biome != null)
							{
								num5 = matrix[j + matrix2.rect.offset.x, k + matrix2.rect.offset.z];
							}
							array[k, j] = instanceRandom.RandomToInt(num4 * grassOutput2.baseLayers[i].density * num2 * num5);
						}
					}
					list.Add(array);
					list2.Add(grassOutput2.baseLayers[i].det);
				}
			}
			if (chunk.stop)
			{
				return;
			}
			GrassOutput.GrassTuple value = new GrassOutput.GrassTuple
			{
				details = list.ToArray(),
				prototypes = list2.ToArray()
			};
			chunk.apply.CheckAdd(typeof(GrassOutput), value, true);
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugProcessTimes.CheckAdd(typeof(GrassOutput), chunk.timer.ElapsedMilliseconds, true);
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00018009 File Offset: 0x00016209
		public static IEnumerator Apply(Chunk chunk)
		{
			if (MapMagic.instance.guiDebug)
			{
				if (chunk.timer == null)
				{
					chunk.timer = new Stopwatch();
				}
				else
				{
					chunk.timer.Reset();
				}
				chunk.timer.Start();
			}
			GrassOutput.GrassTuple grassTuple = (GrassOutput.GrassTuple)chunk.apply[typeof(GrassOutput)];
			chunk.terrain.terrainData.SetDetailResolution(MapMagic.instance.resolution, GrassOutput.patchResolution);
			chunk.terrain.terrainData.detailPrototypes = grassTuple.prototypes;
			for (int i = 0; i < grassTuple.details.Length; i++)
			{
				chunk.terrain.terrainData.SetDetailLayer(0, 0, i, grassTuple.details[i]);
			}
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugApplyTimes.CheckAdd(typeof(GrassOutput), chunk.timer.ElapsedMilliseconds, true);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000DA3A8 File Offset: 0x000D85A8
		public static void Purge(Chunk chunk)
		{
			if (chunk.locked)
			{
				return;
			}
			DetailPrototype[] detailPrototypes = new DetailPrototype[0];
			chunk.terrain.terrainData.detailPrototypes = detailPrototypes;
			chunk.terrain.terrainData.SetDetailResolution(16, 8);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000DA3EC File Offset: 0x000D85EC
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Field<int>(ref GrassOutput.patchResolution, "Patch Res", default(Rect), 4f, 64f, 0.35f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			GrassOutput.patchResolution = Mathf.ClosestPowerOfTwo(GrassOutput.patchResolution);
			this.layout.Field<bool>(ref this.obscureLayers, "Obscure Layers", default(Rect), -200000000f, 200000000f, 0.35f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Par(3, default(Layout.Val), default(Layout.Val));
			this.layout.DrawLayered(this, "Layers:", "");
			this.layout.fieldSize = 0.4f;
			this.layout.margin = 10;
			this.layout.rightMargin = 10;
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
		}

		// Token: 0x040021D5 RID: 8661
		public GrassOutput.Layer[] baseLayers = new GrassOutput.Layer[0];

		// Token: 0x040021D9 RID: 8665
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x040021DA RID: 8666
		public static int patchResolution = 16;

		// Token: 0x040021DB RID: 8667
		public bool obscureLayers;

		// Token: 0x02000549 RID: 1353
		public class Layer : Layout.ILayer
		{
			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x0600221C RID: 8732 RVA: 0x00018041 File Offset: 0x00016241
			// (set) Token: 0x0600221D RID: 8733 RVA: 0x00018049 File Offset: 0x00016249
			public bool pinned { get; set; }

			// Token: 0x0600221E RID: 8734 RVA: 0x000DA5C4 File Offset: 0x000D87C4
			public void OnCollapsedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 20;
				layout.fieldSize = 1f;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.input.DrawIcon(layout, null, false);
				layout.Label(this.name, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				if (this.output == null)
				{
					this.output = new Generator.Output(Generator.InoutType.Map);
				}
				this.output.DrawIcon(layout, null);
			}

			// Token: 0x0600221F RID: 8735 RVA: 0x000DA688 File Offset: 0x000D8888
			public void OnExtendedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 20;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.input.DrawIcon(layout, null, false);
				layout.Field<string>(ref this.name, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				if (this.output == null)
				{
					this.output = new Generator.Output(Generator.InoutType.Map);
				}
				this.output.DrawIcon(layout, null);
				layout.margin = 5;
				layout.rightMargin = 10;
				layout.fieldSize = 0.6f;
				layout.fieldSize = 0.65f;
				if (this.renderMode == GrassOutput.Layer.GrassRenderMode.Grass && this.det.renderMode != DetailRenderMode.Grass)
				{
					if (this.det.renderMode == DetailRenderMode.GrassBillboard)
					{
						this.renderMode = GrassOutput.Layer.GrassRenderMode.GrassBillboard;
					}
					else
					{
						this.renderMode = GrassOutput.Layer.GrassRenderMode.VertexLit;
					}
				}
				this.renderMode = layout.Field<GrassOutput.Layer.GrassRenderMode>(this.renderMode, "Mode", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				if (this.renderMode == GrassOutput.Layer.GrassRenderMode.Object || this.renderMode == GrassOutput.Layer.GrassRenderMode.VertexLit)
				{
					this.det.prototype = layout.Field<GameObject>(this.det.prototype, "Object", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
					this.det.prototypeTexture = null;
					this.det.usePrototypeMesh = true;
				}
				else
				{
					layout.Par(60, default(Layout.Val), default(Layout.Val));
					layout.Inset((layout.field.width - 60f) / 2f, default(Layout.Val), default(Layout.Val), default(Layout.Val));
					this.det.prototypeTexture = layout.Field<Texture2D>(this.det.prototypeTexture, null, layout.Inset(60, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
					this.det.prototype = null;
					this.det.usePrototypeMesh = false;
					layout.Par(2, default(Layout.Val), default(Layout.Val));
				}
				switch (this.renderMode)
				{
				case GrassOutput.Layer.GrassRenderMode.Grass:
					this.det.renderMode = DetailRenderMode.Grass;
					break;
				case GrassOutput.Layer.GrassRenderMode.GrassBillboard:
					this.det.renderMode = DetailRenderMode.GrassBillboard;
					break;
				case GrassOutput.Layer.GrassRenderMode.VertexLit:
					this.det.renderMode = DetailRenderMode.VertexLit;
					break;
				case GrassOutput.Layer.GrassRenderMode.Object:
					this.det.renderMode = DetailRenderMode.Grass;
					break;
				}
				this.density = layout.Field<float>(this.density, "Density", default(Rect), -200000000f, 50f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.det.dryColor = layout.Field<Color>(this.det.dryColor, "Dry", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.det.healthyColor = layout.Field<Color>(this.det.healthyColor, "Healthy", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				Vector2 vector = new Vector2(this.det.minWidth, this.det.maxWidth);
				layout.Field<Vector2>(ref vector, "Width", default(Rect), -200000000f, 10f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.det.minWidth = vector.x;
				this.det.maxWidth = vector.y;
				vector = new Vector2(this.det.minHeight, this.det.maxHeight);
				layout.Field<Vector2>(ref vector, "Height", default(Rect), -200000000f, 10f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.det.minHeight = vector.x;
				this.det.maxHeight = vector.y;
				this.det.noiseSpread = layout.Field<float>(this.det.noiseSpread, "Noise", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}

			// Token: 0x06002220 RID: 8736 RVA: 0x00018052 File Offset: 0x00016252
			public void OnAdd()
			{
				this.name = "Grass";
			}

			// Token: 0x06002221 RID: 8737 RVA: 0x0001805F File Offset: 0x0001625F
			public void OnRemove()
			{
				this.input.Link(null, null);
			}

			// Token: 0x040021DC RID: 8668
			public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

			// Token: 0x040021DD RID: 8669
			public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

			// Token: 0x040021DE RID: 8670
			public DetailPrototype det = new DetailPrototype();

			// Token: 0x040021DF RID: 8671
			public string name;

			// Token: 0x040021E0 RID: 8672
			public float density = 0.5f;

			// Token: 0x040021E1 RID: 8673
			public GrassOutput.Layer.GrassRenderMode renderMode;

			// Token: 0x0200054A RID: 1354
			public enum GrassRenderMode
			{
				// Token: 0x040021E4 RID: 8676
				Grass,
				// Token: 0x040021E5 RID: 8677
				GrassBillboard,
				// Token: 0x040021E6 RID: 8678
				VertexLit,
				// Token: 0x040021E7 RID: 8679
				Object
			}
		}

		// Token: 0x0200054B RID: 1355
		public class GrassTuple
		{
			// Token: 0x040021E8 RID: 8680
			public int[][,] details;

			// Token: 0x040021E9 RID: 8681
			public DetailPrototype[] prototypes;
		}
	}
}
