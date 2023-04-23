using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000533 RID: 1331
	[GeneratorMenu(menu = "Output", name = "Height", disengageable = true)]
	[Serializable]
	public class HeightOutput : Generator, Generator.IOutput
	{
		// Token: 0x06002169 RID: 8553 RVA: 0x00017AA5 File Offset: 0x00015CA5
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00017AB5 File Offset: 0x00015CB5
		public override IEnumerable<Generator.Output> Outputs()
		{
			if (this.output == null)
			{
				this.output = new Generator.Output(Generator.InoutType.Map);
			}
			yield return this.output;
			yield break;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x00017AC5 File Offset: 0x00015CC5
		// (set) Token: 0x0600216C RID: 8556 RVA: 0x00017ACD File Offset: 0x00015CCD
		public float layer { get; set; }

		// Token: 0x0600216D RID: 8557 RVA: 0x000D6924 File Offset: 0x000D4B24
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
			if (chunk.heights == null || chunk.heights.rect.size.x != MapMagic.instance.resolution)
			{
				chunk.heights = chunk.defaultMatrix;
			}
			if (chunk.heights.rect != chunk.defaultRect)
			{
				chunk.heights.Resize(chunk.defaultRect, null);
			}
			chunk.heights.Clear();
			foreach (HeightOutput heightOutput in MapMagic.instance.gens.GeneratorsOfType<HeightOutput>(true, true))
			{
				if (chunk.stop)
				{
					return;
				}
				Matrix matrix = (Matrix)heightOutput.input.GetObject(chunk);
				if (matrix != null)
				{
					Matrix matrix2 = null;
					if (heightOutput.biome != null)
					{
						matrix2 = (Matrix)heightOutput.biome.mask.GetObject(chunk);
						if (matrix2 == null)
						{
							continue;
						}
					}
					if (heightOutput.biome == null)
					{
						chunk.heights.Add(matrix);
					}
					else
					{
						chunk.heights.Add(matrix, matrix2);
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			int num = MapMagic.instance.resolution + 1;
			float[,] array = new float[num, num];
			for (int i = 0; i < chunk.heights.rect.size.x; i++)
			{
				for (int j = 0; j < chunk.heights.rect.size.z; j++)
				{
					array[j, i] += chunk.heights[i + chunk.heights.rect.offset.x, j + chunk.heights.rect.offset.z];
				}
			}
			for (int k = 0; k < num; k++)
			{
				float num2 = array[num - 3, k];
				float num3 = array[num - 2, k];
				float num4 = num3 - (num2 - num3);
				array[num - 1, k] = num4;
			}
			for (int l = 0; l < num; l++)
			{
				float num5 = array[l, num - 3];
				float num6 = array[l, num - 2];
				float num7 = num6 - (num5 - num6);
				array[l, num - 1] = num7;
			}
			array[num - 1, num - 1] = array[num - 1, num - 2];
			if (chunk.stop)
			{
				return;
			}
			chunk.apply.CheckAdd(typeof(HeightOutput), array, true);
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugProcessTimes.CheckAdd(typeof(HeightOutput), chunk.timer.ElapsedMilliseconds, true);
			}
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000D6C38 File Offset: 0x000D4E38
		public static void Purge(Chunk tw)
		{
			if (tw.locked)
			{
				return;
			}
			float[,] array = new float[33, 33];
			tw.terrain.terrainData.heightmapResolution = array.GetLength(0);
			tw.terrain.terrainData.SetHeights(0, 0, array);
			tw.terrain.terrainData.size = new Vector3((float)MapMagic.instance.terrainSize, (float)MapMagic.instance.terrainHeight, (float)MapMagic.instance.terrainSize);
			if (MapMagic.instance.guiDebug)
			{
				Debug.Log("Heights Purged");
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x00017AD6 File Offset: 0x00015CD6
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
			float[,] heights2D = (float[,])chunk.apply[typeof(HeightOutput)];
			TerrainData data = chunk.terrain.terrainData;
			Vector3 vector = new Vector3((float)MapMagic.instance.terrainSize, (float)MapMagic.instance.terrainHeight, (float)MapMagic.instance.terrainSize);
			int length = heights2D.GetLength(0);
			if ((data.size - vector).sqrMagnitude > 0.01f || data.heightmapResolution != length)
			{
				if (length <= 64)
				{
					data.heightmapResolution = length;
					data.size = new Vector3(vector.x, vector.y, vector.z);
				}
				else
				{
					data.heightmapResolution = 65;
					chunk.terrain.Flush();
					int num = (length - 1) / 64;
					data.size = new Vector3(vector.x / (float)num, vector.y, vector.z / (float)num);
					data.heightmapResolution = length;
				}
			}
			yield return null;
			TerrainGrid terrains = MapMagic.instance.terrains;
			WeldTerrains.WeldHeights(heights2D, terrains.GetTerrain(chunk.coord.x - 1, chunk.coord.z, true), terrains.GetTerrain(chunk.coord.x, chunk.coord.z + 1, true), terrains.GetTerrain(chunk.coord.x + 1, chunk.coord.z, true), terrains.GetTerrain(chunk.coord.x, chunk.coord.z - 1, true), MapMagic.instance.heightWeldMargins);
			yield return null;
			data.SetHeightsDelayLOD(0, 0, heights2D);
			yield return null;
			chunk.terrain.ApplyDelayedHeightmapModification();
			chunk.terrain.Flush();
			chunk.terrain.SetNeighbors(terrains.GetTerrain(chunk.coord.x - 1, chunk.coord.z, false), terrains.GetTerrain(chunk.coord.x, chunk.coord.z + 1, false), terrains.GetTerrain(chunk.coord.x + 1, chunk.coord.z, false), terrains.GetTerrain(chunk.coord.x, chunk.coord.z - 1, false));
			if (chunk.timer != null)
			{
				chunk.timer.Stop();
				MapMagic.instance.guiDebugApplyTimes.CheckAdd(typeof(HeightOutput), chunk.timer.ElapsedMilliseconds, true);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000D6CD0 File Offset: 0x000D4ED0
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Height", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			if (this.output == null)
			{
				this.output = new Generator.Output(Generator.InoutType.Map);
			}
			this.layout.Field<float>(ref this.scale, "Scale", default(Rect), 0.0625f, 8f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.scale > 1f)
			{
				this.scale = (float)Mathf.ClosestPowerOfTwo((int)this.scale);
				return;
			}
			this.scale = 1f / (float)Mathf.ClosestPowerOfTwo((int)(1f / this.scale));
		}

		// Token: 0x04002170 RID: 8560
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002171 RID: 8561
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002172 RID: 8562
		public float scale = 1f;
	}
}
