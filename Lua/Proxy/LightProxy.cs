using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009D0 RID: 2512
	[Proxy(typeof(Light))]
	public class LightProxy : IProxy
	{
		// Token: 0x06004632 RID: 17970 RVA: 0x00030EB9 File Offset: 0x0002F0B9
		[MoonSharpHidden]
		public LightProxy(Light value)
		{
			this._value = value;
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x00030EC8 File Offset: 0x0002F0C8
		public LightProxy()
		{
			this._value = new Light();
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x00030EDB File Offset: 0x0002F0DB
		// (set) Token: 0x06004635 RID: 17973 RVA: 0x00030EE8 File Offset: 0x0002F0E8
		public float bounceIntensity
		{
			get
			{
				return this._value.bounceIntensity;
			}
			set
			{
				this._value.bounceIntensity = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x00030EF6 File Offset: 0x0002F0F6
		// (set) Token: 0x06004637 RID: 17975 RVA: 0x00030F08 File Offset: 0x0002F108
		public Vector4Proxy boundingSphereOverride
		{
			get
			{
				return Vector4Proxy.New(this._value.boundingSphereOverride);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.boundingSphereOverride = value._value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x00030F29 File Offset: 0x0002F129
		// (set) Token: 0x06004639 RID: 17977 RVA: 0x00030F3B File Offset: 0x0002F13B
		public ColorProxy color
		{
			get
			{
				return ColorProxy.New(this._value.color);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.color = value._value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x00030F5C File Offset: 0x0002F15C
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x00030F69 File Offset: 0x0002F169
		public float colorTemperature
		{
			get
			{
				return this._value.colorTemperature;
			}
			set
			{
				this._value.colorTemperature = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x00030F77 File Offset: 0x0002F177
		public int commandBufferCount
		{
			get
			{
				return this._value.commandBufferCount;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x00030F84 File Offset: 0x0002F184
		// (set) Token: 0x0600463E RID: 17982 RVA: 0x00130DE4 File Offset: 0x0012EFE4
		public TextureProxy cookie
		{
			get
			{
				return TextureProxy.New(this._value.cookie);
			}
			set
			{
				Texture cookie = null;
				if (value != null)
				{
					cookie = value._value;
				}
				this._value.cookie = cookie;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x00030F96 File Offset: 0x0002F196
		// (set) Token: 0x06004640 RID: 17984 RVA: 0x00030FA3 File Offset: 0x0002F1A3
		public float cookieSize
		{
			get
			{
				return this._value.cookieSize;
			}
			set
			{
				this._value.cookieSize = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x00030FB1 File Offset: 0x0002F1B1
		// (set) Token: 0x06004642 RID: 17986 RVA: 0x00030FBE File Offset: 0x0002F1BE
		public int cullingMask
		{
			get
			{
				return this._value.cullingMask;
			}
			set
			{
				this._value.cullingMask = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x00030FCC File Offset: 0x0002F1CC
		// (set) Token: 0x06004644 RID: 17988 RVA: 0x00030FD9 File Offset: 0x0002F1D9
		public float innerSpotAngle
		{
			get
			{
				return this._value.innerSpotAngle;
			}
			set
			{
				this._value.innerSpotAngle = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x00030FE7 File Offset: 0x0002F1E7
		// (set) Token: 0x06004646 RID: 17990 RVA: 0x00030FF4 File Offset: 0x0002F1F4
		public float intensity
		{
			get
			{
				return this._value.intensity;
			}
			set
			{
				this._value.intensity = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x00031002 File Offset: 0x0002F202
		// (set) Token: 0x06004648 RID: 17992 RVA: 0x0003100F File Offset: 0x0002F20F
		public float[] layerShadowCullDistances
		{
			get
			{
				return this._value.layerShadowCullDistances;
			}
			set
			{
				this._value.layerShadowCullDistances = value;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x0003101D File Offset: 0x0002F21D
		// (set) Token: 0x0600464A RID: 17994 RVA: 0x0003102A File Offset: 0x0002F22A
		public float range
		{
			get
			{
				return this._value.range;
			}
			set
			{
				this._value.range = value;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x00031038 File Offset: 0x0002F238
		// (set) Token: 0x0600464C RID: 17996 RVA: 0x00031045 File Offset: 0x0002F245
		public int renderingLayerMask
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

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x00031053 File Offset: 0x0002F253
		// (set) Token: 0x0600464E RID: 17998 RVA: 0x00031060 File Offset: 0x0002F260
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

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x0003106E File Offset: 0x0002F26E
		// (set) Token: 0x06004650 RID: 18000 RVA: 0x0003107B File Offset: 0x0002F27B
		public int shadowCustomResolution
		{
			get
			{
				return this._value.shadowCustomResolution;
			}
			set
			{
				this._value.shadowCustomResolution = value;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x00031089 File Offset: 0x0002F289
		// (set) Token: 0x06004652 RID: 18002 RVA: 0x0003109B File Offset: 0x0002F29B
		public Matrix4x4Proxy shadowMatrixOverride
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.shadowMatrixOverride);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.shadowMatrixOverride = value._value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x000310BC File Offset: 0x0002F2BC
		// (set) Token: 0x06004654 RID: 18004 RVA: 0x000310C9 File Offset: 0x0002F2C9
		public float shadowNearPlane
		{
			get
			{
				return this._value.shadowNearPlane;
			}
			set
			{
				this._value.shadowNearPlane = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x000310D7 File Offset: 0x0002F2D7
		// (set) Token: 0x06004656 RID: 18006 RVA: 0x000310E4 File Offset: 0x0002F2E4
		public float shadowNormalBias
		{
			get
			{
				return this._value.shadowNormalBias;
			}
			set
			{
				this._value.shadowNormalBias = value;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x000310F2 File Offset: 0x0002F2F2
		// (set) Token: 0x06004658 RID: 18008 RVA: 0x000310FF File Offset: 0x0002F2FF
		public float shadowStrength
		{
			get
			{
				return this._value.shadowStrength;
			}
			set
			{
				this._value.shadowStrength = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x0003110D File Offset: 0x0002F30D
		// (set) Token: 0x0600465A RID: 18010 RVA: 0x0003111A File Offset: 0x0002F31A
		public float spotAngle
		{
			get
			{
				return this._value.spotAngle;
			}
			set
			{
				this._value.spotAngle = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x00031128 File Offset: 0x0002F328
		// (set) Token: 0x0600465C RID: 18012 RVA: 0x00031135 File Offset: 0x0002F335
		public bool useBoundingSphereOverride
		{
			get
			{
				return this._value.useBoundingSphereOverride;
			}
			set
			{
				this._value.useBoundingSphereOverride = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x00031143 File Offset: 0x0002F343
		// (set) Token: 0x0600465E RID: 18014 RVA: 0x00031150 File Offset: 0x0002F350
		public bool useColorTemperature
		{
			get
			{
				return this._value.useColorTemperature;
			}
			set
			{
				this._value.useColorTemperature = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x0003115E File Offset: 0x0002F35E
		// (set) Token: 0x06004660 RID: 18016 RVA: 0x0003116B File Offset: 0x0002F36B
		public bool useShadowMatrixOverride
		{
			get
			{
				return this._value.useShadowMatrixOverride;
			}
			set
			{
				this._value.useShadowMatrixOverride = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x00031179 File Offset: 0x0002F379
		// (set) Token: 0x06004662 RID: 18018 RVA: 0x00031186 File Offset: 0x0002F386
		public bool useViewFrustumForShadowCasterCull
		{
			get
			{
				return this._value.useViewFrustumForShadowCasterCull;
			}
			set
			{
				this._value.useViewFrustumForShadowCasterCull = value;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x00031194 File Offset: 0x0002F394
		// (set) Token: 0x06004664 RID: 18020 RVA: 0x000311A1 File Offset: 0x0002F3A1
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

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06004665 RID: 18021 RVA: 0x000311AF File Offset: 0x0002F3AF
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x000311BC File Offset: 0x0002F3BC
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06004667 RID: 18023 RVA: 0x000311CE File Offset: 0x0002F3CE
		// (set) Token: 0x06004668 RID: 18024 RVA: 0x000311DB File Offset: 0x0002F3DB
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

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x000311E9 File Offset: 0x0002F3E9
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600466A RID: 18026 RVA: 0x000311FB File Offset: 0x0002F3FB
		// (set) Token: 0x0600466B RID: 18027 RVA: 0x00031208 File Offset: 0x0002F408
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

		// Token: 0x0600466C RID: 18028 RVA: 0x00031216 File Offset: 0x0002F416
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x00130E0C File Offset: 0x0012F00C
		[MoonSharpHidden]
		public static LightProxy New(Light value)
		{
			if (value == null)
			{
				return null;
			}
			LightProxy lightProxy = (LightProxy)ObjectCache.Get(typeof(LightProxy), value);
			if (lightProxy == null)
			{
				lightProxy = new LightProxy(value);
				ObjectCache.Add(typeof(LightProxy), value, lightProxy);
			}
			return lightProxy;
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x0003121E File Offset: 0x0002F41E
		[MoonSharpUserDataMetamethod("__call")]
		public static LightProxy Call(DynValue _)
		{
			return new LightProxy();
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x00031225 File Offset: 0x0002F425
		public void RemoveAllCommandBuffers()
		{
			this._value.RemoveAllCommandBuffers();
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x00031232 File Offset: 0x0002F432
		public void Reset()
		{
			this._value.Reset();
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x0003123F File Offset: 0x0002F43F
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x0003124D File Offset: 0x0002F44D
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x0003125A File Offset: 0x0002F45A
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003168 RID: 12648
		[MoonSharpHidden]
		public Light _value;
	}
}
