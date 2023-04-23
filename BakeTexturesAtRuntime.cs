using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class BakeTexturesAtRuntime : MonoBehaviour
{
	// Token: 0x060000C6 RID: 198 RVA: 0x0003FE14 File Offset: 0x0003E014
	private void OnGUI()
	{
		GUILayout.Label("Time to bake textures: " + this.elapsedTime.ToString(), Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Combine textures & build combined mesh all at once", Array.Empty<GUILayoutOption>()))
		{
			MB3_MeshBaker componentInChildren = this.target.GetComponentInChildren<MB3_MeshBaker>();
			MB3_TextureBaker component = this.target.GetComponent<MB3_TextureBaker>();
			component.textureBakeResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
			component.resultMaterial = new Material(Shader.Find("Diffuse"));
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			component.CreateAtlases();
			this.elapsedTime = Time.realtimeSinceStartup - realtimeSinceStartup;
			componentInChildren.ClearMesh();
			componentInChildren.textureBakeResults = component.textureBakeResults;
			componentInChildren.AddDeleteGameObjects(component.GetObjectsToCombine().ToArray(), null, true);
			componentInChildren.Apply(null);
		}
		if (GUILayout.Button("Combine textures & build combined mesh using coroutine", Array.Empty<GUILayoutOption>()))
		{
			Debug.Log("Starting to bake textures on frame " + Time.frameCount.ToString());
			MB3_TextureBaker component2 = this.target.GetComponent<MB3_TextureBaker>();
			component2.textureBakeResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
			component2.resultMaterial = new Material(Shader.Find("Diffuse"));
			component2.onBuiltAtlasesSuccess = new MB3_TextureBaker.OnCombinedTexturesCoroutineSuccess(this.OnBuiltAtlasesSuccess);
			base.StartCoroutine(component2.CreateAtlasesCoroutine(null, this.result, false, null, 0.01f));
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0003FF54 File Offset: 0x0003E154
	private void OnBuiltAtlasesSuccess()
	{
		Debug.Log("Calling success callback. baking meshes");
		MB3_MeshBaker componentInChildren = this.target.GetComponentInChildren<MB3_MeshBaker>();
		MB3_TextureBaker component = this.target.GetComponent<MB3_TextureBaker>();
		if (this.result.isFinished && this.result.success)
		{
			componentInChildren.ClearMesh();
			componentInChildren.textureBakeResults = component.textureBakeResults;
			componentInChildren.AddDeleteGameObjects(component.GetObjectsToCombine().ToArray(), null, true);
			componentInChildren.Apply(null);
		}
		Debug.Log("Completed baking textures on frame " + Time.frameCount.ToString());
	}

	// Token: 0x0400007C RID: 124
	public GameObject target;

	// Token: 0x0400007D RID: 125
	private float elapsedTime;

	// Token: 0x0400007E RID: 126
	private MB3_TextureBaker.CreateAtlasesCoroutineResult result = new MB3_TextureBaker.CreateAtlasesCoroutineResult();
}
