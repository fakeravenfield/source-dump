using System;
using MoonSharp.Interpreter;
using UnityEngine.Video;

namespace Lua.Proxy
{
	// Token: 0x02000A14 RID: 2580
	[Proxy(typeof(VideoClip))]
	public class VideoClipProxy : IProxy
	{
		// Token: 0x060051A3 RID: 20899 RVA: 0x0003C390 File Offset: 0x0003A590
		[MoonSharpHidden]
		public VideoClipProxy(VideoClip value)
		{
			this._value = value;
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060051A4 RID: 20900 RVA: 0x0003C39F File Offset: 0x0003A59F
		public ushort audioTrackCount
		{
			get
			{
				return this._value.audioTrackCount;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060051A5 RID: 20901 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		public ulong frameCount
		{
			get
			{
				return this._value.frameCount;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x0003C3B9 File Offset: 0x0003A5B9
		public double frameRate
		{
			get
			{
				return this._value.frameRate;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0003C3C6 File Offset: 0x0003A5C6
		public uint height
		{
			get
			{
				return this._value.height;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x0003C3D3 File Offset: 0x0003A5D3
		public double length
		{
			get
			{
				return this._value.length;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		public string originalPath
		{
			get
			{
				return this._value.originalPath;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x0003C3ED File Offset: 0x0003A5ED
		public uint pixelAspectRatioDenominator
		{
			get
			{
				return this._value.pixelAspectRatioDenominator;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x0003C3FA File Offset: 0x0003A5FA
		public uint pixelAspectRatioNumerator
		{
			get
			{
				return this._value.pixelAspectRatioNumerator;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x0003C407 File Offset: 0x0003A607
		public bool sRGB
		{
			get
			{
				return this._value.sRGB;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x060051AD RID: 20909 RVA: 0x0003C414 File Offset: 0x0003A614
		public uint width
		{
			get
			{
				return this._value.width;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060051AE RID: 20910 RVA: 0x0003C421 File Offset: 0x0003A621
		// (set) Token: 0x060051AF RID: 20911 RVA: 0x0003C42E File Offset: 0x0003A62E
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

		// Token: 0x060051B0 RID: 20912 RVA: 0x0003C43C File Offset: 0x0003A63C
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x00138DA4 File Offset: 0x00136FA4
		[MoonSharpHidden]
		public static VideoClipProxy New(VideoClip value)
		{
			if (value == null)
			{
				return null;
			}
			VideoClipProxy videoClipProxy = (VideoClipProxy)ObjectCache.Get(typeof(VideoClipProxy), value);
			if (videoClipProxy == null)
			{
				videoClipProxy = new VideoClipProxy(value);
				ObjectCache.Add(typeof(VideoClipProxy), value, videoClipProxy);
			}
			return videoClipProxy;
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x0003C444 File Offset: 0x0003A644
		public ushort GetAudioChannelCount(ushort audioTrackIdx)
		{
			return this._value.GetAudioChannelCount(audioTrackIdx);
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0003C452 File Offset: 0x0003A652
		public string GetAudioLanguage(ushort audioTrackIdx)
		{
			return this._value.GetAudioLanguage(audioTrackIdx);
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0003C460 File Offset: 0x0003A660
		public uint GetAudioSampleRate(ushort audioTrackIdx)
		{
			return this._value.GetAudioSampleRate(audioTrackIdx);
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0003C46E File Offset: 0x0003A66E
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0003C47B File Offset: 0x0003A67B
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A3 RID: 12963
		[MoonSharpHidden]
		public VideoClip _value;
	}
}
