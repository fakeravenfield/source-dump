using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A5 RID: 677
public class Speedometer : MonoBehaviour
{
	// Token: 0x060011E5 RID: 4581 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0008D784 File Offset: 0x0008B984
	private void Update()
	{
		float num = this.target.transform.worldToLocalMatrix.MultiplyVector(this.target.velocity).z * this.multiplier;
		try
		{
			this.text.text = Mathf.Abs((int)num).ToString();
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x04001309 RID: 4873
	public Rigidbody target;

	// Token: 0x0400130A RID: 4874
	public float multiplier = 3.6f;

	// Token: 0x0400130B RID: 4875
	private Text text;
}
