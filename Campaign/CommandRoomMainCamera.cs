using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Campaign
{
	// Token: 0x020003E7 RID: 999
	public class CommandRoomMainCamera : MonoBehaviour
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x0001362B File Offset: 0x0001182B
		private void Awake()
		{
			CommandRoomMainCamera.instance = this;
			this.camera = base.GetComponent<Camera>();
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0001363F File Offset: 0x0001183F
		private void Start()
		{
			base.StartCoroutine(this.SetupDefaultTarget());
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0001364E File Offset: 0x0001184E
		private IEnumerator SetupDefaultTarget()
		{
			yield return new WaitForEndOfFrame();
			if (this.defaultTarget != null)
			{
				this.JumpToTarget(this.defaultTarget);
			}
			yield break;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000A7FA8 File Offset: 0x000A61A8
		private void Update()
		{
			Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
			bool flag = EventSystem.current.IsPointerOverGameObject();
			bool flag2 = false;
			RaycastHit raycastHit;
			if (!this.isLocked && !flag && !this.IsInCameraTransition() && Physics.Raycast(ray, out raycastHit, 9999f, 536870913) && raycastHit.collider.gameObject.layer == 29)
			{
				this.hoverRayPoint = raycastHit.point;
				CommandRoomClickable componentInParent = raycastHit.collider.gameObject.GetComponentInParent<CommandRoomClickable>();
				flag2 = (componentInParent != null && this.CanInteractWithClickableGroup(componentInParent.clickableGroup));
				if (componentInParent != null && componentInParent != this.hoverTarget)
				{
					if (this.hoverTarget != null)
					{
						this.hoverTarget.OnEndHover();
					}
					this.hoverTarget = componentInParent;
					this.hoverTarget.OnStartHover();
				}
			}
			if (!flag2 && this.hoverTarget != null)
			{
				this.hoverTarget.OnEndHover();
				this.hoverTarget = null;
			}
			if (!this.IsInCameraTransition() && !flag)
			{
				if (this.hoverTarget != null)
				{
					if (Input.GetMouseButtonDown(0))
					{
						this.hoverTarget.OnClick();
					}
					else if (Input.GetMouseButtonDown(1))
					{
						this.hoverTarget.OnRightClick();
					}
				}
				if (this.hoverTarget == null && Input.GetMouseButtonDown(1) && !CampaignBase.instance.OnNothingRightClicked())
				{
					this.GoBack();
				}
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0001365D File Offset: 0x0001185D
		private void GoBack()
		{
			if (this.target != null && this.target.backTarget != null)
			{
				this.TransitionToTarget(this.target.backTarget);
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000A812C File Offset: 0x000A632C
		private bool CanInteractWithClickableGroup(int group)
		{
			if (this.target == null)
			{
				return true;
			}
			for (int i = 0; i < this.target.clickableGroups.Length; i++)
			{
				if (this.target.clickableGroups[i] == group)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000A8174 File Offset: 0x000A6374
		private void LateUpdate()
		{
			if (this.target != null)
			{
				if (this.IsInCameraTransition())
				{
					float t = Mathf.SmoothStep(0f, 1f, this.transitionAction.Ratio());
					base.transform.position = Vector3.Lerp(this.transitionOriginPosition, this.target.transform.position, t);
					base.transform.rotation = Quaternion.Slerp(this.transitionOriginRotation, this.target.transform.rotation, t);
					this.camera.fieldOfView = Mathf.Lerp(this.transitionOriginFov, this.target.fieldOfView, t);
					return;
				}
				base.transform.position = this.target.transform.position;
				base.transform.rotation = this.target.transform.rotation;
				this.camera.fieldOfView = this.target.fieldOfView;
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00013691 File Offset: 0x00011891
		private bool IsInCameraTransition()
		{
			return !this.transitionAction.TrueDone();
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000A8278 File Offset: 0x000A6478
		private void SetTarget(CommandRoomCamera target)
		{
			if (this.target != null)
			{
				this.target.isActive = false;
			}
			this.target = target;
			target.OnActivated();
			this.target.isActive = true;
			if (this.target != null)
			{
				ClickableManager.OnClickableGroupsChanged(this.target.clickableGroups);
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000136A1 File Offset: 0x000118A1
		private void JumpToTarget(CommandRoomCamera target)
		{
			this.SetTarget(target);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000A82D8 File Offset: 0x000A64D8
		public void TransitionToTarget(CommandRoomCamera target)
		{
			this.SetTarget(target);
			this.transitionOriginPosition = base.transform.position;
			this.transitionOriginRotation = base.transform.rotation;
			this.transitionOriginFov = this.camera.fieldOfView;
			this.transitionAction.Start();
		}

		// Token: 0x04001AD6 RID: 6870
		public static CommandRoomMainCamera instance;

		// Token: 0x04001AD7 RID: 6871
		private const int RAYCAST_HIT_LAYER = 536870913;

		// Token: 0x04001AD8 RID: 6872
		private const float MOVE_CAMERA_DEADZONE = 0.3f;

		// Token: 0x04001AD9 RID: 6873
		public CommandRoomCamera defaultTarget;

		// Token: 0x04001ADA RID: 6874
		private CommandRoomClickable hoverTarget;

		// Token: 0x04001ADB RID: 6875
		[NonSerialized]
		public Camera camera;

		// Token: 0x04001ADC RID: 6876
		private CommandRoomCamera target;

		// Token: 0x04001ADD RID: 6877
		private Vector3 transitionOriginPosition;

		// Token: 0x04001ADE RID: 6878
		[NonSerialized]
		public Vector3 hoverRayPoint = Vector3.zero;

		// Token: 0x04001ADF RID: 6879
		private Quaternion transitionOriginRotation;

		// Token: 0x04001AE0 RID: 6880
		private float transitionOriginFov = 40f;

		// Token: 0x04001AE1 RID: 6881
		private TimedAction transitionAction = new TimedAction(0.5f, false);

		// Token: 0x04001AE2 RID: 6882
		[NonSerialized]
		public bool isLocked;
	}
}
