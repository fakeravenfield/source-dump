using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B6 RID: 694
public class LoadoutUi : MonoBehaviour
{
	// Token: 0x06001265 RID: 4709 RVA: 0x0000E80D File Offset: 0x0000CA0D
	private void Awake()
	{
		LoadoutUi.instance = this;
		this.uiCanvas = base.GetComponent<Canvas>();
		this.hasAcceptedLoadoutOnce = false;
		this.LoadInitialLoadout();
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x0008F4BC File Offset: 0x0008D6BC
	private void LoadInitialLoadout()
	{
		this.loadout = new WeaponManager.LoadoutSet();
		this.loadout.primary = this.LoadSlotEntry(WeaponManager.WeaponSlot.Primary, "primary");
		this.loadout.secondary = this.LoadSlotEntry(WeaponManager.WeaponSlot.Secondary, "secondary");
		this.loadout.gear1 = this.LoadSlotEntry(WeaponManager.WeaponSlot.Gear, "gear1");
		this.loadout.gear2 = this.LoadSlotEntry(WeaponManager.WeaponSlot.LargeGear, "gear2");
		if (this.loadout.gear2 != null && this.loadout.gear2.slot != WeaponManager.WeaponSlot.LargeGear)
		{
			this.loadout.gear3 = this.LoadSlotEntry(WeaponManager.WeaponSlot.Gear, "gear3");
		}
		else
		{
			this.loadout.gear3 = null;
		}
		this.primaryWeaponSelection.SetEntry(this.loadout.primary);
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x0008F58C File Offset: 0x0008D78C
	private WeaponManager.WeaponEntry LoadSlotEntry(WeaponManager.WeaponSlot entrySlot, string keyName)
	{
		if (!PlayerPrefs.HasKey(keyName))
		{
			foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
			{
				if ((GameManager.GameParameters().playerHasAllWeapons || GameManager.instance.gameInfo.team[GameManager.PlayerTeam()].IsWeaponEntryAvailable(weaponEntry)) && weaponEntry.slot == entrySlot)
				{
					return weaponEntry;
				}
			}
			return null;
		}
		int @int = PlayerPrefs.GetInt(keyName);
		if (@int == -1)
		{
			return null;
		}
		WeaponManager.WeaponEntry weaponEntry2 = null;
		foreach (WeaponManager.WeaponEntry weaponEntry3 in WeaponManager.instance.allWeapons)
		{
			if ((GameManager.GameParameters().playerHasAllWeapons || GameManager.instance.gameInfo.team[GameManager.PlayerTeam()].IsWeaponEntryAvailable(weaponEntry3)) && (weaponEntry3.slot == entrySlot || (entrySlot == WeaponManager.WeaponSlot.LargeGear && weaponEntry3.slot == WeaponManager.WeaponSlot.Gear)))
			{
				if (weaponEntry3.nameHash == @int)
				{
					return weaponEntry3;
				}
				if (weaponEntry2 == null)
				{
					weaponEntry2 = weaponEntry3;
				}
			}
		}
		return weaponEntry2;
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x0008F6CC File Offset: 0x0008D8CC
	private void Start()
	{
		this.tacticsButtonGraphics = this.tacticsButton.GetComponentsInChildren<Graphic>();
		this.deployButtonGraphics = this.deployButton.GetComponentsInChildren<Graphic>();
		this.tacticsButton.targetGraphic = null;
		this.deployButton.targetGraphic = null;
		LoadoutUi.Show(true);
		this.SetupGameUi();
		LoadoutUi.HideInitializationStage();
		this.hasBeenOpen = false;
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x0000E82E File Offset: 0x0000CA2E
	private void SetupGameUi()
	{
		this.UpdateLoadout();
		BattlePlanUi.instance.Setup();
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x0000E840 File Offset: 0x0000CA40
	private void Update()
	{
		if (LoadoutUi.IsOpen())
		{
			this.deployText.text = (FpsActorController.instance.actor.dead ? "DEPLOY" : "RESPAWN");
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x0008F72C File Offset: 0x0008D92C
	public void OpenWeaponSelector(int slot)
	{
		if (slot == 0)
		{
			this.primaryWeaponSelection.Show(this.loadout.primary);
		}
		else if (slot == 1)
		{
			this.secondaryWeaponSelection.Show(this.loadout.secondary);
		}
		else if (slot == 2)
		{
			this.gearWeaponSelection.Show(this.loadout.gear1);
		}
		else if (slot == 3)
		{
			this.gearWeaponSelection.Show(this.loadout.gear2);
		}
		else
		{
			this.gearWeaponSelection.Show(this.loadout.gear3);
		}
		this.targetSlot = slot;
		this.SetLoadoutVisible(false);
		this.SetMinimapVisible(false);
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x0000E871 File Offset: 0x0000CA71
	public void SetLoadoutVisible(bool visible)
	{
		if (this.overrideLoadout == null)
		{
			this.SetLoadoutVisibleStandard(visible);
			return;
		}
		this.overrideLoadout.SetActive(visible);
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0000E895 File Offset: 0x0000CA95
	private void SetLoadoutVisibleStandard(bool visible)
	{
		this.loadoutContainer.gameObject.SetActive(visible);
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
	public void SetMinimapVisible(bool visible)
	{
		if (this.overrideMinimap == null)
		{
			this.SetMinimapVisibleStandard(visible);
			return;
		}
		this.overrideMinimap.SetActive(visible);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x0008F7D4 File Offset: 0x0008D9D4
	private void SetMinimapVisibleStandard(bool visible)
	{
		Vector3 localScale = visible ? Vector3.one : Vector3.zero;
		this.minimapContainer.localScale = localScale;
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x0000E8CC File Offset: 0x0000CACC
	public void SetLoadoutOverride(GameObject loadout)
	{
		if (this.overrideLoadout != null)
		{
			this.overrideLoadout.gameObject.SetActive(false);
		}
		this.overrideLoadout = loadout;
		this.FitOverrideGO(loadout, this.loadoutParent);
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x0000E901 File Offset: 0x0000CB01
	public void SetMinimapOverride(GameObject minimap)
	{
		if (this.overrideMinimap != null)
		{
			this.overrideMinimap.gameObject.SetActive(false);
		}
		this.overrideMinimap = minimap;
		this.FitOverrideGO(minimap, this.minimapParent);
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x0008F800 File Offset: 0x0008DA00
	private void FitOverrideGO(GameObject go, RectTransform parent)
	{
		RectTransform rectTransform = go.transform as RectTransform;
		if (rectTransform != null)
		{
			rectTransform.SetParent(parent, false);
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
		}
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x0000E936 File Offset: 0x0000CB36
	public void CancelWeaponSelection()
	{
		this.primaryWeaponSelection.Hide();
		this.secondaryWeaponSelection.Hide();
		this.gearWeaponSelection.Hide();
		this.SetLoadoutVisible(true);
		this.SetMinimapVisible(true);
		this.targetSlot = -1;
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x0008F864 File Offset: 0x0008DA64
	public void WeaponSelected(WeaponManager.WeaponEntry entry)
	{
		this.primaryWeaponSelection.Hide();
		this.secondaryWeaponSelection.Hide();
		this.gearWeaponSelection.Hide();
		this.SetLoadoutVisible(true);
		this.SetMinimapVisible(true);
		if (this.targetSlot == -1)
		{
			return;
		}
		int slot = this.targetSlot;
		switch (this.targetSlot)
		{
		case 0:
			this.loadout.primary = entry;
			break;
		case 1:
			this.loadout.secondary = entry;
			break;
		case 2:
			if (this.IsLargeEntry(entry))
			{
				if (this.loadout.gear2 != null && !this.IsLargeEntry(this.loadout.gear2))
				{
					this.loadout.gear1 = this.loadout.gear2;
				}
				else if (this.loadout.gear3 != null && !this.IsLargeEntry(this.loadout.gear3))
				{
					this.loadout.gear1 = this.loadout.gear3;
				}
				else
				{
					this.loadout.gear1 = null;
				}
				slot = 3;
				this.loadout.gear2 = entry;
				this.loadout.gear3 = null;
			}
			else
			{
				this.loadout.gear1 = entry;
			}
			break;
		case 3:
			if (this.IsLargeEntry(entry))
			{
				this.loadout.gear3 = null;
				this.loadout.gear2 = entry;
			}
			else
			{
				this.loadout.gear2 = entry;
			}
			break;
		case 4:
			if (this.IsLargeEntry(entry))
			{
				this.loadout.gear3 = null;
				this.loadout.gear2 = entry;
				slot = 3;
			}
			else
			{
				this.loadout.gear3 = entry;
			}
			break;
		}
		this.SaveDefaultSlotEntry(slot, entry);
		this.UpdateLoadout();
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x0008FA1C File Offset: 0x0008DC1C
	private void UpdateLoadout()
	{
		this.UpdateLoadoutButton(this.primaryButton, this.loadout.primary);
		this.UpdateLoadoutButton(this.secondaryButton, this.loadout.secondary);
		this.UpdateLoadoutButton(this.gear1Button, this.loadout.gear1);
		if (this.IsLargeEntry(this.loadout.gear2))
		{
			this.largeGear2Button.gameObject.SetActive(true);
			this.gear2Button.gameObject.SetActive(false);
			this.gear3Button.gameObject.SetActive(false);
			this.UpdateLoadoutButton(this.largeGear2Button, this.loadout.gear2);
			return;
		}
		this.largeGear2Button.gameObject.SetActive(false);
		this.gear2Button.gameObject.SetActive(true);
		this.gear3Button.gameObject.SetActive(true);
		this.UpdateLoadoutButton(this.gear2Button, this.loadout.gear2);
		this.UpdateLoadoutButton(this.gear3Button, this.loadout.gear3);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x0008FB30 File Offset: 0x0008DD30
	private void UpdateLoadoutButton(RectTransform button, WeaponManager.WeaponEntry entry)
	{
		if (entry == null)
		{
			button.Find("Image").GetComponent<Image>().sprite = this.nothingSprite;
			button.GetComponentInChildren<Text>().text = "Nothing";
			return;
		}
		button.Find("Image").GetComponent<Image>().sprite = entry.prefab.GetComponent<Weapon>().uiSprite;
		button.GetComponentInChildren<Text>().text = entry.name;
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x0008FBA4 File Offset: 0x0008DDA4
	private void SaveDefaultSlotEntry(int slot, WeaponManager.WeaponEntry entry)
	{
		int value;
		if (entry == null)
		{
			value = -1;
		}
		else
		{
			value = entry.nameHash;
		}
		PlayerPrefs.SetInt(this.GetSlotName(slot), value);
		PlayerPrefs.Save();
		if (slot == 3 && this.IsLargeEntry(entry))
		{
			this.SaveDefaultSlotEntry(4, null);
		}
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x0008FBE8 File Offset: 0x0008DDE8
	public void SaveCurrentLoadout()
	{
		this.SaveDefaultSlotEntry(0, this.loadout.primary);
		this.SaveDefaultSlotEntry(1, this.loadout.secondary);
		this.SaveDefaultSlotEntry(4, this.loadout.gear3);
		this.SaveDefaultSlotEntry(3, this.loadout.gear2);
		this.SaveDefaultSlotEntry(2, this.loadout.gear1);
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x0000E96E File Offset: 0x0000CB6E
	private string GetSlotName(int slot)
	{
		if (slot == 0)
		{
			return "primary";
		}
		if (slot == 1)
		{
			return "secondary";
		}
		if (slot == 2)
		{
			return "gear1";
		}
		if (slot == 3)
		{
			return "gear2";
		}
		return "gear3";
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x0000E99C File Offset: 0x0000CB9C
	private bool IsLargeEntry(WeaponManager.WeaponEntry entry)
	{
		return entry != null && (entry.slot == WeaponManager.WeaponSlot.LargeGear || entry.slot == WeaponManager.WeaponSlot.Primary);
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x0000E9B7 File Offset: 0x0000CBB7
	private void ShowCanvas()
	{
		this.hasBeenOpen = true;
		if (this.uiCanvas.enabled)
		{
			return;
		}
		MinimapUi.PinToLoadoutScreen();
		this.uiCanvas.enabled = true;
		KillCamera.Hide();
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
	private void HideCanvas()
	{
		MinimapUi.PinToIngameScreen();
		this.uiCanvas.enabled = false;
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x0000E9F7 File Offset: 0x0000CBF7
	public void OnDeployClick()
	{
		if (!FpsActorController.instance.actor.dead)
		{
			FpsActorController.instance.actor.Kill(DamageInfo.Default);
			this.ShowDeploymentTab();
			return;
		}
		FpsActorController.instance.CloseLoadout();
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x0000EA2F File Offset: 0x0000CC2F
	public static bool IsOpen()
	{
		return LoadoutUi.instance.uiCanvas.enabled;
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x0008FC50 File Offset: 0x0008DE50
	public static void Show(bool tactics)
	{
		LoadoutUi.instance.primaryWeaponSelection.Hide();
		LoadoutUi.instance.secondaryWeaponSelection.Hide();
		LoadoutUi.instance.gearWeaponSelection.Hide();
		LoadoutUi.instance.SetLoadoutVisible(true);
		LoadoutUi.instance.SetMinimapVisible(true);
		LoadoutUi.instance.targetSlot = -1;
		if (StrategyUi.IsOpen())
		{
			StrategyUi.Hide();
		}
		LoadoutUi.instance.ShowCanvas();
		if (tactics)
		{
			LoadoutUi.instance.ShowTacticsTab();
			return;
		}
		LoadoutUi.instance.ShowDeploymentTab();
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x0008FCDC File Offset: 0x0008DEDC
	public static void Hide(bool isInitialSetup = false)
	{
		LoadoutUi.instance.primaryWeaponSelection.Hide();
		LoadoutUi.instance.secondaryWeaponSelection.Hide();
		LoadoutUi.instance.gearWeaponSelection.Hide();
		LoadoutUi.instance.targetSlot = -1;
		LoadoutUi.instance.HideCanvas();
		if (!isInitialSetup && !LoadoutUi.instance.hasAcceptedLoadoutOnce && !GameManager.IsSpectating())
		{
			LoadoutUi.instance.hasAcceptedLoadoutOnce = true;
			GameModeBase.instance.PlayerAcceptedLoadoutFirstTime();
		}
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x0000EA40 File Offset: 0x0000CC40
	public static void HideInitializationStage()
	{
		LoadoutUi.Hide(true);
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x0000EA48 File Offset: 0x0000CC48
	public static bool HasBeenOpen()
	{
		return LoadoutUi.instance.hasBeenOpen;
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x0000EA54 File Offset: 0x0000CC54
	public static bool HasBeenClosed()
	{
		return !LoadoutUi.IsOpen() && LoadoutUi.HasBeenOpen();
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x0008FD58 File Offset: 0x0008DF58
	public void ShowDeploymentTab()
	{
		BattlePlanUi.instance.tacticsPanel.gameObject.SetActive(false);
		Graphic[] array = this.tacticsButtonGraphics;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CrossFadeAlpha(0.3f, 0.2f, true);
		}
		array = this.deployButtonGraphics;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CrossFadeAlpha(1f, 0.2f, true);
		}
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x0008FDCC File Offset: 0x0008DFCC
	public void ShowTacticsTab()
	{
		BattlePlanUi.instance.tacticsPanel.gameObject.SetActive(true);
		Graphic[] array = this.tacticsButtonGraphics;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CrossFadeAlpha(1f, 0.2f, true);
		}
		array = this.deployButtonGraphics;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CrossFadeAlpha(0.3f, 0.2f, true);
		}
	}

	// Token: 0x0400139B RID: 5019
	private const int SLOT_PRIMARY = 0;

	// Token: 0x0400139C RID: 5020
	private const int SLOT_SECONDARY = 1;

	// Token: 0x0400139D RID: 5021
	private const int SLOT_GEAR_1 = 2;

	// Token: 0x0400139E RID: 5022
	private const int SLOT_GEAR_2 = 3;

	// Token: 0x0400139F RID: 5023
	private const int SLOT_GEAR_3 = 4;

	// Token: 0x040013A0 RID: 5024
	private const float LOADOUT_ITEM_PADDING = 10f;

	// Token: 0x040013A1 RID: 5025
	public static LoadoutUi instance;

	// Token: 0x040013A2 RID: 5026
	public RectTransform minimapParent;

	// Token: 0x040013A3 RID: 5027
	public RectTransform loadoutParent;

	// Token: 0x040013A4 RID: 5028
	public RectTransform minimapContainer;

	// Token: 0x040013A5 RID: 5029
	public RectTransform loadoutContainer;

	// Token: 0x040013A6 RID: 5030
	public RectTransform primaryButton;

	// Token: 0x040013A7 RID: 5031
	public RectTransform secondaryButton;

	// Token: 0x040013A8 RID: 5032
	public RectTransform gear1Button;

	// Token: 0x040013A9 RID: 5033
	public RectTransform gear2Button;

	// Token: 0x040013AA RID: 5034
	public RectTransform gear3Button;

	// Token: 0x040013AB RID: 5035
	public RectTransform largeGear2Button;

	// Token: 0x040013AC RID: 5036
	public Text deployText;

	// Token: 0x040013AD RID: 5037
	public Button tacticsButton;

	// Token: 0x040013AE RID: 5038
	public Button deployButton;

	// Token: 0x040013AF RID: 5039
	public WeaponSelectionUi primaryWeaponSelection;

	// Token: 0x040013B0 RID: 5040
	public WeaponSelectionUi secondaryWeaponSelection;

	// Token: 0x040013B1 RID: 5041
	public WeaponSelectionUi gearWeaponSelection;

	// Token: 0x040013B2 RID: 5042
	public Sprite nothingSprite;

	// Token: 0x040013B3 RID: 5043
	private Graphic[] tacticsButtonGraphics;

	// Token: 0x040013B4 RID: 5044
	private Graphic[] deployButtonGraphics;

	// Token: 0x040013B5 RID: 5045
	private bool hasBeenOpen;

	// Token: 0x040013B6 RID: 5046
	private bool hasAcceptedLoadoutOnce;

	// Token: 0x040013B7 RID: 5047
	[NonSerialized]
	public GameObject overrideLoadout;

	// Token: 0x040013B8 RID: 5048
	[NonSerialized]
	public GameObject overrideMinimap;

	// Token: 0x040013B9 RID: 5049
	[NonSerialized]
	public WeaponManager.LoadoutSet loadout;

	// Token: 0x040013BA RID: 5050
	private float loadoutButtonHeight;

	// Token: 0x040013BB RID: 5051
	private int targetSlot = -1;

	// Token: 0x040013BC RID: 5052
	private Canvas uiCanvas;
}
