using System;
using System.Linq;
using Ravenfield.Mutator.Configuration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C0 RID: 704
public class MutatorConfigField : MonoBehaviour
{
	// Token: 0x060012C6 RID: 4806 RVA: 0x00091060 File Offset: 0x0008F260
	public void SetField(MutatorConfigurationSortableField field)
	{
		this.InitializeFields(field);
		this.fieldLabel.text = field.displayName;
		if (this.label != null)
		{
			this.fieldLabel.rectTransform.anchorMax = new Vector2(1f, 1f);
			this.fieldLabel.alignment = TextAnchor.MiddleCenter;
		}
		else if (this.intField != null)
		{
			this.textFieldParent.SetActive(true);
			this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnIntSubmit));
		}
		else if (this.floatField != null)
		{
			this.textFieldParent.SetActive(true);
			this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnFloatSubmit));
		}
		else if (this.stringField != null)
		{
			this.textFieldParent.SetActive(true);
			this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnStringSubmit));
		}
		else if (this.rangeField != null)
		{
			this.sliderParent.SetActive(true);
			this.slider.minValue = this.rangeField.value.min;
			this.slider.maxValue = this.rangeField.value.max;
			this.slider.wholeNumbers = this.rangeField.value.wholeNumbers;
			this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnRangeSubmit));
		}
		else if (this.boolField != null)
		{
			this.toggleParent.SetActive(true);
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnBoolSubmit));
		}
		else if (this.dropdownField != null)
		{
			this.dropdownParent.SetActive(true);
			this.dropdown.ClearOptions();
			this.dropdown.AddOptions(this.dropdownField.value.labels.ToList<string>());
			this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDropdownSubmit));
		}
		this.UpdateDisplayValue();
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x0000ED71 File Offset: 0x0000CF71
	private void OnDropdownSubmit(int arg0)
	{
		if (this.ignoreSubmit)
		{
			return;
		}
		this.dropdownField.value.index = arg0;
		this.UpdateDisplayValue();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0000ED93 File Offset: 0x0000CF93
	private void OnBoolSubmit(bool arg0)
	{
		if (this.ignoreSubmit)
		{
			return;
		}
		this.boolField.value = arg0;
		this.UpdateDisplayValue();
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00091278 File Offset: 0x0008F478
	private void OnRangeSubmit(float arg0)
	{
		if (this.ignoreSubmit)
		{
			return;
		}
		float value = Mathf.Round(this.slider.value * 100f) / 100f;
		this.rangeField.value.value = value;
		this.UpdateDisplayValue();
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
	private void OnStringSubmit(string arg0)
	{
		if (this.ignoreSubmit)
		{
			return;
		}
		this.stringField.value = arg0;
		this.UpdateDisplayValue();
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x0000EDCD File Offset: 0x0000CFCD
	private void OnFloatSubmit(string arg0)
	{
		if (this.ignoreSubmit)
		{
			return;
		}
		float.TryParse(arg0, out this.floatField.value);
		this.UpdateDisplayValue();
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
	private void OnIntSubmit(string arg0)
	{
		if (this.ignoreSubmit)
		{
			return;
		}
		int.TryParse(arg0, out this.intField.value);
		this.UpdateDisplayValue();
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x0000EE13 File Offset: 0x0000D013
	private void OnEnable()
	{
		this.UpdateDisplayValue();
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000912C4 File Offset: 0x0008F4C4
	private void UpdateDisplayValue()
	{
		this.ignoreSubmit = true;
		try
		{
			if (this.intField != null)
			{
				this.inputField.text = this.intField.value.ToString();
			}
			else if (this.floatField != null)
			{
				this.inputField.text = this.floatField.value.ToString();
			}
			else if (this.stringField != null)
			{
				this.inputField.text = this.stringField.value;
			}
			else if (this.rangeField != null)
			{
				this.slider.value = this.rangeField.value.value;
				this.sliderLabel.text = this.rangeField.value.value.ToString();
			}
			else if (this.boolField != null)
			{
				this.toggle.isOn = this.boolField.value;
			}
			else if (this.dropdownField != null)
			{
				this.dropdown.value = this.dropdownField.value.index;
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		this.ignoreSubmit = false;
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000913F4 File Offset: 0x0008F5F4
	private void InitializeFields(MutatorConfigurationSortableField field)
	{
		this.label = (field as MutatorConfigurationLabel);
		this.intField = (field as IntegerConfigurationField);
		this.floatField = (field as FloatConfigurationField);
		this.stringField = (field as StringConfigurationField);
		this.rangeField = (field as RangeConfigurationField);
		this.boolField = (field as BoolConfigurationField);
		this.dropdownField = (field as DropdownConfigurationField);
	}

	// Token: 0x04001405 RID: 5125
	public GameObject textFieldParent;

	// Token: 0x04001406 RID: 5126
	public GameObject sliderParent;

	// Token: 0x04001407 RID: 5127
	public GameObject toggleParent;

	// Token: 0x04001408 RID: 5128
	public GameObject dropdownParent;

	// Token: 0x04001409 RID: 5129
	public Text fieldLabel;

	// Token: 0x0400140A RID: 5130
	public InputField inputField;

	// Token: 0x0400140B RID: 5131
	public Slider slider;

	// Token: 0x0400140C RID: 5132
	public Text sliderLabel;

	// Token: 0x0400140D RID: 5133
	public Toggle toggle;

	// Token: 0x0400140E RID: 5134
	public Dropdown dropdown;

	// Token: 0x0400140F RID: 5135
	private MutatorConfigurationLabel label;

	// Token: 0x04001410 RID: 5136
	private IntegerConfigurationField intField;

	// Token: 0x04001411 RID: 5137
	private FloatConfigurationField floatField;

	// Token: 0x04001412 RID: 5138
	private StringConfigurationField stringField;

	// Token: 0x04001413 RID: 5139
	private RangeConfigurationField rangeField;

	// Token: 0x04001414 RID: 5140
	private BoolConfigurationField boolField;

	// Token: 0x04001415 RID: 5141
	private DropdownConfigurationField dropdownField;

	// Token: 0x04001416 RID: 5142
	private bool ignoreSubmit;
}
