using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200045F RID: 1119
	public class MB_Utility
	{
		// Token: 0x06001C3D RID: 7229 RVA: 0x00015520 File Offset: 0x00013720
		public static Texture2D createTextureCopy(Texture2D source)
		{
			Texture2D texture2D = new Texture2D(source.width, source.height, TextureFormat.ARGB32, true);
			texture2D.SetPixels(source.GetPixels());
			return texture2D;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000B93E8 File Offset: 0x000B75E8
		public static bool ArrayBIsSubsetOfA(object[] a, object[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < a.Length; j++)
				{
					if (a[j] == b[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000B9428 File Offset: 0x000B7628
		public static Material[] GetGOMaterials(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			Material[] array = null;
			Mesh mesh = null;
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				array = component.sharedMaterials;
				MeshFilter component2 = go.GetComponent<MeshFilter>();
				if (component2 == null)
				{
					throw new Exception("Object " + ((go != null) ? go.ToString() : null) + " has a MeshRenderer but no MeshFilter.");
				}
				mesh = component2.sharedMesh;
			}
			SkinnedMeshRenderer component3 = go.GetComponent<SkinnedMeshRenderer>();
			if (component3 != null)
			{
				array = component3.sharedMaterials;
				mesh = component3.sharedMesh;
			}
			if (array == null)
			{
				Debug.LogError("Object " + go.name + " does not have a MeshRenderer or a SkinnedMeshRenderer component");
				return new Material[0];
			}
			if (mesh == null)
			{
				Debug.LogError("Object " + go.name + " has a MeshRenderer or SkinnedMeshRenderer but no mesh.");
				return new Material[0];
			}
			if (mesh.subMeshCount < array.Length)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Object ",
					(go != null) ? go.ToString() : null,
					" has only ",
					mesh.subMeshCount.ToString(),
					" submeshes and has ",
					array.Length.ToString(),
					" materials. Extra materials do nothing."
				}));
				Material[] array2 = new Material[mesh.subMeshCount];
				Array.Copy(array, array2, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000B9588 File Offset: 0x000B7788
		public static Mesh GetMesh(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			MeshFilter component = go.GetComponent<MeshFilter>();
			if (component != null)
			{
				return component.sharedMesh;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				return component2.sharedMesh;
			}
			return null;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000B95D0 File Offset: 0x000B77D0
		public static void SetMesh(GameObject go, Mesh m)
		{
			if (go == null)
			{
				return;
			}
			MeshFilter component = go.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.sharedMesh = m;
				return;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				component2.sharedMesh = m;
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000B9618 File Offset: 0x000B7818
		public static Renderer GetRenderer(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				return component;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				return component2;
			}
			return null;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000B9658 File Offset: 0x000B7858
		public static void DisableRendererInSource(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				component.enabled = false;
				return;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				component2.enabled = false;
				return;
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000B96A0 File Offset: 0x000B78A0
		public static bool hasOutOfBoundsUVs(Mesh m, ref Rect uvBounds)
		{
			MB_Utility.MeshAnalysisResult meshAnalysisResult = default(MB_Utility.MeshAnalysisResult);
			bool result = MB_Utility.hasOutOfBoundsUVs(m, ref meshAnalysisResult, -1, 0);
			uvBounds = meshAnalysisResult.uvRect;
			return result;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000B96CC File Offset: 0x000B78CC
		public static bool hasOutOfBoundsUVs(Mesh m, ref MB_Utility.MeshAnalysisResult putResultHere, int submeshIndex = -1, int uvChannel = 0)
		{
			if (m == null)
			{
				putResultHere.hasOutOfBoundsUVs = false;
				return putResultHere.hasOutOfBoundsUVs;
			}
			Vector2[] uvs;
			if (uvChannel == 0)
			{
				uvs = m.uv;
			}
			else if (uvChannel == 1)
			{
				uvs = m.uv2;
			}
			else if (uvChannel == 2)
			{
				uvs = m.uv3;
			}
			else
			{
				uvs = m.uv4;
			}
			return MB_Utility.hasOutOfBoundsUVs(uvs, m, ref putResultHere, submeshIndex);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000B9728 File Offset: 0x000B7928
		public static bool hasOutOfBoundsUVs(Vector2[] uvs, Mesh m, ref MB_Utility.MeshAnalysisResult putResultHere, int submeshIndex = -1)
		{
			putResultHere.hasUVs = true;
			if (uvs.Length == 0)
			{
				putResultHere.hasUVs = false;
				putResultHere.hasOutOfBoundsUVs = false;
				putResultHere.uvRect = default(Rect);
				return putResultHere.hasOutOfBoundsUVs;
			}
			if (submeshIndex >= m.subMeshCount)
			{
				putResultHere.hasOutOfBoundsUVs = false;
				putResultHere.uvRect = default(Rect);
				return putResultHere.hasOutOfBoundsUVs;
			}
			float x;
			float num;
			float y;
			float num2;
			if (submeshIndex >= 0)
			{
				int[] triangles = m.GetTriangles(submeshIndex);
				if (triangles.Length == 0)
				{
					putResultHere.hasOutOfBoundsUVs = false;
					putResultHere.uvRect = default(Rect);
					return putResultHere.hasOutOfBoundsUVs;
				}
				num = (x = uvs[triangles[0]].x);
				num2 = (y = uvs[triangles[0]].y);
				foreach (int num3 in triangles)
				{
					if (uvs[num3].x < x)
					{
						x = uvs[num3].x;
					}
					if (uvs[num3].x > num)
					{
						num = uvs[num3].x;
					}
					if (uvs[num3].y < y)
					{
						y = uvs[num3].y;
					}
					if (uvs[num3].y > num2)
					{
						num2 = uvs[num3].y;
					}
				}
			}
			else
			{
				num = (x = uvs[0].x);
				num2 = (y = uvs[0].y);
				for (int j = 0; j < uvs.Length; j++)
				{
					if (uvs[j].x < x)
					{
						x = uvs[j].x;
					}
					if (uvs[j].x > num)
					{
						num = uvs[j].x;
					}
					if (uvs[j].y < y)
					{
						y = uvs[j].y;
					}
					if (uvs[j].y > num2)
					{
						num2 = uvs[j].y;
					}
				}
			}
			Rect uvRect = default(Rect);
			uvRect.x = x;
			uvRect.y = y;
			uvRect.width = num - x;
			uvRect.height = num2 - y;
			if (num > 1f || x < 0f || num2 > 1f || y < 0f)
			{
				putResultHere.hasOutOfBoundsUVs = true;
			}
			else
			{
				putResultHere.hasOutOfBoundsUVs = false;
			}
			putResultHere.uvRect = uvRect;
			return putResultHere.hasOutOfBoundsUVs;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000B998C File Offset: 0x000B7B8C
		public static void setSolidColor(Texture2D t, Color c)
		{
			Color[] pixels = t.GetPixels();
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = c;
			}
			t.SetPixels(pixels);
			t.Apply();
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000B99C4 File Offset: 0x000B7BC4
		public static Texture2D resampleTexture(Texture2D source, int newWidth, int newHeight)
		{
			TextureFormat format = source.format;
			if (format == TextureFormat.ARGB32 || format == TextureFormat.RGBA32 || format == TextureFormat.BGRA32 || format == TextureFormat.RGB24 || format == TextureFormat.Alpha8 || format == TextureFormat.DXT1)
			{
				Texture2D texture2D = new Texture2D(newWidth, newHeight, TextureFormat.ARGB32, true);
				float num = (float)newWidth;
				float num2 = (float)newHeight;
				for (int i = 0; i < newWidth; i++)
				{
					for (int j = 0; j < newHeight; j++)
					{
						float u = (float)i / num;
						float v = (float)j / num2;
						texture2D.SetPixel(i, j, source.GetPixelBilinear(u, v));
					}
				}
				texture2D.Apply();
				return texture2D;
			}
			Debug.LogError("Can only resize textures in formats ARGB32, RGBA32, BGRA32, RGB24, Alpha8 or DXT");
			return null;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000B9A58 File Offset: 0x000B7C58
		public static bool AreAllSharedMaterialsDistinct(Material[] sharedMaterials)
		{
			for (int i = 0; i < sharedMaterials.Length; i++)
			{
				for (int j = i + 1; j < sharedMaterials.Length; j++)
				{
					if (sharedMaterials[i] == sharedMaterials[j])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000B9A94 File Offset: 0x000B7C94
		public static int doSubmeshesShareVertsOrTris(Mesh m, ref MB_Utility.MeshAnalysisResult mar)
		{
			MB_Utility.MB_Triangle mb_Triangle = new MB_Utility.MB_Triangle();
			MB_Utility.MB_Triangle mb_Triangle2 = new MB_Utility.MB_Triangle();
			int[][] array = new int[m.subMeshCount][];
			for (int i = 0; i < m.subMeshCount; i++)
			{
				array[i] = m.GetTriangles(i);
			}
			bool flag = false;
			bool flag2 = false;
			for (int j = 0; j < m.subMeshCount; j++)
			{
				int[] array2 = array[j];
				for (int k = j + 1; k < m.subMeshCount; k++)
				{
					int[] array3 = array[k];
					for (int l = 0; l < array2.Length; l += 3)
					{
						mb_Triangle.Initialize(array2, l, j);
						for (int n = 0; n < array3.Length; n += 3)
						{
							mb_Triangle2.Initialize(array3, n, k);
							if (mb_Triangle.isSame(mb_Triangle2))
							{
								flag2 = true;
								break;
							}
							if (mb_Triangle.sharesVerts(mb_Triangle2))
							{
								flag = true;
								break;
							}
						}
					}
				}
			}
			if (flag2)
			{
				mar.hasOverlappingSubmeshVerts = true;
				mar.hasOverlappingSubmeshTris = true;
				return 2;
			}
			if (flag)
			{
				mar.hasOverlappingSubmeshVerts = true;
				mar.hasOverlappingSubmeshTris = false;
				return 1;
			}
			mar.hasOverlappingSubmeshTris = false;
			mar.hasOverlappingSubmeshVerts = false;
			return 0;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000B9BB0 File Offset: 0x000B7DB0
		public static bool GetBounds(GameObject go, out Bounds b)
		{
			if (go == null)
			{
				Debug.LogError("go paramater was null");
				b = new Bounds(Vector3.zero, Vector3.zero);
				return false;
			}
			Renderer renderer = MB_Utility.GetRenderer(go);
			if (renderer == null)
			{
				Debug.LogError("GetBounds must be called on an object with a Renderer");
				b = new Bounds(Vector3.zero, Vector3.zero);
				return false;
			}
			if (renderer is MeshRenderer)
			{
				b = renderer.bounds;
				return true;
			}
			if (renderer is SkinnedMeshRenderer)
			{
				b = renderer.bounds;
				return true;
			}
			Debug.LogError("GetBounds must be called on an object with a MeshRender or a SkinnedMeshRenderer.");
			b = new Bounds(Vector3.zero, Vector3.zero);
			return false;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00015541 File Offset: 0x00013741
		public static void Destroy(UnityEngine.Object o)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(o);
				return;
			}
			UnityEngine.Object.DestroyImmediate(o, false);
		}

		// Token: 0x04001D2F RID: 7471
		public static bool DO_INTEGRITY_CHECKS;

		// Token: 0x02000460 RID: 1120
		public struct MeshAnalysisResult
		{
			// Token: 0x04001D30 RID: 7472
			public Rect uvRect;

			// Token: 0x04001D31 RID: 7473
			public bool hasOutOfBoundsUVs;

			// Token: 0x04001D32 RID: 7474
			public bool hasOverlappingSubmeshVerts;

			// Token: 0x04001D33 RID: 7475
			public bool hasOverlappingSubmeshTris;

			// Token: 0x04001D34 RID: 7476
			public bool hasUVs;

			// Token: 0x04001D35 RID: 7477
			public float submeshArea;
		}

		// Token: 0x02000461 RID: 1121
		private class MB_Triangle
		{
			// Token: 0x06001C4F RID: 7247 RVA: 0x000B9C64 File Offset: 0x000B7E64
			public bool isSame(object obj)
			{
				MB_Utility.MB_Triangle mb_Triangle = (MB_Utility.MB_Triangle)obj;
				return this.vs[0] == mb_Triangle.vs[0] && this.vs[1] == mb_Triangle.vs[1] && this.vs[2] == mb_Triangle.vs[2] && this.submeshIdx != mb_Triangle.submeshIdx;
			}

			// Token: 0x06001C50 RID: 7248 RVA: 0x000B9CC0 File Offset: 0x000B7EC0
			public bool sharesVerts(MB_Utility.MB_Triangle obj)
			{
				return ((this.vs[0] == obj.vs[0] || this.vs[0] == obj.vs[1] || this.vs[0] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx) || ((this.vs[1] == obj.vs[0] || this.vs[1] == obj.vs[1] || this.vs[1] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx) || ((this.vs[2] == obj.vs[0] || this.vs[2] == obj.vs[1] || this.vs[2] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx);
			}

			// Token: 0x06001C51 RID: 7249 RVA: 0x00015558 File Offset: 0x00013758
			public void Initialize(int[] ts, int idx, int sIdx)
			{
				this.vs[0] = ts[idx];
				this.vs[1] = ts[idx + 1];
				this.vs[2] = ts[idx + 2];
				this.submeshIdx = sIdx;
				Array.Sort<int>(this.vs);
			}

			// Token: 0x04001D36 RID: 7478
			private int submeshIdx;

			// Token: 0x04001D37 RID: 7479
			private int[] vs = new int[3];
		}
	}
}
