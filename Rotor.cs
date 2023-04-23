using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000341 RID: 833
public class Rotor : MonoBehaviour
{
	// Token: 0x06001525 RID: 5413 RVA: 0x00010D54 File Offset: 0x0000EF54
	private void Awake()
	{
		this.engine = base.GetComponentInParent<Vehicle>().engine;
		this.applyRotorRenderer = (this.solidRotorRenderer != null && this.blurredRotorRenderer != null);
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x00099EB8 File Offset: 0x000980B8
	private void LateUpdate()
	{
		base.transform.localEulerAngles += this.rotationSpeed * this.engine.power * Time.deltaTime;
		if (this.applyRotorRenderer)
		{
			bool flag = this.engine.power > 0.8f;
			if (this.renderSolidRendererShadow)
			{
				this.solidRotorRenderer.shadowCastingMode = (flag ? ShadowCastingMode.ShadowsOnly : ShadowCastingMode.On);
				this.blurredRotorRenderer.enabled = flag;
				return;
			}
			this.solidRotorRenderer.enabled = !flag;
			this.blurredRotorRenderer.enabled = flag;
		}
	}

	// Token: 0x04001712 RID: 5906
	private Vehicle.Engine engine;

	// Token: 0x04001713 RID: 5907
	public Vector3 rotationSpeed = new Vector3(0f, 0f, 1000f);

	// Token: 0x04001714 RID: 5908
	public Renderer solidRotorRenderer;

	// Token: 0x04001715 RID: 5909
	public Renderer blurredRotorRenderer;

	// Token: 0x04001716 RID: 5910
	public bool renderSolidRendererShadow = true;

	// Token: 0x04001717 RID: 5911
	private bool applyRotorRenderer;
}
