using System;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x0200094F RID: 2383
	[Wrapper(typeof(Dropdown), includeTarget = true)]
	[Name("Dropdown")]
	public static class WDropdown
	{
		// Token: 0x06003C55 RID: 15445 RVA: 0x00028E5B File Offset: 0x0002705B
		[Getter]
		[Doc("Arguments: ``int value``")]
		public static ScriptEvent GetOnValueChanged(Dropdown self)
		{
			return RavenscriptManager.events.WrapUnityEvent<int>(self, self.onValueChanged);
		}
	}
}
