using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class IngameDebugGizmos : MonoBehaviour
{
	// Token: 0x06000884 RID: 2180 RVA: 0x000078BA File Offset: 0x00005ABA
	public static bool IsEnabled()
	{
		return IngameDebugGizmos.instance != null && IngameDebugGizmos.instance.enabled;
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x000078D5 File Offset: 0x00005AD5
	public static IngameDebugGizmos.Gizmo DrawRay(Vector3 start, Vector3 direction, Color color, float duration = 0f)
	{
		IngameDebugGizmos.Gizmo gizmo = new IngameDebugGizmos.Gizmo(start, start + direction, color, duration);
		IngameDebugGizmos.PushGizmo(gizmo);
		return gizmo;
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000078EC File Offset: 0x00005AEC
	public static IngameDebugGizmos.Gizmo DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0f)
	{
		IngameDebugGizmos.Gizmo gizmo = new IngameDebugGizmos.Gizmo(start, end, color, duration);
		IngameDebugGizmos.PushGizmo(gizmo);
		return gizmo;
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x000078FD File Offset: 0x00005AFD
	public static IngameDebugGizmos.Gizmo DrawPath(Vector3[] vertices, Color color, float duration = 0f)
	{
		IngameDebugGizmos.Gizmo gizmo = new IngameDebugGizmos.Gizmo(vertices, color, duration);
		IngameDebugGizmos.PushGizmo(gizmo);
		return gizmo;
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0000790D File Offset: 0x00005B0D
	private static void PushGizmo(IngameDebugGizmos.Gizmo gizmo)
	{
		IngameDebugGizmos.instance.gizmos.Add(gizmo);
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0000791F File Offset: 0x00005B1F
	private void Awake()
	{
		IngameDebugGizmos.instance = this;
		this.gizmos = new List<IngameDebugGizmos.Gizmo>(1024);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00007937 File Offset: 0x00005B37
	private void Update()
	{
		this.gizmos.RemoveAll((IngameDebugGizmos.Gizmo g) => g.Expired());
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00007964 File Offset: 0x00005B64
	private void OnRenderObject()
	{
		if ((Camera.current.cullingMask & 1) == 0)
		{
			return;
		}
		this.gizmos.ForEach(delegate(IngameDebugGizmos.Gizmo g)
		{
			g.Draw(this.material);
		});
		GL.Flush();
	}

	// Token: 0x04000926 RID: 2342
	public static IngameDebugGizmos instance;

	// Token: 0x04000927 RID: 2343
	public Material material;

	// Token: 0x04000928 RID: 2344
	private List<IngameDebugGizmos.Gizmo> gizmos;

	// Token: 0x02000125 RID: 293
	public class Gizmo
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0000799F File Offset: 0x00005B9F
		public bool Expired()
		{
			return this.lifetimeAction.TrueDone();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000079AC File Offset: 0x00005BAC
		public void Destroy()
		{
			this.lifetimeAction.Stop();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000079B9 File Offset: 0x00005BB9
		public Gizmo(Vector3 start, Vector3 end, Color color, float duration) : this(new Vector3[]
		{
			start,
			end
		}, color, duration)
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000079DA File Offset: 0x00005BDA
		public Gizmo(Vector3[] vertices, Color color, float duration)
		{
			this.vertices = vertices;
			this.color = color;
			this.localToWorldMatrix = Matrix4x4.identity;
			this.lifetimeAction = new TimedAction(duration, false);
			this.lifetimeAction.Start();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00068910 File Offset: 0x00066B10
		public void Draw(Material material)
		{
			GL.PushMatrix();
			GL.MultMatrix(this.localToWorldMatrix);
			for (int i = 0; i < 2; i++)
			{
				material.SetPass(i);
				GL.Begin(2);
				GL.Color(this.color);
				for (int j = 0; j < this.vertices.Length; j++)
				{
					GL.Vertex(this.vertices[j]);
				}
				GL.End();
			}
			GL.PopMatrix();
		}

		// Token: 0x04000929 RID: 2345
		private Vector3[] vertices;

		// Token: 0x0400092A RID: 2346
		public Matrix4x4 localToWorldMatrix;

		// Token: 0x0400092B RID: 2347
		private Color color;

		// Token: 0x0400092C RID: 2348
		private TimedAction lifetimeAction;
	}
}
