using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class LevelBounds : MonoBehaviour
{
	// Token: 0x06000BEC RID: 3052 RVA: 0x00009D7E File Offset: 0x00007F7E
	public static bool IsInside(Vector3 point)
	{
		return LevelBounds.instance == null || LevelBounds.instance.bounds.Contains(point);
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00009D9F File Offset: 0x00007F9F
	private void SetupBounds()
	{
		LevelBounds.instance = this;
		this.bounds = new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00009DC8 File Offset: 0x00007FC8
	private void Awake()
	{
		this.SetupBounds();
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x04000CF9 RID: 3321
	public static LevelBounds instance;

	// Token: 0x04000CFA RID: 3322
	private Bounds bounds;
}
