using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000352 RID: 850
public static class StairwaysExtensions
{
	// Token: 0x0600159C RID: 5532 RVA: 0x0009C3FC File Offset: 0x0009A5FC
	public static bool IsBuilt(this Stairways self)
	{
		MeshFilter component = self.GetComponent<MeshFilter>();
		return component && component.mesh && component.mesh.vertexCount > 0;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x0009C438 File Offset: 0x0009A638
	public static void Build(this Stairways self)
	{
		float y = self.target.y;
		Vector3 target = self.target;
		target.y = 0f;
		float magnitude = target.magnitude;
		target.Normalize();
		int num = Mathf.RoundToInt(Mathf.Abs(y) / self.targetStepHeight);
		Vector3 right = Vector3.Cross(target, Vector3.up).normalized * self.width / 2f;
		float stepHeight = y / (float)num;
		float stepDepth = magnitude / (float)num;
		Mesh mesh = StairwaysExtensions.BuildMesh(self, num, stepHeight, stepDepth, target, right);
		self.GetComponent<MeshFilter>().mesh = mesh;
		self.GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x0009C4E4 File Offset: 0x0009A6E4
	private static Mesh BuildMesh(Stairways stairway, int steps, float stepHeight, float stepDepth, Vector3 forward, Vector3 right)
	{
		Mesh mesh = new Mesh();
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		List<Vector2> list3 = new List<Vector2>();
		int num = 0;
		Vector3 vector = right.normalized * stairway.edgeWidth;
		for (int i = 0; i < steps; i++)
		{
			if (i == 0)
			{
				Vector3 b = right + (stairway.rightEdge ? vector : Vector3.zero);
				Vector3 b2 = -right - (stairway.leftEdge ? vector : Vector3.zero);
				list.Add((float)i * stepDepth * forward + (float)i * stepHeight * Vector3.up + b2);
				list.Add((float)i * stepDepth * forward + (float)i * stepHeight * Vector3.up + b);
				list.Add((float)(i + 1) * stepDepth * forward + (float)i * stepHeight * Vector3.up + b);
				list.Add((float)(i + 1) * stepDepth * forward + (float)i * stepHeight * Vector3.up + b2);
				float x = stairway.leftEdge ? (-stairway.edgeWidth) : 0f;
				float num2 = stairway.rightEdge ? stairway.edgeWidth : 0f;
				list3.Add(new Vector2(x, (float)i * stepDepth));
				list3.Add(new Vector2(stairway.width + num2, (float)i * stepDepth));
				list3.Add(new Vector2(stairway.width + num2, (float)(i + 1) * stepDepth));
				list3.Add(new Vector2(x, (float)(i + 1) * stepDepth));
			}
			else
			{
				list.Add((float)i * stepDepth * forward + (float)i * stepHeight * Vector3.up - right);
				list.Add((float)i * stepDepth * forward + (float)i * stepHeight * Vector3.up + right);
				list.Add((float)(i + 1) * stepDepth * forward + (float)i * stepHeight * Vector3.up + right);
				list.Add((float)(i + 1) * stepDepth * forward + (float)i * stepHeight * Vector3.up - right);
				list3.Add(new Vector2(0f, (float)i * stepDepth));
				list3.Add(new Vector2(stairway.width, (float)i * stepDepth));
				list3.Add(new Vector2(stairway.width, (float)(i + 1) * stepDepth));
				list3.Add(new Vector2(0f, (float)(i + 1) * stepDepth));
			}
			list.Add((float)(i + 1) * stepDepth * forward + (float)i * stepHeight * Vector3.up - right);
			list.Add((float)(i + 1) * stepDepth * forward + (float)i * stepHeight * Vector3.up + right);
			list.Add((float)(i + 1) * stepDepth * forward + (float)(i + 1) * stepHeight * Vector3.up + right);
			list.Add((float)(i + 1) * stepDepth * forward + (float)(i + 1) * stepHeight * Vector3.up - right);
			StairwaysExtensions.PushQuad(list2, ref num);
			StairwaysExtensions.PushQuad(list2, ref num);
			list3.Add(new Vector2(0f, (float)i * stepHeight));
			list3.Add(new Vector2(stairway.width, (float)i * stepHeight));
			list3.Add(new Vector2(stairway.width, (float)(i + 1) * stepHeight));
			list3.Add(new Vector2(0f, (float)(i + 1) * stepHeight));
		}
		if (stairway.leftEdge)
		{
			list.Add(stepDepth * forward - right);
			list.Add(-stairway.depth * stepDepth * forward - right);
			list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right);
			list.Add(stepDepth * forward - right - vector);
			list.Add(stepDepth * forward - right);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right - vector);
			list.Add(-stairway.depth * stepDepth * forward - right - vector);
			list.Add(stepDepth * forward - right - vector);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right - vector);
			list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right - vector);
			StairwaysExtensions.PushQuad(list2, ref num);
			StairwaysExtensions.PushQuad(list2, ref num);
			StairwaysExtensions.PushQuad(list2, ref num);
			list3.Add(new Vector2(stepDepth, 0f));
			list3.Add(new Vector2(-stairway.depth * stepDepth, 0f));
			list3.Add(new Vector2(((float)steps - stairway.depth) * stepDepth, (float)steps * stepHeight));
			list3.Add(new Vector2((float)(steps + 1) * stepDepth, (float)steps * stepHeight));
			list3.Add(new Vector2(stairway.edgeWidth, 0f));
			list3.Add(new Vector2(0f, 0f));
			list3.Add(new Vector2(0f, (float)(steps + 1) * stepDepth));
			list3.Add(new Vector2(stairway.edgeWidth, (float)(steps + 1) * stepDepth));
			list3.Add(new Vector2(-stairway.depth * stepDepth, 0f));
			list3.Add(new Vector2(stepDepth, 0f));
			list3.Add(new Vector2((float)(steps + 1) * stepDepth, (float)steps * stepHeight));
			list3.Add(new Vector2(((float)steps - stairway.depth) * stepDepth, (float)steps * stepHeight));
		}
		if (stairway.rightEdge)
		{
			list.Add(-stairway.depth * stepDepth * forward + right);
			list.Add(stepDepth * forward + right);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right);
			list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right);
			list.Add(stepDepth * forward + right);
			list.Add(stepDepth * forward + right + vector);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right + vector);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right);
			list.Add(stepDepth * forward + right + vector);
			list.Add(-stairway.depth * stepDepth * forward + right + vector);
			list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right + vector);
			list.Add((float)(steps + 1) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right + vector);
			StairwaysExtensions.PushQuad(list2, ref num);
			StairwaysExtensions.PushQuad(list2, ref num);
			StairwaysExtensions.PushQuad(list2, ref num);
			list3.Add(new Vector2(-stairway.depth * stepDepth, 0f));
			list3.Add(new Vector2(stepDepth, 0f));
			list3.Add(new Vector2((float)(steps + 1) * stepDepth, (float)steps * stepHeight));
			list3.Add(new Vector2(((float)steps - stairway.depth) * stepDepth, (float)steps * stepHeight));
			list3.Add(new Vector2(0f, 0f));
			list3.Add(new Vector2(stairway.edgeWidth, 0f));
			list3.Add(new Vector2(stairway.edgeWidth, (float)(steps + 1) * stepDepth));
			list3.Add(new Vector2(0f, (float)(steps + 1) * stepDepth));
			list3.Add(new Vector2(stepDepth, 0f));
			list3.Add(new Vector2(-stairway.depth * stepDepth, 0f));
			list3.Add(new Vector2(((float)steps - stairway.depth) * stepDepth, (float)steps * stepHeight));
			list3.Add(new Vector2((float)(steps + 1) * stepDepth, (float)steps * stepHeight));
		}
		if (stairway.leftEdge || stairway.rightEdge)
		{
			if (stairway.leftEdge)
			{
				list.Add(-stairway.depth * stepDepth * forward - right - vector);
				list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right - vector);
			}
			else
			{
				list.Add(-stairway.depth * stepDepth * forward - right);
				list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up - right);
			}
			if (stairway.rightEdge)
			{
				list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right + vector);
				list.Add(-stairway.depth * stepDepth * forward + right + vector);
			}
			else
			{
				list.Add(((float)steps - stairway.depth) * stepDepth * forward + (float)steps * stepHeight * Vector3.up + right);
				list.Add(-stairway.depth * stepDepth * forward + right);
			}
			StairwaysExtensions.PushQuad(list2, ref num);
			list3.Add(new Vector2(0f, 0f));
			list3.Add(new Vector2(0f, (float)steps * stepHeight));
			list3.Add(new Vector2(stairway.width, (float)steps * stepHeight));
			list3.Add(new Vector2(stairway.width, 0f));
		}
		mesh.vertices = list.ToArray();
		mesh.triangles = list2.ToArray();
		mesh.uv = list3.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.name = "Staircase";
		return mesh;
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000112A4 File Offset: 0x0000F4A4
	private static void PushQuad(List<int> triangles, ref int vertexBase)
	{
		triangles.Add(vertexBase);
		triangles.Add(vertexBase + 1);
		triangles.Add(vertexBase + 2);
		triangles.Add(vertexBase);
		triangles.Add(vertexBase + 2);
		triangles.Add(vertexBase + 3);
		vertexBase += 4;
	}
}
