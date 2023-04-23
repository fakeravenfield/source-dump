using System;
using Lua;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029B RID: 667
[OnlyStatic]
public class EffectUi : MonoBehaviour
{
	// Token: 0x060011B8 RID: 4536 RVA: 0x0000DEF2 File Offset: 0x0000C0F2
	private void Awake()
	{
		EffectUi.instance = this;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0000DEFA File Offset: 0x0000C0FA
	private void LateUpdate()
	{
		this.UpdateFade();
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x0008CDB4 File Offset: 0x0008AFB4
	private void UpdateFade()
	{
		object obj = !this.fadeAction.TrueDone() || this.fadeOut;
		float num = 0f;
		if (!this.fadeAction.TrueDone())
		{
			num = this.fadeAction.Ratio();
			if (this.fadeOut)
			{
				num = 1f - num;
			}
		}
		object obj2 = obj;
		if (obj2 != null && (this.fadeType == EffectUi.FadeType.FullScreen || this.fadeType == EffectUi.FadeType.EyeAndFullScreen))
		{
			this.fadeImage.enabled = true;
		}
		else
		{
			this.fadeImage.enabled = false;
		}
		if (obj2 != null && (this.fadeType == EffectUi.FadeType.Eye || this.fadeType == EffectUi.FadeType.EyeAndFullScreen))
		{
			this.eyeLidParent.gameObject.SetActive(true);
			this.eyeLidParent.localScale = new Vector3(1f, 0.1f + 0.9f * num, 1f);
			float y = 1f - num;
			float y2 = num;
			this.eyeLidTop.anchorMin = new Vector2(0f, y2);
			this.eyeLidTop.anchorMax = new Vector2(1f, y2);
			this.eyeLidBottom.anchorMin = new Vector2(0f, y);
			this.eyeLidBottom.anchorMax = new Vector2(1f, y);
			return;
		}
		this.eyeLidParent.gameObject.SetActive(false);
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0008CEFC File Offset: 0x0008B0FC
	public static void FadeOut(EffectUi.FadeType type, float duration, Color color)
	{
		EffectUi.ChangeFadeColor(color, 0f);
		if (EffectUi.instance.fadeType == EffectUi.FadeType.FullScreen || EffectUi.instance.fadeType == EffectUi.FadeType.EyeAndFullScreen)
		{
			Color color2 = color;
			color2.a = 0f;
			EffectUi.instance.fadeImage.canvasRenderer.SetColor(color2);
			EffectUi.instance.fadeImage.CrossFadeAlpha(1f, duration, false);
		}
		EffectUi.instance.fadeType = type;
		EffectUi.instance.fadeOut = true;
		EffectUi.instance.fadeAction.StartLifetime(duration);
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x0008CF8C File Offset: 0x0008B18C
	public static void FadeIn(EffectUi.FadeType type, float duration, Color color)
	{
		EffectUi.ChangeFadeColor(color, 0f);
		if (EffectUi.instance.fadeType == EffectUi.FadeType.FullScreen || EffectUi.instance.fadeType == EffectUi.FadeType.EyeAndFullScreen)
		{
			EffectUi.instance.fadeImage.canvasRenderer.SetColor(color);
			EffectUi.instance.fadeImage.CrossFadeAlpha(0f, duration, false);
		}
		EffectUi.instance.fadeType = type;
		EffectUi.instance.fadeOut = false;
		EffectUi.instance.fadeAction.StartLifetime(duration);
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x0008D010 File Offset: 0x0008B210
	public static void ChangeFadeColor(Color color, float changeTime)
	{
		Graphic[] array = EffectUi.instance.fadeGraphics;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CrossFadeColor(color, changeTime, false, false);
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0000DF02 File Offset: 0x0000C102
	public static void Clear()
	{
		EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0f, Color.black);
	}

	// Token: 0x040012D1 RID: 4817
	[Ignore]
	public static EffectUi instance;

	// Token: 0x040012D2 RID: 4818
	[Ignore]
	public Graphic[] fadeGraphics;

	// Token: 0x040012D3 RID: 4819
	[Ignore]
	public RawImage fadeImage;

	// Token: 0x040012D4 RID: 4820
	[Ignore]
	public RectTransform eyeLidParent;

	// Token: 0x040012D5 RID: 4821
	[Ignore]
	public RectTransform eyeLidTop;

	// Token: 0x040012D6 RID: 4822
	[Ignore]
	public RectTransform eyeLidBottom;

	// Token: 0x040012D7 RID: 4823
	private EffectUi.FadeType fadeType;

	// Token: 0x040012D8 RID: 4824
	private TimedAction fadeAction = new TimedAction(1f, false);

	// Token: 0x040012D9 RID: 4825
	private bool fadeOut;

	// Token: 0x0200029C RID: 668
	public enum FadeType
	{
		// Token: 0x040012DB RID: 4827
		FullScreen,
		// Token: 0x040012DC RID: 4828
		Eye,
		// Token: 0x040012DD RID: 4829
		EyeAndFullScreen
	}
}
