using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class DistanceField : MonoBehaviour
{
	// Token: 0x06001019 RID: 4121 RVA: 0x0000CE86 File Offset: 0x0000B086
	private void Awake()
	{
		DistanceField.instance = this;
		this.FindBounds();
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x00087714 File Offset: 0x00085914
	private void FindBounds()
	{
		AstarPath component = base.GetComponent<AstarPath>();
		RecastGraph recastGraph = (RecastGraph)component.graphs[0];
		if (recastGraph.CountNodes() == 0)
		{
			component.astarData.LoadFromCache();
		}
		this.bounds = recastGraph.forcedBounds;
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00087758 File Offset: 0x00085958
	public void Generate()
	{
		this.FindBounds();
		RecastGraph recastGraph = (RecastGraph)base.GetComponent<AstarPath>().graphs[0];
		Vector3 vector = this.bounds.size / 5f;
		this.width = Mathf.CeilToInt(vector.x);
		this.height = Mathf.CeilToInt(vector.y);
		this.depth = Mathf.CeilToInt(vector.z);
		Debug.Log(string.Concat(new string[]
		{
			"Generating distance field of size ",
			this.width.ToString(),
			", ",
			this.height.ToString(),
			", ",
			this.depth.ToString(),
			" (",
			((float)(this.width * this.height * this.depth) * 8E-06f).ToString(),
			" MBs)"
		}));
		this.field = new byte[this.width * this.height * this.depth];
		for (int i = 0; i < this.width; i++)
		{
			for (int j = 0; j < this.depth; j++)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(DistanceField.CoordinateToPosition(i, this.height, j) + Vector3.up, Vector3.down), out raycastHit, this.bounds.size.y + 10f, recastGraph.mask))
				{
					int x;
					int num;
					int z;
					DistanceField.PositionToCoordinate(raycastHit.point, out x, out num, out z);
					Debug.DrawRay(DistanceField.CoordinateToPosition(x, num, z), Vector3.up * 5f, Color.red, 10f);
					for (int k = 0; k < this.height; k++)
					{
						this.field[DistanceField.CoordinateToIndex(i, k, j)] = (byte)((k > num) ? (k - num) : 0);
					}
				}
			}
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0000CE94 File Offset: 0x0000B094
	public static Vector3 CoordinateToPosition(int x, int y, int z)
	{
		return DistanceField.instance.bounds.min + new Vector3((float)x + 0.5f, (float)y + 0.5f, (float)z + 0.5f) * 5f;
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0000CED1 File Offset: 0x0000B0D1
	public static int CoordinateToIndex(int x, int y, int z)
	{
		return y + x * DistanceField.instance.height + z * (DistanceField.instance.width * DistanceField.instance.height);
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x00087960 File Offset: 0x00085B60
	public static void PositionToCoordinate(Vector3 position, out int x, out int y, out int z)
	{
		Vector3 vector = (position - DistanceField.instance.bounds.min) / 5f;
		x = Mathf.Clamp(Mathf.RoundToInt(vector.x), 0, DistanceField.instance.width - 1);
		y = Mathf.Clamp(Mathf.RoundToInt(vector.y), 0, DistanceField.instance.height - 1);
		z = Mathf.Clamp(Mathf.RoundToInt(vector.z), 0, DistanceField.instance.depth - 1);
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x000879EC File Offset: 0x00085BEC
	public static int PositionToIndex(Vector3 position)
	{
		int x;
		int y;
		int z;
		DistanceField.PositionToCoordinate(position, out x, out y, out z);
		return DistanceField.CoordinateToIndex(x, y, z);
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0000CEF9 File Offset: 0x0000B0F9
	public static float DistanceAtPosition(Vector3 position)
	{
		return (float)DistanceField.instance.field[DistanceField.PositionToIndex(position)] * 5f + Mathf.Sqrt(DistanceField.instance.bounds.SqrDistance(position));
	}

	// Token: 0x040010F4 RID: 4340
	private const float CELL_SIZE = 5f;

	// Token: 0x040010F5 RID: 4341
	public static DistanceField instance;

	// Token: 0x040010F6 RID: 4342
	[SerializeField]
	private int width;

	// Token: 0x040010F7 RID: 4343
	[SerializeField]
	private int height;

	// Token: 0x040010F8 RID: 4344
	[SerializeField]
	private int depth;

	// Token: 0x040010F9 RID: 4345
	[SerializeField]
	[HideInInspector]
	public byte[] field;

	// Token: 0x040010FA RID: 4346
	private Bounds bounds;
}
