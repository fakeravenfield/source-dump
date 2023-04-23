using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000225 RID: 549
[Serializable]
public class PatchedAnimationClip
{
	// Token: 0x06000EC1 RID: 3777 RVA: 0x0000256A File Offset: 0x0000076A
	public PatchedAnimationClip()
	{
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0000BC88 File Offset: 0x00009E88
	public PatchedAnimationClip(string controllerName, string clipName)
	{
		this.controllerName = controllerName;
		this.clipName = clipName;
		this.curves = new List<PatchedAnimationClip.AnimationCurve>(128);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0000BCAE File Offset: 0x00009EAE
	public void ReconstructAndDisposeSerializedData()
	{
		this.ReconstructClip();
		this.DisposeSerializedData();
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00081938 File Offset: 0x0007FB38
	public void ReconstructClip()
	{
		this.reconstructedClip = new AnimationClip
		{
			name = this.clipName,
			frameRate = this.frameRate,
			wrapMode = this.wrapMode,
			legacy = true
		};
		for (int i = 0; i < this.curves.Count; i++)
		{
			PatchedAnimationClip.AnimationCurve animationCurve = this.curves[i];
			UnityEngine.AnimationCurve curve = animationCurve.Reconstruct();
			this.reconstructedClip.SetCurve(animationCurve.relativePath, PatchedAnimationClip.TRANSFORM_TYPE, animationCurve.propertyName, curve);
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0000BCBC File Offset: 0x00009EBC
	public void DisposeSerializedData()
	{
		this.curves = null;
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0000BCC5 File Offset: 0x00009EC5
	public void SetCurve(string path, Type _, string property, PatchedAnimationClip.AnimationCurve animationCurve)
	{
		animationCurve.relativePath = path;
		animationCurve.propertyName = property;
		this.curves.Add(animationCurve);
	}

	// Token: 0x04000F95 RID: 3989
	public string controllerName;

	// Token: 0x04000F96 RID: 3990
	public string clipName;

	// Token: 0x04000F97 RID: 3991
	public float frameRate;

	// Token: 0x04000F98 RID: 3992
	public WrapMode wrapMode;

	// Token: 0x04000F99 RID: 3993
	public List<PatchedAnimationClip.AnimationCurve> curves;

	// Token: 0x04000F9A RID: 3994
	[NonSerialized]
	public AnimationClip reconstructedClip;

	// Token: 0x04000F9B RID: 3995
	private static readonly Type TRANSFORM_TYPE = typeof(Transform);

	// Token: 0x02000226 RID: 550
	[Serializable]
	public struct AnimationCurve
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x000819C4 File Offset: 0x0007FBC4
		public AnimationCurve(PatchedAnimationClip.Keyframe[] frames)
		{
			this = default(PatchedAnimationClip.AnimationCurve);
			this.times = new int[frames.Length];
			this.values = new int[frames.Length];
			if (frames.Length == 0)
			{
				return;
			}
			int num = PatchedAnimationClip.AnimationCurve.Encode(frames[0].value);
			bool flag = true;
			for (int i = 0; i < frames.Length; i++)
			{
				PatchedAnimationClip.Keyframe keyframe = frames[i];
				this.times[i] = PatchedAnimationClip.AnimationCurve.Encode(keyframe.time);
				this.values[i] = PatchedAnimationClip.AnimationCurve.Encode(keyframe.value);
				if (this.values[i] != num)
				{
					flag = false;
				}
			}
			if (flag && frames.Length > 2)
			{
				PatchedAnimationClip.Keyframe keyframe2 = frames[0];
				PatchedAnimationClip.Keyframe keyframe3 = frames[frames.Length - 1];
				this.times = new int[]
				{
					PatchedAnimationClip.AnimationCurve.Encode(keyframe2.time),
					PatchedAnimationClip.AnimationCurve.Encode(keyframe3.time)
				};
				this.values = new int[]
				{
					PatchedAnimationClip.AnimationCurve.Encode(keyframe2.value),
					PatchedAnimationClip.AnimationCurve.Encode(keyframe3.value)
				};
			}
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00081ACC File Offset: 0x0007FCCC
		public UnityEngine.AnimationCurve Reconstruct()
		{
			UnityEngine.Keyframe[] array = new UnityEngine.Keyframe[this.times.Length];
			for (int i = 0; i < this.times.Length; i++)
			{
				array[i] = new UnityEngine.Keyframe(PatchedAnimationClip.AnimationCurve.Decode(this.times[i]), PatchedAnimationClip.AnimationCurve.Decode(this.values[i]));
			}
			return new UnityEngine.AnimationCurve(array);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		private static int Encode(float value)
		{
			return Mathf.RoundToInt(value * 100000f);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0000BD03 File Offset: 0x00009F03
		private static float Decode(int value)
		{
			return (float)value / 100000f;
		}

		// Token: 0x04000F9C RID: 3996
		private const int PRECISION = 100000;

		// Token: 0x04000F9D RID: 3997
		public string relativePath;

		// Token: 0x04000F9E RID: 3998
		public string propertyName;

		// Token: 0x04000F9F RID: 3999
		public int[] times;

		// Token: 0x04000FA0 RID: 4000
		public int[] values;
	}

	// Token: 0x02000227 RID: 551
	[Serializable]
	public struct Keyframe
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0000BD0D File Offset: 0x00009F0D
		public Keyframe(float time, float value)
		{
			this.time = time;
			this.value = value;
		}

		// Token: 0x04000FA1 RID: 4001
		public float time;

		// Token: 0x04000FA2 RID: 4002
		public float value;
	}
}
