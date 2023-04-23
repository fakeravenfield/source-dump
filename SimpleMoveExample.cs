using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class SimpleMoveExample : MonoBehaviour
{
	// Token: 0x06000095 RID: 149 RVA: 0x000029C3 File Offset: 0x00000BC3
	private void Start()
	{
		this.m_originalPosition = base.transform.position;
		this.m_previous = base.transform.position;
		this.m_target = base.transform.position;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0003F51C File Offset: 0x0003D71C
	private void Update()
	{
		base.transform.position = Vector3.Slerp(this.m_previous, this.m_target, Time.deltaTime * this.Speed);
		this.m_previous = base.transform.position;
		if (Vector3.Distance(this.m_target, base.transform.position) < 0.1f)
		{
			this.m_target = base.transform.position + UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(0.7f, 4f);
			this.m_target.Set(Mathf.Clamp(this.m_target.x, this.m_originalPosition.x - this.BoundingVolume.x, this.m_originalPosition.x + this.BoundingVolume.x), Mathf.Clamp(this.m_target.y, this.m_originalPosition.y - this.BoundingVolume.y, this.m_originalPosition.y + this.BoundingVolume.y), Mathf.Clamp(this.m_target.z, this.m_originalPosition.z - this.BoundingVolume.z, this.m_originalPosition.z + this.BoundingVolume.z));
		}
	}

	// Token: 0x04000055 RID: 85
	private Vector3 m_previous;

	// Token: 0x04000056 RID: 86
	private Vector3 m_target;

	// Token: 0x04000057 RID: 87
	private Vector3 m_originalPosition;

	// Token: 0x04000058 RID: 88
	public Vector3 BoundingVolume = new Vector3(3f, 1f, 3f);

	// Token: 0x04000059 RID: 89
	public float Speed = 10f;
}
