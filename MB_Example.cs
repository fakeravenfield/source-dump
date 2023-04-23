using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class MB_Example : MonoBehaviour
{
	// Token: 0x060000DC RID: 220 RVA: 0x00002C54 File Offset: 0x00000E54
	private void Start()
	{
		this.meshbaker.AddDeleteGameObjects(this.objsToCombine, null, true);
		this.meshbaker.Apply(null);
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0004048C File Offset: 0x0003E68C
	private void LateUpdate()
	{
		this.meshbaker.UpdateGameObjects(this.objsToCombine, true, true, true, true, false, false, false, false, false);
		this.meshbaker.Apply(false, true, true, true, false, false, false, false, false, false, false, null);
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00002C76 File Offset: 0x00000E76
	private void OnGUI()
	{
		GUILayout.Label("Dynamically updates the vertices, normals and tangents in combined mesh every frame.\nThis is similar to dynamic batching. It is not recommended to do this every frame.\nAlso consider baking the mesh renderer objects into a skinned mesh renderer\nThe skinned mesh approach is faster for objects that need to move independently of each other every frame.", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x0400008A RID: 138
	public MB3_MeshBaker meshbaker;

	// Token: 0x0400008B RID: 139
	public GameObject[] objsToCombine;
}
