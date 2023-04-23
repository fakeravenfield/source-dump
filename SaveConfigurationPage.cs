using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000201 RID: 513
public class SaveConfigurationPage : MonoBehaviour
{
	// Token: 0x06000DAC RID: 3500 RVA: 0x0000B09F File Offset: 0x0000929F
	public static string GetFolderPath()
	{
		return Application.persistentDataPath + "/GameConfigurations/";
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0007D1C4 File Offset: 0x0007B3C4
	private void OnEnable()
	{
		this.okFeedbackObject.SetActive(false);
		if (InstantActionMaps.instance.gameConfigDropdown.value == 0)
		{
			this.nameField.text = "MyNewConfig";
		}
		else
		{
			int value = InstantActionMaps.instance.gameConfigDropdown.value;
			string text = InstantActionMaps.instance.gameConfigDropdown.options[value].text;
			int num = value - 2;
			if (num >= 0 && InstantActionMaps.instance.gameConfigurationPaths[num].sourceMod != null)
			{
				text += " copy";
			}
			this.nameField.text = text;
		}
		this.ignoreIconCallbacks = true;
		this.iconHorizontalSlider.value = 0f;
		this.iconVerticalSlider.value = 0f;
		this.iconZoomSlider.value = 0f;
		if (GameManager.IsConnectedToSteam())
		{
			this.ResetIconUi("A GAME CONFIGURATION", "BY " + GameManager.instance.steamworks.GetSteamNick());
		}
		else
		{
			this.ResetIconUi("A GAME CONFIGURATION", "BY *YOUR NAME HERE*");
		}
		GamePreview.instance.SetIconCameraEnabled(true);
		this.ignoreIconCallbacks = false;
		this.OnIconCameraChanged();
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0000296E File Offset: 0x00000B6E
	public void Upload()
	{
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0000B0B0 File Offset: 0x000092B0
	private void ResetIconUi(string header, string footer)
	{
		GamePreview.instance.SetupIconUi(true, header, true, footer);
		this.iconHeaderToggle.isOn = true;
		this.iconFooterToggle.isOn = true;
		this.iconHeaderInput.text = header;
		this.iconFooterInput.text = footer;
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0000B0F0 File Offset: 0x000092F0
	private void OnDisable()
	{
		GamePreview.instance.SetIconCameraEnabled(false);
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0007D2F0 File Offset: 0x0007B4F0
	public void Save()
	{
		string text = this.nameField.text;
		if (string.IsNullOrEmpty(text))
		{
			text = "MyNewConfig";
		}
		string str = text + ".rgc";
		string folderPath = SaveConfigurationPage.GetFolderPath();
		string path = folderPath + str;
		if (!Directory.Exists(folderPath))
		{
			Directory.CreateDirectory(folderPath);
		}
		SerializedGameInfo serializedGameInfo = new SerializedGameInfo(GameManager.instance.gameInfo);
		serializedGameInfo.playerHasAllWeapons = InstantActionMaps.instance.playerHasAllWeaponsToggle.isOn;
		try
		{
			serializedGameInfo.WriteToFile(path);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			return;
		}
		this.okFeedbackObject.SetActive(true);
		InstantActionMaps.instance.SetupGameConfigurationPaths();
		InstantActionMaps.instance.SelectGameConfiguration(text);
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0000B0FD File Offset: 0x000092FD
	public void OnIconCameraChanged()
	{
		if (this.ignoreIconCallbacks)
		{
			return;
		}
		GamePreview.instance.UpdateIconCamera(this.iconHorizontalSlider.value, this.iconVerticalSlider.value, this.iconZoomSlider.value);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0007D3AC File Offset: 0x0007B5AC
	public void OnIconUiChanged()
	{
		if (this.ignoreIconCallbacks)
		{
			return;
		}
		GamePreview.instance.SetupIconUi(this.iconHeaderToggle.isOn, this.iconHeaderInput.text, this.iconFooterToggle.isOn, this.iconFooterInput.text);
	}

	// Token: 0x04000EC1 RID: 3777
	public const string FILE_EXTENSION = ".rgc";

	// Token: 0x04000EC2 RID: 3778
	public const string FILE_DATA_FOLDER = "/GameConfigurations/";

	// Token: 0x04000EC3 RID: 3779
	public InputField nameField;

	// Token: 0x04000EC4 RID: 3780
	public GameObject okFeedbackObject;

	// Token: 0x04000EC5 RID: 3781
	public Slider iconHorizontalSlider;

	// Token: 0x04000EC6 RID: 3782
	public Slider iconVerticalSlider;

	// Token: 0x04000EC7 RID: 3783
	public Slider iconZoomSlider;

	// Token: 0x04000EC8 RID: 3784
	public Toggle iconHeaderToggle;

	// Token: 0x04000EC9 RID: 3785
	public InputField iconHeaderInput;

	// Token: 0x04000ECA RID: 3786
	public Toggle iconFooterToggle;

	// Token: 0x04000ECB RID: 3787
	public InputField iconFooterInput;

	// Token: 0x04000ECC RID: 3788
	private bool ignoreIconCallbacks;
}
