using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class ActiveRaggy : MonoBehaviour
{
	// Token: 0x06000059 RID: 89 RVA: 0x0003EA44 File Offset: 0x0003CC44
	private void Awake()
	{
		this.InitializeDictionaries();
		this.jointDrive = default(JointDrive);
		this.jointDrive.mode = JointDriveMode.Position;
		this.jointDrive.maximumForce = 5000f;
		foreach (Rigidbody rigidbody in this.rigidbodies)
		{
			rigidbody.maxAngularVelocity = 200f;
			rigidbody.sleepThreshold = 0.005f;
			rigidbody.maxDepenetrationVelocity = 10f;
		}
		this.SetDriveDefault();
		this.ragdollObject.SetActive(false);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000026FB File Offset: 0x000008FB
	public void ClearSerializedLists()
	{
		this.animatedRenderers = null;
		this.animator = null;
		this.joints = null;
		this.rigidbodies = null;
		this.jointTargets = null;
		this.r2aTransforms = null;
		this.a2rTransforms = null;
		this.ragdollTransforms = null;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0003EACC File Offset: 0x0003CCCC
	public void SetupSerializedTransformLists()
	{
		this.animatedRenderers = this.animatedObject.GetComponentsInChildren<Renderer>();
		Debug.Log("Renderers: " + this.animatedRenderers.Length.ToString());
		this.animator = this.animatedObject.GetComponent<Animator>();
		if (this.animator == null)
		{
			throw new Exception("No animator component found in Animated Object.");
		}
		if (!this.animator.isHuman)
		{
			throw new Exception("Animator is not a humanoid!");
		}
		Debug.Log("Serialized trnasform list");
		this.animatedTransforms = new List<Transform>(this.animatedObject.GetComponentsInChildren<Transform>());
		this.ragdollTransforms = new List<Transform>(this.ragdollObject.GetComponentsInChildren<Transform>());
		this.animatedTransforms.Remove(this.animatedObject.transform);
		this.ragdollTransforms.Remove(this.ragdollObject.transform);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002735 File Offset: 0x00000935
	public void SetupSerializedRagdollLists()
	{
		this.rigidbodies = this.ragdollObject.GetComponentsInChildren<Rigidbody>();
		this.joints = this.ragdollObject.GetComponentsInChildren<ConfigurableJoint>();
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00002759 File Offset: 0x00000959
	public void SetupSerializedColliderLists()
	{
		this.animatedColliders = this.animatedObject.GetComponentsInChildren<Collider>();
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
	public void InitializeDictionaries()
	{
		this.r2aTransforms = new Dictionary<Transform, Transform>();
		this.a2rTransforms = new Dictionary<Transform, Transform>();
		for (int i = 0; i < this.ragdollTransforms.Count; i++)
		{
			this.r2aTransforms.Add(this.ragdollTransforms[i], this.animatedTransforms[i]);
			this.a2rTransforms.Add(this.animatedTransforms[i], this.ragdollTransforms[i]);
		}
		this.jointTargets = new Dictionary<ConfigurableJoint, Transform>();
		if (this.joints != null)
		{
			foreach (ConfigurableJoint configurableJoint in this.joints)
			{
				Transform value = this.r2aTransforms[configurableJoint.transform];
				this.jointTargets.Add(configurableJoint, value);
			}
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0003EC7C File Offset: 0x0003CE7C
	private void InitializeJointRotations()
	{
		this.initialRotations = new Dictionary<ConfigurableJoint, Quaternion>(this.joints.Length);
		foreach (ConfigurableJoint configurableJoint in this.joints)
		{
			Quaternion localRotation = configurableJoint.transform.localRotation;
			this.initialRotations.Add(configurableJoint, localRotation);
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000276C File Offset: 0x0000096C
	public void SetDriveDefault()
	{
		this.SetDrive(1000f, 3f);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0003ECD0 File Offset: 0x0003CED0
	public void SetDrive(float spring, float damping)
	{
		this.jointDrive.positionSpring = spring;
		this.jointDrive.positionDamper = damping;
		ConfigurableJoint[] array = this.joints;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].slerpDrive = this.jointDrive;
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0003ED18 File Offset: 0x0003CF18
	public void Animate()
	{
		if (this.state != ActiveRaggy.State.Ragdoll || !this.hasControl)
		{
			return;
		}
		this.state = ActiveRaggy.State.PreInterpolate;
		this.interpolateSourcePosition = new Dictionary<Transform, Vector3>(this.ragdollTransforms.Count);
		this.interpolateSourceRotation = new Dictionary<Transform, Quaternion>(this.ragdollTransforms.Count);
		foreach (Transform transform in this.ragdollTransforms)
		{
			this.interpolateSourcePosition.Add(transform, transform.position);
			this.interpolateSourceRotation.Add(transform, transform.rotation);
		}
		base.StartCoroutine(this.SyncAnimatorPosition());
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000277E File Offset: 0x0000097E
	public void InstantAnimate()
	{
		this.StopRagdoll();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00002786 File Offset: 0x00000986
	private IEnumerator SyncAnimatorPosition()
	{
		yield return new WaitForFixedUpdate();
		Vector3 vector = this.interpolateSourcePosition[this.HumanBoneTransformRagdoll(HumanBodyBones.Hips)];
		Vector3 position = vector;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(vector, Vector3.down), out raycastHit, 2f))
		{
			position = raycastHit.point;
		}
		base.transform.rotation = Quaternion.LookRotation(this.PlanarForward());
		base.transform.position = position;
		this.interpolateStartTime = Time.time;
		this.state = ActiveRaggy.State.Interpolate;
		yield break;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0003EDDC File Offset: 0x0003CFDC
	public void Ragdoll(Vector3 velocity)
	{
		if (this.state == ActiveRaggy.State.Ragdoll)
		{
			return;
		}
		this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		if (this.OnStartRagdoll != null)
		{
			this.OnStartRagdoll();
		}
		if (this.autoDisableColliders)
		{
			Collider[] array = this.animatedColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		if (this.controlRenderers)
		{
			Renderer[] array2 = this.animatedRenderers;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
		}
		this.ragdollObject.SetActive(true);
		this.InitializeJointRotations();
		this.ragdollObject.transform.rotation = Quaternion.identity;
		foreach (Transform transform in this.ragdollTransforms)
		{
			Transform transform2 = this.r2aTransforms[transform];
			transform.position = transform2.position;
			transform.rotation = transform2.rotation;
			transform.localScale = transform2.localScale;
		}
		Rigidbody[] array3 = this.rigidbodies;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].velocity = velocity;
		}
		this.state = ActiveRaggy.State.Ragdoll;
		Physics.SyncTransforms();
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0003EF2C File Offset: 0x0003D12C
	public void DisableAnimatedAndRagdollObjects()
	{
		if (this.autoDisableColliders)
		{
			Collider[] array = this.animatedColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		if (this.controlRenderers)
		{
			Renderer[] array2 = this.animatedRenderers;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
		}
		this.ragdollObject.SetActive(false);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0003EF94 File Offset: 0x0003D194
	private void StopRagdoll()
	{
		if (this.OnEndRagdoll != null)
		{
			this.OnEndRagdoll();
		}
		if (this.autoDisableColliders)
		{
			Collider[] array = this.animatedColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
		if (this.controlRenderers)
		{
			Renderer[] array2 = this.animatedRenderers;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = true;
			}
		}
		this.ragdollObject.SetActive(false);
		this.animator.cullingMode = AnimatorCullingMode.CullCompletely;
		this.state = ActiveRaggy.State.Animate;
		Physics.SyncTransforms();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00002795 File Offset: 0x00000995
	public bool OnBack()
	{
		return this.Forward(this.HumanBoneTransformRagdoll(HumanBodyBones.Spine)).y > 0f;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0003F024 File Offset: 0x0003D224
	public bool Upright()
	{
		return (this.HumanBoneTransformRagdoll(HumanBodyBones.Head).position - this.HumanBoneTransformRagdoll(HumanBodyBones.Spine).position).normalized.y > 0.70710677f;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000027B0 File Offset: 0x000009B0
	public Vector3 Velocity()
	{
		return this.rigidbodies[0].velocity;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000027BF File Offset: 0x000009BF
	public Rigidbody MainRigidbody()
	{
		return this.rigidbodies[0];
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000027C9 File Offset: 0x000009C9
	public Vector3 Position()
	{
		if (this.state == ActiveRaggy.State.Ragdoll || this.state == ActiveRaggy.State.PreInterpolate)
		{
			return this.HumanBoneTransformRagdoll(HumanBodyBones.Hips).position;
		}
		return this.HumanBoneTransformAnimated(HumanBodyBones.Hips).position;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0003F064 File Offset: 0x0003D264
	public Vector3 PlanarForward()
	{
		Vector3 vector;
		if (this.Upright())
		{
			vector = this.Forward(this.HumanBoneTransformRagdoll(HumanBodyBones.Spine));
		}
		else if (this.OnBack())
		{
			vector = -this.Along(this.HumanBoneTransformRagdoll(HumanBodyBones.Spine));
		}
		else
		{
			vector = this.Along(this.HumanBoneTransformRagdoll(HumanBodyBones.Spine));
		}
		vector.y = 0f;
		return vector.normalized;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000027F6 File Offset: 0x000009F6
	public Vector3 Forward(Transform transform)
	{
		return this.TransformVector(this.configuration.forwardAxis, transform);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000280A File Offset: 0x00000A0A
	public Vector3 Along(Transform transform)
	{
		return this.TransformVector(this.configuration.alongAxis, transform);
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0000281E File Offset: 0x00000A1E
	public Vector3 Right(Transform transform)
	{
		return this.TransformVector(this.configuration.rightAxis, transform);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0003F0C8 File Offset: 0x0003D2C8
	private Vector3 TransformVector(ActiveRaggy.SignedAxis axis, Transform t)
	{
		switch (axis)
		{
		case ActiveRaggy.SignedAxis.X:
			return t.right;
		case ActiveRaggy.SignedAxis.XNegative:
			return -t.right;
		case ActiveRaggy.SignedAxis.Y:
			return t.up;
		case ActiveRaggy.SignedAxis.YNegative:
			return -t.up;
		case ActiveRaggy.SignedAxis.Z:
			return t.forward;
		case ActiveRaggy.SignedAxis.ZNegative:
			return -t.forward;
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00002832 File Offset: 0x00000A32
	public Vector3 LocalForward()
	{
		return this.WorldVector(this.configuration.forwardAxis);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00002845 File Offset: 0x00000A45
	public Vector3 LocalAlong()
	{
		return this.WorldVector(this.configuration.alongAxis);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00002858 File Offset: 0x00000A58
	public Vector3 LocalRight()
	{
		return this.WorldVector(this.configuration.rightAxis);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0003F134 File Offset: 0x0003D334
	private Vector3 WorldVector(ActiveRaggy.SignedAxis axis)
	{
		switch (axis)
		{
		case ActiveRaggy.SignedAxis.X:
			return Vector3.right;
		case ActiveRaggy.SignedAxis.XNegative:
			return Vector3.left;
		case ActiveRaggy.SignedAxis.Y:
			return Vector3.up;
		case ActiveRaggy.SignedAxis.YNegative:
			return Vector3.down;
		case ActiveRaggy.SignedAxis.Z:
			return Vector3.forward;
		case ActiveRaggy.SignedAxis.ZNegative:
			return Vector3.back;
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0003F18C File Offset: 0x0003D38C
	public int AlongDirectionIndex()
	{
		switch (this.configuration.alongAxis)
		{
		case ActiveRaggy.SignedAxis.X:
			return 0;
		case ActiveRaggy.SignedAxis.XNegative:
			return 0;
		case ActiveRaggy.SignedAxis.Y:
			return 1;
		case ActiveRaggy.SignedAxis.YNegative:
			return 1;
		case ActiveRaggy.SignedAxis.Z:
			return 2;
		case ActiveRaggy.SignedAxis.ZNegative:
			return 2;
		default:
			return 0;
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000286B File Offset: 0x00000A6B
	public bool IsRagdoll()
	{
		return this.state == ActiveRaggy.State.Ragdoll;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00002876 File Offset: 0x00000A76
	public bool IsRenderingRagdollTransforms()
	{
		return this.state > ActiveRaggy.State.Animate;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00002881 File Offset: 0x00000A81
	public void HumanBoneTransform(HumanBodyBones bone, out Transform animated, out Transform ragdoll)
	{
		animated = this.animator.GetBoneTransform(bone);
		if (animated != null)
		{
			ragdoll = this.a2rTransforms[animated];
			return;
		}
		ragdoll = null;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000028AE File Offset: 0x00000AAE
	public Transform HumanBoneTransformAnimated(HumanBodyBones bone)
	{
		return this.animator.GetBoneTransform(bone);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000028BC File Offset: 0x00000ABC
	public Transform HumanBoneTransformRagdoll(HumanBodyBones bone)
	{
		return this.a2rTransforms[this.HumanBoneTransformAnimated(bone)];
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000028D0 File Offset: 0x00000AD0
	public Transform HumanBoneTransformActive(HumanBodyBones bone)
	{
		if (this.IsRagdoll())
		{
			return this.HumanBoneTransformRagdoll(bone);
		}
		return this.HumanBoneTransformAnimated(bone);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000028E9 File Offset: 0x00000AE9
	public Transform AnimatedToRagdoll(Transform transform)
	{
		return this.a2rTransforms[transform];
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000028F7 File Offset: 0x00000AF7
	public Transform RagdollToAnimated(Transform transform)
	{
		return this.r2aTransforms[transform];
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00002905 File Offset: 0x00000B05
	public Renderer[] AnimatedRenderers()
	{
		return this.animatedRenderers;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000290D File Offset: 0x00000B0D
	public void SetControl(bool control)
	{
		this.hasControl = control;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0003F1D4 File Offset: 0x0003D3D4
	private void Update()
	{
		if (this.state == ActiveRaggy.State.Ragdoll)
		{
			if (this.hasControl)
			{
				foreach (ConfigurableJoint configurableJoint in this.joints)
				{
					Quaternion localRotation = this.jointTargets[configurableJoint].localRotation;
					configurableJoint.SetTargetRotationLocal(localRotation, this.initialRotations[configurableJoint]);
				}
			}
			bool flag = this.rigidbodies[0].velocity.sqrMagnitude > 400f;
			if (flag != this.useContinuousCollisions)
			{
				this.useContinuousCollisions = flag;
				for (int j = 0; j < this.rigidbodies.Length; j++)
				{
					this.rigidbodies[j].collisionDetectionMode = (this.useContinuousCollisions ? CollisionDetectionMode.Continuous : CollisionDetectionMode.Discrete);
				}
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0003F294 File Offset: 0x0003D494
	private void LateUpdate()
	{
		if (this.state == ActiveRaggy.State.Interpolate)
		{
			float num = Mathf.Clamp01((Time.time - this.interpolateStartTime) / this.interpolateDuration);
			try
			{
				foreach (Transform transform in this.ragdollTransforms)
				{
					Transform transform2 = this.r2aTransforms[transform];
					transform.position = Vector3.Lerp(this.interpolateSourcePosition[transform], transform2.position, num);
					transform.rotation = Quaternion.Slerp(this.interpolateSourceRotation[transform], transform2.rotation, num);
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Caught exception:");
				Debug.LogException(exception);
			}
			if (num == 1f)
			{
				this.StopRagdoll();
			}
		}
	}

	// Token: 0x0400001B RID: 27
	private const float DEFAULT_DRIVE_SPRING = 1000f;

	// Token: 0x0400001C RID: 28
	private const float DEFAULT_DRIVE_DRAG = 3f;

	// Token: 0x0400001D RID: 29
	private const float MAXIMUM_DRIVE_FORCE = 5000f;

	// Token: 0x0400001E RID: 30
	private const float MAX_PROJECTION_DISTANCE = 2f;

	// Token: 0x0400001F RID: 31
	private const float MAX_ANGULAR_VELOCITY = 200f;

	// Token: 0x04000020 RID: 32
	private const float MAX_DEPENETRATION_VELOCITY = 10f;

	// Token: 0x04000021 RID: 33
	public const float SLEEP_THRESHOLD = 0.005f;

	// Token: 0x04000022 RID: 34
	private const float HALFPOINT = 0.70710677f;

	// Token: 0x04000023 RID: 35
	public GameObject animatedObject;

	// Token: 0x04000024 RID: 36
	public GameObject ragdollObject;

	// Token: 0x04000025 RID: 37
	public float interpolateDuration = 0.5f;

	// Token: 0x04000026 RID: 38
	public bool autoDisableColliders;

	// Token: 0x04000027 RID: 39
	[NonSerialized]
	public bool controlRenderers = true;

	// Token: 0x04000028 RID: 40
	public ActiveRaggy.Configuration configuration;

	// Token: 0x04000029 RID: 41
	[SerializeField]
	private Animator animator;

	// Token: 0x0400002A RID: 42
	[SerializeField]
	private Renderer[] animatedRenderers;

	// Token: 0x0400002B RID: 43
	[SerializeField]
	private ConfigurableJoint[] joints;

	// Token: 0x0400002C RID: 44
	[SerializeField]
	public Rigidbody[] rigidbodies;

	// Token: 0x0400002D RID: 45
	[SerializeField]
	private List<Transform> ragdollTransforms;

	// Token: 0x0400002E RID: 46
	[SerializeField]
	private List<Transform> animatedTransforms;

	// Token: 0x0400002F RID: 47
	[SerializeField]
	public Collider[] animatedColliders;

	// Token: 0x04000030 RID: 48
	public ActiveRaggy.OnStatechange OnStartRagdoll;

	// Token: 0x04000031 RID: 49
	public ActiveRaggy.OnStatechange OnEndRagdoll;

	// Token: 0x04000032 RID: 50
	private Dictionary<ConfigurableJoint, Transform> jointTargets;

	// Token: 0x04000033 RID: 51
	private Dictionary<Transform, Transform> r2aTransforms;

	// Token: 0x04000034 RID: 52
	private Dictionary<Transform, Transform> a2rTransforms;

	// Token: 0x04000035 RID: 53
	private Dictionary<ConfigurableJoint, Quaternion> initialRotations;

	// Token: 0x04000036 RID: 54
	private Dictionary<Transform, Vector3> interpolateSourcePosition;

	// Token: 0x04000037 RID: 55
	private Dictionary<Transform, Quaternion> interpolateSourceRotation;

	// Token: 0x04000038 RID: 56
	[NonSerialized]
	public ActiveRaggy.State state;

	// Token: 0x04000039 RID: 57
	private JointDrive jointDrive;

	// Token: 0x0400003A RID: 58
	private float interpolateStartTime;

	// Token: 0x0400003B RID: 59
	private bool hasControl = true;

	// Token: 0x0400003C RID: 60
	private const float CONTINUOUS_COLLISION_SPEED_THRESHOLD = 20f;

	// Token: 0x0400003D RID: 61
	private const float CONTINUOUS_COLLISION_SPEED_THRESHOLD_SQR = 400f;

	// Token: 0x0400003E RID: 62
	private bool useContinuousCollisions;

	// Token: 0x02000014 RID: 20
	public enum SignedAxis
	{
		// Token: 0x04000040 RID: 64
		X,
		// Token: 0x04000041 RID: 65
		XNegative,
		// Token: 0x04000042 RID: 66
		Y,
		// Token: 0x04000043 RID: 67
		YNegative,
		// Token: 0x04000044 RID: 68
		Z,
		// Token: 0x04000045 RID: 69
		ZNegative
	}

	// Token: 0x02000015 RID: 21
	public enum State
	{
		// Token: 0x04000047 RID: 71
		Animate,
		// Token: 0x04000048 RID: 72
		Ragdoll,
		// Token: 0x04000049 RID: 73
		PreInterpolate,
		// Token: 0x0400004A RID: 74
		Interpolate
	}

	// Token: 0x02000016 RID: 22
	[Serializable]
	public class Configuration
	{
		// Token: 0x0400004B RID: 75
		public float limbScale = 1f;

		// Token: 0x0400004C RID: 76
		public ActiveRaggy.SignedAxis alongAxis = ActiveRaggy.SignedAxis.XNegative;

		// Token: 0x0400004D RID: 77
		public ActiveRaggy.SignedAxis forwardAxis = ActiveRaggy.SignedAxis.Z;

		// Token: 0x0400004E RID: 78
		public ActiveRaggy.SignedAxis rightAxis = ActiveRaggy.SignedAxis.Y;
	}

	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x06000086 RID: 134
	public delegate void OnStatechange();
}
