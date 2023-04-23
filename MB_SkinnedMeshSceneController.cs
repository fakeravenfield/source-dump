using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class MB_SkinnedMeshSceneController : MonoBehaviour
{
	// Token: 0x060000E4 RID: 228 RVA: 0x00040520 File Offset: 0x0003E720
	private void Start()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.workerPrefab);
		gameObject.transform.position = new Vector3(1.31f, 0.985f, -0.25f);
		Animation component = gameObject.GetComponent<Animation>();
		component.wrapMode = WrapMode.Loop;
		component.cullingType = AnimationCullingType.AlwaysAnimate;
		component.Play("run");
		GameObject[] gos = new GameObject[]
		{
			gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject
		};
		this.skinnedMeshBaker.AddDeleteGameObjects(gos, null, true);
		this.skinnedMeshBaker.Apply(null);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x000405A8 File Offset: 0x0003E7A8
	private void OnGUI()
	{
		if (GUILayout.Button("Add/Remove Sword", Array.Empty<GUILayoutOption>()))
		{
			if (this.swordInstance == null)
			{
				Transform parent = this.SearchHierarchyForBone(this.targetCharacter.transform, "RightHandAttachPoint");
				this.swordInstance = UnityEngine.Object.Instantiate<GameObject>(this.swordPrefab);
				this.swordInstance.transform.parent = parent;
				this.swordInstance.transform.localPosition = Vector3.zero;
				this.swordInstance.transform.localRotation = Quaternion.identity;
				this.swordInstance.transform.localScale = Vector3.one;
				GameObject[] gos = new GameObject[]
				{
					this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject
				};
				this.skinnedMeshBaker.AddDeleteGameObjects(gos, null, true);
				this.skinnedMeshBaker.Apply(null);
			}
			else if (this.skinnedMeshBaker.CombinedMeshContains(this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject))
			{
				GameObject[] deleteGOs = new GameObject[]
				{
					this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject
				};
				this.skinnedMeshBaker.AddDeleteGameObjects(null, deleteGOs, true);
				this.skinnedMeshBaker.Apply(null);
				UnityEngine.Object.Destroy(this.swordInstance);
				this.swordInstance = null;
			}
		}
		if (GUILayout.Button("Add/Remove Hat", Array.Empty<GUILayoutOption>()))
		{
			if (this.hatInstance == null)
			{
				Transform parent2 = this.SearchHierarchyForBone(this.targetCharacter.transform, "HeadAttachPoint");
				this.hatInstance = UnityEngine.Object.Instantiate<GameObject>(this.hatPrefab);
				this.hatInstance.transform.parent = parent2;
				this.hatInstance.transform.localPosition = Vector3.zero;
				this.hatInstance.transform.localRotation = Quaternion.identity;
				this.hatInstance.transform.localScale = Vector3.one;
				GameObject[] gos2 = new GameObject[]
				{
					this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject
				};
				this.skinnedMeshBaker.AddDeleteGameObjects(gos2, null, true);
				this.skinnedMeshBaker.Apply(null);
			}
			else if (this.skinnedMeshBaker.CombinedMeshContains(this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject))
			{
				GameObject[] deleteGOs2 = new GameObject[]
				{
					this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject
				};
				this.skinnedMeshBaker.AddDeleteGameObjects(null, deleteGOs2, true);
				this.skinnedMeshBaker.Apply(null);
				UnityEngine.Object.Destroy(this.hatInstance);
				this.hatInstance = null;
			}
		}
		if (GUILayout.Button("Add/Remove Glasses", Array.Empty<GUILayoutOption>()))
		{
			if (this.glassesInstance == null)
			{
				Transform parent3 = this.SearchHierarchyForBone(this.targetCharacter.transform, "NoseAttachPoint");
				this.glassesInstance = UnityEngine.Object.Instantiate<GameObject>(this.glassesPrefab);
				this.glassesInstance.transform.parent = parent3;
				this.glassesInstance.transform.localPosition = Vector3.zero;
				this.glassesInstance.transform.localRotation = Quaternion.identity;
				this.glassesInstance.transform.localScale = Vector3.one;
				GameObject[] gos3 = new GameObject[]
				{
					this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject
				};
				this.skinnedMeshBaker.AddDeleteGameObjects(gos3, null, true);
				this.skinnedMeshBaker.Apply(null);
				return;
			}
			if (this.skinnedMeshBaker.CombinedMeshContains(this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject))
			{
				GameObject[] deleteGOs3 = new GameObject[]
				{
					this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject
				};
				this.skinnedMeshBaker.AddDeleteGameObjects(null, deleteGOs3, true);
				this.skinnedMeshBaker.Apply(null);
				UnityEngine.Object.Destroy(this.glassesInstance);
				this.glassesInstance = null;
			}
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00040968 File Offset: 0x0003EB68
	public Transform SearchHierarchyForBone(Transform current, string name)
	{
		if (current.name.Equals(name))
		{
			return current;
		}
		for (int i = 0; i < current.childCount; i++)
		{
			Transform transform = this.SearchHierarchyForBone(current.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x0400008D RID: 141
	public GameObject swordPrefab;

	// Token: 0x0400008E RID: 142
	public GameObject hatPrefab;

	// Token: 0x0400008F RID: 143
	public GameObject glassesPrefab;

	// Token: 0x04000090 RID: 144
	public GameObject workerPrefab;

	// Token: 0x04000091 RID: 145
	public GameObject targetCharacter;

	// Token: 0x04000092 RID: 146
	public MB3_MeshBaker skinnedMeshBaker;

	// Token: 0x04000093 RID: 147
	private GameObject swordInstance;

	// Token: 0x04000094 RID: 148
	private GameObject glassesInstance;

	// Token: 0x04000095 RID: 149
	private GameObject hatInstance;
}
