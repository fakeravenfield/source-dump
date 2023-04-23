using System;
using System.Collections.Generic;

namespace Lua.Wrapper
{
	// Token: 0x0200097D RID: 2429
	[Wrapper(typeof(SpawnPointNeighborManager))]
	public static class WNeighbourManager
	{
		// Token: 0x06003DBB RID: 15803 RVA: 0x00029C0E File Offset: 0x00027E0E
		[Doc("Returns true if there is a direct land connection between the two SpawnPoints.[..] Indirect connection, through other SpawnPoints, are not tested.")]
		public static bool HasLandConnection(SpawnPoint A, SpawnPoint B)
		{
			return SpawnPointNeighborManager.HasLandConnection(A, B);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00029C17 File Offset: 0x00027E17
		[Doc("Returns true if there is a direct water connection between the two SpawnPoints.[..] Indirect connection, through other SpawnPoints, are not tested.")]
		public static bool HasWaterConnection(SpawnPoint A, SpawnPoint B)
		{
			return SpawnPointNeighborManager.HasWaterConnection(A, B);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x00029C20 File Offset: 0x00027E20
		[Doc("Returns all SpawnPoint neighbours including disabled ones.[..]")]
		public static IList<SpawnPoint> GetAllNeighbours(SpawnPoint spawnPoint)
		{
			return SpawnPointNeighborManager.GetNeighborsIncludeDisabled(spawnPoint);
		}
	}
}
