using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
public class OwnerIndicator : MonoBehaviour
{
	// Token: 0x0600092D RID: 2349 RVA: 0x00008173 File Offset: 0x00006373
	private void Start()
	{
		this.renderer = base.GetComponent<QualitySwitcher>().activeObject.GetComponent<Renderer>();
		this.SetOwner(-1);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0006A594 File Offset: 0x00068794
	public void SetOwner(int team)
	{
		if (team == -1)
		{
			this.renderer.enabled = false;
			return;
		}
		this.renderer.enabled = true;
		this.renderer.material.color = Color.Lerp(ColorScheme.TeamColor(team), Color.black, 0.2f);
	}

	// Token: 0x04000A08 RID: 2568
	private Renderer renderer;
}
