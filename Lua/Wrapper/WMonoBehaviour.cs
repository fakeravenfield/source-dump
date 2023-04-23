using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000954 RID: 2388
	[Wrapper(typeof(MonoBehaviour), includeBase = false)]
	public static class WMonoBehaviour
	{
		// Token: 0x06003C8B RID: 15499 RVA: 0x00029041 File Offset: 0x00027241
		[Getter]
		public static Transform GetTransform(MonoBehaviour self)
		{
			return self.transform;
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x00029049 File Offset: 0x00027249
		[Getter]
		public static GameObject GetGameObject(MonoBehaviour self)
		{
			return self.gameObject;
		}
	}
}
