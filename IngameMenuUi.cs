using System;
using MapEditor;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020002A7 RID: 679
public class IngameMenuUi : MonoBehaviour
{
	// Token: 0x060011EA RID: 4586 RVA: 0x0000E10E File Offset: 0x0000C30E
	public static void Show()
	{
		IngameMenuUi.instance.canvas.enabled = true;
		GameManager.PauseGame();
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x0000E125 File Offset: 0x0000C325
	public static void Hide()
	{
		IngameMenuUi.instance.canvas.enabled = false;
		if (Options.IsOpen())
		{
			Options.SaveAndClose();
		}
		GameManager.UnpauseGame();
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x0000E148 File Offset: 0x0000C348
	public static bool IsOpen()
	{
		return IngameMenuUi.instance.canvas.enabled;
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0008D900 File Offset: 0x0008BB00
	private void Awake()
	{
		IngameMenuUi.instance = this;
		if (GameManager.IsPlayingCampaign())
		{
			GameObject[] array = this.unavailableInCampaign;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			this.surrenderButton.SetActive(true);
		}
		this.canvas = base.GetComponent<Canvas>();
		this.canvas.enabled = false;
		this.backToEditorButton.SetActive(MapEditor.isTestingMap);
		IngameMenuUi.Hide();
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x0000E159 File Offset: 0x0000C359
	public void Resume()
	{
		IngameMenuUi.Hide();
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0000E160 File Offset: 0x0000C360
	public void Restart()
	{
		GameManager.RestartLevel();
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0008D974 File Offset: 0x0008BB74
	public void BackToEditor()
	{
		try
		{
			GameManager.UnpauseGame();
			SceneConstructor.GotoLoadingScreen(MapEditor.descriptorFilePath, SceneConstructor.Mode.Edit, GameManager.GameParameters().nightMode);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			this.Menu();
		}
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0000E167 File Offset: 0x0000C367
	public void ShowOptions()
	{
		Options.Show();
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0000E16E File Offset: 0x0000C36E
	public void Menu()
	{
		GameManager.ReturnToMenu();
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0000E175 File Offset: 0x0000C375
	public void Surrender()
	{
		GameModeBase.instance.OnSurrender();
		GameManager.ReturnToCampaignLobby();
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x0000ACEF File Offset: 0x00008EEF
	public void Quit()
	{
		Application.Quit();
	}

	// Token: 0x04001318 RID: 4888
	public static IngameMenuUi instance;

	// Token: 0x04001319 RID: 4889
	public UnityEngine.Audio.AudioMixer mixer;

	// Token: 0x0400131A RID: 4890
	public GameObject backToEditorButton;

	// Token: 0x0400131B RID: 4891
	public GameObject surrenderButton;

	// Token: 0x0400131C RID: 4892
	[NonSerialized]
	public Canvas canvas;

	// Token: 0x0400131D RID: 4893
	public GameObject[] unavailableInCampaign;
}
