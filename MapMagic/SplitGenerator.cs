using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000516 RID: 1302
	[GeneratorMenu(menu = "Objects", name = "Split", disengageable = true)]
	[Serializable]
	public class SplitGenerator : Generator, Layout.ILayered
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x000D2370 File Offset: 0x000D0570
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x000175B9 File Offset: 0x000157B9
		public Layout.ILayer[] layers
		{
			get
			{
				return this.baseLayers;
			}
			set
			{
				this.baseLayers = ArrayTools.Convert<SplitGenerator.Layer, Layout.ILayer>(value);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x000175C7 File Offset: 0x000157C7
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x000175CF File Offset: 0x000157CF
		public int selected { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x000175D8 File Offset: 0x000157D8
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x000175E0 File Offset: 0x000157E0
		public int collapsedHeight { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x000175E9 File Offset: 0x000157E9
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x000175F1 File Offset: 0x000157F1
		public int extendedHeight { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x000175FA File Offset: 0x000157FA
		public Layout.ILayer def
		{
			get
			{
				return new SplitGenerator.Layer();
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00017601 File Offset: 0x00015801
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00017611 File Offset: 0x00015811
		public override IEnumerable<Generator.Output> Outputs()
		{
			int num;
			for (int i = 0; i < this.baseLayers.Length; i = num + 1)
			{
				yield return this.baseLayers[i].output;
				num = i;
			}
			yield break;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000D2388 File Offset: 0x000D0588
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.input.GetObject(chunk);
			if (chunk.stop || this.baseLayers.Length == 0)
			{
				return;
			}
			if (!this.enabled || spatialHash == null)
			{
				for (int i = 0; i < this.baseLayers.Length; i++)
				{
					this.baseLayers[i].output.SetObject(chunk, null);
				}
				return;
			}
			SpatialHash[] array = new SpatialHash[this.baseLayers.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = new SpatialHash(spatialHash.offset, spatialHash.size, spatialHash.resolution);
			}
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + 12345 + chunk.coord.x * 1000 + chunk.coord.z, 100);
			bool[] array2 = new bool[this.baseLayers.Length];
			foreach (SpatialObject spatialObject in spatialHash.AllObjs())
			{
				int num = 0;
				float num2 = 0f;
				int num3 = 0;
				for (int k = 0; k < this.baseLayers.Length; k++)
				{
					SplitGenerator.Layer layer = this.baseLayers[k];
					if (spatialObject.height >= layer.heightCondition.x && spatialObject.height <= layer.heightCondition.y && spatialObject.rotation % 360f >= layer.rotationCondition.x && spatialObject.rotation % 360f <= layer.rotationCondition.y && spatialObject.size >= layer.scaleCondition.x && spatialObject.size <= layer.scaleCondition.y)
					{
						array2[k] = true;
						num++;
						num2 += layer.chance;
						num3 = k;
					}
					else
					{
						array2[k] = false;
					}
				}
				if (num != 0)
				{
					if (num == 1 || this.matchType == SplitGenerator.MatchType.layered)
					{
						array[num3].Add(spatialObject);
					}
					else if (num > 1 && this.matchType == SplitGenerator.MatchType.random)
					{
						float num4 = instanceRandom.CoordinateRandom(spatialObject.id);
						num4 *= num2;
						num2 = 0f;
						for (int l = 0; l < this.baseLayers.Length; l++)
						{
							if (array2[l])
							{
								SplitGenerator.Layer layer2 = this.baseLayers[l];
								if (num4 > num2 && num4 < num2 + layer2.chance)
								{
									array[l].Add(spatialObject);
									break;
								}
								num2 += layer2.chance;
							}
						}
					}
				}
			}
			for (int m = 0; m < this.baseLayers.Length; m++)
			{
				this.baseLayers[m].output.SetObject(chunk, array[m]);
			}
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000D2678 File Offset: 0x000D0878
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
			this.layout.Label("Match Type", this.layout.Inset(0.5f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			this.layout.Field<SplitGenerator.MatchType>(ref this.matchType, null, this.layout.Inset(0.5f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.DrawLayered(this, "Layers:", "");
		}

		// Token: 0x040020D9 RID: 8409
		public SplitGenerator.Layer[] baseLayers = new SplitGenerator.Layer[0];

		// Token: 0x040020DD RID: 8413
		public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x040020DE RID: 8414
		public SplitGenerator.MatchType matchType;

		// Token: 0x02000517 RID: 1303
		public class Layer : Layout.ILayer
		{
			// Token: 0x1700024C RID: 588
			// (get) Token: 0x060020AA RID: 8362 RVA: 0x0000257D File Offset: 0x0000077D
			public bool pinned
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060020AB RID: 8363 RVA: 0x000D2818 File Offset: 0x000D0A18
			public void OnCollapsedGUI(Layout layout)
			{
				layout.rightMargin = 20;
				layout.fieldSize = 1f;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				layout.Label(this.name, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				this.output.DrawIcon(layout, null);
			}

			// Token: 0x060020AC RID: 8364 RVA: 0x000D28B4 File Offset: 0x000D0AB4
			public void OnExtendedGUI(Layout layout)
			{
				layout.margin = 7;
				layout.rightMargin = 20;
				layout.fieldSize = 1f;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.name = layout.Field<string>(this.name, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				this.output.DrawIcon(layout, null);
				layout.margin = 5;
				layout.rightMargin = 5;
				layout.fieldSize = 0.6f;
				layout.Field<Vector2>(ref this.heightCondition, "Height", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Field<Vector2>(ref this.rotationCondition, "Rotation", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Field<Vector2>(ref this.scaleCondition, "Scale", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Field<float>(ref this.chance, "Chance", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}

			// Token: 0x060020AD RID: 8365 RVA: 0x0000296E File Offset: 0x00000B6E
			public void OnAdd()
			{
			}

			// Token: 0x060020AE RID: 8366 RVA: 0x000D2B98 File Offset: 0x000D0D98
			public void OnRemove()
			{
				Generator.Input connectedInput = this.output.GetConnectedInput(MapMagic.instance.gens.list);
				if (connectedInput != null)
				{
					connectedInput.Link(null, null);
				}
			}

			// Token: 0x040020DF RID: 8415
			public string name = "Object Layer";

			// Token: 0x040020E0 RID: 8416
			public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

			// Token: 0x040020E1 RID: 8417
			public Vector2 heightCondition = new Vector2(0f, 1f);

			// Token: 0x040020E2 RID: 8418
			public Vector2 rotationCondition = new Vector2(0f, 360f);

			// Token: 0x040020E3 RID: 8419
			public Vector2 scaleCondition = new Vector2(0f, 100f);

			// Token: 0x040020E4 RID: 8420
			public float chance = 1f;
		}

		// Token: 0x02000518 RID: 1304
		public enum MatchType
		{
			// Token: 0x040020E6 RID: 8422
			layered,
			// Token: 0x040020E7 RID: 8423
			random
		}
	}
}
