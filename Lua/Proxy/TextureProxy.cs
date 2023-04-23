using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A0B RID: 2571
	[Proxy(typeof(Texture))]
	public class TextureProxy : IProxy
	{
		// Token: 0x06005012 RID: 20498 RVA: 0x0003A3C7 File Offset: 0x000385C7
		[MoonSharpHidden]
		public TextureProxy(Texture value)
		{
			this._value = value;
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x0003A3D6 File Offset: 0x000385D6
		public static int GenerateAllMips
		{
			get
			{
				return Texture.GenerateAllMips;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06005014 RID: 20500 RVA: 0x0003A3DD File Offset: 0x000385DD
		// (set) Token: 0x06005015 RID: 20501 RVA: 0x0003A3E4 File Offset: 0x000385E4
		public static bool allowThreadedTextureCreation
		{
			get
			{
				return Texture.allowThreadedTextureCreation;
			}
			set
			{
				Texture.allowThreadedTextureCreation = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06005016 RID: 20502 RVA: 0x0003A3EC File Offset: 0x000385EC
		// (set) Token: 0x06005017 RID: 20503 RVA: 0x0003A3F9 File Offset: 0x000385F9
		public int anisoLevel
		{
			get
			{
				return this._value.anisoLevel;
			}
			set
			{
				this._value.anisoLevel = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x0003A407 File Offset: 0x00038607
		public static ulong currentTextureMemory
		{
			get
			{
				return Texture.currentTextureMemory;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x0003A40E File Offset: 0x0003860E
		public static ulong desiredTextureMemory
		{
			get
			{
				return Texture.desiredTextureMemory;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x0003A415 File Offset: 0x00038615
		// (set) Token: 0x0600501B RID: 20507 RVA: 0x0003A422 File Offset: 0x00038622
		public int height
		{
			get
			{
				return this._value.height;
			}
			set
			{
				this._value.height = value;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x0003A430 File Offset: 0x00038630
		public bool isReadable
		{
			get
			{
				return this._value.isReadable;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600501D RID: 20509 RVA: 0x0003A43D File Offset: 0x0003863D
		// (set) Token: 0x0600501E RID: 20510 RVA: 0x0003A444 File Offset: 0x00038644
		public static int masterTextureLimit
		{
			get
			{
				return Texture.masterTextureLimit;
			}
			set
			{
				Texture.masterTextureLimit = value;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600501F RID: 20511 RVA: 0x0003A44C File Offset: 0x0003864C
		// (set) Token: 0x06005020 RID: 20512 RVA: 0x0003A459 File Offset: 0x00038659
		public float mipMapBias
		{
			get
			{
				return this._value.mipMapBias;
			}
			set
			{
				this._value.mipMapBias = value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06005021 RID: 20513 RVA: 0x0003A467 File Offset: 0x00038667
		public int mipmapCount
		{
			get
			{
				return this._value.mipmapCount;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06005022 RID: 20514 RVA: 0x0003A474 File Offset: 0x00038674
		public static ulong nonStreamingTextureCount
		{
			get
			{
				return Texture.nonStreamingTextureCount;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06005023 RID: 20515 RVA: 0x0003A47B File Offset: 0x0003867B
		public static ulong nonStreamingTextureMemory
		{
			get
			{
				return Texture.nonStreamingTextureMemory;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x0003A482 File Offset: 0x00038682
		public static ulong streamingMipmapUploadCount
		{
			get
			{
				return Texture.streamingMipmapUploadCount;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x0003A489 File Offset: 0x00038689
		public static ulong streamingRendererCount
		{
			get
			{
				return Texture.streamingRendererCount;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x0003A490 File Offset: 0x00038690
		public static ulong streamingTextureCount
		{
			get
			{
				return Texture.streamingTextureCount;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x0003A497 File Offset: 0x00038697
		// (set) Token: 0x06005028 RID: 20520 RVA: 0x0003A49E File Offset: 0x0003869E
		public static bool streamingTextureDiscardUnusedMips
		{
			get
			{
				return Texture.streamingTextureDiscardUnusedMips;
			}
			set
			{
				Texture.streamingTextureDiscardUnusedMips = value;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x0003A4A6 File Offset: 0x000386A6
		// (set) Token: 0x0600502A RID: 20522 RVA: 0x0003A4AD File Offset: 0x000386AD
		public static bool streamingTextureForceLoadAll
		{
			get
			{
				return Texture.streamingTextureForceLoadAll;
			}
			set
			{
				Texture.streamingTextureForceLoadAll = value;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x0003A4B5 File Offset: 0x000386B5
		public static ulong streamingTextureLoadingCount
		{
			get
			{
				return Texture.streamingTextureLoadingCount;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x0003A4BC File Offset: 0x000386BC
		public static ulong streamingTexturePendingLoadCount
		{
			get
			{
				return Texture.streamingTexturePendingLoadCount;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x0003A4C3 File Offset: 0x000386C3
		public static ulong targetTextureMemory
		{
			get
			{
				return Texture.targetTextureMemory;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x0003A4CA File Offset: 0x000386CA
		public Vector2Proxy texelSize
		{
			get
			{
				return Vector2Proxy.New(this._value.texelSize);
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x0003A4DC File Offset: 0x000386DC
		public static ulong totalTextureMemory
		{
			get
			{
				return Texture.totalTextureMemory;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x0003A4E3 File Offset: 0x000386E3
		public uint updateCount
		{
			get
			{
				return this._value.updateCount;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x0003A4F0 File Offset: 0x000386F0
		// (set) Token: 0x06005032 RID: 20530 RVA: 0x0003A4FD File Offset: 0x000386FD
		public int width
		{
			get
			{
				return this._value.width;
			}
			set
			{
				this._value.width = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x0003A50B File Offset: 0x0003870B
		// (set) Token: 0x06005034 RID: 20532 RVA: 0x0003A518 File Offset: 0x00038718
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

		// Token: 0x06005035 RID: 20533 RVA: 0x0003A526 File Offset: 0x00038726
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00138714 File Offset: 0x00136914
		[MoonSharpHidden]
		public static TextureProxy New(Texture value)
		{
			if (value == null)
			{
				return null;
			}
			TextureProxy textureProxy = (TextureProxy)ObjectCache.Get(typeof(TextureProxy), value);
			if (textureProxy == null)
			{
				textureProxy = new TextureProxy(value);
				ObjectCache.Add(typeof(TextureProxy), value, textureProxy);
			}
			return textureProxy;
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0003A52E File Offset: 0x0003872E
		public void IncrementUpdateCount()
		{
			this._value.IncrementUpdateCount();
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0003A53B File Offset: 0x0003873B
		public static void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax)
		{
			Texture.SetGlobalAnisotropicFilteringLimits(forcedMin, globalMax);
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0003A544 File Offset: 0x00038744
		public static void SetStreamingTextureMaterialDebugProperties()
		{
			Texture.SetStreamingTextureMaterialDebugProperties();
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0003A54B File Offset: 0x0003874B
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0003A558 File Offset: 0x00038758
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400329A RID: 12954
		[MoonSharpHidden]
		public Texture _value;
	}
}
