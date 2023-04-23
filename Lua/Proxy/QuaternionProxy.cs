using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009E5 RID: 2533
	[Proxy(typeof(Quaternion))]
	public class QuaternionProxy : IProxy
	{
		// Token: 0x06004AC4 RID: 19140 RVA: 0x00034FBA File Offset: 0x000331BA
		[MoonSharpHidden]
		public QuaternionProxy(Quaternion value)
		{
			this._value = value;
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00034FC9 File Offset: 0x000331C9
		public QuaternionProxy(float x, float y, float z, float w)
		{
			this._value = new Quaternion(x, y, z, w);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00034FE1 File Offset: 0x000331E1
		public QuaternionProxy()
		{
			this._value = default(Quaternion);
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x00034FF5 File Offset: 0x000331F5
		public static float kEpsilon
		{
			get
			{
				return 1E-06f;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x00034FFC File Offset: 0x000331FC
		// (set) Token: 0x06004AC9 RID: 19145 RVA: 0x00035009 File Offset: 0x00033209
		public float w
		{
			get
			{
				return this._value.w;
			}
			set
			{
				this._value.w = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06004ACA RID: 19146 RVA: 0x00035017 File Offset: 0x00033217
		// (set) Token: 0x06004ACB RID: 19147 RVA: 0x00035024 File Offset: 0x00033224
		public float x
		{
			get
			{
				return this._value.x;
			}
			set
			{
				this._value.x = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06004ACC RID: 19148 RVA: 0x00035032 File Offset: 0x00033232
		// (set) Token: 0x06004ACD RID: 19149 RVA: 0x0003503F File Offset: 0x0003323F
		public float y
		{
			get
			{
				return this._value.y;
			}
			set
			{
				this._value.y = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x0003504D File Offset: 0x0003324D
		// (set) Token: 0x06004ACF RID: 19151 RVA: 0x0003505A File Offset: 0x0003325A
		public float z
		{
			get
			{
				return this._value.z;
			}
			set
			{
				this._value.z = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x00035068 File Offset: 0x00033268
		// (set) Token: 0x06004AD1 RID: 19153 RVA: 0x0003507A File Offset: 0x0003327A
		public Vector3Proxy eulerAngles
		{
			get
			{
				return Vector3Proxy.New(this._value.eulerAngles);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.eulerAngles = value._value;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06004AD2 RID: 19154 RVA: 0x0003509B File Offset: 0x0003329B
		public static QuaternionProxy identity
		{
			get
			{
				return QuaternionProxy.New(Quaternion.identity);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x000350A7 File Offset: 0x000332A7
		public QuaternionProxy normalized
		{
			get
			{
				return QuaternionProxy.New(this._value.normalized);
			}
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x000350B9 File Offset: 0x000332B9
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x000350C6 File Offset: 0x000332C6
		[MoonSharpHidden]
		public static QuaternionProxy New(Quaternion value)
		{
			return new QuaternionProxy(value);
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x000350CE File Offset: 0x000332CE
		[MoonSharpUserDataMetamethod("__call")]
		public static QuaternionProxy Call(DynValue _, float x, float y, float z, float w)
		{
			return new QuaternionProxy(x, y, z, w);
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x000350DA File Offset: 0x000332DA
		[MoonSharpUserDataMetamethod("__call")]
		public static QuaternionProxy Call(DynValue _)
		{
			return new QuaternionProxy();
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x000350E1 File Offset: 0x000332E1
		public static float Angle(QuaternionProxy a, QuaternionProxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Quaternion.Angle(a._value, b._value);
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00035110 File Offset: 0x00033310
		public static QuaternionProxy AngleAxis(float angle, Vector3Proxy axis)
		{
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			return QuaternionProxy.New(Quaternion.AngleAxis(angle, axis._value));
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00035131 File Offset: 0x00033331
		public static float Dot(QuaternionProxy a, QuaternionProxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Quaternion.Dot(a._value, b._value);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00035160 File Offset: 0x00033360
		public static QuaternionProxy Euler(float x, float y, float z)
		{
			return QuaternionProxy.New(Quaternion.Euler(x, y, z));
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0003516F File Offset: 0x0003336F
		public static QuaternionProxy Euler(Vector3Proxy euler)
		{
			if (euler == null)
			{
				throw new ScriptRuntimeException("argument 'euler' is nil");
			}
			return QuaternionProxy.New(Quaternion.Euler(euler._value));
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0003518F File Offset: 0x0003338F
		public static QuaternionProxy FromToRotation(Vector3Proxy fromDirection, Vector3Proxy toDirection)
		{
			if (fromDirection == null)
			{
				throw new ScriptRuntimeException("argument 'fromDirection' is nil");
			}
			if (toDirection == null)
			{
				throw new ScriptRuntimeException("argument 'toDirection' is nil");
			}
			return QuaternionProxy.New(Quaternion.FromToRotation(fromDirection._value, toDirection._value));
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x000351C3 File Offset: 0x000333C3
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x000351D6 File Offset: 0x000333D6
		public static QuaternionProxy Inverse(QuaternionProxy rotation)
		{
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			return QuaternionProxy.New(Quaternion.Inverse(rotation._value));
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x000351F6 File Offset: 0x000333F6
		public static QuaternionProxy Lerp(QuaternionProxy a, QuaternionProxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return QuaternionProxy.New(Quaternion.Lerp(a._value, b._value, t));
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x0003522B File Offset: 0x0003342B
		public static QuaternionProxy LerpUnclamped(QuaternionProxy a, QuaternionProxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return QuaternionProxy.New(Quaternion.LerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x00035260 File Offset: 0x00033460
		public static QuaternionProxy LookRotation(Vector3Proxy forward, Vector3Proxy upwards)
		{
			if (forward == null)
			{
				throw new ScriptRuntimeException("argument 'forward' is nil");
			}
			if (upwards == null)
			{
				throw new ScriptRuntimeException("argument 'upwards' is nil");
			}
			return QuaternionProxy.New(Quaternion.LookRotation(forward._value, upwards._value));
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00035294 File Offset: 0x00033494
		public static QuaternionProxy LookRotation(Vector3Proxy forward)
		{
			if (forward == null)
			{
				throw new ScriptRuntimeException("argument 'forward' is nil");
			}
			return QuaternionProxy.New(Quaternion.LookRotation(forward._value));
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x000352B4 File Offset: 0x000334B4
		public void Normalize()
		{
			this._value.Normalize();
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x000352C1 File Offset: 0x000334C1
		public static QuaternionProxy Normalize(QuaternionProxy q)
		{
			if (q == null)
			{
				throw new ScriptRuntimeException("argument 'q' is nil");
			}
			return QuaternionProxy.New(Quaternion.Normalize(q._value));
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x000352E1 File Offset: 0x000334E1
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(QuaternionProxy lhs, QuaternionProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return lhs._value == rhs._value;
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x00035310 File Offset: 0x00033510
		[MoonSharpHidden]
		public static bool operator !=(QuaternionProxy lhs, QuaternionProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return lhs._value != rhs._value;
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x0003533F File Offset: 0x0003353F
		[MoonSharpUserDataMetamethod("__mul")]
		public static QuaternionProxy operator *(QuaternionProxy lhs, QuaternionProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return QuaternionProxy.New(lhs._value * rhs._value);
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x00035373 File Offset: 0x00033573
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector3Proxy operator *(QuaternionProxy rotation, Vector3Proxy point)
		{
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(rotation._value * point._value);
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x000353A7 File Offset: 0x000335A7
		public static QuaternionProxy RotateTowards(QuaternionProxy from, QuaternionProxy to, float maxDegreesDelta)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			return QuaternionProxy.New(Quaternion.RotateTowards(from._value, to._value, maxDegreesDelta));
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x000353DC File Offset: 0x000335DC
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this._value.Set(newX, newY, newZ, newW);
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x000353EE File Offset: 0x000335EE
		public void SetFromToRotation(Vector3Proxy fromDirection, Vector3Proxy toDirection)
		{
			if (fromDirection == null)
			{
				throw new ScriptRuntimeException("argument 'fromDirection' is nil");
			}
			if (toDirection == null)
			{
				throw new ScriptRuntimeException("argument 'toDirection' is nil");
			}
			this._value.SetFromToRotation(fromDirection._value, toDirection._value);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x00035423 File Offset: 0x00033623
		public void SetLookRotation(Vector3Proxy view)
		{
			if (view == null)
			{
				throw new ScriptRuntimeException("argument 'view' is nil");
			}
			this._value.SetLookRotation(view._value);
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00035444 File Offset: 0x00033644
		public void SetLookRotation(Vector3Proxy view, Vector3Proxy up)
		{
			if (view == null)
			{
				throw new ScriptRuntimeException("argument 'view' is nil");
			}
			if (up == null)
			{
				throw new ScriptRuntimeException("argument 'up' is nil");
			}
			this._value.SetLookRotation(view._value, up._value);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x00035479 File Offset: 0x00033679
		public static QuaternionProxy Slerp(QuaternionProxy a, QuaternionProxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return QuaternionProxy.New(Quaternion.Slerp(a._value, b._value, t));
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x000354AE File Offset: 0x000336AE
		public static QuaternionProxy SlerpUnclamped(QuaternionProxy a, QuaternionProxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return QuaternionProxy.New(Quaternion.SlerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x000354E3 File Offset: 0x000336E3
		public void ToAngleAxis(out float angle, Vector3Proxy axis)
		{
			angle = 0f;
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			this._value.ToAngleAxis(out angle, out axis._value);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x0003550C File Offset: 0x0003370C
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x0003551F File Offset: 0x0003371F
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x0400317D RID: 12669
		[MoonSharpHidden]
		public Quaternion _value;
	}
}
