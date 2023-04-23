using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000445 RID: 1093
	[Serializable]
	public class MB3_AgglomerativeClustering
	{
		// Token: 0x06001B0A RID: 6922 RVA: 0x00014A86 File Offset: 0x00012C86
		private float euclidean_distance(Vector3 a, Vector3 b)
		{
			return Vector3.Distance(a, b);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000B05AC File Offset: 0x000AE7AC
		public bool agglomerate(ProgressUpdateCancelableDelegate progFunc)
		{
			this.wasCanceled = true;
			if (progFunc != null)
			{
				this.wasCanceled = progFunc("Filling Priority Queue:", 0f);
			}
			if (this.items.Count <= 1)
			{
				this.clusters = new MB3_AgglomerativeClustering.ClusterNode[0];
				return false;
			}
			this.clusters = new MB3_AgglomerativeClustering.ClusterNode[this.items.Count * 2 - 1];
			for (int i = 0; i < this.items.Count; i++)
			{
				this.clusters[i] = new MB3_AgglomerativeClustering.ClusterNode(this.items[i], i);
			}
			int num = this.items.Count;
			List<MB3_AgglomerativeClustering.ClusterNode> list = new List<MB3_AgglomerativeClustering.ClusterNode>();
			for (int j = 0; j < num; j++)
			{
				this.clusters[j].isUnclustered = true;
				list.Add(this.clusters[j]);
			}
			int num2 = 0;
			new Stopwatch().Start();
			float num3 = 0f;
			long num4 = GC.GetTotalMemory(false) / 1000000L;
			PriorityQueue<float, MB3_AgglomerativeClustering.ClusterDistance> priorityQueue = new PriorityQueue<float, MB3_AgglomerativeClustering.ClusterDistance>();
			int num5 = 0;
			while (list.Count > 1)
			{
				int num6 = 0;
				num2++;
				if (priorityQueue.Count == 0)
				{
					num5++;
					num4 = GC.GetTotalMemory(false) / 1000000L;
					if (progFunc != null)
					{
						this.wasCanceled = progFunc(string.Concat(new string[]
						{
							"Refilling Q:",
							((float)(this.items.Count - list.Count) * 100f / (float)this.items.Count).ToString(),
							" unclustered:",
							list.Count.ToString(),
							" inQ:",
							priorityQueue.Count.ToString(),
							" usedMem:",
							num4.ToString()
						}), (float)(this.items.Count - list.Count) / (float)this.items.Count);
					}
					num3 = this._RefillPriorityQWithSome(priorityQueue, list, this.clusters, progFunc);
					if (priorityQueue.Count == 0)
					{
						break;
					}
				}
				KeyValuePair<float, MB3_AgglomerativeClustering.ClusterDistance> keyValuePair = priorityQueue.Dequeue();
				while (!keyValuePair.Value.a.isUnclustered || !keyValuePair.Value.b.isUnclustered)
				{
					if (priorityQueue.Count == 0)
					{
						num5++;
						num4 = GC.GetTotalMemory(false) / 1000000L;
						if (progFunc != null)
						{
							this.wasCanceled = progFunc(string.Concat(new string[]
							{
								"Creating clusters:",
								((float)(this.items.Count - list.Count) * 100f / (float)this.items.Count).ToString(),
								" unclustered:",
								list.Count.ToString(),
								" inQ:",
								priorityQueue.Count.ToString(),
								" usedMem:",
								num4.ToString()
							}), (float)(this.items.Count - list.Count) / (float)this.items.Count);
						}
						num3 = this._RefillPriorityQWithSome(priorityQueue, list, this.clusters, progFunc);
						if (priorityQueue.Count == 0)
						{
							break;
						}
					}
					keyValuePair = priorityQueue.Dequeue();
					num6++;
				}
				num++;
				MB3_AgglomerativeClustering.ClusterNode clusterNode = new MB3_AgglomerativeClustering.ClusterNode(keyValuePair.Value.a, keyValuePair.Value.b, num - 1, num2, keyValuePair.Key, this.clusters);
				list.Remove(keyValuePair.Value.a);
				list.Remove(keyValuePair.Value.b);
				keyValuePair.Value.a.isUnclustered = false;
				keyValuePair.Value.b.isUnclustered = false;
				int num7 = num - 1;
				if (num7 == this.clusters.Length)
				{
					Debug.LogError("how did this happen");
				}
				this.clusters[num7] = clusterNode;
				list.Add(clusterNode);
				clusterNode.isUnclustered = true;
				for (int k = 0; k < list.Count - 1; k++)
				{
					float num8 = this.euclidean_distance(clusterNode.centroid, list[k].centroid);
					if (num8 < num3)
					{
						priorityQueue.Add(new KeyValuePair<float, MB3_AgglomerativeClustering.ClusterDistance>(num8, new MB3_AgglomerativeClustering.ClusterDistance(clusterNode, list[k])));
					}
				}
				if (this.wasCanceled)
				{
					break;
				}
				num4 = GC.GetTotalMemory(false) / 1000000L;
				if (progFunc != null)
				{
					this.wasCanceled = progFunc(string.Concat(new string[]
					{
						"Creating clusters:",
						((float)(this.items.Count - list.Count) * 100f / (float)this.items.Count).ToString(),
						" unclustered:",
						list.Count.ToString(),
						" inQ:",
						priorityQueue.Count.ToString(),
						" usedMem:",
						num4.ToString()
					}), (float)(this.items.Count - list.Count) / (float)this.items.Count);
				}
			}
			if (progFunc != null)
			{
				this.wasCanceled = progFunc("Finished clustering:", 100f);
			}
			return !this.wasCanceled;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000B0B0C File Offset: 0x000AED0C
		private float _RefillPriorityQWithSome(PriorityQueue<float, MB3_AgglomerativeClustering.ClusterDistance> pq, List<MB3_AgglomerativeClustering.ClusterNode> unclustered, MB3_AgglomerativeClustering.ClusterNode[] clusters, ProgressUpdateCancelableDelegate progFunc)
		{
			List<float> list = new List<float>(2048);
			for (int i = 0; i < unclustered.Count; i++)
			{
				for (int j = i + 1; j < unclustered.Count; j++)
				{
					list.Add(this.euclidean_distance(unclustered[i].centroid, unclustered[j].centroid));
				}
				this.wasCanceled = progFunc("Refilling Queue Part A:", (float)i / ((float)unclustered.Count * 2f));
				if (this.wasCanceled)
				{
					return 10f;
				}
			}
			if (list.Count == 0)
			{
				return 1E+11f;
			}
			float num = MB3_AgglomerativeClustering.NthSmallestElement<float>(list, 2048);
			for (int k = 0; k < unclustered.Count; k++)
			{
				for (int l = k + 1; l < unclustered.Count; l++)
				{
					int idx = unclustered[k].idx;
					int idx2 = unclustered[l].idx;
					float num2 = this.euclidean_distance(unclustered[k].centroid, unclustered[l].centroid);
					if (num2 <= num)
					{
						pq.Add(new KeyValuePair<float, MB3_AgglomerativeClustering.ClusterDistance>(num2, new MB3_AgglomerativeClustering.ClusterDistance(clusters[idx], clusters[idx2])));
					}
				}
				this.wasCanceled = progFunc("Refilling Queue Part B:", (float)(unclustered.Count + k) / ((float)unclustered.Count * 2f));
				if (this.wasCanceled)
				{
					return 10f;
				}
			}
			return num;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000B0C84 File Offset: 0x000AEE84
		public int TestRun(List<GameObject> gos)
		{
			List<MB3_AgglomerativeClustering.item_s> list = new List<MB3_AgglomerativeClustering.item_s>();
			for (int i = 0; i < gos.Count; i++)
			{
				list.Add(new MB3_AgglomerativeClustering.item_s
				{
					go = gos[i],
					coord = gos[i].transform.position
				});
			}
			this.items = list;
			if (this.items.Count > 0)
			{
				this.agglomerate(null);
			}
			return 0;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000B0CF8 File Offset: 0x000AEEF8
		public static void Main()
		{
			List<float> list = new List<float>();
			list.AddRange(new float[]
			{
				19f,
				18f,
				17f,
				16f,
				15f,
				10f,
				11f,
				12f,
				13f,
				14f
			});
			Debug.Log("Loop quick select 10 times.");
			Debug.Log(MB3_AgglomerativeClustering.NthSmallestElement<float>(list, 0));
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000B0D40 File Offset: 0x000AEF40
		public static T NthSmallestElement<T>(List<T> array, int n) where T : IComparable<T>
		{
			if (n < 0)
			{
				n = 0;
			}
			if (n > array.Count - 1)
			{
				n = array.Count - 1;
			}
			if (array.Count == 0)
			{
				throw new ArgumentException("Array is empty.", "array");
			}
			if (array.Count == 1)
			{
				return array[0];
			}
			return MB3_AgglomerativeClustering.QuickSelectSmallest<T>(array, n)[n];
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000B0DA0 File Offset: 0x000AEFA0
		private static List<T> QuickSelectSmallest<T>(List<T> input, int n) where T : IComparable<T>
		{
			int num = 0;
			int i = input.Count - 1;
			int num2 = n;
			System.Random random = new System.Random();
			while (i > num)
			{
				num2 = MB3_AgglomerativeClustering.QuickSelectPartition<T>(input, num, i, num2);
				if (num2 == n)
				{
					break;
				}
				if (num2 > n)
				{
					i = num2 - 1;
				}
				else
				{
					num = num2 + 1;
				}
				num2 = random.Next(num, i);
			}
			return input;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000B0DF0 File Offset: 0x000AEFF0
		private static int QuickSelectPartition<T>(List<T> array, int startIndex, int endIndex, int pivotIndex) where T : IComparable<T>
		{
			T other = array[pivotIndex];
			MB3_AgglomerativeClustering.Swap<T>(array, pivotIndex, endIndex);
			for (int i = startIndex; i < endIndex; i++)
			{
				T t = array[i];
				if (t.CompareTo(other) <= 0)
				{
					MB3_AgglomerativeClustering.Swap<T>(array, i, startIndex);
					startIndex++;
				}
			}
			MB3_AgglomerativeClustering.Swap<T>(array, endIndex, startIndex);
			return startIndex;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000B0E48 File Offset: 0x000AF048
		private static void Swap<T>(List<T> array, int index1, int index2)
		{
			if (index1 == index2)
			{
				return;
			}
			T value = array[index1];
			array[index1] = array[index2];
			array[index2] = value;
		}

		// Token: 0x04001C9A RID: 7322
		public List<MB3_AgglomerativeClustering.item_s> items = new List<MB3_AgglomerativeClustering.item_s>();

		// Token: 0x04001C9B RID: 7323
		public MB3_AgglomerativeClustering.ClusterNode[] clusters;

		// Token: 0x04001C9C RID: 7324
		public bool wasCanceled;

		// Token: 0x04001C9D RID: 7325
		private const int MAX_PRIORITY_Q_SIZE = 2048;

		// Token: 0x02000446 RID: 1094
		[Serializable]
		public class ClusterNode
		{
			// Token: 0x06001B14 RID: 6932 RVA: 0x000B0E78 File Offset: 0x000AF078
			public ClusterNode(MB3_AgglomerativeClustering.item_s ii, int index)
			{
				this.leaf = ii;
				this.idx = index;
				this.leafs = new int[1];
				this.leafs[0] = index;
				this.centroid = ii.coord;
				this.height = 0;
			}

			// Token: 0x06001B15 RID: 6933 RVA: 0x000B0EC8 File Offset: 0x000AF0C8
			public ClusterNode(MB3_AgglomerativeClustering.ClusterNode a, MB3_AgglomerativeClustering.ClusterNode b, int index, int h, float dist, MB3_AgglomerativeClustering.ClusterNode[] clusters)
			{
				this.cha = a;
				this.chb = b;
				this.idx = index;
				this.leafs = new int[a.leafs.Length + b.leafs.Length];
				Array.Copy(a.leafs, this.leafs, a.leafs.Length);
				Array.Copy(b.leafs, 0, this.leafs, a.leafs.Length, b.leafs.Length);
				Vector3 a2 = Vector3.zero;
				for (int i = 0; i < this.leafs.Length; i++)
				{
					a2 += clusters[this.leafs[i]].centroid;
				}
				this.centroid = a2 / (float)this.leafs.Length;
				this.height = h;
				this.distToMergedCentroid = dist;
			}

			// Token: 0x04001C9E RID: 7326
			public MB3_AgglomerativeClustering.item_s leaf;

			// Token: 0x04001C9F RID: 7327
			public MB3_AgglomerativeClustering.ClusterNode cha;

			// Token: 0x04001CA0 RID: 7328
			public MB3_AgglomerativeClustering.ClusterNode chb;

			// Token: 0x04001CA1 RID: 7329
			public int height;

			// Token: 0x04001CA2 RID: 7330
			public float distToMergedCentroid;

			// Token: 0x04001CA3 RID: 7331
			public Vector3 centroid;

			// Token: 0x04001CA4 RID: 7332
			public int[] leafs;

			// Token: 0x04001CA5 RID: 7333
			public int idx;

			// Token: 0x04001CA6 RID: 7334
			public bool isUnclustered = true;
		}

		// Token: 0x02000447 RID: 1095
		[Serializable]
		public class item_s
		{
			// Token: 0x04001CA7 RID: 7335
			public GameObject go;

			// Token: 0x04001CA8 RID: 7336
			public Vector3 coord;
		}

		// Token: 0x02000448 RID: 1096
		public class ClusterDistance
		{
			// Token: 0x06001B17 RID: 6935 RVA: 0x00014AA2 File Offset: 0x00012CA2
			public ClusterDistance(MB3_AgglomerativeClustering.ClusterNode aa, MB3_AgglomerativeClustering.ClusterNode bb)
			{
				this.a = aa;
				this.b = bb;
			}

			// Token: 0x04001CA9 RID: 7337
			public MB3_AgglomerativeClustering.ClusterNode a;

			// Token: 0x04001CAA RID: 7338
			public MB3_AgglomerativeClustering.ClusterNode b;
		}
	}
}
