using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A2A RID: 2602
	[Proxy(typeof(WWater))]
	public class WWaterProxy : IProxy
	{
		// Token: 0x060052FD RID: 21245 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0003D2A3 File Offset: 0x0003B4A3
		public static float GetWaterDepth(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WWater.GetWaterDepth(position._value);
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0003D2BE File Offset: 0x0003B4BE
		public static float GetWaterLevel()
		{
			return WWater.GetWaterLevel();
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0003D2C5 File Offset: 0x0003B4C5
		public static bool IsInWater(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WWater.IsInWater(position._value);
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x0003D2E0 File Offset: 0x0003B4E0
		public static object Raycast(RayProxy ray, float range)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			return WWater.Raycast(ray._value, range);
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		public static void SetWaterLevel(float height)
		{
			WWater.SetWaterLevel(height);
		}
	}
}
