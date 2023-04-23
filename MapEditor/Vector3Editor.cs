using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006BE RID: 1726
	public class Vector3Editor : MonoBehaviour
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x0001DE90 File Offset: 0x0001C090
		public bool isFocused
		{
			get
			{
				return this.xInput.isFocuesed || this.yInput.isFocuesed || this.zInput.isFocuesed;
			}
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00101F14 File Offset: 0x00100114
		private void Start()
		{
			this.xInput.onValueChanged.AddListener(new UnityAction<float>(this.InputChanged));
			this.yInput.onValueChanged.AddListener(new UnityAction<float>(this.InputChanged));
			this.zInput.onValueChanged.AddListener(new UnityAction<float>(this.InputChanged));
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x0001DEB9 File Offset: 0x0001C0B9
		public void SetDescription(string text)
		{
			this.description.text = text;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x0001DEC7 File Offset: 0x0001C0C7
		public void SetValue(Vector3 value)
		{
			this.xInput.SetValue(value.x);
			this.yInput.SetValue(value.y);
			this.zInput.SetValue(value.z);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0001DEFC File Offset: 0x0001C0FC
		public Vector3 GetValue()
		{
			return new Vector3(this.xInput.GetValue(), this.yInput.GetValue(), this.zInput.GetValue());
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0001DF24 File Offset: 0x0001C124
		private void InputChanged(float _)
		{
			this.OnValueChanged();
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0001DF2C File Offset: 0x0001C12C
		private void OnValueChanged()
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.GetValue());
			}
		}

		// Token: 0x04002848 RID: 10312
		public FloatInput xInput;

		// Token: 0x04002849 RID: 10313
		public FloatInput yInput;

		// Token: 0x0400284A RID: 10314
		public FloatInput zInput;

		// Token: 0x0400284B RID: 10315
		public Text description;

		// Token: 0x0400284C RID: 10316
		[NonSerialized]
		public Vector3Editor.ValueChangedEvent onValueChanged = new Vector3Editor.ValueChangedEvent();

		// Token: 0x020006BF RID: 1727
		public class ValueChangedEvent : UnityEvent<Vector3>
		{
		}
	}
}
