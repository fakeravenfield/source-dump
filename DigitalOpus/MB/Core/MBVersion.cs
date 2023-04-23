using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000444 RID: 1092
	public class MBVersion
	{
		// Token: 0x06001AF1 RID: 6897 RVA: 0x000147A8 File Offset: 0x000129A8
		private static MBVersionInterface _CreateMBVersionConcrete()
		{
			return (MBVersionInterface)Activator.CreateInstance(typeof(MBVersionConcrete));
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000147BE File Offset: 0x000129BE
		public static string version()
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.version();
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000147DB File Offset: 0x000129DB
		public static int GetMajorVersion()
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetMajorVersion();
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000147F8 File Offset: 0x000129F8
		public static int GetMinorVersion()
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetMinorVersion();
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00014815 File Offset: 0x00012A15
		public static bool GetActive(GameObject go)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetActive(go);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00014833 File Offset: 0x00012A33
		public static void SetActive(GameObject go, bool isActive)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.SetActive(go, isActive);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00014852 File Offset: 0x00012A52
		public static void SetActiveRecursively(GameObject go, bool isActive)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.SetActiveRecursively(go, isActive);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00014871 File Offset: 0x00012A71
		public static UnityEngine.Object[] FindSceneObjectsOfType(Type t)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.FindSceneObjectsOfType(t);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0001488F File Offset: 0x00012A8F
		public static bool IsRunningAndMeshNotReadWriteable(Mesh m)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.IsRunningAndMeshNotReadWriteable(m);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000148AD File Offset: 0x00012AAD
		public static Vector2[] GetMeshUV3orUV4(Mesh m, bool get3, MB2_LogLevel LOG_LEVEL)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetMeshUV3orUV4(m, get3, LOG_LEVEL);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000148CD File Offset: 0x00012ACD
		public static void MeshClear(Mesh m, bool t)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.MeshClear(m, t);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000148EC File Offset: 0x00012AEC
		public static void MeshAssignUV3(Mesh m, Vector2[] uv3s)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.MeshAssignUV3(m, uv3s);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0001490B File Offset: 0x00012B0B
		public static void MeshAssignUV4(Mesh m, Vector2[] uv4s)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.MeshAssignUV4(m, uv4s);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0001492A File Offset: 0x00012B2A
		public static Vector4 GetLightmapTilingOffset(Renderer r)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetLightmapTilingOffset(r);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00014948 File Offset: 0x00012B48
		public static Transform[] GetBones(Renderer r)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetBones(r);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00014966 File Offset: 0x00012B66
		public static void OptimizeMesh(Mesh m)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.OptimizeMesh(m);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00014984 File Offset: 0x00012B84
		public static int GetBlendShapeFrameCount(Mesh m, int shapeIndex)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetBlendShapeFrameCount(m, shapeIndex);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000149A3 File Offset: 0x00012BA3
		public static float GetBlendShapeFrameWeight(Mesh m, int shapeIndex, int frameIndex)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GetBlendShapeFrameWeight(m, shapeIndex, frameIndex);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000149C3 File Offset: 0x00012BC3
		public static void GetBlendShapeFrameVertices(Mesh m, int shapeIndex, int frameIndex, Vector3[] vs, Vector3[] ns, Vector3[] ts)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.GetBlendShapeFrameVertices(m, shapeIndex, frameIndex, vs, ns, ts);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000149E8 File Offset: 0x00012BE8
		public static void ClearBlendShapes(Mesh m)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.ClearBlendShapes(m);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00014A06 File Offset: 0x00012C06
		public static void AddBlendShapeFrame(Mesh m, string nm, float wt, Vector3[] vs, Vector3[] ns, Vector3[] ts)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.AddBlendShapeFrame(m, nm, wt, vs, ns, ts);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00014A2B File Offset: 0x00012C2B
		public static int MaxMeshVertexCount()
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.MaxMeshVertexCount();
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00014A48 File Offset: 0x00012C48
		public static void SetMeshIndexFormatAndClearMesh(Mesh m, int numVerts, bool vertices, bool justClearTriangles)
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			MBVersion._MBVersion.SetMeshIndexFormatAndClearMesh(m, numVerts, vertices, justClearTriangles);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00014A69 File Offset: 0x00012C69
		public static bool GraphicsUVStartsAtTop()
		{
			if (MBVersion._MBVersion == null)
			{
				MBVersion._MBVersion = MBVersion._CreateMBVersionConcrete();
			}
			return MBVersion._MBVersion.GraphicsUVStartsAtTop();
		}

		// Token: 0x04001C99 RID: 7321
		private static MBVersionInterface _MBVersion;
	}
}
