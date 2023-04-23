using System;
using UnityEngine;

namespace LevelSystem
{
	// Token: 0x02000362 RID: 866
	public class Activator : MonoBehaviour
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x0001174B File Offset: 0x0000F94B
		public virtual void Trigger()
		{
			this.ActivateObjects();
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0009F738 File Offset: 0x0009D938
		protected void ActivateObjects()
		{
			GameObject[] array = this.objectsToActivate;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(true);
			}
			array = this.objectsToDeactivate;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}

		// Token: 0x04001886 RID: 6278
		public GameObject[] objectsToActivate;

		// Token: 0x04001887 RID: 6279
		public GameObject[] objectsToDeactivate;
	}
}
