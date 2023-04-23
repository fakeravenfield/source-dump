using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000443 RID: 1091
	public interface MBVersionInterface
	{
		// Token: 0x06001ADA RID: 6874
		string version();

		// Token: 0x06001ADB RID: 6875
		int GetMajorVersion();

		// Token: 0x06001ADC RID: 6876
		int GetMinorVersion();

		// Token: 0x06001ADD RID: 6877
		bool GetActive(GameObject go);

		// Token: 0x06001ADE RID: 6878
		void SetActive(GameObject go, bool isActive);

		// Token: 0x06001ADF RID: 6879
		void SetActiveRecursively(GameObject go, bool isActive);

		// Token: 0x06001AE0 RID: 6880
		UnityEngine.Object[] FindSceneObjectsOfType(Type t);

		// Token: 0x06001AE1 RID: 6881
		bool IsRunningAndMeshNotReadWriteable(Mesh m);

		// Token: 0x06001AE2 RID: 6882
		Vector2[] GetMeshUV3orUV4(Mesh m, bool get3, MB2_LogLevel LOG_LEVEL);

		// Token: 0x06001AE3 RID: 6883
		void MeshClear(Mesh m, bool t);

		// Token: 0x06001AE4 RID: 6884
		void MeshAssignUV3(Mesh m, Vector2[] uv3s);

		// Token: 0x06001AE5 RID: 6885
		void MeshAssignUV4(Mesh m, Vector2[] uv4s);

		// Token: 0x06001AE6 RID: 6886
		Vector4 GetLightmapTilingOffset(Renderer r);

		// Token: 0x06001AE7 RID: 6887
		Transform[] GetBones(Renderer r);

		// Token: 0x06001AE8 RID: 6888
		void OptimizeMesh(Mesh m);

		// Token: 0x06001AE9 RID: 6889
		int GetBlendShapeFrameCount(Mesh m, int shapeIndex);

		// Token: 0x06001AEA RID: 6890
		float GetBlendShapeFrameWeight(Mesh m, int shapeIndex, int frameIndex);

		// Token: 0x06001AEB RID: 6891
		void GetBlendShapeFrameVertices(Mesh m, int shapeIndex, int frameIndex, Vector3[] vs, Vector3[] ns, Vector3[] ts);

		// Token: 0x06001AEC RID: 6892
		void ClearBlendShapes(Mesh m);

		// Token: 0x06001AED RID: 6893
		void AddBlendShapeFrame(Mesh m, string nm, float wt, Vector3[] vs, Vector3[] ns, Vector3[] ts);

		// Token: 0x06001AEE RID: 6894
		int MaxMeshVertexCount();

		// Token: 0x06001AEF RID: 6895
		void SetMeshIndexFormatAndClearMesh(Mesh m, int numVerts, bool vertices, bool justClearTriangles);

		// Token: 0x06001AF0 RID: 6896
		bool GraphicsUVStartsAtTop();
	}
}
