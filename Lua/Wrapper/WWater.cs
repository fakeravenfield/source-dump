using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x020009A1 RID: 2465
	[Name("Water")]
	public static class WWater
	{
		// Token: 0x06003EAF RID: 16047 RVA: 0x0002A58D File Offset: 0x0002878D
		public static bool IsInWater(Vector3 position)
		{
			return WaterLevel.IsInWater(position);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0012F4A4 File Offset: 0x0012D6A4
		public static float GetWaterDepth(Vector3 position)
		{
			float result;
			if (WaterLevel.IsInWater(position, out result))
			{
				return result;
			}
			return -1f;
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x0000A608 File Offset: 0x00008808
		public static float GetWaterLevel()
		{
			return WaterLevel.instance.height;
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0002A595 File Offset: 0x00028795
		public static void SetWaterLevel(float height)
		{
			WaterLevel.instance.height = height;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x0012F4C4 File Offset: 0x0012D6C4
		[Doc("Casts a ray through the scene until it collides with the WaterLevel or a WaterVolume.[..]")]
		[return: Doc("A RaycastHit if a collision occurs along the ray; otherwise nil.")]
		public static object Raycast([Doc("Test for collisions along this ray.")] Ray ray, [Doc("Look no further than this [meters].")] float range)
		{
			RaycastHit raycastHit;
			if (WaterLevel.Raycast(ray, out raycastHit, range))
			{
				return raycastHit;
			}
			return null;
		}
	}
}
