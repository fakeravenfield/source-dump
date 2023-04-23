using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MapEditor
{
	// Token: 0x02000620 RID: 1568
	public class CircleRenderer : MonoBehaviour, IAllowInPrefabRenderer
	{
		// Token: 0x0600284B RID: 10315 RVA: 0x000FA090 File Offset: 0x000F8290
		private void Awake()
		{
			this.circleMesh = new Mesh();
			this.triangles = new List<int>();
			this.vertices = new List<Vector3>();
			this.materialInstance = UnityEngine.Object.Instantiate<Material>(this.material);
			this.meshFilter = this.GetOrCreateComponent<MeshFilter>();
			this.meshFilter.sharedMesh = this.circleMesh;
			this.meshRenderer = this.GetOrCreateComponent<MeshRenderer>();
			this.meshRenderer.enabled = false;
			this.meshRenderer.sharedMaterial = this.materialInstance;
			this.meshRenderer.receiveShadows = false;
			this.meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			this.meshRenderer.lightProbeUsage = LightProbeUsage.Off;
			this.meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
			this.lineRenderer = this.GetOrCreateComponent<LineRenderer>();
			this.lineRenderer.enabled = false;
			this.lineRenderer.useWorldSpace = false;
			this.lineRenderer.loop = true;
			this.lineRenderer.sharedMaterial = this.materialInstance;
			this.lineRenderer.receiveShadows = false;
			this.lineRenderer.shadowCastingMode = ShadowCastingMode.Off;
			this.lineRenderer.lightProbeUsage = LightProbeUsage.Off;
			this.lineRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0001BCC7 File Offset: 0x00019EC7
		private void OnEnable()
		{
			this.Draw();
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0001BCCF File Offset: 0x00019ECF
		private void Update()
		{
			if (this.GetHashCode() != this.hashCode)
			{
				this.Draw();
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000FA1B8 File Offset: 0x000F83B8
		private void Draw()
		{
			this.meshRenderer.enabled = false;
			this.lineRenderer.enabled = false;
			this.materialInstance.color = this.color;
			if (this.isFilled)
			{
				this.GenerateCircleMesh();
			}
			else
			{
				this.GenerateCircleLine();
			}
			this.hashCode = this.GetHashCode();
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000FA210 File Offset: 0x000F8410
		private static Vector3[] PointsOnCircle(float radius, float sliceAngle, int pointsPerRevolution)
		{
			float num = Mathf.Sign(sliceAngle);
			if (Mathf.Abs(sliceAngle) > 360f)
			{
				sliceAngle -= Mathf.Floor(sliceAngle / 360f) * 360f;
			}
			sliceAngle = Mathf.Abs(sliceAngle);
			sliceAngle = Mathf.Clamp(sliceAngle, 0f, 360f) / 360f;
			int num2 = Mathf.RoundToInt(sliceAngle * (float)pointsPerRevolution);
			float num3 = num * 3.1415927f * 2f / (float)(pointsPerRevolution - 1);
			Vector3[] array = new Vector3[num2];
			for (int i = 0; i < num2; i++)
			{
				float f = num3 * (float)i;
				float x = Mathf.Cos(f) * radius;
				float z = Mathf.Sin(f) * radius;
				array[i] = new Vector3(x, 0f, z);
			}
			return array;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000FA2C4 File Offset: 0x000F84C4
		private void GenerateCircleLine()
		{
			Vector3[] array = CircleRenderer.PointsOnCircle(this.radius, this.sliceAngle, this.pointsPerRevolution);
			this.lineRenderer.loop = (this.sliceAngle >= 360f);
			this.lineRenderer.positionCount = array.Length;
			this.lineRenderer.SetPositions(array);
			this.lineRenderer.enabled = true;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000FA32C File Offset: 0x000F852C
		private void GenerateCircleMesh()
		{
			Vector3[] array = CircleRenderer.PointsOnCircle(this.radius, this.sliceAngle, this.pointsPerRevolution);
			this.vertices.Clear();
			this.vertices.Add(Vector3.zero);
			this.vertices.AddRange(array);
			int num = (this.sliceAngle >= 360f) ? 1 : 0;
			this.triangles.Clear();
			for (int i = 1; i < array.Length + num; i++)
			{
				this.triangles.Add(0);
				this.triangles.Add(i % array.Length + 1);
				this.triangles.Add(i);
			}
			this.circleMesh.Clear();
			this.circleMesh.SetVertices(this.vertices);
			this.circleMesh.SetTriangles(this.triangles, 0);
			this.circleMesh.RecalculateBounds();
			this.circleMesh.RecalculateNormals();
			this.meshRenderer.enabled = true;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000FA420 File Offset: 0x000F8620
		public override int GetHashCode()
		{
			return ((((((-370766270 * -1521134295 + base.GetHashCode()) * -1521134295 + this.pointsPerRevolution.GetHashCode()) * -1521134295 + this.radius.GetHashCode()) * -1521134295 + this.isFilled.GetHashCode()) * -1521134295 + this.sliceAngle.GetHashCode()) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.color)) * -1521134295 + EqualityComparer<Material>.Default.GetHashCode(this.material);
		}

		// Token: 0x04002655 RID: 9813
		[Range(8f, 360f)]
		public int pointsPerRevolution = 64;

		// Token: 0x04002656 RID: 9814
		public float radius = 1f;

		// Token: 0x04002657 RID: 9815
		[Range(-360f, 360f)]
		public float sliceAngle = 360f;

		// Token: 0x04002658 RID: 9816
		public bool isFilled;

		// Token: 0x04002659 RID: 9817
		public Color color = Color.white;

		// Token: 0x0400265A RID: 9818
		public Material material;

		// Token: 0x0400265B RID: 9819
		private int hashCode;

		// Token: 0x0400265C RID: 9820
		private Mesh circleMesh;

		// Token: 0x0400265D RID: 9821
		private List<int> triangles;

		// Token: 0x0400265E RID: 9822
		private List<Vector3> vertices;

		// Token: 0x0400265F RID: 9823
		private Material materialInstance;

		// Token: 0x04002660 RID: 9824
		private MeshFilter meshFilter;

		// Token: 0x04002661 RID: 9825
		private LineRenderer lineRenderer;

		// Token: 0x04002662 RID: 9826
		private MeshRenderer meshRenderer;
	}
}
