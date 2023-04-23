using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D2 RID: 1490
	public static class MinimapCameraExtensions
	{
		// Token: 0x06002698 RID: 9880 RVA: 0x000F4E64 File Offset: 0x000F3064
		public static void ResizeToIncludeAllTerrain(this MinimapCamera self, Terrain terrain)
		{
			Camera componentInChildren = self.GetComponentInChildren<Camera>();
			float fieldOfView = MinimapCameraExtensions.ComputeFov(self.transform.position.y, terrain.terrainData.size.x);
			componentInChildren.fieldOfView = fieldOfView;
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000F4EA4 File Offset: 0x000F30A4
		public static void AdjustZoomAndPosition(this MinimapCamera self, MapEditorTerrain terrain, MeoCapturePoint[] capturePoints = null)
		{
			List<Vector3> list = new List<Vector3>();
			MapEditor instance = MapEditor.instance;
			if (terrain != null)
			{
				float num = (float)terrain.GetTerrainSize();
				Vector3 position = terrain.GetTerrain().GetPosition();
				list.Add(position);
				list.Add(position + new Vector3(num, 0f, 0f));
				list.Add(position + new Vector3(0f, 0f, num));
				list.Add(position + new Vector3(num, 0f, num));
			}
			if (capturePoints != null)
			{
				foreach (MeoCapturePoint meoCapturePoint in capturePoints)
				{
					list.Add(meoCapturePoint.transform.position);
				}
			}
			self.ShowAllPoints(list);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000F4F68 File Offset: 0x000F3168
		public static void ShowAllPoints(this MinimapCamera self, IEnumerable<Vector3> points)
		{
			Camera componentInChildren = self.GetComponentInChildren<Camera>();
			UnityEngine.Plane plane = new UnityEngine.Plane(componentInChildren.transform.forward, componentInChildren.transform.position + componentInChildren.farClipPlane * componentInChildren.transform.forward);
			Vector3 vector = Vector3.one * float.MaxValue;
			Vector3 vector2 = Vector3.one * float.MinValue;
			foreach (Vector3 a in points)
			{
				Ray ray = new Ray(componentInChildren.transform.position, a - componentInChildren.transform.position);
				float d;
				if (plane.Raycast(ray, out d))
				{
					Vector3 rhs = ray.origin + ray.direction * d;
					vector = Vector3.Min(vector, rhs);
					vector2 = Vector3.Max(vector2, rhs);
				}
			}
			Vector3 vector3 = vector2 - vector;
			Vector3 center = vector + vector3 / 2f;
			Bounds bounds = new Bounds(center, vector3);
			float y = componentInChildren.transform.position.y;
			componentInChildren.transform.position = new Vector3(bounds.center.x, y, bounds.center.z);
			componentInChildren.fieldOfView = MinimapCameraExtensions.ComputeFov(componentInChildren.farClipPlane, Mathf.Max(bounds.size.x, bounds.size.z));
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0001AAFC File Offset: 0x00018CFC
		private static float ComputeFov(float height, float viewWidth)
		{
			return 2f * Mathf.Atan2(viewWidth * 0.5f, height) * 57.29578f;
		}
	}
}
