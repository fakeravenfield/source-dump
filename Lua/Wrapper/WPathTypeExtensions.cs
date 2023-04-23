using System;

namespace Lua.Wrapper
{
	// Token: 0x02000985 RID: 2437
	public static class WPathTypeExtensions
	{
		// Token: 0x06003DDB RID: 15835 RVA: 0x00008D0C File Offset: 0x00006F0C
		public static PathfindingBox.Type ToPathfindingBoxType(this WPathfindingNodeType self)
		{
			return (PathfindingBox.Type)self;
		}
	}
}
