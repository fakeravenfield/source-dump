using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public static class ColorScheme
{
	// Token: 0x060013EB RID: 5099 RVA: 0x0000FDCC File Offset: 0x0000DFCC
	public static Material GetActorMaterial(int team)
	{
		if (team < 0)
		{
			return null;
		}
		return ActorManager.instance.teamActorMaterial[team];
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
	public static Color TeamColor(int team)
	{
		if (team >= 0 && team < ColorScheme.teamColors.Length)
		{
			return ColorScheme.teamColors[team];
		}
		return ColorScheme.defaultColor;
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x0000FE01 File Offset: 0x0000E001
	public static Color TeamColorBrighter(int team)
	{
		return Color.Lerp(ColorScheme.TeamColor(team), Color.white, 0.3f);
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x0000FE18 File Offset: 0x0000E018
	public static string RichTextColorTagOfTeam(int team)
	{
		return ColorScheme.RichTextColorTagOfTeam(team, Color.white, 0f);
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0000FE2A File Offset: 0x0000E02A
	public static string RichTextColorTagOfTeam(int team, Color blendTarget, float blendAmount)
	{
		return ColorScheme.RichTextColorTag(Color.Lerp(ColorScheme.TeamColor(team), blendTarget, blendAmount));
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x00094F70 File Offset: 0x00093170
	public static string RichTextColorTag(Color color)
	{
		return "<color=#" + Mathf.FloorToInt(color.r * 255f).ToString("X2") + Mathf.FloorToInt(color.g * 255f).ToString("X2") + Mathf.FloorToInt(color.b * 255f).ToString("X2") + ">";
	}

	// Token: 0x04001573 RID: 5491
	public static Color defaultColor = new Color(0.75f, 0.75f, 0.75f, 1f);

	// Token: 0x04001574 RID: 5492
	public static Color[] teamColors = new Color[]
	{
		Color.blue,
		Color.red
	};
}
