using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DA RID: 730
public class SquadPointUi : MonoBehaviour
{
	// Token: 0x0600134D RID: 4941 RVA: 0x0009263C File Offset: 0x0009083C
	public static void Register(SquadOrderPoint point)
	{
		SquadPointUi.instance.points.Add(point);
		point.image.transform.SetParent(SquadPointUi.instance.pointOrderContainer);
		point.image.rectTransform.anchorMin = Vector2.zero;
		point.image.rectTransform.anchorMax = Vector2.zero;
		point.image.rectTransform.localScale = Vector3.one;
		point.image.rectTransform.rotation = Quaternion.identity;
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x0000F613 File Offset: 0x0000D813
	public static void Unregister(SquadOrderPoint point)
	{
		if (SquadPointUi.instance)
		{
			SquadPointUi.instance.points.Remove(point);
		}
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x0000F632 File Offset: 0x0000D832
	public static SquadOrderPoint GetAimingAtPoint()
	{
		return SquadPointUi.instance.aimPoint;
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x0000F63E File Offset: 0x0000D83E
	public static Actor GetAttackTarget()
	{
		return SquadPointUi.instance.aimActor;
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x000926C8 File Offset: 0x000908C8
	public static void Show(SquadPointUi.SquadPointOrderBaseType type)
	{
		SquadPointUi.instance.type = type;
		SquadPointUi.instance.pointOrderContainer.gameObject.SetActive(type == SquadPointUi.SquadPointOrderBaseType.PointOrder);
		SquadPointUi.instance.targetOrderContainer.gameObject.SetActive(type == SquadPointUi.SquadPointOrderBaseType.TargetOrder);
		SquadPointUi.instance.pointSelector.SetActive(type == SquadPointUi.SquadPointOrderBaseType.PointOrder);
		SquadPointUi.instance.targetSelector.SetActive(type == SquadPointUi.SquadPointOrderBaseType.TargetOrder);
		if (!SquadPointUi.instance.canvas.enabled)
		{
			SquadPointUi.instance.openAction.Start();
			SquadPointUi.PlayShowUi();
			if (SquadPointUi.instance.type == SquadPointUi.SquadPointOrderBaseType.PointOrder)
			{
				foreach (SquadOrderPoint squadOrderPoint in SquadPointUi.instance.points)
				{
					squadOrderPoint.image.enabled = true;
				}
			}
		}
		SquadPointUi.instance.closeWhenFlashDone = false;
		SquadPointUi.instance.canvas.enabled = true;
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x000927D0 File Offset: 0x000909D0
	public static void Hide(bool flashLastPoint)
	{
		if (flashLastPoint)
		{
			if (SquadPointUi.instance.type == SquadPointUi.SquadPointOrderBaseType.PointOrder)
			{
				foreach (SquadOrderPoint squadOrderPoint in SquadPointUi.instance.points)
				{
					squadOrderPoint.image.enabled = (squadOrderPoint == SquadPointUi.instance.aimPoint);
				}
			}
			SquadPointUi.instance.flashAction.Start();
			SquadPointUi.instance.closeWhenFlashDone = true;
			return;
		}
		SquadPointUi.instance.canvas.enabled = false;
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x0000F64A File Offset: 0x0000D84A
	public static void SetDefaultText(string text)
	{
		SquadPointUi.instance.defaultText = text;
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x0000F657 File Offset: 0x0000D857
	public static void PlayCommitOrderQuick()
	{
		SquadPointUi.instance.audio.PlayOneShot(SquadPointUi.instance.orderQuickUiSound);
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x0000F672 File Offset: 0x0000D872
	public static void PlayCommitOrder()
	{
		SquadPointUi.instance.audio.PlayOneShot(SquadPointUi.instance.orderUiSound);
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x0000F68D File Offset: 0x0000D88D
	public static void PlayShowUi()
	{
		SquadPointUi.instance.audio.PlayOneShot(SquadPointUi.instance.openUiSound);
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
	public static void PlayCancelOrderUi()
	{
		SquadPointUi.instance.audio.PlayOneShot(SquadPointUi.instance.cancelOrderUiSound);
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x00092878 File Offset: 0x00090A78
	private void Awake()
	{
		SquadPointUi.instance = this;
		this.points = new List<SquadOrderPoint>();
		this.canvas = base.GetComponent<Canvas>();
		this.text = base.GetComponentInChildren<Text>();
		this.audio = base.GetComponent<AudioSource>();
		this.canvas.enabled = false;
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x000928C8 File Offset: 0x00090AC8
	private void LateUpdate()
	{
		if (!this.canvas.enabled)
		{
			return;
		}
		float scaleFactor = 1f;
		if (!this.flashAction.TrueDone())
		{
			scaleFactor = Mathf.Clamp01(0.5f + this.flashAction.Ratio() * 3f);
		}
		else
		{
			if (this.closeWhenFlashDone)
			{
				this.canvas.enabled = false;
				return;
			}
			if (!this.openAction.TrueDone())
			{
				scaleFactor = this.openAction.Ratio();
			}
		}
		if (this.type == SquadPointUi.SquadPointOrderBaseType.PointOrder)
		{
			this.UpdatePointsUi(scaleFactor);
			return;
		}
		if (this.type == SquadPointUi.SquadPointOrderBaseType.TargetOrder)
		{
			this.UpdateTargetUi(scaleFactor);
		}
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x00092964 File Offset: 0x00090B64
	private void UpdatePointsUi(float scaleFactor)
	{
		if (this.flashAction.TrueDone())
		{
			this.aimPoint = null;
		}
		float num = 0.95f;
		Camera activeCamera = FpsActorController.instance.GetActiveCamera();
		foreach (SquadOrderPoint squadOrderPoint in this.points)
		{
			if (this.flashAction.TrueDone() || !(squadOrderPoint != this.aimPoint))
			{
				if (!squadOrderPoint.isActiveAndEnabled)
				{
					squadOrderPoint.image.enabled = false;
				}
				else
				{
					Vector3 vector = squadOrderPoint.transform.position - activeCamera.transform.position;
					float magnitude = vector.magnitude;
					if (magnitude < squadOrderPoint.maxIssueDistance)
					{
						float num2 = scaleFactor * Mathf.Lerp(1f, 0.6f, (magnitude - 5f) / 30f);
						float num3 = Vector3.Dot(vector.normalized, activeCamera.transform.forward);
						squadOrderPoint.image.enabled = (num3 > 0f);
						squadOrderPoint.image.rectTransform.anchoredPosition3D = activeCamera.WorldToScreenPoint(squadOrderPoint.transform.position);
						squadOrderPoint.image.rectTransform.localScale = new Vector3(num2, num2, num2);
						if (num3 > num)
						{
							this.aimPoint = squadOrderPoint;
							num = num3;
						}
					}
					else
					{
						squadOrderPoint.image.enabled = false;
					}
				}
			}
		}
		this.pointSelector.SetActive(this.aimPoint != null);
		this.selectorLabel.SetActive(true);
		this.selector.anchorMin = new Vector2(0f, 0f);
		this.selector.anchorMax = new Vector2(0f, 0f);
		this.selector.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50f);
		if (this.aimPoint != null)
		{
			this.selector.anchoredPosition = this.aimPoint.image.rectTransform.anchoredPosition;
			this.selector.localScale = this.aimPoint.image.rectTransform.localScale;
			if (this.flashAction.TrueDone())
			{
				this.text.text = this.aimPoint.ToString();
				return;
			}
		}
		else
		{
			this.selector.anchoredPosition = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
			this.selector.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
			this.text.text = this.defaultText;
		}
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x00092C18 File Offset: 0x00090E18
	private void UpdateTargetUi(float scaleFactor)
	{
		if (this.flashAction.TrueDone())
		{
			this.ResolveTargetActor();
		}
		this.targetSelector.SetActive(this.aimActor != null);
		this.selectorLabel.SetActive(this.targetSelector.activeInHierarchy);
		if (this.aimActor != null)
		{
			Vector3 vector;
			Vector3 vector3;
			if (this.aimActor.IsSeated())
			{
				Vehicle vehicle = this.aimActor.seat.vehicle;
				vector = vehicle.targetLockPoint.position;
				Vector2 vector2 = Vector2.Max(new Vector3(3f, 2f), vehicle.avoidanceSize);
				vector3 = new Vector3(vector2.x, vector2.x, vector2.y);
				this.text.text = string.Format("ATTACK {0}", vehicle.name);
			}
			else
			{
				this.text.text = "ATTACK SOLDIER";
				vector = this.aimActor.CenterPosition();
				if (this.aimActor.fallenOver)
				{
					vector3 = new Vector3(1.4f, 1.4f, 1.4f);
				}
				else
				{
					Actor.Stance stance = this.aimActor.stance;
					if (stance != Actor.Stance.Crouch)
					{
						if (stance != Actor.Stance.Prone)
						{
							vector3 = new Vector3(0.7f, 1.9f, 0.7f);
						}
						else
						{
							vector3 = new Vector3(1.5f, 0.7f, 1.5f);
						}
					}
					else
					{
						vector3 = new Vector3(0.8f, 1.2f, 0.8f);
					}
				}
			}
			Camera activeCamera = FpsActorController.instance.GetActiveCamera();
			Vector3 vector4 = activeCamera.WorldToViewportPoint(vector);
			Vector3 vector5 = activeCamera.WorldToViewportPoint(vector + vector3.x / 2f * activeCamera.transform.right + vector3.y / 2f * activeCamera.transform.up) - vector4;
			float num = (float)Screen.width / (float)Screen.height;
			vector5.x = Mathf.Max(0.03f, Mathf.Abs(vector5.x));
			vector5.y = Mathf.Max(num * 0.03f, Mathf.Abs(vector5.y));
			this.selector.anchorMin = new Vector2(vector4.x - vector5.x, vector4.y - vector5.y);
			this.selector.anchorMax = new Vector2(vector4.x + vector5.x, vector4.y + vector5.y);
			this.selector.anchoredPosition = Vector2.zero;
		}
		this.selector.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x00092ECC File Offset: 0x000910CC
	private void ResolveTargetActor()
	{
		Camera activeCamera = FpsActorController.instance.GetActiveCamera();
		Vector3 position = activeCamera.transform.position;
		Vector3 forward = activeCamera.transform.forward;
		float num = float.MaxValue;
		Actor actor = null;
		List<Actor> list = ActorManager.AliveActorsOnTeam(1 - GameManager.PlayerTeam());
		float num2 = Mathf.Cos(activeCamera.fieldOfView * 0.2f * 0.017453292f);
		foreach (Actor actor2 in list)
		{
			if (ActorManager.ActorCanSeePlayer(actor2))
			{
				bool flag = actor2.IsSeated();
				Vector3 a;
				if (flag)
				{
					a = actor2.seat.vehicle.targetLockPoint.position;
				}
				else
				{
					a = actor2.CenterPosition();
				}
				Vector3 a2 = a - position;
				float num3 = a2.magnitude;
				float num4 = Vector3.Dot(forward, a2 / num3);
				float value = (num4 - num2) / (1f - num2);
				num3 -= Mathf.Clamp01(value) * 50f;
				if (flag)
				{
					num3 -= 50f;
				}
				if (num4 > num2 && num3 < num)
				{
					actor = actor2;
					num = num3;
				}
			}
		}
		this.aimActor = actor;
	}

	// Token: 0x040014C9 RID: 5321
	private const float AIM_AT_DOT = 0.95f;

	// Token: 0x040014CA RID: 5322
	private const float TARGET_SELECTION_CAMERA_FOV_MULTIPLIER = 0.2f;

	// Token: 0x040014CB RID: 5323
	private const float TARGET_SELECTION_MAX_CENTER_DISTANCE_BONUS = 50f;

	// Token: 0x040014CC RID: 5324
	private const float TARGET_SELECTION_VEHICLE_DISTANCE_BONUS = 50f;

	// Token: 0x040014CD RID: 5325
	private const float MIN_TARGET_VIEWPORT_SIZE = 0.03f;

	// Token: 0x040014CE RID: 5326
	public static SquadPointUi instance;

	// Token: 0x040014CF RID: 5327
	private SquadPointUi.SquadPointOrderBaseType type;

	// Token: 0x040014D0 RID: 5328
	public RectTransform pointOrderContainer;

	// Token: 0x040014D1 RID: 5329
	public RectTransform targetOrderContainer;

	// Token: 0x040014D2 RID: 5330
	public RectTransform selector;

	// Token: 0x040014D3 RID: 5331
	public GameObject selectorLabel;

	// Token: 0x040014D4 RID: 5332
	public GameObject pointSelector;

	// Token: 0x040014D5 RID: 5333
	public GameObject targetSelector;

	// Token: 0x040014D6 RID: 5334
	public AudioClip openUiSound;

	// Token: 0x040014D7 RID: 5335
	public AudioClip orderUiSound;

	// Token: 0x040014D8 RID: 5336
	public AudioClip orderQuickUiSound;

	// Token: 0x040014D9 RID: 5337
	public AudioClip cancelOrderUiSound;

	// Token: 0x040014DA RID: 5338
	private List<SquadOrderPoint> points;

	// Token: 0x040014DB RID: 5339
	private Canvas canvas;

	// Token: 0x040014DC RID: 5340
	private Actor aimActor;

	// Token: 0x040014DD RID: 5341
	private SquadOrderPoint aimPoint;

	// Token: 0x040014DE RID: 5342
	private Text text;

	// Token: 0x040014DF RID: 5343
	private string defaultText = "ISSUE ORDER";

	// Token: 0x040014E0 RID: 5344
	private TimedAction openAction = new TimedAction(0.07f, false);

	// Token: 0x040014E1 RID: 5345
	private TimedAction flashAction = new TimedAction(0.4f, false);

	// Token: 0x040014E2 RID: 5346
	private bool closeWhenFlashDone;

	// Token: 0x040014E3 RID: 5347
	private AudioSource audio;

	// Token: 0x020002DB RID: 731
	public enum SquadPointOrderBaseType
	{
		// Token: 0x040014E5 RID: 5349
		PointOrder,
		// Token: 0x040014E6 RID: 5350
		TargetOrder
	}
}
