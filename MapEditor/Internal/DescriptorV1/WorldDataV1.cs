using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000757 RID: 1879
	[Serializable]
	public struct WorldDataV1
	{
		// Token: 0x06002EA8 RID: 11944 RVA: 0x00020183 File Offset: 0x0001E383
		public WorldDataV1(BiomeContainer biomeContainer)
		{
			this.waterLevel = biomeContainer.GetWaterLevel().transform.position.y;
			this.serializedVersion = 1;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x001092FC File Offset: 0x001074FC
		public void CopyTo(BiomeContainer biomeContainer)
		{
			if (this.serializedVersion == 0)
			{
				return;
			}
			Debug.Log("Setting water level");
			Vector3 position = new Vector3(0f, this.waterLevel, 0f);
			biomeContainer.GetWaterLevel().transform.position = position;
		}

		// Token: 0x04002AAF RID: 10927
		public float waterLevel;

		// Token: 0x04002AB0 RID: 10928
		public int serializedVersion;
	}
}
