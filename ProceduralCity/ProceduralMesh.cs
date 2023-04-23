using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralCity
{
	// Token: 0x02000368 RID: 872
	public class ProceduralMesh
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000117EB File Offset: 0x0000F9EB
		public int submeshCount
		{
			get
			{
				return this._submeshCount;
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000117F3 File Offset: 0x0000F9F3
		public ProceduralMesh(int submeshCount = 1)
		{
			this._submeshCount = submeshCount;
			this.submeshTriangles = new List<int>[this.submeshCount];
			this.Clear();
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0009FEC0 File Offset: 0x0009E0C0
		public void Clear()
		{
			this.vertices = new List<Vector3>();
			this.uvs = new List<Vector2>();
			for (int i = 0; i < this.submeshCount; i++)
			{
				this.submeshTriangles[i] = new List<int>();
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0009FF04 File Offset: 0x0009E104
		public List<ProceduralMesh.ManagedTriangleQueryResult> QueryTriangles(ProceduralMesh.DelMatchTriangle match)
		{
			List<ProceduralMesh.ManagedTriangleQueryResult> list = new List<ProceduralMesh.ManagedTriangleQueryResult>();
			for (int i = 0; i < this.submeshCount; i++)
			{
				List<int> list2 = this.submeshTriangles[i];
				for (int j = 0; j < list2.Count; j += 3)
				{
					if (match(new ProceduralMesh.TriangleVertIndices(list2[j], list2[j + 1], list2[j + 2])))
					{
						list.Add(new ProceduralMesh.ManagedTriangleQueryResult(i, j));
					}
				}
			}
			this.lastQueryResult = list.ToArray();
			return list;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0009FF84 File Offset: 0x0009E184
		public Mesh BakeMesh(bool calculateRenderData = true)
		{
			Mesh mesh = new Mesh();
			mesh.name = "ProceduralMesh";
			mesh.SetVertices(this.vertices);
			mesh.SetUVs(0, this.uvs);
			for (int i = 0; i < this.submeshCount; i++)
			{
				mesh.SetTriangles(this.submeshTriangles[i], i);
			}
			if (calculateRenderData)
			{
				mesh.RecalculateNormals();
				mesh.RecalculateBounds();
				mesh.RecalculateTangents();
			}
			return mesh;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0009FFF4 File Offset: 0x0009E1F4
		public void SetUVSpace(Vector3 origin, Vector3 xAxis, Vector3 yAxis, Vector2 uvScale)
		{
			Vector3 forward = Vector3.Cross(xAxis, yAxis);
			this.SetUVSpace(Matrix4x4.TRS(origin, Quaternion.LookRotation(forward, yAxis), new Vector3(uvScale.x, uvScale.y, 1f)).inverse);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00011824 File Offset: 0x0000FA24
		public void SetUVSpace(Matrix4x4 uvMatrix)
		{
			this.uvMatrix = uvMatrix;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000A003C File Offset: 0x0009E23C
		public void CullTriangle(int triangleBaseIndex, int submesh = 0)
		{
			foreach (ProceduralMesh.ManagedTriangleQueryResult managedTriangleQueryResult in this.lastQueryResult)
			{
				if (managedTriangleQueryResult.submesh == submesh && managedTriangleQueryResult.triangleBaseIndex > triangleBaseIndex)
				{
					managedTriangleQueryResult.triangleBaseIndex -= 3;
				}
			}
			this.submeshTriangles[submesh].RemoveRange(triangleBaseIndex, 3);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0001182D File Offset: 0x0000FA2D
		public void AddQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int submesh = 0)
		{
			this.AddTriangle(a, b, c, submesh);
			this.AddTriangle(c, d, a, submesh);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000A0094 File Offset: 0x0009E294
		public void AddTriangle(Vector3 a, Vector3 b, Vector3 c, int submesh = 0)
		{
			this.submeshTriangles[submesh].Add(this.vertices.Count);
			this.submeshTriangles[submesh].Add(this.vertices.Count + 1);
			this.submeshTriangles[submesh].Add(this.vertices.Count + 2);
			this.vertices.Add(a);
			this.vertices.Add(b);
			this.vertices.Add(c);
			this.uvs.Add(this.uvMatrix.MultiplyPoint(a));
			this.uvs.Add(this.uvMatrix.MultiplyPoint(b));
			this.uvs.Add(this.uvMatrix.MultiplyPoint(c));
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00011846 File Offset: 0x0000FA46
		public ProceduralMesh.TriangleVertIndices GetTriangleVertIndices(ProceduralMesh.ManagedTriangleQueryResult result)
		{
			return this.GetTriangleVertIndices(result.triangleBaseIndex, result.submesh);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0001185A File Offset: 0x0000FA5A
		public ProceduralMesh.TriangleVertIndices GetTriangleVertIndices(int triangleBaseIndex, int submesh = 0)
		{
			return new ProceduralMesh.TriangleVertIndices(this.submeshTriangles[submesh][triangleBaseIndex], this.submeshTriangles[submesh][triangleBaseIndex + 1], this.submeshTriangles[submesh][triangleBaseIndex + 2]);
		}

		// Token: 0x0400189C RID: 6300
		public List<Vector3> vertices;

		// Token: 0x0400189D RID: 6301
		public List<Vector2> uvs;

		// Token: 0x0400189E RID: 6302
		public List<int>[] submeshTriangles;

		// Token: 0x0400189F RID: 6303
		private Matrix4x4 uvMatrix = Matrix4x4.identity;

		// Token: 0x040018A0 RID: 6304
		private ProceduralMesh.ManagedTriangleQueryResult[] lastQueryResult;

		// Token: 0x040018A1 RID: 6305
		private int _submeshCount;

		// Token: 0x02000369 RID: 873
		// (Invoke) Token: 0x06001640 RID: 5696
		public delegate bool DelMatchTriangle(ProceduralMesh.TriangleVertIndices triangle);

		// Token: 0x0200036A RID: 874
		public class ManagedTriangleQueryResult
		{
			// Token: 0x06001643 RID: 5699 RVA: 0x0001188F File Offset: 0x0000FA8F
			public ManagedTriangleQueryResult(int submesh, int baseIndex)
			{
				this.submesh = submesh;
				this.triangleBaseIndex = baseIndex;
			}

			// Token: 0x040018A2 RID: 6306
			public int submesh;

			// Token: 0x040018A3 RID: 6307
			public int triangleBaseIndex;
		}

		// Token: 0x0200036B RID: 875
		public struct TriangleVertIndices
		{
			// Token: 0x06001644 RID: 5700 RVA: 0x000118A5 File Offset: 0x0000FAA5
			public TriangleVertIndices(int a, int b, int c)
			{
				this.a = a;
				this.b = b;
				this.c = c;
			}

			// Token: 0x040018A4 RID: 6308
			private int a;

			// Token: 0x040018A5 RID: 6309
			private int b;

			// Token: 0x040018A6 RID: 6310
			private int c;
		}
	}
}
