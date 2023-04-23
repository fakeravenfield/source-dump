using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Unity.Collections;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009D9 RID: 2521
	[Proxy(typeof(Mesh))]
	public class MeshProxy : IProxy
	{
		// Token: 0x06004802 RID: 18434 RVA: 0x000328DD File Offset: 0x00030ADD
		[MoonSharpHidden]
		public MeshProxy(Mesh value)
		{
			this._value = value;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x000328EC File Offset: 0x00030AEC
		public MeshProxy()
		{
			this._value = new Mesh();
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x000328FF File Offset: 0x00030AFF
		// (set) Token: 0x06004805 RID: 18437 RVA: 0x0003290C File Offset: 0x00030B0C
		public Matrix4x4[] bindposes
		{
			get
			{
				return this._value.bindposes;
			}
			set
			{
				this._value.bindposes = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x0003291A File Offset: 0x00030B1A
		public int blendShapeCount
		{
			get
			{
				return this._value.blendShapeCount;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00032927 File Offset: 0x00030B27
		// (set) Token: 0x06004808 RID: 18440 RVA: 0x00032939 File Offset: 0x00030B39
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.bounds = value._value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x0003295A File Offset: 0x00030B5A
		// (set) Token: 0x0600480A RID: 18442 RVA: 0x00032967 File Offset: 0x00030B67
		public Color[] colors
		{
			get
			{
				return this._value.colors;
			}
			set
			{
				this._value.colors = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x00032975 File Offset: 0x00030B75
		public bool isReadable
		{
			get
			{
				return this._value.isReadable;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x00032982 File Offset: 0x00030B82
		// (set) Token: 0x0600480D RID: 18445 RVA: 0x0003298F File Offset: 0x00030B8F
		public Vector3[] normals
		{
			get
			{
				return this._value.normals;
			}
			set
			{
				this._value.normals = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x0003299D File Offset: 0x00030B9D
		// (set) Token: 0x0600480F RID: 18447 RVA: 0x000329AA File Offset: 0x00030BAA
		public int subMeshCount
		{
			get
			{
				return this._value.subMeshCount;
			}
			set
			{
				this._value.subMeshCount = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x000329B8 File Offset: 0x00030BB8
		// (set) Token: 0x06004811 RID: 18449 RVA: 0x000329C5 File Offset: 0x00030BC5
		public Vector4[] tangents
		{
			get
			{
				return this._value.tangents;
			}
			set
			{
				this._value.tangents = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x000329D3 File Offset: 0x00030BD3
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x000329E0 File Offset: 0x00030BE0
		public int[] triangles
		{
			get
			{
				return this._value.triangles;
			}
			set
			{
				this._value.triangles = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x000329EE File Offset: 0x00030BEE
		// (set) Token: 0x06004815 RID: 18453 RVA: 0x000329FB File Offset: 0x00030BFB
		public Vector2[] uv
		{
			get
			{
				return this._value.uv;
			}
			set
			{
				this._value.uv = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x00032A09 File Offset: 0x00030C09
		// (set) Token: 0x06004817 RID: 18455 RVA: 0x00032A16 File Offset: 0x00030C16
		public Vector2[] uv2
		{
			get
			{
				return this._value.uv2;
			}
			set
			{
				this._value.uv2 = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06004818 RID: 18456 RVA: 0x00032A24 File Offset: 0x00030C24
		// (set) Token: 0x06004819 RID: 18457 RVA: 0x00032A31 File Offset: 0x00030C31
		public Vector2[] uv3
		{
			get
			{
				return this._value.uv3;
			}
			set
			{
				this._value.uv3 = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x00032A3F File Offset: 0x00030C3F
		// (set) Token: 0x0600481B RID: 18459 RVA: 0x00032A4C File Offset: 0x00030C4C
		public Vector2[] uv4
		{
			get
			{
				return this._value.uv4;
			}
			set
			{
				this._value.uv4 = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x00032A5A File Offset: 0x00030C5A
		// (set) Token: 0x0600481D RID: 18461 RVA: 0x00032A67 File Offset: 0x00030C67
		public Vector2[] uv5
		{
			get
			{
				return this._value.uv5;
			}
			set
			{
				this._value.uv5 = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x00032A75 File Offset: 0x00030C75
		// (set) Token: 0x0600481F RID: 18463 RVA: 0x00032A82 File Offset: 0x00030C82
		public Vector2[] uv6
		{
			get
			{
				return this._value.uv6;
			}
			set
			{
				this._value.uv6 = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x00032A90 File Offset: 0x00030C90
		// (set) Token: 0x06004821 RID: 18465 RVA: 0x00032A9D File Offset: 0x00030C9D
		public Vector2[] uv7
		{
			get
			{
				return this._value.uv7;
			}
			set
			{
				this._value.uv7 = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00032AAB File Offset: 0x00030CAB
		// (set) Token: 0x06004823 RID: 18467 RVA: 0x00032AB8 File Offset: 0x00030CB8
		public Vector2[] uv8
		{
			get
			{
				return this._value.uv8;
			}
			set
			{
				this._value.uv8 = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x00032AC6 File Offset: 0x00030CC6
		public int vertexAttributeCount
		{
			get
			{
				return this._value.vertexAttributeCount;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x00032AD3 File Offset: 0x00030CD3
		public int vertexBufferCount
		{
			get
			{
				return this._value.vertexBufferCount;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x00032AE0 File Offset: 0x00030CE0
		public int vertexCount
		{
			get
			{
				return this._value.vertexCount;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x00032AED File Offset: 0x00030CED
		// (set) Token: 0x06004828 RID: 18472 RVA: 0x00032AFA File Offset: 0x00030CFA
		public Vector3[] vertices
		{
			get
			{
				return this._value.vertices;
			}
			set
			{
				this._value.vertices = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x00032B08 File Offset: 0x00030D08
		// (set) Token: 0x0600482A RID: 18474 RVA: 0x00032B15 File Offset: 0x00030D15
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x00032B23 File Offset: 0x00030D23
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x00131480 File Offset: 0x0012F680
		[MoonSharpHidden]
		public static MeshProxy New(Mesh value)
		{
			if (value == null)
			{
				return null;
			}
			MeshProxy meshProxy = (MeshProxy)ObjectCache.Get(typeof(MeshProxy), value);
			if (meshProxy == null)
			{
				meshProxy = new MeshProxy(value);
				ObjectCache.Add(typeof(MeshProxy), value, meshProxy);
			}
			return meshProxy;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00032B2B File Offset: 0x00030D2B
		[MoonSharpUserDataMetamethod("__call")]
		public static MeshProxy Call(DynValue _)
		{
			return new MeshProxy();
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00032B32 File Offset: 0x00030D32
		public void AddBlendShapeFrame(string shapeName, float frameWeight, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents)
		{
			this._value.AddBlendShapeFrame(shapeName, frameWeight, deltaVertices, deltaNormals, deltaTangents);
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00032B46 File Offset: 0x00030D46
		public void Clear(bool keepVertexLayout)
		{
			this._value.Clear(keepVertexLayout);
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x00032B54 File Offset: 0x00030D54
		public void Clear()
		{
			this._value.Clear();
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00032B61 File Offset: 0x00030D61
		public void ClearBlendShapes()
		{
			this._value.ClearBlendShapes();
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00032B6E File Offset: 0x00030D6E
		public uint GetBaseVertex(int submesh)
		{
			return this._value.GetBaseVertex(submesh);
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00032B7C File Offset: 0x00030D7C
		public void GetBindposes(List<Matrix4x4> bindposes)
		{
			this._value.GetBindposes(bindposes);
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x00032B8A File Offset: 0x00030D8A
		public int GetBlendShapeFrameCount(int shapeIndex)
		{
			return this._value.GetBlendShapeFrameCount(shapeIndex);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x00032B98 File Offset: 0x00030D98
		public float GetBlendShapeFrameWeight(int shapeIndex, int frameIndex)
		{
			return this._value.GetBlendShapeFrameWeight(shapeIndex, frameIndex);
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x00032BA7 File Offset: 0x00030DA7
		public void GetBlendShapeFrameVertices(int shapeIndex, int frameIndex, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents)
		{
			this._value.GetBlendShapeFrameVertices(shapeIndex, frameIndex, deltaVertices, deltaNormals, deltaTangents);
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x00032BBB File Offset: 0x00030DBB
		public int GetBlendShapeIndex(string blendShapeName)
		{
			return this._value.GetBlendShapeIndex(blendShapeName);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00032BC9 File Offset: 0x00030DC9
		public string GetBlendShapeName(int shapeIndex)
		{
			return this._value.GetBlendShapeName(shapeIndex);
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00032BD7 File Offset: 0x00030DD7
		public NativeArray<byte> GetBonesPerVertex()
		{
			return this._value.GetBonesPerVertex();
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00032BE4 File Offset: 0x00030DE4
		public void GetColors(List<Color> colors)
		{
			this._value.GetColors(colors);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00032BF2 File Offset: 0x00030DF2
		public uint GetIndexCount(int submesh)
		{
			return this._value.GetIndexCount(submesh);
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x00032C00 File Offset: 0x00030E00
		public uint GetIndexStart(int submesh)
		{
			return this._value.GetIndexStart(submesh);
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x00032C0E File Offset: 0x00030E0E
		public int[] GetIndices(int submesh)
		{
			return this._value.GetIndices(submesh);
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00032C1C File Offset: 0x00030E1C
		public int[] GetIndices(int submesh, bool applyBaseVertex)
		{
			return this._value.GetIndices(submesh, applyBaseVertex);
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00032C2B File Offset: 0x00030E2B
		public void GetIndices(List<int> indices, int submesh)
		{
			this._value.GetIndices(indices, submesh);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00032C3A File Offset: 0x00030E3A
		public void GetIndices(List<int> indices, int submesh, bool applyBaseVertex)
		{
			this._value.GetIndices(indices, submesh, applyBaseVertex);
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00032C4A File Offset: 0x00030E4A
		public void GetNormals(List<Vector3> normals)
		{
			this._value.GetNormals(normals);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00032C58 File Offset: 0x00030E58
		public void GetTangents(List<Vector4> tangents)
		{
			this._value.GetTangents(tangents);
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00032C66 File Offset: 0x00030E66
		public int[] GetTriangles(int submesh)
		{
			return this._value.GetTriangles(submesh);
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00032C74 File Offset: 0x00030E74
		public int[] GetTriangles(int submesh, bool applyBaseVertex)
		{
			return this._value.GetTriangles(submesh, applyBaseVertex);
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00032C83 File Offset: 0x00030E83
		public void GetTriangles(List<int> triangles, int submesh)
		{
			this._value.GetTriangles(triangles, submesh);
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x00032C92 File Offset: 0x00030E92
		public void GetTriangles(List<int> triangles, int submesh, bool applyBaseVertex)
		{
			this._value.GetTriangles(triangles, submesh, applyBaseVertex);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00032CA2 File Offset: 0x00030EA2
		public float GetUVDistributionMetric(int uvSetIndex)
		{
			return this._value.GetUVDistributionMetric(uvSetIndex);
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00032CB0 File Offset: 0x00030EB0
		public void GetUVs(int channel, List<Vector2> uvs)
		{
			this._value.GetUVs(channel, uvs);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x00032CBF File Offset: 0x00030EBF
		public void GetVertices(List<Vector3> vertices)
		{
			this._value.GetVertices(vertices);
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00032CCD File Offset: 0x00030ECD
		public void MarkDynamic()
		{
			this._value.MarkDynamic();
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x00032CDA File Offset: 0x00030EDA
		public void MarkModified()
		{
			this._value.MarkModified();
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x00032CE7 File Offset: 0x00030EE7
		public void Optimize()
		{
			this._value.Optimize();
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x00032CF4 File Offset: 0x00030EF4
		public void OptimizeIndexBuffers()
		{
			this._value.OptimizeIndexBuffers();
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00032D01 File Offset: 0x00030F01
		public void OptimizeReorderVertexBuffer()
		{
			this._value.OptimizeReorderVertexBuffer();
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x00032D0E File Offset: 0x00030F0E
		public void RecalculateBounds()
		{
			this._value.RecalculateBounds();
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x00032D1B File Offset: 0x00030F1B
		public void RecalculateNormals()
		{
			this._value.RecalculateNormals();
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00032D28 File Offset: 0x00030F28
		public void RecalculateTangents()
		{
			this._value.RecalculateTangents();
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x00032D35 File Offset: 0x00030F35
		public void RecalculateUVDistributionMetric(int uvSetIndex, float uvAreaThreshold)
		{
			this._value.RecalculateUVDistributionMetric(uvSetIndex, uvAreaThreshold);
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x00032D44 File Offset: 0x00030F44
		public void RecalculateUVDistributionMetrics(float uvAreaThreshold)
		{
			this._value.RecalculateUVDistributionMetrics(uvAreaThreshold);
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x00032D52 File Offset: 0x00030F52
		public void SetColors(List<Color> inColors)
		{
			this._value.SetColors(inColors);
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x00032D60 File Offset: 0x00030F60
		public void SetColors(List<Color> inColors, int start, int length)
		{
			this._value.SetColors(inColors, start, length);
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x00032D70 File Offset: 0x00030F70
		public void SetColors(Color[] inColors)
		{
			this._value.SetColors(inColors);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00032D7E File Offset: 0x00030F7E
		public void SetColors(Color[] inColors, int start, int length)
		{
			this._value.SetColors(inColors, start, length);
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x00032D8E File Offset: 0x00030F8E
		public void SetNormals(List<Vector3> inNormals)
		{
			this._value.SetNormals(inNormals);
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00032D9C File Offset: 0x00030F9C
		public void SetNormals(List<Vector3> inNormals, int start, int length)
		{
			this._value.SetNormals(inNormals, start, length);
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x00032DAC File Offset: 0x00030FAC
		public void SetNormals(Vector3[] inNormals)
		{
			this._value.SetNormals(inNormals);
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00032DBA File Offset: 0x00030FBA
		public void SetNormals(Vector3[] inNormals, int start, int length)
		{
			this._value.SetNormals(inNormals, start, length);
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x00032DCA File Offset: 0x00030FCA
		public void SetTangents(List<Vector4> inTangents)
		{
			this._value.SetTangents(inTangents);
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x00032DD8 File Offset: 0x00030FD8
		public void SetTangents(List<Vector4> inTangents, int start, int length)
		{
			this._value.SetTangents(inTangents, start, length);
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x00032DE8 File Offset: 0x00030FE8
		public void SetTangents(Vector4[] inTangents)
		{
			this._value.SetTangents(inTangents);
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x00032DF6 File Offset: 0x00030FF6
		public void SetTangents(Vector4[] inTangents, int start, int length)
		{
			this._value.SetTangents(inTangents, start, length);
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x00032E06 File Offset: 0x00031006
		public void SetTriangles(int[] triangles, int submesh)
		{
			this._value.SetTriangles(triangles, submesh);
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00032E15 File Offset: 0x00031015
		public void SetTriangles(int[] triangles, int submesh, bool calculateBounds)
		{
			this._value.SetTriangles(triangles, submesh, calculateBounds);
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x00032E25 File Offset: 0x00031025
		public void SetTriangles(int[] triangles, int submesh, bool calculateBounds, int baseVertex)
		{
			this._value.SetTriangles(triangles, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x00032E37 File Offset: 0x00031037
		public void SetTriangles(int[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds, int baseVertex)
		{
			this._value.SetTriangles(triangles, trianglesStart, trianglesLength, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x00032E4D File Offset: 0x0003104D
		public void SetTriangles(ushort[] triangles, int submesh, bool calculateBounds, int baseVertex)
		{
			this._value.SetTriangles(triangles, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x00032E5F File Offset: 0x0003105F
		public void SetTriangles(ushort[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds, int baseVertex)
		{
			this._value.SetTriangles(triangles, trianglesStart, trianglesLength, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x00032E75 File Offset: 0x00031075
		public void SetTriangles(List<int> triangles, int submesh)
		{
			this._value.SetTriangles(triangles, submesh);
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x00032E84 File Offset: 0x00031084
		public void SetTriangles(List<int> triangles, int submesh, bool calculateBounds)
		{
			this._value.SetTriangles(triangles, submesh, calculateBounds);
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x00032E94 File Offset: 0x00031094
		public void SetTriangles(List<int> triangles, int submesh, bool calculateBounds, int baseVertex)
		{
			this._value.SetTriangles(triangles, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x00032EA6 File Offset: 0x000310A6
		public void SetTriangles(List<int> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds, int baseVertex)
		{
			this._value.SetTriangles(triangles, trianglesStart, trianglesLength, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x00032EBC File Offset: 0x000310BC
		public void SetUVs(int channel, List<Vector2> uvs)
		{
			this._value.SetUVs(channel, uvs);
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x00032ECB File Offset: 0x000310CB
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length)
		{
			this._value.SetUVs(channel, uvs, start, length);
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x00032EDD File Offset: 0x000310DD
		public void SetUVs(int channel, Vector2[] uvs)
		{
			this._value.SetUVs(channel, uvs);
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x00032EEC File Offset: 0x000310EC
		public void SetUVs(int channel, Vector3[] uvs)
		{
			this._value.SetUVs(channel, uvs);
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x00032EFB File Offset: 0x000310FB
		public void SetUVs(int channel, Vector4[] uvs)
		{
			this._value.SetUVs(channel, uvs);
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x00032F0A File Offset: 0x0003110A
		public void SetUVs(int channel, Vector2[] uvs, int start, int length)
		{
			this._value.SetUVs(channel, uvs, start, length);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x00032F1C File Offset: 0x0003111C
		public void SetUVs(int channel, Vector3[] uvs, int start, int length)
		{
			this._value.SetUVs(channel, uvs, start, length);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00032F2E File Offset: 0x0003112E
		public void SetUVs(int channel, Vector4[] uvs, int start, int length)
		{
			this._value.SetUVs(channel, uvs, start, length);
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00032F40 File Offset: 0x00031140
		public void SetVertices(List<Vector3> inVertices)
		{
			this._value.SetVertices(inVertices);
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x00032F4E File Offset: 0x0003114E
		public void SetVertices(List<Vector3> inVertices, int start, int length)
		{
			this._value.SetVertices(inVertices, start, length);
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x00032F5E File Offset: 0x0003115E
		public void SetVertices(Vector3[] inVertices)
		{
			this._value.SetVertices(inVertices);
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x00032F6C File Offset: 0x0003116C
		public void SetVertices(Vector3[] inVertices, int start, int length)
		{
			this._value.SetVertices(inVertices, start, length);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00032F7C File Offset: 0x0003117C
		public void UploadMeshData(bool markNoLongerReadable)
		{
			this._value.UploadMeshData(markNoLongerReadable);
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x00032F8A File Offset: 0x0003118A
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x00032F97 File Offset: 0x00031197
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003171 RID: 12657
		[MoonSharpHidden]
		public Mesh _value;
	}
}
