using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A1C RID: 2588
	[Proxy(typeof(WIngameDialog))]
	public class WIngameDialogProxy : IProxy
	{
		// Token: 0x06005264 RID: 21092 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0003CC22 File Offset: 0x0003AE22
		public static void Hide()
		{
			WIngameDialog.Hide();
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0003CC29 File Offset: 0x0003AE29
		public static void HideAfter(float duration)
		{
			WIngameDialog.HideAfter(duration);
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0003CC31 File Offset: 0x0003AE31
		public static void HideInstant()
		{
			WIngameDialog.HideInstant();
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0003CC38 File Offset: 0x0003AE38
		public static void PrintActorText(string actorPose, string text)
		{
			WIngameDialog.PrintActorText(actorPose, text);
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0003CC41 File Offset: 0x0003AE41
		public static void PrintActorText(string actorPose, string text, string overrideName)
		{
			WIngameDialog.PrintActorText(actorPose, text, overrideName);
		}
	}
}
