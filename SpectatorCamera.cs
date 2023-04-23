using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x0200023F RID: 575
public class SpectatorCamera : MonoBehaviour
{
	// Token: 0x06000FFB RID: 4091 RVA: 0x00086178 File Offset: 0x00084378
	private void Awake()
	{
		SpectatorCamera.instance = this;
		this.camera = base.GetComponentInChildren<Camera>();
		this.depthOfField = base.GetComponent<DepthOfField>();
		this.depthOfField.enabled = false;
		GameObject gameObject = new GameObject("Spectator Camera Parent");
		this.cameraParent = gameObject.transform;
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0000CD0A File Offset: 0x0000AF0A
	private void Start()
	{
		PostProcessingManager.RegisterWorldCamera(this.camera);
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000861C8 File Offset: 0x000843C8
	private void UpdateScroll()
	{
		float y = Input.mouseScrollDelta.y;
		int num = 0;
		if (y > 0f)
		{
			num = 1;
		}
		else if (y < 0f)
		{
			num = -1;
		}
		if (this.currentEffectType == SpectatorCamera.ImageEffectType.None)
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{
				float num2 = Mathf.Clamp(this.camera.fieldOfView / 10f, 0.1f, 2f);
				if (y < 0f)
				{
					this.camera.fieldOfView += num2;
					return;
				}
				if (y > 0f)
				{
					this.camera.fieldOfView -= num2;
					return;
				}
			}
			else
			{
				if (y < 0f)
				{
					this.speedMultiplier /= 1.3f;
					return;
				}
				if (y > 0f)
				{
					this.speedMultiplier *= 1.3f;
					return;
				}
			}
		}
		else if (this.currentEffectType == SpectatorCamera.ImageEffectType.DepthOfField)
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{
				this.depthOfField.aperture = Mathf.Clamp(this.depthOfField.aperture + (float)num * 0.05f, 0f, 1f);
				return;
			}
			this.depthOfField.focalSize = Mathf.Clamp(this.depthOfField.focalSize + (float)num * 0.05f, 0f, 2f);
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x00086318 File Offset: 0x00084518
	private void LateUpdate()
	{
		if (Benchmark.isRunning)
		{
			return;
		}
		float d = GameManager.IsSpectating() ? Time.deltaTime : Time.unscaledDeltaTime;
		this.UpdateScroll();
		float num = 20f;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			num = 80f;
		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			num = 5f;
		}
		num *= this.speedMultiplier;
		this.velocity = (base.transform.forward * SteelInput.GetAxis(SteelInput.KeyBinds.Vertical) - base.transform.right * SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal) - base.transform.up * SteelInput.GetAxis(SteelInput.KeyBinds.Lean)) * num;
		if (!this.smooth)
		{
			base.transform.position += this.velocity * d;
		}
		else
		{
			base.transform.position += this.smoothVelocity.Value() * d;
		}
		this.angularVelocity = new Vector3(-SteelInput.GetAxis(SteelInput.KeyBinds.AimY), -SteelInput.GetAxis(SteelInput.KeyBinds.AimX), 0f) * Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity) * 4f;
		if (!this.smooth)
		{
			base.transform.eulerAngles += this.angularVelocity;
		}
		else
		{
			base.transform.eulerAngles += this.smoothAngularVelocity.Value();
		}
		RaycastHit raycastHit;
		if (this.currentEffectType == SpectatorCamera.ImageEffectType.DepthOfField && Input.GetKeyUp(KeyCode.F))
		{
			if (Physics.Raycast(new Ray(this.camera.transform.position, this.camera.transform.forward), out raycastHit, 9999f, -12618245))
			{
				this.depthOfField.focalLength = raycastHit.distance;
			}
			else
			{
				this.depthOfField.focalLength = 10000f;
			}
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			this.fullLock = !this.fullLock;
			if (this.fullLock && this.target != null)
			{
				this.LockCameraParent();
			}
			else if (!this.fullLock && this.target != null)
			{
				this.lastParentPosition = this.target.position;
				this.UnlockCameraParent();
			}
		}
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(base.transform.position, base.transform.forward, out raycastHit) && !raycastHit.collider.gameObject.isStatic)
		{
			this.target = raycastHit.collider.transform;
			this.lastParentPosition = this.target.position;
			if (this.fullLock)
			{
				this.LockCameraParent();
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.target = null;
		}
		if (this.hadTarget && this.target == null && this.fullLock)
		{
			this.UnlockCameraParent();
		}
		if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.F7))
		{
			this.smooth = !this.smooth;
		}
		if (Input.GetKeyDown(KeyCode.F9))
		{
			ScreenCapture.CaptureScreenshot("screenshot.png", 4);
		}
		if (this.target != null)
		{
			if (!this.fullLock)
			{
				Vector3 position = this.target.position;
				Vector3 b = position - this.lastParentPosition;
				base.transform.position += b;
				this.lastParentPosition = position;
			}
			else
			{
				this.cameraParent.transform.position = this.target.position;
				this.cameraParent.transform.rotation = this.target.rotation;
			}
		}
		this.hadTarget = (this.target != null);
		if (Input.GetKeyDown(KeyCode.I))
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{
				this.DisableImageEffects();
				return;
			}
			this.SwitchImageEffectMode();
		}
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0000CD17 File Offset: 0x0000AF17
	private void SwitchImageEffectMode()
	{
		this.currentEffectType++;
		if (!Enum.IsDefined(typeof(SpectatorCamera.ImageEffectType), this.currentEffectType))
		{
			this.currentEffectType = SpectatorCamera.ImageEffectType.None;
		}
		this.depthOfField.enabled = true;
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x00086708 File Offset: 0x00084908
	public void DisableImageEffects()
	{
		try
		{
			this.depthOfField.enabled = false;
			this.currentEffectType = SpectatorCamera.ImageEffectType.None;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0000CD56 File Offset: 0x0000AF56
	private void LockCameraParent()
	{
		this.cameraParent.position = this.target.position;
		this.cameraParent.rotation = this.target.rotation;
		base.transform.SetParent(this.cameraParent, true);
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x00086744 File Offset: 0x00084944
	private void UnlockCameraParent()
	{
		base.transform.SetParent(null, true);
		Vector3 eulerAngles = base.transform.eulerAngles;
		eulerAngles.z = 0f;
		base.transform.eulerAngles = eulerAngles;
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0000CD96 File Offset: 0x0000AF96
	private void FixedUpdate()
	{
		if (Benchmark.isRunning)
		{
			return;
		}
		this.smoothVelocity.Tick(this.velocity);
		this.smoothAngularVelocity.Tick(this.angularVelocity);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x00086784 File Offset: 0x00084984
	private void OnGUI()
	{
		if (!GameManager.instance.hudEnabled || Benchmark.isRunning)
		{
			return;
		}
		GUI.Label(new Rect(0f, 0f, 500f, 20f), "Press I for image effects, Ctrl+I to disable image effects. Hide the UI with Home/End");
		GUI.color = Color.white;
		if (this.currentEffectType == SpectatorCamera.ImageEffectType.None)
		{
			GUI.Label(new Rect(0f, 60f, 500f, 20f), "Camera Speed (scroll): " + this.speedMultiplier.ToString());
			GUI.Label(new Rect(0f, 80f, 500f, 20f), "Camera FOV (ctrl+scroll): " + this.camera.fieldOfView.ToString());
		}
		if (this.currentEffectType == SpectatorCamera.ImageEffectType.DepthOfField)
		{
			GUI.Label(new Rect(0f, 40f, 500f, 20f), "Depth Of Field");
			GUI.Label(new Rect(0f, 60f, 500f, 20f), "Press F to set focus distance");
			GUI.Label(new Rect(0f, 80f, 500f, 20f), "Focal Length (scroll): " + this.depthOfField.focalSize.ToString());
			GUI.Label(new Rect(0f, 100f, 500f, 20f), "Aperture (ctrl+scroll): " + this.depthOfField.aperture.ToString());
			if (Input.GetKey(KeyCode.F))
			{
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect((float)Screen.width / 2f - 50f, (float)(Screen.height / 2) - 50f, 100f, 100f), "+");
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
			}
		}
	}

	// Token: 0x040010BE RID: 4286
	public static SpectatorCamera instance;

	// Token: 0x040010BF RID: 4287
	private Vector3 velocity = Vector3.zero;

	// Token: 0x040010C0 RID: 4288
	private MeanFilterVector3 smoothVelocity = new MeanFilterVector3(60);

	// Token: 0x040010C1 RID: 4289
	private Vector3 angularVelocity;

	// Token: 0x040010C2 RID: 4290
	private MeanFilterVector3 smoothAngularVelocity = new MeanFilterVector3(60);

	// Token: 0x040010C3 RID: 4291
	private bool smooth;

	// Token: 0x040010C4 RID: 4292
	public float speedMultiplier = 1f;

	// Token: 0x040010C5 RID: 4293
	public GameObject projectilePrefab;

	// Token: 0x040010C6 RID: 4294
	private Transform cameraParent;

	// Token: 0x040010C7 RID: 4295
	private Transform target;

	// Token: 0x040010C8 RID: 4296
	private Vector3 lastParentPosition = Vector3.zero;

	// Token: 0x040010C9 RID: 4297
	[NonSerialized]
	public Camera camera;

	// Token: 0x040010CA RID: 4298
	private bool fullLock;

	// Token: 0x040010CB RID: 4299
	private bool hadTarget;

	// Token: 0x040010CC RID: 4300
	private SpectatorCamera.ImageEffectType currentEffectType;

	// Token: 0x040010CD RID: 4301
	private DepthOfField depthOfField;

	// Token: 0x02000240 RID: 576
	public enum ImageEffectType
	{
		// Token: 0x040010CF RID: 4303
		None,
		// Token: 0x040010D0 RID: 4304
		DepthOfField
	}
}
