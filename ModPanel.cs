using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F6 RID: 502
public class ModPanel : MonoBehaviour
{
	// Token: 0x06000D75 RID: 3445 RVA: 0x0007CB5C File Offset: 0x0007AD5C
	private void Awake()
	{
		ButtonHoverTransition[] components = this.disableButton.GetComponents<ButtonHoverTransition>();
		this.disableButtonOverlayText = components[0];
		this.disableButtonGraphic = components[1];
		this.modPage = UnityEngine.Object.FindObjectOfType<ModPage>();
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0000ADCD File Offset: 0x00008FCD
	public void OnClickToggleEnabled()
	{
		this.modPage.UserWantsToToggle(this);
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0000ADDB File Offset: 0x00008FDB
	public void ShowEnabled()
	{
		this.disableText.text = "DISABLE";
		this.disableButtonGraphic.ReleaseOverride();
		this.disableButtonOverlayText.ReleaseOverride();
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0007CB94 File Offset: 0x0007AD94
	public void ShowDisabled()
	{
		this.disableText.text = "DISABLED";
		this.disableButtonGraphic.OverrideColor(new Color(0.2f, 0.2f, 0.2f, 1f));
		this.disableButtonOverlayText.OverrideColor(Color.white);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0000AE03 File Offset: 0x00009003
	public void SetupWorkshopMod(ulong itemId)
	{
		this.workshopUrl = "http://steamcommunity.com/sharedfiles/filedetails/?id=" + itemId.ToString();
		this.workshopLinkButton.gameObject.SetActive(true);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0000AE2D File Offset: 0x0000902D
	public void SetupStagedMod()
	{
		this.workshopLinkButton.gameObject.SetActive(false);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0007CBE8 File Offset: 0x0007ADE8
	public void ShowError(string message)
	{
		this.disableButton.enabled = false;
		this.disableButtonGraphic.enabled = false;
		this.disableButtonOverlayText.enabled = false;
		this.disableButtonGraphic.overlayGraphic.color = Color.red;
		this.disableText.text = "ERROR";
		this.description.text = message;
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0000AE40 File Offset: 0x00009040
	public void OpenWorkshopPage()
	{
		GameManager.instance.steamworks.OpenUrl(this.workshopUrl, true);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0000AE58 File Offset: 0x00009058
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			this.ShowLoading();
		}
		if (Input.GetKeyUp(KeyCode.L))
		{
			this.EndLoading();
		}
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0000AE78 File Offset: 0x00009078
	public void SetIconTexture(Texture2D texture)
	{
		this.image.texture = texture;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0000AE86 File Offset: 0x00009086
	public void ShowLoading()
	{
		this.disableButtonGraphic.OverrideColor(Color.clear);
		this.disableButtonGraphic.OverrideColor(Color.gray);
		this.disableButton.enabled = false;
		this.spinner.SetActive(true);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0000AEC0 File Offset: 0x000090C0
	public void EndLoading()
	{
		this.disableButtonGraphic.ReleaseOverride();
		this.disableButtonGraphic.ReleaseOverride();
		this.disableButton.enabled = true;
		this.spinner.SetActive(false);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0000AEF0 File Offset: 0x000090F0
	public void SetText(string title, string description)
	{
		this.title.text = title;
		this.description.text = description;
	}

	// Token: 0x04000E90 RID: 3728
	private const string WORKSHOP_URL = "http://steamcommunity.com/sharedfiles/filedetails/?id=";

	// Token: 0x04000E91 RID: 3729
	public string workshopUrl;

	// Token: 0x04000E92 RID: 3730
	public Text title;

	// Token: 0x04000E93 RID: 3731
	public Text description;

	// Token: 0x04000E94 RID: 3732
	public Text disableText;

	// Token: 0x04000E95 RID: 3733
	public RawImage image;

	// Token: 0x04000E96 RID: 3734
	public Button disableButton;

	// Token: 0x04000E97 RID: 3735
	public GameObject spinner;

	// Token: 0x04000E98 RID: 3736
	public Button workshopLinkButton;

	// Token: 0x04000E99 RID: 3737
	private ModPage modPage;

	// Token: 0x04000E9A RID: 3738
	private ButtonHoverTransition disableButtonOverlayText;

	// Token: 0x04000E9B RID: 3739
	private ButtonHoverTransition disableButtonGraphic;
}
