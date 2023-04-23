using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001BA RID: 442
public static class LevelPatcher
{
	// Token: 0x06000BF7 RID: 3063 RVA: 0x000776F8 File Offset: 0x000758F8
	public static void Run(ModManager.EngineVersionInfo sourceVersion)
	{
		Debug.Log(string.Format("Scene: {0}, Version: {1}", SceneManager.GetActiveScene().name, sourceVersion));
		if (sourceVersion.majorVersion == 5)
		{
			LevelPatcher.UpgradeUnity5();
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00077738 File Offset: 0x00075938
	private static void UpgradeUnity5()
	{
		Terrain[] array = UnityEngine.Object.FindObjectsOfType<Terrain>();
		for (int i = 0; i < array.Length; i++)
		{
			LevelPatcher.UpgradeTerrainTreeLayers(array[i]);
		}
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00009E27 File Offset: 0x00008027
	private static bool AssumeVisionOccluder(Collider collider)
	{
		return !(collider is CapsuleCollider);
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00077764 File Offset: 0x00075964
	private static void UpgradeTerrainTreeLayers(Terrain terrain)
	{
		TerrainData terrainData = terrain.terrainData;
		GameObject[] array = new GameObject[terrainData.treePrototypes.Length];
		TreePrototype[] treePrototypes = terrainData.treePrototypes;
		for (int i = 0; i < treePrototypes.Length; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(treePrototypes[i].prefab);
			gameObject.name += " COLLISION IMPOSTER";
			foreach (Collider collider in gameObject.GetComponentsInChildren<Collider>(true))
			{
				collider.gameObject.layer = (LevelPatcher.AssumeVisionOccluder(collider) ? 23 : 0);
			}
			Renderer[] componentsInChildren2 = gameObject.GetComponentsInChildren<Renderer>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				UnityEngine.Object.DestroyImmediate(componentsInChildren2[j]);
			}
			array[i] = gameObject;
			Collider[] componentsInChildren = treePrototypes[i].prefab.GetComponentsInChildren<Collider>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				UnityEngine.Object.DestroyImmediate(componentsInChildren[j], true);
			}
		}
		terrainData.treePrototypes = treePrototypes;
		TreeInstance[] treeInstances = terrainData.treeInstances;
		terrainData.SetTreeInstances(treeInstances, false);
		TerrainCollider component = terrain.GetComponent<TerrainCollider>();
		component.enabled = false;
		component.enabled = true;
		Vector3 size = terrain.terrainData.size;
		Vector3 position = terrain.transform.position;
		for (int k = 0; k < treeInstances.Length; k++)
		{
			Vector3 b = Vector3.Scale(treeInstances[k].position, size);
			Quaternion rotation = Quaternion.Euler(0f, treeInstances[k].rotation * 57.29578f, 0f);
			Vector3 b2 = new Vector3(treeInstances[k].widthScale, treeInstances[k].heightScale, treeInstances[k].widthScale);
			Transform transform = UnityEngine.Object.Instantiate<GameObject>(array[treeInstances[k].prototypeIndex]).transform;
			transform.position = position + b;
			transform.rotation = rotation;
			transform.localScale = Vector3.Scale(transform.localScale, b2);
			transform.hideFlags = HideFlags.HideInHierarchy;
		}
		GameObject[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].SetActive(false);
		}
	}

	// Token: 0x04000D15 RID: 3349
	private const int DEFAULT_TREE_LAYER = 0;
}
