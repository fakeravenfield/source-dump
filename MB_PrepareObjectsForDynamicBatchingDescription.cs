using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class MB_PrepareObjectsForDynamicBatchingDescription : MonoBehaviour
{
	// Token: 0x060000CF RID: 207 RVA: 0x00002BF9 File Offset: 0x00000DF9
	private void OnGUI()
	{
		GUILayout.Label("This scene creates a combined material and meshes with adjusted UVs so objects \n can share a material and be batched by Unity's static/dynamic batching.\n Output has been set to 'bakeMeshAssetsInPlace' on the Mesh Baker\n Position, Scale and Rotation will be baked into meshes so place them appropriately.\n Dynamic batching requires objects with uniform scale. You can fix non-uniform scale here\n After baking you need to duplicate your source prefab assets and replace the  \n meshes and materials with the generated ones.\n", Array.Empty<GUILayoutOption>());
	}
}
