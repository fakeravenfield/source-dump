using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A0F RID: 2575
	[Proxy(typeof(Vector2))]
	public class Vector2Proxy : IProxy
	{
		// Token: 0x060050B3 RID: 20659 RVA: 0x0003AE1B File Offset: 0x0003901B
		[MoonSharpHidden]
		public Vector2Proxy(Vector2 value)
		{
			this._value = value;
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0003AE2A File Offset: 0x0003902A
		public Vector2Proxy(float x, float y)
		{
			this._value = new Vector2(x, y);
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0003AE3F File Offset: 0x0003903F
		public Vector2Proxy()
		{
			this._value = default(Vector2);
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x0003AE53 File Offset: 0x00039053
		public static float kEpsilon
		{
			get
			{
				return 1E-05f;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x0003AE5A File Offset: 0x0003905A
		public static float kEpsilonNormalSqrt
		{
			get
			{
				return 1E-15f;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060050B8 RID: 20664 RVA: 0x0003AE61 File Offset: 0x00039061
		// (set) Token: 0x060050B9 RID: 20665 RVA: 0x0003AE6E File Offset: 0x0003906E
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

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x0003AE7C File Offset: 0x0003907C
		// (set) Token: 0x060050BB RID: 20667 RVA: 0x0003AE89 File Offset: 0x00039089
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

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x0003AE97 File Offset: 0x00039097
		public static Vector2Proxy down
		{
			get
			{
				return Vector2Proxy.New(Vector2.down);
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x0003AEA3 File Offset: 0x000390A3
		public static Vector2Proxy left
		{
			get
			{
				return Vector2Proxy.New(Vector2.left);
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x0003AEAF File Offset: 0x000390AF
		public float magnitude
		{
			get
			{
				return this._value.magnitude;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x0003AEBC File Offset: 0x000390BC
		public static Vector2Proxy negativeInfinity
		{
			get
			{
				return Vector2Proxy.New(Vector2.negativeInfinity);
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060050C0 RID: 20672 RVA: 0x0003AEC8 File Offset: 0x000390C8
		public Vector2Proxy normalized
		{
			get
			{
				return Vector2Proxy.New(this._value.normalized);
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x0003AEDA File Offset: 0x000390DA
		public static Vector2Proxy one
		{
			get
			{
				return Vector2Proxy.New(Vector2.one);
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x0003AEE6 File Offset: 0x000390E6
		public static Vector2Proxy positiveInfinity
		{
			get
			{
				return Vector2Proxy.New(Vector2.positiveInfinity);
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x0003AEF2 File Offset: 0x000390F2
		public static Vector2Proxy right
		{
			get
			{
				return Vector2Proxy.New(Vector2.right);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060050C4 RID: 20676 RVA: 0x0003AEFE File Offset: 0x000390FE
		public float sqrMagnitude
		{
			get
			{
				return this._value.sqrMagnitude;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x0003AF0B File Offset: 0x0003910B
		public static Vector2Proxy up
		{
			get
			{
				return Vector2Proxy.New(Vector2.up);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x0003AF17 File Offset: 0x00039117
		public static Vector2Proxy zero
		{
			get
			{
				return Vector2Proxy.New(Vector2.zero);
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x0003AF23 File Offset: 0x00039123
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x0003AF30 File Offset: 0x00039130
		[MoonSharpHidden]
		public static Vector2Proxy New(Vector2 value)
		{
			return new Vector2Proxy(value);
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x0003AF38 File Offset: 0x00039138
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector2Proxy Call(DynValue _, float x, float y)
		{
			return new Vector2Proxy(x, y);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0003AF41 File Offset: 0x00039141
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector2Proxy Call(DynValue _)
		{
			return new Vector2Proxy();
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x0003AF48 File Offset: 0x00039148
		public static float Angle(Vector2Proxy from, Vector2Proxy to)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			return Vector2.Angle(from._value, to._value);
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0003AF77 File Offset: 0x00039177
		public static Vector2Proxy ClampMagnitude(Vector2Proxy vector, float maxLength)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector2Proxy.New(Vector2.ClampMagnitude(vector._value, maxLength));
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0003AF98 File Offset: 0x00039198
		public static float Distance(Vector2Proxy a, Vector2Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2.Distance(a._value, b._value);
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x0003AFC7 File Offset: 0x000391C7
		public static float Dot(Vector2Proxy lhs, Vector2Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector2.Dot(lhs._value, rhs._value);
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x0003AFF6 File Offset: 0x000391F6
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0003B009 File Offset: 0x00039209
		public static Vector2Proxy Lerp(Vector2Proxy a, Vector2Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(Vector2.Lerp(a._value, b._value, t));
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x0003B03E File Offset: 0x0003923E
		public static Vector2Proxy LerpUnclamped(Vector2Proxy a, Vector2Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(Vector2.LerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0003B073 File Offset: 0x00039273
		public static Vector2Proxy Max(Vector2Proxy lhs, Vector2Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector2Proxy.New(Vector2.Max(lhs._value, rhs._value));
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0003B0A7 File Offset: 0x000392A7
		public static Vector2Proxy Min(Vector2Proxy lhs, Vector2Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector2Proxy.New(Vector2.Min(lhs._value, rhs._value));
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0003B0DB File Offset: 0x000392DB
		public static Vector2Proxy MoveTowards(Vector2Proxy current, Vector2Proxy target, float maxDistanceDelta)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return Vector2Proxy.New(Vector2.MoveTowards(current._value, target._value, maxDistanceDelta));
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0003B110 File Offset: 0x00039310
		public void Normalize()
		{
			this._value.Normalize();
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x0003B11D File Offset: 0x0003931D
		[MoonSharpUserDataMetamethod("__add")]
		public static Vector2Proxy operator +(Vector2Proxy a, Vector2Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(a._value + b._value);
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0003B151 File Offset: 0x00039351
		[MoonSharpUserDataMetamethod("__div")]
		public static Vector2Proxy operator /(Vector2Proxy a, Vector2Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(a._value / b._value);
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0003B185 File Offset: 0x00039385
		[MoonSharpUserDataMetamethod("__div")]
		public static Vector2Proxy operator /(Vector2Proxy a, float d)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector2Proxy.New(a._value / d);
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x0003B1A6 File Offset: 0x000393A6
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(Vector2Proxy lhs, Vector2Proxy rhs)
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

		// Token: 0x060050DA RID: 20698 RVA: 0x0003B1D5 File Offset: 0x000393D5
		[MoonSharpHidden]
		public static bool operator !=(Vector2Proxy lhs, Vector2Proxy rhs)
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

		// Token: 0x060050DB RID: 20699 RVA: 0x0003B204 File Offset: 0x00039404
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector2Proxy operator *(Vector2Proxy a, Vector2Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(a._value * b._value);
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0003B238 File Offset: 0x00039438
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector2Proxy operator *(Vector2Proxy a, float d)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector2Proxy.New(a._value * d);
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0003B259 File Offset: 0x00039459
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector2Proxy operator *(float d, Vector2Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector2Proxy.New(d * a._value);
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x0003B27A File Offset: 0x0003947A
		[MoonSharpUserDataMetamethod("__sub")]
		public static Vector2Proxy operator -(Vector2Proxy a, Vector2Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(a._value - b._value);
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0003B2AE File Offset: 0x000394AE
		[MoonSharpUserDataMetamethod("__unm")]
		public static Vector2Proxy operator -(Vector2Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector2Proxy.New(-a._value);
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x0003B2CE File Offset: 0x000394CE
		public static Vector2Proxy Perpendicular(Vector2Proxy inDirection)
		{
			if (inDirection == null)
			{
				throw new ScriptRuntimeException("argument 'inDirection' is nil");
			}
			return Vector2Proxy.New(Vector2.Perpendicular(inDirection._value));
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0003B2EE File Offset: 0x000394EE
		public static Vector2Proxy Reflect(Vector2Proxy inDirection, Vector2Proxy inNormal)
		{
			if (inDirection == null)
			{
				throw new ScriptRuntimeException("argument 'inDirection' is nil");
			}
			if (inNormal == null)
			{
				throw new ScriptRuntimeException("argument 'inNormal' is nil");
			}
			return Vector2Proxy.New(Vector2.Reflect(inDirection._value, inNormal._value));
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0003B322 File Offset: 0x00039522
		public void Scale(Vector2Proxy scale)
		{
			if (scale == null)
			{
				throw new ScriptRuntimeException("argument 'scale' is nil");
			}
			this._value.Scale(scale._value);
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x0003B343 File Offset: 0x00039543
		public static Vector2Proxy Scale(Vector2Proxy a, Vector2Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector2Proxy.New(Vector2.Scale(a._value, b._value));
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0003B377 File Offset: 0x00039577
		public void Set(float newX, float newY)
		{
			this._value.Set(newX, newY);
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x0003B386 File Offset: 0x00039586
		public static float SignedAngle(Vector2Proxy from, Vector2Proxy to)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			return Vector2.SignedAngle(from._value, to._value);
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00138A00 File Offset: 0x00136C00
		public static Vector2Proxy SmoothDamp(Vector2Proxy current, Vector2Proxy target, ref Vector2Proxy currentVelocity, float smoothTime, float maxSpeed)
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
			return Vector2Proxy.New(Vector2.SmoothDamp(current._value, target._value, ref currentVelocity._value, smoothTime, maxSpeed));
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00138A58 File Offset: 0x00136C58
		public static Vector2Proxy SmoothDamp(Vector2Proxy current, Vector2Proxy target, ref Vector2Proxy currentVelocity, float smoothTime)
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
			return Vector2Proxy.New(Vector2.SmoothDamp(current._value, target._value, ref currentVelocity._value, smoothTime));
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x00138AB0 File Offset: 0x00136CB0
		public static Vector2Proxy SmoothDamp(Vector2Proxy current, Vector2Proxy target, ref Vector2Proxy currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
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
			return Vector2Proxy.New(Vector2.SmoothDamp(current._value, target._value, ref currentVelocity._value, smoothTime, maxSpeed, deltaTime));
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x0003B3B5 File Offset: 0x000395B5
		public float SqrMagnitude()
		{
			return this._value.SqrMagnitude();
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x0003B3C2 File Offset: 0x000395C2
		public static float SqrMagnitude(Vector2Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector2.SqrMagnitude(a._value);
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x0003B3DD File Offset: 0x000395DD
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0003B3F0 File Offset: 0x000395F0
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x0400329E RID: 12958
		[MoonSharpHidden]
		public Vector2 _value;
	}
}
