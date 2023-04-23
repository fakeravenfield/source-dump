using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A10 RID: 2576
	[Proxy(typeof(Vector3))]
	public class Vector3Proxy : IProxy
	{
		// Token: 0x060050ED RID: 20717 RVA: 0x0003B3FE File Offset: 0x000395FE
		[MoonSharpHidden]
		public Vector3Proxy(Vector3 value)
		{
			this._value = value;
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x0003B40D File Offset: 0x0003960D
		public Vector3Proxy(float x, float y, float z)
		{
			this._value = new Vector3(x, y, z);
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0003B423 File Offset: 0x00039623
		public Vector3Proxy(float x, float y)
		{
			this._value = new Vector3(x, y);
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0003B438 File Offset: 0x00039638
		public Vector3Proxy()
		{
			this._value = default(Vector3);
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x0003AE53 File Offset: 0x00039053
		public static float kEpsilon
		{
			get
			{
				return 1E-05f;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x0003AE5A File Offset: 0x0003905A
		public static float kEpsilonNormalSqrt
		{
			get
			{
				return 1E-15f;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060050F3 RID: 20723 RVA: 0x0003B44C File Offset: 0x0003964C
		// (set) Token: 0x060050F4 RID: 20724 RVA: 0x0003B459 File Offset: 0x00039659
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

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060050F5 RID: 20725 RVA: 0x0003B467 File Offset: 0x00039667
		// (set) Token: 0x060050F6 RID: 20726 RVA: 0x0003B474 File Offset: 0x00039674
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

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x0003B482 File Offset: 0x00039682
		// (set) Token: 0x060050F8 RID: 20728 RVA: 0x0003B48F File Offset: 0x0003968F
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

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060050F9 RID: 20729 RVA: 0x0003B49D File Offset: 0x0003969D
		public static Vector3Proxy back
		{
			get
			{
				return Vector3Proxy.New(Vector3.back);
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x0003B4A9 File Offset: 0x000396A9
		public static Vector3Proxy down
		{
			get
			{
				return Vector3Proxy.New(Vector3.down);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x0003B4B5 File Offset: 0x000396B5
		public static Vector3Proxy forward
		{
			get
			{
				return Vector3Proxy.New(Vector3.forward);
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x0003B4C1 File Offset: 0x000396C1
		public static Vector3Proxy left
		{
			get
			{
				return Vector3Proxy.New(Vector3.left);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x0003B4CD File Offset: 0x000396CD
		public float magnitude
		{
			get
			{
				return this._value.magnitude;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x0003B4DA File Offset: 0x000396DA
		public static Vector3Proxy negativeInfinity
		{
			get
			{
				return Vector3Proxy.New(Vector3.negativeInfinity);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x0003B4E6 File Offset: 0x000396E6
		public Vector3Proxy normalized
		{
			get
			{
				return Vector3Proxy.New(this._value.normalized);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x0003B4F8 File Offset: 0x000396F8
		public static Vector3Proxy one
		{
			get
			{
				return Vector3Proxy.New(Vector3.one);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x0003B504 File Offset: 0x00039704
		public static Vector3Proxy positiveInfinity
		{
			get
			{
				return Vector3Proxy.New(Vector3.positiveInfinity);
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x0003B510 File Offset: 0x00039710
		public static Vector3Proxy right
		{
			get
			{
				return Vector3Proxy.New(Vector3.right);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x0003B51C File Offset: 0x0003971C
		public float sqrMagnitude
		{
			get
			{
				return this._value.sqrMagnitude;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x0003B529 File Offset: 0x00039729
		public static Vector3Proxy up
		{
			get
			{
				return Vector3Proxy.New(Vector3.up);
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x0003B535 File Offset: 0x00039735
		public static Vector3Proxy zero
		{
			get
			{
				return Vector3Proxy.New(Vector3.zero);
			}
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x0003B541 File Offset: 0x00039741
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x0003B54E File Offset: 0x0003974E
		[MoonSharpHidden]
		public static Vector3Proxy New(Vector3 value)
		{
			return new Vector3Proxy(value);
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0003B556 File Offset: 0x00039756
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector3Proxy Call(DynValue _, float x, float y, float z)
		{
			return new Vector3Proxy(x, y, z);
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x0003B560 File Offset: 0x00039760
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector3Proxy Call(DynValue _, float x, float y)
		{
			return new Vector3Proxy(x, y);
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x0003B569 File Offset: 0x00039769
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector3Proxy Call(DynValue _)
		{
			return new Vector3Proxy();
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x0003B570 File Offset: 0x00039770
		public static float Angle(Vector3Proxy from, Vector3Proxy to)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			return Vector3.Angle(from._value, to._value);
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0003B59F File Offset: 0x0003979F
		public static Vector3Proxy ClampMagnitude(Vector3Proxy vector, float maxLength)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3Proxy.New(Vector3.ClampMagnitude(vector._value, maxLength));
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0003B5C0 File Offset: 0x000397C0
		public static Vector3Proxy Cross(Vector3Proxy lhs, Vector3Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector3Proxy.New(Vector3.Cross(lhs._value, rhs._value));
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x0003B5F4 File Offset: 0x000397F4
		public static float Distance(Vector3Proxy a, Vector3Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3.Distance(a._value, b._value);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x0003B623 File Offset: 0x00039823
		public static float Dot(Vector3Proxy lhs, Vector3Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector3.Dot(lhs._value, rhs._value);
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0003B652 File Offset: 0x00039852
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x0003B665 File Offset: 0x00039865
		public static Vector3Proxy Lerp(Vector3Proxy a, Vector3Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(Vector3.Lerp(a._value, b._value, t));
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0003B69A File Offset: 0x0003989A
		public static Vector3Proxy LerpUnclamped(Vector3Proxy a, Vector3Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(Vector3.LerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0003B6CF File Offset: 0x000398CF
		public static float Magnitude(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3.Magnitude(vector._value);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0003B6EA File Offset: 0x000398EA
		public static Vector3Proxy Max(Vector3Proxy lhs, Vector3Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector3Proxy.New(Vector3.Max(lhs._value, rhs._value));
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x0003B71E File Offset: 0x0003991E
		public static Vector3Proxy Min(Vector3Proxy lhs, Vector3Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector3Proxy.New(Vector3.Min(lhs._value, rhs._value));
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0003B752 File Offset: 0x00039952
		public static Vector3Proxy MoveTowards(Vector3Proxy current, Vector3Proxy target, float maxDistanceDelta)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return Vector3Proxy.New(Vector3.MoveTowards(current._value, target._value, maxDistanceDelta));
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x0003B787 File Offset: 0x00039987
		public void Normalize()
		{
			this._value.Normalize();
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x0003B794 File Offset: 0x00039994
		public static Vector3Proxy Normalize(Vector3Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			return Vector3Proxy.New(Vector3.Normalize(value._value));
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x0003B7B4 File Offset: 0x000399B4
		[MoonSharpUserDataMetamethod("__add")]
		public static Vector3Proxy operator +(Vector3Proxy a, Vector3Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(a._value + b._value);
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x0003B7E8 File Offset: 0x000399E8
		[MoonSharpUserDataMetamethod("__div")]
		public static Vector3Proxy operator /(Vector3Proxy a, float d)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector3Proxy.New(a._value / d);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0003B809 File Offset: 0x00039A09
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(Vector3Proxy lhs, Vector3Proxy rhs)
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

		// Token: 0x0600511C RID: 20764 RVA: 0x0003B838 File Offset: 0x00039A38
		[MoonSharpHidden]
		public static bool operator !=(Vector3Proxy lhs, Vector3Proxy rhs)
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

		// Token: 0x0600511D RID: 20765 RVA: 0x0003B867 File Offset: 0x00039A67
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector3Proxy operator *(Vector3Proxy a, float d)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector3Proxy.New(a._value * d);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x0003B888 File Offset: 0x00039A88
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector3Proxy operator *(float d, Vector3Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector3Proxy.New(d * a._value);
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x0003B8A9 File Offset: 0x00039AA9
		[MoonSharpUserDataMetamethod("__sub")]
		public static Vector3Proxy operator -(Vector3Proxy a, Vector3Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(a._value - b._value);
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0003B8DD File Offset: 0x00039ADD
		[MoonSharpUserDataMetamethod("__unm")]
		public static Vector3Proxy operator -(Vector3Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector3Proxy.New(-a._value);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0003B8FD File Offset: 0x00039AFD
		public static void OrthoNormalize(ref Vector3Proxy normal, ref Vector3Proxy tangent)
		{
			if (normal == null)
			{
				throw new ScriptRuntimeException("argument 'normal' is nil");
			}
			if (tangent == null)
			{
				throw new ScriptRuntimeException("argument 'tangent' is nil");
			}
			Vector3.OrthoNormalize(ref normal._value, ref tangent._value);
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00138B0C File Offset: 0x00136D0C
		public static void OrthoNormalize(ref Vector3Proxy normal, ref Vector3Proxy tangent, ref Vector3Proxy binormal)
		{
			if (normal == null)
			{
				throw new ScriptRuntimeException("argument 'normal' is nil");
			}
			if (tangent == null)
			{
				throw new ScriptRuntimeException("argument 'tangent' is nil");
			}
			if (binormal == null)
			{
				throw new ScriptRuntimeException("argument 'binormal' is nil");
			}
			Vector3.OrthoNormalize(ref normal._value, ref tangent._value, ref binormal._value);
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x0003B930 File Offset: 0x00039B30
		public static Vector3Proxy Project(Vector3Proxy vector, Vector3Proxy onNormal)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			if (onNormal == null)
			{
				throw new ScriptRuntimeException("argument 'onNormal' is nil");
			}
			return Vector3Proxy.New(Vector3.Project(vector._value, onNormal._value));
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x0003B964 File Offset: 0x00039B64
		public static Vector3Proxy ProjectOnPlane(Vector3Proxy vector, Vector3Proxy planeNormal)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			if (planeNormal == null)
			{
				throw new ScriptRuntimeException("argument 'planeNormal' is nil");
			}
			return Vector3Proxy.New(Vector3.ProjectOnPlane(vector._value, planeNormal._value));
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x0003B998 File Offset: 0x00039B98
		public static Vector3Proxy Reflect(Vector3Proxy inDirection, Vector3Proxy inNormal)
		{
			if (inDirection == null)
			{
				throw new ScriptRuntimeException("argument 'inDirection' is nil");
			}
			if (inNormal == null)
			{
				throw new ScriptRuntimeException("argument 'inNormal' is nil");
			}
			return Vector3Proxy.New(Vector3.Reflect(inDirection._value, inNormal._value));
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x0003B9CC File Offset: 0x00039BCC
		public static Vector3Proxy RotateTowards(Vector3Proxy current, Vector3Proxy target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return Vector3Proxy.New(Vector3.RotateTowards(current._value, target._value, maxRadiansDelta, maxMagnitudeDelta));
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x0003BA02 File Offset: 0x00039C02
		public void Scale(Vector3Proxy scale)
		{
			if (scale == null)
			{
				throw new ScriptRuntimeException("argument 'scale' is nil");
			}
			this._value.Scale(scale._value);
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x0003BA23 File Offset: 0x00039C23
		public static Vector3Proxy Scale(Vector3Proxy a, Vector3Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(Vector3.Scale(a._value, b._value));
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x0003BA57 File Offset: 0x00039C57
		public void Set(float newX, float newY, float newZ)
		{
			this._value.Set(newX, newY, newZ);
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00138B60 File Offset: 0x00136D60
		public static float SignedAngle(Vector3Proxy from, Vector3Proxy to, Vector3Proxy axis)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			return Vector3.SignedAngle(from._value, to._value, axis._value);
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x0003BA67 File Offset: 0x00039C67
		public static Vector3Proxy Slerp(Vector3Proxy a, Vector3Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(Vector3.Slerp(a._value, b._value, t));
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x0003BA9C File Offset: 0x00039C9C
		public static Vector3Proxy SlerpUnclamped(Vector3Proxy a, Vector3Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector3Proxy.New(Vector3.SlerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x00138BB0 File Offset: 0x00136DB0
		public static Vector3Proxy SmoothDamp(Vector3Proxy current, Vector3Proxy target, ref Vector3Proxy currentVelocity, float smoothTime, float maxSpeed)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			if (currentVelocity == null)
			{
				throw new ScriptRuntimeException("argument 'currentVelocity' is nil");
			}
			return Vector3Proxy.New(Vector3.SmoothDamp(current._value, target._value, ref currentVelocity._value, smoothTime, maxSpeed));
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x00138C08 File Offset: 0x00136E08
		public static Vector3Proxy SmoothDamp(Vector3Proxy current, Vector3Proxy target, ref Vector3Proxy currentVelocity, float smoothTime)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			if (currentVelocity == null)
			{
				throw new ScriptRuntimeException("argument 'currentVelocity' is nil");
			}
			return Vector3Proxy.New(Vector3.SmoothDamp(current._value, target._value, ref currentVelocity._value, smoothTime));
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x00138C60 File Offset: 0x00136E60
		public static Vector3Proxy SmoothDamp(Vector3Proxy current, Vector3Proxy target, ref Vector3Proxy currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			if (currentVelocity == null)
			{
				throw new ScriptRuntimeException("argument 'currentVelocity' is nil");
			}
			return Vector3Proxy.New(Vector3.SmoothDamp(current._value, target._value, ref currentVelocity._value, smoothTime, maxSpeed, deltaTime));
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0003BAD1 File Offset: 0x00039CD1
		public static float SqrMagnitude(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3.SqrMagnitude(vector._value);
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x0003BAFF File Offset: 0x00039CFF
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x0400329F RID: 12959
		[MoonSharpHidden]
		public Vector3 _value;
	}
}
