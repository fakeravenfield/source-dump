using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class MB2_TestShowHide : MonoBehaviour
{
	// Token: 0x06000181 RID: 385 RVA: 0x00042A14 File Offset: 0x00040C14
	private void Update()
	{
		if (Time.frameCount == 100)
		{
			this.mb.ShowHide(null, this.objs);
			this.mb.ApplyShowHide();
			Debug.Log("should have disappeared");
		}
		if (Time.frameCount == 200)
		{
			this.mb.ShowHide(this.objs, null);
			this.mb.ApplyShowHide();
			Debug.Log("should show");
		}
	}

	// Token: 0x040000FA RID: 250
	public MB3_MeshBaker mb;

	// Token: 0x040000FB RID: 251
	public GameObject[] objs;
}
