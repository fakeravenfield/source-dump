using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006B2 RID: 1714
	public class SliderWithInput : MonoBehaviour, IValueChangeCallbackProvider
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x0001DB4A File Offset: 0x0001BD4A
		public bool isFocused
		{
			get
			{
				return this.input.isFocused || MeInput.instance.IsFocusOnUI(this.slider);
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x001012A0 File Offset: 0x000FF4A0
		private void Initialize()
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.slider = base.GetComponentInChildren<Slider>();
				this.input = base.GetComponentInChildren<InputField>();
				this.description = base.GetComponentInChildren<Text>();
				this.slider.minValue = this.minSliderValue;
				this.slider.maxValue = this.maxSliderValue;
			}
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0001DB6B File Offset: 0x0001BD6B
		private void Start()
		{
			this.Initialize();
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x00101304 File Offset: 0x000FF504
		private void Update()
		{
			if (!this.input.isFocused && this.input.text != this.previousText)
			{
				float v;
				if (float.TryParse(this.input.text, out v))
				{
					this.InternalSetValue(v, false);
					return;
				}
			}
			else if (this.slider.value != this.previousSlider)
			{
				this.InternalSetValue(this.slider.value, false);
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0001DB73 File Offset: 0x0001BD73
		public float GetMaximumValue()
		{
			return this.maxInputValue;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00101378 File Offset: 0x000FF578
		public void SetRange(float minValue, float maxInputValue, float maxSliderValue)
		{
			this.Initialize();
			if (maxInputValue < maxSliderValue)
			{
				Debug.LogError("Maximum input value must be larger or equal to slider.");
				maxInputValue = maxSliderValue;
			}
			this.minSliderValue = minValue;
			this.maxSliderValue = maxSliderValue;
			this.maxInputValue = maxInputValue;
			this.slider.minValue = this.minSliderValue;
			this.slider.maxValue = this.maxSliderValue;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0001DB7B File Offset: 0x0001BD7B
		public void SetRange(float minValue, float maxValue)
		{
			this.SetRange(minValue, maxValue, maxValue);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0001DB86 File Offset: 0x0001BD86
		public void SetDescription(string text)
		{
			this.Initialize();
			this.description.text = text;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0001DB9A File Offset: 0x0001BD9A
		public void SetValue(float v)
		{
			this.InternalSetValue(v, true);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x001013D4 File Offset: 0x000FF5D4
		private void InternalSetValue(float v, bool quite = false)
		{
			this.Initialize();
			v = Mathf.Min(this.maxInputValue, Mathf.Max(this.minSliderValue, v));
			bool flag = this.value != v;
			this.value = v;
			this.slider.value = v;
			this.previousSlider = this.slider.value;
			if (string.IsNullOrEmpty(this.valueFormat) && Mathf.Abs(v) > 10f)
			{
				Mathf.Abs(v);
			}
			this.input.text = v.ToString(this.valueFormat);
			this.previousText = this.input.text;
			if (!quite && flag && this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0001DBA4 File Offset: 0x0001BDA4
		public float GetValue()
		{
			this.Initialize();
			return this.value;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x001014A4 File Offset: 0x000FF6A4
		public void RegisterOnValueChangeCallback(DelOnValueChangedCallback callback)
		{
			this.onValueChanged.AddListener(delegate(float a)
			{
				callback();
			});
		}

		// Token: 0x04002810 RID: 10256
		public string valueFormat;

		// Token: 0x04002811 RID: 10257
		public Slider.SliderEvent onValueChanged;

		// Token: 0x04002812 RID: 10258
		private Slider slider;

		// Token: 0x04002813 RID: 10259
		private InputField input;

		// Token: 0x04002814 RID: 10260
		private Text description;

		// Token: 0x04002815 RID: 10261
		private float value;

		// Token: 0x04002816 RID: 10262
		private float maxInputValue = 1f;

		// Token: 0x04002817 RID: 10263
		private float maxSliderValue = 1f;

		// Token: 0x04002818 RID: 10264
		private float minSliderValue;

		// Token: 0x04002819 RID: 10265
		private string previousText;

		// Token: 0x0400281A RID: 10266
		private float previousSlider;

		// Token: 0x0400281B RID: 10267
		private bool isInitialized;
	}
}
