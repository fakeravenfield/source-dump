using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class Spawner : MonoBehaviour
{
	// Token: 0x06001442 RID: 5186 RVA: 0x0001026F File Offset: 0x0000E46F
	private void Update()
	{
		if (Input.GetKeyDown(this.key))
		{
			UnityEngine.Object.Instantiate<GameObject>(this.prefab, base.transform.position, base.transform.rotation);
		}
	}

	// Token: 0x0400159B RID: 5531
	public KeyCode key;

	// Token: 0x0400159C RID: 5532
	public GameObject prefab;
}
