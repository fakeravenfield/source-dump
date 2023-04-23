using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x020002F8 RID: 760
[Serializable]
public class CoverPointList
{
	// Token: 0x060013FB RID: 5115 RVA: 0x0009510C File Offset: 0x0009330C
	public static void Serialize(CoverPoint[] coverPoints, Stream stream)
	{
		CoverPointList graph = new CoverPointList(coverPoints);
		new BinaryFormatter().Serialize(stream, graph);
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x0009512C File Offset: 0x0009332C
	public static bool Serialize(CoverPoint[] coverPoints, string filePath)
	{
		bool result;
		try
		{
			using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				CoverPointList.Serialize(coverPoints, fileStream);
				fileStream.Flush();
				fileStream.Close();
			}
			result = true;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			result = false;
		}
		return result;
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x0009518C File Offset: 0x0009338C
	public static void Deserialize(Transform parent, Stream stream)
	{
		foreach (CoverPointList.Entry entry in ((CoverPointList)new BinaryFormatter().Deserialize(stream)).entries)
		{
			CoverPoint coverPoint = new GameObject("Cover Point").AddComponent<CoverPoint>();
			coverPoint.type = entry.type;
			coverPoint.coverage = entry.coverage;
			coverPoint.transform.position = new Vector3(entry.px, entry.py, entry.pz);
			coverPoint.transform.rotation = new Quaternion(entry.qx, entry.qy, entry.qz, entry.qw);
			coverPoint.transform.parent = parent;
		}
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x00095244 File Offset: 0x00093444
	public static bool Deserialize(Transform parent, TextAsset asset)
	{
		bool result;
		try
		{
			using (MemoryStream memoryStream = new MemoryStream(asset.bytes))
			{
				CoverPointList.Deserialize(parent, memoryStream);
			}
			result = true;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			result = false;
		}
		return result;
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x0009529C File Offset: 0x0009349C
	public CoverPointList(CoverPoint[] coverPoints)
	{
		this.entries = new CoverPointList.Entry[coverPoints.Length];
		for (int i = 0; i < coverPoints.Length; i++)
		{
			CoverPointList.Entry entry = new CoverPointList.Entry();
			entry.type = coverPoints[i].type;
			entry.coverage = coverPoints[i].coverage;
			entry.px = coverPoints[i].transform.position.x;
			entry.py = coverPoints[i].transform.position.y;
			entry.pz = coverPoints[i].transform.position.z;
			entry.qx = coverPoints[i].transform.rotation.x;
			entry.qy = coverPoints[i].transform.rotation.y;
			entry.qz = coverPoints[i].transform.rotation.z;
			entry.qw = coverPoints[i].transform.rotation.w;
			this.entries[i] = entry;
		}
	}

	// Token: 0x04001578 RID: 5496
	public CoverPointList.Entry[] entries;

	// Token: 0x020002F9 RID: 761
	[Serializable]
	public class Entry
	{
		// Token: 0x04001579 RID: 5497
		public float px;

		// Token: 0x0400157A RID: 5498
		public float py;

		// Token: 0x0400157B RID: 5499
		public float pz;

		// Token: 0x0400157C RID: 5500
		public float qx;

		// Token: 0x0400157D RID: 5501
		public float qy;

		// Token: 0x0400157E RID: 5502
		public float qz;

		// Token: 0x0400157F RID: 5503
		public float qw;

		// Token: 0x04001580 RID: 5504
		public CoverPoint.Type type;

		// Token: 0x04001581 RID: 5505
		public byte coverage;
	}
}
