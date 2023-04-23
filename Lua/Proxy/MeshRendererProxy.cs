using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009DA RID: 2522
	[Proxy(typeof(MeshRenderer))]
	public class MeshRendererProxy : IProxy
	{
		// Token: 0x06004879 RID: 18553 RVA: 0x00032FA4 File Offset: 0x000311A4
		[MoonSharpHidden]
		public MeshRendererProxy(MeshRenderer value)
		{
			this._value = value;
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x00032FB3 File Offset: 0x000311B3
		public MeshRendererProxy()
		{
			this._value = new MeshRenderer();
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x00032FC6 File Offset: 0x000311C6
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x001314CC File Offset: 0x0012F6CC
		public MeshProxy additionalVertexStreams
		{
			get
			{
				return MeshProxy.New(this._value.additionalVertexStreams);
			}
			set
			{
				Mesh additionalVertexStreams = null;
				if (value != null)
				{
					additionalVertexStreams = value._value;
				}
				this._value.additionalVertexStreams = additionalVertexStreams;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x00032FD8 File Offset: 0x000311D8
		// (set) Token: 0x0600487E RID: 18558 RVA: 0x001314F4 File Offset: 0x0012F6F4
		public MeshProxy enlightenVertexStream
		{
			get
			{
				return MeshProxy.New(this._value.enlightenVertexStream);
			}
			set
			{
				Mesh enlightenVertexStream = null;
				if (value != null)
				{
					enlightenVertexStream = value._value;
				}
				this._value.enlightenVertexStream = enlightenVertexStream;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x00032FEA File Offset: 0x000311EA
		public int subMeshStartIndex
		{
			get
			{
				return this._value.subMeshStartIndex;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x00032FF7 File Offset: 0x000311F7
		// (set) Token: 0x06004881 RID: 18561 RVA: 0x00033004 File Offset: 0x00031204
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

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x00033012 File Offset: 0x00031212
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x00033024 File Offset: 0x00031224
		// (set) Token: 0x06004884 RID: 18564 RVA: 0x00033031 File Offset: 0x00031231
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

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x0003303F File Offset: 0x0003123F
		// (set) Token: 0x06004886 RID: 18566 RVA: 0x0003304C File Offset: 0x0003124C
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

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x0003305A File Offset: 0x0003125A
		public bool isPartOfStaticBatch
		{
			get
			{
				return this._value.isPartOfStaticBatch;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x00033067 File Offset: 0x00031267
		public bool isVisible
		{
			get
			{
				return this._value.isVisible;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x00033074 File Offset: 0x00031274
		// (set) Token: 0x0600488A RID: 18570 RVA: 0x00033081 File Offset: 0x00031281
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

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x0003308F File Offset: 0x0003128F
		// (set) Token: 0x0600488C RID: 18572 RVA: 0x000330A1 File Offset: 0x000312A1
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

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x000330C2 File Offset: 0x000312C2
		// (set) Token: 0x0600488E RID: 18574 RVA: 0x0013151C File Offset: 0x0012F71C
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

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000330D4 File Offset: 0x000312D4
		public Matrix4x4Proxy localToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.localToWorldMatrix);
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x000330E6 File Offset: 0x000312E6
		// (set) Token: 0x06004891 RID: 18577 RVA: 0x00131544 File Offset: 0x0012F744
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

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x000330F8 File Offset: 0x000312F8
		// (set) Token: 0x06004893 RID: 18579 RVA: 0x00033105 File Offset: 0x00031305
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

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x00033113 File Offset: 0x00031313
		// (set) Token: 0x06004895 RID: 18581 RVA: 0x0013156C File Offset: 0x0012F76C
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

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x00033125 File Offset: 0x00031325
		// (set) Token: 0x06004897 RID: 18583 RVA: 0x00033132 File Offset: 0x00031332
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

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x00033140 File Offset: 0x00031340
		// (set) Token: 0x06004899 RID: 18585 RVA: 0x00033152 File Offset: 0x00031352
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

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x00033173 File Offset: 0x00031373
		// (set) Token: 0x0600489B RID: 18587 RVA: 0x00033180 File Offset: 0x00031380
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

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600489C RID: 18588 RVA: 0x0003318E File Offset: 0x0003138E
		// (set) Token: 0x0600489D RID: 18589 RVA: 0x0003319B File Offset: 0x0003139B
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

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600489E RID: 18590 RVA: 0x000331A9 File Offset: 0x000313A9
		// (set) Token: 0x0600489F RID: 18591 RVA: 0x000331B6 File Offset: 0x000313B6
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

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060048A0 RID: 18592 RVA: 0x000331C4 File Offset: 0x000313C4
		// (set) Token: 0x060048A1 RID: 18593 RVA: 0x00131594 File Offset: 0x0012F794
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

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060048A2 RID: 18594 RVA: 0x000331D6 File Offset: 0x000313D6
		// (set) Token: 0x060048A3 RID: 18595 RVA: 0x000331E3 File Offset: 0x000313E3
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

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060048A4 RID: 18596 RVA: 0x000331F1 File Offset: 0x000313F1
		// (set) Token: 0x060048A5 RID: 18597 RVA: 0x000331FE File Offset: 0x000313FE
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

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060048A6 RID: 18598 RVA: 0x0003320C File Offset: 0x0003140C
		// (set) Token: 0x060048A7 RID: 18599 RVA: 0x00033219 File Offset: 0x00031419
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

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060048A8 RID: 18600 RVA: 0x00033227 File Offset: 0x00031427
		// (set) Token: 0x060048A9 RID: 18601 RVA: 0x00033234 File Offset: 0x00031434
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

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x00033242 File Offset: 0x00031442
		public Matrix4x4Proxy worldToLocalMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToLocalMatrix);
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060048AB RID: 18603 RVA: 0x00033254 File Offset: 0x00031454
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x00033266 File Offset: 0x00031466
		// (set) Token: 0x060048AD RID: 18605 RVA: 0x00033273 File Offset: 0x00031473
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

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x00033281 File Offset: 0x00031481
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060048AF RID: 18607 RVA: 0x00033293 File Offset: 0x00031493
		// (set) Token: 0x060048B0 RID: 18608 RVA: 0x000332A0 File Offset: 0x000314A0
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

		// Token: 0x060048B1 RID: 18609 RVA: 0x000332AE File Offset: 0x000314AE
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x001315BC File Offset: 0x0012F7BC
		[MoonSharpHidden]
		public static MeshRendererProxy New(MeshRenderer value)
		{
			if (value == null)
			{
				return null;
			}
			MeshRendererProxy meshRendererProxy = (MeshRendererProxy)ObjectCache.Get(typeof(MeshRendererProxy), value);
			if (meshRendererProxy == null)
			{
				meshRendererProxy = new MeshRendererProxy(value);
				ObjectCache.Add(typeof(MeshRendererProxy), value, meshRendererProxy);
			}
			return meshRendererProxy;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x000332B6 File Offset: 0x000314B6
		[MoonSharpUserDataMetamethod("__call")]
		public static MeshRendererProxy Call(DynValue _)
		{
			return new MeshRendererProxy();
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x000332BD File Offset: 0x000314BD
		public void GetMaterials(List<Material> m)
		{
			this._value.GetMaterials(m);
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x000332CB File Offset: 0x000314CB
		public void GetSharedMaterials(List<Material> m)
		{
			this._value.GetSharedMaterials(m);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x000332D9 File Offset: 0x000314D9
		public bool HasPropertyBlock()
		{
			return this._value.HasPropertyBlock();
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x000332E6 File Offset: 0x000314E6
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x000332F4 File Offset: 0x000314F4
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x00033301 File Offset: 0x00031501
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003172 RID: 12658
		[MoonSharpHidden]
		public MeshRenderer _value;
	}
}
