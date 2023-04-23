using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C7 RID: 711
public class OptionResolution : MonoBehaviour
{
	// Token: 0x060012F0 RID: 4848 RVA: 0x000918AC File Offset: 0x0008FAAC
	private void Start()
	{
		this.uiElement = base.GetComponentInChildren<Dropdown>();
		this.uiElement.ClearOptions();
		this.options = new List<string>();
		this.resolutions = new List<int[]>();
		int num = 0;
		int num2 = 0;
		foreach (Resolution resolution in Screen.resolutions)
		{
			if (resolution.width != num || resolution.height != num2)
			{
				this.options.Add(resolution.width.ToString() + " x " + resolution.height.ToString());
				this.resolutions.Add(new int[]
				{
					resolution.width,
					resolution.height
				});
				num = resolution.width;
				num2 = resolution.height;
			}
		}
		this.uiElement.AddOptions(this.options);
		this.Load();
		this.uiElement.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChange));
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x000919C0 File Offset: 0x0008FBC0
	public void Save()
	{
		PlayerPrefs.SetInt("d_ResolutionX", this.resolutions[this.uiElement.value][0]);
		PlayerPrefs.SetInt("d_ResolutionY", this.resolutions[this.uiElement.value][1]);
		this.valueChanged = false;
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x0000EFD6 File Offset: 0x0000D1D6
	private void OnValueChange(int index)
	{
		this.valueChanged = true;
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x00091A18 File Offset: 0x0008FC18
	private void Load()
	{
		int[] array = new int[]
		{
			PlayerPrefs.GetInt("d_ResolutionX", Screen.width),
			PlayerPrefs.GetInt("d_ResolutionY", Screen.height)
		};
		string item = array[0].ToString() + " x " + array[1].ToString();
		if (this.options.Contains(item))
		{
			this.uiElement.value = this.options.IndexOf(item);
			return;
		}
		this.options.Add(item);
		this.resolutions.Add(array);
		this.uiElement.value = this.options.Count - 1;
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x00091ACC File Offset: 0x0008FCCC
	public void Apply()
	{
		int width = this.resolutions[this.uiElement.value][0];
		int height = this.resolutions[this.uiElement.value][1];
		Screen.SetResolution(width, height, Options.GetToggle(OptionToggle.Id.FullScreen));
	}

	// Token: 0x0400143C RID: 5180
	private Dropdown uiElement;

	// Token: 0x0400143D RID: 5181
	private List<string> options;

	// Token: 0x0400143E RID: 5182
	private List<int[]> resolutions;

	// Token: 0x0400143F RID: 5183
	[NonSerialized]
	public bool valueChanged;
}
