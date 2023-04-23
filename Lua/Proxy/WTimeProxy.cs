using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A28 RID: 2600
	[Proxy(typeof(WTime))]
	public class WTimeProxy : IProxy
	{
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060052EB RID: 21227 RVA: 0x0003D20F File Offset: 0x0003B40F
		public static float deltaTime
		{
			get
			{
				return WTime.deltaTime;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060052EC RID: 21228 RVA: 0x0003D216 File Offset: 0x0003B416
		public static float fixedDeltaTime
		{
			get
			{
				return WTime.fixedDeltaTime;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x0003D21D File Offset: 0x0003B41D
		public static float time
		{
			get
			{
				return WTime.time;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060052EE RID: 21230 RVA: 0x0003D224 File Offset: 0x0003B424
		// (set) Token: 0x060052EF RID: 21231 RVA: 0x0003D22B File Offset: 0x0003B42B
		public static float timeScale
		{
			get
			{
				return WTime.timeScale;
			}
			set
			{
				WTime.timeScale = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060052F0 RID: 21232 RVA: 0x0003D233 File Offset: 0x0003B433
		public static float timeSinceLevelLoad
		{
			get
			{
				return WTime.timeSinceLevelLoad;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x0003D23A File Offset: 0x0003B43A
		public static float unscaledDeltaTime
		{
			get
			{
				return WTime.unscaledDeltaTime;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x0003D241 File Offset: 0x0003B441
		public static float unscaledTime
		{
			get
			{
				return WTime.unscaledTime;
			}
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}
	}
}
