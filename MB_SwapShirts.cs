using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class MB_SwapShirts : MonoBehaviour
{
	// Token: 0x060000CB RID: 203 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
	private void Start()
	{
		GameObject[] array = new GameObject[this.clothingAndBodyPartsBareTorso.Length];
		for (int i = 0; i < this.clothingAndBodyPartsBareTorso.Length; i++)
		{
			array[i] = this.clothingAndBodyPartsBareTorso[i].gameObject;
		}
		this.meshBaker.ClearMesh();
		this.meshBaker.AddDeleteGameObjects(array, null, true);
		this.meshBaker.Apply(null);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0004004C File Offset: 0x0003E24C
	private void OnGUI()
	{
		if (GUILayout.Button("Wear Hoodie", Array.Empty<GUILayoutOption>()))
		{
			this.ChangeOutfit(this.clothingAndBodyPartsHoodie);
		}
		if (GUILayout.Button("Bare Torso", Array.Empty<GUILayoutOption>()))
		{
			this.ChangeOutfit(this.clothingAndBodyPartsBareTorso);
		}
		if (GUILayout.Button("Damaged Arm", Array.Empty<GUILayoutOption>()))
		{
			this.ChangeOutfit(this.clothingAndBodyPartsBareTorsoDamagedArm);
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x000400B0 File Offset: 0x0003E2B0
	private void ChangeOutfit(Renderer[] outfit)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in this.meshBaker.meshCombiner.GetObjectsInCombined())
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			bool flag = false;
			for (int i = 0; i < outfit.Length; i++)
			{
				if (component == outfit[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(component.gameObject);
				string str = "Removing ";
				GameObject gameObject2 = component.gameObject;
				Debug.Log(str + ((gameObject2 != null) ? gameObject2.ToString() : null));
			}
		}
		List<GameObject> list2 = new List<GameObject>();
		for (int j = 0; j < outfit.Length; j++)
		{
			if (!this.meshBaker.meshCombiner.GetObjectsInCombined().Contains(outfit[j].gameObject))
			{
				list2.Add(outfit[j].gameObject);
				string str2 = "Adding ";
				GameObject gameObject3 = outfit[j].gameObject;
				Debug.Log(str2 + ((gameObject3 != null) ? gameObject3.ToString() : null));
			}
		}
		this.meshBaker.AddDeleteGameObjects(list2.ToArray(), list.ToArray(), true);
		this.meshBaker.Apply(null);
	}

	// Token: 0x0400007F RID: 127
	public MB3_MeshBaker meshBaker;

	// Token: 0x04000080 RID: 128
	public Renderer[] clothingAndBodyPartsBareTorso;

	// Token: 0x04000081 RID: 129
	public Renderer[] clothingAndBodyPartsBareTorsoDamagedArm;

	// Token: 0x04000082 RID: 130
	public Renderer[] clothingAndBodyPartsHoodie;
}
