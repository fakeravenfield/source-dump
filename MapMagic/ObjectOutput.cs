using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200053D RID: 1341
	[GeneratorMenu(menu = "Output", name = "Objects", disengageable = true)]
	[Serializable]
	public class ObjectOutput : Generator, Generator.IOutput, Layout.ILayered
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x000D84BC File Offset: 0x000D66BC
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x00017D1B File Offset: 0x00015F1B
		public Layout.ILayer[] layers
		{
			get
			{
				return this.baseLayers;
			}
			set
			{
				this.baseLayers = ArrayTools.Convert<ObjectOutput.Layer, Layout.ILayer>(value);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x00017D29 File Offset: 0x00015F29
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x00017D31 File Offset: 0x00015F31
		public int selected { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x00017D3A File Offset: 0x00015F3A
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x00017D42 File Offset: 0x00015F42
		public int collapsedHeight { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x00017D4B File Offset: 0x00015F4B
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x00017D53 File Offset: 0x00015F53
		public int extendedHeight { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x00017D5C File Offset: 0x00015F5C
		public Layout.ILayer def
		{
			get
			{
				return new ObjectOutput.Layer();
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00017D63 File Offset: 0x00015F63
		public override IEnumerable<Generator.Input> Inputs()
		{
			if (this.baseLayers == null)
			{
				this.baseLayers = new ObjectOutput.Layer[0];
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

		// Token: 0x060021C2 RID: 8642 RVA: 0x000D84D4 File Offset: 0x000D66D4
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
			List<TransformPool.InstanceDraft[]> list = new List<TransformPool.InstanceDraft[]>();
			List<ObjectOutput.Layer> list2 = new List<ObjectOutput.Layer>();
			foreach (ObjectOutput objectOutput in MapMagic.instance.gens.GeneratorsOfType<ObjectOutput>(true, true))
			{
				Matrix matrix = null;
				if (objectOutput.biome != null)
				{
					matrix = (Matrix)objectOutput.biome.mask.GetObject(chunk);
					if (matrix == null)
					{
						continue;
					}
				}
				for (int i = 0; i < objectOutput.baseLayers.Length; i++)
				{
					if (chunk.stop)
					{
						return;
					}
					ObjectOutput.Layer layer = objectOutput.baseLayers[i];
					if (!(layer.prefab == null))
					{
						SpatialHash spatialHash = (SpatialHash)objectOutput.baseLayers[i].input.GetObject(chunk);
						if (spatialHash != null)
						{
							list2.Add(layer);
							List<TransformPool.InstanceDraft> list3 = new List<TransformPool.InstanceDraft>();
							foreach (SpatialObject spatialObject in spatialHash.AllObjs())
							{
								if (objectOutput.biome == null || matrix[spatialObject.pos] >= 0.5f)
								{
									float num = layer.relativeHeight ? chunk.heights.GetInterpolated(spatialObject.pos.x, spatialObject.pos.y, Matrix.WrapMode.Once) : 0f;
									Vector3 vector = new Vector3(spatialObject.pos.x, 0f, spatialObject.pos.y);
									vector = vector / spatialHash.size * (float)MapMagic.instance.terrainSize;
									vector.y = (spatialObject.height + num) * (float)MapMagic.instance.terrainHeight;
									if (!layer.parentToRoot)
									{
										vector -= chunk.coord.vector3 * (float)MapMagic.instance.terrainSize;
									}
									Quaternion rotation = layer.rotate ? (spatialObject.rotation % 360f).EulerToQuat() : Quaternion.identity;
									Vector3 scale = layer.scale ? new Vector3(layer.scaleY ? 1f : spatialObject.size, spatialObject.size, layer.scaleY ? 1f : spatialObject.size) : Vector3.one;
									list3.Add(new TransformPool.InstanceDraft
									{
										pos = vector,
										rotation = rotation,
										scale = scale
									});
								}
							}
							list.Add(list3.ToArray());
						}
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			ObjectOutput.ObjectsTuple value = new ObjectOutput.ObjectsTuple
			{
				instances = list,
				layers = list2
			};
			chunk.apply.CheckAdd(typeof(ObjectOutput), value, true);
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugProcessTimes.CheckAdd(typeof(ObjectOutput), chunk.timer.ElapsedMilliseconds, true);
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00017D73 File Offset: 0x00015F73
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
			ObjectOutput.ObjectsTuple objectsTuple = (ObjectOutput.ObjectsTuple)chunk.apply[typeof(ObjectOutput)];
			List<TransformPool.InstanceDraft[]> instancesList = objectsTuple.instances;
			List<ObjectOutput.Layer> layersList = objectsTuple.layers;
			int layersCount = layersList.Count;
			if (chunk.pools == null)
			{
				chunk.pools = new TransformPool[layersCount];
			}
			if (chunk.pools.Length > layersCount)
			{
				TransformPool[] array = new TransformPool[layersCount];
				for (int j = 0; j < layersCount; j++)
				{
					array[j] = chunk.pools[j];
				}
				for (int k = layersCount; k < chunk.pools.Length; k++)
				{
					chunk.pools[k].Clear();
				}
				chunk.pools = array;
			}
			if (chunk.pools.Length < layersCount)
			{
				TransformPool[] array2 = new TransformPool[layersCount];
				for (int l = 0; l < chunk.pools.Length; l++)
				{
					array2[l] = chunk.pools[l];
				}
				chunk.pools = array2;
			}
			for (int m = 0; m < layersCount; m++)
			{
				if (chunk.pools[m] == null)
				{
					chunk.pools[m] = new TransformPool
					{
						prefab = layersList[m].prefab,
						parent = chunk.terrain.transform
					};
				}
				if (chunk.pools[m].prefab != layersList[m].prefab)
				{
					chunk.pools[m].Clear();
					chunk.pools[m].prefab = layersList[m].prefab;
				}
				chunk.pools[m].allowReposition = layersList[m].usePool;
				chunk.pools[m].parent = ((!layersList[m].parentToRoot) ? chunk.terrain.transform : null);
			}
			int num;
			for (int i = 0; i < layersCount; i = num + 1)
			{
				if (layersList[i].prefab == null)
				{
					chunk.pools[i].Clear();
				}
				else
				{
					IEnumerator e = chunk.pools[i].SetTransformsCoroutine(instancesList[i]);
					while (e.MoveNext())
					{
						yield return null;
					}
					e = null;
				}
				num = i;
			}
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugApplyTimes.CheckAdd(typeof(ObjectOutput), chunk.timer.ElapsedMilliseconds, true);
			}
			yield return null;
			yield break;
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000D8874 File Offset: 0x000D6A74
		public static void Purge(Chunk chunk)
		{
			if (chunk.locked || chunk.pools == null)
			{
				return;
			}
			for (int i = 0; i < chunk.pools.Length; i++)
			{
				chunk.pools[i].Clear();
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00017C41 File Offset: 0x00015E41
		public override void OnGUI()
		{
			this.layout.DrawLayered(this, "Layers:", "");
		}

		// Token: 0x0400219C RID: 8604
		public ObjectOutput.Layer[] baseLayers = new ObjectOutput.Layer[0];

		// Token: 0x0200053E RID: 1342
		public class Layer : Layout.ILayer
		{
			// Token: 0x1700028A RID: 650
			// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00017D96 File Offset: 0x00015F96
			// (set) Token: 0x060021C8 RID: 8648 RVA: 0x00017D9E File Offset: 0x00015F9E
			public bool pinned { get; set; }

			// Token: 0x060021C9 RID: 8649 RVA: 0x000D88B4 File Offset: 0x000D6AB4
			public void OnCollapsedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 5;
				layout.fieldSize = 1f;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.input.DrawIcon(layout, null, false);
				layout.Field<Transform>(ref this.prefab, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}

			// Token: 0x060021CA RID: 8650 RVA: 0x000D899C File Offset: 0x000D6B9C
			public void OnExtendedGUI(Layout layout)
			{
				layout.margin = 20;
				layout.rightMargin = 5;
				layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.input.DrawIcon(layout, null, false);
				layout.Field<Transform>(ref this.prefab, null, layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.relativeHeight, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Relative Height", layout.Inset(100, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.rotate, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Rotate", layout.Inset(45, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				layout.Toggle(ref this.scale, null, layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Scale", layout.Inset(40, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.disabled = !this.scale;
				layout.Toggle(ref this.scaleY, null, layout.Inset(18, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null);
				layout.Label("Y only", layout.Inset(45, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				layout.disabled = false;
				layout.Toggle(ref this.usePool, "Use Object Pool", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null);
			}

			// Token: 0x060021CB RID: 8651 RVA: 0x0000296E File Offset: 0x00000B6E
			public void OnAdd()
			{
			}

			// Token: 0x060021CC RID: 8652 RVA: 0x00017DA7 File Offset: 0x00015FA7
			public void OnRemove()
			{
				this.input.Link(null, null);
			}

			// Token: 0x040021A0 RID: 8608
			public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

			// Token: 0x040021A1 RID: 8609
			public Transform prefab;

			// Token: 0x040021A2 RID: 8610
			public bool relativeHeight = true;

			// Token: 0x040021A3 RID: 8611
			public bool rotate = true;

			// Token: 0x040021A4 RID: 8612
			public bool scale = true;

			// Token: 0x040021A5 RID: 8613
			public bool scaleY;

			// Token: 0x040021A6 RID: 8614
			public bool usePool = true;

			// Token: 0x040021A7 RID: 8615
			public bool parentToRoot;
		}

		// Token: 0x0200053F RID: 1343
		public class ObjectsTuple
		{
			// Token: 0x040021A9 RID: 8617
			public List<TransformPool.InstanceDraft[]> instances;

			// Token: 0x040021AA RID: 8618
			public List<ObjectOutput.Layer> layers;
		}
	}
}
