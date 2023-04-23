using System;
using UnityEngine;

// Token: 0x02000310 RID: 784
public static class UpgradeTerrainSplats
{
	// Token: 0x06001453 RID: 5203 RVA: 0x00095D40 File Offset: 0x00093F40
	public static TerrainLayer[] Upgrade(SplatPrototype[] splats)
	{
		TerrainLayer[] array = new TerrainLayer[splats.Length];
		for (int i = 0; i < splats.Length; i++)
		{
			array[i] = UpgradeTerrainSplats.UpgradeSplat(splats[i]);
		}
		return array;
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0001039A File Offset: 0x0000E59A
	private static TerrainLayer UpgradeSplat(SplatPrototype splatPrototype)
	{
		return new TerrainLayer
		{
			diffuseTexture = splatPrototype.texture,
			normalMapTexture = splatPrototype.normalMap,
			tileSize = new Vector2(10f, 10f)
		};
	}
}
