using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000426 RID: 1062
	[Serializable]
	public class MB3_MeshBakerGrouperKMeans : MB3_MeshBakerGrouperCore
	{
		// Token: 0x06001A6C RID: 6764 RVA: 0x00014469 File Offset: 0x00012669
		public MB3_MeshBakerGrouperKMeans(GrouperData data)
		{
			this.d = data;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000AD9B0 File Offset: 0x000ABBB0
		public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
		{
			Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
			List<GameObject> list = new List<GameObject>();
			int num = 20;
			foreach (GameObject gameObject in selection)
			{
				if (!(gameObject == null))
				{
					GameObject gameObject2 = gameObject;
					Renderer component = gameObject2.GetComponent<Renderer>();
					if (component is MeshRenderer || component is SkinnedMeshRenderer)
					{
						list.Add(gameObject2);
					}
				}
			}
			if (list.Count > 0 && num > 0 && num < list.Count)
			{
				MB3_KMeansClustering mb3_KMeansClustering = new MB3_KMeansClustering(list, num);
				mb3_KMeansClustering.Cluster();
				this.clusterCenters = new Vector3[num];
				this.clusterSizes = new float[num];
				for (int i = 0; i < num; i++)
				{
					List<Renderer> cluster = mb3_KMeansClustering.GetCluster(i, out this.clusterCenters[i], out this.clusterSizes[i]);
					if (cluster.Count > 0)
					{
						dictionary.Add("Cluster_" + i.ToString(), cluster);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		public override void DrawGizmos(Bounds sceneObjectBounds)
		{
			if (this.clusterCenters != null && this.clusterSizes != null && this.clusterCenters.Length == this.clusterSizes.Length)
			{
				for (int i = 0; i < this.clusterSizes.Length; i++)
				{
					Gizmos.DrawWireSphere(this.clusterCenters[i], this.clusterSizes[i]);
				}
			}
		}

		// Token: 0x04001C00 RID: 7168
		public int numClusters = 4;

		// Token: 0x04001C01 RID: 7169
		public Vector3[] clusterCenters = new Vector3[0];

		// Token: 0x04001C02 RID: 7170
		public float[] clusterSizes = new float[0];
	}
}
