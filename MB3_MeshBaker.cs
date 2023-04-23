using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class MB3_MeshBaker : MB3_MeshBakerCommon
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600010E RID: 270 RVA: 0x00002E1D File Offset: 0x0000101D
	public override MB3_MeshCombiner meshCombiner
	{
		get
		{
			return this._meshCombiner;
		}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00002E25 File Offset: 0x00001025
	public void BuildSceneMeshObject()
	{
		this._meshCombiner.BuildSceneMeshObject(null, false);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00002E34 File Offset: 0x00001034
	public virtual bool ShowHide(GameObject[] gos, GameObject[] deleteGOs)
	{
		return this._meshCombiner.ShowHideGameObjects(gos, deleteGOs);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00002E43 File Offset: 0x00001043
	public virtual void ApplyShowHide()
	{
		this._meshCombiner.ApplyShowHide();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00002E50 File Offset: 0x00001050
	public override bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource)
	{
		this._meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjects(gos, deleteGOs, disableRendererInSource);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00002E7B File Offset: 0x0000107B
	public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource)
	{
		this._meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjectsByID(gos, deleteGOinstanceIDs, disableRendererInSource);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00002EA6 File Offset: 0x000010A6
	public void OnDestroy()
	{
		this._meshCombiner.DisposeRuntimeCreated();
	}

	// Token: 0x040000BD RID: 189
	[SerializeField]
	protected MB3_MeshCombinerSingle _meshCombiner = new MB3_MeshCombinerSingle();
}
