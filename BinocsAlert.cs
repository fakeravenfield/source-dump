using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class BinocsAlert : MonoBehaviour
{
	// Token: 0x060008B2 RID: 2226 RVA: 0x00007AFA File Offset: 0x00005CFA
	private void Start()
	{
		this.lifetime.Start();
		this.material = base.GetComponentInChildren<Renderer>().sharedMaterial;
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x00068C14 File Offset: 0x00066E14
	private void Update()
	{
		if (this.lifetime.TrueDone())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.material.SetColor("_TintColor", Color.Lerp(Color.black, this.color, Mathf.Clamp01(2f - 2f * this.lifetime.Ratio())));
	}

	// Token: 0x0400094F RID: 2383
	public Color color;

	// Token: 0x04000950 RID: 2384
	private TimedAction lifetime = new TimedAction(1.3f, false);

	// Token: 0x04000951 RID: 2385
	private Material material;
}
