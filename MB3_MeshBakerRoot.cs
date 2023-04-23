using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000045 RID: 69
public abstract class MB3_MeshBakerRoot : MonoBehaviour
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x0600012F RID: 303
	// (set) Token: 0x06000130 RID: 304
	[HideInInspector]
	public abstract MB2_TextureBakeResults textureBakeResults { get; set; }

	// Token: 0x06000131 RID: 305 RVA: 0x00002FD8 File Offset: 0x000011D8
	public virtual List<GameObject> GetObjectsToCombine()
	{
		return null;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00041A88 File Offset: 0x0003FC88
	public static bool DoCombinedValidate(MB3_MeshBakerRoot mom, MB_ObjsToCombineTypes objToCombineType, MB2_EditorMethodsInterface editorMethods, MB2_ValidationLevel validationLevel)
	{
		if (mom.textureBakeResults == null)
		{
			Debug.LogError("Need to set Texture Bake Result on " + ((mom != null) ? mom.ToString() : null));
			return false;
		}
		if (mom is MB3_MeshBakerCommon)
		{
			MB3_TextureBaker textureBaker = ((MB3_MeshBakerCommon)mom).GetTextureBaker();
			if (textureBaker != null && textureBaker.textureBakeResults != mom.textureBakeResults)
			{
				Debug.LogWarning("Texture Bake Result on this component is not the same as the Texture Bake Result on the MB3_TextureBaker.");
			}
		}
		Dictionary<int, MB_Utility.MeshAnalysisResult> dictionary = null;
		if (validationLevel == MB2_ValidationLevel.robust)
		{
			dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult>();
		}
		List<GameObject> objectsToCombine = mom.GetObjectsToCombine();
		for (int i = 0; i < objectsToCombine.Count; i++)
		{
			GameObject gameObject = objectsToCombine[i];
			if (gameObject == null)
			{
				Debug.LogError("The list of objects to combine contains a null at position." + i.ToString() + " Select and use [shift] delete to remove");
				return false;
			}
			for (int j = i + 1; j < objectsToCombine.Count; j++)
			{
				if (objectsToCombine[i] == objectsToCombine[j])
				{
					Debug.LogError("The list of objects to combine contains duplicates at " + i.ToString() + " and " + j.ToString());
					return false;
				}
			}
			if (MB_Utility.GetGOMaterials(gameObject).Length == 0)
			{
				string str = "Object ";
				GameObject gameObject2 = gameObject;
				Debug.LogError(str + ((gameObject2 != null) ? gameObject2.ToString() : null) + " in the list of objects to be combined does not have a material");
				return false;
			}
			Mesh mesh = MB_Utility.GetMesh(gameObject);
			if (mesh == null)
			{
				string str2 = "Object ";
				GameObject gameObject3 = gameObject;
				Debug.LogError(str2 + ((gameObject3 != null) ? gameObject3.ToString() : null) + " in the list of objects to be combined does not have a mesh");
				return false;
			}
			if (mesh != null && !Application.isEditor && Application.isPlaying && mom.textureBakeResults.doMultiMaterial && validationLevel >= MB2_ValidationLevel.robust)
			{
				MB_Utility.MeshAnalysisResult meshAnalysisResult;
				if (!dictionary.TryGetValue(mesh.GetInstanceID(), out meshAnalysisResult))
				{
					MB_Utility.doSubmeshesShareVertsOrTris(mesh, ref meshAnalysisResult);
					dictionary.Add(mesh.GetInstanceID(), meshAnalysisResult);
				}
				if (meshAnalysisResult.hasOverlappingSubmeshVerts)
				{
					string str3 = "Object ";
					GameObject gameObject4 = objectsToCombine[i];
					Debug.LogWarning(str3 + ((gameObject4 != null) ? gameObject4.ToString() : null) + " in the list of objects to combine has overlapping submeshes (submeshes share vertices). If the UVs associated with the shared vertices are important then this bake may not work. If you are using multiple materials then this object can only be combined with objects that use the exact same set of textures (each atlas contains one texture). There may be other undesirable side affects as well. Mesh Master, available in the asset store can fix overlapping submeshes.");
				}
			}
		}
		if (mom is MB3_MeshBaker)
		{
			List<GameObject> objectsToCombine2 = mom.GetObjectsToCombine();
			if (objectsToCombine2 == null || objectsToCombine2.Count == 0)
			{
				Debug.LogError("No meshes to combine. Please assign some meshes to combine.");
				return false;
			}
			if (mom is MB3_MeshBaker && ((MB3_MeshBaker)mom).meshCombiner.renderType == MB_RenderType.skinnedMeshRenderer && !editorMethods.ValidateSkinnedMeshes(objectsToCombine2))
			{
				return false;
			}
		}
		if (editorMethods != null)
		{
			editorMethods.CheckPrefabTypes(objToCombineType, objectsToCombine);
		}
		return true;
	}

	// Token: 0x040000CC RID: 204
	public static bool DO_INTEGRITY_CHECKS;

	// Token: 0x040000CD RID: 205
	public Vector3 sortAxis;

	// Token: 0x02000046 RID: 70
	public class ZSortObjects
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00041CF4 File Offset: 0x0003FEF4
		public void SortByDistanceAlongAxis(List<GameObject> gos)
		{
			if (this.sortAxis == Vector3.zero)
			{
				Debug.LogError("The sort axis cannot be the zero vector.");
				return;
			}
			Debug.Log("Z sorting meshes along axis numObjs=" + gos.Count.ToString());
			List<MB3_MeshBakerRoot.ZSortObjects.Item> list = new List<MB3_MeshBakerRoot.ZSortObjects.Item>();
			Quaternion rotation = Quaternion.FromToRotation(this.sortAxis, Vector3.forward);
			for (int i = 0; i < gos.Count; i++)
			{
				if (gos[i] != null)
				{
					MB3_MeshBakerRoot.ZSortObjects.Item item = new MB3_MeshBakerRoot.ZSortObjects.Item();
					item.point = gos[i].transform.position;
					item.go = gos[i];
					item.point = rotation * item.point;
					list.Add(item);
				}
			}
			list.Sort(new MB3_MeshBakerRoot.ZSortObjects.ItemComparer());
			for (int j = 0; j < gos.Count; j++)
			{
				gos[j] = list[j].go;
			}
		}

		// Token: 0x040000CE RID: 206
		public Vector3 sortAxis;

		// Token: 0x02000047 RID: 71
		public class Item
		{
			// Token: 0x040000CF RID: 207
			public GameObject go;

			// Token: 0x040000D0 RID: 208
			public Vector3 point;
		}

		// Token: 0x02000048 RID: 72
		public class ItemComparer : IComparer<MB3_MeshBakerRoot.ZSortObjects.Item>
		{
			// Token: 0x06000138 RID: 312 RVA: 0x00002FDB File Offset: 0x000011DB
			public int Compare(MB3_MeshBakerRoot.ZSortObjects.Item a, MB3_MeshBakerRoot.ZSortObjects.Item b)
			{
				return (int)Mathf.Sign(b.point.z - a.point.z);
			}
		}
	}
}
