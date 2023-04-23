using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009EF RID: 2543
	[Proxy(typeof(RenderSettings))]
	public class RenderSettingsProxy : IProxy
	{
		// Token: 0x06004D14 RID: 19732 RVA: 0x000378E5 File Offset: 0x00035AE5
		[MoonSharpHidden]
		public RenderSettingsProxy(RenderSettings value)
		{
			this._value = value;
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x000378F4 File Offset: 0x00035AF4
		// (set) Token: 0x06004D16 RID: 19734 RVA: 0x00037900 File Offset: 0x00035B00
		public static ColorProxy ambientEquatorColor
		{
			get
			{
				return ColorProxy.New(RenderSettings.ambientEquatorColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				RenderSettings.ambientEquatorColor = value._value;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x0003791B File Offset: 0x00035B1B
		// (set) Token: 0x06004D18 RID: 19736 RVA: 0x00037927 File Offset: 0x00035B27
		public static ColorProxy ambientGroundColor
		{
			get
			{
				return ColorProxy.New(RenderSettings.ambientGroundColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				RenderSettings.ambientGroundColor = value._value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x00037942 File Offset: 0x00035B42
		// (set) Token: 0x06004D1A RID: 19738 RVA: 0x00037949 File Offset: 0x00035B49
		public static float ambientIntensity
		{
			get
			{
				return RenderSettings.ambientIntensity;
			}
			set
			{
				RenderSettings.ambientIntensity = value;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x00037951 File Offset: 0x00035B51
		// (set) Token: 0x06004D1C RID: 19740 RVA: 0x0003795D File Offset: 0x00035B5D
		public static ColorProxy ambientLight
		{
			get
			{
				return ColorProxy.New(RenderSettings.ambientLight);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				RenderSettings.ambientLight = value._value;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x00037978 File Offset: 0x00035B78
		// (set) Token: 0x06004D1E RID: 19742 RVA: 0x00037984 File Offset: 0x00035B84
		public static ColorProxy ambientSkyColor
		{
			get
			{
				return ColorProxy.New(RenderSettings.ambientSkyColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				RenderSettings.ambientSkyColor = value._value;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x0003799F File Offset: 0x00035B9F
		public static TextureProxy customReflection
		{
			get
			{
				return TextureProxy.New(RenderSettings.customReflection);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06004D20 RID: 19744 RVA: 0x000379AB File Offset: 0x00035BAB
		// (set) Token: 0x06004D21 RID: 19745 RVA: 0x000379B2 File Offset: 0x00035BB2
		public static int defaultReflectionResolution
		{
			get
			{
				return RenderSettings.defaultReflectionResolution;
			}
			set
			{
				RenderSettings.defaultReflectionResolution = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x000379BA File Offset: 0x00035BBA
		// (set) Token: 0x06004D23 RID: 19747 RVA: 0x000379C1 File Offset: 0x00035BC1
		public static float flareFadeSpeed
		{
			get
			{
				return RenderSettings.flareFadeSpeed;
			}
			set
			{
				RenderSettings.flareFadeSpeed = value;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x000379C9 File Offset: 0x00035BC9
		// (set) Token: 0x06004D25 RID: 19749 RVA: 0x000379D0 File Offset: 0x00035BD0
		public static float flareStrength
		{
			get
			{
				return RenderSettings.flareStrength;
			}
			set
			{
				RenderSettings.flareStrength = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x000379D8 File Offset: 0x00035BD8
		// (set) Token: 0x06004D27 RID: 19751 RVA: 0x000379DF File Offset: 0x00035BDF
		public static bool fog
		{
			get
			{
				return RenderSettings.fog;
			}
			set
			{
				RenderSettings.fog = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x000379E7 File Offset: 0x00035BE7
		// (set) Token: 0x06004D29 RID: 19753 RVA: 0x000379F3 File Offset: 0x00035BF3
		public static ColorProxy fogColor
		{
			get
			{
				return ColorProxy.New(RenderSettings.fogColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				RenderSettings.fogColor = value._value;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x00037A0E File Offset: 0x00035C0E
		// (set) Token: 0x06004D2B RID: 19755 RVA: 0x00037A15 File Offset: 0x00035C15
		public static float fogDensity
		{
			get
			{
				return RenderSettings.fogDensity;
			}
			set
			{
				RenderSettings.fogDensity = value;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x00037A1D File Offset: 0x00035C1D
		// (set) Token: 0x06004D2D RID: 19757 RVA: 0x00037A24 File Offset: 0x00035C24
		public static float fogEndDistance
		{
			get
			{
				return RenderSettings.fogEndDistance;
			}
			set
			{
				RenderSettings.fogEndDistance = value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06004D2E RID: 19758 RVA: 0x00037A2C File Offset: 0x00035C2C
		// (set) Token: 0x06004D2F RID: 19759 RVA: 0x00037A33 File Offset: 0x00035C33
		public static float fogStartDistance
		{
			get
			{
				return RenderSettings.fogStartDistance;
			}
			set
			{
				RenderSettings.fogStartDistance = value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06004D30 RID: 19760 RVA: 0x00037A3B File Offset: 0x00035C3B
		// (set) Token: 0x06004D31 RID: 19761 RVA: 0x00037A42 File Offset: 0x00035C42
		public static float haloStrength
		{
			get
			{
				return RenderSettings.haloStrength;
			}
			set
			{
				RenderSettings.haloStrength = value;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06004D32 RID: 19762 RVA: 0x00037A4A File Offset: 0x00035C4A
		// (set) Token: 0x06004D33 RID: 19763 RVA: 0x00037A51 File Offset: 0x00035C51
		public static int reflectionBounces
		{
			get
			{
				return RenderSettings.reflectionBounces;
			}
			set
			{
				RenderSettings.reflectionBounces = value;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06004D34 RID: 19764 RVA: 0x00037A59 File Offset: 0x00035C59
		// (set) Token: 0x06004D35 RID: 19765 RVA: 0x00037A60 File Offset: 0x00035C60
		public static float reflectionIntensity
		{
			get
			{
				return RenderSettings.reflectionIntensity;
			}
			set
			{
				RenderSettings.reflectionIntensity = value;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06004D36 RID: 19766 RVA: 0x00037A68 File Offset: 0x00035C68
		// (set) Token: 0x06004D37 RID: 19767 RVA: 0x001379A8 File Offset: 0x00135BA8
		public static MaterialProxy skybox
		{
			get
			{
				return MaterialProxy.New(RenderSettings.skybox);
			}
			set
			{
				Material skybox = null;
				if (value != null)
				{
					skybox = value._value;
				}
				RenderSettings.skybox = skybox;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004D38 RID: 19768 RVA: 0x00037A74 File Offset: 0x00035C74
		// (set) Token: 0x06004D39 RID: 19769 RVA: 0x00037A80 File Offset: 0x00035C80
		public static ColorProxy subtractiveShadowColor
		{
			get
			{
				return ColorProxy.New(RenderSettings.subtractiveShadowColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				RenderSettings.subtractiveShadowColor = value._value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x00037A9B File Offset: 0x00035C9B
		// (set) Token: 0x06004D3B RID: 19771 RVA: 0x001379C8 File Offset: 0x00135BC8
		public static LightProxy sun
		{
			get
			{
				return LightProxy.New(RenderSettings.sun);
			}
			set
			{
				Light sun = null;
				if (value != null)
				{
					sun = value._value;
				}
				RenderSettings.sun = sun;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x00037AA7 File Offset: 0x00035CA7
		// (set) Token: 0x06004D3D RID: 19773 RVA: 0x00037AB4 File Offset: 0x00035CB4
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

		// Token: 0x06004D3E RID: 19774 RVA: 0x00037AC2 File Offset: 0x00035CC2
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x001379E8 File Offset: 0x00135BE8
		[MoonSharpHidden]
		public static RenderSettingsProxy New(RenderSettings value)
		{
			if (value == null)
			{
				return null;
			}
			RenderSettingsProxy renderSettingsProxy = (RenderSettingsProxy)ObjectCache.Get(typeof(RenderSettingsProxy), value);
			if (renderSettingsProxy == null)
			{
				renderSettingsProxy = new RenderSettingsProxy(value);
				ObjectCache.Add(typeof(RenderSettingsProxy), value, renderSettingsProxy);
			}
			return renderSettingsProxy;
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00037ACA File Offset: 0x00035CCA
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00037AD7 File Offset: 0x00035CD7
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400327F RID: 12927
		[MoonSharpHidden]
		public RenderSettings _value;
	}
}
