using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class Ladder : PathfindingLink
{
	// Token: 0x06000BDE RID: 3038 RVA: 0x00009C63 File Offset: 0x00007E63
	private void Start()
	{
		base.Invoke("Register", 0.5f);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00009C75 File Offset: 0x00007E75
	private void Register()
	{
		ActorManager.RegisterLadder(this);
		if (!PathfindingManager.FindLadderNodes(this))
		{
			Debug.Log("No pathfinding nodes found for ladder " + base.name + ", is pathfinding navmesh outdated?");
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00009C9F File Offset: 0x00007E9F
	public float GetProjectedHeightAtPosition(Vector3 position)
	{
		return Mathf.Clamp(SMath.ProjectScalar(position - base.transform.position, base.transform.up), 0f, this.height);
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00009CD2 File Offset: 0x00007ED2
	public Vector3 GetClosestPositionTo(Vector3 position)
	{
		return this.GetPositionAtHeight(this.GetProjectedHeightAtPosition(position));
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00009CE1 File Offset: 0x00007EE1
	public Vector3 GetPositionAtHeight(float height)
	{
		return base.transform.position + base.transform.up * height;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00004EA0 File Offset: 0x000030A0
	public Vector3 GetBottomPosition()
	{
		return base.transform.position;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00009D04 File Offset: 0x00007F04
	public Vector3 GetTopPosition()
	{
		return base.transform.position + base.transform.up * this.height;
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00077250 File Offset: 0x00075450
	public Vector3 GetBottomExitPosition()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(this.bottomExitDirection);
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00077278 File Offset: 0x00075478
	public Vector3 GetTopExitPosition()
	{
		return this.GetTopPosition() + base.transform.localToWorldMatrix.MultiplyVector(this.topExitDirection);
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsOk()
	{
		return true;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00009D2C File Offset: 0x00007F2C
	public override Vector3 StartPosition()
	{
		return this.GetBottomExitPosition();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00009D34 File Offset: 0x00007F34
	public override Vector3 EndPosition()
	{
		return this.GetTopExitPosition();
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x00009D3C File Offset: 0x00007F3C
	public override void OnNodeCreated(PointNode node)
	{
		node.Tag = 31U;
	}

	// Token: 0x04000CF4 RID: 3316
	public float height = 10f;

	// Token: 0x04000CF5 RID: 3317
	public Vector3 bottomExitDirection = new Vector3(0f, 0f, -1f);

	// Token: 0x04000CF6 RID: 3318
	public Vector3 topExitDirection = Vector3.forward;

	// Token: 0x04000CF7 RID: 3319
	[NonSerialized]
	public GraphNode bottomNode;

	// Token: 0x04000CF8 RID: 3320
	[NonSerialized]
	public GraphNode topNode;
}
