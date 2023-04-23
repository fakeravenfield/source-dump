using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009F3 RID: 2547
	[Proxy(typeof(Rigidbody))]
	public class RigidbodyProxy : IProxy
	{
		// Token: 0x06004DA3 RID: 19875 RVA: 0x00038006 File Offset: 0x00036206
		[MoonSharpHidden]
		public RigidbodyProxy(Rigidbody value)
		{
			this._value = value;
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00038015 File Offset: 0x00036215
		public RigidbodyProxy()
		{
			this._value = new Rigidbody();
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x00038028 File Offset: 0x00036228
		// (set) Token: 0x06004DA6 RID: 19878 RVA: 0x00038035 File Offset: 0x00036235
		public float angularDrag
		{
			get
			{
				return this._value.angularDrag;
			}
			set
			{
				this._value.angularDrag = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x00038043 File Offset: 0x00036243
		// (set) Token: 0x06004DA8 RID: 19880 RVA: 0x00038055 File Offset: 0x00036255
		public Vector3Proxy angularVelocity
		{
			get
			{
				return Vector3Proxy.New(this._value.angularVelocity);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.angularVelocity = value._value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x00038076 File Offset: 0x00036276
		// (set) Token: 0x06004DAA RID: 19882 RVA: 0x00038088 File Offset: 0x00036288
		public Vector3Proxy centerOfMass
		{
			get
			{
				return Vector3Proxy.New(this._value.centerOfMass);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.centerOfMass = value._value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x000380A9 File Offset: 0x000362A9
		// (set) Token: 0x06004DAC RID: 19884 RVA: 0x000380B6 File Offset: 0x000362B6
		public RigidbodyConstraints constraints
		{
			get
			{
				return this._value.constraints;
			}
			set
			{
				this._value.constraints = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x000380C4 File Offset: 0x000362C4
		// (set) Token: 0x06004DAE RID: 19886 RVA: 0x000380D1 File Offset: 0x000362D1
		public bool detectCollisions
		{
			get
			{
				return this._value.detectCollisions;
			}
			set
			{
				this._value.detectCollisions = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x000380DF File Offset: 0x000362DF
		// (set) Token: 0x06004DB0 RID: 19888 RVA: 0x000380EC File Offset: 0x000362EC
		public float drag
		{
			get
			{
				return this._value.drag;
			}
			set
			{
				this._value.drag = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x000380FA File Offset: 0x000362FA
		// (set) Token: 0x06004DB2 RID: 19890 RVA: 0x00038107 File Offset: 0x00036307
		public bool freezeRotation
		{
			get
			{
				return this._value.freezeRotation;
			}
			set
			{
				this._value.freezeRotation = value;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06004DB3 RID: 19891 RVA: 0x00038115 File Offset: 0x00036315
		// (set) Token: 0x06004DB4 RID: 19892 RVA: 0x00038127 File Offset: 0x00036327
		public Vector3Proxy inertiaTensor
		{
			get
			{
				return Vector3Proxy.New(this._value.inertiaTensor);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.inertiaTensor = value._value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x00038148 File Offset: 0x00036348
		// (set) Token: 0x06004DB6 RID: 19894 RVA: 0x0003815A File Offset: 0x0003635A
		public QuaternionProxy inertiaTensorRotation
		{
			get
			{
				return QuaternionProxy.New(this._value.inertiaTensorRotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.inertiaTensorRotation = value._value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x0003817B File Offset: 0x0003637B
		// (set) Token: 0x06004DB8 RID: 19896 RVA: 0x00038188 File Offset: 0x00036388
		public bool isKinematic
		{
			get
			{
				return this._value.isKinematic;
			}
			set
			{
				this._value.isKinematic = value;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x00038196 File Offset: 0x00036396
		// (set) Token: 0x06004DBA RID: 19898 RVA: 0x000381A3 File Offset: 0x000363A3
		public float mass
		{
			get
			{
				return this._value.mass;
			}
			set
			{
				this._value.mass = value;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06004DBB RID: 19899 RVA: 0x000381B1 File Offset: 0x000363B1
		// (set) Token: 0x06004DBC RID: 19900 RVA: 0x000381BE File Offset: 0x000363BE
		public float maxAngularVelocity
		{
			get
			{
				return this._value.maxAngularVelocity;
			}
			set
			{
				this._value.maxAngularVelocity = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x000381CC File Offset: 0x000363CC
		// (set) Token: 0x06004DBE RID: 19902 RVA: 0x000381D9 File Offset: 0x000363D9
		public float maxDepenetrationVelocity
		{
			get
			{
				return this._value.maxDepenetrationVelocity;
			}
			set
			{
				this._value.maxDepenetrationVelocity = value;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x000381E7 File Offset: 0x000363E7
		// (set) Token: 0x06004DC0 RID: 19904 RVA: 0x000381F9 File Offset: 0x000363F9
		public Vector3Proxy position
		{
			get
			{
				return Vector3Proxy.New(this._value.position);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.position = value._value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x0003821A File Offset: 0x0003641A
		// (set) Token: 0x06004DC2 RID: 19906 RVA: 0x0003822C File Offset: 0x0003642C
		public QuaternionProxy rotation
		{
			get
			{
				return QuaternionProxy.New(this._value.rotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.rotation = value._value;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x0003824D File Offset: 0x0003644D
		// (set) Token: 0x06004DC4 RID: 19908 RVA: 0x0003825A File Offset: 0x0003645A
		public float sleepThreshold
		{
			get
			{
				return this._value.sleepThreshold;
			}
			set
			{
				this._value.sleepThreshold = value;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x00038268 File Offset: 0x00036468
		// (set) Token: 0x06004DC6 RID: 19910 RVA: 0x00038275 File Offset: 0x00036475
		public int solverIterations
		{
			get
			{
				return this._value.solverIterations;
			}
			set
			{
				this._value.solverIterations = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x00038283 File Offset: 0x00036483
		// (set) Token: 0x06004DC8 RID: 19912 RVA: 0x00038290 File Offset: 0x00036490
		public int solverVelocityIterations
		{
			get
			{
				return this._value.solverVelocityIterations;
			}
			set
			{
				this._value.solverVelocityIterations = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x0003829E File Offset: 0x0003649E
		// (set) Token: 0x06004DCA RID: 19914 RVA: 0x000382AB File Offset: 0x000364AB
		public bool useGravity
		{
			get
			{
				return this._value.useGravity;
			}
			set
			{
				this._value.useGravity = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x000382B9 File Offset: 0x000364B9
		// (set) Token: 0x06004DCC RID: 19916 RVA: 0x000382CB File Offset: 0x000364CB
		public Vector3Proxy velocity
		{
			get
			{
				return Vector3Proxy.New(this._value.velocity);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.velocity = value._value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x000382EC File Offset: 0x000364EC
		public Vector3Proxy worldCenterOfMass
		{
			get
			{
				return Vector3Proxy.New(this._value.worldCenterOfMass);
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x000382FE File Offset: 0x000364FE
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06004DCF RID: 19919 RVA: 0x00038310 File Offset: 0x00036510
		// (set) Token: 0x06004DD0 RID: 19920 RVA: 0x0003831D File Offset: 0x0003651D
		public string tag
		{
			get
			{
				return this._value.tag;
			}
			set
			{
				this._value.tag = value;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x0003832B File Offset: 0x0003652B
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x0003833D File Offset: 0x0003653D
		// (set) Token: 0x06004DD3 RID: 19923 RVA: 0x0003834A File Offset: 0x0003654A
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x00038358 File Offset: 0x00036558
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x00137C08 File Offset: 0x00135E08
		[MoonSharpHidden]
		public static RigidbodyProxy New(Rigidbody value)
		{
			if (value == null)
			{
				return null;
			}
			RigidbodyProxy rigidbodyProxy = (RigidbodyProxy)ObjectCache.Get(typeof(RigidbodyProxy), value);
			if (rigidbodyProxy == null)
			{
				rigidbodyProxy = new RigidbodyProxy(value);
				ObjectCache.Add(typeof(RigidbodyProxy), value, rigidbodyProxy);
			}
			return rigidbodyProxy;
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x00038360 File Offset: 0x00036560
		[MoonSharpUserDataMetamethod("__call")]
		public static RigidbodyProxy Call(DynValue _)
		{
			return new RigidbodyProxy();
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x00038367 File Offset: 0x00036567
		public void AddExplosionForce(float explosionForce, Vector3Proxy explosionPosition, float explosionRadius, float upwardsModifier, ForceMode mode)
		{
			if (explosionPosition == null)
			{
				throw new ScriptRuntimeException("argument 'explosionPosition' is nil");
			}
			this._value.AddExplosionForce(explosionForce, explosionPosition._value, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0003838E File Offset: 0x0003658E
		public void AddExplosionForce(float explosionForce, Vector3Proxy explosionPosition, float explosionRadius, float upwardsModifier)
		{
			if (explosionPosition == null)
			{
				throw new ScriptRuntimeException("argument 'explosionPosition' is nil");
			}
			this._value.AddExplosionForce(explosionForce, explosionPosition._value, explosionRadius, upwardsModifier);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x000383B3 File Offset: 0x000365B3
		public void AddExplosionForce(float explosionForce, Vector3Proxy explosionPosition, float explosionRadius)
		{
			if (explosionPosition == null)
			{
				throw new ScriptRuntimeException("argument 'explosionPosition' is nil");
			}
			this._value.AddExplosionForce(explosionForce, explosionPosition._value, explosionRadius);
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x000383D6 File Offset: 0x000365D6
		public void AddForce(Vector3Proxy force, ForceMode mode)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			this._value.AddForce(force._value, mode);
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x000383F8 File Offset: 0x000365F8
		public void AddForce(Vector3Proxy force)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			this._value.AddForce(force._value);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x00038419 File Offset: 0x00036619
		public void AddForce(float x, float y, float z, ForceMode mode)
		{
			this._value.AddForce(x, y, z, mode);
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0003842B File Offset: 0x0003662B
		public void AddForce(float x, float y, float z)
		{
			this._value.AddForce(x, y, z);
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x0003843B File Offset: 0x0003663B
		public void AddForceAtPosition(Vector3Proxy force, Vector3Proxy position, ForceMode mode)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			this._value.AddForceAtPosition(force._value, position._value, mode);
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00038471 File Offset: 0x00036671
		public void AddForceAtPosition(Vector3Proxy force, Vector3Proxy position)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			this._value.AddForceAtPosition(force._value, position._value);
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x000384A6 File Offset: 0x000366A6
		public void AddRelativeForce(Vector3Proxy force, ForceMode mode)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			this._value.AddRelativeForce(force._value, mode);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x000384C8 File Offset: 0x000366C8
		public void AddRelativeForce(Vector3Proxy force)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			this._value.AddRelativeForce(force._value);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x000384E9 File Offset: 0x000366E9
		public void AddRelativeForce(float x, float y, float z, ForceMode mode)
		{
			this._value.AddRelativeForce(x, y, z, mode);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x000384FB File Offset: 0x000366FB
		public void AddRelativeForce(float x, float y, float z)
		{
			this._value.AddRelativeForce(x, y, z);
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x0003850B File Offset: 0x0003670B
		public void AddRelativeTorque(Vector3Proxy torque, ForceMode mode)
		{
			if (torque == null)
			{
				throw new ScriptRuntimeException("argument 'torque' is nil");
			}
			this._value.AddRelativeTorque(torque._value, mode);
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0003852D File Offset: 0x0003672D
		public void AddRelativeTorque(Vector3Proxy torque)
		{
			if (torque == null)
			{
				throw new ScriptRuntimeException("argument 'torque' is nil");
			}
			this._value.AddRelativeTorque(torque._value);
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0003854E File Offset: 0x0003674E
		public void AddRelativeTorque(float x, float y, float z, ForceMode mode)
		{
			this._value.AddRelativeTorque(x, y, z, mode);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00038560 File Offset: 0x00036760
		public void AddRelativeTorque(float x, float y, float z)
		{
			this._value.AddRelativeTorque(x, y, z);
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00038570 File Offset: 0x00036770
		public void AddTorque(Vector3Proxy torque, ForceMode mode)
		{
			if (torque == null)
			{
				throw new ScriptRuntimeException("argument 'torque' is nil");
			}
			this._value.AddTorque(torque._value, mode);
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x00038592 File Offset: 0x00036792
		public void AddTorque(Vector3Proxy torque)
		{
			if (torque == null)
			{
				throw new ScriptRuntimeException("argument 'torque' is nil");
			}
			this._value.AddTorque(torque._value);
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x000385B3 File Offset: 0x000367B3
		public void AddTorque(float x, float y, float z, ForceMode mode)
		{
			this._value.AddTorque(x, y, z, mode);
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x000385C5 File Offset: 0x000367C5
		public void AddTorque(float x, float y, float z)
		{
			this._value.AddTorque(x, y, z);
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x000385D5 File Offset: 0x000367D5
		public Vector3Proxy ClosestPointOnBounds(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.ClosestPointOnBounds(position._value));
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x000385FB File Offset: 0x000367FB
		public Vector3Proxy GetPointVelocity(Vector3Proxy worldPoint)
		{
			if (worldPoint == null)
			{
				throw new ScriptRuntimeException("argument 'worldPoint' is nil");
			}
			return Vector3Proxy.New(this._value.GetPointVelocity(worldPoint._value));
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x00038621 File Offset: 0x00036821
		public Vector3Proxy GetRelativePointVelocity(Vector3Proxy relativePoint)
		{
			if (relativePoint == null)
			{
				throw new ScriptRuntimeException("argument 'relativePoint' is nil");
			}
			return Vector3Proxy.New(this._value.GetRelativePointVelocity(relativePoint._value));
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x00038647 File Offset: 0x00036847
		public bool IsSleeping()
		{
			return this._value.IsSleeping();
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x00038654 File Offset: 0x00036854
		public void MovePosition(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			this._value.MovePosition(position._value);
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x00038675 File Offset: 0x00036875
		public void MoveRotation(QuaternionProxy rot)
		{
			if (rot == null)
			{
				throw new ScriptRuntimeException("argument 'rot' is nil");
			}
			this._value.MoveRotation(rot._value);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00038696 File Offset: 0x00036896
		public void ResetCenterOfMass()
		{
			this._value.ResetCenterOfMass();
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x000386A3 File Offset: 0x000368A3
		public void ResetInertiaTensor()
		{
			this._value.ResetInertiaTensor();
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x000386B0 File Offset: 0x000368B0
		public void SetDensity(float density)
		{
			this._value.SetDensity(density);
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x000386BE File Offset: 0x000368BE
		public void Sleep()
		{
			this._value.Sleep();
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x000386CB File Offset: 0x000368CB
		public bool SweepTest(Vector3Proxy direction, RaycastHitProxy hitInfo, float maxDistance)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			if (hitInfo == null)
			{
				throw new ScriptRuntimeException("argument 'hitInfo' is nil");
			}
			return this._value.SweepTest(direction._value, out hitInfo._value, maxDistance);
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x00038701 File Offset: 0x00036901
		public bool SweepTest(Vector3Proxy direction, RaycastHitProxy hitInfo)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			if (hitInfo == null)
			{
				throw new ScriptRuntimeException("argument 'hitInfo' is nil");
			}
			return this._value.SweepTest(direction._value, out hitInfo._value);
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x00038736 File Offset: 0x00036936
		public RaycastHit[] SweepTestAll(Vector3Proxy direction, float maxDistance)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			return this._value.SweepTestAll(direction._value, maxDistance);
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00038758 File Offset: 0x00036958
		public RaycastHit[] SweepTestAll(Vector3Proxy direction)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			return this._value.SweepTestAll(direction._value);
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00038779 File Offset: 0x00036979
		public void WakeUp()
		{
			this._value.WakeUp();
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x00038786 File Offset: 0x00036986
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00038794 File Offset: 0x00036994
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x000387A1 File Offset: 0x000369A1
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003283 RID: 12931
		[MoonSharpHidden]
		public Rigidbody _value;
	}
}
