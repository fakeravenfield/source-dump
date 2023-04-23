using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

// Token: 0x0200023D RID: 573
public class PlayerFpParent : MonoBehaviour
{
	// Token: 0x06000FE1 RID: 4065 RVA: 0x00085578 File Offset: 0x00083778
	private void Awake()
	{
		PlayerFpParent.instance = this;
		this.fpCamera.fieldOfView = 60f;
		this.positionSpring = new Spring(150f, 10f, -Vector3.one * 0.2f, Vector3.one * 0.2f, 8);
		this.rotationSpring = new Spring(70f, 6f, -Vector3.one * 15f, Vector3.one * 15f, 8);
		this.weaponParentOrigin = this.weaponParent.transform.localPosition;
		this.fpsController = base.GetComponentInParent<FirstPersonController>();
		this.actor = base.GetComponentInParent<FpsActorController>().actor;
		AudioListener.volume = 0.3f;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0008564C File Offset: 0x0008384C
	private void Start()
	{
		this.shoulderParentOrigin = this.shoulderParent.localPosition;
		this.lastPosition = this.movementProbe.position;
		this.lastRotation = this.movementProbe.eulerAngles;
		this.SetupHorizontalFov(Options.GetSlider(OptionSlider.Id.Fov));
		this.SetAimFov(45f, true);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x000856A4 File Offset: 0x000838A4
	private void FixedUpdate()
	{
		float num = this.fpsController.StepCycle() * 3.1415927f;
		float num2 = 0f;
		Vector3 input = new Vector3(SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal), 0f, SteelInput.GetAxis(SteelInput.KeyBinds.Vertical));
		Vector3 vector = this.smoothInputFilter.Tick(input);
		float num3 = this.proneCrawlAction.TrueDone() ? Mathf.Clamp01(vector.magnitude) : 0f;
		if (!this.actor.IsSeated())
		{
			num2 = Mathf.Clamp01(this.actor.Velocity().sqrMagnitude / 60f) * (this.aiming ? 0.003f : 0.04f);
			if (this.actor.activeWeapon != null)
			{
				if (this.actor.controller.IsSprinting())
				{
					num2 *= this.actor.activeWeapon.sprintBobMultiplier;
				}
				else
				{
					num2 *= this.actor.activeWeapon.walkBobMultiplier;
				}
				num3 *= this.actor.activeWeapon.proneBobMultiplier;
			}
		}
		Vector3 localEulerAngles = this.fpCameraRoot.localEulerAngles;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		this.proneCrawlAmount = Mathf.MoveTowards(this.proneCrawlAmount, num3, Time.deltaTime * 2.5f);
		if (this.proneCrawlAmount > 0f)
		{
			this.walkOffsetMagnitude = Mathf.MoveTowards(this.walkOffsetMagnitude, 0f, Time.deltaTime * 0.06f);
			float num4 = Mathf.Pow(Mathf.Sin(num * 2f), 2f) * 1.5f;
			float num5 = Mathf.SmoothStep(0f, 1f, this.proneCrawlAmount);
			float num6 = Mathf.DeltaAngle(0f, localEulerAngles.x);
			zero = new Vector3((Mathf.Cos(num * 2f) - num4 * vector.x) * num5 * 0.05f, (num6 * 0.002f - 0.13f) * num5, num4 * vector.z * num5 * 0.05f);
			zero2 = new Vector3(num6 * -0.7f * num5, (Mathf.Cos(num * 2f) - 1f) * num5 * 20f, 0f);
		}
		else
		{
			if (!this.fpsController.OnGround())
			{
				this.walkOffsetMagnitude = Mathf.MoveTowards(this.walkOffsetMagnitude, 0f, Time.deltaTime * 0.24f);
				if (this.fpsController.Velocity().y < -2f && !this.aiming)
				{
					this.rotationSpring.AddVelocity(new Vector3(-3f, Mathf.Sin(Time.time * 6f), 0f));
				}
			}
			else
			{
				this.walkOffsetMagnitude = Mathf.MoveTowards(this.walkOffsetMagnitude, num2, Time.deltaTime * 0.06f);
			}
			zero = new Vector3(Mathf.Cos(num) * this.walkOffsetMagnitude, Mathf.Sin(num * 2f) * this.walkOffsetMagnitude * 0.7f, 0f);
		}
		this.positionSpring.Update();
		this.rotationSpring.Update();
		Vector3 vector2 = this.fpCameraParent.worldToLocalMatrix.MultiplyVector(this.movementProbe.position - this.lastPosition);
		Vector2 vector3 = new Vector2(Mathf.DeltaAngle(this.lastRotation.x, this.movementProbe.eulerAngles.x), Mathf.DeltaAngle(this.lastRotation.y, this.movementProbe.eulerAngles.y));
		this.lastPosition = this.movementProbe.position;
		this.lastRotation = this.movementProbe.eulerAngles;
		float num7 = 0f;
		if (!this.weaponSnapAction.TrueDone())
		{
			float num8 = 1f - this.weaponSnapAction.Ratio();
			num7 = num8 * Mathf.Sin(this.weaponSnapAction.Ratio() * (0.1f + num8) * this.weaponSnapFrequency) * this.weaponSnapMagnitude;
		}
		this.shoulderParent.localPosition = this.shoulderParentOrigin + this.positionSpring.position + new Vector3(0f, num7 * -0.1f, 0f) + zero;
		this.shoulderParent.localEulerAngles = this.rotationSpring.position + new Vector3(num7 * -20f, 0f, 0f) + zero2;
		this.rotationSpring.position += new Vector3(-0.1f * vector3.x + vector2.y * 5f, -0.1f * vector3.y, 0f);
		this.positionSpring.position += new Vector3(-0.0001f * vector3.y, 0.0001f * vector3.x, 0f);
		this.fovRatio = Mathf.MoveTowards(this.fovRatio, this.aiming ? 1f : 0f, Time.deltaTime * this.fovSpeed);
		this.fpCamera.fieldOfView = Mathf.Lerp(this.normalFov, this.zoomFov, this.fovRatio);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x0000CBCE File Offset: 0x0000ADCE
	public Vector3 GetSpringLocalPosition()
	{
		return this.positionSpring.position;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00085C04 File Offset: 0x00083E04
	public Vector3 GetSpringLocalEuler(bool includeCameraKick)
	{
		Vector3 vector = this.rotationSpring.position;
		if (includeCameraKick && !this.fpKickAction.TrueDone())
		{
			vector += this.GetKickLocalEuler();
		}
		return vector;
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0000CBDB File Offset: 0x0000ADDB
	public Vector3 GetKickLocalEuler()
	{
		return Mathf.Sin(this.fpKickAction.Ratio() * 3.1415927f) * this.kickEuler;
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00085C3C File Offset: 0x00083E3C
	private void LateUpdate()
	{
		Vector3 vector = Vector3.right * this.lean * 0.4f;
		Vector3 vector2 = this.fpCameraParent.localToWorldMatrix.MultiplyVector(vector);
		RaycastHit raycastHit;
		if (Physics.SphereCast(new Ray(this.fpCameraParent.parent.position, vector2.normalized), 0.3f, out raycastHit, vector2.magnitude, 1))
		{
			vector = vector.normalized * raycastHit.distance;
		}
		this.fpCameraParent.localPosition = vector;
		Vector3 vector3 = Vector3.back * this.lean * 3f;
		Vector3 vector4 = this.weaponParentOrigin;
		if (!this.fpKickAction.Done())
		{
			float d = Mathf.Sin(this.fpKickAction.Ratio() * 3.1415927f);
			vector3 += d * this.kickEuler;
		}
		this.fpCameraParent.localEulerAngles = vector3;
		if (!this.aiming)
		{
			if (this.lean > 0f)
			{
				vector4 += new Vector3(1f, -1f, 0f) * this.lean * 0.1f;
			}
			else
			{
				vector4 += new Vector3(1f, 0.3f, 0f) * this.lean * 0.23f;
			}
		}
		Vector3 localPosition = Vector3.MoveTowards(this.weaponParent.transform.localPosition, vector4, 2f * Time.deltaTime);
		this.weaponParent.transform.localPosition = localPosition;
		this.extraPitch = Mathf.MoveTowards(this.extraPitch, this.aiming ? 0f : 0.1f, Time.deltaTime);
		this.weaponParent.transform.localEulerAngles = Vector3.right * Mathf.DeltaAngle(0f, this.fpCameraRoot.localEulerAngles.x) * this.extraPitch + Vector3.back * this.lean * 12f;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0000CBFE File Offset: 0x0000ADFE
	public void ApplyScreenshake(float magnitude, int iterations)
	{
		if (this.screenshake)
		{
			base.StopCoroutine(this.screenshakeCoroutine);
		}
		this.screenshakeCoroutine = base.StartCoroutine(this.Screenshake(magnitude, iterations));
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0000CC28 File Offset: 0x0000AE28
	public void ResetCameraOffset()
	{
		this.fpCamera.transform.localPosition = Vector3.zero;
		this.fpCamera.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0000CC54 File Offset: 0x0000AE54
	private IEnumerator Screenshake(float magnitude, int iterations)
	{
		magnitude = Mathf.Clamp(magnitude, 0f, 50f);
		iterations = Mathf.Clamp(iterations, 1, 15);
		this.screenshake = true;
		int num;
		for (int i = 0; i < iterations; i = num + 1)
		{
			float d = magnitude * ((float)(iterations - i) / (float)iterations);
			this.positionSpring.AddVelocity(UnityEngine.Random.insideUnitSphere * d * 0.1f);
			this.KickCamera(UnityEngine.Random.insideUnitSphere * d);
			yield return new WaitForSeconds(0.17f);
			num = i;
		}
		this.screenshake = false;
		yield break;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00085E70 File Offset: 0x00084070
	public void ApplyRecoil(Vector3 impulse, bool kickCamera = true)
	{
		this.positionSpring.AddVelocity(impulse);
		Vector3 vector = new Vector3(impulse.z, impulse.x, -impulse.x);
		float d = 0.1f + this.positionSpring.position.magnitude / 0.2f;
		this.rotationSpring.AddVelocity(vector * d * 100f);
		if (!this.screenshake && kickCamera)
		{
			this.KickCamera(vector);
		}
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00085EF0 File Offset: 0x000840F0
	public void ResetRecoil()
	{
		this.positionSpring.position = Vector3.zero;
		this.positionSpring.velocity = Vector3.zero;
		this.rotationSpring.position = Vector3.zero;
		this.rotationSpring.velocity = Vector3.zero;
		this.screenshake = false;
		base.StopAllCoroutines();
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0000CC71 File Offset: 0x0000AE71
	public void ApplyWeaponSnap(float magnitude, float duration, float frequency)
	{
		this.weaponSnapAction.StartLifetime(duration);
		this.weaponSnapFrequency = frequency;
		this.weaponSnapMagnitude = magnitude;
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0000CC8D File Offset: 0x0000AE8D
	public void Aim()
	{
		this.aiming = true;
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0000CC96 File Offset: 0x0000AE96
	public void StopAim()
	{
		this.aiming = false;
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x00085F4C File Offset: 0x0008414C
	public void SetupHorizontalFov(float hFov)
	{
		float num = (float)Screen.width / (float)Screen.height;
		this.verticalFov = 114.59156f * Mathf.Atan(Mathf.Tan(hFov / 2f * 0.017453292f) / num);
		this.fpCamera.fieldOfView = this.verticalFov;
		this.normalFov = this.verticalFov;
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0000CC9F File Offset: 0x0000AE9F
	public void SetAimFov(float zoom, bool forceReset)
	{
		if (forceReset || this.zoomFov != zoom)
		{
			this.SetFov(this.verticalFov, zoom);
		}
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0000CCBA File Offset: 0x0000AEBA
	public void SetFov(float normal, float zoom)
	{
		this.normalFov = normal;
		this.zoomFov = zoom;
		this.fovRatio = 0f;
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0000CCD5 File Offset: 0x0000AED5
	public void KickCamera(Vector3 kick)
	{
		this.fpKickAction.Start();
		this.kickEuler = kick * 0.7f;
	}

	// Token: 0x04001079 RID: 4217
	private const float CAMERA_HIP_FOV = 60f;

	// Token: 0x0400107A RID: 4218
	private const float CAMERA_AIM_FOV = 45f;

	// Token: 0x0400107B RID: 4219
	private const float FOV_SPEED = 150f;

	// Token: 0x0400107C RID: 4220
	private const float POSITION_SPRING = 150f;

	// Token: 0x0400107D RID: 4221
	private const float POSITION_DRAG = 10f;

	// Token: 0x0400107E RID: 4222
	private const float MAX_POSITION_OFFSET = 0.2f;

	// Token: 0x0400107F RID: 4223
	private const int POSITION_SPRING_ITERAIONS = 8;

	// Token: 0x04001080 RID: 4224
	private const float ROTATION_SPRING = 70f;

	// Token: 0x04001081 RID: 4225
	private const float ROTATION_DRAG = 6f;

	// Token: 0x04001082 RID: 4226
	private const float MAX_ROTATION_OFFSET = 15f;

	// Token: 0x04001083 RID: 4227
	private const float ROTATION_IMPULSE_GAIN = 100f;

	// Token: 0x04001084 RID: 4228
	private const int ROTATION_SPRING_ITERAIONS = 8;

	// Token: 0x04001085 RID: 4229
	private const float CAMERA_KICK_MULTIPLIER = 0.7f;

	// Token: 0x04001086 RID: 4230
	private const float WEAPON_EXTRA_PITCH = 0.1f;

	// Token: 0x04001087 RID: 4231
	private const float LEAN_MAX_HEAD_OFFSET = 0.4f;

	// Token: 0x04001088 RID: 4232
	private const float LEAN_MAX_WEAPON_OFFSET_LEFT = 0.23f;

	// Token: 0x04001089 RID: 4233
	private const float LEAN_MAX_WEAPON_OFFSET_RIGHT = 0.1f;

	// Token: 0x0400108A RID: 4234
	private const float LEAN_MAX_CAMERA_ROLL_ANGLE = 3f;

	// Token: 0x0400108B RID: 4235
	private const float LEAN_MAX_ROLL_ANGLE = 12f;

	// Token: 0x0400108C RID: 4236
	private const float LEAN_HEAD_SPHERECAST_RADIUS = 0.3f;

	// Token: 0x0400108D RID: 4237
	private const float SCREENSHAKE_WEAPON_SHAKE = 0.1f;

	// Token: 0x0400108E RID: 4238
	private const float SCREENSHAKE_ITERATION_TIME = 0.17f;

	// Token: 0x0400108F RID: 4239
	private const float WALK_OFFSET_MAGNITUDE_AIMING = 0.003f;

	// Token: 0x04001090 RID: 4240
	private const float WALK_OFFSET_MAGNITUDE = 0.04f;

	// Token: 0x04001091 RID: 4241
	private const float WALK_OFFSET_MAGNITUDE_CHANGE_SPEED = 0.06f;

	// Token: 0x04001092 RID: 4242
	private const float JUMP_OFFSET_MAGNITUDE_CHANGE_SPEED_FALLING = 0.24f;

	// Token: 0x04001093 RID: 4243
	private const float PRONE_OFFSET_MAGNITUDE_CHANGE_SPEED = 2.5f;

	// Token: 0x04001094 RID: 4244
	private const float PRONE_MAGNITUDE = 0.05f;

	// Token: 0x04001095 RID: 4245
	private const float PRONE_MAGNITUDE_ROTATION = 20f;

	// Token: 0x04001096 RID: 4246
	public static PlayerFpParent instance;

	// Token: 0x04001097 RID: 4247
	public Camera fpCamera;

	// Token: 0x04001098 RID: 4248
	public Transform fpCameraRoot;

	// Token: 0x04001099 RID: 4249
	public Transform fpCameraParent;

	// Token: 0x0400109A RID: 4250
	public Transform movementProbe;

	// Token: 0x0400109B RID: 4251
	public Transform shoulderParent;

	// Token: 0x0400109C RID: 4252
	public Transform weaponParent;

	// Token: 0x0400109D RID: 4253
	private FirstPersonController fpsController;

	// Token: 0x0400109E RID: 4254
	private Actor actor;

	// Token: 0x0400109F RID: 4255
	private Vector3 shoulderParentOrigin = Vector3.zero;

	// Token: 0x040010A0 RID: 4256
	private Spring positionSpring;

	// Token: 0x040010A1 RID: 4257
	private Spring rotationSpring;

	// Token: 0x040010A2 RID: 4258
	private Vector3 lastPosition = Vector3.zero;

	// Token: 0x040010A3 RID: 4259
	private Vector3 lastRotation = Vector3.zero;

	// Token: 0x040010A4 RID: 4260
	private Vector3 weaponParentOrigin;

	// Token: 0x040010A5 RID: 4261
	private Vector3 kickEuler = Vector3.zero;

	// Token: 0x040010A6 RID: 4262
	private TimedAction fpKickAction = new TimedAction(0.2f, false);

	// Token: 0x040010A7 RID: 4263
	private TimedAction weaponSnapAction = new TimedAction(0.5f, false);

	// Token: 0x040010A8 RID: 4264
	private float weaponSnapMagnitude = 0.5f;

	// Token: 0x040010A9 RID: 4265
	private float weaponSnapFrequency = 5f;

	// Token: 0x040010AA RID: 4266
	private MeanFilterVector3 smoothInputFilter = new MeanFilterVector3(16);

	// Token: 0x040010AB RID: 4267
	[NonSerialized]
	public float lean;

	// Token: 0x040010AC RID: 4268
	private float fovRatio;

	// Token: 0x040010AD RID: 4269
	private float fovSpeed = 5f;

	// Token: 0x040010AE RID: 4270
	private bool aiming;

	// Token: 0x040010AF RID: 4271
	private bool screenshake;

	// Token: 0x040010B0 RID: 4272
	private Coroutine screenshakeCoroutine;

	// Token: 0x040010B1 RID: 4273
	private float verticalFov = 60f;

	// Token: 0x040010B2 RID: 4274
	private float normalFov = 60f;

	// Token: 0x040010B3 RID: 4275
	private float zoomFov = 45f;

	// Token: 0x040010B4 RID: 4276
	private float walkOffsetMagnitude;

	// Token: 0x040010B5 RID: 4277
	private float proneCrawlAmount;

	// Token: 0x040010B6 RID: 4278
	private float extraPitch;

	// Token: 0x040010B7 RID: 4279
	[NonSerialized]
	public TimedAction proneCrawlAction = new TimedAction(0.5f, false);
}
