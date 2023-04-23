using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009E6 RID: 2534
	[Proxy(typeof(UnityEngine.Random))]
	public class RandomProxy : IProxy
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x0003552D File Offset: 0x0003372D
		public static Vector2Proxy insideUnitCircle
		{
			get
			{
				return Vector2Proxy.New(UnityEngine.Random.insideUnitCircle);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x00035539 File Offset: 0x00033739
		public static Vector3Proxy insideUnitSphere
		{
			get
			{
				return Vector3Proxy.New(UnityEngine.Random.insideUnitSphere);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x00035545 File Offset: 0x00033745
		public static Vector3Proxy onUnitSphere
		{
			get
			{
				return Vector3Proxy.New(UnityEngine.Random.onUnitSphere);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x00035551 File Offset: 0x00033751
		public static QuaternionProxy rotation
		{
			get
			{
				return QuaternionProxy.New(UnityEngine.Random.rotation);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x0003555D File Offset: 0x0003375D
		public static QuaternionProxy rotationUniform
		{
			get
			{
				return QuaternionProxy.New(UnityEngine.Random.rotationUniform);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x00035569 File Offset: 0x00033769
		public static float value
		{
			get
			{
				return UnityEngine.Random.value;
			}
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00035570 File Offset: 0x00033770
		public static ColorProxy ColorHSV()
		{
			return ColorProxy.New(UnityEngine.Random.ColorHSV());
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x0003557C File Offset: 0x0003377C
		public static ColorProxy ColorHSV(float hueMin, float hueMax)
		{
			return ColorProxy.New(UnityEngine.Random.ColorHSV(hueMin, hueMax));
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0003558A File Offset: 0x0003378A
		public static ColorProxy ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return ColorProxy.New(UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax));
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0003559A File Offset: 0x0003379A
		public static ColorProxy ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return ColorProxy.New(UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax));
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x000355AE File Offset: 0x000337AE
		public static ColorProxy ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			return ColorProxy.New(UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax));
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x000355C6 File Offset: 0x000337C6
		public static void InitState(int seed)
		{
			UnityEngine.Random.InitState(seed);
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x000355CE File Offset: 0x000337CE
		public static float Range(float minInclusive, float maxInclusive)
		{
			return UnityEngine.Random.Range(minInclusive, maxInclusive);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x000355D7 File Offset: 0x000337D7
		public static int Range(int minInclusive, int maxExclusive)
		{
			return UnityEngine.Random.Range(minInclusive, maxExclusive);
		}
	}
}
