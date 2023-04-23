using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000542 RID: 1346
	[GeneratorMenu(menu = "Output", name = "Trees", disengageable = true)]
	[Serializable]
	public class TreesOutput : Generator, Generator.IOutput, Layout.ILayered
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x000D927C File Offset: 0x000D747C
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x00017E27 File Offset: 0x00016027
		public Layout.ILayer[] layers
		{
			get
			{
				return this.baseLayers;
			}
			set
			{
				this.baseLayers = ArrayTools.Convert<TreesOutput.Layer, Layout.ILayer>(value);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x00017E35 File Offset: 0x00016035
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x00017E3D File Offset: 0x0001603D
		public int selected { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00017E46 File Offset: 0x00016046
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x00017E4E File Offset: 0x0001604E
		public int collapsedHeight { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x00017E57 File Offset: 0x00016057
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x00017E5F File Offset: 0x0001605F
		public int extendedHeight { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00017E68 File Offset: 0x00016068
		public Layout.ILayer def
		{
			get
			{
				return new TreesOutput.Layer();
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00017E6F File Offset: 0x0001606F
		public override IEnumerable<Generator.Input> Inputs()
		{
			if (this.baseLayers == null)
			{
				this.baseLayers = new TreesOutput.Layer[0];
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

		// Token: 0x060021E7 RID: 8679 RVA: 0x00017E7F File Offset: 0x0001607F
		public override IEnumerable<Generator.Output> Outputs()
		{
			if (this.baseLayers == null)
			{
				this.baseLayers = new TreesOutput.Layer[0];
			}
			int num;
			for (int i = 0; i < this.baseLayers.Length; i = num + 1)
			{
				if (this.baseLayers[i].output != null)
				{
					yield return this.baseLayers[i].output;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000D9294 File Offset: 0x000D7494
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
			List<TreeInstance> list = new List<TreeInstance>();
			List<TreePrototype> list2 = new List<TreePrototype>();
			foreach (TreesOutput treesOutput in MapMagic.instance.gens.GeneratorsOfType<TreesOutput>(true, true))
			{
				Matrix matrix = null;
				if (treesOutput.biome != null)
				{
					matrix = (Matrix)treesOutput.biome.mask.GetObject(chunk);
					if (matrix == null)
					{
						continue;
					}
				}
				for (int i = 0; i < treesOutput.baseLayers.Length; i++)
				{
					if (chunk.stop)
					{
						return;
					}
					TreesOutput.Layer layer = treesOutput.baseLayers[i];
					SpatialHash spatialHash = (SpatialHash)treesOutput.baseLayers[i].input.GetObject(chunk);
					if (spatialHash != null && !(layer.prefab == null))
					{
						TreePrototype item = new TreePrototype
						{
							prefab = layer.prefab,
							bendFactor = layer.bendFactor
						};
						list2.Add(item);
						int prototypeIndex = list2.Count - 1;
						foreach (SpatialObject spatialObject in spatialHash.AllObjs())
						{
							if (treesOutput.biome == null || matrix[spatialObject.pos] >= 0.5f)
							{
								float num = layer.relativeHeight ? chunk.heights.GetInterpolated(spatialObject.pos.x, spatialObject.pos.y, Matrix.WrapMode.Once) : 0f;
								list.Add(new TreeInstance
								{
									position = new Vector3((spatialObject.pos.x - spatialHash.offset.x) / spatialHash.size, spatialObject.height + num, (spatialObject.pos.y - spatialHash.offset.y) / spatialHash.size),
									rotation = (layer.rotate ? (spatialObject.rotation % 360f) : 0f),
									widthScale = (layer.widthScale ? spatialObject.size : 1f),
									heightScale = (layer.heightScale ? spatialObject.size : 1f),
									prototypeIndex = prototypeIndex,
									color = layer.color,
									lightmapColor = layer.color
								});
							}
						}
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			if (list.Count == 0 && list2.Count == 0)
			{
				return;
			}
			TreesOutput.TreesTuple value = new TreesOutput.TreesTuple
			{
				instances = list.ToArray(),
				prototypes = list2.ToArray()
			};
			chunk.apply.CheckAdd(typeof(TreesOutput), value, true);
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugProcessTimes.CheckAdd(typeof(TreesOutput), chunk.timer.ElapsedMilliseconds, true);
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x00017E8F File Offset: 0x0001608F
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
			chunk.terrain.terrainData.treeInstances = new TreeInstance[0];
			TreesOutput.TreesTuple treesTuple = (TreesOutput.TreesTuple)chunk.apply[typeof(TreesOutput)];
			chunk.terrain.terrainData.treePrototypes = treesTuple.prototypes;
			chunk.terrain.terrainData.treeInstances = treesTuple.instances;
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugApplyTimes.CheckAdd(typeof(TreesOutput), chunk.timer.ElapsedMilliseconds, true);
			}
			yield return null;
			yield break;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00017E9E File Offset: 0x0001609E
		public static void Purge(Chunk chunk)
		{
			if (chunk.locked)
			{
				return;
			}
			chunk.terrain.terrainData.treeInstances = new TreeInstance[0];
			chunk.terrain.terrainData.treePrototypes = new TreePrototype[0];
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00017C41 File Offset: 0x00015E41
		public override void OnGUI()
		{
			this.layout.DrawLayered(this, "Layers:", "");
		}

		// Token: 0x040021B8 RID: 8632
		public TreesOutput.Layer[] baseLayers = new TreesOutput.Layer[0];

		// Token: 0x02000543 RID: 1347
		public class Layer : Layout.ILayer
		{
			// Token: 0x17000294 RID: 660
			// (get) Token: 0x060021ED RID: 8685 RVA: 0x00017EE9 File Offset: 0x000160E9
			// (set) Token: 0x060021EE RID: 8686 RVA: 0x00017EF1 File Offset: 0x000160F1
			public bool pinned { get; set; }

			// Token: 0x060021EF RID: 8687 RVA: 0x000D9634 File Offset: 0x000D7834
			public void OnCollapsedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 5;
				layout.fieldSize = 1f;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.input.DrawIcon(layout, null, false);
				layout.Field<GameObject>(ref this.prefab, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}

			// Token: 0x060021F0 RID: 8688 RVA: 0x000D971C File Offset: 0x000D791C
			public void OnExtendedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 5;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.input.DrawIcon(layout, null, false);
				layout.Field<GameObject>(ref this.prefab, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.relativeHeight, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Relative Height", layout.Inset(100, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.rotate, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Rotate", layout.Inset(45, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.widthScale, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Width Scale", layout.Inset(100, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.heightScale, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Height Scale", layout.Inset(100, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.fieldSize = 0.37f;
				layout.Field<Color>(ref this.color, "Color", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Field<float>(ref this.bendFactor, "Bend Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}

			// Token: 0x060021F1 RID: 8689 RVA: 0x0000296E File Offset: 0x00000B6E
			public void OnAdd()
			{
			}

			// Token: 0x060021F2 RID: 8690 RVA: 0x00017EFA File Offset: 0x000160FA
			public void OnRemove()
			{
				this.input.Link(null, null);
			}

			// Token: 0x040021BC RID: 8636
			public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

			// Token: 0x040021BD RID: 8637
			public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

			// Token: 0x040021BE RID: 8638
			public GameObject prefab;

			// Token: 0x040021BF RID: 8639
			public bool relativeHeight = true;

			// Token: 0x040021C0 RID: 8640
			public bool rotate;

			// Token: 0x040021C1 RID: 8641
			public bool widthScale;

			// Token: 0x040021C2 RID: 8642
			public bool heightScale;

			// Token: 0x040021C3 RID: 8643
			public Color color = Color.white;

			// Token: 0x040021C4 RID: 8644
			public float bendFactor;
		}

		// Token: 0x02000544 RID: 1348
		public class TreesTuple
		{
			// Token: 0x040021C6 RID: 8646
			public TreeInstance[] instances;

			// Token: 0x040021C7 RID: 8647
			public TreePrototype[] prototypes;
		}
	}
}
