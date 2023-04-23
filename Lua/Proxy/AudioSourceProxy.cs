using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B3 RID: 2483
	[Proxy(typeof(AudioSource))]
	public class AudioSourceProxy : IProxy
	{
		// Token: 0x0600418D RID: 16781 RVA: 0x0002CB44 File Offset: 0x0002AD44
		[MoonSharpHidden]
		public AudioSourceProxy(AudioSource value)
		{
			this._value = value;
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0002CB53 File Offset: 0x0002AD53
		public AudioSourceProxy()
		{
			this._value = new AudioSource();
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x0002CB66 File Offset: 0x0002AD66
		// (set) Token: 0x06004190 RID: 16784 RVA: 0x0002CB73 File Offset: 0x0002AD73
		public bool bypassEffects
		{
			get
			{
				return this._value.bypassEffects;
			}
			set
			{
				this._value.bypassEffects = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x0002CB81 File Offset: 0x0002AD81
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x0002CB8E File Offset: 0x0002AD8E
		public bool bypassListenerEffects
		{
			get
			{
				return this._value.bypassListenerEffects;
			}
			set
			{
				this._value.bypassListenerEffects = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x0002CB9C File Offset: 0x0002AD9C
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x0002CBA9 File Offset: 0x0002ADA9
		public bool bypassReverbZones
		{
			get
			{
				return this._value.bypassReverbZones;
			}
			set
			{
				this._value.bypassReverbZones = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x0002CBB7 File Offset: 0x0002ADB7
		// (set) Token: 0x06004196 RID: 16790 RVA: 0x0012FE8C File Offset: 0x0012E08C
		public AudioClipProxy clip
		{
			get
			{
				return AudioClipProxy.New(this._value.clip);
			}
			set
			{
				AudioClip clip = null;
				if (value != null)
				{
					clip = value._value;
				}
				this._value.clip = clip;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x0002CBC9 File Offset: 0x0002ADC9
		// (set) Token: 0x06004198 RID: 16792 RVA: 0x0002CBD6 File Offset: 0x0002ADD6
		public float dopplerLevel
		{
			get
			{
				return this._value.dopplerLevel;
			}
			set
			{
				this._value.dopplerLevel = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
		// (set) Token: 0x0600419A RID: 16794 RVA: 0x0002CBF1 File Offset: 0x0002ADF1
		public bool ignoreListenerPause
		{
			get
			{
				return this._value.ignoreListenerPause;
			}
			set
			{
				this._value.ignoreListenerPause = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x0002CBFF File Offset: 0x0002ADFF
		// (set) Token: 0x0600419C RID: 16796 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		public bool ignoreListenerVolume
		{
			get
			{
				return this._value.ignoreListenerVolume;
			}
			set
			{
				this._value.ignoreListenerVolume = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x0002CC1A File Offset: 0x0002AE1A
		public bool isPlaying
		{
			get
			{
				return this._value.isPlaying;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x0002CC27 File Offset: 0x0002AE27
		public bool isVirtual
		{
			get
			{
				return this._value.isVirtual;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x0002CC34 File Offset: 0x0002AE34
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x0002CC41 File Offset: 0x0002AE41
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x0002CC4F File Offset: 0x0002AE4F
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x0002CC5C File Offset: 0x0002AE5C
		public float maxDistance
		{
			get
			{
				return this._value.maxDistance;
			}
			set
			{
				this._value.maxDistance = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x0002CC6A File Offset: 0x0002AE6A
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x0002CC77 File Offset: 0x0002AE77
		public float minDistance
		{
			get
			{
				return this._value.minDistance;
			}
			set
			{
				this._value.minDistance = value;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x0002CC85 File Offset: 0x0002AE85
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x0002CC92 File Offset: 0x0002AE92
		public bool mute
		{
			get
			{
				return this._value.mute;
			}
			set
			{
				this._value.mute = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
		// (set) Token: 0x060041A8 RID: 16808 RVA: 0x0002CCAD File Offset: 0x0002AEAD
		public float panStereo
		{
			get
			{
				return this._value.panStereo;
			}
			set
			{
				this._value.panStereo = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x0002CCBB File Offset: 0x0002AEBB
		// (set) Token: 0x060041AA RID: 16810 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
		public float pitch
		{
			get
			{
				return this._value.pitch;
			}
			set
			{
				this._value.pitch = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x0002CCD6 File Offset: 0x0002AED6
		// (set) Token: 0x060041AC RID: 16812 RVA: 0x0002CCE3 File Offset: 0x0002AEE3
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

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x0002CCF1 File Offset: 0x0002AEF1
		// (set) Token: 0x060041AE RID: 16814 RVA: 0x0002CCFE File Offset: 0x0002AEFE
		public int priority
		{
			get
			{
				return this._value.priority;
			}
			set
			{
				this._value.priority = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		// (set) Token: 0x060041B0 RID: 16816 RVA: 0x0002CD19 File Offset: 0x0002AF19
		public float reverbZoneMix
		{
			get
			{
				return this._value.reverbZoneMix;
			}
			set
			{
				this._value.reverbZoneMix = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x0002CD27 File Offset: 0x0002AF27
		// (set) Token: 0x060041B2 RID: 16818 RVA: 0x0002CD34 File Offset: 0x0002AF34
		public float spatialBlend
		{
			get
			{
				return this._value.spatialBlend;
			}
			set
			{
				this._value.spatialBlend = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x0002CD42 File Offset: 0x0002AF42
		// (set) Token: 0x060041B4 RID: 16820 RVA: 0x0002CD4F File Offset: 0x0002AF4F
		public bool spatialize
		{
			get
			{
				return this._value.spatialize;
			}
			set
			{
				this._value.spatialize = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x0002CD5D File Offset: 0x0002AF5D
		// (set) Token: 0x060041B6 RID: 16822 RVA: 0x0002CD6A File Offset: 0x0002AF6A
		public bool spatializePostEffects
		{
			get
			{
				return this._value.spatializePostEffects;
			}
			set
			{
				this._value.spatializePostEffects = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x0002CD78 File Offset: 0x0002AF78
		// (set) Token: 0x060041B8 RID: 16824 RVA: 0x0002CD85 File Offset: 0x0002AF85
		public float spread
		{
			get
			{
				return this._value.spread;
			}
			set
			{
				this._value.spread = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x0002CD93 File Offset: 0x0002AF93
		// (set) Token: 0x060041BA RID: 16826 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		public float time
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

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x0002CDAE File Offset: 0x0002AFAE
		// (set) Token: 0x060041BC RID: 16828 RVA: 0x0002CDBB File Offset: 0x0002AFBB
		public int timeSamples
		{
			get
			{
				return this._value.timeSamples;
			}
			set
			{
				this._value.timeSamples = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x0002CDC9 File Offset: 0x0002AFC9
		// (set) Token: 0x060041BE RID: 16830 RVA: 0x0002CDD6 File Offset: 0x0002AFD6
		public float volume
		{
			get
			{
				return this._value.volume;
			}
			set
			{
				this._value.volume = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x0002CDE4 File Offset: 0x0002AFE4
		// (set) Token: 0x060041C0 RID: 16832 RVA: 0x0002CDF1 File Offset: 0x0002AFF1
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

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x0002CDFF File Offset: 0x0002AFFF
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x0002CE0C File Offset: 0x0002B00C
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x0002CE1E File Offset: 0x0002B01E
		// (set) Token: 0x060041C4 RID: 16836 RVA: 0x0002CE2B File Offset: 0x0002B02B
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

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x0002CE39 File Offset: 0x0002B039
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x0002CE4B File Offset: 0x0002B04B
		// (set) Token: 0x060041C7 RID: 16839 RVA: 0x0002CE58 File Offset: 0x0002B058
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

		// Token: 0x060041C8 RID: 16840 RVA: 0x0002CE66 File Offset: 0x0002B066
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x0012FEB4 File Offset: 0x0012E0B4
		[MoonSharpHidden]
		public static AudioSourceProxy New(AudioSource value)
		{
			if (value == null)
			{
				return null;
			}
			AudioSourceProxy audioSourceProxy = (AudioSourceProxy)ObjectCache.Get(typeof(AudioSourceProxy), value);
			if (audioSourceProxy == null)
			{
				audioSourceProxy = new AudioSourceProxy(value);
				ObjectCache.Add(typeof(AudioSourceProxy), value, audioSourceProxy);
			}
			return audioSourceProxy;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x0002CE6E File Offset: 0x0002B06E
		[MoonSharpUserDataMetamethod("__call")]
		public static AudioSourceProxy Call(DynValue _)
		{
			return new AudioSourceProxy();
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x0002CE75 File Offset: 0x0002B075
		public bool GetAmbisonicDecoderFloat(int index, out float value)
		{
			value = 0f;
			return this._value.GetAmbisonicDecoderFloat(index, out value);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x0002CE8B File Offset: 0x0002B08B
		public void GetOutputData(float[] samples, int channel)
		{
			this._value.GetOutputData(samples, channel);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x0002CE9A File Offset: 0x0002B09A
		public bool GetSpatializerFloat(int index, out float value)
		{
			value = 0f;
			return this._value.GetSpatializerFloat(index, out value);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
		public void Pause()
		{
			this._value.Pause();
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x0002CEBD File Offset: 0x0002B0BD
		public void Play()
		{
			this._value.Play();
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x0002CECA File Offset: 0x0002B0CA
		public void Play(ulong delay)
		{
			this._value.Play(delay);
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x0012FF00 File Offset: 0x0012E100
		public static void PlayClipAtPoint(AudioClipProxy clip, Vector3Proxy position)
		{
			AudioClip clip2 = null;
			if (clip != null)
			{
				clip2 = clip._value;
			}
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			AudioSource.PlayClipAtPoint(clip2, position._value);
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x0012FF34 File Offset: 0x0012E134
		public static void PlayClipAtPoint(AudioClipProxy clip, Vector3Proxy position, float volume)
		{
			AudioClip clip2 = null;
			if (clip != null)
			{
				clip2 = clip._value;
			}
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			AudioSource.PlayClipAtPoint(clip2, position._value, volume);
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		public void PlayDelayed(float delay)
		{
			this._value.PlayDelayed(delay);
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x0012FF68 File Offset: 0x0012E168
		public void PlayOneShot(AudioClipProxy clip)
		{
			AudioClip clip2 = null;
			if (clip != null)
			{
				clip2 = clip._value;
			}
			this._value.PlayOneShot(clip2);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x0012FF90 File Offset: 0x0012E190
		public void PlayOneShot(AudioClipProxy clip, float volumeScale)
		{
			AudioClip clip2 = null;
			if (clip != null)
			{
				clip2 = clip._value;
			}
			this._value.PlayOneShot(clip2, volumeScale);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x0002CEE6 File Offset: 0x0002B0E6
		public void PlayScheduled(double time)
		{
			this._value.PlayScheduled(time);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		public bool SetAmbisonicDecoderFloat(int index, float value)
		{
			return this._value.SetAmbisonicDecoderFloat(index, value);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x0002CF03 File Offset: 0x0002B103
		public void SetScheduledEndTime(double time)
		{
			this._value.SetScheduledEndTime(time);
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x0002CF11 File Offset: 0x0002B111
		public void SetScheduledStartTime(double time)
		{
			this._value.SetScheduledStartTime(time);
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x0002CF1F File Offset: 0x0002B11F
		public bool SetSpatializerFloat(int index, float value)
		{
			return this._value.SetSpatializerFloat(index, value);
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x0002CF2E File Offset: 0x0002B12E
		public void Stop()
		{
			this._value.Stop();
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x0002CF3B File Offset: 0x0002B13B
		public void UnPause()
		{
			this._value.UnPause();
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x0002CF48 File Offset: 0x0002B148
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x0002CF56 File Offset: 0x0002B156
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x0002CF63 File Offset: 0x0002B163
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x0002CF70 File Offset: 0x0002B170
		public float[] GetOutputData(int channel)
		{
			return WAudioSource.GetOutputData(this._value, channel);
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x0002CF7E File Offset: 0x0002B17E
		public float[] GetSpectrumData(int channel)
		{
			return WAudioSource.GetSpectrumData(this._value, channel);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x0002CF8C File Offset: 0x0002B18C
		public void SetOutputAudioMixer(AudioMixer mixer)
		{
			WAudioSource.SetOutputAudioMixer(this._value, mixer);
		}

		// Token: 0x0400314C RID: 12620
		[MoonSharpHidden]
		public AudioSource _value;
	}
}
