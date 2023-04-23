using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200041F RID: 1055
	public class MBVersionConcrete : MBVersionInterface
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x000142E7 File Offset: 0x000124E7
		public string version()
		{
			return "3.26.3";
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000142EE File Offset: 0x000124EE
		public int GetMajorVersion()
		{
			return int.Parse(Application.unityVersion.Split(new char[]
			{
				'.'
			})[0]);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0001430C File Offset: 0x0001250C
		public int GetMinorVersion()
		{
			return int.Parse(Application.unityVersion.Split(new char[]
			{
				'.'
			})[1]);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0001432A File Offset: 0x0001252A
		public bool GetActive(GameObject go)
		{
			return go.activeInHierarchy;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00014332 File Offset: 0x00012532
		public void SetActive(GameObject go, bool isActive)
		{
			go.SetActive(isActive);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00014332 File Offset: 0x00012532
		public void SetActiveRecursively(GameObject go, bool isActive)
		{
			go.SetActive(isActive);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0001433B File Offset: 0x0001253B
		public UnityEngine.Object[] FindSceneObjectsOfType(Type t)
		{
			return UnityEngine.Object.FindObjectsOfType(t);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0000296E File Offset: 0x00000B6E
		public void OptimizeMesh(Mesh m)
		{
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00014343 File Offset: 0x00012543
		public bool IsRunningAndMeshNotReadWriteable(Mesh m)
		{
			return Application.isPlaying && !m.isReadable;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000ACA44 File Offset: 0x000AAC44
		public Vector2[] GetMeshUV1s(Mesh m, MB2_LogLevel LOG_LEVEL)
		{
			if (LOG_LEVEL >= MB2_LogLevel.warn)
			{
				MB2_Log.LogDebug("UV1 does not exist in Unity 5+", Array.Empty<object>());
			}
			Vector2[] array = m.uv;
			if (array.Length == 0)
			{
				if (LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Mesh " + ((m != null) ? m.ToString() : null) + " has no uv1s. Generating", Array.Empty<object>());
				}
				if (LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Mesh " + ((m != null) ? m.ToString() : null) + " didn't have uv1s. Generating uv1s.");
				}
				array = new Vector2[m.vertexCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._HALF_UV;
				}
			}
			return array;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000ACAE8 File Offset: 0x000AACE8
		public Vector2[] GetMeshUV3orUV4(Mesh m, bool get3, MB2_LogLevel LOG_LEVEL)
		{
			Vector2[] array;
			if (get3)
			{
				array = m.uv3;
			}
			else
			{
				array = m.uv4;
			}
			if (array.Length == 0)
			{
				if (LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new string[]
					{
						"Mesh ",
						(m != null) ? m.ToString() : null,
						" has no uv",
						get3 ? "3" : "4",
						". Generating"
					}), Array.Empty<object>());
				}
				array = new Vector2[m.vertexCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._HALF_UV;
				}
			}
			return array;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00014357 File Offset: 0x00012557
		public void MeshClear(Mesh m, bool t)
		{
			m.Clear(t);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00014360 File Offset: 0x00012560
		public void MeshAssignUV3(Mesh m, Vector2[] uv3s)
		{
			m.uv3 = uv3s;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00014369 File Offset: 0x00012569
		public void MeshAssignUV4(Mesh m, Vector2[] uv4s)
		{
			m.uv4 = uv4s;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00014372 File Offset: 0x00012572
		public Vector4 GetLightmapTilingOffset(Renderer r)
		{
			return r.lightmapScaleOffset;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0001437A File Offset: 0x0001257A
		public Transform[] GetBones(Renderer r)
		{
			if (r is SkinnedMeshRenderer)
			{
				return ((SkinnedMeshRenderer)r).bones;
			}
			if (r is MeshRenderer)
			{
				return new Transform[]
				{
					r.transform
				};
			}
			Debug.LogError("Could not getBones. Object does not have a renderer");
			return null;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000143B3 File Offset: 0x000125B3
		public int GetBlendShapeFrameCount(Mesh m, int shapeIndex)
		{
			return m.GetBlendShapeFrameCount(shapeIndex);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000143BC File Offset: 0x000125BC
		public float GetBlendShapeFrameWeight(Mesh m, int shapeIndex, int frameIndex)
		{
			return m.GetBlendShapeFrameWeight(shapeIndex, frameIndex);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000143C6 File Offset: 0x000125C6
		public void GetBlendShapeFrameVertices(Mesh m, int shapeIndex, int frameIndex, Vector3[] vs, Vector3[] ns, Vector3[] ts)
		{
			m.GetBlendShapeFrameVertices(shapeIndex, frameIndex, vs, ns, ts);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000143D6 File Offset: 0x000125D6
		public void ClearBlendShapes(Mesh m)
		{
			m.ClearBlendShapes();
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000143DE File Offset: 0x000125DE
		public void AddBlendShapeFrame(Mesh m, string nm, float wt, Vector3[] vs, Vector3[] ns, Vector3[] ts)
		{
			m.AddBlendShapeFrame(nm, wt, vs, ns, ts);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000143EE File Offset: 0x000125EE
		public int MaxMeshVertexCount()
		{
			return 2147483646;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000ACB88 File Offset: 0x000AAD88
		public void SetMeshIndexFormatAndClearMesh(Mesh m, int numVerts, bool vertices, bool justClearTriangles)
		{
			if (vertices && numVerts > 65534 && m.indexFormat == IndexFormat.UInt16)
			{
				MBVersion.MeshClear(m, false);
				m.indexFormat = IndexFormat.UInt32;
				return;
			}
			if (vertices && numVerts <= 65534 && m.indexFormat == IndexFormat.UInt32)
			{
				MBVersion.MeshClear(m, false);
				m.indexFormat = IndexFormat.UInt16;
				return;
			}
			if (justClearTriangles)
			{
				MBVersion.MeshClear(m, true);
				return;
			}
			MBVersion.MeshClear(m, false);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000143F5 File Offset: 0x000125F5
		public bool GraphicsUVStartsAtTop()
		{
			return SystemInfo.graphicsUVStartsAtTop;
		}

		// Token: 0x04001BF3 RID: 7155
		private Vector2 _HALF_UV = new Vector2(0.5f, 0.5f);
	}
}
