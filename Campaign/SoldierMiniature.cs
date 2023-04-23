using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003E5 RID: 997
	public class SoldierMiniature : MonoBehaviour
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x000134D6 File Offset: 0x000116D6
		private void Awake()
		{
			this.renderer = base.GetComponent<Renderer>();
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000A7988 File Offset: 0x000A5B88
		public bool SetLevel(LevelClickable level)
		{
			LevelClickable levelClickable = this.level;
			if (level.AddSoldier(this))
			{
				if (levelClickable != null)
				{
					levelClickable.RemoveSoldier(this);
				}
				this.level = level;
				this.rootTransform = level.GetSoldierMiniatureRoot(this);
				this.positionOffset = Vector3.zero;
				this.rotationOffset = Quaternion.identity;
				return true;
			}
			return false;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000134E4 File Offset: 0x000116E4
		public void ResetTransform()
		{
			base.transform.position = this.rootTransform.position;
			base.transform.rotation = this.rootTransform.rotation;
			base.transform.localScale = Vector3.one;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000A79E4 File Offset: 0x000A5BE4
		public void TransitionToLevel(LevelClickable level, bool useArcTransition)
		{
			if (this.SetLevel(level))
			{
				this.transitionOriginPosition = base.transform.position;
				this.transitionOriginRotation = base.transform.rotation;
				this.positionOffset = Vector3.zero;
				this.rotationOffset = Quaternion.identity;
				this.transitionAction.Start();
				this.useArcTransition = useArcTransition;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000A7A44 File Offset: 0x000A5C44
		public void TransitionToRootTransform(Transform transform, bool useArcTransition)
		{
			this.transitionOriginPosition = base.transform.position;
			this.transitionOriginRotation = base.transform.rotation;
			this.positionOffset = Vector3.zero;
			this.rotationOffset = Quaternion.identity;
			this.transitionAction.Start();
			this.useArcTransition = useArcTransition;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00013522 File Offset: 0x00011722
		public void TransitionToLocalOffset(Vector3 localPosition, Quaternion localRotation)
		{
			this.transitionOriginPosition = base.transform.position;
			this.transitionOriginRotation = base.transform.rotation;
			this.positionOffset = localPosition;
			this.rotationOffset = localRotation;
			this.transitionAction.Start();
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0001355F File Offset: 0x0001175F
		public void SetRootTransform(Transform transform)
		{
			this.rootTransform = transform;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00013568 File Offset: 0x00011768
		public void PlayWakeUpAnimation()
		{
			this.wakeUpAction.StartLifetime(UnityEngine.Random.Range(0.2f, 0.4f));
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000A7A9C File Offset: 0x000A5C9C
		public void SetTapped(bool isTapped)
		{
			try
			{
				this.isTapped = isTapped;
				this.renderer.material = (isTapped ? this.tapMaterial : this.originalMaterial);
			}
			catch (Exception exception)
			{
				Debug.Log("Caught exception when writing soldier miniature material. Was the renderer destroyed unexpectedly?");
				Debug.LogException(exception);
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000A7AF0 File Offset: 0x000A5CF0
		private void LateUpdate()
		{
			Vector3 vector = this.rootTransform.position + this.rootTransform.rotation * this.positionOffset;
			Quaternion quaternion = this.rootTransform.rotation * this.rotationOffset;
			float num = 0.1f + Mathf.Sin(Time.time * 2.5f) * 0.02f;
			if (!this.pickupAction.TrueDone())
			{
				float num2 = this.pickupAction.Ratio();
				if (!this.isPickedUp)
				{
					num2 = 1f - num2;
				}
				vector.y += num2 * num;
			}
			else if (this.isPickedUp)
			{
				vector.y += num;
			}
			if (!this.dropAction.TrueDone())
			{
				float num3 = 1f * (1f - 2f * this.dropAction.Ratio());
				if (this.dropAction.Ratio() > 0.5f)
				{
					num3 = 0.12f * Mathf.Sin(3.1415927f * (2f * this.dropAction.Ratio() - 1f));
				}
				vector.y += num3;
			}
			if (!this.dieAction.TrueDone())
			{
				float num4 = Mathf.Clamp01(this.dieAction.Ratio() * 3f);
				float num5 = Mathf.Clamp01(this.dieAction.Ratio() * 1.3f - 0.3f);
				float t = Mathf.Clamp01(this.dieAction.Ratio() * 2f - 1f);
				quaternion = quaternion * Quaternion.Euler(num4 * 10f, 0f, num4 * 90f) * Quaternion.Euler(0f, -100f * num5 + -60f * Mathf.Sin(num5 * 3.1415927f), 0f);
				vector += this.rootTransform.forward * this.dieAction.Ratio() * 0.1f;
				vector.y += 0.05f * Mathf.Sin(num4 * 3.1415927f);
				float num6 = Mathf.SmoothStep(1f, 0f, t);
				base.transform.localScale = new Vector3(num6, num6, num6);
			}
			if (!this.wakeUpAction.TrueDone())
			{
				quaternion *= Quaternion.Euler(0f, 0f, 10f * (1f - this.wakeUpAction.Ratio()) * Mathf.Sin(this.wakeUpAction.Ratio() * 20f));
				vector.y += 0.05f * Mathf.Sin(this.wakeUpAction.Ratio() * 3.1415927f);
			}
			if (!this.transitionAction.TrueDone())
			{
				float t2 = Mathf.SmoothStep(0f, 1f, this.transitionAction.Ratio());
				Vector3 position = Vector3.Lerp(this.transitionOriginPosition, vector, t2);
				if (this.useArcTransition)
				{
					position.y += Mathf.Sin(this.transitionAction.Ratio() * 3.1415927f) * 0.25f;
				}
				base.transform.position = position;
				base.transform.rotation = Quaternion.Slerp(this.transitionOriginRotation, quaternion, t2);
				return;
			}
			base.transform.position = vector;
			base.transform.rotation = quaternion;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00013584 File Offset: 0x00011784
		public void PickUp()
		{
			this.pickupAction.Start();
			this.isPickedUp = true;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00013598 File Offset: 0x00011798
		public void SetDown()
		{
			this.pickupAction.Start();
			this.isPickedUp = false;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000135AC File Offset: 0x000117AC
		public void PlayDropAnimation(float duration)
		{
			this.dropAction.StartLifetime(duration);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000135BA File Offset: 0x000117BA
		public void SetTeam(int team)
		{
			this.team = team;
			base.GetComponent<MeshRenderer>().material = ColorScheme.GetActorMaterial(team);
			this.originalMaterial = this.renderer.material;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000A7E64 File Offset: 0x000A6064
		public void Die()
		{
			if (this.level != null)
			{
				this.level.RemoveSoldier(this);
			}
			float num = UnityEngine.Random.Range(0.9f, 1.2f);
			this.dieAction.StartLifetime(num);
			base.Invoke("Cleanup", num);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000A7EB4 File Offset: 0x000A60B4
		public void PlayDeathAnimation()
		{
			float lifetime = UnityEngine.Random.Range(0.9f, 1.2f);
			this.dieAction.StartLifetime(lifetime);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0000684B File Offset: 0x00004A4B
		private void Cleanup()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04001AB6 RID: 6838
		public const int LAYER = 0;

		// Token: 0x04001AB7 RID: 6839
		private const float PICK_UP_FLOAT_HEIGHT = 0.1f;

		// Token: 0x04001AB8 RID: 6840
		private const float PICK_UP_FLOAT_WOBBLE_HEIGHT = 0.02f;

		// Token: 0x04001AB9 RID: 6841
		private const float PICK_UP_FLOAT_WOBBLE_FREQUENCY = 2.5f;

		// Token: 0x04001ABA RID: 6842
		private const float TRANSITION_ARC_HEIGHT = 0.25f;

		// Token: 0x04001ABB RID: 6843
		private const float DROP_HEIGHT = 1f;

		// Token: 0x04001ABC RID: 6844
		private const float DROP_BOUNCE_HEIGHT = 0.12f;

		// Token: 0x04001ABD RID: 6845
		public LevelClickable level;

		// Token: 0x04001ABE RID: 6846
		public Transform rootTransform;

		// Token: 0x04001ABF RID: 6847
		public Material tapMaterial;

		// Token: 0x04001AC0 RID: 6848
		private Material originalMaterial;

		// Token: 0x04001AC1 RID: 6849
		[NonSerialized]
		public int team;

		// Token: 0x04001AC2 RID: 6850
		private Vector3 transitionOriginPosition;

		// Token: 0x04001AC3 RID: 6851
		private Quaternion transitionOriginRotation;

		// Token: 0x04001AC4 RID: 6852
		private TimedAction transitionAction = new TimedAction(0.5f, false);

		// Token: 0x04001AC5 RID: 6853
		private bool useArcTransition;

		// Token: 0x04001AC6 RID: 6854
		private Vector3 positionOffset = Vector3.zero;

		// Token: 0x04001AC7 RID: 6855
		private Quaternion rotationOffset = Quaternion.identity;

		// Token: 0x04001AC8 RID: 6856
		private TimedAction pickupAction = new TimedAction(0.1f, false);

		// Token: 0x04001AC9 RID: 6857
		private TimedAction dropAction = new TimedAction(0.5f, false);

		// Token: 0x04001ACA RID: 6858
		private TimedAction wakeUpAction = new TimedAction(0.3f, false);

		// Token: 0x04001ACB RID: 6859
		private TimedAction dieAction = new TimedAction(1f, false);

		// Token: 0x04001ACC RID: 6860
		[NonSerialized]
		public bool isPickedUp;

		// Token: 0x04001ACD RID: 6861
		private Renderer renderer;

		// Token: 0x04001ACE RID: 6862
		public bool isTapped;
	}
}
