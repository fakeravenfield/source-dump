using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A00 RID: 2560
	[Proxy(typeof(SkinnedMeshRenderer))]
	public class SkinnedMeshRendererProxy : IProxy
	{
		// Token: 0x06004EBB RID: 20155 RVA: 0x00039126 File Offset: 0x00037326
		[MoonSharpHidden]
		public SkinnedMeshRendererProxy(SkinnedMeshRenderer value)
		{
			this._value = value;
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x00039135 File Offset: 0x00037335
		public SkinnedMeshRendererProxy()
		{
			this._value = new SkinnedMeshRenderer();
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004EBD RID: 20157 RVA: 0x00039148 File Offset: 0x00037348
		// (set) Token: 0x06004EBE RID: 20158 RVA: 0x00039155 File Offset: 0x00037355
		public Transform[] bones
		{
			get
			{
				return this._value.bones;
			}
			set
			{
				this._value.bones = value;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x00039163 File Offset: 0x00037363
		// (set) Token: 0x06004EC0 RID: 20160 RVA: 0x00039170 File Offset: 0x00037370
		public bool forceMatrixRecalculationPerRender
		{
			get
			{
				return this._value.forceMatrixRecalculationPerRender;
			}
			set
			{
				this._value.forceMatrixRecalculationPerRender = value;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x0003917E File Offset: 0x0003737E
		// (set) Token: 0x06004EC2 RID: 20162 RVA: 0x00039190 File Offset: 0x00037390
		public BoundsProxy localBounds
		{
			get
			{
				return BoundsProxy.New(this._value.localBounds);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.localBounds = value._value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x000391B1 File Offset: 0x000373B1
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x00138078 File Offset: 0x00136278
		public TransformProxy rootBone
		{
			get
			{
				return TransformProxy.New(this._value.rootBone);
			}
			set
			{
				Transform rootBone = null;
				if (value != null)
				{
					rootBone = value._value;
				}
				this._value.rootBone = rootBone;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x000391C3 File Offset: 0x000373C3
		// (set) Token: 0x06004EC6 RID: 20166 RVA: 0x001380A0 File Offset: 0x001362A0
		public MeshProxy sharedMesh
		{
			get
			{
				return MeshProxy.New(this._value.sharedMesh);
			}
			set
			{
				Mesh sharedMesh = null;
				if (value != null)
				{
					sharedMesh = value._value;
				}
				this._value.sharedMesh = sharedMesh;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x000391D5 File Offset: 0x000373D5
		// (set) Token: 0x06004EC8 RID: 20168 RVA: 0x000391E2 File Offset: 0x000373E2
		public bool skinnedMotionVectors
		{
			get
			{
				return this._value.skinnedMotionVectors;
			}
			set
			{
				this._value.skinnedMotionVectors = value;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x000391F0 File Offset: 0x000373F0
		// (set) Token: 0x06004ECA RID: 20170 RVA: 0x000391FD File Offset: 0x000373FD
		public bool updateWhenOffscreen
		{
			get
			{
				return this._value.updateWhenOffscreen;
			}
			set
			{
				this._value.updateWhenOffscreen = value;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x0003920B File Offset: 0x0003740B
		// (set) Token: 0x06004ECC RID: 20172 RVA: 0x00039218 File Offset: 0x00037418
		public bool allowOcclusionWhenDynamic
		{
			get
			{
				return this._value.allowOcclusionWhenDynamic;
			}
			set
			{
				this._value.allowOcclusionWhenDynamic = value;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x00039226 File Offset: 0x00037426
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x00039238 File Offset: 0x00037438
		// (set) Token: 0x06004ECF RID: 20175 RVA: 0x00039245 File Offset: 0x00037445
		public bool enabled
		{
			get
			{
				return this._value.enabled;
			}
			set
			{
				this._value.enabled = value;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x00039253 File Offset: 0x00037453
		// (set) Token: 0x06004ED1 RID: 20177 RVA: 0x00039260 File Offset: 0x00037460
		public bool forceRenderingOff
		{
			get
			{
				return this._value.forceRenderingOff;
			}
			set
			{
				this._value.forceRenderingOff = value;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x0003926E File Offset: 0x0003746E
		public bool isPartOfStaticBatch
		{
			get
			{
				return this._value.isPartOfStaticBatch;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x0003927B File Offset: 0x0003747B
		public bool isVisible
		{
			get
			{
				return this._value.isVisible;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x00039288 File Offset: 0x00037488
		// (set) Token: 0x06004ED5 RID: 20181 RVA: 0x00039295 File Offset: 0x00037495
		public int lightmapIndex
		{
			get
			{
				return this._value.lightmapIndex;
			}
			set
			{
				this._value.lightmapIndex = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x000392A3 File Offset: 0x000374A3
		// (set) Token: 0x06004ED7 RID: 20183 RVA: 0x000392B5 File Offset: 0x000374B5
		public Vector4Proxy lightmapScaleOffset
		{
			get
			{
				return Vector4Proxy.New(this._value.lightmapScaleOffset);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.lightmapScaleOffset = value._value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x000392D6 File Offset: 0x000374D6
		// (set) Token: 0x06004ED9 RID: 20185 RVA: 0x001380C8 File Offset: 0x001362C8
		public GameObjectProxy lightProbeProxyVolumeOverride
		{
			get
			{
				return GameObjectProxy.New(this._value.lightProbeProxyVolumeOverride);
			}
			set
			{
				GameObject lightProbeProxyVolumeOverride = null;
				if (value != null)
				{
					lightProbeProxyVolumeOverride = value._value;
				}
				this._value.lightProbeProxyVolumeOverride = lightProbeProxyVolumeOverride;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x000392E8 File Offset: 0x000374E8
		public Matrix4x4Proxy localToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.localToWorldMatrix);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x000392FA File Offset: 0x000374FA
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x001380F0 File Offset: 0x001362F0
		public MaterialProxy material
		{
			get
			{
				return MaterialProxy.New(this._value.material);
			}
			set
			{
				Material material = null;
				if (value != null)
				{
					material = value._value;
				}
				this._value.material = material;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x0003930C File Offset: 0x0003750C
		// (set) Token: 0x06004EDE RID: 20190 RVA: 0x00039319 File Offset: 0x00037519
		public Material[] materials
		{
			get
			{
				return this._value.materials;
			}
			set
			{
				this._value.materials = value;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x00039327 File Offset: 0x00037527
		// (set) Token: 0x06004EE0 RID: 20192 RVA: 0x00138118 File Offset: 0x00136318
		public TransformProxy probeAnchor
		{
			get
			{
				return TransformProxy.New(this._value.probeAnchor);
			}
			set
			{
				Transform probeAnchor = null;
				if (value != null)
				{
					probeAnchor = value._value;
				}
				this._value.probeAnchor = probeAnchor;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x00039339 File Offset: 0x00037539
		// (set) Token: 0x06004EE2 RID: 20194 RVA: 0x00039346 File Offset: 0x00037546
		public int realtimeLightmapIndex
		{
			get
			{
				return this._value.realtimeLightmapIndex;
			}
			set
			{
				this._value.realtimeLightmapIndex = value;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x00039354 File Offset: 0x00037554
		// (set) Token: 0x06004EE4 RID: 20196 RVA: 0x00039366 File Offset: 0x00037566
		public Vector4Proxy realtimeLightmapScaleOffset
		{
			get
			{
				return Vector4Proxy.New(this._value.realtimeLightmapScaleOffset);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.realtimeLightmapScaleOffset = value._value;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x00039387 File Offset: 0x00037587
		// (set) Token: 0x06004EE6 RID: 20198 RVA: 0x00039394 File Offset: 0x00037594
		public bool receiveShadows
		{
			get
			{
				return this._value.receiveShadows;
			}
			set
			{
				this._value.receiveShadows = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x000393A2 File Offset: 0x000375A2
		// (set) Token: 0x06004EE8 RID: 20200 RVA: 0x000393AF File Offset: 0x000375AF
		public int rendererPriority
		{
			get
			{
				return this._value.rendererPriority;
			}
			set
			{
				this._value.rendererPriority = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x000393BD File Offset: 0x000375BD
		// (set) Token: 0x06004EEA RID: 20202 RVA: 0x000393CA File Offset: 0x000375CA
		public uint renderingLayerMask
		{
			get
			{
				return this._value.renderingLayerMask;
			}
			set
			{
				this._value.renderingLayerMask = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x000393D8 File Offset: 0x000375D8
		// (set) Token: 0x06004EEC RID: 20204 RVA: 0x00138140 File Offset: 0x00136340
		public MaterialProxy sharedMaterial
		{
			get
			{
				return MaterialProxy.New(this._value.sharedMaterial);
			}
			set
			{
				Material sharedMaterial = null;
				if (value != null)
				{
					sharedMaterial = value._value;
				}
				this._value.sharedMaterial = sharedMaterial;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x000393EA File Offset: 0x000375EA
		// (set) Token: 0x06004EEE RID: 20206 RVA: 0x000393F7 File Offset: 0x000375F7
		public Material[] sharedMaterials
		{
			get
			{
				return this._value.sharedMaterials;
			}
			set
			{
				this._value.sharedMaterials = value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x00039405 File Offset: 0x00037605
		// (set) Token: 0x06004EF0 RID: 20208 RVA: 0x00039412 File Offset: 0x00037612
		public int sortingLayerID
		{
			get
			{
				return this._value.sortingLayerID;
			}
			set
			{
				this._value.sortingLayerID = value;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x00039420 File Offset: 0x00037620
		// (set) Token: 0x06004EF2 RID: 20210 RVA: 0x0003942D File Offset: 0x0003762D
		public string sortingLayerName
		{
			get
			{
				return this._value.sortingLayerName;
			}
			set
			{
				this._value.sortingLayerName = value;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x0003943B File Offset: 0x0003763B
		// (set) Token: 0x06004EF4 RID: 20212 RVA: 0x00039448 File Offset: 0x00037648
		public int sortingOrder
		{
			get
			{
				return this._value.sortingOrder;
			}
			set
			{
				this._value.sortingOrder = value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x00039456 File Offset: 0x00037656
		public Matrix4x4Proxy worldToLocalMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToLocalMatrix);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x00039468 File Offset: 0x00037668
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x0003947A File Offset: 0x0003767A
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x00039487 File Offset: 0x00037687
		public string tag
		{
			get
			{
				return this._value.tag;
			}
			set
			{
				this._value.tag = value;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00039495 File Offset: 0x00037695
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x000394A7 File Offset: 0x000376A7
		// (set) Token: 0x06004EFB RID: 20219 RVA: 0x000394B4 File Offset: 0x000376B4
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

		// Token: 0x06004EFC RID: 20220 RVA: 0x000394C2 File Offset: 0x000376C2
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x00138168 File Offset: 0x00136368
		[MoonSharpHidden]
		public static SkinnedMeshRendererProxy New(SkinnedMeshRenderer value)
		{
			if (value == null)
			{
				return null;
			}
			SkinnedMeshRendererProxy skinnedMeshRendererProxy = (SkinnedMeshRendererProxy)ObjectCache.Get(typeof(SkinnedMeshRendererProxy), value);
			if (skinnedMeshRendererProxy == null)
			{
				skinnedMeshRendererProxy = new SkinnedMeshRendererProxy(value);
				ObjectCache.Add(typeof(SkinnedMeshRendererProxy), value, skinnedMeshRendererProxy);
			}
			return skinnedMeshRendererProxy;
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x000394CA File Offset: 0x000376CA
		[MoonSharpUserDataMetamethod("__call")]
		public static SkinnedMeshRendererProxy Call(DynValue _)
		{
			return new SkinnedMeshRendererProxy();
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x001381B4 File Offset: 0x001363B4
		public void BakeMesh(MeshProxy mesh)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			this._value.BakeMesh(mesh2);
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x001381DC File Offset: 0x001363DC
		public void BakeMesh(MeshProxy mesh, bool useScale)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			this._value.BakeMesh(mesh2, useScale);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x000394D1 File Offset: 0x000376D1
		public float GetBlendShapeWeight(int index)
		{
			return this._value.GetBlendShapeWeight(index);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x000394DF File Offset: 0x000376DF
		public void SetBlendShapeWeight(int index, float value)
		{
			this._value.SetBlendShapeWeight(index, value);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x000394EE File Offset: 0x000376EE
		public void GetMaterials(List<Material> m)
		{
			this._value.GetMaterials(m);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x000394FC File Offset: 0x000376FC
		public void GetSharedMaterials(List<Material> m)
		{
			this._value.GetSharedMaterials(m);
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0003950A File Offset: 0x0003770A
		public bool HasPropertyBlock()
		{
			return this._value.HasPropertyBlock();
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x00039517 File Offset: 0x00037717
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x00039525 File Offset: 0x00037725
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00039532 File Offset: 0x00037732
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400328F RID: 12943
		[MoonSharpHidden]
		public SkinnedMeshRenderer _value;
	}
}
