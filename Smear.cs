using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class Smear : MonoBehaviour
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000098 RID: 152 RVA: 0x00002A25 File Offset: 0x00000C25
	// (set) Token: 0x06000099 RID: 153 RVA: 0x00002A2D File Offset: 0x00000C2D
	private Material InstancedMaterial
	{
		get
		{
			return this.m_instancedMaterial;
		}
		set
		{
			this.m_instancedMaterial = value;
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00002A36 File Offset: 0x00000C36
	private void Start()
	{
		this.InstancedMaterial = this.Renderer.material;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0003F678 File Offset: 0x0003D878
	private void LateUpdate()
	{
		if (this.m_recentPositions.Count > this.FramesBufferSize)
		{
			this.InstancedMaterial.SetVector("_PrevPosition", this.m_recentPositions.Dequeue());
		}
		this.InstancedMaterial.SetVector("_Position", base.transform.position);
		this.m_recentPositions.Enqueue(base.transform.position);
	}

	// Token: 0x0400005A RID: 90
	private Queue<Vector3> m_recentPositions = new Queue<Vector3>();

	// Token: 0x0400005B RID: 91
	public int FramesBufferSize;

	// Token: 0x0400005C RID: 92
	public Renderer Renderer;

	// Token: 0x0400005D RID: 93
	private Material m_instancedMaterial;
}
