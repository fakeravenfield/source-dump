using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class MB3_MultiMeshBaker : MB3_MeshBakerCommon
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x0600013A RID: 314 RVA: 0x00002FFA File Offset: 0x000011FA
	public override MB3_MeshCombiner meshCombiner
	{
		get
		{
			return this._meshCombiner;
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00041DF0 File Offset: 0x0003FFF0
	public override bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource)
	{
		if (this._meshCombiner.resultSceneObject == null)
		{
			this._meshCombiner.resultSceneObject = new GameObject("CombinedMesh-" + base.name);
		}
		this.meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjects(gos, deleteGOs, disableRendererInSource);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00041E5C File Offset: 0x0004005C
	public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOs, bool disableRendererInSource)
	{
		if (this._meshCombiner.resultSceneObject == null)
		{
			this._meshCombiner.resultSceneObject = new GameObject("CombinedMesh-" + base.name);
		}
		this.meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjectsByID(gos, deleteGOs, disableRendererInSource);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00003002 File Offset: 0x00001202
	public void OnDestroy()
	{
		this._meshCombiner.DisposeRuntimeCreated();
	}

	// Token: 0x040000D1 RID: 209
	[SerializeField]
	protected MB3_MultiMeshCombiner _meshCombiner = new MB3_MultiMeshCombiner();
}
