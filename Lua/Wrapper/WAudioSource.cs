using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200094D RID: 2381
	[Wrapper(typeof(AudioSource), includeTarget = true)]
	[Name("AudioSource")]
	public static class WAudioSource
	{
		// Token: 0x06003C50 RID: 15440 RVA: 0x00028E0A File Offset: 0x0002700A
		[Doc("Sets the output audio mix of this audio source.")]
		public static void SetOutputAudioMixer(AudioSource self, AudioMixer mixer)
		{
			GameManager.SetOutputAudioMixer(self, mixer);
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x00028E13 File Offset: 0x00027013
		[Doc("Gets a float[64] array of the next audio samples.")]
		public static float[] GetOutputData(AudioSource self, int channel)
		{
			self.GetOutputData(WAudioSource.OUTPUT_DATA, channel);
			return WAudioSource.OUTPUT_DATA;
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00028E26 File Offset: 0x00027026
		[Doc("Gets a float[64] array of current spectrum data using FFT.")]
		public static float[] GetSpectrumData(AudioSource self, int channel)
		{
			self.GetSpectrumData(WAudioSource.OUTPUT_DATA, channel, FFTWindow.Rectangular);
			return WAudioSource.OUTPUT_DATA;
		}

		// Token: 0x04003102 RID: 12546
		private static float[] OUTPUT_DATA = new float[64];
	}
}
