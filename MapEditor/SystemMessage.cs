using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006B4 RID: 1716
	public class SystemMessage : MonoBehaviour
	{
		// Token: 0x06002B50 RID: 11088 RVA: 0x001014D8 File Offset: 0x000FF6D8
		private void Start()
		{
			this.image = base.GetComponent<Image>();
			this.startTime = Time.time;
			this.fadeDuration = this.fadeOut.keys[this.fadeOut.length - 1].time;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x00101524 File Offset: 0x000FF724
		private void Update()
		{
			float num = Time.time - this.startTime;
			if (num > this.showDuration)
			{
				num -= this.showDuration;
				float b = this.fadeOut.Evaluate(num);
				Color color = this.image.color;
				color.a = Mathf.Min(color.a, b);
				this.image.color = color;
				if (num > this.fadeDuration)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0000A756 File Offset: 0x00008956
		public void SetText(string text)
		{
			base.GetComponentInChildren<Text>().text = text;
		}

		// Token: 0x0400281D RID: 10269
		public AnimationCurve fadeOut = AnimationCurve.EaseInOut(0f, 1f, 0.5f, 0f);

		// Token: 0x0400281E RID: 10270
		public float showDuration = 3f;

		// Token: 0x0400281F RID: 10271
		private Image image;

		// Token: 0x04002820 RID: 10272
		private float startTime;

		// Token: 0x04002821 RID: 10273
		private float fadeDuration;
	}
}
