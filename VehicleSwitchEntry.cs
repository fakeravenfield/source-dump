using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200020D RID: 525
public class VehicleSwitchEntry : MonoBehaviour
{
	// Token: 0x06000DEA RID: 3562 RVA: 0x0000B368 File Offset: 0x00009568
	private void Awake()
	{
		base.GetComponentInChildren<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0000B386 File Offset: 0x00009586
	public void OnClick()
	{
		VehicleSwitch.OpenPicker(this.type, this.team);
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0000B399 File Offset: 0x00009599
	public void SetPrefab(GameObject prefab)
	{
		this.currentPrefab = prefab;
		this.UpdateLabel();
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x0007DA64 File Offset: 0x0007BC64
	private void UpdateLabel()
	{
		if (this.currentPrefab != null)
		{
			try
			{
				this.nameLabel.text = string.Format("{0} - {1}", this.currentPrefab.GetComponent<Vehicle>().name, ModManager.GetVehiclePrefabSourceMod(this.currentPrefab).title);
				return;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.nameLabel.text = "Prefab Error";
				return;
			}
		}
		this.nameLabel.text = "<color=grey>NONE</color>";
	}

	// Token: 0x04000EEB RID: 3819
	public VehicleCompoundType type;

	// Token: 0x04000EEC RID: 3820
	public Text nameLabel;

	// Token: 0x04000EED RID: 3821
	[NonSerialized]
	public GameObject currentPrefab;

	// Token: 0x04000EEE RID: 3822
	[NonSerialized]
	public int team;
}
