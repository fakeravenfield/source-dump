using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class MB3_KMeansClustering
{
	// Token: 0x06000196 RID: 406 RVA: 0x00043438 File Offset: 0x00041638
	public MB3_KMeansClustering(List<GameObject> gos, int numClusters)
	{
		for (int i = 0; i < gos.Count; i++)
		{
			if (gos[i] != null)
			{
				MB3_KMeansClustering.DataPoint item = new MB3_KMeansClustering.DataPoint(gos[i]);
				this._normalizedDataToCluster.Add(item);
			}
			else
			{
				Debug.LogWarning(string.Format("Object {0} in list of objects to cluster was null.", i));
			}
		}
		if (numClusters <= 0)
		{
			Debug.LogError("Number of clusters must be posititve.");
			numClusters = 1;
		}
		if (this._normalizedDataToCluster.Count <= numClusters)
		{
			Debug.LogError("There must be fewer clusters than objects to cluster");
			numClusters = this._normalizedDataToCluster.Count - 1;
		}
		this._numberOfClusters = numClusters;
		if (this._numberOfClusters <= 0)
		{
			this._numberOfClusters = 1;
		}
		this._clusters = new Vector3[this._numberOfClusters];
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00043514 File Offset: 0x00041714
	private void InitializeCentroids()
	{
		for (int i = 0; i < this._numberOfClusters; i++)
		{
			this._normalizedDataToCluster[i].Cluster = i;
		}
		for (int j = this._numberOfClusters; j < this._normalizedDataToCluster.Count; j++)
		{
			this._normalizedDataToCluster[j].Cluster = UnityEngine.Random.Range(0, this._numberOfClusters);
		}
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0004357C File Offset: 0x0004177C
	private bool UpdateDataPointMeans(bool force)
	{
		if (this.AnyAreEmpty(this._normalizedDataToCluster) && !force)
		{
			return false;
		}
		Vector3[] array = new Vector3[this._numberOfClusters];
		int[] array2 = new int[this._numberOfClusters];
		for (int i = 0; i < this._normalizedDataToCluster.Count; i++)
		{
			int cluster = this._normalizedDataToCluster[i].Cluster;
			array[cluster] += this._normalizedDataToCluster[i].center;
			array2[cluster]++;
		}
		for (int j = 0; j < this._numberOfClusters; j++)
		{
			this._clusters[j] = array[j] / (float)array2[j];
		}
		return true;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00043648 File Offset: 0x00041848
	private bool AnyAreEmpty(List<MB3_KMeansClustering.DataPoint> data)
	{
		int[] array = new int[this._numberOfClusters];
		for (int i = 0; i < this._normalizedDataToCluster.Count; i++)
		{
			array[this._normalizedDataToCluster[i].Cluster]++;
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] == 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x000436AC File Offset: 0x000418AC
	private bool UpdateClusterMembership()
	{
		bool result = false;
		float[] array = new float[this._numberOfClusters];
		for (int i = 0; i < this._normalizedDataToCluster.Count; i++)
		{
			for (int j = 0; j < this._numberOfClusters; j++)
			{
				array[j] = this.ElucidanDistance(this._normalizedDataToCluster[i], this._clusters[j]);
			}
			int num = this.MinIndex(array);
			if (num != this._normalizedDataToCluster[i].Cluster)
			{
				result = true;
				this._normalizedDataToCluster[i].Cluster = num;
			}
		}
		return result;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000323B File Offset: 0x0000143B
	private float ElucidanDistance(MB3_KMeansClustering.DataPoint dataPoint, Vector3 mean)
	{
		return Vector3.Distance(dataPoint.center, mean);
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0004374C File Offset: 0x0004194C
	private int MinIndex(float[] distances)
	{
		int result = 0;
		double num = (double)distances[0];
		for (int i = 0; i < distances.Length; i++)
		{
			if ((double)distances[i] < num)
			{
				num = (double)distances[i];
				result = i;
			}
		}
		return result;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00043780 File Offset: 0x00041980
	public List<Renderer> GetCluster(int idx, out Vector3 mean, out float size)
	{
		if (idx < 0 || idx >= this._numberOfClusters)
		{
			Debug.LogError("idx is out of bounds");
			mean = Vector3.zero;
			size = 1f;
			return new List<Renderer>();
		}
		this.UpdateDataPointMeans(true);
		List<Renderer> list = new List<Renderer>();
		mean = this._clusters[idx];
		float num = 0f;
		for (int i = 0; i < this._normalizedDataToCluster.Count; i++)
		{
			if (this._normalizedDataToCluster[i].Cluster == idx)
			{
				float num2 = Vector3.Distance(mean, this._normalizedDataToCluster[i].center);
				if (num2 > num)
				{
					num = num2;
				}
				list.Add(this._normalizedDataToCluster[i].gameObject.GetComponent<Renderer>());
			}
		}
		mean = this._clusters[idx];
		size = num;
		return list;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00043864 File Offset: 0x00041A64
	public void Cluster()
	{
		bool flag = true;
		bool flag2 = true;
		this.InitializeCentroids();
		int num = this._normalizedDataToCluster.Count * 1000;
		int num2 = 0;
		while (flag2 && flag && num2 < num)
		{
			num2++;
			flag2 = this.UpdateDataPointMeans(false);
			flag = this.UpdateClusterMembership();
		}
	}

	// Token: 0x0400010C RID: 268
	private List<MB3_KMeansClustering.DataPoint> _normalizedDataToCluster = new List<MB3_KMeansClustering.DataPoint>();

	// Token: 0x0400010D RID: 269
	private Vector3[] _clusters = new Vector3[0];

	// Token: 0x0400010E RID: 270
	private int _numberOfClusters;

	// Token: 0x02000056 RID: 86
	private class DataPoint
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000438B0 File Offset: 0x00041AB0
		public DataPoint(GameObject go)
		{
			this.gameObject = go;
			this.center = go.transform.position;
			if (go.GetComponent<Renderer>() == null)
			{
				Debug.LogError("Object does not have a renderer " + ((go != null) ? go.ToString() : null));
			}
		}

		// Token: 0x0400010F RID: 271
		public Vector3 center;

		// Token: 0x04000110 RID: 272
		public GameObject gameObject;

		// Token: 0x04000111 RID: 273
		public int Cluster;
	}
}
