using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x0200081D RID: 2077
	public static class PlatformAutoDetector
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x00023698 File Offset: 0x00021898
		// (set) Token: 0x060033A9 RID: 13225 RVA: 0x0002369F File Offset: 0x0002189F
		public static bool IsRunningOnMono { get; private set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000236A7 File Offset: 0x000218A7
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x000236AE File Offset: 0x000218AE
		public static bool IsRunningOnClr4 { get; private set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000236B6 File Offset: 0x000218B6
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000236BD File Offset: 0x000218BD
		public static bool IsRunningOnUnity { get; private set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000236C5 File Offset: 0x000218C5
		// (set) Token: 0x060033AF RID: 13231 RVA: 0x000236CC File Offset: 0x000218CC
		public static bool IsPortableFramework { get; private set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000236D4 File Offset: 0x000218D4
		// (set) Token: 0x060033B1 RID: 13233 RVA: 0x000236DB File Offset: 0x000218DB
		public static bool IsUnityNative { get; private set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000236E3 File Offset: 0x000218E3
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x000236EA File Offset: 0x000218EA
		public static bool IsUnityIL2CPP { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x00116C9C File Offset: 0x00114E9C
		public static bool IsRunningOnAOT
		{
			get
			{
				if (PlatformAutoDetector.m_IsRunningOnAOT == null)
				{
					try
					{
						Expression.Lambda<Func<int>>(Expression.Constant(5, typeof(int)), Array.Empty<ParameterExpression>()).Compile();
						PlatformAutoDetector.m_IsRunningOnAOT = new bool?(false);
					}
					catch (Exception)
					{
						PlatformAutoDetector.m_IsRunningOnAOT = new bool?(true);
					}
				}
				return PlatformAutoDetector.m_IsRunningOnAOT.Value;
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x00116D10 File Offset: 0x00114F10
		private static void AutoDetectPlatformFlags()
		{
			if (PlatformAutoDetector.m_AutoDetectionsDone)
			{
				return;
			}
			PlatformAutoDetector.IsRunningOnUnity = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly a) => a.SafeGetTypes()).Any((Type t) => t.FullName.StartsWith("UnityEngine."));
			PlatformAutoDetector.IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);
			PlatformAutoDetector.IsRunningOnClr4 = (Type.GetType("System.Lazy`1") != null);
			PlatformAutoDetector.m_AutoDetectionsDone = true;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000236F2 File Offset: 0x000218F2
		internal static IPlatformAccessor GetDefaultPlatform()
		{
			PlatformAutoDetector.AutoDetectPlatformFlags();
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				return new LimitedPlatformAccessor();
			}
			return new StandardPlatformAccessor();
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x0002370B File Offset: 0x0002190B
		internal static IScriptLoader GetDefaultScriptLoader()
		{
			PlatformAutoDetector.AutoDetectPlatformFlags();
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				return new UnityAssetsScriptLoader(null);
			}
			return new FileSystemScriptLoader();
		}

		// Token: 0x04002D7A RID: 11642
		private static bool? m_IsRunningOnAOT;

		// Token: 0x04002D7B RID: 11643
		private static bool m_AutoDetectionsDone;
	}
}
