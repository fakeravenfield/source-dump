using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000696 RID: 1686
	public class ColorPickerUI : WindowBase
	{
		// Token: 0x06002ACF RID: 10959 RVA: 0x001008E0 File Offset: 0x000FEAE0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			Action<SliderWithInput> action = delegate(SliderWithInput s)
			{
				s.SetRange(0f, 255f);
				s.valueFormat = "0";
				s.onValueChanged.AddListener(new UnityAction<float>(this.SliderChanged));
			};
			action(this.sliderRed);
			action(this.sliderGreen);
			action(this.sliderBlue);
			action(this.sliderAlpha);
			this.textHex.onEndEdit.AddListener(new UnityAction<string>(this.HexChanged));
			this.buttonOk.onClick.AddListener(delegate()
			{
				this.Close(ColorPickerUI.DialogResult.Ok);
			});
			this.buttonCancel.onClick.AddListener(delegate()
			{
				this.Close(ColorPickerUI.DialogResult.Cancel);
			});
			this.InternalSetColor(this.color);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0001D620 File Offset: 0x0001B820
		public void SetColor(Color color)
		{
			this.color = color;
			base.Initialize();
			this.InternalSetColor(this.color);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0001D63B File Offset: 0x0001B83B
		public Color GetColor()
		{
			return this.color;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0001D643 File Offset: 0x0001B843
		public ColorPickerUI.DialogResult GetDialogResult()
		{
			return this.dialogResult;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0001D64B File Offset: 0x0001B84B
		public void SetCallback(ColorPickerUI.OnCloseCallback onClose)
		{
			this.onClose = onClose;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0001D654 File Offset: 0x0001B854
		public void IncreaseIntensity()
		{
			this.TweakIntensity(1.1f);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0001D661 File Offset: 0x0001B861
		public void DecreaseIntensity()
		{
			this.TweakIntensity(0.9f);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00100990 File Offset: 0x000FEB90
		private void TweakIntensity(float multiplier)
		{
			float r = this.sliderRed.GetValue() / 255f;
			float g = this.sliderGreen.GetValue() / 255f;
			float b = this.sliderBlue.GetValue() / 255f;
			float a = this.sliderAlpha.GetValue() / 255f;
			Color a2 = new Color(r, g, b, a);
			a2 *= multiplier;
			a2.a = a;
			this.InternalSetColor(a2);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0001D66E File Offset: 0x0001B86E
		private void Close(ColorPickerUI.DialogResult result)
		{
			this.dialogResult = result;
			base.Hide();
			if (this.onClose != null)
			{
				this.onClose(result);
				this.onClose = null;
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00100A0C File Offset: 0x000FEC0C
		private void InternalSetColor(Color color)
		{
			this.color = color;
			this.imagePreview.color = new Color(color.r, color.g, color.b, 1f);
			int num = Mathf.RoundToInt(color.r * 255f);
			int num2 = Mathf.RoundToInt(color.g * 255f);
			int num3 = Mathf.RoundToInt(color.b * 255f);
			int num4 = Mathf.RoundToInt(color.a * 255f);
			this.sliderRed.SetValue((float)num);
			this.sliderGreen.SetValue((float)num2);
			this.sliderBlue.SetValue((float)num3);
			this.sliderAlpha.SetValue((float)num4);
			this.textHex.SetText(Utils.ColorToHex(color).Substring(1));
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00100ADC File Offset: 0x000FECDC
		private void HexChanged(string hex)
		{
			Color color = Utils.HexToColor(hex);
			this.InternalSetColor(color);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00100AF8 File Offset: 0x000FECF8
		private void SliderChanged(float _)
		{
			float r = this.sliderRed.GetValue() / 255f;
			float g = this.sliderGreen.GetValue() / 255f;
			float b = this.sliderBlue.GetValue() / 255f;
			float a = this.sliderAlpha.GetValue() / 255f;
			Color color = new Color(r, g, b, a);
			this.InternalSetColor(color);
		}

		// Token: 0x040027D4 RID: 10196
		public SliderWithInput sliderRed;

		// Token: 0x040027D5 RID: 10197
		public SliderWithInput sliderGreen;

		// Token: 0x040027D6 RID: 10198
		public SliderWithInput sliderBlue;

		// Token: 0x040027D7 RID: 10199
		public SliderWithInput sliderAlpha;

		// Token: 0x040027D8 RID: 10200
		public InputWithText textHex;

		// Token: 0x040027D9 RID: 10201
		public Image imagePreview;

		// Token: 0x040027DA RID: 10202
		public Button buttonOk;

		// Token: 0x040027DB RID: 10203
		public Button buttonCancel;

		// Token: 0x040027DC RID: 10204
		private ColorPickerUI.DialogResult dialogResult;

		// Token: 0x040027DD RID: 10205
		private ColorPickerUI.OnCloseCallback onClose;

		// Token: 0x040027DE RID: 10206
		private Color color = Color.magenta;

		// Token: 0x02000697 RID: 1687
		public enum DialogResult
		{
			// Token: 0x040027E0 RID: 10208
			Ok,
			// Token: 0x040027E1 RID: 10209
			Cancel
		}

		// Token: 0x02000698 RID: 1688
		// (Invoke) Token: 0x06002AE0 RID: 10976
		public delegate void OnCloseCallback(ColorPickerUI.DialogResult dialogResult);
	}
}
