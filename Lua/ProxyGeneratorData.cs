using System;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000925 RID: 2341
	[CreateAssetMenu(fileName = "ProxyGeneratorData", menuName = "Lua Proxy Generator", order = 1)]
	public class ProxyGeneratorData : ScriptableObject
	{
		// Token: 0x0400307A RID: 12410
		public string proxiedTypes;

		// Token: 0x0400307B RID: 12411
		public string allowedTypes;

		// Token: 0x0400307C RID: 12412
		public string outputPath;

		// Token: 0x0400307D RID: 12413
		public string docPath;
	}
}
