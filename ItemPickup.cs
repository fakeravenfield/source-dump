using System;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class ItemPickup : MonoBehaviour
{
	// Token: 0x06001076 RID: 4214 RVA: 0x000894E0 File Offset: 0x000876E0
	private void Start()
	{
		this.velocity = UnityEngine.Random.insideUnitSphere.normalized.ToGround() * 2f;
		this.velocity.y = 4f;
		this.allowPickupAction.Start();
		this.moving = true;
		this.childTransform = base.transform.GetChild(0);
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00089544 File Offset: 0x00087744
	private void Update()
	{
		Vector3 b = FpsActorController.instance.actor.Position();
		Vector3 vector = base.transform.position - b;
		float magnitude = vector.ToGround().magnitude;
		if (this.allowPickupAction.TrueDone() && magnitude < 0.7f && vector.y < 1.5f && vector.y > -0.5f && this.Pickup())
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (this.moving)
		{
			this.UpdateMovement();
		}
		this.childTransform.localEulerAngles += new Vector3(0f, Time.deltaTime * 100f, 0f);
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0000257D File Offset: 0x0000077D
	protected virtual bool Pickup()
	{
		return false;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00089604 File Offset: 0x00087804
	private void UpdateMovement()
	{
		Vector3 vector = this.velocity * Time.deltaTime;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(base.transform.position, vector), out raycastHit, vector.magnitude, 1))
		{
			if (raycastHit.normal.y < 0.5f)
			{
				this.velocity = Vector3.Reflect(this.velocity, raycastHit.normal) * 0.6f;
				if (this.velocity.magnitude < 1f)
				{
					this.moving = false;
				}
			}
			else
			{
				this.moving = false;
			}
			base.transform.position = raycastHit.point + raycastHit.normal * 0.05f;
		}
		else
		{
			base.transform.position += vector;
		}
		this.velocity += Physics.gravity * 1.5f * Time.deltaTime;
	}

	// Token: 0x04001193 RID: 4499
	private const float PICKUP_RADIUS = 0.7f;

	// Token: 0x04001194 RID: 4500
	private const float ROTATION_SPEED = 100f;

	// Token: 0x04001195 RID: 4501
	private const int RAYCAST_MASK = 1;

	// Token: 0x04001196 RID: 4502
	private const float velocityUp = 4f;

	// Token: 0x04001197 RID: 4503
	private const float velocityRandom = 2f;

	// Token: 0x04001198 RID: 4504
	private Transform childTransform;

	// Token: 0x04001199 RID: 4505
	private bool moving;

	// Token: 0x0400119A RID: 4506
	private Vector3 velocity;

	// Token: 0x0400119B RID: 4507
	private TimedAction allowPickupAction = new TimedAction(1f, false);
}
