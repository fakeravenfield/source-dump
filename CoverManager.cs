using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class CoverManager : MonoBehaviour
{
	// Token: 0x0600048A RID: 1162 RVA: 0x00004E32 File Offset: 0x00003032
	public void Awake()
	{
		CoverManager.instance = this;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00055AC4 File Offset: 0x00053CC4
	public void StartGame()
	{
		this.coverPoints = UnityEngine.Object.FindObjectsOfType<CoverPoint>();
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			spawnPoints[i].FindCoverPoints();
		}
		this.SetupCoverGrid();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00055B04 File Offset: 0x00053D04
	private void SetupCoverGrid()
	{
		PathfindingBox[] array = UnityEngine.Object.FindObjectsOfType<PathfindingBox>();
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		foreach (PathfindingBox pathfindingBox in array)
		{
			if (pathfindingBox.type == PathfindingBox.Type.Infantry)
			{
				Vector3 vector3 = pathfindingBox.transform.localToWorldMatrix.MultiplyPoint(new Vector3(-0.5f, 0f, -0.5f));
				Vector3 rhs = pathfindingBox.transform.localToWorldMatrix.MultiplyPoint(new Vector3(-0.5f, 0f, 0.5f));
				Vector3 rhs2 = pathfindingBox.transform.localToWorldMatrix.MultiplyPoint(new Vector3(0.5f, 0f, -0.5f));
				Vector3 rhs3 = pathfindingBox.transform.localToWorldMatrix.MultiplyPoint(new Vector3(0.5f, 0f, 0.5f));
				if (vector == vector2)
				{
					vector = vector3;
					vector2 = vector3;
				}
				vector = Vector3.Min(vector, vector3);
				vector = Vector3.Min(vector, rhs);
				vector = Vector3.Min(vector, rhs2);
				vector = Vector3.Min(vector, rhs3);
				vector2 = Vector3.Max(vector2, vector3);
				vector2 = Vector3.Max(vector2, rhs);
				vector2 = Vector3.Max(vector2, rhs2);
				vector2 = Vector3.Max(vector2, rhs3);
			}
		}
		this.coverGridOrigin = new Vector2(vector.x, vector.z);
		this.coverGridDimensions = 32;
		this.coverGridCellSize = new Vector2((vector2.x - vector.x) / (float)this.coverGridDimensions, (vector2.z - vector.z) / (float)this.coverGridDimensions);
		int num = 0;
		while (num < 2 && Mathf.Min(this.coverGridCellSize.x, this.coverGridCellSize.y) <= 15f)
		{
			this.coverGridDimensions /= 2;
			this.coverGridCellSize = new Vector2((vector2.x - vector.x) / (float)this.coverGridDimensions, (vector2.z - vector.z) / (float)this.coverGridDimensions);
			num++;
		}
		this.coverGrid = new List<CoverPoint>[this.coverGridDimensions, this.coverGridDimensions];
		this.isCqcCell = new bool[this.coverGridDimensions, this.coverGridDimensions];
		int num2 = Mathf.CeilToInt(0.015f * this.coverGridCellSize.x * this.coverGridCellSize.y);
		for (int j = 0; j < this.coverGridDimensions; j++)
		{
			for (int k = 0; k < this.coverGridDimensions; k++)
			{
				this.coverGrid[j, k] = new List<CoverPoint>();
			}
		}
		foreach (CoverPoint coverPoint in this.coverPoints)
		{
			int j;
			int k;
			this.PositionToCoverGridCoordinate(coverPoint.transform.position, out j, out k);
			if (j > 0 && j < this.coverGridDimensions && k > 0 && k < this.coverGridDimensions)
			{
				this.coverGrid[j, k].Add(coverPoint);
			}
		}
		for (int j = 0; j < this.coverGridDimensions; j++)
		{
			for (int k = 0; k < this.coverGridDimensions; k++)
			{
				this.isCqcCell[j, k] = (this.coverGrid[j, k].Count > num2);
			}
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00055E5C File Offset: 0x0005405C
	private void PositionToCoverGridCoordinate(Vector3 position, out int x, out int y)
	{
		x = Mathf.FloorToInt((position.x - this.coverGridOrigin.x) / this.coverGridCellSize.x);
		y = Mathf.FloorToInt((position.z - this.coverGridOrigin.y) / this.coverGridCellSize.y);
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00055EB4 File Offset: 0x000540B4
	public bool IsInCqcCell(Vector3 position)
	{
		int num;
		int num2;
		this.PositionToCoverGridCoordinate(position, out num, out num2);
		return num > 0 && num < this.coverGridDimensions && num2 > 0 && num2 < this.coverGridDimensions && this.isCqcCell[num, num2];
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00055EF8 File Offset: 0x000540F8
	public CoverPoint ClosestVacant(Vector3 point)
	{
		CoverPoint result = null;
		float num = 20f;
		foreach (CoverPoint coverPoint in this.coverPoints)
		{
			if (!coverPoint.taken)
			{
				float num2 = Vector3.Distance(coverPoint.transform.position, point);
				if (num2 < num)
				{
					num = num2;
					result = coverPoint;
				}
			}
		}
		return result;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00055F50 File Offset: 0x00054150
	public CoverPoint GetCoverPositionAgainstTarget(Vector3 point, Vector3 target)
	{
		int num;
		int num2;
		this.PositionToCoverGridCoordinate(point, out num, out num2);
		Vector3 direction = target - point;
		if (num > 0 && num < this.coverGridDimensions && num2 > 0 && num2 < this.coverGridDimensions)
		{
			CoverPoint result = null;
			float num3 = 20f;
			foreach (CoverPoint coverPoint in this.coverGrid[num, num2])
			{
				if (!coverPoint.taken && coverPoint.CoversDirection(direction) && coverPoint.CoversPoint(target) && coverPoint.CanSee(target))
				{
					float num4 = Vector3.Distance(coverPoint.transform.position, point);
					if (num4 < num3)
					{
						num3 = num4;
						result = coverPoint;
					}
				}
			}
			return result;
		}
		return null;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00056034 File Offset: 0x00054234
	public CoverPoint GetCoverPositionAgainstDirection(Vector3 point, Vector3 direction)
	{
		int num;
		int num2;
		this.PositionToCoverGridCoordinate(point, out num, out num2);
		if (num > 0 && num < this.coverGridDimensions && num2 > 0 && num2 < this.coverGridDimensions)
		{
			CoverPoint result = null;
			float num3 = 20f;
			foreach (CoverPoint coverPoint in this.coverGrid[num, num2])
			{
				if (!coverPoint.taken && coverPoint.CoversDirection(direction))
				{
					float num4 = Vector3.Distance(coverPoint.transform.position, point);
					if (num4 < num3)
					{
						num3 = num4;
						result = coverPoint;
					}
				}
			}
			return result;
		}
		return null;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x000560F4 File Offset: 0x000542F4
	public CoverPoint ClosestVacantCoveringAwayFrom(Vector3 point)
	{
		CoverPoint result = null;
		float num = 20f;
		foreach (CoverPoint coverPoint in this.coverPoints)
		{
			Vector3 normalized = (coverPoint.transform.position - point).ToGround().normalized;
			if (!coverPoint.taken && coverPoint.CoversDirection(normalized))
			{
				float num2 = Vector3.Distance(coverPoint.transform.position, point);
				if (num2 < num)
				{
					num = num2;
					result = coverPoint;
				}
			}
		}
		return result;
	}

	// Token: 0x0400047C RID: 1148
	private const int MAX_COVER_GRID_DIMENSIONS = 32;

	// Token: 0x0400047D RID: 1149
	private const float IS_CQC_CELL_COVER_COUNT_PER_SQUARE_METER = 0.015f;

	// Token: 0x0400047E RID: 1150
	private const float MIN_COVER_CELL_DIMENSION_METERS = 15f;

	// Token: 0x0400047F RID: 1151
	private const float MAX_COVER_DISTANCE = 20f;

	// Token: 0x04000480 RID: 1152
	public static CoverManager instance;

	// Token: 0x04000481 RID: 1153
	[NonSerialized]
	public CoverPoint[] coverPoints;

	// Token: 0x04000482 RID: 1154
	private Vector2 coverGridOrigin;

	// Token: 0x04000483 RID: 1155
	private Vector2 coverGridCellSize;

	// Token: 0x04000484 RID: 1156
	private List<CoverPoint>[,] coverGrid;

	// Token: 0x04000485 RID: 1157
	private bool[,] isCqcCell;

	// Token: 0x04000486 RID: 1158
	private int coverGridDimensions = 32;
}
