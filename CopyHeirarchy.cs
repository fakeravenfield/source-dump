using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class CopyHeirarchy : MonoBehaviour
{
	// Token: 0x060006A5 RID: 1701 RVA: 0x000063F1 File Offset: 0x000045F1
	private void Awake()
	{
		this.BuildTransformMapping();
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x000063F9 File Offset: 0x000045F9
	private void BuildTransformMapping()
	{
		this.map = new Dictionary<Transform, Transform>(64);
		this.BuildMap(base.transform, this.targetRoot);
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0000641A File Offset: 0x0000461A
	private void LateUpdate()
	{
		this.ApplyTransforms();
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0005FB08 File Offset: 0x0005DD08
	private void ApplyTransforms()
	{
		foreach (Transform transform in this.map.Keys)
		{
			bool flag = transform == base.transform;
			Transform transform2 = this.map[transform];
			if (flag)
			{
				transform.position = transform2.position;
				transform.rotation = transform2.rotation;
			}
			else
			{
				transform.localPosition = transform2.localPosition;
				transform.localRotation = transform2.localRotation;
			}
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0005FBA8 File Offset: 0x0005DDA8
	private void BuildMap(Transform mine, Transform theirs)
	{
		this.map.Add(mine, theirs);
		int num = Mathf.Min(mine.childCount, theirs.childCount);
		for (int i = 0; i < num; i++)
		{
			this.BuildMap(mine.GetChild(i), theirs.GetChild(i));
		}
	}

	// Token: 0x0400068B RID: 1675
	public Transform targetRoot;

	// Token: 0x0400068C RID: 1676
	private Dictionary<Transform, Transform> map;
}
