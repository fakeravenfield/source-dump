using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class ProjectilePool
{
	// Token: 0x06000739 RID: 1849 RVA: 0x00006A57 File Offset: 0x00004C57
	public ProjectilePool(GameObject prefab)
	{
		this.prefab = prefab;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00061EA4 File Offset: 0x000600A4
	public void Initialize(int count)
	{
		this.inactiveProjectiles = new Stack<Projectile>(count);
		int instanceID = this.prefab.GetInstanceID();
		for (int i = 0; i < count; i++)
		{
			Projectile component = UnityEngine.Object.Instantiate<GameObject>(this.prefab).GetComponent<Projectile>();
			component.prefabInstanceId = instanceID;
			component.gameObject.SetActive(false);
			component.gameObject.hideFlags = HideFlags.HideInHierarchy;
			this.inactiveProjectiles.Push(component);
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00061F14 File Offset: 0x00060114
	public Projectile Instantiate(Vector3 position, Quaternion rotation)
	{
		Projectile projectile;
		if (this.inactiveProjectiles.Count > 0)
		{
			projectile = this.inactiveProjectiles.Pop();
			projectile.transform.position = position;
			projectile.transform.rotation = rotation;
			projectile.gameObject.SetActive(true);
		}
		else
		{
			projectile = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation).GetComponent<Projectile>();
			projectile.prefabInstanceId = this.prefab.GetInstanceID();
		}
		projectile.ResetFields();
		return projectile;
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00006A66 File Offset: 0x00004C66
	public void ReturnToPool(Projectile projectile)
	{
		projectile.gameObject.SetActive(false);
		this.inactiveProjectiles.Push(projectile);
	}

	// Token: 0x04000742 RID: 1858
	private GameObject prefab;

	// Token: 0x04000743 RID: 1859
	private Stack<Projectile> inactiveProjectiles;
}
