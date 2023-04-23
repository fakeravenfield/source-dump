using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A02 RID: 2562
	[Proxy(typeof(SoundBank))]
	public class SoundBankProxy : IProxy
	{
		// Token: 0x06004F2B RID: 20267 RVA: 0x000396DF File Offset: 0x000378DF
		[MoonSharpHidden]
		public SoundBankProxy(SoundBank value)
		{
			this._value = value;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x000396EE File Offset: 0x000378EE
		public SoundBankProxy()
		{
			this._value = new SoundBank();
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x00039701 File Offset: 0x00037901
		// (set) Token: 0x06004F2E RID: 20270 RVA: 0x001382C8 File Offset: 0x001364C8
		public AudioSourceProxy audioSource
		{
			get
			{
				return AudioSourceProxy.New(this._value.audioSource);
			}
			set
			{
				AudioSource audioSource = null;
				if (value != null)
				{
					audioSource = value._value;
				}
				this._value.audioSource = audioSource;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x00039713 File Offset: 0x00037913
		// (set) Token: 0x06004F30 RID: 20272 RVA: 0x00039720 File Offset: 0x00037920
		public AudioClip[] clips
		{
			get
			{
				return this._value.clips;
			}
			set
			{
				this._value.clips = value;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x0003972E File Offset: 0x0003792E
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x00039740 File Offset: 0x00037940
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x00039752 File Offset: 0x00037952
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x001382F0 File Offset: 0x001364F0
		[MoonSharpHidden]
		public static SoundBankProxy New(SoundBank value)
		{
			if (value == null)
			{
				return null;
			}
			SoundBankProxy soundBankProxy = (SoundBankProxy)ObjectCache.Get(typeof(SoundBankProxy), value);
			if (soundBankProxy == null)
			{
				soundBankProxy = new SoundBankProxy(value);
				ObjectCache.Add(typeof(SoundBankProxy), value, soundBankProxy);
			}
			return soundBankProxy;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x0003975A File Offset: 0x0003795A
		[MoonSharpUserDataMetamethod("__call")]
		public static SoundBankProxy Call(DynValue _)
		{
			return new SoundBankProxy();
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x00039761 File Offset: 0x00037961
		public bool IsPlaying()
		{
			return this._value.IsPlaying();
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0003976E File Offset: 0x0003796E
		public void PlayRandom()
		{
			this._value.PlayRandom();
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x0003977B File Offset: 0x0003797B
		public void PlaySoundBank(int index)
		{
			this._value.PlaySoundBank(index);
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00039789 File Offset: 0x00037989
		public void SetVolume(float volume)
		{
			this._value.SetVolume(volume);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00039797 File Offset: 0x00037997
		public void Start()
		{
			this._value.Start();
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x000397A4 File Offset: 0x000379A4
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003291 RID: 12945
		[MoonSharpHidden]
		public SoundBank _value;
	}
}
