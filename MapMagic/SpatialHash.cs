using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020005AA RID: 1450
	[Serializable]
	public class SpatialHash : ICloneable
	{
		// Token: 0x0600257B RID: 9595 RVA: 0x000F10C0 File Offset: 0x000EF2C0
		public SpatialHash(Vector2 offset, float size, int resolution)
		{
			this.resolution = resolution;
			this.size = size;
			this.offset = offset;
			this.Count = 0;
			this.cells = new SpatialHash.Cell[resolution * resolution];
			float num = size / (float)resolution;
			for (int i = 0; i < resolution; i++)
			{
				for (int j = 0; j < resolution; j++)
				{
					SpatialHash.Cell cell = default(SpatialHash.Cell);
					cell.min = new Vector2((float)i * num, (float)j * num) + offset;
					cell.max = new Vector2((float)(i + 1) * num, (float)(j + 1) * num) + offset;
					cell.objs = new List<SpatialObject>();
					this.cells[j * resolution + i] = cell;
				}
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000F1178 File Offset: 0x000EF378
		public SpatialHash Copy()
		{
			SpatialHash spatialHash = new SpatialHash(this.offset, this.size, this.resolution);
			for (int i = 0; i < this.cells.Length; i++)
			{
				spatialHash.cells[i].min = this.cells[i].min;
				spatialHash.cells[i].max = this.cells[i].max;
				spatialHash.cells[i].objs = new List<SpatialObject>(this.cells[i].objs);
				List<SpatialObject> objs = spatialHash.cells[i].objs;
				for (int j = objs.Count - 1; j >= 0; j--)
				{
					objs[j] = objs[j].Copy();
				}
			}
			spatialHash.Count = this.Count;
			return spatialHash;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00019FAC File Offset: 0x000181AC
		public object Clone()
		{
			return this.Copy();
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000F1268 File Offset: 0x000EF468
		public SpatialHash.Cell GetCellByPoint(Vector2 point)
		{
			point -= this.offset;
			float num = this.size / (float)this.resolution;
			int num2 = (int)(point.x / num);
			int num3 = (int)(point.y / num);
			return this.cells[num3 * this.resolution + num2];
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000F12BC File Offset: 0x000EF4BC
		public void ChangeResolution(int newResolution)
		{
			SpatialHash spatialHash = new SpatialHash(this.offset, this.size, newResolution);
			foreach (SpatialObject obj in this.AllObjs())
			{
				spatialHash.Add(obj);
			}
			this.resolution = newResolution;
			this.cells = spatialHash.cells;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00019FB4 File Offset: 0x000181B4
		public IEnumerator GetEnumerator()
		{
			int num;
			for (int c = 0; c < this.cells.Length; c = num + 1)
			{
				List<SpatialObject> list = this.cells[c].objs;
				for (int i = list.Count - 1; i >= 0; i = num - 1)
				{
					yield return list[i];
					num = i;
				}
				list = null;
				num = c;
			}
			yield break;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00019FC3 File Offset: 0x000181C3
		public IEnumerable<SpatialObject> AllObjs()
		{
			int num;
			for (int c = 0; c < this.cells.Length; c = num + 1)
			{
				SpatialHash.Cell cell = this.cells[c];
				int objsCount = cell.objs.Count;
				for (int i = 0; i < objsCount; i = num + 1)
				{
					yield return cell.objs[i];
					num = i;
				}
				cell = default(SpatialHash.Cell);
				num = c;
			}
			yield break;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00019FD3 File Offset: 0x000181D3
		public IEnumerable<SpatialObject> ObjsInCell(int cellNum)
		{
			SpatialHash.Cell cell = this.cells[cellNum];
			int objsCount = cell.objs.Count;
			int num;
			for (int i = 0; i < objsCount; i = num + 1)
			{
				yield return cell.objs[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00019FEA File Offset: 0x000181EA
		public IEnumerable<SpatialObject> ObjsInCell(Vector2 point)
		{
			SpatialHash.Cell cell = this.GetCellByPoint(point);
			int objsCount = cell.objs.Count;
			int num;
			for (int i = 0; i < objsCount; i = num + 1)
			{
				yield return cell.objs[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0001A001 File Offset: 0x00018201
		public IEnumerable<int> CellNumsInRect(Vector2 min, Vector2 max, bool inCenter = true)
		{
			min -= this.offset;
			max -= this.offset;
			float num = this.size / (float)this.resolution;
			int minX = (int)(min.x / num);
			int minY = (int)(min.y / num);
			int maxX = (int)(max.x / num);
			int maxY = (int)(max.y / num);
			minX = Mathf.Max(0, minX);
			minY = Mathf.Max(0, minY);
			maxX = Mathf.Min(this.resolution - 1, maxX);
			maxY = Mathf.Min(this.resolution - 1, maxY);
			if (inCenter)
			{
				int num2;
				for (int x = minX; x <= maxX; x = num2 + 1)
				{
					for (int y = minY; y <= maxY; y = num2 + 1)
					{
						yield return y * this.resolution + x;
						num2 = y;
					}
					num2 = x;
				}
			}
			else
			{
				int num2;
				for (int x = minX; x <= maxX; x = num2 + 1)
				{
					yield return minY * this.resolution + x;
					yield return maxY * this.resolution + x;
					num2 = x;
				}
				for (int x = minY; x <= maxY; x = num2 + 1)
				{
					yield return x * this.resolution + minX;
					yield return x * this.resolution + maxX;
					num2 = x;
				}
			}
			yield break;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0001A026 File Offset: 0x00018226
		public IEnumerable<SpatialHash.Cell> CellsInRect(Vector2 min, Vector2 max)
		{
			foreach (int num in this.CellNumsInRect(min, max, true))
			{
				yield return this.cells[num];
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0001A044 File Offset: 0x00018244
		public IEnumerable<SpatialObject> ObjsInRect(Vector2 min, Vector2 max)
		{
			foreach (int num in this.CellNumsInRect(min, max, true))
			{
				List<SpatialObject> objs = this.cells[num].objs;
				int objsCount = objs.Count;
				int num2;
				for (int i = 0; i < objsCount; i = num2 + 1)
				{
					yield return objs[i];
					num2 = i;
				}
				objs = null;
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0001A062 File Offset: 0x00018262
		public IEnumerable<SpatialObject> ObjsInRange(Vector2 pos, float range)
		{
			Vector2 min = new Vector2(pos.x - range, pos.y - range);
			Vector2 max = new Vector2(pos.x + range, pos.y + range);
			foreach (int num in this.CellNumsInRect(min, max, true))
			{
				List<SpatialObject> objs = this.cells[num].objs;
				int objsCount = objs.Count;
				int num2;
				for (int i = 0; i < objsCount; i = num2 + 1)
				{
					if ((pos - objs[i].pos).sqrMagnitude < range * range)
					{
						yield return objs[i];
					}
					num2 = i;
				}
				objs = null;
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000F1330 File Offset: 0x000EF530
		public void RemoveObjsInRange(Vector2 pos, float range)
		{
			Vector2 min = new Vector2(pos.x - range, pos.y - range);
			Vector2 max = new Vector2(pos.x + range, pos.y + range);
			foreach (int num in this.CellNumsInRect(min, max, true))
			{
				List<SpatialObject> objs = this.cells[num].objs;
				for (int i = objs.Count - 1; i >= 0; i--)
				{
					if ((pos - objs[i].pos).sqrMagnitude < range * range)
					{
						objs.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000F1400 File Offset: 0x000EF600
		public bool IsAnyObjInRange(Vector2 pos, float range)
		{
			Vector2 min = new Vector2(pos.x - range, pos.y - range);
			Vector2 max = new Vector2(pos.x + range, pos.y + range);
			foreach (int num in this.CellNumsInRect(min, max, true))
			{
				List<SpatialObject> objs = this.cells[num].objs;
				int count = objs.Count;
				for (int i = 0; i < count; i++)
				{
					if ((pos - objs[i].pos).sqrMagnitude < range * range)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000F14D0 File Offset: 0x000EF6D0
		public void Add(SpatialObject obj, Vector2 pos, float extend)
		{
			Vector2 min = pos - new Vector2(extend, extend);
			Vector2 max = pos + new Vector2(extend, extend);
			foreach (int num in this.CellNumsInRect(min, max, true))
			{
				this.cells[num].objs.Add(obj);
			}
			this.Count++;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0001A080 File Offset: 0x00018280
		public void Add(SpatialObject obj, Vector2 pos)
		{
			this.GetCellByPoint(pos).objs.Add(obj);
			this.Count++;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000F155C File Offset: 0x000EF75C
		public void Add(Vector2 p, float h, float r, float s, int id = -1)
		{
			if (id == -1)
			{
				id = this.Count;
			}
			this.GetCellByPoint(p).objs.Add(new SpatialObject
			{
				pos = p,
				height = h,
				rotation = r,
				size = s,
				id = id
			});
			this.Count++;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x0001A0A2 File Offset: 0x000182A2
		public void Add(SpatialObject obj)
		{
			this.GetCellByPoint(obj.pos).objs.Add(obj);
			this.Count++;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000F15C0 File Offset: 0x000EF7C0
		public void Add(SpatialHash addHash)
		{
			if (addHash.cells.Length != this.cells.Length)
			{
				UnityEngine.Debug.LogError("Add SpatialHash: cell number is different");
				return;
			}
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i].objs.AddRange(addHash.cells[i].objs);
				this.Count += this.cells[i].objs.Count;
			}
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000F1648 File Offset: 0x000EF848
		public void Clear()
		{
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i].objs.Clear();
			}
			this.Count = 0;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0001A0C9 File Offset: 0x000182C9
		public void Remove(SpatialObject obj, Vector2 pos)
		{
			this.GetCellByPoint(pos).objs.Remove(obj);
			this.Count--;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0001A0EC File Offset: 0x000182EC
		public void Remove(SpatialObject obj)
		{
			this.GetCellByPoint(obj.pos).objs.Remove(obj);
			this.Count--;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000F1688 File Offset: 0x000EF888
		public SpatialObject Closest(Vector2 pos, float range, bool skipSelf = true)
		{
			float num = 20000000f;
			SpatialObject result = null;
			Vector2 min = new Vector2(pos.x - range, pos.y - range);
			Vector2 max = new Vector2(pos.x + range, pos.y + range);
			foreach (SpatialObject spatialObject in this.ObjsInRect(min, max))
			{
				float sqrMagnitude = (spatialObject.pos - pos).sqrMagnitude;
				if (sqrMagnitude < num && (!skipSelf || sqrMagnitude >= 1E-05f))
				{
					num = Mathf.Min(sqrMagnitude, num);
					result = spatialObject;
				}
			}
			return result;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000F1744 File Offset: 0x000EF944
		public SpatialObject Closest(Vector2 pos, bool skipSelf = true)
		{
			float num = 20000000f;
			SpatialObject spatialObject = null;
			float num2 = this.size / (float)this.resolution;
			float num3 = 0.0001f;
			float num4 = this.size * 1.415f;
			bool flag = false;
			while (num3 <= num4)
			{
				Vector2 min = new Vector2(pos.x - num3, pos.y - num3);
				Vector2 max = new Vector2(pos.x + num3, pos.y + num3);
				foreach (int num5 in this.CellNumsInRect(min, max, false))
				{
					SpatialHash.Cell cell = this.cells[num5];
					int count = cell.objs.Count;
					for (int i = 0; i < count; i++)
					{
						SpatialObject spatialObject2 = cell.objs[i];
						float sqrMagnitude = (spatialObject2.pos - pos).sqrMagnitude;
						if (sqrMagnitude < num && (!skipSelf || sqrMagnitude >= 1E-05f))
						{
							num = sqrMagnitude;
							spatialObject = spatialObject2;
						}
					}
				}
				num3 += num2;
				if (flag)
				{
					break;
				}
				if (spatialObject != null)
				{
					flag = true;
				}
			}
			return spatialObject;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000F187C File Offset: 0x000EFA7C
		public float MinDist(Vector2 p, bool skipSelf = true)
		{
			SpatialObject spatialObject = this.Closest(p, skipSelf);
			if (spatialObject == null)
			{
				return 20000000f;
			}
			return (p - spatialObject.pos).magnitude;
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000F18B0 File Offset: 0x000EFAB0
		public SpatialObject Closest_outdated(Vector2 p)
		{
			float num = 20000000f;
			SpatialObject result = null;
			foreach (object obj in this)
			{
				SpatialObject spatialObject = (SpatialObject)obj;
				float sqrMagnitude = (spatialObject.pos - p).sqrMagnitude;
				if (sqrMagnitude >= 1E-05f && sqrMagnitude < num)
				{
					num = Mathf.Min(sqrMagnitude, num);
					result = spatialObject;
				}
			}
			return result;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0000296E File Offset: 0x00000B6E
		public void Debug()
		{
		}

		// Token: 0x0400241D RID: 9245
		public SpatialHash.Cell[] cells;

		// Token: 0x0400241E RID: 9246
		public Vector2 offset;

		// Token: 0x0400241F RID: 9247
		public float size;

		// Token: 0x04002420 RID: 9248
		public int resolution;

		// Token: 0x04002421 RID: 9249
		public int Count;

		// Token: 0x020005AB RID: 1451
		[Serializable]
		public struct Cell
		{
			// Token: 0x04002422 RID: 9250
			public List<SpatialObject> objs;

			// Token: 0x04002423 RID: 9251
			public Vector2 min;

			// Token: 0x04002424 RID: 9252
			public Vector2 max;
		}
	}
}
