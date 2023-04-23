using System;

namespace Lua.Wrapper
{
	// Token: 0x02000957 RID: 2391
	[Name("RaycastTarget")]
	[Flags]
	public enum WRaycastTarget
	{
		// Token: 0x0400310B RID: 12555
		[Doc("The ground and most static objects such as buildings and trees[..]")]
		Default = 1,
		// Token: 0x0400310C RID: 12556
		[Doc("Things that block vision such as terrain and walls[..]")]
		Opaque = 3,
		// Token: 0x0400310D RID: 12557
		[Doc("Things that block vision such as terrain and walls, including vehicles[..]")]
		OpaqueAndVehicles = 3,
		// Token: 0x0400310E RID: 12558
		Vehicle = 12,
		// Token: 0x0400310F RID: 12559
		Hitbox = 26,
		// Token: 0x04003110 RID: 12560
		[Doc("Things that stop a bullet[..]")]
		ProjectileHit = -12618245,
		// Token: 0x04003111 RID: 12561
		[Doc("Things that stop a penetrating bullet[..]")]
		PenetratingProjectileHit = -14715397,
		// Token: 0x04003112 RID: 12562
		[Doc("Things that actors can walk on[..]")]
		ActorWalkable = 2232321
	}
}
