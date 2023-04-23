using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class BombCamera : MonoBehaviour
{
	// Token: 0x060014DB RID: 5339 RVA: 0x00010A2E File Offset: 0x0000EC2E
	private void Awake()
	{
		this.vehicle = base.GetComponentInParent<Vehicle>();
		this.heightFilter = new MeanFilter(32);
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x00098E00 File Offset: 0x00097000
	private void Update()
	{
		try
		{
			if (!this.lockTargetWhenAiming || !FpsActorController.instance.Aiming())
			{
				Ray ray = new Ray(base.transform.position, base.transform.forward);
				float input = base.transform.position.y - WaterLevel.GetHeight();
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 1000f, 1))
				{
					input = base.transform.position.y - raycastHit.point.y;
				}
				float num = this.heightFilter.Tick(input);
				float y = Physics.gravity.y;
				Vector3 vector = this.vehicle.Velocity() + this.weapon.CurrentMuzzle().forward * this.weapon.projectileSpeed;
				float y2 = vector.y;
				float d = -(y2 / y) + Mathf.Sqrt(Mathf.Pow(y2 / y, 2f) - 2f * num / y);
				this.lookTarget = base.transform.position + vector.ToGround() * d - new Vector3(0f, num, 0f);
			}
			if ((this.lookTarget - base.transform.position).sqrMagnitude > 0.0001f)
			{
				Quaternion rotation = Quaternion.LookRotation(this.lookTarget - base.transform.position, this.vehicle.transform.up);
				base.transform.rotation = rotation;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x040016A6 RID: 5798
	private const float RAY_MAX_DISTANCE = 1000f;

	// Token: 0x040016A7 RID: 5799
	private const int RAY_MASK = 1;

	// Token: 0x040016A8 RID: 5800
	public Weapon weapon;

	// Token: 0x040016A9 RID: 5801
	public bool lockTargetWhenAiming = true;

	// Token: 0x040016AA RID: 5802
	private Vehicle vehicle;

	// Token: 0x040016AB RID: 5803
	private MeanFilter heightFilter;

	// Token: 0x040016AC RID: 5804
	private Vector3 lookTarget;
}
