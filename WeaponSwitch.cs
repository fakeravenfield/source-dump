using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000211 RID: 529
public class WeaponSwitch : MonoBehaviour
{
	// Token: 0x06000DFD RID: 3581 RVA: 0x0000B42A File Offset: 0x0000962A
	private void Awake()
	{
		WeaponSwitch.instance = this;
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0007DE04 File Offset: 0x0007C004
	public void OnEnable()
	{
		this.weaponGroup = new List<WeaponSwitchGroup>();
		List<string> list;
		Dictionary<string, List<WeaponManager.WeaponEntry>> weaponTagDictionary = WeaponManager.GetWeaponTagDictionary(WeaponManager.WeaponSlot.Primary, new List<string>
		{
			"ASSAULT",
			"MARKSMAN",
			"CLOSE QUARTERS",
			"STEALTH",
			"HANDGUNS",
			"PDW",
			"GRENADES",
			"ANTI ARMOR",
			"EQUIPMENT"
		}, out list, true);
		this.togglesOfEntry = new Dictionary<WeaponManager.WeaponEntry, List<Toggle>>();
		foreach (WeaponManager.WeaponEntry key in WeaponManager.instance.allWeapons)
		{
			this.togglesOfEntry.Add(key, new List<Toggle>());
		}
		this.yPosition = 0f;
		foreach (string text in list)
		{
			this.AddWeaponSwitchGroup(text, weaponTagDictionary[text]);
		}
		this.panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, -this.yPosition);
		this.SetTeam(0);
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0007DF60 File Offset: 0x0007C160
	private void OnDisable()
	{
		foreach (WeaponSwitchGroup weaponSwitchGroup in this.weaponGroup)
		{
			UnityEngine.Object.Destroy(weaponSwitchGroup.gameObject);
		}
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x0007DFB8 File Offset: 0x0007C1B8
	private void AddWeaponSwitchGroup(string tag, List<WeaponManager.WeaponEntry> entries)
	{
		WeaponSwitchGroup component = UnityEngine.Object.Instantiate<GameObject>(this.weaponGroupPrefab).GetComponent<WeaponSwitchGroup>();
		this.weaponGroup.Add(component);
		component.SetTitle(tag);
		RectTransform rectTransform = (RectTransform)component.transform;
		rectTransform.SetParent(this.panel, false);
		rectTransform.anchoredPosition = new Vector2(0f, this.yPosition);
		Dictionary<WeaponManager.WeaponEntry, Toggle> dictionary = component.SetEntries(entries);
		this.yPosition -= component.height + 80f;
		foreach (WeaponManager.WeaponEntry key in entries)
		{
			if (dictionary.ContainsKey(key))
			{
				this.togglesOfEntry[key].Add(dictionary[key]);
			}
		}
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x0007E094 File Offset: 0x0007C294
	public void ToggleAll()
	{
		foreach (WeaponSwitchGroup weaponSwitchGroup in this.weaponGroup)
		{
			weaponSwitchGroup.SetAllToggles(this.toggle);
		}
		this.toggle = !this.toggle;
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0007E0FC File Offset: 0x0007C2FC
	public void OnEntryToggled(WeaponManager.WeaponEntry entry, bool isOn)
	{
		if (!this.consumeToggleEvent)
		{
			return;
		}
		this.consumeToggleEvent = false;
		foreach (Toggle toggle in this.togglesOfEntry[entry])
		{
			toggle.isOn = isOn;
			if (this.team != 0 && this.team != 1)
			{
				toggle.graphic.color = Color.white;
			}
		}
		if (this.team == -1 || this.team == 0)
		{
			if (isOn)
			{
				GameManager.instance.gameInfo.team[0].AddWeaponEntry(entry);
			}
			else
			{
				GameManager.instance.gameInfo.team[0].RemoveWeaponEntry(entry);
			}
		}
		if (this.team == -1 || this.team == 1)
		{
			if (isOn)
			{
				GameManager.instance.gameInfo.team[1].AddWeaponEntry(entry);
			}
			else
			{
				GameManager.instance.gameInfo.team[1].RemoveWeaponEntry(entry);
			}
		}
		this.consumeToggleEvent = true;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x0007E218 File Offset: 0x0007C418
	public void Apply()
	{
		List<WeaponManager.WeaponEntry> list = new List<WeaponManager.WeaponEntry>();
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (this.togglesOfEntry.ContainsKey(weaponEntry) && this.togglesOfEntry[weaponEntry].Count > 0 && this.togglesOfEntry[weaponEntry][0].isOn)
			{
				list.Add(weaponEntry);
			}
		}
		WeaponManager.instance.SetupAiWeaponEntries(list);
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0007E2BC File Offset: 0x0007C4BC
	public void SetTeam(int dropdownIndex)
	{
		this.team = dropdownIndex - 1;
		bool flag = this.team != 0 && this.team != 1;
		this.consumeToggleEvent = false;
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (this.togglesOfEntry.ContainsKey(weaponEntry))
			{
				Color color = Color.white;
				bool isOn;
				if (!flag)
				{
					isOn = GameManager.instance.gameInfo.team[this.team].IsWeaponEntryAvailable(weaponEntry);
					color = ColorScheme.TeamColorBrighter(this.team);
				}
				else
				{
					bool flag2 = GameManager.instance.gameInfo.team[0].IsWeaponEntryAvailable(weaponEntry);
					bool flag3 = GameManager.instance.gameInfo.team[1].IsWeaponEntryAvailable(weaponEntry);
					isOn = (flag2 || flag3);
					if (flag2 && !flag3)
					{
						color = ColorScheme.TeamColorBrighter(0);
					}
					else if (!flag2 && flag3)
					{
						color = ColorScheme.TeamColorBrighter(1);
					}
				}
				foreach (Toggle toggle in this.togglesOfEntry[weaponEntry])
				{
					toggle.isOn = isOn;
					toggle.graphic.color = color;
				}
			}
		}
		this.consumeToggleEvent = true;
	}

	// Token: 0x04000EF6 RID: 3830
	public static WeaponSwitch instance;

	// Token: 0x04000EF7 RID: 3831
	public GameObject weaponGroupPrefab;

	// Token: 0x04000EF8 RID: 3832
	public RectTransform panel;

	// Token: 0x04000EF9 RID: 3833
	private List<WeaponSwitchGroup> weaponGroup;

	// Token: 0x04000EFA RID: 3834
	private Dictionary<WeaponManager.WeaponEntry, List<Toggle>> togglesOfEntry;

	// Token: 0x04000EFB RID: 3835
	private float yPosition;

	// Token: 0x04000EFC RID: 3836
	private bool toggle;

	// Token: 0x04000EFD RID: 3837
	private int team = -1;

	// Token: 0x04000EFE RID: 3838
	private bool consumeToggleEvent = true;
}
