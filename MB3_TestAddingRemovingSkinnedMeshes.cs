using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class MB3_TestAddingRemovingSkinnedMeshes : MonoBehaviour
{
	// Token: 0x06000186 RID: 390 RVA: 0x000031FE File Offset: 0x000013FE
	private void Start()
	{
		base.StartCoroutine(this.TestScript());
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000320D File Offset: 0x0000140D
	private IEnumerator TestScript()
	{
		Debug.Log("Test 1 adding 0,1,2");
		GameObject[] gos = new GameObject[]
		{
			this.g[0],
			this.g[1],
			this.g[2]
		};
		this.meshBaker.AddDeleteGameObjects(gos, null, true);
		this.meshBaker.Apply(null);
		this.meshBaker.meshCombiner.CheckIntegrity();
		yield return new WaitForSeconds(3f);
		Debug.Log("Test 2 remove 1 and add 3,4,5");
		GameObject[] deleteGOs = new GameObject[]
		{
			this.g[1]
		};
		gos = new GameObject[]
		{
			this.g[3],
			this.g[4],
			this.g[5]
		};
		this.meshBaker.AddDeleteGameObjects(gos, deleteGOs, true);
		this.meshBaker.Apply(null);
		this.meshBaker.meshCombiner.CheckIntegrity();
		yield return new WaitForSeconds(3f);
		Debug.Log("Test 3 remove 0,2,5 and add 1");
		deleteGOs = new GameObject[]
		{
			this.g[3],
			this.g[4],
			this.g[5]
		};
		gos = new GameObject[]
		{
			this.g[1]
		};
		this.meshBaker.AddDeleteGameObjects(gos, deleteGOs, true);
		this.meshBaker.Apply(null);
		this.meshBaker.meshCombiner.CheckIntegrity();
		yield return new WaitForSeconds(3f);
		Debug.Log("Test 3 remove all remaining");
		deleteGOs = new GameObject[]
		{
			this.g[0],
			this.g[1],
			this.g[2]
		};
		this.meshBaker.AddDeleteGameObjects(null, deleteGOs, true);
		this.meshBaker.Apply(null);
		this.meshBaker.meshCombiner.CheckIntegrity();
		yield return new WaitForSeconds(3f);
		Debug.Log("Test 3 add all");
		this.meshBaker.AddDeleteGameObjects(this.g, null, true);
		this.meshBaker.Apply(null);
		this.meshBaker.meshCombiner.CheckIntegrity();
		yield return new WaitForSeconds(1f);
		Debug.Log("Done");
		yield break;
	}

	// Token: 0x04000102 RID: 258
	public MB3_MeshBaker meshBaker;

	// Token: 0x04000103 RID: 259
	public GameObject[] g;
}
