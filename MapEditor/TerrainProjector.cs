using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000607 RID: 1543
	[ExecuteInEditMode]
	public class TerrainProjector : MonoBehaviour
	{
		// Token: 0x06002785 RID: 10117 RVA: 0x000F8430 File Offset: 0x000F6630
		private void Start()
		{
			this.meshFilter = this.GetOrCreateComponent<MeshFilter>();
			this.meshRenderer = this.GetOrCreateComponent<MeshRenderer>();
			this.editorTerrain = MapEditor.instance.GetEditorTerrain();
			this.mesh = new Mesh();
			this.meshFilter.mesh = this.mesh;
			this.vertices = new List<Vector3>();
			this.triangles = new List<int>();
			this.uvs = new List<Vector2>();
			this.normals = new List<Vector3>();
			this.GeneratePlane();
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000F84B4 File Offset: 0x000F66B4
		private void Update()
		{
			if (this.refresh || this.lastPosition != base.transform.position || this.lastScale != base.transform.localScale)
			{
				this.UpdateMesh();
			}
			this.refresh = false;
			this.lastPosition = base.transform.position;
			this.lastScale = base.transform.localScale;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0001B479 File Offset: 0x00019679
		private void OnDrawGizmosSelected()
		{
			this.DrawBoundingBox();
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0001B481 File Offset: 0x00019681
		public void SetSize(float size)
		{
			base.transform.localScale = new Vector3(size, size, 1f);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0001B49A File Offset: 0x0001969A
		public void UpdateMeshAsync()
		{
			this.refresh = true;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000F8528 File Offset: 0x000F6728
		public void UpdateMesh()
		{
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					int index = j + i * 16;
					Vector3 vector = new Vector3((float)j / 15f - 0.5f, (float)i / 15f - 0.5f);
					Vector3 vector2 = Vector3.up;
					Vector3 vector3 = base.transform.TransformPoint(vector);
					Vector3 a = base.transform.TransformPoint(vector + Vector3.forward);
					RaycastHit raycastHit;
					if (MapEditorTerrain.RayCast(new Ray(vector3, (a - vector3).normalized), out raycastHit))
					{
						vector = base.transform.InverseTransformPoint(raycastHit.point);
						vector2 = base.transform.InverseTransformDirection(raycastHit.normal).normalized;
						vector += vector2 * 0.01f;
					}
					else
					{
						Vector3 vector4 = base.transform.InverseTransformPoint(this.editorTerrain.transform.position);
						vector.z = vector4.z;
					}
					this.vertices[index] = vector;
					this.normals[index] = vector2;
				}
			}
			this.mesh.SetVertices(this.vertices);
			this.mesh.SetNormals(this.normals);
			this.mesh.RecalculateBounds();
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000F868C File Offset: 0x000F688C
		private void GeneratePlane()
		{
			this.vertices.Clear();
			this.vertices.Capacity = 256;
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					this.vertices.Add(new Vector3((float)j, (float)i, 0f));
				}
			}
			this.triangles.Clear();
			this.triangles.Capacity = 1350;
			for (int k = 0; k < 15; k++)
			{
				for (int l = 0; l < 15; l++)
				{
					int num = l + k * 16;
					this.triangles.Add(num);
					this.triangles.Add(num + 16);
					this.triangles.Add(num + 1);
					this.triangles.Add(num + 1);
					this.triangles.Add(num + 16);
					this.triangles.Add(num + 16 + 1);
				}
			}
			this.uvs.Clear();
			this.uvs.Capacity = 256;
			for (int m = 0; m < 16; m++)
			{
				for (int n = 0; n < 16; n++)
				{
					this.uvs.Add(new Vector2((float)n / 15f, (float)m / 15f));
				}
			}
			this.normals.Clear();
			this.normals.Capacity = 256;
			for (int num2 = 0; num2 < 16; num2++)
			{
				for (int num3 = 0; num3 < 16; num3++)
				{
					this.normals.Add(new Vector3(0f, 1f, 0f));
				}
			}
			this.mesh.SetVertices(this.vertices);
			this.mesh.SetTriangles(this.triangles, 0);
			this.mesh.SetUVs(0, this.uvs);
			this.mesh.SetNormals(this.normals);
			this.mesh.RecalculateBounds();
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000F8890 File Offset: 0x000F6A90
		private void DrawBoundingBox()
		{
			float num = 100000f;
			Vector3 localScale = base.transform.localScale;
			localScale = new Vector3(localScale.x, 1f, localScale.y);
			Vector3 size = localScale + new Vector3(0f, num, 0f);
			Gizmos.DrawWireCube(base.transform.position - new Vector3(0f, num / 2f, 0f), size);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000F890C File Offset: 0x000F6B0C
		private void DrawVertices()
		{
			if (this.vertices != null)
			{
				for (int i = 0; i < this.vertices.Count; i++)
				{
					Vector3 vector = base.transform.TransformPoint(this.vertices[i]);
					Gizmos.color = Color.black;
					Gizmos.DrawSphere(vector, 0.1f);
					Gizmos.color = Color.white;
					Gizmos.DrawLine(base.transform.position, vector);
				}
				Gizmos.color = Color.white;
				for (int j = 0; j < this.triangles.Count; j += 3)
				{
					int index = this.triangles[j];
					int index2 = this.triangles[j + 1];
					int index3 = this.triangles[j + 2];
					Vector3 vector2 = base.transform.TransformPoint(this.vertices[index]);
					Vector3 vector3 = base.transform.TransformPoint(this.vertices[index2]);
					Vector3 vector4 = base.transform.TransformPoint(this.vertices[index3]);
					Gizmos.DrawLine(vector2, vector3);
					Gizmos.DrawLine(vector3, vector4);
					Gizmos.DrawLine(vector4, vector2);
				}
			}
		}

		// Token: 0x0400258C RID: 9612
		private const int SIZE = 16;

		// Token: 0x0400258D RID: 9613
		private const float TERRAIN_OFFSET = 0.01f;

		// Token: 0x0400258E RID: 9614
		private MeshFilter meshFilter;

		// Token: 0x0400258F RID: 9615
		private MeshRenderer meshRenderer;

		// Token: 0x04002590 RID: 9616
		private MapEditorTerrain editorTerrain;

		// Token: 0x04002591 RID: 9617
		private List<Vector3> vertices;

		// Token: 0x04002592 RID: 9618
		private List<int> triangles;

		// Token: 0x04002593 RID: 9619
		private List<Vector2> uvs;

		// Token: 0x04002594 RID: 9620
		private List<Vector3> normals;

		// Token: 0x04002595 RID: 9621
		private Mesh mesh;

		// Token: 0x04002596 RID: 9622
		private bool refresh;

		// Token: 0x04002597 RID: 9623
		private Vector3 lastPosition;

		// Token: 0x04002598 RID: 9624
		private Vector3 lastScale;
	}
}
