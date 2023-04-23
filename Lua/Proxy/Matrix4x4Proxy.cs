using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009D6 RID: 2518
	[Proxy(typeof(Matrix4x4))]
	public class Matrix4x4Proxy : IProxy
	{
		// Token: 0x060047A6 RID: 18342 RVA: 0x00032268 File Offset: 0x00030468
		[MoonSharpHidden]
		public Matrix4x4Proxy(Matrix4x4 value)
		{
			this._value = value;
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0013128C File Offset: 0x0012F48C
		public Matrix4x4Proxy(Vector4Proxy column0, Vector4Proxy column1, Vector4Proxy column2, Vector4Proxy column3)
		{
			if (column0 == null)
			{
				throw new ScriptRuntimeException("argument 'column0' is nil");
			}
			if (column1 == null)
			{
				throw new ScriptRuntimeException("argument 'column1' is nil");
			}
			if (column2 == null)
			{
				throw new ScriptRuntimeException("argument 'column2' is nil");
			}
			if (column3 == null)
			{
				throw new ScriptRuntimeException("argument 'column3' is nil");
			}
			this._value = new Matrix4x4(column0._value, column1._value, column2._value, column3._value);
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x00032277 File Offset: 0x00030477
		public Matrix4x4Proxy()
		{
			this._value = default(Matrix4x4);
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x0003228B File Offset: 0x0003048B
		// (set) Token: 0x060047AA RID: 18346 RVA: 0x00032298 File Offset: 0x00030498
		public float m00
		{
			get
			{
				return this._value.m00;
			}
			set
			{
				this._value.m00 = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x000322A6 File Offset: 0x000304A6
		// (set) Token: 0x060047AC RID: 18348 RVA: 0x000322B3 File Offset: 0x000304B3
		public float m01
		{
			get
			{
				return this._value.m01;
			}
			set
			{
				this._value.m01 = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060047AD RID: 18349 RVA: 0x000322C1 File Offset: 0x000304C1
		// (set) Token: 0x060047AE RID: 18350 RVA: 0x000322CE File Offset: 0x000304CE
		public float m02
		{
			get
			{
				return this._value.m02;
			}
			set
			{
				this._value.m02 = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060047AF RID: 18351 RVA: 0x000322DC File Offset: 0x000304DC
		// (set) Token: 0x060047B0 RID: 18352 RVA: 0x000322E9 File Offset: 0x000304E9
		public float m03
		{
			get
			{
				return this._value.m03;
			}
			set
			{
				this._value.m03 = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060047B1 RID: 18353 RVA: 0x000322F7 File Offset: 0x000304F7
		// (set) Token: 0x060047B2 RID: 18354 RVA: 0x00032304 File Offset: 0x00030504
		public float m10
		{
			get
			{
				return this._value.m10;
			}
			set
			{
				this._value.m10 = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060047B3 RID: 18355 RVA: 0x00032312 File Offset: 0x00030512
		// (set) Token: 0x060047B4 RID: 18356 RVA: 0x0003231F File Offset: 0x0003051F
		public float m11
		{
			get
			{
				return this._value.m11;
			}
			set
			{
				this._value.m11 = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060047B5 RID: 18357 RVA: 0x0003232D File Offset: 0x0003052D
		// (set) Token: 0x060047B6 RID: 18358 RVA: 0x0003233A File Offset: 0x0003053A
		public float m12
		{
			get
			{
				return this._value.m12;
			}
			set
			{
				this._value.m12 = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x00032348 File Offset: 0x00030548
		// (set) Token: 0x060047B8 RID: 18360 RVA: 0x00032355 File Offset: 0x00030555
		public float m13
		{
			get
			{
				return this._value.m13;
			}
			set
			{
				this._value.m13 = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x00032363 File Offset: 0x00030563
		// (set) Token: 0x060047BA RID: 18362 RVA: 0x00032370 File Offset: 0x00030570
		public float m20
		{
			get
			{
				return this._value.m20;
			}
			set
			{
				this._value.m20 = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x0003237E File Offset: 0x0003057E
		// (set) Token: 0x060047BC RID: 18364 RVA: 0x0003238B File Offset: 0x0003058B
		public float m21
		{
			get
			{
				return this._value.m21;
			}
			set
			{
				this._value.m21 = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x00032399 File Offset: 0x00030599
		// (set) Token: 0x060047BE RID: 18366 RVA: 0x000323A6 File Offset: 0x000305A6
		public float m22
		{
			get
			{
				return this._value.m22;
			}
			set
			{
				this._value.m22 = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060047BF RID: 18367 RVA: 0x000323B4 File Offset: 0x000305B4
		// (set) Token: 0x060047C0 RID: 18368 RVA: 0x000323C1 File Offset: 0x000305C1
		public float m23
		{
			get
			{
				return this._value.m23;
			}
			set
			{
				this._value.m23 = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060047C1 RID: 18369 RVA: 0x000323CF File Offset: 0x000305CF
		// (set) Token: 0x060047C2 RID: 18370 RVA: 0x000323DC File Offset: 0x000305DC
		public float m30
		{
			get
			{
				return this._value.m30;
			}
			set
			{
				this._value.m30 = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060047C3 RID: 18371 RVA: 0x000323EA File Offset: 0x000305EA
		// (set) Token: 0x060047C4 RID: 18372 RVA: 0x000323F7 File Offset: 0x000305F7
		public float m31
		{
			get
			{
				return this._value.m31;
			}
			set
			{
				this._value.m31 = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060047C5 RID: 18373 RVA: 0x00032405 File Offset: 0x00030605
		// (set) Token: 0x060047C6 RID: 18374 RVA: 0x00032412 File Offset: 0x00030612
		public float m32
		{
			get
			{
				return this._value.m32;
			}
			set
			{
				this._value.m32 = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x00032420 File Offset: 0x00030620
		// (set) Token: 0x060047C8 RID: 18376 RVA: 0x0003242D File Offset: 0x0003062D
		public float m33
		{
			get
			{
				return this._value.m33;
			}
			set
			{
				this._value.m33 = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060047C9 RID: 18377 RVA: 0x0003243B File Offset: 0x0003063B
		public float determinant
		{
			get
			{
				return this._value.determinant;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x00032448 File Offset: 0x00030648
		public static Matrix4x4Proxy identity
		{
			get
			{
				return Matrix4x4Proxy.New(Matrix4x4.identity);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060047CB RID: 18379 RVA: 0x00032454 File Offset: 0x00030654
		public Matrix4x4Proxy inverse
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.inverse);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x00032466 File Offset: 0x00030666
		public bool isIdentity
		{
			get
			{
				return this._value.isIdentity;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060047CD RID: 18381 RVA: 0x00032473 File Offset: 0x00030673
		public Vector3Proxy lossyScale
		{
			get
			{
				return Vector3Proxy.New(this._value.lossyScale);
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x00032485 File Offset: 0x00030685
		public QuaternionProxy rotation
		{
			get
			{
				return QuaternionProxy.New(this._value.rotation);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060047CF RID: 18383 RVA: 0x00032497 File Offset: 0x00030697
		public Matrix4x4Proxy transpose
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.transpose);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x000324A9 File Offset: 0x000306A9
		public static Matrix4x4Proxy zero
		{
			get
			{
				return Matrix4x4Proxy.New(Matrix4x4.zero);
			}
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x000324B5 File Offset: 0x000306B5
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000324C2 File Offset: 0x000306C2
		[MoonSharpHidden]
		public static Matrix4x4Proxy New(Matrix4x4 value)
		{
			return new Matrix4x4Proxy(value);
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x000324CA File Offset: 0x000306CA
		[MoonSharpUserDataMetamethod("__call")]
		public static Matrix4x4Proxy Call(DynValue _, Vector4Proxy column0, Vector4Proxy column1, Vector4Proxy column2, Vector4Proxy column3)
		{
			return new Matrix4x4Proxy(column0, column1, column2, column3);
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x000324D6 File Offset: 0x000306D6
		[MoonSharpUserDataMetamethod("__call")]
		public static Matrix4x4Proxy Call(DynValue _)
		{
			return new Matrix4x4Proxy();
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x000324DD File Offset: 0x000306DD
		public static float Determinant(Matrix4x4Proxy m)
		{
			if (m == null)
			{
				throw new ScriptRuntimeException("argument 'm' is nil");
			}
			return Matrix4x4.Determinant(m._value);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x000324F8 File Offset: 0x000306F8
		public static Matrix4x4Proxy Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			return Matrix4x4Proxy.New(Matrix4x4.Frustum(left, right, bottom, top, zNear, zFar));
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x0003250C File Offset: 0x0003070C
		public Vector4Proxy GetColumn(int index)
		{
			return Vector4Proxy.New(this._value.GetColumn(index));
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x0003251F File Offset: 0x0003071F
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x00032532 File Offset: 0x00030732
		public Vector4Proxy GetRow(int index)
		{
			return Vector4Proxy.New(this._value.GetRow(index));
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x00032545 File Offset: 0x00030745
		public static Matrix4x4Proxy Inverse(Matrix4x4Proxy m)
		{
			if (m == null)
			{
				throw new ScriptRuntimeException("argument 'm' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.Inverse(m._value));
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x00032565 File Offset: 0x00030765
		public static bool Inverse3DAffine(Matrix4x4Proxy input, ref Matrix4x4Proxy result)
		{
			if (input == null)
			{
				throw new ScriptRuntimeException("argument 'input' is nil");
			}
			if (result == null)
			{
				throw new ScriptRuntimeException("argument 'result' is nil");
			}
			return Matrix4x4.Inverse3DAffine(input._value, ref result._value);
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x001312FC File Offset: 0x0012F4FC
		public static Matrix4x4Proxy LookAt(Vector3Proxy from, Vector3Proxy to, Vector3Proxy up)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			if (up == null)
			{
				throw new ScriptRuntimeException("argument 'up' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.LookAt(from._value, to._value, up._value));
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x00032596 File Offset: 0x00030796
		public Vector3Proxy MultiplyPoint(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(this._value.MultiplyPoint(point._value));
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x000325BC File Offset: 0x000307BC
		public Vector3Proxy MultiplyPoint3x4(Vector3Proxy point)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return Vector3Proxy.New(this._value.MultiplyPoint3x4(point._value));
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x000325E2 File Offset: 0x000307E2
		public Vector3Proxy MultiplyVector(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3Proxy.New(this._value.MultiplyVector(vector._value));
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x00032608 File Offset: 0x00030808
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(Matrix4x4Proxy lhs, Matrix4x4Proxy rhs)
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

		// Token: 0x060047E1 RID: 18401 RVA: 0x00032637 File Offset: 0x00030837
		[MoonSharpHidden]
		public static bool operator !=(Matrix4x4Proxy lhs, Matrix4x4Proxy rhs)
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

		// Token: 0x060047E2 RID: 18402 RVA: 0x00032666 File Offset: 0x00030866
		[MoonSharpUserDataMetamethod("__mul")]
		public static Matrix4x4Proxy operator *(Matrix4x4Proxy lhs, Matrix4x4Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Matrix4x4Proxy.New(lhs._value * rhs._value);
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x0003269A File Offset: 0x0003089A
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector4Proxy operator *(Matrix4x4Proxy lhs, Vector4Proxy vector)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector4Proxy.New(lhs._value * vector._value);
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x000326CE File Offset: 0x000308CE
		public static Matrix4x4Proxy Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			return Matrix4x4Proxy.New(Matrix4x4.Ortho(left, right, bottom, top, zNear, zFar));
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x000326E2 File Offset: 0x000308E2
		public static Matrix4x4Proxy Perspective(float fov, float aspect, float zNear, float zFar)
		{
			return Matrix4x4Proxy.New(Matrix4x4.Perspective(fov, aspect, zNear, zFar));
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x000326F2 File Offset: 0x000308F2
		public static Matrix4x4Proxy Rotate(QuaternionProxy q)
		{
			if (q == null)
			{
				throw new ScriptRuntimeException("argument 'q' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.Rotate(q._value));
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x00032712 File Offset: 0x00030912
		public static Matrix4x4Proxy Scale(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.Scale(vector._value));
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x00032732 File Offset: 0x00030932
		public void SetColumn(int index, Vector4Proxy column)
		{
			if (column == null)
			{
				throw new ScriptRuntimeException("argument 'column' is nil");
			}
			this._value.SetColumn(index, column._value);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x00032754 File Offset: 0x00030954
		public void SetRow(int index, Vector4Proxy row)
		{
			if (row == null)
			{
				throw new ScriptRuntimeException("argument 'row' is nil");
			}
			this._value.SetRow(index, row._value);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x00131350 File Offset: 0x0012F550
		public void SetTRS(Vector3Proxy pos, QuaternionProxy q, Vector3Proxy s)
		{
			if (pos == null)
			{
				throw new ScriptRuntimeException("argument 'pos' is nil");
			}
			if (q == null)
			{
				throw new ScriptRuntimeException("argument 'q' is nil");
			}
			if (s == null)
			{
				throw new ScriptRuntimeException("argument 's' is nil");
			}
			this._value.SetTRS(pos._value, q._value, s._value);
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00032776 File Offset: 0x00030976
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x00032789 File Offset: 0x00030989
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x00032797 File Offset: 0x00030997
		public PlaneProxy TransformPlane(PlaneProxy plane)
		{
			if (plane == null)
			{
				throw new ScriptRuntimeException("argument 'plane' is nil");
			}
			return PlaneProxy.New(this._value.TransformPlane(plane._value));
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x000327BD File Offset: 0x000309BD
		public static Matrix4x4Proxy Translate(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.Translate(vector._value));
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x000327DD File Offset: 0x000309DD
		public static Matrix4x4Proxy Transpose(Matrix4x4Proxy m)
		{
			if (m == null)
			{
				throw new ScriptRuntimeException("argument 'm' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.Transpose(m._value));
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x001313A4 File Offset: 0x0012F5A4
		public static Matrix4x4Proxy TRS(Vector3Proxy pos, QuaternionProxy q, Vector3Proxy s)
		{
			if (pos == null)
			{
				throw new ScriptRuntimeException("argument 'pos' is nil");
			}
			if (q == null)
			{
				throw new ScriptRuntimeException("argument 'q' is nil");
			}
			if (s == null)
			{
				throw new ScriptRuntimeException("argument 's' is nil");
			}
			return Matrix4x4Proxy.New(Matrix4x4.TRS(pos._value, q._value, s._value));
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x000327FD File Offset: 0x000309FD
		public bool ValidTRS()
		{
			return this._value.ValidTRS();
		}

		// Token: 0x0400316E RID: 12654
		[MoonSharpHidden]
		public Matrix4x4 _value;
	}
}
