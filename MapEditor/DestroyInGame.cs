using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000623 RID: 1571
	public class DestroyInGame : MonoBehaviour
	{
		// Token: 0x0600285D RID: 10333 RVA: 0x0001BD70 File Offset: 0x00019F70
		private void Start()
		{
			if (!MapEditor.IsAvailable())
			{
				base.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
