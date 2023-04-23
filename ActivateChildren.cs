using System;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class ActivateChildren : MonoBehaviour
{
	// Token: 0x06001166 RID: 4454 RVA: 0x0008BD34 File Offset: 0x00089F34
	public virtual void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
}
