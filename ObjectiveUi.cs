using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class ObjectiveUi : MonoBehaviour
{
	// Token: 0x060012DE RID: 4830 RVA: 0x0000EEDD File Offset: 0x0000D0DD
	private void Awake()
	{
		ObjectiveUi.instance = this;
		this.entries = new List<ObjectiveEntry>(8);
		this.canvas = base.GetComponent<Canvas>();
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x00091700 File Offset: 0x0008F900
	public static void SortEntries()
	{
		ObjectiveUi.instance.entries.Sort(delegate(ObjectiveEntry a, ObjectiveEntry b)
		{
			float value = -1000f;
			float num = -1000f;
			if (a.hasWorldTarget)
			{
				value = ObjectiveUi.GetSortValue(a.GetTargetWorldPosition());
			}
			if (b.hasWorldTarget)
			{
				num = ObjectiveUi.GetSortValue(b.GetTargetWorldPosition());
			}
			return num.CompareTo(value);
		});
		for (int i = 0; i < ObjectiveUi.instance.entries.Count; i++)
		{
			ObjectiveUi.instance.entries[i].SetSortIndex(i);
		}
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0009176C File Offset: 0x0008F96C
	private static float GetSortValue(Vector3 worldPosition)
	{
		Vector3 vector = MinimapCamera.WorldToNormalizedPosition(worldPosition);
		return vector.y + vector.x * 0.5f;
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x00091794 File Offset: 0x0008F994
	public static ObjectiveEntry CreateObjective(string text)
	{
		ObjectiveEntry component = UnityEngine.Object.Instantiate<GameObject>(ObjectiveUi.instance.entryPrefab, ObjectiveUi.instance.container).GetComponent<ObjectiveEntry>();
		component.SetText(text);
		ObjectiveUi.instance.entries.Add(component);
		return component;
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x000917D8 File Offset: 0x0008F9D8
	public static ObjectiveEntry CreateObjective(string text, Transform target)
	{
		ObjectiveEntry objectiveEntry = ObjectiveUi.CreateObjective(text);
		objectiveEntry.SetWorldTarget(target);
		ObjectiveUi.instance.entries.Add(objectiveEntry);
		return objectiveEntry;
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x00091804 File Offset: 0x0008FA04
	public static ObjectiveEntry CreateObjective(string text, Vector3 target)
	{
		ObjectiveEntry objectiveEntry = ObjectiveUi.CreateObjective(text);
		objectiveEntry.SetWorldTarget(target);
		ObjectiveUi.instance.entries.Add(objectiveEntry);
		return objectiveEntry;
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x00091830 File Offset: 0x0008FA30
	private void Update()
	{
		bool flag;
		bool openInput = ScoreboardUi.GetOpenInput(out flag);
		this.canvas.enabled = (openInput && ScoreboardUi.activeScoreboardType == ScoreboardUi.ActiveScoreboardUI.Objective);
	}

	// Token: 0x04001422 RID: 5154
	public static ObjectiveUi instance;

	// Token: 0x04001423 RID: 5155
	public GameObject entryPrefab;

	// Token: 0x04001424 RID: 5156
	public RectTransform container;

	// Token: 0x04001425 RID: 5157
	private List<ObjectiveEntry> entries;

	// Token: 0x04001426 RID: 5158
	private Canvas canvas;
}
