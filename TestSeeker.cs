using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class TestSeeker : MonoBehaviour
{
	// Token: 0x0600115B RID: 4443 RVA: 0x0000DA54 File Offset: 0x0000BC54
	private void Start()
	{
		base.Invoke("Seek", 0.1f);
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0008BC28 File Offset: 0x00089E28
	private void Seek()
	{
		Seeker component = base.GetComponent<Seeker>();
		if (this.anyGraph)
		{
			component.StartPath(base.transform.position, this.target.position);
			return;
		}
		int mask;
		if (this.type == PathfindingBox.Type.Boat)
		{
			mask = PathfindingManager.boatGraphMask;
		}
		else if (this.type == PathfindingBox.Type.Car)
		{
			mask = PathfindingManager.carGraphMask;
		}
		else
		{
			mask = PathfindingManager.infantryGraphMask;
		}
		Debug.Log("Calculating path, mask: " + mask.ToString());
		component.StartPath(base.transform.position, this.target.position, null, mask);
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Update()
	{
	}

	// Token: 0x04001281 RID: 4737
	public bool anyGraph = true;

	// Token: 0x04001282 RID: 4738
	public PathfindingBox.Type type;

	// Token: 0x04001283 RID: 4739
	public Transform target;
}
