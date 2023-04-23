using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000537 RID: 1335
	[GeneratorMenu(menu = "Output", name = "Textures", disengageable = true)]
	[Serializable]
	public class SplatOutput : Generator, Generator.IOutput, Layout.ILayered
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000D7368 File Offset: 0x000D5568
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x00017B7B File Offset: 0x00015D7B
		public Layout.ILayer[] layers
		{
			get
			{
				return this.baseLayers;
			}
			set
			{
				this.baseLayers = ArrayTools.Convert<SplatOutput.Layer, Layout.ILayer>(value);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00017B89 File Offset: 0x00015D89
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x00017B91 File Offset: 0x00015D91
		public int selected { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x00017B9A File Offset: 0x00015D9A
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x00017BA2 File Offset: 0x00015DA2
		public int collapsedHeight { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x00017BAB File Offset: 0x00015DAB
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x00017BB3 File Offset: 0x00015DB3
		public int extendedHeight { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x00017BBC File Offset: 0x00015DBC
		public Layout.ILayer def
		{
			get
			{
				return new SplatOutput.Layer
				{
					splat = new SplatPrototype
					{
						texture = SplatOutput.defaultTex
					}
				};
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x00017BD9 File Offset: 0x00015DD9
		public static Texture2D defaultTex
		{
			get
			{
				if (SplatOutput._defaultTex == null)
				{
					SplatOutput._defaultTex = Extensions.ColorTexture(2, 2, new Color(0.5f, 0.5f, 0.5f, 0f));
				}
				return SplatOutput._defaultTex;
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00017C12 File Offset: 0x00015E12
		public override IEnumerable<Generator.Input> Inputs()
		{
			if (this.baseLayers == null)
			{
				this.baseLayers = new SplatOutput.Layer[0];
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

		// Token: 0x06002193 RID: 8595 RVA: 0x00017C22 File Offset: 0x00015E22
		public override IEnumerable<Generator.Output> Outputs()
		{
			if (this.baseLayers == null)
			{
				this.baseLayers = new SplatOutput.Layer[0];
			}
			int num;
			for (int i = 0; i < this.baseLayers.Length; i = num + 1)
			{
				if (this.baseLayers[i].input != null)
				{
					yield return this.baseLayers[i].output;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000D7380 File Offset: 0x000D5580
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			if (chunk.stop || !this.enabled)
			{
				return;
			}
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
			array[0].Fill(1f);
			float[] array2 = new float[array.Length];
			for (int j = 0; j < this.baseLayers.Length; j++)
			{
				array2[j] = this.baseLayers[j].opacity;
			}
			array2[0] = 1f;
			Matrix.BlendLayers(array, array2);
			for (int k = 0; k < this.baseLayers.Length; k++)
			{
				if (chunk.stop)
				{
					return;
				}
				this.baseLayers[k].output.SetObject(chunk, array[k]);
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000D7488 File Offset: 0x000D5688
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
			List<SplatPrototype> list = new List<SplatPrototype>();
			List<float> list2 = new List<float>();
			List<Matrix> list3 = new List<Matrix>();
			List<Matrix> list4 = new List<Matrix>();
			foreach (SplatOutput splatOutput in MapMagic.instance.gens.GeneratorsOfType<SplatOutput>(true, true))
			{
				Matrix matrix = null;
				if (splatOutput.biome != null)
				{
					matrix = (Matrix)splatOutput.biome.mask.GetObject(chunk);
					if (matrix == null)
					{
						continue;
					}
				}
				for (int i = 0; i < splatOutput.baseLayers.Length; i++)
				{
					list.Add(splatOutput.baseLayers[i].splat);
					list2.Add(splatOutput.baseLayers[i].opacity);
					Generator.Output output = splatOutput.baseLayers[i].output;
					if (chunk.stop)
					{
						return;
					}
					Matrix item = (Matrix)chunk.results[output];
					list3.Add(item);
					list4.Add((splatOutput.biome == null) ? null : matrix);
				}
			}
			for (int j = list3.Count - 1; j >= 0; j--)
			{
				if (list2[j] < 0.001f || list3[j].IsEmpty(0.0001f) || (list4[j] != null && list4[j].IsEmpty(0.0001f)))
				{
					list.RemoveAt(j);
					list2.RemoveAt(j);
					list3.RemoveAt(j);
					list4.RemoveAt(j);
				}
			}
			float[,,] array = new float[MapMagic.instance.resolution, MapMagic.instance.resolution, list.Count];
			if (chunk.stop)
			{
				return;
			}
			if (list3.Count == 0)
			{
				chunk.apply.CheckAdd(typeof(SplatOutput), array, true);
				return;
			}
			int count = list3.Count;
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			CoordRect rect = list3[0].rect;
			float[] array2 = new float[count];
			for (int k = 0; k < length; k++)
			{
				for (int l = 0; l < length2; l++)
				{
					int pos = rect.GetPos(k + rect.offset.x, l + rect.offset.z);
					float num = 0f;
					for (int m = 0; m < count; m++)
					{
						float num2 = list3[m].array[pos];
						if (list4[m] != null)
						{
							num2 *= list4[m].array[pos];
						}
						num += num2;
						array2[m] = num2;
					}
					for (int n = 0; n < count; n++)
					{
						array[l, k, n] = array2[n] / num;
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			SplatOutput.SplatsTuple value = new SplatOutput.SplatsTuple
			{
				array = array,
				prototypes = list.ToArray()
			};
			chunk.apply.CheckAdd(typeof(SplatOutput), value, true);
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugProcessTimes.CheckAdd(typeof(SplatOutput), chunk.timer.ElapsedMilliseconds, true);
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00017C32 File Offset: 0x00015E32
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
			SplatOutput.SplatsTuple splatsTuple = (SplatOutput.SplatsTuple)chunk.apply[typeof(SplatOutput)];
			float[,,] array = splatsTuple.array;
			SplatPrototype[] prototypes = splatsTuple.prototypes;
			if (array.GetLength(2) == 0)
			{
				chunk.ClearSplats();
				yield break;
			}
			TerrainData terrainData = chunk.terrain.terrainData;
			int length = array.GetLength(0);
			if (terrainData.alphamapResolution != length)
			{
				terrainData.alphamapResolution = length;
			}
			for (int i = 0; i < prototypes.Length; i++)
			{
				if (prototypes[i].texture == null)
				{
					prototypes[i].texture = SplatOutput.defaultTex;
				}
			}
			Chunk chunk2 = MapMagic.instance.terrains[chunk.coord.x - 1, chunk.coord.z];
			Chunk chunk3 = MapMagic.instance.terrains[chunk.coord.x + 1, chunk.coord.z];
			Chunk chunk4 = MapMagic.instance.terrains[chunk.coord.x, chunk.coord.z - 1];
			Chunk chunk5 = MapMagic.instance.terrains[chunk.coord.x, chunk.coord.z + 1];
			Terrain prevX = (chunk2 != null && chunk2.complete) ? chunk2.terrain : null;
			Terrain nextX = (chunk3 != null && chunk3.complete) ? chunk3.terrain : null;
			Terrain prevZ = (chunk4 != null && chunk4.complete) ? chunk4.terrain : null;
			Terrain nextZ = (chunk5 != null && chunk5.complete) ? chunk5.terrain : null;
			WeldTerrains.WeldSplats(array, prevX, nextZ, nextX, prevZ, MapMagic.instance.splatsWeldMargins);
			terrainData.terrainLayers = UpgradeTerrainSplats.Upgrade(prototypes);
			terrainData.SetAlphamaps(0, 0, array);
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugApplyTimes.CheckAdd(typeof(SplatOutput), chunk.timer.ElapsedMilliseconds, true);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000D7838 File Offset: 0x000D5A38
		public static void Purge(Chunk chunk)
		{
			if (chunk.locked)
			{
				return;
			}
			SplatPrototype[] array = new SplatPrototype[1];
			if (array[0] == null)
			{
				array[0] = new SplatPrototype();
			}
			if (array[0].texture == null)
			{
				array[0].texture = SplatOutput.defaultTex;
			}
			chunk.terrain.terrainData.terrainLayers = UpgradeTerrainSplats.Upgrade(array);
			float[,,] array2 = new float[16, 16, 1];
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					array2[j, i, 0] = 1f;
				}
			}
			chunk.terrain.terrainData.alphamapResolution = 16;
			chunk.terrain.terrainData.SetAlphamaps(0, 0, array2);
			if (MapMagic.instance.guiDebug)
			{
				Debug.Log("Splats Purged");
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00017C41 File Offset: 0x00015E41
		public override void OnGUI()
		{
			this.layout.DrawLayered(this, "Layers:", "");
		}

		// Token: 0x04002182 RID: 8578
		public SplatOutput.Layer[] baseLayers = new SplatOutput.Layer[]
		{
			new SplatOutput.Layer
			{
				pinned = true,
				name = "Background"
			}
		};

		// Token: 0x04002186 RID: 8582
		public static Texture2D _defaultTex;

		// Token: 0x02000538 RID: 1336
		public class Layer : Layout.ILayer
		{
			// Token: 0x1700027E RID: 638
			// (get) Token: 0x0600219A RID: 8602 RVA: 0x00017C87 File Offset: 0x00015E87
			// (set) Token: 0x0600219B RID: 8603 RVA: 0x00017C8F File Offset: 0x00015E8F
			public bool pinned { get; set; }

			// Token: 0x0600219C RID: 8604 RVA: 0x000D7904 File Offset: 0x000D5B04
			public void OnCollapsedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 20;
				layout.fieldSize = 1f;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				if (!this.pinned)
				{
					this.input.DrawIcon(layout, null, false);
				}
				layout.Label(this.name, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				this.output.DrawIcon(layout, null);
			}

			// Token: 0x0600219D RID: 8605 RVA: 0x000D79BC File Offset: 0x000D5BBC
			public void OnExtendedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 20;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				if (!this.pinned)
				{
					this.input.DrawIcon(layout, null, false);
				}
				layout.Field<string>(ref this.name, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.output.DrawIcon(layout, null);
				layout.Par(2, default(Layout.Val), default(Layout.Val));
				layout.Par(60, default(Layout.Val), default(Layout.Val));
				this.splat.texture = layout.Field<Texture2D>(this.splat.texture, null, layout.Inset(60, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.splat.normalMap = layout.Field<Texture2D>(this.splat.normalMap, null, layout.Inset(60, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Par(2, default(Layout.Val), default(Layout.Val));
				layout.margin = 5;
				layout.rightMargin = 5;
				layout.fieldSize = 0.6f;
				this.opacity = layout.Field<float>(this.opacity, "Opacity", default(Rect), 0f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.splat.tileSize = layout.Field<Vector2>(this.splat.tileSize, "Size", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.splat.tileOffset = layout.Field<Vector2>(this.splat.tileOffset, "Offset", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.splat.specular = layout.Field<Color>(this.splat.specular, "Specular", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.splat.smoothness = layout.Field<float>(this.splat.smoothness, "Smooth", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.splat.metallic = layout.Field<float>(this.splat.metallic, "Metallic", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}

			// Token: 0x0600219E RID: 8606 RVA: 0x00017C98 File Offset: 0x00015E98
			public void OnAdd()
			{
				this.splat = new SplatPrototype
				{
					texture = SplatOutput.defaultTex
				};
			}

			// Token: 0x0600219F RID: 8607 RVA: 0x000D7F88 File Offset: 0x000D6188
			public void OnRemove()
			{
				this.input.Link(null, null);
				Generator.Input connectedInput = this.output.GetConnectedInput(MapMagic.instance.gens.list);
				if (connectedInput != null)
				{
					connectedInput.Link(null, null);
				}
			}

			// Token: 0x04002187 RID: 8583
			public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

			// Token: 0x04002188 RID: 8584
			public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

			// Token: 0x04002189 RID: 8585
			public string name = "Layer";

			// Token: 0x0400218A RID: 8586
			public float opacity = 1f;

			// Token: 0x0400218B RID: 8587
			public SplatPrototype splat = new SplatPrototype();
		}

		// Token: 0x02000539 RID: 1337
		public class SplatsTuple
		{
			// Token: 0x0400218D RID: 8589
			public float[,,] array;

			// Token: 0x0400218E RID: 8590
			public SplatPrototype[] prototypes;
		}
	}
}
