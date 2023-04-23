using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class MB2_TestUpdate : MonoBehaviour
{
	// Token: 0x06000183 RID: 387 RVA: 0x00042A88 File Offset: 0x00040C88
	private void Start()
	{
		this.meshbaker.AddDeleteGameObjects(this.objsToMove, null, true);
		this.meshbaker.AddDeleteGameObjects(new GameObject[]
		{
			this.objWithChangingUVs
		}, null, true);
		MeshFilter component = this.objWithChangingUVs.GetComponent<MeshFilter>();
		this.m = component.sharedMesh;
		this.uvs = this.m.uv;
		this.meshbaker.Apply(null);
		this.multiMeshBaker.AddDeleteGameObjects(this.objsToMove, null, true);
		this.multiMeshBaker.AddDeleteGameObjects(new GameObject[]
		{
			this.objWithChangingUVs
		}, null, true);
		component = this.objWithChangingUVs.GetComponent<MeshFilter>();
		this.m = component.sharedMesh;
		this.uvs = this.m.uv;
		this.multiMeshBaker.Apply(null);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00042B64 File Offset: 0x00040D64
	private void LateUpdate()
	{
		this.meshbaker.UpdateGameObjects(this.objsToMove, false, true, true, true, false, false, false, false, false);
		Vector2[] uv = this.m.uv;
		for (int i = 0; i < uv.Length; i++)
		{
			uv[i] = Mathf.Sin(Time.time) * this.uvs[i];
		}
		this.m.uv = uv;
		this.meshbaker.UpdateGameObjects(new GameObject[]
		{
			this.objWithChangingUVs
		}, true, true, true, true, true, false, false, false, false);
		this.meshbaker.Apply(false, true, true, true, true, false, false, false, false, false, false, null);
		this.multiMeshBaker.UpdateGameObjects(this.objsToMove, false, true, true, true, false, false, false, false, false);
		uv = this.m.uv;
		for (int j = 0; j < uv.Length; j++)
		{
			uv[j] = Mathf.Sin(Time.time) * this.uvs[j];
		}
		this.m.uv = uv;
		this.multiMeshBaker.UpdateGameObjects(new GameObject[]
		{
			this.objWithChangingUVs
		}, true, true, true, true, true, false, false, false, false);
		this.multiMeshBaker.Apply(false, true, true, true, true, false, false, false, false, false, false, null);
	}

	// Token: 0x040000FC RID: 252
	public MB3_MeshBaker meshbaker;

	// Token: 0x040000FD RID: 253
	public MB3_MultiMeshBaker multiMeshBaker;

	// Token: 0x040000FE RID: 254
	public GameObject[] objsToMove;

	// Token: 0x040000FF RID: 255
	public GameObject objWithChangingUVs;

	// Token: 0x04000100 RID: 256
	private Vector2[] uvs;

	// Token: 0x04000101 RID: 257
	private Mesh m;
}
