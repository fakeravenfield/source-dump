using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class MutatorBrowser : MonoBehaviour
{
	// Token: 0x06000D85 RID: 3461 RVA: 0x0000AF30 File Offset: 0x00009130
	private void Awake()
	{
		MutatorBrowser.instance = this;
		this.SetupEntries();
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0007CC4C File Offset: 0x0007AE4C
	public void SetupEntries()
	{
		this.ClearEntries();
		foreach (MutatorEntry mutator in ModManager.instance.loadedMutators)
		{
			this.AddEntry(mutator);
		}
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0007CCAC File Offset: 0x0007AEAC
	private void ClearEntries()
	{
		this.column = 0;
		this.row = 0;
		float num = 0.7f * (float)Screen.height;
		this.rows = Mathf.FloorToInt(num / 220f);
		this.entryHeight = 1f / (float)this.rows;
		int childCount = this.contentPanel.childCount;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < childCount; i++)
		{
			list.Add(this.contentPanel.GetChild(i));
		}
		foreach (Transform transform in list)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0000AF3E File Offset: 0x0000913E
	public void OpenMutatorConfig(MutatorEntry mutator)
	{
		MainMenu.instance.OpenPageIndex(14);
		MutatorConfigurationMenu.instance.SetMutator(mutator);
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0007CD70 File Offset: 0x0007AF70
	private void AddEntry(MutatorEntry mutator)
	{
		MutatorEntryPanel component = UnityEngine.Object.Instantiate<GameObject>(this.uiEntryPrefab, this.contentPanel).GetComponent<MutatorEntryPanel>();
		RectTransform rectTransform = (RectTransform)component.transform;
		component.SetMutator(mutator);
		Vector2 vector = new Vector2(0f, ((float)this.row + 0.5f) * this.entryHeight);
		rectTransform.anchorMin = vector;
		rectTransform.anchorMax = vector;
		rectTransform.anchoredPosition = new Vector2(((float)this.column + 0.5f) * 360f, 0f);
		this.contentPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)(this.column + 1) * 360f);
		this.row++;
		if (this.row >= this.rows)
		{
			this.row = 0;
			this.column++;
		}
	}

	// Token: 0x04000E9F RID: 3743
	public static MutatorBrowser instance;

	// Token: 0x04000EA0 RID: 3744
	private const float SPACING_X = 360f;

	// Token: 0x04000EA1 RID: 3745
	private const float MIN_SPACING_Y = 220f;

	// Token: 0x04000EA2 RID: 3746
	public GameObject uiEntryPrefab;

	// Token: 0x04000EA3 RID: 3747
	public RectTransform contentPanel;

	// Token: 0x04000EA4 RID: 3748
	private int column;

	// Token: 0x04000EA5 RID: 3749
	private int row;

	// Token: 0x04000EA6 RID: 3750
	private int rows = 3;

	// Token: 0x04000EA7 RID: 3751
	private float entryWidth;

	// Token: 0x04000EA8 RID: 3752
	private float entryHeight;

	// Token: 0x04000EA9 RID: 3753
	private Queue<MutatorEntry> dataLoadQueue;
}
