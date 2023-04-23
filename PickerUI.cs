using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FD RID: 509
public abstract class PickerUI<T, U> : MonoBehaviour where T : IPickerEntry
{
	// Token: 0x06000D9C RID: 3484 RVA: 0x0000B007 File Offset: 0x00009207
	public void Clear()
	{
		this.DisposeEntries();
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
	public void DisposeEntries()
	{
		foreach (T key in this.entries)
		{
			key.Dispose();
			UnityEngine.Object.Destroy(this.entryPanels[key]);
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0000B007 File Offset: 0x00009207
	public void OnDestroy()
	{
		this.DisposeEntries();
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0007D040 File Offset: 0x0007B240
	public virtual void Populate(IEnumerable<U> collection)
	{
		this.entries = new List<T>();
		this.entryPanels = new Dictionary<T, GameObject>();
		foreach (U element in collection)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.entryPrefab, this.container);
			T entry = this.RegisterEntry(element, gameObject);
			this.entries.Add(entry);
			this.entryPanels.Add(entry, gameObject);
			gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate()
			{
				entry.OnPick();
			});
		}
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0007D0FC File Offset: 0x0007B2FC
	public void FilterVisible(PickerUI<T, U>.DelEntryFilter filter)
	{
		foreach (T t in this.entries)
		{
			this.entryPanels[t].SetActive(filter(t));
		}
	}

	// Token: 0x06000DA1 RID: 3489
	public abstract T RegisterEntry(U element, GameObject entryInstance);

	// Token: 0x04000EB7 RID: 3767
	public GameObject entryPrefab;

	// Token: 0x04000EB8 RID: 3768
	public RectTransform container;

	// Token: 0x04000EB9 RID: 3769
	protected Dictionary<T, GameObject> entryPanels = new Dictionary<T, GameObject>();

	// Token: 0x04000EBA RID: 3770
	public List<T> entries = new List<T>();

	// Token: 0x020001FE RID: 510
	// (Invoke) Token: 0x06000DA4 RID: 3492
	public delegate bool DelEntryFilter(T entry);
}
