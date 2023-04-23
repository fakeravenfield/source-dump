using System;
using System.Collections.Generic;
using System.Linq;
using Ravenfield.Mutator.Configuration;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F9 RID: 505
public class MutatorConfigurationMenu : MonoBehaviour
{
	// Token: 0x06000D8B RID: 3467 RVA: 0x0000AF66 File Offset: 0x00009166
	private void Awake()
	{
		MutatorConfigurationMenu.instance = this;
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0000AF6E File Offset: 0x0000916E
	public void SetMutator(MutatorEntry mutator)
	{
		this.mutator = mutator;
		this.title.text = string.Format("CONFIGURING {0}", mutator.name);
		this.PopulateContainer();
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0000AF98 File Offset: 0x00009198
	public void OnDisable()
	{
		this.CleanupContainer();
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0007CE44 File Offset: 0x0007B044
	private void PopulateContainer()
	{
		List<MutatorConfigurationSortableField> list = this.mutator.configuration.GetAllFields().ToList<MutatorConfigurationSortableField>();
		list.Sort((MutatorConfigurationSortableField a, MutatorConfigurationSortableField b) => a.orderPriority.CompareTo(b.orderPriority));
		foreach (MutatorConfigurationSortableField field in list)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.entryPrefab, this.container).GetComponent<MutatorConfigField>().SetField(field);
		}
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0007CEE0 File Offset: 0x0007B0E0
	private void CleanupContainer()
	{
		for (int i = this.container.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.container.GetChild(i).gameObject);
		}
	}

	// Token: 0x04000EAA RID: 3754
	public static MutatorConfigurationMenu instance;

	// Token: 0x04000EAB RID: 3755
	public Text title;

	// Token: 0x04000EAC RID: 3756
	public RectTransform container;

	// Token: 0x04000EAD RID: 3757
	public GameObject entryPrefab;

	// Token: 0x04000EAE RID: 3758
	private MutatorEntry mutator;
}
