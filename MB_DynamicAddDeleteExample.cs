using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class MB_DynamicAddDeleteExample : MonoBehaviour
{
	// Token: 0x060000D1 RID: 209 RVA: 0x000401F8 File Offset: 0x0003E3F8
	private float GaussianValue()
	{
		float num;
		float num3;
		do
		{
			num = 2f * UnityEngine.Random.Range(0f, 1f) - 1f;
			float num2 = 2f * UnityEngine.Random.Range(0f, 1f) - 1f;
			num3 = num * num + num2 * num2;
		}
		while (num3 >= 1f);
		num3 = Mathf.Sqrt(-2f * Mathf.Log(num3) / num3);
		return num * num3;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00040264 File Offset: 0x0003E464
	private void Start()
	{
		this.mbd = base.GetComponentInChildren<MB3_MultiMeshBaker>();
		int num = 10;
		GameObject[] array = new GameObject[num * num];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
				array[i * num + j] = gameObject.GetComponentInChildren<MeshRenderer>().gameObject;
				float num2 = UnityEngine.Random.Range(-4f, 4f);
				float num3 = UnityEngine.Random.Range(-4f, 4f);
				gameObject.transform.position = new Vector3(3f * (float)i + num2, 0f, 3f * (float)j + num3);
				float y = (float)UnityEngine.Random.Range(0, 360);
				gameObject.transform.rotation = Quaternion.Euler(0f, y, 0f);
				Vector3 localScale = Vector3.one + Vector3.one * this.GaussianValue() * 0.15f;
				gameObject.transform.localScale = localScale;
				if ((i * num + j) % 3 == 0)
				{
					this.objsInCombined.Add(array[i * num + j]);
				}
			}
		}
		this.mbd.AddDeleteGameObjects(array, null, true);
		this.mbd.Apply(null);
		this.objs = this.objsInCombined.ToArray();
		base.StartCoroutine(this.largeNumber());
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00002C0A File Offset: 0x00000E0A
	private IEnumerator largeNumber()
	{
		for (;;)
		{
			yield return new WaitForSeconds(1.5f);
			this.mbd.AddDeleteGameObjects(null, this.objs, true);
			this.mbd.Apply(null);
			yield return new WaitForSeconds(1.5f);
			this.mbd.AddDeleteGameObjects(this.objs, null, true);
			this.mbd.Apply(null);
		}
		yield break;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00002C19 File Offset: 0x00000E19
	private void OnGUI()
	{
		GUILayout.Label("Dynamically instantiates game objects. \nRepeatedly adds and removes some of them\n from the combined mesh.", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x04000083 RID: 131
	public GameObject prefab;

	// Token: 0x04000084 RID: 132
	private List<GameObject> objsInCombined = new List<GameObject>();

	// Token: 0x04000085 RID: 133
	private MB3_MultiMeshBaker mbd;

	// Token: 0x04000086 RID: 134
	private GameObject[] objs;
}
