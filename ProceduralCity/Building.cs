using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralCity
{
	// Token: 0x02000365 RID: 869
	public class Building : ProceduralObject
	{
		// Token: 0x0600162B RID: 5675 RVA: 0x0009FBEC File Offset: 0x0009DDEC
		public override void Generate()
		{
			ProceduralMesh proceduralMesh = new ProceduralMesh(1);
			this.GenerateFacade(proceduralMesh);
			this.ApplyHoles(proceduralMesh);
			this.ApplyMesh(proceduralMesh.BakeMesh(true), new Material[]
			{
				this.wallMaterial
			});
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0009FC2C File Offset: 0x0009DE2C
		private void GenerateFacade(ProceduralMesh procedural)
		{
			bool flag = this.ShouldFlipWallQuads();
			for (int i = 0; i < this.vertices.Count; i++)
			{
				Vector2 vector = this.vertices[i];
				Vector2 vector2 = this.vertices[(i + 1) % this.vertices.Count];
				Vector3 vector3 = new Vector3(vector.x, 0f, vector.y);
				Vector3 vector4 = new Vector3(vector2.x, 0f, vector2.y);
				Vector3 vector5 = vector3;
				Vector3 vector6 = vector4;
				vector5.y = this.floorHeight * (float)this.floors;
				vector6.y = this.floorHeight * (float)this.floors;
				procedural.SetUVSpace(vector3, vector3 - vector4, Vector3.up, Vector2.one);
				if (!flag)
				{
					procedural.AddQuad(vector3, vector4, vector6, vector5, 0);
				}
				else
				{
					procedural.AddQuad(vector4, vector3, vector5, vector6, 0);
				}
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0009FD24 File Offset: 0x0009DF24
		private void ApplyHoles(ProceduralMesh procedural)
		{
			using (List<Building.Hole>.Enumerator enumerator = this.holes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Building.Hole hole = enumerator.Current;
					Debug.Log(procedural.QueryTriangles((ProceduralMesh.TriangleVertIndices triangle) => this.CoarseTriangleIsInHole(triangle, hole)).Count);
				}
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0000476F File Offset: 0x0000296F
		private bool CoarseTriangleIsInHole(ProceduralMesh.TriangleVertIndices triangle, Building.Hole hole)
		{
			return true;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0009FDA4 File Offset: 0x0009DFA4
		private bool ShouldFlipWallQuads()
		{
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < this.vertices.Count; i++)
			{
				vector += new Vector3(this.vertices[i].x, 0f, this.vertices[i].y);
			}
			vector /= (float)this.vertices.Count;
			float num = 0f;
			for (int j = 0; j < this.vertices.Count; j++)
			{
				Vector2 vector2 = this.vertices[j];
				Vector2 vector3 = this.vertices[(j + 1) % this.vertices.Count];
				Vector3 vector4 = new Vector3(vector2.x, 0f, vector2.y);
				Vector3 rhs = new Vector3(vector3.x, 0f, vector3.y) - vector4;
				Vector3 lhs = vector4 - vector;
				num += Vector3.Cross(lhs, rhs).y;
			}
			return num < 0f;
		}

		// Token: 0x04001892 RID: 6290
		public float floorHeight = 2.5f;

		// Token: 0x04001893 RID: 6291
		public int floors = 3;

		// Token: 0x04001894 RID: 6292
		public List<Vector2> vertices;

		// Token: 0x04001895 RID: 6293
		public Material wallMaterial;

		// Token: 0x04001896 RID: 6294
		public List<Building.Hole> holes;

		// Token: 0x02000366 RID: 870
		[Serializable]
		public struct Hole
		{
			// Token: 0x04001897 RID: 6295
			public Vector3 center;

			// Token: 0x04001898 RID: 6296
			public Vector3 scale;

			// Token: 0x04001899 RID: 6297
			public Vector3 rotation;
		}
	}
}
