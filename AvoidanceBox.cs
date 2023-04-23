using System;
using UnityEngine;

// Token: 0x02000241 RID: 577
public class AvoidanceBox : MonoBehaviour
{
	// Token: 0x06001006 RID: 4102 RVA: 0x000869BC File Offset: 0x00084BBC
	public bool Contains(Vector3 position)
	{
		Vector3 vector = base.transform.worldToLocalMatrix.MultiplyPoint(position);
		return vector.x > -0.5f && vector.x < 0.5f && vector.y > -0.5f && vector.y < 0.5f && vector.z > -0.5f && vector.z < 0.5f;
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00086A30 File Offset: 0x00084C30
	private void Awake()
	{
		if (!GameManager.instance.generateNavCache)
		{
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}

	// Token: 0x040010D1 RID: 4305
	public bool applyToAllTypes = true;

	// Token: 0x040010D2 RID: 4306
	public PathfindingBox.Type type;

	// Token: 0x040010D3 RID: 4307
	public uint penalty = 10000U;

	// Token: 0x040010D4 RID: 4308
	public bool unwalkable;
}
