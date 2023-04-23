using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class LogBoneMap : MonoBehaviour
{
	// Token: 0x0600112A RID: 4394 RVA: 0x0008B900 File Offset: 0x00089B00
	private void Start()
	{
		SkinnedMeshRenderer component = base.GetComponent<SkinnedMeshRenderer>();
		Transform[] bones = component.bones;
		string text = component.sharedMesh.name + " bones:";
		for (int i = 0; i < component.bones.Length; i++)
		{
			text += string.Format("\n{0}: {1}", i, component.bones[i].gameObject.name);
		}
		Debug.Log(text);
	}
}
