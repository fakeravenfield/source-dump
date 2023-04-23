using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class MB_SwitchBakedObjectsTexture : MonoBehaviour
{
	// Token: 0x060000E8 RID: 232 RVA: 0x00002C98 File Offset: 0x00000E98
	public void OnGUI()
	{
		GUILayout.Label("Press space to switch the material on one of the cubes. This scene reuses the Texture Bake Result from the SceneBasic example.", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00002CA9 File Offset: 0x00000EA9
	public void Start()
	{
		this.meshBaker.AddDeleteGameObjects(this.meshBaker.GetObjectsToCombine().ToArray(), null, true);
		this.meshBaker.Apply(null);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x000409B4 File Offset: 0x0003EBB4
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Material sharedMaterial = this.targetRenderer.sharedMaterial;
			int num = -1;
			for (int i = 0; i < this.materials.Length; i++)
			{
				if (this.materials[i] == sharedMaterial)
				{
					num = i;
				}
			}
			num++;
			if (num >= this.materials.Length)
			{
				num = 0;
			}
			if (num != -1)
			{
				this.targetRenderer.sharedMaterial = this.materials[num];
				string str = "Updating Material to: ";
				Material sharedMaterial2 = this.targetRenderer.sharedMaterial;
				Debug.Log(str + ((sharedMaterial2 != null) ? sharedMaterial2.ToString() : null));
				GameObject[] gos = new GameObject[]
				{
					this.targetRenderer.gameObject
				};
				this.meshBaker.UpdateGameObjects(gos, false, false, false, false, true, false, false, false, false);
				this.meshBaker.Apply(null);
			}
		}
	}

	// Token: 0x04000096 RID: 150
	public MeshRenderer targetRenderer;

	// Token: 0x04000097 RID: 151
	public Material[] materials;

	// Token: 0x04000098 RID: 152
	public MB3_MeshBaker meshBaker;
}
