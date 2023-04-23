using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A17 RID: 2583
	[Proxy(typeof(WColorScheme))]
	public class WColorSchemeProxy : IProxy
	{
		// Token: 0x0600522E RID: 21038 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0003CA1B File Offset: 0x0003AC1B
		public static ColorProxy GetTeamColor(WTeam team)
		{
			return ColorProxy.New(WColorScheme.GetTeamColor(team));
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0003CA28 File Offset: 0x0003AC28
		public static ColorProxy GetTeamColorBrighter(WTeam team)
		{
			return ColorProxy.New(WColorScheme.GetTeamColorBrighter(team));
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0003CA35 File Offset: 0x0003AC35
		public static string RichTextColorTag(ColorProxy color)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			return WColorScheme.RichTextColorTag(color._value);
		}
	}
}
