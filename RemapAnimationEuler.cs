using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class RemapAnimationEuler : MonoBehaviour
{
	// Token: 0x0600141D RID: 5149 RVA: 0x0001009C File Offset: 0x0000E29C
	private void Start()
	{
		this.BuildTransformArray();
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000955DC File Offset: 0x000937DC
	private void BuildTransformArray()
	{
		List<Transform> list = new List<Transform>();
		this.RecRegisterChild(base.transform, list);
		this.children = list.ToArray();
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x00095608 File Offset: 0x00093808
	private void RecRegisterChild(Transform t, List<Transform> l)
	{
		l.Add(t);
		for (int i = 0; i < t.childCount; i++)
		{
			this.RecRegisterChild(t.GetChild(i), l);
		}
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0009563C File Offset: 0x0009383C
	private void LateUpdate()
	{
		for (int i = 0; i < this.children.Length; i++)
		{
			Vector3 localEulerAngles = this.children[i].localEulerAngles;
			this.children[i].localEulerAngles = RemapAnimationEuler.Remap(localEulerAngles);
		}
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x00095680 File Offset: 0x00093880
	private static Vector3 Remap(Vector3 euler)
	{
		return (Quaternion.AngleAxis(euler.z, Vector3.forward) * Quaternion.AngleAxis(euler.y, Vector3.up) * Quaternion.AngleAxis(euler.x, Vector3.right)).eulerAngles;
	}

	// Token: 0x04001592 RID: 5522
	private Transform[] children;
}
