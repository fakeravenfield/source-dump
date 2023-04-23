using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200062D RID: 1581
	public abstract class MeoAssistant : MonoBehaviour, IAllowInPrefabRenderer
	{
		// Token: 0x0600288C RID: 10380 RVA: 0x0001BF18 File Offset: 0x0001A118
		protected virtual void Start()
		{
			if (!MapEditor.IsAvailable())
			{
				base.enabled = false;
				UnityEngine.Object.Destroy(this);
			}
		}
	}
}
