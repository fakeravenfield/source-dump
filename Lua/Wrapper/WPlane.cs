using System;

namespace Lua.Wrapper
{
	// Token: 0x02000986 RID: 2438
	[Wrapper(typeof(Airplane), includeTarget = true, includeBase = false)]
	[Name("Airplane")]
	public static class WPlane
	{
		// Token: 0x06003DDC RID: 15836 RVA: 0x00008D0C File Offset: 0x00006F0C
		[Getter]
		public static Vehicle GetVehicle(Airplane self)
		{
			return self;
		}
	}
}
