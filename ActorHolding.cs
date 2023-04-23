using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class ActorHolding : MonoBehaviour
{
	// Token: 0x06001124 RID: 4388 RVA: 0x0008B85C File Offset: 0x00089A5C
	private void Start()
	{
		Weapon weapon = UnityEngine.Object.FindObjectOfType<Weapon>();
		weapon.CullFpsObjects();
		weapon.transform.parent = this.parent;
		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.localRotation = Quaternion.identity;
		weapon.thirdPersonTransform.localPosition = weapon.thirdPersonOffset;
		weapon.thirdPersonTransform.localScale = Vector3.one * weapon.thirdPersonScale;
		weapon.thirdPersonTransform.localEulerAngles = weapon.thirdPersonRotation;
		base.GetComponent<Animator>().SetFloat("pose type", (float)weapon.configuration.pose);
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Update()
	{
	}

	// Token: 0x0400126D RID: 4717
	public Transform parent;
}
