using System;
using UnityEngine;

// Token: 0x020002B5 RID: 693
public class KillIndicatorUi : MonoBehaviour
{
	// Token: 0x0600125F RID: 4703 RVA: 0x0008F3BC File Offset: 0x0008D5BC
	public static void ShowActorMessage(string prefix, Actor actor)
	{
		if (Options.GetToggle(OptionToggle.Id.KillIndicator))
		{
			KillIndicatorUi.ShowMessage(string.Concat(new string[]
			{
				prefix,
				" ",
				ColorScheme.RichTextColorTagOfTeam(actor.team, Color.white, 0.7f),
				actor.name,
				"</color>"
			}));
		}
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x0008F418 File Offset: 0x0008D618
	public static void ShowMessage(string message)
	{
		if (Options.GetToggle(OptionToggle.Id.KillIndicator))
		{
			KillIndicatorUi.instance.indicators[KillIndicatorUi.instance.index].ShowMessage(message);
			KillIndicatorUi.instance.indicators[KillIndicatorUi.instance.index].transform.SetAsFirstSibling();
			KillIndicatorUi.instance.index = (KillIndicatorUi.instance.index + 1) % KillIndicatorUi.instance.indicators.Length;
		}
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
	public static void PlayKillChime()
	{
		if (Options.GetToggle(OptionToggle.Id.KillIndicator))
		{
			KillIndicatorUi.instance.audioSource.Play();
		}
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x0008F48C File Offset: 0x0008D68C
	public static void Hide()
	{
		KillIndicator[] array = KillIndicatorUi.instance.indicators;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Hide();
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x0000E7ED File Offset: 0x0000C9ED
	private void Awake()
	{
		KillIndicatorUi.instance = this;
		this.indicators = base.GetComponentsInChildren<KillIndicator>();
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x04001397 RID: 5015
	public static KillIndicatorUi instance;

	// Token: 0x04001398 RID: 5016
	private AudioSource audioSource;

	// Token: 0x04001399 RID: 5017
	private KillIndicator[] indicators;

	// Token: 0x0400139A RID: 5018
	private int index;
}
