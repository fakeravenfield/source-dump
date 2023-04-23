using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EC RID: 748
public class WeaponSwitchGroup : MonoBehaviour
{
	// Token: 0x060013BB RID: 5051 RVA: 0x0000FC4A File Offset: 0x0000DE4A
	private void Awake()
	{
		this.toggle = base.GetComponentInChildren<Toggle>();
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x0000A756 File Offset: 0x00008956
	public void SetTitle(string title)
	{
		base.GetComponentInChildren<Text>().text = title;
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x0009408C File Offset: 0x0009228C
	public Dictionary<WeaponManager.WeaponEntry, Toggle> SetEntries(List<WeaponManager.WeaponEntry> entries)
	{
		int num = Mathf.FloorToInt((float)Screen.width / 250f);
		float num2 = 1f / (float)num;
		int num3 = 0;
		this.height = 0f;
		this.weaponToggles = new Dictionary<WeaponManager.WeaponEntry, Toggle>();
		foreach (WeaponManager.WeaponEntry weaponEntry in entries)
		{
			RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.entryPrefab).GetComponent<RectTransform>();
			component.SetParent(this.container, false);
			component.anchorMin = new Vector2((float)num3 * num2, 1f);
			component.anchorMax = new Vector2((float)(num3 + 1) * num2, 1f);
			component.anchoredPosition = new Vector2(5f, -this.height - 5f);
			component.GetComponentInChildren<Text>().text = weaponEntry.name;
			Toggle componentInChildren = component.GetComponentInChildren<Toggle>();
			componentInChildren.GetComponentInChildren<Text>().color = (weaponEntry.usableByAi ? Color.white : Color.gray);
			WeaponManager.WeaponEntry anonEntry = weaponEntry;
			componentInChildren.onValueChanged.AddListener(delegate(bool arg0)
			{
				WeaponSwitch.instance.OnEntryToggled(anonEntry, arg0);
			});
			this.weaponToggles.Add(weaponEntry, component.GetComponentInChildren<Toggle>());
			num3++;
			if (num3 >= num)
			{
				num3 = 0;
				this.height += 30f;
			}
		}
		if (num3 > 0)
		{
			this.height += 30f;
		}
		this.container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.height);
		return this.weaponToggles;
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x00094244 File Offset: 0x00092444
	public void OnToggleGroup()
	{
		foreach (Toggle toggle in this.weaponToggles.Values)
		{
			toggle.isOn = this.toggle.isOn;
		}
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0000FC58 File Offset: 0x0000DE58
	public void SetAllToggles(bool on)
	{
		this.toggle.isOn = on;
	}

	// Token: 0x04001537 RID: 5431
	private const float WIDTH_PER_ITEM = 250f;

	// Token: 0x04001538 RID: 5432
	private const float HEIGHT_PER_ITEM = 30f;

	// Token: 0x04001539 RID: 5433
	public RectTransform container;

	// Token: 0x0400153A RID: 5434
	public GameObject entryPrefab;

	// Token: 0x0400153B RID: 5435
	private Toggle toggle;

	// Token: 0x0400153C RID: 5436
	private Dictionary<WeaponManager.WeaponEntry, Toggle> weaponToggles;

	// Token: 0x0400153D RID: 5437
	[NonSerialized]
	public float height;
}
