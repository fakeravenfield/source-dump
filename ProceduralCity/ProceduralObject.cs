using System;
using UnityEngine;

namespace ProceduralCity
{
	// Token: 0x0200036C RID: 876
	public class ProceduralObject : MonoBehaviour
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x000118BC File Offset: 0x0000FABC
		public virtual void Generate()
		{
			Debug.Log("Generate " + this.ToString());
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000A0168 File Offset: 0x0009E368
		public virtual void ApplyMesh(Mesh mesh, params Material[] materials)
		{
			Renderer component = base.GetComponent<Renderer>();
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			MeshCollider component3 = base.GetComponent<MeshCollider>();
			if (component2 != null)
			{
				component2.mesh = mesh;
			}
			if (component3 != null)
			{
				component3.sharedMesh = mesh;
			}
			if (component != null)
			{
				component.materials = materials;
			}
		}
	}
}
