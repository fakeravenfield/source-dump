using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class SpecialEffect : MonoBehaviour
{
	// Token: 0x0600094E RID: 2382 RVA: 0x0006AB48 File Offset: 0x00068D48
	private void Start()
	{
		float num = 0f;
		foreach (SpecialEffect.MaterialColorTween materialColorTween in this.colorTweens)
		{
			materialColorTween.Start();
			num = Mathf.Max(num, materialColorTween.duration);
		}
		this.tweenAction = new TimedAction(num, false);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0006ABBC File Offset: 0x00068DBC
	private void Update()
	{
		bool flag = !this.tweenAction.TrueDone();
		if (flag)
		{
			using (List<SpecialEffect.MaterialColorTween>.Enumerator enumerator = this.colorTweens.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpecialEffect.MaterialColorTween materialColorTween = enumerator.Current;
					materialColorTween.Update(this.tweenAction.Elapsed());
				}
				goto IL_8B;
			}
		}
		if (this.tweenActionRunningLastFrame)
		{
			foreach (SpecialEffect.MaterialColorTween materialColorTween2 in this.colorTweens)
			{
				materialColorTween2.AssignFinalColor();
			}
		}
		IL_8B:
		this.tweenActionRunningLastFrame = flag;
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0000833C File Offset: 0x0000653C
	private void PlaySpecialEffect()
	{
		this.particles.Play();
		this.tweenAction.Start();
	}

	// Token: 0x04000A2F RID: 2607
	public ParticleSystem particles;

	// Token: 0x04000A30 RID: 2608
	public List<SpecialEffect.MaterialColorTween> colorTweens = new List<SpecialEffect.MaterialColorTween>();

	// Token: 0x04000A31 RID: 2609
	private TimedAction tweenAction = new TimedAction(0f, false);

	// Token: 0x04000A32 RID: 2610
	private bool tweenActionRunningLastFrame;

	// Token: 0x0200015D RID: 349
	[Serializable]
	public class MaterialColorTween
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x00008378 File Offset: 0x00006578
		public void Start()
		{
			this.material = this.renderer.materials[this.targetMaterialIndex];
			this.sourceColor = this.material.GetColor(this.colorName);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0006AC78 File Offset: 0x00068E78
		public void Update(float time)
		{
			float t = this.lerpCurve.Evaluate(time / this.duration);
			this.AssignColor(Color.Lerp(this.targetColor, this.sourceColor, t));
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000083A9 File Offset: 0x000065A9
		public void AssignFinalColor()
		{
			this.AssignColor(this.targetColor);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000083B7 File Offset: 0x000065B7
		private void AssignColor(Color c)
		{
			this.material.SetColor(this.colorName, c);
		}

		// Token: 0x04000A33 RID: 2611
		public Renderer renderer;

		// Token: 0x04000A34 RID: 2612
		public int targetMaterialIndex;

		// Token: 0x04000A35 RID: 2613
		public string colorName;

		// Token: 0x04000A36 RID: 2614
		public Color targetColor;

		// Token: 0x04000A37 RID: 2615
		public AnimationCurve lerpCurve;

		// Token: 0x04000A38 RID: 2616
		public float duration;

		// Token: 0x04000A39 RID: 2617
		private Color sourceColor;

		// Token: 0x04000A3A RID: 2618
		private Material material;
	}
}
