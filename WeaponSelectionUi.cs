using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E7 RID: 743
public class WeaponSelectionUi : MonoBehaviour
{
	// Token: 0x060013AA RID: 5034 RVA: 0x0000FB65 File Offset: 0x0000DD65
	public void SetEntry(WeaponManager.WeaponEntry entry)
	{
		this.currentEntry = entry;
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x0000FB6E File Offset: 0x0000DD6E
	private void Awake()
	{
		this.Hide();
		this.SetupTagGroups();
		this.OpenTag(0);
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x00093A80 File Offset: 0x00091C80
	private void SetupTagGroups()
	{
		List<string> list;
		Dictionary<string, List<WeaponManager.WeaponEntry>> weaponsTagged = WeaponManager.GetWeaponTagDictionary(this.slot, this.defaultTags, out list, false);
		this.selectionHighlighter = new Dictionary<int, RawImage>(list.Count);
		this.tagText = new Dictionary<int, Text>(list.Count);
		this.tagButtonPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20f + 340f * (float)list.Count);
		this.tagGroupContainer = new List<GameObject>();
		int num = Mathf.Max(1, Mathf.FloorToInt((float)Screen.width / 410f));
		float num2 = (float)(Screen.width / num);
		this.tagIndexOfEntry = new Dictionary<WeaponManager.WeaponEntry, int>();
		bool flag = false;
		foreach (string key in list)
		{
			weaponsTagged[key].RemoveAll((WeaponManager.WeaponEntry entry) => GameManager.PlayerTeam() != -1 && !GameManager.GameParameters().playerHasAllWeapons && !GameManager.instance.gameInfo.team[GameManager.PlayerTeam()].IsWeaponEntryAvailable(entry));
			if (weaponsTagged[key].Count > 0)
			{
				weaponsTagged[key].Insert(0, null);
				flag = true;
			}
		}
		if (!flag)
		{
			weaponsTagged[list[0]].Add(null);
		}
		list.RemoveAll((string tag) => weaponsTagged[tag].Count == 0);
		for (int i = 0; i < list.Count; i++)
		{
			Button component = UnityEngine.Object.Instantiate<GameObject>(this.tagGroupButtonPrefab).GetComponent<Button>();
			RectTransform rectTransform = (RectTransform)component.transform;
			rectTransform.SetParent(this.tagButtonPanel, false);
			rectTransform.anchoredPosition = new Vector2(20f + 340f * (float)i, 0f);
			int index = i;
			component.onClick.AddListener(delegate()
			{
				this.OpenTag(index);
			});
			component.GetComponentInChildren<Text>().text = list[i];
			this.selectionHighlighter.Add(i, component.GetComponentInChildren<RawImage>());
			this.tagText.Add(i, component.GetComponentInChildren<Text>());
			this.tagText[i].color = Color.gray;
			RectTransform rectTransform2 = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.tagGroupContainerPrefab).transform;
			rectTransform2.SetParent(this.weaponEntryPanel, false);
			this.tagGroupContainer.Add(rectTransform2.gameObject);
			RectTransform rectTransform3 = (RectTransform)rectTransform2.GetChild(0).GetChild(0);
			if (weaponsTagged.ContainsKey(list[i]))
			{
				int num3 = 0;
				int num4 = 0;
				float num5 = 0f;
				ModInformation modInformation = ModInformation.OfficialContent;
				using (List<WeaponManager.WeaponEntry>.Enumerator enumerator2 = weaponsTagged[list[i]].GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						WeaponManager.WeaponEntry weaponEntry = enumerator2.Current;
						if (weaponEntry != null && weaponEntry.sourceMod != modInformation)
						{
							modInformation = weaponEntry.sourceMod;
							if (num3 > 0)
							{
								num3 = 0;
								num4++;
							}
							RectTransform rectTransform4 = UnityEngine.Object.Instantiate<GameObject>(this.weaponEntryGroupPrefab).transform as RectTransform;
							rectTransform4.SetParent(rectTransform3, false);
							rectTransform4.anchoredPosition = new Vector2(((float)num3 + 0.5f) * num2, -20f - (float)num4 * 190f - num5);
							rectTransform4.GetComponentInChildren<Text>().text = modInformation.title;
							num5 += 40f;
						}
						Button component2 = UnityEngine.Object.Instantiate<GameObject>((weaponEntry == null || weaponEntry.slot == WeaponManager.WeaponSlot.Primary || weaponEntry.slot == WeaponManager.WeaponSlot.LargeGear) ? this.weaponEntryPrefab : this.weaponEntrySmallPrefab).GetComponent<Button>();
						component2.onClick.AddListener(delegate()
						{
							LoadoutUi.instance.WeaponSelected(weaponEntry);
						});
						RectTransform rectTransform5 = (RectTransform)component2.transform;
						rectTransform5.SetParent(rectTransform3, false);
						if (weaponEntry != null)
						{
							rectTransform5.GetComponentInChildren<Image>().sprite = weaponEntry.uiSprite;
							rectTransform5.GetComponentInChildren<Text>().text = weaponEntry.name;
						}
						else
						{
							rectTransform5.GetComponentInChildren<Image>().sprite = LoadoutUi.instance.nothingSprite;
							rectTransform5.GetComponentInChildren<Text>().text = "NOTHING";
						}
						rectTransform5.anchoredPosition = new Vector2(((float)num3 + 0.5f) * num2, -20f - (float)num4 * 190f - num5);
						if (weaponEntry != null && !this.tagIndexOfEntry.ContainsKey(weaponEntry))
						{
							this.tagIndexOfEntry.Add(weaponEntry, i);
						}
						num3++;
						if (num3 >= num)
						{
							num3 = 0;
							num4++;
						}
					}
				}
				if (num3 > 0)
				{
					num4++;
				}
				rectTransform3.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 60f + (float)num4 * 190f + num5);
			}
			rectTransform2.gameObject.SetActive(false);
		}
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x00093FE4 File Offset: 0x000921E4
	public void OpenTag(int tagIndex)
	{
		this.selectionHighlighter[this.selectedTag].enabled = false;
		this.tagText[this.selectedTag].color = Color.gray;
		this.tagGroupContainer[this.selectedTag].SetActive(false);
		this.selectedTag = tagIndex;
		this.selectionHighlighter[this.selectedTag].enabled = true;
		this.tagText[this.selectedTag].color = Color.white;
		this.tagGroupContainer[this.selectedTag].SetActive(true);
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x0000FB83 File Offset: 0x0000DD83
	public void Show(WeaponManager.WeaponEntry displayEntry)
	{
		base.gameObject.SetActive(true);
		if (displayEntry != null && this.tagIndexOfEntry.ContainsKey(displayEntry))
		{
			this.OpenTag(this.tagIndexOfEntry[displayEntry]);
			return;
		}
		this.OpenTag(0);
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x0000969C File Offset: 0x0000789C
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x0000FBBC File Offset: 0x0000DDBC
	public void BackButtonPressed()
	{
		LoadoutUi.instance.CancelWeaponSelection();
	}

	// Token: 0x0400151D RID: 5405
	private const float WEAPON_ENTRY_MIN_X_SPACING = 410f;

	// Token: 0x0400151E RID: 5406
	private const float WEAPON_ENTRY_Y_SPACING = 190f;

	// Token: 0x0400151F RID: 5407
	private const float WEAPON_ENTRY_Y_PADDING = 20f;

	// Token: 0x04001520 RID: 5408
	private const float WEAPON_SOURCE_MOD_TITLE_Y_PADDING = 40f;

	// Token: 0x04001521 RID: 5409
	private WeaponManager.WeaponEntry currentEntry;

	// Token: 0x04001522 RID: 5410
	public List<string> defaultTags;

	// Token: 0x04001523 RID: 5411
	public RectTransform tagButtonPanel;

	// Token: 0x04001524 RID: 5412
	public RectTransform weaponEntryPanel;

	// Token: 0x04001525 RID: 5413
	public GameObject tagGroupButtonPrefab;

	// Token: 0x04001526 RID: 5414
	public GameObject tagGroupContainerPrefab;

	// Token: 0x04001527 RID: 5415
	public GameObject weaponEntryPrefab;

	// Token: 0x04001528 RID: 5416
	public GameObject weaponEntrySmallPrefab;

	// Token: 0x04001529 RID: 5417
	public GameObject weaponEntryGroupPrefab;

	// Token: 0x0400152A RID: 5418
	public WeaponManager.WeaponSlot slot;

	// Token: 0x0400152B RID: 5419
	private int selectedTag;

	// Token: 0x0400152C RID: 5420
	private Dictionary<int, RawImage> selectionHighlighter;

	// Token: 0x0400152D RID: 5421
	private Dictionary<int, Text> tagText;

	// Token: 0x0400152E RID: 5422
	private List<GameObject> tagGroupContainer;

	// Token: 0x0400152F RID: 5423
	private Dictionary<WeaponManager.WeaponEntry, int> tagIndexOfEntry;
}
