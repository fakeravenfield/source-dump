using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000699 RID: 1689
	public class ColorPicker : MonoBehaviour, IValueChangeCallbackProvider
	{
		// Token: 0x06002AE3 RID: 10979 RVA: 0x0001D6F1 File Offset: 0x0001B8F1
		private void Start()
		{
			this.colorPickerWindow = MapEditor.instance.GetEditorUI().colorPickerWindow;
			this.buttonPick.onClick.AddListener(new UnityAction(this.PickColor));
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0001D724 File Offset: 0x0001B924
		private void PickColor()
		{
			this.colorPickerWindow.SetColor(this.selectedColor);
			this.colorPickerWindow.SetCallback(new ColorPickerUI.OnCloseCallback(this.PickerClosed));
			this.colorPickerWindow.Show();
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00100B60 File Offset: 0x000FED60
		private void PickerClosed(ColorPickerUI.DialogResult result)
		{
			if (result == ColorPickerUI.DialogResult.Ok)
			{
				Color color = this.colorPickerWindow.GetColor();
				this.InternalSetColor(color, true);
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00100B84 File Offset: 0x000FED84
		private void InternalSetColor(Color color, bool notify)
		{
			this.selectedColor = color;
			this.imagePreview.color = new Color(color.r, color.g, color.b, 1f);
			this.textColorValue.text = Utils.ColorToHex(color);
			if (notify)
			{
				this.onColorChanged.Invoke(color);
			}
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0001D759 File Offset: 0x0001B959
		public Color GetColor()
		{
			return this.selectedColor;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0001D761 File Offset: 0x0001B961
		public void SetColor(Color color)
		{
			this.InternalSetColor(color, false);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0001D76B File Offset: 0x0001B96B
		public void SetDescription(string description)
		{
			this.textDescription.text = description;
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x00100BE0 File Offset: 0x000FEDE0
		public void RegisterOnValueChangeCallback(DelOnValueChangedCallback callback)
		{
			this.onColorChanged.AddListener(delegate(Color a)
			{
				callback();
			});
		}

		// Token: 0x040027E2 RID: 10210
		public Text textDescription;

		// Token: 0x040027E3 RID: 10211
		public Text textColorValue;

		// Token: 0x040027E4 RID: 10212
		public Image imagePreview;

		// Token: 0x040027E5 RID: 10213
		public Button buttonPick;

		// Token: 0x040027E6 RID: 10214
		[NonSerialized]
		public ColorPicker.ColorChangedEvent onColorChanged = new ColorPicker.ColorChangedEvent();

		// Token: 0x040027E7 RID: 10215
		private ColorPickerUI colorPickerWindow;

		// Token: 0x040027E8 RID: 10216
		private Color selectedColor;

		// Token: 0x0200069A RID: 1690
		public class ColorChangedEvent : UnityEvent<Color>
		{
		}
	}
}
