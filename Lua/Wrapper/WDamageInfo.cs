using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000967 RID: 2407
	[Wrapper(typeof(DamageInfo), includeTarget = true)]
	public class WDamageInfo
	{
		// Token: 0x06003D6E RID: 15726 RVA: 0x000066EF File Offset: 0x000048EF
		public static DamageInfo EvaluateLastExplosionDamage(Vector3 point, bool ignoreLevelGeometry)
		{
			return ActorManager.EvaluateLastExplosionDamage(point, ignoreLevelGeometry);
		}
	}
}
