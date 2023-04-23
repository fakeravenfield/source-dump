using System;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x02000958 RID: 2392
	[Wrapper(typeof(Slider), includeTarget = true)]
	[Name("Slider")]
	public static class WSlider
	{
		// Token: 0x06003C9D RID: 15517 RVA: 0x000290DC File Offset: 0x000272DC
		[Getter]
		[Doc("Arguments: ``float value``")]
		public static ScriptEvent GetOnValueChanged(Slider self)
		{
			return RavenscriptManager.events.WrapUnityEvent<float>(self, self.onValueChanged);
		}
	}
}
