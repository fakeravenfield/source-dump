using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200061E RID: 1566
	public static class MonoBehaviourExtensions
	{
		// Token: 0x06002846 RID: 10310 RVA: 0x000FA034 File Offset: 0x000F8234
		public static T GetOrCreateComponent<T>(this MonoBehaviour self) where T : Component
		{
			T t = self.GetComponent<T>();
			if (t == null)
			{
				t = self.gameObject.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000FA064 File Offset: 0x000F8264
		public static T GetOrCreateComponent<T>(this GameObject self) where T : Component
		{
			T t = self.GetComponent<T>();
			if (t == null)
			{
				t = self.AddComponent<T>();
			}
			return t;
		}
	}
}
