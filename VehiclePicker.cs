using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000204 RID: 516
public class VehiclePicker : PickerUI<VehiclePicker.VehicleEntry, VehiclePicker.VehicleEntryElement>
{
	// Token: 0x06000DBF RID: 3519 RVA: 0x0000B23A File Offset: 0x0000943A
	private void Awake()
	{
		this.showAllToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowAllToggled));
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0000B258 File Offset: 0x00009458
	private IEnumerator RenderPreviews()
	{
		while (this.scheduledForRender.Count > 0)
		{
			while (!base.gameObject.activeInHierarchy)
			{
				yield return 0;
			}
			VehiclePicker.VehicleEntry vehicleEntry = this.scheduledForRender.FirstOrDefault((VehiclePicker.VehicleEntry e) => this.entryPanels[e].activeInHierarchy);
			if (vehicleEntry == null)
			{
				vehicleEntry = this.scheduledForRender[0];
			}
			this.scheduledForRender.Remove(vehicleEntry);
			yield return this.RenderEntry(vehicleEntry);
		}
		yield break;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0000B267 File Offset: 0x00009467
	private IEnumerator RenderEntry(VehiclePicker.VehicleEntry entry)
	{
		GamePreview.RenderVehiclePreview(entry.prefab, -1, entry.texture);
		yield return 0;
		yield break;
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x0007D3F8 File Offset: 0x0007B5F8
	public override void Populate(IEnumerable<VehiclePicker.VehicleEntryElement> collection)
	{
		if (this.renderCoroutine != null)
		{
			GamePreview.instance.StopCoroutine(this.renderCoroutine);
		}
		this.scheduledForRender = new List<VehiclePicker.VehicleEntry>();
		base.Populate(collection);
		this.renderCoroutine = GamePreview.instance.StartCoroutine(this.RenderPreviews());
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0007D448 File Offset: 0x0007B648
	public override VehiclePicker.VehicleEntry RegisterEntry(VehiclePicker.VehicleEntryElement element, GameObject entryPanel)
	{
		VehiclePicker.VehicleEntry vehicleEntry = new VehiclePicker.VehicleEntry
		{
			prefab = element.prefab,
			type = element.type
		};
		if (vehicleEntry.prefab == null)
		{
			entryPanel.GetComponentInChildren<Text>().text = "NONE";
		}
		else
		{
			entryPanel.GetComponentInChildren<Text>().text = element.prefab.GetComponent<Vehicle>().name;
			vehicleEntry.texture = new Texture2D(512, 512, TextureFormat.ARGB32, false, false);
			entryPanel.GetComponentInChildren<RawImage>().texture = vehicleEntry.texture;
			this.scheduledForRender.Add(vehicleEntry);
		}
		return vehicleEntry;
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x0007D4E4 File Offset: 0x0007B6E4
	private void ShowAll()
	{
		HashSet<GameObject> visiblePrefabs = new HashSet<GameObject>();
		base.FilterVisible(delegate(VehiclePicker.VehicleEntry e)
		{
			if (e.type.isTurret != this.pickingType.isTurret || visiblePrefabs.Contains(e.prefab))
			{
				return false;
			}
			visiblePrefabs.Add(e.prefab);
			return true;
		});
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x0007D51C File Offset: 0x0007B71C
	private void ShowType(VehicleCompoundType type)
	{
		base.FilterVisible((VehiclePicker.VehicleEntry e) => e.type == type);
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0000B276 File Offset: 0x00009476
	public void Open(VehicleCompoundType type)
	{
		this.pickingType = type;
		this.UpdateVisibility();
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0000B285 File Offset: 0x00009485
	private void OnShowAllToggled(bool showAll)
	{
		this.UpdateVisibility();
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0000B28D File Offset: 0x0000948D
	private void UpdateVisibility()
	{
		if (this.showAllToggle.isOn)
		{
			this.ShowAll();
			return;
		}
		this.ShowType(this.pickingType);
	}

	// Token: 0x04000ED1 RID: 3793
	public VehicleCompoundType pickingType;

	// Token: 0x04000ED2 RID: 3794
	public Toggle showAllToggle;

	// Token: 0x04000ED3 RID: 3795
	private List<VehiclePicker.VehicleEntry> scheduledForRender;

	// Token: 0x04000ED4 RID: 3796
	private Coroutine renderCoroutine;

	// Token: 0x02000205 RID: 517
	public class VehicleEntry : IPickerEntry
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x0000296E File Offset: 0x00000B6E
		public void Dispose()
		{
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0000B2CA File Offset: 0x000094CA
		public void OnPick()
		{
			VehicleSwitch.OnPick(this.prefab);
		}

		// Token: 0x04000ED5 RID: 3797
		public GameObject prefab;

		// Token: 0x04000ED6 RID: 3798
		public VehicleCompoundType type;

		// Token: 0x04000ED7 RID: 3799
		public Texture2D texture;
	}

	// Token: 0x02000206 RID: 518
	public struct VehicleEntryElement
	{
		// Token: 0x04000ED8 RID: 3800
		public GameObject prefab;

		// Token: 0x04000ED9 RID: 3801
		public VehicleCompoundType type;
	}
}
