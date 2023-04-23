using System;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class SpinWhenHoldingFire : MonoBehaviour
{
	// Token: 0x0600095C RID: 2396 RVA: 0x0006ADC0 File Offset: 0x00068FC0
	private void Update()
	{
		try
		{
			this.spinSpeed = Mathf.MoveTowards(this.spinSpeed, (this.weapon.holdingFire && this.weapon.unholstered) ? 1f : 0f, this.spinUpRate * Time.deltaTime);
			Vector3 vector = base.transform.localEulerAngles;
			vector += this.rotation * this.spinSpeed * Time.deltaTime;
			base.transform.localEulerAngles = vector;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x04000A41 RID: 2625
	public Weapon weapon;

	// Token: 0x04000A42 RID: 2626
	public Vector3 rotation = new Vector3(0f, 0f, 1000f);

	// Token: 0x04000A43 RID: 2627
	public float spinUpRate = 1f;

	// Token: 0x04000A44 RID: 2628
	private float spinSpeed;
}
