using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000965 RID: 2405
	[Name("ColorScheme")]
	[Doc("Use these methods to get the game's color scheme.")]
	public static class WColorScheme
	{
		// Token: 0x06003D68 RID: 15720 RVA: 0x0002986B File Offset: 0x00027A6B
		[Doc("Returns the main color of a specific team.[..]")]
		public static Color GetTeamColor(WTeam team)
		{
			return ColorScheme.TeamColor((int)team);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x00029873 File Offset: 0x00027A73
		[Doc("Returns a brighter color of a specific team.[..] Useful for UI elements that need to be bright such as text and icons.")]
		public static Color GetTeamColorBrighter(WTeam team)
		{
			return ColorScheme.TeamColorBrighter((int)team);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0002987B File Offset: 0x00027A7B
		[Doc("Returns a rich text color tag such as ``<color=blue>`` for a specified color.[..]")]
		public static string RichTextColorTag(Color color)
		{
			return ColorScheme.RichTextColorTag(color);
		}
	}
}
