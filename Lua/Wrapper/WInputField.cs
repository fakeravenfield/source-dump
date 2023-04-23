using System;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x02000953 RID: 2387
	[Wrapper(typeof(InputField), includeTarget = true)]
	[Name("InputField")]
	public static class WInputField
	{
		// Token: 0x06003C89 RID: 15497 RVA: 0x0002901B File Offset: 0x0002721B
		[Getter]
		[Doc("Arguments: ``string value``")]
		public static ScriptEvent GetOnValueChanged(InputField self)
		{
			return RavenscriptManager.events.WrapUnityEvent<string>(self, self.onValueChanged);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x0002902E File Offset: 0x0002722E
		[Getter]
		[Doc("Arguments: ``string value``")]
		public static ScriptEvent OnEndEdit(InputField self)
		{
			return RavenscriptManager.events.WrapUnityEvent<string>(self, self.onEndEdit);
		}
	}
}
