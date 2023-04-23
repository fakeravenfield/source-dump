using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class ModPage : MonoBehaviour
{
	// Token: 0x06000D66 RID: 3430 RVA: 0x0000AD1E File Offset: 0x00008F1E
	private void Start()
	{
		this.reloadModContentButton.SetActive(false);
		this.ReloadPanels();
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0000AD32 File Offset: 0x00008F32
	private void OnEnable()
	{
		base.StartCoroutine(this.ReapplyDisabledVisuals());
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0000AD41 File Offset: 0x00008F41
	public void OpenWorkshopInSteam()
	{
		if (GameManager.IsConnectedToSteam())
		{
			GameManager.instance.steamworks.OpenUrl("steam://url/SteamWorkshopPage/636480", true);
			return;
		}
		Application.OpenURL("steam://url/SteamWorkshopPage/636480");
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0000AD6A File Offset: 0x00008F6A
	private IEnumerator ReapplyDisabledVisuals()
	{
		yield return new WaitForEndOfFrame();
		using (List<ModInformation>.Enumerator enumerator = ModManager.instance.mods.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ModInformation modInformation = enumerator.Current;
				modInformation.ReapplyPanelState();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0007C9AC File Offset: 0x0007ABAC
	public void ReloadPanels()
	{
		for (int i = 0; i < this.panelContainer.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.panelContainer.GetChild(i).gameObject);
		}
		this.containerHeight = 0f;
		this.modOfPanel = new Dictionary<ModPanel, ModInformation>();
		foreach (ModInformation mod in ModManager.instance.mods)
		{
			this.AddPanelForMod(mod);
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0007CA48 File Offset: 0x0007AC48
	public void AddPanelForMod(ModInformation mod)
	{
		if (mod.hideInModList)
		{
			return;
		}
		ModPanel component = UnityEngine.Object.Instantiate<GameObject>(this.modPanelPrefab, this.panelContainer).GetComponent<ModPanel>();
		((RectTransform)component.transform).anchoredPosition = new Vector2(0f, -this.containerHeight);
		mod.SetupPanel(component);
		this.modOfPanel.Add(component, mod);
		this.containerHeight += 180f;
		((RectTransform)this.panelContainer).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.containerHeight);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0000AD72 File Offset: 0x00008F72
	public void UserWantsToToggle(ModPanel panel)
	{
		ModInformation modInformation = this.modOfPanel[panel];
		modInformation.ToggleEnabled();
		if (modInformation.content.HasGameContent())
		{
			this.reloadModContentButton.SetActive(true);
		}
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0000AD9E File Offset: 0x00008F9E
	public void ReloadModContentButtonPressed()
	{
		ModManager.instance.ReloadModContent();
		this.reloadModContentButton.SetActive(false);
	}

	// Token: 0x04000E87 RID: 3719
	private const string WORKSHOP_URL = "steam://url/SteamWorkshopPage/636480";

	// Token: 0x04000E88 RID: 3720
	private const float PANEL_SPACING = 180f;

	// Token: 0x04000E89 RID: 3721
	public GameObject modPanelPrefab;

	// Token: 0x04000E8A RID: 3722
	public Transform panelContainer;

	// Token: 0x04000E8B RID: 3723
	public GameObject reloadModContentButton;

	// Token: 0x04000E8C RID: 3724
	private Dictionary<ModPanel, ModInformation> modOfPanel;

	// Token: 0x04000E8D RID: 3725
	private float containerHeight;
}
