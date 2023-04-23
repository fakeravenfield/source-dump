using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200059B RID: 1435
	[Serializable]
	public class GameObjectGrid : ObjectGrid<GameObject>
	{
		// Token: 0x06002555 RID: 9557 RVA: 0x00019D84 File Offset: 0x00017F84
		public override GameObject Construct()
		{
			return new GameObject();
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000EF734 File Offset: 0x000ED934
		public override void OnCreate(GameObject obj, Coord coord)
		{
			obj.name = "Chunk " + coord.x.ToString() + "," + coord.z.ToString();
			obj.transform.parent = this.parent;
			obj.transform.localPosition = this.CoordToPos(coord);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00019D8B File Offset: 0x00017F8B
		public override void OnMove(GameObject obj, Coord newCoord)
		{
			obj.transform.localPosition = this.CoordToPos(newCoord);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00019D9F File Offset: 0x00017F9F
		public override void OnRemove(GameObject obj)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000EF794 File Offset: 0x000ED994
		public Coord PosToCoord(Vector3 pos, bool ceil = false)
		{
			if (!ceil)
			{
				return new Coord(Mathf.FloorToInt(pos.x / this.objectSize.x), Mathf.FloorToInt(pos.z / this.objectSize.z));
			}
			return new Coord(Mathf.CeilToInt(pos.x / this.objectSize.x), Mathf.CeilToInt(pos.z / this.objectSize.z));
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00019DA7 File Offset: 0x00017FA7
		public Vector3 CoordToPos(Coord coord)
		{
			return new Vector3((float)coord.x * this.objectSize.x, 0f, (float)coord.z * this.objectSize.z);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00019DD9 File Offset: 0x00017FD9
		public Coord GetCoord(GameObject obj)
		{
			return this.PosToCoord(obj.transform.position + this.objectSize / 2f, false);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00019E02 File Offset: 0x00018002
		public Vector3 GetCenter(GameObject obj)
		{
			return obj.transform.position + this.objectSize / 2f;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00019E24 File Offset: 0x00018024
		public void Deploy(Vector3 pos, float range)
		{
			this.Deploy(new Vector3[]
			{
				pos
			}, range);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000EF80C File Offset: 0x000EDA0C
		public void Deploy(Vector3[] poses, float range)
		{
			bool flag = false;
			if (this.prevRects == null || this.prevRects.Length != poses.Length)
			{
				flag = true;
				this.prevRects = new CoordRect[poses.Length];
				this.currRects = new CoordRect[poses.Length];
				this.currCenters = new Coord[poses.Length];
			}
			for (int i = 0; i < poses.Length; i++)
			{
				Vector3 vector = poses[i];
				this.currCenters[i] = new Coord(Mathf.RoundToInt(vector.x / this.objectSize.x), Mathf.RoundToInt(vector.z / this.objectSize.z));
				Coord coord = new Coord(Mathf.FloorToInt((vector.x - range) / this.objectSize.x), Mathf.FloorToInt((vector.z - range) / this.objectSize.z));
				Coord c = new Coord(Mathf.FloorToInt((vector.x + range) / this.objectSize.x), Mathf.FloorToInt((vector.z + range) / this.objectSize.z)) + 1;
				this.currRects[i] = new CoordRect(coord, c - coord);
				if (this.currRects[i] != this.prevRects[i])
				{
					flag = true;
				}
				this.prevRects[i] = this.currRects[i];
			}
			if (flag)
			{
				Debug.Log("Redeploy");
				base.Deploy(this.currRects, this.currCenters, true);
			}
		}

		// Token: 0x040023E5 RID: 9189
		public Vector3 objectSize;

		// Token: 0x040023E6 RID: 9190
		public Transform parent;

		// Token: 0x040023E7 RID: 9191
		public CoordRect[] prevRects = new CoordRect[0];

		// Token: 0x040023E8 RID: 9192
		public CoordRect[] currRects = new CoordRect[0];

		// Token: 0x040023E9 RID: 9193
		public Coord[] currCenters = new Coord[0];
	}
}
