using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001E RID: 30
public class CameraScroll : MonoBehaviour
{
	// Token: 0x0600009D RID: 157 RVA: 0x00002A5C File Offset: 0x00000C5C
	private void Start()
	{
		this.speedSlider.onValueChanged.AddListener(delegate(float <p0>)
		{
			this.ChangeSpeed();
		});
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
	private void Update()
	{
		base.transform.Translate(Vector3.left * (Time.deltaTime * this.moveSpeed));
		if (base.transform.position.x > 112f)
		{
			base.transform.position = new Vector3(0f, base.transform.position.y, base.transform.position.z);
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00002A7A File Offset: 0x00000C7A
	private void ChangeSpeed()
	{
		this.moveSpeed = this.speedSlider.value;
	}

	// Token: 0x0400005E RID: 94
	public Slider speedSlider;

	// Token: 0x0400005F RID: 95
	public float moveSpeed = 0.5f;
}
