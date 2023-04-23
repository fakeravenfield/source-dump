using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class MB3_MeshBakerGrouper : MonoBehaviour
{
	// Token: 0x0600012C RID: 300 RVA: 0x000419B8 File Offset: 0x0003FBB8
	private void OnDrawGizmosSelected()
	{
		if (this.grouper == null)
		{
			this.grouper = this.CreateGrouper(this.clusterType, this.data);
		}
		if (this.grouper.d == null)
		{
			this.grouper.d = this.data;
		}
		this.grouper.DrawGizmos(this.sourceObjectBounds);
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00041A14 File Offset: 0x0003FC14
	public MB3_MeshBakerGrouperCore CreateGrouper(MB3_MeshBakerGrouper.ClusterType t, GrouperData data)
	{
		if (t == MB3_MeshBakerGrouper.ClusterType.grid)
		{
			this.grouper = new MB3_MeshBakerGrouperGrid(data);
		}
		if (t == MB3_MeshBakerGrouper.ClusterType.pie)
		{
			this.grouper = new MB3_MeshBakerGrouperPie(data);
		}
		if (t == MB3_MeshBakerGrouper.ClusterType.agglomerative)
		{
			MB3_TextureBaker component = base.GetComponent<MB3_TextureBaker>();
			List<GameObject> gos;
			if (component != null)
			{
				gos = component.GetObjectsToCombine();
			}
			else
			{
				gos = new List<GameObject>();
			}
			this.grouper = new MB3_MeshBakerGrouperCluster(data, gos);
		}
		if (t == MB3_MeshBakerGrouper.ClusterType.none)
		{
			this.grouper = new MB3_MeshBakerGrouperNone(data);
		}
		return this.grouper;
	}

	// Token: 0x040000C3 RID: 195
	public MB3_MeshBakerGrouperCore grouper;

	// Token: 0x040000C4 RID: 196
	public MB3_MeshBakerGrouper.ClusterType clusterType;

	// Token: 0x040000C5 RID: 197
	public GrouperData data = new GrouperData();

	// Token: 0x040000C6 RID: 198
	[HideInInspector]
	public Bounds sourceObjectBounds = new Bounds(Vector3.zero, Vector3.one);

	// Token: 0x02000044 RID: 68
	public enum ClusterType
	{
		// Token: 0x040000C8 RID: 200
		none,
		// Token: 0x040000C9 RID: 201
		grid,
		// Token: 0x040000CA RID: 202
		pie,
		// Token: 0x040000CB RID: 203
		agglomerative
	}
}
