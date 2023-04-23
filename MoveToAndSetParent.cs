using System;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class MoveToAndSetParent : MonoBehaviour
{
	// Token: 0x06000CBF RID: 3263 RVA: 0x0000A6B5 File Offset: 0x000088B5
	public void MoveTo(Transform target)
	{
		base.transform.parent = target;
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0000A6F3 File Offset: 0x000088F3
	public void MoveToTargetIndex(int index)
	{
		this.MoveTo(this.targets[index]);
	}

	// Token: 0x04000DC1 RID: 3521
	public Transform[] targets;
}
