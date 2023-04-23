using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class Stairways : MonoBehaviour
{
	// Token: 0x040017DF RID: 6111
	[HideInInspector]
	public Vector3 target;

	// Token: 0x040017E0 RID: 6112
	public bool singleAxis = true;

	// Token: 0x040017E1 RID: 6113
	public float targetStepHeight = 0.2f;

	// Token: 0x040017E2 RID: 6114
	public float width = 2f;

	// Token: 0x040017E3 RID: 6115
	public float depth = 1f;

	// Token: 0x040017E4 RID: 6116
	public bool leftEdge = true;

	// Token: 0x040017E5 RID: 6117
	public bool rightEdge = true;

	// Token: 0x040017E6 RID: 6118
	public float edgeWidth = 0.2f;
}
