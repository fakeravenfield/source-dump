using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign.Tech
{
	// Token: 0x02000419 RID: 1049
	[Serializable]
	public class TechTreeEntry
	{
		// Token: 0x06001A2B RID: 6699 RVA: 0x000AC6A4 File Offset: 0x000AA8A4
		public TechTreeEntry(Vector2 position)
		{
			this.position = position;
			this.name = "";
			this.description = "";
			this.techId = "";
			this.price = 10;
			this.parents = new List<int>();
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000AC700 File Offset: 0x000AA900
		public int[] GetResourceTransactionAmount()
		{
			int[] array = new int[16];
			array[(int)this.techTree.defaultPriceResource] = this.price;
			foreach (ResourceAmount resourceAmount in this.additionalCosts)
			{
				array[(int)resourceAmount.type] = resourceAmount.amount;
			}
			return array;
		}

		// Token: 0x04001BD7 RID: 7127
		public Vector2 position;

		// Token: 0x04001BD8 RID: 7128
		public List<int> parents = new List<int>();

		// Token: 0x04001BD9 RID: 7129
		public bool alwaysAvailable;

		// Token: 0x04001BDA RID: 7130
		public string name;

		// Token: 0x04001BDB RID: 7131
		public string description;

		// Token: 0x04001BDC RID: 7132
		public Texture2D icon;

		// Token: 0x04001BDD RID: 7133
		public int price;

		// Token: 0x04001BDE RID: 7134
		public List<ResourceAmount> additionalCosts;

		// Token: 0x04001BDF RID: 7135
		public string techId;

		// Token: 0x04001BE0 RID: 7136
		[NonSerialized]
		public TechTree techTree;
	}
}
