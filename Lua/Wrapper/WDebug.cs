using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000968 RID: 2408
	[Name("Debug")]
	[Doc("Contains helper functions that help you debug your code. Please note that for performance reasons, drawing rays and lines is only supported when launching the game in test mode.")]
	public static class WDebug
	{
		// Token: 0x06003D70 RID: 15728 RVA: 0x000298D9 File Offset: 0x00027AD9
		public static void DrawLine(Vector3 from, Vector3 to, Color color)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawLine(from, to, color, 0f);
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000298F1 File Offset: 0x00027AF1
		public static void DrawLine(Vector3 from, Vector3 to, Color color, float duration)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawLine(from, to, color, duration);
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x00029905 File Offset: 0x00027B05
		public static void DrawLine(Vector3 from, Vector3 to, Color color, float duration, Matrix4x4 localToWorldMatrix)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawLine(from, to, color, duration).localToWorldMatrix = localToWorldMatrix;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x0002991F File Offset: 0x00027B1F
		public static void DrawPath(Vector3[] vertices, Color color)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawPath(vertices, color, 0f);
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00029936 File Offset: 0x00027B36
		public static void DrawPath(Vector3[] vertices, Color color, float duration)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawPath(vertices, color, duration);
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00029949 File Offset: 0x00027B49
		public static void DrawPath(Vector3[] vertices, Color color, float duration, Matrix4x4 localToWorldMatrix)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawPath(vertices, color, duration).localToWorldMatrix = localToWorldMatrix;
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x00029961 File Offset: 0x00027B61
		public static void DrawRay(Vector3 from, Vector3 direction, Color color)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawRay(from, direction, color, 0f);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x00029979 File Offset: 0x00027B79
		public static void DrawRay(Vector3 from, Vector3 direction, Color color, float duration)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawRay(from, direction, color, duration);
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x0002998D File Offset: 0x00027B8D
		public static void DrawRay(Vector3 from, Vector3 direction, Color color, float duration, Matrix4x4 localToWorldMatrix)
		{
			if (!IngameDebugGizmos.IsEnabled())
			{
				return;
			}
			IngameDebugGizmos.DrawRay(from, direction, color, duration).localToWorldMatrix = localToWorldMatrix;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000097E6 File Offset: 0x000079E6
		[Doc("Returns true if the game has been launched from the Unity Editor via the -testcontentmod parameter.")]
		public static bool isTestMode
		{
			get
			{
				return GameManager.instance.testContentModMode;
			}
		}
	}
}
