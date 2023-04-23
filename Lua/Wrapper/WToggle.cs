using System;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x0200095A RID: 2394
	[Wrapper(typeof(Toggle), includeTarget = true)]
	[Name("Toggle")]
	public static class WToggle
	{
		// Token: 0x06003CA6 RID: 15526 RVA: 0x00029152 File Offset: 0x00027352
		[Getter]
		[CallbackSignature(new string[]
		{
			"newValue"
		})]
		public static ScriptEvent GetOnValueChanged(Toggle self)
		{
			return RavenscriptManager.events.WrapUnityEvent<bool>(self, self.onValueChanged);
		}
	}
}
