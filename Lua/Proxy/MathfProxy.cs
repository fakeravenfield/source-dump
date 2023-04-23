using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009D5 RID: 2517
	[Proxy(typeof(Mathf))]
	public class MathfProxy : IProxy
	{
		// Token: 0x0600475E RID: 18270 RVA: 0x00031FC9 File Offset: 0x000301C9
		[MoonSharpHidden]
		public MathfProxy(Mathf value)
		{
			this._value = value;
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00031FD8 File Offset: 0x000301D8
		public MathfProxy()
		{
			this._value = default(Mathf);
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x00031FEC File Offset: 0x000301EC
		public static float Deg2Rad
		{
			get
			{
				return 0.017453292f;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06004761 RID: 18273 RVA: 0x00031FF3 File Offset: 0x000301F3
		public static float Epsilon
		{
			get
			{
				return Mathf.Epsilon;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x00031FFA File Offset: 0x000301FA
		public static float Infinity
		{
			get
			{
				return float.PositiveInfinity;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x00032001 File Offset: 0x00030201
		public static float NegativeInfinity
		{
			get
			{
				return float.NegativeInfinity;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x00032008 File Offset: 0x00030208
		public static float PI
		{
			get
			{
				return 3.1415927f;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x0003200F File Offset: 0x0003020F
		public static float Rad2Deg
		{
			get
			{
				return 57.29578f;
			}
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x00032016 File Offset: 0x00030216
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x00032023 File Offset: 0x00030223
		[MoonSharpHidden]
		public static MathfProxy New(Mathf value)
		{
			return new MathfProxy(value);
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x0003202B File Offset: 0x0003022B
		[MoonSharpUserDataMetamethod("__call")]
		public static MathfProxy Call(DynValue _)
		{
			return new MathfProxy();
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x00032032 File Offset: 0x00030232
		public static float Abs(float f)
		{
			return Mathf.Abs(f);
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0003203A File Offset: 0x0003023A
		public static int Abs(int value)
		{
			return Mathf.Abs(value);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00032042 File Offset: 0x00030242
		public static float Acos(float f)
		{
			return Mathf.Acos(f);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x0003204A File Offset: 0x0003024A
		public static bool Approximately(float a, float b)
		{
			return Mathf.Approximately(a, b);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x00032053 File Offset: 0x00030253
		public static float Asin(float f)
		{
			return Mathf.Asin(f);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x0003205B File Offset: 0x0003025B
		public static float Atan(float f)
		{
			return Mathf.Atan(f);
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00032063 File Offset: 0x00030263
		public static float Atan2(float y, float x)
		{
			return Mathf.Atan2(y, x);
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x0003206C File Offset: 0x0003026C
		public static float Ceil(float f)
		{
			return Mathf.Ceil(f);
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x00032074 File Offset: 0x00030274
		public static int CeilToInt(float f)
		{
			return Mathf.CeilToInt(f);
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x0003207C File Offset: 0x0003027C
		public static float Clamp(float value, float min, float max)
		{
			return Mathf.Clamp(value, min, max);
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x00032086 File Offset: 0x00030286
		public static int Clamp(int value, int min, int max)
		{
			return Mathf.Clamp(value, min, max);
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x00032090 File Offset: 0x00030290
		public static float Clamp01(float value)
		{
			return Mathf.Clamp01(value);
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00032098 File Offset: 0x00030298
		public static int ClosestPowerOfTwo(int value)
		{
			return Mathf.ClosestPowerOfTwo(value);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000320A0 File Offset: 0x000302A0
		public static ColorProxy CorrelatedColorTemperatureToRGB(float kelvin)
		{
			return ColorProxy.New(Mathf.CorrelatedColorTemperatureToRGB(kelvin));
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000320AD File Offset: 0x000302AD
		public static float Cos(float f)
		{
			return Mathf.Cos(f);
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000320B5 File Offset: 0x000302B5
		public static float DeltaAngle(float current, float target)
		{
			return Mathf.DeltaAngle(current, target);
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000320BE File Offset: 0x000302BE
		public static float Exp(float power)
		{
			return Mathf.Exp(power);
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000320C6 File Offset: 0x000302C6
		public static ushort FloatToHalf(float val)
		{
			return Mathf.FloatToHalf(val);
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000320CE File Offset: 0x000302CE
		public static float Floor(float f)
		{
			return Mathf.Floor(f);
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000320D6 File Offset: 0x000302D6
		public static int FloorToInt(float f)
		{
			return Mathf.FloorToInt(f);
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000320DE File Offset: 0x000302DE
		public static float Gamma(float value, float absmax, float gamma)
		{
			return Mathf.Gamma(value, absmax, gamma);
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x000320E8 File Offset: 0x000302E8
		public static float GammaToLinearSpace(float value)
		{
			return Mathf.GammaToLinearSpace(value);
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000320F0 File Offset: 0x000302F0
		public static float HalfToFloat(ushort val)
		{
			return Mathf.HalfToFloat(val);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000320F8 File Offset: 0x000302F8
		public static float InverseLerp(float a, float b, float value)
		{
			return Mathf.InverseLerp(a, b, value);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00032102 File Offset: 0x00030302
		public static bool IsPowerOfTwo(int value)
		{
			return Mathf.IsPowerOfTwo(value);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x0003210A File Offset: 0x0003030A
		public static float Lerp(float a, float b, float t)
		{
			return Mathf.Lerp(a, b, t);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00032114 File Offset: 0x00030314
		public static float LerpAngle(float a, float b, float t)
		{
			return Mathf.LerpAngle(a, b, t);
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x0003211E File Offset: 0x0003031E
		public static float LerpUnclamped(float a, float b, float t)
		{
			return Mathf.LerpUnclamped(a, b, t);
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00032128 File Offset: 0x00030328
		public static float LinearToGammaSpace(float value)
		{
			return Mathf.LinearToGammaSpace(value);
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x00032130 File Offset: 0x00030330
		public static float Log(float f, float p)
		{
			return Mathf.Log(f, p);
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x00032139 File Offset: 0x00030339
		public static float Log(float f)
		{
			return Mathf.Log(f);
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00032141 File Offset: 0x00030341
		public static float Log10(float f)
		{
			return Mathf.Log10(f);
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x00032149 File Offset: 0x00030349
		public static float Max(float a, float b)
		{
			return Mathf.Max(a, b);
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x00032152 File Offset: 0x00030352
		public static float Max(float[] values)
		{
			return Mathf.Max(values);
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x0003215A File Offset: 0x0003035A
		public static int Max(int a, int b)
		{
			return Mathf.Max(a, b);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00032163 File Offset: 0x00030363
		public static int Max(int[] values)
		{
			return Mathf.Max(values);
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x0003216B File Offset: 0x0003036B
		public static float Min(float a, float b)
		{
			return Mathf.Min(a, b);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x00032174 File Offset: 0x00030374
		public static float Min(float[] values)
		{
			return Mathf.Min(values);
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x0003217C File Offset: 0x0003037C
		public static int Min(int a, int b)
		{
			return Mathf.Min(a, b);
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00032185 File Offset: 0x00030385
		public static int Min(int[] values)
		{
			return Mathf.Min(values);
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0003218D File Offset: 0x0003038D
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			return Mathf.MoveTowards(current, target, maxDelta);
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00032197 File Offset: 0x00030397
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			return Mathf.MoveTowardsAngle(current, target, maxDelta);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x000321A1 File Offset: 0x000303A1
		public static int NextPowerOfTwo(int value)
		{
			return Mathf.NextPowerOfTwo(value);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x000321A9 File Offset: 0x000303A9
		public static float PerlinNoise(float x, float y)
		{
			return Mathf.PerlinNoise(x, y);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x000321B2 File Offset: 0x000303B2
		public static float PingPong(float t, float length)
		{
			return Mathf.PingPong(t, length);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x000321BB File Offset: 0x000303BB
		public static float Pow(float f, float p)
		{
			return Mathf.Pow(f, p);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x000321C4 File Offset: 0x000303C4
		public static float Repeat(float t, float length)
		{
			return Mathf.Repeat(t, length);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x000321CD File Offset: 0x000303CD
		public static float Round(float f)
		{
			return Mathf.Round(f);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x000321D5 File Offset: 0x000303D5
		public static int RoundToInt(float f)
		{
			return Mathf.RoundToInt(f);
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x000321DD File Offset: 0x000303DD
		public static float Sign(float f)
		{
			return Mathf.Sign(f);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000321E5 File Offset: 0x000303E5
		public static float Sin(float f)
		{
			return Mathf.Sin(f);
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x000321ED File Offset: 0x000303ED
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x000321FA File Offset: 0x000303FA
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x00032205 File Offset: 0x00030405
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00032214 File Offset: 0x00030414
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed);
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x00032221 File Offset: 0x00030421
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0003222C File Offset: 0x0003042C
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0003223B File Offset: 0x0003043B
		public static float SmoothStep(float from, float to, float t)
		{
			return Mathf.SmoothStep(from, to, t);
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x00032245 File Offset: 0x00030445
		public static float Sqrt(float f)
		{
			return Mathf.Sqrt(f);
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0003224D File Offset: 0x0003044D
		public static float Tan(float f)
		{
			return Mathf.Tan(f);
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00032255 File Offset: 0x00030455
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400316D RID: 12653
		[MoonSharpHidden]
		public Mathf _value;
	}
}
