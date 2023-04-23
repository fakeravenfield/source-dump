using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B8 RID: 2488
	[Proxy(typeof(Camera))]
	public class CameraProxy : IProxy
	{
		// Token: 0x06004273 RID: 17011 RVA: 0x0002D96E File Offset: 0x0002BB6E
		[MoonSharpHidden]
		public CameraProxy(Camera value)
		{
			this._value = value;
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x0002D97D File Offset: 0x0002BB7D
		public CameraProxy()
		{
			this._value = new Camera();
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x0002D990 File Offset: 0x0002BB90
		public TextureProxy activeTexture
		{
			get
			{
				return TextureProxy.New(this._value.activeTexture);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x0002D9A2 File Offset: 0x0002BBA2
		public static Camera[] allCameras
		{
			get
			{
				return Camera.allCameras;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x0002D9A9 File Offset: 0x0002BBA9
		public static int allCamerasCount
		{
			get
			{
				return Camera.allCamerasCount;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		// (set) Token: 0x06004279 RID: 17017 RVA: 0x0002D9BD File Offset: 0x0002BBBD
		public bool allowDynamicResolution
		{
			get
			{
				return this._value.allowDynamicResolution;
			}
			set
			{
				this._value.allowDynamicResolution = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x0002D9CB File Offset: 0x0002BBCB
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x0002D9D8 File Offset: 0x0002BBD8
		public bool allowHDR
		{
			get
			{
				return this._value.allowHDR;
			}
			set
			{
				this._value.allowHDR = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x0002D9E6 File Offset: 0x0002BBE6
		// (set) Token: 0x0600427D RID: 17021 RVA: 0x0002D9F3 File Offset: 0x0002BBF3
		public bool allowMSAA
		{
			get
			{
				return this._value.allowMSAA;
			}
			set
			{
				this._value.allowMSAA = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x0002DA01 File Offset: 0x0002BC01
		public bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			get
			{
				return this._value.areVRStereoViewMatricesWithinSingleCullTolerance;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x0002DA0E File Offset: 0x0002BC0E
		// (set) Token: 0x06004280 RID: 17024 RVA: 0x0002DA1B File Offset: 0x0002BC1B
		public float aspect
		{
			get
			{
				return this._value.aspect;
			}
			set
			{
				this._value.aspect = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x0002DA29 File Offset: 0x0002BC29
		// (set) Token: 0x06004282 RID: 17026 RVA: 0x0002DA3B File Offset: 0x0002BC3B
		public ColorProxy backgroundColor
		{
			get
			{
				return ColorProxy.New(this._value.backgroundColor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.backgroundColor = value._value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		public Matrix4x4Proxy cameraToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.cameraToWorldMatrix);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x0002DA6E File Offset: 0x0002BC6E
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x0002DA7B File Offset: 0x0002BC7B
		public bool clearStencilAfterLightingPass
		{
			get
			{
				return this._value.clearStencilAfterLightingPass;
			}
			set
			{
				this._value.clearStencilAfterLightingPass = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x0002DA89 File Offset: 0x0002BC89
		public int commandBufferCount
		{
			get
			{
				return this._value.commandBufferCount;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x0002DA96 File Offset: 0x0002BC96
		// (set) Token: 0x06004288 RID: 17032 RVA: 0x0002DAA3 File Offset: 0x0002BCA3
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

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x0002DAB1 File Offset: 0x0002BCB1
		// (set) Token: 0x0600428A RID: 17034 RVA: 0x0002DAC3 File Offset: 0x0002BCC3
		public Matrix4x4Proxy cullingMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.cullingMatrix);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.cullingMatrix = value._value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x0002DAE4 File Offset: 0x0002BCE4
		public static CameraProxy current
		{
			get
			{
				return CameraProxy.New(Camera.current);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x0002DAF0 File Offset: 0x0002BCF0
		// (set) Token: 0x0600428D RID: 17037 RVA: 0x0002DAFD File Offset: 0x0002BCFD
		public float depth
		{
			get
			{
				return this._value.depth;
			}
			set
			{
				this._value.depth = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x0002DB0B File Offset: 0x0002BD0B
		// (set) Token: 0x0600428F RID: 17039 RVA: 0x0002DB18 File Offset: 0x0002BD18
		public int eventMask
		{
			get
			{
				return this._value.eventMask;
			}
			set
			{
				this._value.eventMask = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06004290 RID: 17040 RVA: 0x0002DB26 File Offset: 0x0002BD26
		// (set) Token: 0x06004291 RID: 17041 RVA: 0x0002DB33 File Offset: 0x0002BD33
		public float farClipPlane
		{
			get
			{
				return this._value.farClipPlane;
			}
			set
			{
				this._value.farClipPlane = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06004292 RID: 17042 RVA: 0x0002DB41 File Offset: 0x0002BD41
		// (set) Token: 0x06004293 RID: 17043 RVA: 0x0002DB4E File Offset: 0x0002BD4E
		public float fieldOfView
		{
			get
			{
				return this._value.fieldOfView;
			}
			set
			{
				this._value.fieldOfView = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x0002DB5C File Offset: 0x0002BD5C
		// (set) Token: 0x06004295 RID: 17045 RVA: 0x0002DB69 File Offset: 0x0002BD69
		public float focalLength
		{
			get
			{
				return this._value.focalLength;
			}
			set
			{
				this._value.focalLength = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x0002DB77 File Offset: 0x0002BD77
		// (set) Token: 0x06004297 RID: 17047 RVA: 0x0002DB84 File Offset: 0x0002BD84
		public bool forceIntoRenderTexture
		{
			get
			{
				return this._value.forceIntoRenderTexture;
			}
			set
			{
				this._value.forceIntoRenderTexture = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x0002DB92 File Offset: 0x0002BD92
		// (set) Token: 0x06004299 RID: 17049 RVA: 0x0002DB9F File Offset: 0x0002BD9F
		public float[] layerCullDistances
		{
			get
			{
				return this._value.layerCullDistances;
			}
			set
			{
				this._value.layerCullDistances = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x0002DBAD File Offset: 0x0002BDAD
		// (set) Token: 0x0600429B RID: 17051 RVA: 0x0002DBBA File Offset: 0x0002BDBA
		public bool layerCullSpherical
		{
			get
			{
				return this._value.layerCullSpherical;
			}
			set
			{
				this._value.layerCullSpherical = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		// (set) Token: 0x0600429D RID: 17053 RVA: 0x0002DBDA File Offset: 0x0002BDDA
		public Vector2Proxy lensShift
		{
			get
			{
				return Vector2Proxy.New(this._value.lensShift);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.lensShift = value._value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x0002DBFB File Offset: 0x0002BDFB
		public static CameraProxy main
		{
			get
			{
				return CameraProxy.New(Camera.main);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x0002DC07 File Offset: 0x0002BE07
		// (set) Token: 0x060042A0 RID: 17056 RVA: 0x0002DC14 File Offset: 0x0002BE14
		public float nearClipPlane
		{
			get
			{
				return this._value.nearClipPlane;
			}
			set
			{
				this._value.nearClipPlane = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0002DC22 File Offset: 0x0002BE22
		// (set) Token: 0x060042A2 RID: 17058 RVA: 0x0002DC34 File Offset: 0x0002BE34
		public Matrix4x4Proxy nonJitteredProjectionMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.nonJitteredProjectionMatrix);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.nonJitteredProjectionMatrix = value._value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x0002DC55 File Offset: 0x0002BE55
		// (set) Token: 0x060042A4 RID: 17060 RVA: 0x0002DC62 File Offset: 0x0002BE62
		public bool orthographic
		{
			get
			{
				return this._value.orthographic;
			}
			set
			{
				this._value.orthographic = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x0002DC70 File Offset: 0x0002BE70
		// (set) Token: 0x060042A6 RID: 17062 RVA: 0x0002DC7D File Offset: 0x0002BE7D
		public float orthographicSize
		{
			get
			{
				return this._value.orthographicSize;
			}
			set
			{
				this._value.orthographicSize = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x0002DC8B File Offset: 0x0002BE8B
		// (set) Token: 0x060042A8 RID: 17064 RVA: 0x0002DC98 File Offset: 0x0002BE98
		public ulong overrideSceneCullingMask
		{
			get
			{
				return this._value.overrideSceneCullingMask;
			}
			set
			{
				this._value.overrideSceneCullingMask = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x0002DCA6 File Offset: 0x0002BEA6
		public int pixelHeight
		{
			get
			{
				return this._value.pixelHeight;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x0002DCB3 File Offset: 0x0002BEB3
		// (set) Token: 0x060042AB RID: 17067 RVA: 0x0002DCC5 File Offset: 0x0002BEC5
		public RectProxy pixelRect
		{
			get
			{
				return RectProxy.New(this._value.pixelRect);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.pixelRect = value._value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x0002DCE6 File Offset: 0x0002BEE6
		public int pixelWidth
		{
			get
			{
				return this._value.pixelWidth;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x0002DCF3 File Offset: 0x0002BEF3
		public Matrix4x4Proxy previousViewProjectionMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.previousViewProjectionMatrix);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x0002DD05 File Offset: 0x0002BF05
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x0002DD17 File Offset: 0x0002BF17
		public Matrix4x4Proxy projectionMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.projectionMatrix);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.projectionMatrix = value._value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x0002DD38 File Offset: 0x0002BF38
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x0002DD4A File Offset: 0x0002BF4A
		public RectProxy rect
		{
			get
			{
				return RectProxy.New(this._value.rect);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.rect = value._value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x0002DD6B File Offset: 0x0002BF6B
		public int scaledPixelHeight
		{
			get
			{
				return this._value.scaledPixelHeight;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x0002DD78 File Offset: 0x0002BF78
		public int scaledPixelWidth
		{
			get
			{
				return this._value.scaledPixelWidth;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x0002DD85 File Offset: 0x0002BF85
		// (set) Token: 0x060042B5 RID: 17077 RVA: 0x0002DD97 File Offset: 0x0002BF97
		public Vector2Proxy sensorSize
		{
			get
			{
				return Vector2Proxy.New(this._value.sensorSize);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.sensorSize = value._value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public float stereoConvergence
		{
			get
			{
				return this._value.stereoConvergence;
			}
			set
			{
				this._value.stereoConvergence = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x0002DDD3 File Offset: 0x0002BFD3
		public bool stereoEnabled
		{
			get
			{
				return this._value.stereoEnabled;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060042B9 RID: 17081 RVA: 0x0002DDE0 File Offset: 0x0002BFE0
		// (set) Token: 0x060042BA RID: 17082 RVA: 0x0002DDED File Offset: 0x0002BFED
		public float stereoSeparation
		{
			get
			{
				return this._value.stereoSeparation;
			}
			set
			{
				this._value.stereoSeparation = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x0002DDFB File Offset: 0x0002BFFB
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x0002DE08 File Offset: 0x0002C008
		public int targetDisplay
		{
			get
			{
				return this._value.targetDisplay;
			}
			set
			{
				this._value.targetDisplay = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x0002DE16 File Offset: 0x0002C016
		public TextureProxy targetTexture
		{
			get
			{
				return TextureProxy.New(this._value.targetTexture);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x0002DE28 File Offset: 0x0002C028
		// (set) Token: 0x060042BF RID: 17087 RVA: 0x0002DE3A File Offset: 0x0002C03A
		public Vector3Proxy transparencySortAxis
		{
			get
			{
				return Vector3Proxy.New(this._value.transparencySortAxis);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.transparencySortAxis = value._value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x0002DE5B File Offset: 0x0002C05B
		// (set) Token: 0x060042C1 RID: 17089 RVA: 0x0002DE68 File Offset: 0x0002C068
		public bool useJitteredProjectionMatrixForTransparentRendering
		{
			get
			{
				return this._value.useJitteredProjectionMatrixForTransparentRendering;
			}
			set
			{
				this._value.useJitteredProjectionMatrixForTransparentRendering = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x0002DE76 File Offset: 0x0002C076
		// (set) Token: 0x060042C3 RID: 17091 RVA: 0x0002DE83 File Offset: 0x0002C083
		public bool useOcclusionCulling
		{
			get
			{
				return this._value.useOcclusionCulling;
			}
			set
			{
				this._value.useOcclusionCulling = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x0002DE91 File Offset: 0x0002C091
		// (set) Token: 0x060042C5 RID: 17093 RVA: 0x0002DE9E File Offset: 0x0002C09E
		public bool usePhysicalProperties
		{
			get
			{
				return this._value.usePhysicalProperties;
			}
			set
			{
				this._value.usePhysicalProperties = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		public Vector3Proxy velocity
		{
			get
			{
				return Vector3Proxy.New(this._value.velocity);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x0002DEBE File Offset: 0x0002C0BE
		// (set) Token: 0x060042C8 RID: 17096 RVA: 0x0002DED0 File Offset: 0x0002C0D0
		public Matrix4x4Proxy worldToCameraMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToCameraMatrix);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.worldToCameraMatrix = value._value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x0002DEF1 File Offset: 0x0002C0F1
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x0002DEFE File Offset: 0x0002C0FE
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

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x0002DF0C File Offset: 0x0002C10C
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x0002DF19 File Offset: 0x0002C119
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x0002DF2B File Offset: 0x0002C12B
		// (set) Token: 0x060042CE RID: 17102 RVA: 0x0002DF38 File Offset: 0x0002C138
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

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x0002DF46 File Offset: 0x0002C146
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x0002DF58 File Offset: 0x0002C158
		// (set) Token: 0x060042D1 RID: 17105 RVA: 0x0002DF65 File Offset: 0x0002C165
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

		// Token: 0x060042D2 RID: 17106 RVA: 0x0002DF73 File Offset: 0x0002C173
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x001300EC File Offset: 0x0012E2EC
		[MoonSharpHidden]
		public static CameraProxy New(Camera value)
		{
			if (value == null)
			{
				return null;
			}
			CameraProxy cameraProxy = (CameraProxy)ObjectCache.Get(typeof(CameraProxy), value);
			if (cameraProxy == null)
			{
				cameraProxy = new CameraProxy(value);
				ObjectCache.Add(typeof(CameraProxy), value, cameraProxy);
			}
			return cameraProxy;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0002DF7B File Offset: 0x0002C17B
		[MoonSharpUserDataMetamethod("__call")]
		public static CameraProxy Call(DynValue _)
		{
			return new CameraProxy();
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0002DF82 File Offset: 0x0002C182
		public Matrix4x4Proxy CalculateObliqueMatrix(Vector4Proxy clipPlane)
		{
			if (clipPlane == null)
			{
				throw new ScriptRuntimeException("argument 'clipPlane' is nil");
			}
			return Matrix4x4Proxy.New(this._value.CalculateObliqueMatrix(clipPlane._value));
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x00130138 File Offset: 0x0012E338
		public void CopyFrom(CameraProxy other)
		{
			Camera other2 = null;
			if (other != null)
			{
				other2 = other._value;
			}
			this._value.CopyFrom(other2);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0002DFA8 File Offset: 0x0002C1A8
		public static float FieldOfViewToFocalLength(float fieldOfView, float sensorSize)
		{
			return Camera.FieldOfViewToFocalLength(fieldOfView, sensorSize);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0002DFB1 File Offset: 0x0002C1B1
		public static float FocalLengthToFieldOfView(float focalLength, float sensorSize)
		{
			return Camera.FocalLengthToFieldOfView(focalLength, sensorSize);
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0002DFBA File Offset: 0x0002C1BA
		public static int GetAllCameras(Camera[] cameras)
		{
			return Camera.GetAllCameras(cameras);
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0002DFC2 File Offset: 0x0002C1C2
		public float GetGateFittedFieldOfView()
		{
			return this._value.GetGateFittedFieldOfView();
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0002DFCF File Offset: 0x0002C1CF
		public Vector2Proxy GetGateFittedLensShift()
		{
			return Vector2Proxy.New(this._value.GetGateFittedLensShift());
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0002DFE1 File Offset: 0x0002C1E1
		public static float HorizontalToVerticalFieldOfView(float horizontalFieldOfView, float aspectRatio)
		{
			return Camera.HorizontalToVerticalFieldOfView(horizontalFieldOfView, aspectRatio);
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0002DFEA File Offset: 0x0002C1EA
		public void RemoveAllCommandBuffers()
		{
			this._value.RemoveAllCommandBuffers();
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x0002DFF7 File Offset: 0x0002C1F7
		public void Render()
		{
			this._value.Render();
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x0002E004 File Offset: 0x0002C204
		public void RenderDontRestore()
		{
			this._value.RenderDontRestore();
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0002E011 File Offset: 0x0002C211
		public void Reset()
		{
			this._value.Reset();
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0002E01E File Offset: 0x0002C21E
		public void ResetAspect()
		{
			this._value.ResetAspect();
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0002E02B File Offset: 0x0002C22B
		public void ResetCullingMatrix()
		{
			this._value.ResetCullingMatrix();
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0002E038 File Offset: 0x0002C238
		public void ResetProjectionMatrix()
		{
			this._value.ResetProjectionMatrix();
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0002E045 File Offset: 0x0002C245
		public void ResetReplacementShader()
		{
			this._value.ResetReplacementShader();
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x0002E052 File Offset: 0x0002C252
		public void ResetStereoProjectionMatrices()
		{
			this._value.ResetStereoProjectionMatrices();
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x0002E05F File Offset: 0x0002C25F
		public void ResetStereoViewMatrices()
		{
			this._value.ResetStereoViewMatrices();
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x0002E06C File Offset: 0x0002C26C
		public void ResetTransparencySortSettings()
		{
			this._value.ResetTransparencySortSettings();
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0002E079 File Offset: 0x0002C279
		public void ResetWorldToCameraMatrix()
		{
			this._value.ResetWorldToCameraMatrix();
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0002E086 File Offset: 0x0002C286
		public RayProxy ScreenPointToRay(Vector3Proxy pos)
		{
			if (pos == null)
			{
				throw new ScriptRuntimeException("argument 'pos' is nil");
			}
			return RayProxy.New(this._value.ScreenPointToRay(pos._value));
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		public Vector3Proxy ScreenToViewportPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ScreenToViewportPoint(position._value));
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0002E0D2 File Offset: 0x0002C2D2
		public Vector3Proxy ScreenToWorldPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ScreenToWorldPoint(position._value));
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x00130160 File Offset: 0x0012E360
		public static void SetupCurrent(CameraProxy cur)
		{
			Camera cur2 = null;
			if (cur != null)
			{
				cur2 = cur._value;
			}
			Camera.SetupCurrent(cur2);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0002E0F8 File Offset: 0x0002C2F8
		public static float VerticalToHorizontalFieldOfView(float verticalFieldOfView, float aspectRatio)
		{
			return Camera.VerticalToHorizontalFieldOfView(verticalFieldOfView, aspectRatio);
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0002E101 File Offset: 0x0002C301
		public RayProxy ViewportPointToRay(Vector3Proxy pos)
		{
			if (pos == null)
			{
				throw new ScriptRuntimeException("argument 'pos' is nil");
			}
			return RayProxy.New(this._value.ViewportPointToRay(pos._value));
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0002E127 File Offset: 0x0002C327
		public Vector3Proxy ViewportToScreenPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ViewportToScreenPoint(position._value));
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0002E14D File Offset: 0x0002C34D
		public Vector3Proxy ViewportToWorldPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ViewportToWorldPoint(position._value));
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0002E173 File Offset: 0x0002C373
		public Vector3Proxy WorldToScreenPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.WorldToScreenPoint(position._value));
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0002E199 File Offset: 0x0002C399
		public Vector3Proxy WorldToViewportPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.WorldToViewportPoint(position._value));
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x0002E1BF File Offset: 0x0002C3BF
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0002E1CD File Offset: 0x0002C3CD
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x0002E1DA File Offset: 0x0002C3DA
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003151 RID: 12625
		[MoonSharpHidden]
		public Camera _value;
	}
}
