using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class LaserSight : MonoBehaviour
{
	// Token: 0x06000915 RID: 2325 RVA: 0x00007FF1 File Offset: 0x000061F1
	private void Start()
	{
		GameManager.SetupRecursiveLayer(this.beamTransform, 0);
		GameManager.SetupRecursiveLayer(this.dotObject.transform, 0);
		this.weapon = base.GetComponentInParent<Weapon>();
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0006A030 File Offset: 0x00068230
	private void LateUpdate()
	{
		if (!this.forceOn && (!(this.weapon != null) || !(this.weapon.user != null) || this.weapon.reloading || !this.weapon.user.controller.IsAlert() || this.weapon.user.controller.IsSprinting()))
		{
			this.beamTransform.localScale = new Vector3(1f, 1f, 0f);
			this.dotObject.gameObject.SetActive(false);
			return;
		}
		bool flag = this.weapon != null && this.weapon.user.aiControlled;
		if (flag)
		{
			base.transform.rotation = Quaternion.LookRotation(this.weapon.user.controller.FacingDirection());
		}
		Ray ray = new Ray(base.transform.position, base.transform.forward);
		ray.origin -= ray.direction * this.rayBackwardsOffset;
		float num = 500f;
		RaycastHit raycastHit;
		bool flag2 = Physics.Raycast(ray, out raycastHit, 500f, -12879877);
		if (flag2)
		{
			num = Vector3.Distance(GameManager.cachedIngameCameraPosition, raycastHit.point);
			this.dotObject.transform.position = raycastHit.point;
			float num2 = 1f + this.dotScaleGainPerMeter * num;
			if (flag)
			{
				num2 *= this.aiUserDotScaleMultiplier;
			}
			this.dotObject.transform.localScale = new Vector3(num2, num2, num2);
		}
		num = Mathf.Max(num - this.rayBackwardsOffset, 0f);
		this.dotObject.gameObject.SetActive(flag2 && num > 0f);
		this.beamTransform.localScale = new Vector3(1f, 1f, Mathf.Min(this.maxBeamLength, num));
	}

	// Token: 0x040009E3 RID: 2531
	private const int RAY_MASK = -12879877;

	// Token: 0x040009E4 RID: 2532
	private const float MAX_LENGTH = 500f;

	// Token: 0x040009E5 RID: 2533
	public float rayBackwardsOffset;

	// Token: 0x040009E6 RID: 2534
	public float maxBeamLength = 10f;

	// Token: 0x040009E7 RID: 2535
	public float dotScaleGainPerMeter = 0.05f;

	// Token: 0x040009E8 RID: 2536
	public float aiUserDotScaleMultiplier = 1.5f;

	// Token: 0x040009E9 RID: 2537
	public Transform beamTransform;

	// Token: 0x040009EA RID: 2538
	public GameObject dotObject;

	// Token: 0x040009EB RID: 2539
	public bool forceOn;

	// Token: 0x040009EC RID: 2540
	private Weapon weapon;
}
