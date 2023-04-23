using System;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x0200094E RID: 2382
	[Wrapper(typeof(Button), includeTarget = true)]
	[Name("Button")]
	public static class WButton
	{
		// Token: 0x06003C54 RID: 15444 RVA: 0x00028E48 File Offset: 0x00027048
		[Getter]
		public static ScriptEvent GetOnClick(Button self)
		{
			return RavenscriptManager.events.WrapUnityEvent(self, self.onClick);
		}
	}
}
