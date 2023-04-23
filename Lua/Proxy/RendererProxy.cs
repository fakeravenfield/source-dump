using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009F0 RID: 2544
	[Proxy(typeof(Renderer))]
	public class RendererProxy : IProxy
	{
		// Token: 0x06004D42 RID: 19778 RVA: 0x00037AE4 File Offset: 0x00035CE4
		[MoonSharpHidden]
		public RendererProxy(Renderer value)
		{
			this._value = value;
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00037AF3 File Offset: 0x00035CF3
		public RendererProxy()
		{
			this._value = new Renderer();
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x00037B06 File Offset: 0x00035D06
		// (set) Token: 0x06004D45 RID: 19781 RVA: 0x00037B13 File Offset: 0x00035D13
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

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06004D46 RID: 19782 RVA: 0x00037B21 File Offset: 0x00035D21
		public BoundsProxy bounds
		{
			get
			{
				return BoundsProxy.New(this._value.bounds);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x00037B33 File Offset: 0x00035D33
		// (set) Token: 0x06004D48 RID: 19784 RVA: 0x00037B40 File Offset: 0x00035D40
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

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06004D49 RID: 19785 RVA: 0x00037B4E File Offset: 0x00035D4E
		// (set) Token: 0x06004D4A RID: 19786 RVA: 0x00037B5B File Offset: 0x00035D5B
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

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x00037B69 File Offset: 0x00035D69
		public bool isPartOfStaticBatch
		{
			get
			{
				return this._value.isPartOfStaticBatch;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06004D4C RID: 19788 RVA: 0x00037B76 File Offset: 0x00035D76
		public bool isVisible
		{
			get
			{
				return this._value.isVisible;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06004D4D RID: 19789 RVA: 0x00037B83 File Offset: 0x00035D83
		// (set) Token: 0x06004D4E RID: 19790 RVA: 0x00037B90 File Offset: 0x00035D90
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06004D4F RID: 19791 RVA: 0x00037B9E File Offset: 0x00035D9E
		// (set) Token: 0x06004D50 RID: 19792 RVA: 0x00037BB0 File Offset: 0x00035DB0
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

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x00037BD1 File Offset: 0x00035DD1
		// (set) Token: 0x06004D52 RID: 19794 RVA: 0x00137A34 File Offset: 0x00135C34
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x00037BE3 File Offset: 0x00035DE3
		public Matrix4x4Proxy localToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.localToWorldMatrix);
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x00037BF5 File Offset: 0x00035DF5
		// (set) Token: 0x06004D55 RID: 19797 RVA: 0x00137A5C File Offset: 0x00135C5C
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

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x00037C07 File Offset: 0x00035E07
		// (set) Token: 0x06004D57 RID: 19799 RVA: 0x00037C14 File Offset: 0x00035E14
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

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004D58 RID: 19800 RVA: 0x00037C22 File Offset: 0x00035E22
		// (set) Token: 0x06004D59 RID: 19801 RVA: 0x00137A84 File Offset: 0x00135C84
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x00037C34 File Offset: 0x00035E34
		// (set) Token: 0x06004D5B RID: 19803 RVA: 0x00037C41 File Offset: 0x00035E41
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

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x00037C4F File Offset: 0x00035E4F
		// (set) Token: 0x06004D5D RID: 19805 RVA: 0x00037C61 File Offset: 0x00035E61
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

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06004D5E RID: 19806 RVA: 0x00037C82 File Offset: 0x00035E82
		// (set) Token: 0x06004D5F RID: 19807 RVA: 0x00037C8F File Offset: 0x00035E8F
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

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x00037C9D File Offset: 0x00035E9D
		// (set) Token: 0x06004D61 RID: 19809 RVA: 0x00037CAA File Offset: 0x00035EAA
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

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x00037CB8 File Offset: 0x00035EB8
		// (set) Token: 0x06004D63 RID: 19811 RVA: 0x00037CC5 File Offset: 0x00035EC5
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

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x00037CD3 File Offset: 0x00035ED3
		// (set) Token: 0x06004D65 RID: 19813 RVA: 0x00137AAC File Offset: 0x00135CAC
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

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x00037CE5 File Offset: 0x00035EE5
		// (set) Token: 0x06004D67 RID: 19815 RVA: 0x00037CF2 File Offset: 0x00035EF2
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

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x00037D00 File Offset: 0x00035F00
		// (set) Token: 0x06004D69 RID: 19817 RVA: 0x00037D0D File Offset: 0x00035F0D
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

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x00037D1B File Offset: 0x00035F1B
		// (set) Token: 0x06004D6B RID: 19819 RVA: 0x00037D28 File Offset: 0x00035F28
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

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x00037D36 File Offset: 0x00035F36
		// (set) Token: 0x06004D6D RID: 19821 RVA: 0x00037D43 File Offset: 0x00035F43
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

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x00037D51 File Offset: 0x00035F51
		public Matrix4x4Proxy worldToLocalMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToLocalMatrix);
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x00037D63 File Offset: 0x00035F63
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x00037D75 File Offset: 0x00035F75
		// (set) Token: 0x06004D71 RID: 19825 RVA: 0x00037D82 File Offset: 0x00035F82
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

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x00037D90 File Offset: 0x00035F90
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x00037DA2 File Offset: 0x00035FA2
		// (set) Token: 0x06004D74 RID: 19828 RVA: 0x00037DAF File Offset: 0x00035FAF
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

		// Token: 0x06004D75 RID: 19829 RVA: 0x00037DBD File Offset: 0x00035FBD
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00137AD4 File Offset: 0x00135CD4
		[MoonSharpHidden]
		public static RendererProxy New(Renderer value)
		{
			if (value == null)
			{
				return null;
			}
			RendererProxy rendererProxy = (RendererProxy)ObjectCache.Get(typeof(RendererProxy), value);
			if (rendererProxy == null)
			{
				rendererProxy = new RendererProxy(value);
				ObjectCache.Add(typeof(RendererProxy), value, rendererProxy);
			}
			return rendererProxy;
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x00037DC5 File Offset: 0x00035FC5
		[MoonSharpUserDataMetamethod("__call")]
		public static RendererProxy Call(DynValue _)
		{
			return new RendererProxy();
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00037DCC File Offset: 0x00035FCC
		public void GetMaterials(List<Material> m)
		{
			this._value.GetMaterials(m);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x00037DDA File Offset: 0x00035FDA
		public void GetSharedMaterials(List<Material> m)
		{
			this._value.GetSharedMaterials(m);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00037DE8 File Offset: 0x00035FE8
		public bool HasPropertyBlock()
		{
			return this._value.HasPropertyBlock();
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x00037DF5 File Offset: 0x00035FF5
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00037E03 File Offset: 0x00036003
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00037E10 File Offset: 0x00036010
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003280 RID: 12928
		[MoonSharpHidden]
		public Renderer _value;
	}
}
