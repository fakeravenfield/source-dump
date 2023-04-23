using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Unity.Collections;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009D1 RID: 2513
	[Proxy(typeof(LineRenderer))]
	public class LineRendererProxy : IProxy
	{
		// Token: 0x06004674 RID: 18036 RVA: 0x00031267 File Offset: 0x0002F467
		[MoonSharpHidden]
		public LineRendererProxy(LineRenderer value)
		{
			this._value = value;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00031276 File Offset: 0x0002F476
		public LineRendererProxy()
		{
			this._value = new LineRenderer();
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x00031289 File Offset: 0x0002F489
		// (set) Token: 0x06004677 RID: 18039 RVA: 0x00130E58 File Offset: 0x0012F058
		public GradientProxy colorGradient
		{
			get
			{
				return GradientProxy.New(this._value.colorGradient);
			}
			set
			{
				Gradient colorGradient = null;
				if (value != null)
				{
					colorGradient = value._value;
				}
				this._value.colorGradient = colorGradient;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x0003129B File Offset: 0x0002F49B
		// (set) Token: 0x06004679 RID: 18041 RVA: 0x000312AD File Offset: 0x0002F4AD
		public ColorProxy endColor
		{
			get
			{
				return ColorProxy.New(this._value.endColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.endColor = value._value;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x000312CE File Offset: 0x0002F4CE
		// (set) Token: 0x0600467B RID: 18043 RVA: 0x000312DB File Offset: 0x0002F4DB
		public float endWidth
		{
			get
			{
				return this._value.endWidth;
			}
			set
			{
				this._value.endWidth = value;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x000312E9 File Offset: 0x0002F4E9
		// (set) Token: 0x0600467D RID: 18045 RVA: 0x000312F6 File Offset: 0x0002F4F6
		public bool generateLightingData
		{
			get
			{
				return this._value.generateLightingData;
			}
			set
			{
				this._value.generateLightingData = value;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x00031304 File Offset: 0x0002F504
		// (set) Token: 0x0600467F RID: 18047 RVA: 0x00031311 File Offset: 0x0002F511
		public bool loop
		{
			get
			{
				return this._value.loop;
			}
			set
			{
				this._value.loop = value;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x0003131F File Offset: 0x0002F51F
		// (set) Token: 0x06004681 RID: 18049 RVA: 0x0003132C File Offset: 0x0002F52C
		public int numCapVertices
		{
			get
			{
				return this._value.numCapVertices;
			}
			set
			{
				this._value.numCapVertices = value;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x0003133A File Offset: 0x0002F53A
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x00031347 File Offset: 0x0002F547
		public int numCornerVertices
		{
			get
			{
				return this._value.numCornerVertices;
			}
			set
			{
				this._value.numCornerVertices = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x00031355 File Offset: 0x0002F555
		// (set) Token: 0x06004685 RID: 18053 RVA: 0x00031362 File Offset: 0x0002F562
		public int positionCount
		{
			get
			{
				return this._value.positionCount;
			}
			set
			{
				this._value.positionCount = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x00031370 File Offset: 0x0002F570
		// (set) Token: 0x06004687 RID: 18055 RVA: 0x0003137D File Offset: 0x0002F57D
		public float shadowBias
		{
			get
			{
				return this._value.shadowBias;
			}
			set
			{
				this._value.shadowBias = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x0003138B File Offset: 0x0002F58B
		// (set) Token: 0x06004689 RID: 18057 RVA: 0x0003139D File Offset: 0x0002F59D
		public ColorProxy startColor
		{
			get
			{
				return ColorProxy.New(this._value.startColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.startColor = value._value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x000313BE File Offset: 0x0002F5BE
		// (set) Token: 0x0600468B RID: 18059 RVA: 0x000313CB File Offset: 0x0002F5CB
		public float startWidth
		{
			get
			{
				return this._value.startWidth;
			}
			set
			{
				this._value.startWidth = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x000313D9 File Offset: 0x0002F5D9
		// (set) Token: 0x0600468D RID: 18061 RVA: 0x000313E6 File Offset: 0x0002F5E6
		public bool useWorldSpace
		{
			get
			{
				return this._value.useWorldSpace;
			}
			set
			{
				this._value.useWorldSpace = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x000313F4 File Offset: 0x0002F5F4
		// (set) Token: 0x0600468F RID: 18063 RVA: 0x00130E80 File Offset: 0x0012F080
		public AnimationCurveProxy widthCurve
		{
			get
			{
				return AnimationCurveProxy.New(this._value.widthCurve);
			}
			set
			{
				AnimationCurve widthCurve = null;
				if (value != null)
				{
					widthCurve = value._value;
				}
				this._value.widthCurve = widthCurve;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06004690 RID: 18064 RVA: 0x00031406 File Offset: 0x0002F606
		// (set) Token: 0x06004691 RID: 18065 RVA: 0x00031413 File Offset: 0x0002F613
		public float widthMultiplier
		{
			get
			{
				return this._value.widthMultiplier;
			}
			set
			{
				this._value.widthMultiplier = value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06004692 RID: 18066 RVA: 0x00031421 File Offset: 0x0002F621
		// (set) Token: 0x06004693 RID: 18067 RVA: 0x0003142E File Offset: 0x0002F62E
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

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x0003143C File Offset: 0x0002F63C
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x0003144E File Offset: 0x0002F64E
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x0003145B File Offset: 0x0002F65B
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

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x00031469 File Offset: 0x0002F669
		// (set) Token: 0x06004698 RID: 18072 RVA: 0x00031476 File Offset: 0x0002F676
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

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x00031484 File Offset: 0x0002F684
		public bool isPartOfStaticBatch
		{
			get
			{
				return this._value.isPartOfStaticBatch;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x00031491 File Offset: 0x0002F691
		public bool isVisible
		{
			get
			{
				return this._value.isVisible;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x0003149E File Offset: 0x0002F69E
		// (set) Token: 0x0600469C RID: 18076 RVA: 0x000314AB File Offset: 0x0002F6AB
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x000314B9 File Offset: 0x0002F6B9
		// (set) Token: 0x0600469E RID: 18078 RVA: 0x000314CB File Offset: 0x0002F6CB
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

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x000314EC File Offset: 0x0002F6EC
		// (set) Token: 0x060046A0 RID: 18080 RVA: 0x00130EA8 File Offset: 0x0012F0A8
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

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x000314FE File Offset: 0x0002F6FE
		public Matrix4x4Proxy localToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.localToWorldMatrix);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x00031510 File Offset: 0x0002F710
		// (set) Token: 0x060046A3 RID: 18083 RVA: 0x00130ED0 File Offset: 0x0012F0D0
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

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00031522 File Offset: 0x0002F722
		// (set) Token: 0x060046A5 RID: 18085 RVA: 0x0003152F File Offset: 0x0002F72F
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

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x0003153D File Offset: 0x0002F73D
		// (set) Token: 0x060046A7 RID: 18087 RVA: 0x00130EF8 File Offset: 0x0012F0F8
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

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x0003154F File Offset: 0x0002F74F
		// (set) Token: 0x060046A9 RID: 18089 RVA: 0x0003155C File Offset: 0x0002F75C
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

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060046AA RID: 18090 RVA: 0x0003156A File Offset: 0x0002F76A
		// (set) Token: 0x060046AB RID: 18091 RVA: 0x0003157C File Offset: 0x0002F77C
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

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x0003159D File Offset: 0x0002F79D
		// (set) Token: 0x060046AD RID: 18093 RVA: 0x000315AA File Offset: 0x0002F7AA
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

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060046AE RID: 18094 RVA: 0x000315B8 File Offset: 0x0002F7B8
		// (set) Token: 0x060046AF RID: 18095 RVA: 0x000315C5 File Offset: 0x0002F7C5
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060046B0 RID: 18096 RVA: 0x000315D3 File Offset: 0x0002F7D3
		// (set) Token: 0x060046B1 RID: 18097 RVA: 0x000315E0 File Offset: 0x0002F7E0
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x000315EE File Offset: 0x0002F7EE
		// (set) Token: 0x060046B3 RID: 18099 RVA: 0x00130F20 File Offset: 0x0012F120
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

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x00031600 File Offset: 0x0002F800
		// (set) Token: 0x060046B5 RID: 18101 RVA: 0x0003160D File Offset: 0x0002F80D
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

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x0003161B File Offset: 0x0002F81B
		// (set) Token: 0x060046B7 RID: 18103 RVA: 0x00031628 File Offset: 0x0002F828
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

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x00031636 File Offset: 0x0002F836
		// (set) Token: 0x060046B9 RID: 18105 RVA: 0x00031643 File Offset: 0x0002F843
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

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060046BA RID: 18106 RVA: 0x00031651 File Offset: 0x0002F851
		// (set) Token: 0x060046BB RID: 18107 RVA: 0x0003165E File Offset: 0x0002F85E
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

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060046BC RID: 18108 RVA: 0x0003166C File Offset: 0x0002F86C
		public Matrix4x4Proxy worldToLocalMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToLocalMatrix);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x0003167E File Offset: 0x0002F87E
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060046BE RID: 18110 RVA: 0x00031690 File Offset: 0x0002F890
		// (set) Token: 0x060046BF RID: 18111 RVA: 0x0003169D File Offset: 0x0002F89D
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

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060046C0 RID: 18112 RVA: 0x000316AB File Offset: 0x0002F8AB
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060046C1 RID: 18113 RVA: 0x000316BD File Offset: 0x0002F8BD
		// (set) Token: 0x060046C2 RID: 18114 RVA: 0x000316CA File Offset: 0x0002F8CA
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

		// Token: 0x060046C3 RID: 18115 RVA: 0x000316D8 File Offset: 0x0002F8D8
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x00130F48 File Offset: 0x0012F148
		[MoonSharpHidden]
		public static LineRendererProxy New(LineRenderer value)
		{
			if (value == null)
			{
				return null;
			}
			LineRendererProxy lineRendererProxy = (LineRendererProxy)ObjectCache.Get(typeof(LineRendererProxy), value);
			if (lineRendererProxy == null)
			{
				lineRendererProxy = new LineRendererProxy(value);
				ObjectCache.Add(typeof(LineRendererProxy), value, lineRendererProxy);
			}
			return lineRendererProxy;
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000316E0 File Offset: 0x0002F8E0
		[MoonSharpUserDataMetamethod("__call")]
		public static LineRendererProxy Call(DynValue _)
		{
			return new LineRendererProxy();
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x00130F94 File Offset: 0x0012F194
		public void BakeMesh(MeshProxy mesh, bool useTransform)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			this._value.BakeMesh(mesh2, useTransform);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x00130FBC File Offset: 0x0012F1BC
		public void BakeMesh(MeshProxy mesh, CameraProxy camera, bool useTransform)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			Camera camera2 = null;
			if (camera != null)
			{
				camera2 = camera._value;
			}
			this._value.BakeMesh(mesh2, camera2, useTransform);
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000316E7 File Offset: 0x0002F8E7
		public Vector3Proxy GetPosition(int index)
		{
			return Vector3Proxy.New(this._value.GetPosition(index));
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000316FA File Offset: 0x0002F8FA
		public void SetPosition(int index, Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			this._value.SetPosition(index, position._value);
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x0003171C File Offset: 0x0002F91C
		public void SetPositions(Vector3[] positions)
		{
			this._value.SetPositions(positions);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x0003172A File Offset: 0x0002F92A
		public void SetPositions(NativeArray<Vector3> positions)
		{
			this._value.SetPositions(positions);
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x00031738 File Offset: 0x0002F938
		public void SetPositions(NativeSlice<Vector3> positions)
		{
			this._value.SetPositions(positions);
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00031746 File Offset: 0x0002F946
		public void Simplify(float tolerance)
		{
			this._value.Simplify(tolerance);
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x00031754 File Offset: 0x0002F954
		public void GetMaterials(List<Material> m)
		{
			this._value.GetMaterials(m);
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00031762 File Offset: 0x0002F962
		public void GetSharedMaterials(List<Material> m)
		{
			this._value.GetSharedMaterials(m);
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00031770 File Offset: 0x0002F970
		public bool HasPropertyBlock()
		{
			return this._value.HasPropertyBlock();
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x0003177D File Offset: 0x0002F97D
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x0003178B File Offset: 0x0002F98B
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00031798 File Offset: 0x0002F998
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003169 RID: 12649
		[MoonSharpHidden]
		public LineRenderer _value;
	}
}
