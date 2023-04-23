using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000257 RID: 599
public class RoadmapMapText : MonoBehaviour
{
	// Token: 0x0600107B RID: 4219 RVA: 0x0008970C File Offset: 0x0008790C
	private void OnEnable()
	{
		Text component = base.GetComponent<Text>();
		string text = "";
		int num = UnityEngine.Random.Range(1, 7);
		int num2 = UnityEngine.Random.Range(1, 7);
		for (int i = 0; i < num; i++)
		{
			text += "?";
		}
		for (int j = 0; j < num2; j++)
		{
			text += "!";
		}
		component.text = text;
	}
}
