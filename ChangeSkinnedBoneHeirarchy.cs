using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
[ExecuteInEditMode]
public class ChangeSkinnedBoneHeirarchy : MonoBehaviour
{
	// Token: 0x060013E8 RID: 5096 RVA: 0x00094E4C File Offset: 0x0009304C
	[ExecuteInEditMode]
	private void OnEnable()
	{
		SkinnedMeshRenderer component = base.GetComponent<SkinnedMeshRenderer>();
		Transform[] array = new Transform[component.bones.Length];
		for (int i = 0; i < component.bones.Length; i++)
		{
			Debug.Log("Matching " + component.bones[i].name);
			Transform transform = ChangeSkinnedBoneHeirarchy.FindDeepChild(base.transform.parent, component.bones[i].name);
			if (transform == null)
			{
				Debug.LogError("Could not match bone " + component.bones[i].name);
				return;
			}
			array[i] = transform;
		}
		Debug.Log("Bone references updated!");
		component.bones = array;
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x00094EF8 File Offset: 0x000930F8
	public static Transform FindDeepChild(Transform aParent, string aName)
	{
		Transform transform = aParent.Find(aName);
		if (transform != null)
		{
			return transform;
		}
		foreach (object obj in aParent)
		{
			transform = ChangeSkinnedBoneHeirarchy.FindDeepChild((Transform)obj, aName);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}
}
