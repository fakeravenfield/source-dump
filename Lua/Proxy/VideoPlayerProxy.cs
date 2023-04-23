using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Video;

namespace Lua.Proxy
{
	// Token: 0x02000A15 RID: 2581
	[Proxy(typeof(VideoPlayer))]
	public class VideoPlayerProxy : IProxy
	{
		// Token: 0x060051B7 RID: 20919 RVA: 0x0003C488 File Offset: 0x0003A688
		[MoonSharpHidden]
		public VideoPlayerProxy(VideoPlayer value)
		{
			this._value = value;
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x0003C497 File Offset: 0x0003A697
		public VideoPlayerProxy()
		{
			this._value = new VideoPlayer();
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060051B9 RID: 20921 RVA: 0x0003C4AA File Offset: 0x0003A6AA
		public ushort audioTrackCount
		{
			get
			{
				return this._value.audioTrackCount;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060051BA RID: 20922 RVA: 0x0003C4B7 File Offset: 0x0003A6B7
		public bool canSetDirectAudioVolume
		{
			get
			{
				return this._value.canSetDirectAudioVolume;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
		public bool canSetPlaybackSpeed
		{
			get
			{
				return this._value.canSetPlaybackSpeed;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x0003C4D1 File Offset: 0x0003A6D1
		public bool canSetSkipOnDrop
		{
			get
			{
				return this._value.canSetSkipOnDrop;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060051BD RID: 20925 RVA: 0x0003C4DE File Offset: 0x0003A6DE
		public bool canSetTime
		{
			get
			{
				return this._value.canSetTime;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x0003C4EB File Offset: 0x0003A6EB
		public bool canSetTimeSource
		{
			get
			{
				return this._value.canSetTimeSource;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x0003C4F8 File Offset: 0x0003A6F8
		public bool canStep
		{
			get
			{
				return this._value.canStep;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060051C0 RID: 20928 RVA: 0x0003C505 File Offset: 0x0003A705
		// (set) Token: 0x060051C1 RID: 20929 RVA: 0x00138DF0 File Offset: 0x00136FF0
		public VideoClipProxy clip
		{
			get
			{
				return VideoClipProxy.New(this._value.clip);
			}
			set
			{
				VideoClip clip = null;
				if (value != null)
				{
					clip = value._value;
				}
				this._value.clip = clip;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x0003C517 File Offset: 0x0003A717
		public double clockTime
		{
			get
			{
				return this._value.clockTime;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x0003C524 File Offset: 0x0003A724
		// (set) Token: 0x060051C4 RID: 20932 RVA: 0x0003C531 File Offset: 0x0003A731
		public ushort controlledAudioTrackCount
		{
			get
			{
				return this._value.controlledAudioTrackCount;
			}
			set
			{
				this._value.controlledAudioTrackCount = value;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x0003C53F File Offset: 0x0003A73F
		public static ushort controlledAudioTrackMaxCount
		{
			get
			{
				return VideoPlayer.controlledAudioTrackMaxCount;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060051C6 RID: 20934 RVA: 0x0003C546 File Offset: 0x0003A746
		// (set) Token: 0x060051C7 RID: 20935 RVA: 0x0003C553 File Offset: 0x0003A753
		public double externalReferenceTime
		{
			get
			{
				return this._value.externalReferenceTime;
			}
			set
			{
				this._value.externalReferenceTime = value;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060051C8 RID: 20936 RVA: 0x0003C561 File Offset: 0x0003A761
		// (set) Token: 0x060051C9 RID: 20937 RVA: 0x0003C56E File Offset: 0x0003A76E
		public long frame
		{
			get
			{
				return this._value.frame;
			}
			set
			{
				this._value.frame = value;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060051CA RID: 20938 RVA: 0x0003C57C File Offset: 0x0003A77C
		public ulong frameCount
		{
			get
			{
				return this._value.frameCount;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x0003C589 File Offset: 0x0003A789
		public float frameRate
		{
			get
			{
				return this._value.frameRate;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x0003C596 File Offset: 0x0003A796
		public uint height
		{
			get
			{
				return this._value.height;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x0003C5A3 File Offset: 0x0003A7A3
		// (set) Token: 0x060051CE RID: 20942 RVA: 0x0003C5B0 File Offset: 0x0003A7B0
		public bool isLooping
		{
			get
			{
				return this._value.isLooping;
			}
			set
			{
				this._value.isLooping = value;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x0003C5BE File Offset: 0x0003A7BE
		public bool isPaused
		{
			get
			{
				return this._value.isPaused;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060051D0 RID: 20944 RVA: 0x0003C5CB File Offset: 0x0003A7CB
		public bool isPlaying
		{
			get
			{
				return this._value.isPlaying;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
		public bool isPrepared
		{
			get
			{
				return this._value.isPrepared;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060051D2 RID: 20946 RVA: 0x0003C5E5 File Offset: 0x0003A7E5
		public double length
		{
			get
			{
				return this._value.length;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x0003C5F2 File Offset: 0x0003A7F2
		public uint pixelAspectRatioDenominator
		{
			get
			{
				return this._value.pixelAspectRatioDenominator;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060051D4 RID: 20948 RVA: 0x0003C5FF File Offset: 0x0003A7FF
		public uint pixelAspectRatioNumerator
		{
			get
			{
				return this._value.pixelAspectRatioNumerator;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x0003C60C File Offset: 0x0003A80C
		// (set) Token: 0x060051D6 RID: 20950 RVA: 0x0003C619 File Offset: 0x0003A819
		public float playbackSpeed
		{
			get
			{
				return this._value.playbackSpeed;
			}
			set
			{
				this._value.playbackSpeed = value;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x0003C627 File Offset: 0x0003A827
		// (set) Token: 0x060051D8 RID: 20952 RVA: 0x0003C634 File Offset: 0x0003A834
		public bool playOnAwake
		{
			get
			{
				return this._value.playOnAwake;
			}
			set
			{
				this._value.playOnAwake = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x0003C642 File Offset: 0x0003A842
		// (set) Token: 0x060051DA RID: 20954 RVA: 0x0003C64F File Offset: 0x0003A84F
		public VideoRenderMode renderMode
		{
			get
			{
				return this._value.renderMode;
			}
			set
			{
				this._value.renderMode = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x0003C65D File Offset: 0x0003A85D
		// (set) Token: 0x060051DC RID: 20956 RVA: 0x0003C66A File Offset: 0x0003A86A
		public bool sendFrameReadyEvents
		{
			get
			{
				return this._value.sendFrameReadyEvents;
			}
			set
			{
				this._value.sendFrameReadyEvents = value;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060051DD RID: 20957 RVA: 0x0003C678 File Offset: 0x0003A878
		// (set) Token: 0x060051DE RID: 20958 RVA: 0x0003C685 File Offset: 0x0003A885
		public bool skipOnDrop
		{
			get
			{
				return this._value.skipOnDrop;
			}
			set
			{
				this._value.skipOnDrop = value;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x0003C693 File Offset: 0x0003A893
		// (set) Token: 0x060051E0 RID: 20960 RVA: 0x00138E18 File Offset: 0x00137018
		public CameraProxy targetCamera
		{
			get
			{
				return CameraProxy.New(this._value.targetCamera);
			}
			set
			{
				Camera targetCamera = null;
				if (value != null)
				{
					targetCamera = value._value;
				}
				this._value.targetCamera = targetCamera;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x0003C6A5 File Offset: 0x0003A8A5
		// (set) Token: 0x060051E2 RID: 20962 RVA: 0x0003C6B2 File Offset: 0x0003A8B2
		public float targetCameraAlpha
		{
			get
			{
				return this._value.targetCameraAlpha;
			}
			set
			{
				this._value.targetCameraAlpha = value;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060051E3 RID: 20963 RVA: 0x0003C6C0 File Offset: 0x0003A8C0
		// (set) Token: 0x060051E4 RID: 20964 RVA: 0x0003C6CD File Offset: 0x0003A8CD
		public string targetMaterialProperty
		{
			get
			{
				return this._value.targetMaterialProperty;
			}
			set
			{
				this._value.targetMaterialProperty = value;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x0003C6DB File Offset: 0x0003A8DB
		// (set) Token: 0x060051E6 RID: 20966 RVA: 0x00138E40 File Offset: 0x00137040
		public RendererProxy targetMaterialRenderer
		{
			get
			{
				return RendererProxy.New(this._value.targetMaterialRenderer);
			}
			set
			{
				Renderer targetMaterialRenderer = null;
				if (value != null)
				{
					targetMaterialRenderer = value._value;
				}
				this._value.targetMaterialRenderer = targetMaterialRenderer;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060051E7 RID: 20967 RVA: 0x0003C6ED File Offset: 0x0003A8ED
		public TextureProxy targetTexture
		{
			get
			{
				return TextureProxy.New(this._value.targetTexture);
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x0003C6FF File Offset: 0x0003A8FF
		public TextureProxy texture
		{
			get
			{
				return TextureProxy.New(this._value.texture);
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060051E9 RID: 20969 RVA: 0x0003C711 File Offset: 0x0003A911
		// (set) Token: 0x060051EA RID: 20970 RVA: 0x0003C71E File Offset: 0x0003A91E
		public double time
		{
			get
			{
				return this._value.time;
			}
			set
			{
				this._value.time = value;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060051EB RID: 20971 RVA: 0x0003C72C File Offset: 0x0003A92C
		// (set) Token: 0x060051EC RID: 20972 RVA: 0x0003C739 File Offset: 0x0003A939
		public bool waitForFirstFrame
		{
			get
			{
				return this._value.waitForFirstFrame;
			}
			set
			{
				this._value.waitForFirstFrame = value;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060051ED RID: 20973 RVA: 0x0003C747 File Offset: 0x0003A947
		public uint width
		{
			get
			{
				return this._value.width;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060051EE RID: 20974 RVA: 0x0003C754 File Offset: 0x0003A954
		// (set) Token: 0x060051EF RID: 20975 RVA: 0x0003C761 File Offset: 0x0003A961
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

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060051F0 RID: 20976 RVA: 0x0003C76F File Offset: 0x0003A96F
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0003C77C File Offset: 0x0003A97C
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060051F2 RID: 20978 RVA: 0x0003C78E File Offset: 0x0003A98E
		// (set) Token: 0x060051F3 RID: 20979 RVA: 0x0003C79B File Offset: 0x0003A99B
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

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060051F4 RID: 20980 RVA: 0x0003C7A9 File Offset: 0x0003A9A9
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x0003C7BB File Offset: 0x0003A9BB
		// (set) Token: 0x060051F6 RID: 20982 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
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

		// Token: 0x060051F7 RID: 20983 RVA: 0x0003C7D6 File Offset: 0x0003A9D6
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00138E68 File Offset: 0x00137068
		[MoonSharpHidden]
		public static VideoPlayerProxy New(VideoPlayer value)
		{
			if (value == null)
			{
				return null;
			}
			VideoPlayerProxy videoPlayerProxy = (VideoPlayerProxy)ObjectCache.Get(typeof(VideoPlayerProxy), value);
			if (videoPlayerProxy == null)
			{
				videoPlayerProxy = new VideoPlayerProxy(value);
				ObjectCache.Add(typeof(VideoPlayerProxy), value, videoPlayerProxy);
			}
			return videoPlayerProxy;
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x0003C7DE File Offset: 0x0003A9DE
		[MoonSharpUserDataMetamethod("__call")]
		public static VideoPlayerProxy Call(DynValue _)
		{
			return new VideoPlayerProxy();
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x0003C7E5 File Offset: 0x0003A9E5
		public void EnableAudioTrack(ushort trackIndex, bool enabled)
		{
			this._value.EnableAudioTrack(trackIndex, enabled);
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x0003C7F4 File Offset: 0x0003A9F4
		public ushort GetAudioChannelCount(ushort trackIndex)
		{
			return this._value.GetAudioChannelCount(trackIndex);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x0003C802 File Offset: 0x0003AA02
		public string GetAudioLanguageCode(ushort trackIndex)
		{
			return this._value.GetAudioLanguageCode(trackIndex);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0003C810 File Offset: 0x0003AA10
		public uint GetAudioSampleRate(ushort trackIndex)
		{
			return this._value.GetAudioSampleRate(trackIndex);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0003C81E File Offset: 0x0003AA1E
		public bool GetDirectAudioMute(ushort trackIndex)
		{
			return this._value.GetDirectAudioMute(trackIndex);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0003C82C File Offset: 0x0003AA2C
		public float GetDirectAudioVolume(ushort trackIndex)
		{
			return this._value.GetDirectAudioVolume(trackIndex);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x0003C83A File Offset: 0x0003AA3A
		public AudioSourceProxy GetTargetAudioSource(ushort trackIndex)
		{
			return AudioSourceProxy.New(this._value.GetTargetAudioSource(trackIndex));
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x0003C84D File Offset: 0x0003AA4D
		public bool IsAudioTrackEnabled(ushort trackIndex)
		{
			return this._value.IsAudioTrackEnabled(trackIndex);
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0003C85B File Offset: 0x0003AA5B
		public void Pause()
		{
			this._value.Pause();
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0003C868 File Offset: 0x0003AA68
		public void Play()
		{
			this._value.Play();
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0003C875 File Offset: 0x0003AA75
		public void Prepare()
		{
			this._value.Prepare();
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0003C882 File Offset: 0x0003AA82
		public void SetDirectAudioMute(ushort trackIndex, bool mute)
		{
			this._value.SetDirectAudioMute(trackIndex, mute);
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x0003C891 File Offset: 0x0003AA91
		public void SetDirectAudioVolume(ushort trackIndex, float volume)
		{
			this._value.SetDirectAudioVolume(trackIndex, volume);
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00138EB4 File Offset: 0x001370B4
		public void SetTargetAudioSource(ushort trackIndex, AudioSourceProxy source)
		{
			AudioSource source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			this._value.SetTargetAudioSource(trackIndex, source2);
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		public void StepForward()
		{
			this._value.StepForward();
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0003C8AD File Offset: 0x0003AAAD
		public void Stop()
		{
			this._value.Stop();
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0003C8BA File Offset: 0x0003AABA
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x0003C8C8 File Offset: 0x0003AAC8
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x0003C8D5 File Offset: 0x0003AAD5
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0003C8E2 File Offset: 0x0003AAE2
		public void SetModContentFileURL(string localPath)
		{
			WVideoPlayer.SetModContentFileURL(this._value, localPath);
		}

		// Token: 0x040032A4 RID: 12964
		[MoonSharpHidden]
		public VideoPlayer _value;
	}
}
