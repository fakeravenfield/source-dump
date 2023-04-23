using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006A3 RID: 1699
	public class FloatInput : MonoBehaviour
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x0001D954 File Offset: 0x0001BB54
		public bool isFocuesed
		{
			get
			{
				return this.input.isFocused;
			}
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0001D961 File Offset: 0x0001BB61
		private void Start()
		{
			this.input.onEndEdit.AddListener(new UnityAction<string>(this.EndEdit));
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0001D97F File Offset: 0x0001BB7F
		public void SetDescription(string text)
		{
			this.description.text = text;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x00100E4C File Offset: 0x000FF04C
		public void SetValue(float value)
		{
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.oldText = text;
			this.input.text = text;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0001D98D File Offset: 0x0001BB8D
		public float GetValue()
		{
			return this.ParseValue(this.input.text);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x00100E7C File Offset: 0x000FF07C
		private float ParseValue(string text)
		{
			text = text.Replace(',', '.');
			string text2 = "";
			foreach (char c in text.Trim())
			{
				if ("0123456789".IndexOf(c) != -1)
				{
					text2 += c.ToString();
				}
				else if (c == '-')
				{
					if (text2.Length == 0)
					{
						text2 += c.ToString();
					}
				}
				else if (c == '.' && text2.IndexOf(c) == -1)
				{
					text2 += c.ToString();
				}
			}
			float result;
			if (float.TryParse(text2, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
			{
				return result;
			}
			return 0f;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x00100F34 File Offset: 0x000FF134
		private void EndEdit(string text)
		{
			if (text != this.oldText)
			{
				float num = this.ParseValue(text);
				this.SetValue(num);
				if (this.onValueChanged != null)
				{
					this.onValueChanged.Invoke(num);
				}
			}
		}

		// Token: 0x040027F7 RID: 10231
		public Text description;

		// Token: 0x040027F8 RID: 10232
		public InputField input;

		// Token: 0x040027F9 RID: 10233
		[NonSerialized]
		public FloatInput.ValueChangedEvent onValueChanged = new FloatInput.ValueChangedEvent();

		// Token: 0x040027FA RID: 10234
		private string oldText = "";

		// Token: 0x020006A4 RID: 1700
		public class ValueChangedEvent : UnityEvent<float>
		{
		}
	}
}
