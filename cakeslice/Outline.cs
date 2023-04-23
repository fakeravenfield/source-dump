using System;
using System.Linq;
using UnityEngine;

namespace cakeslice
{
	// Token: 0x02000771 RID: 1905
	[DisallowMultipleComponent]
	public class Outline : MonoBehaviour
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x0010A55C File Offset: 0x0010875C
		public Renderer[] GetRenderers()
		{
			bool flag;
			if (this.cachedRenderers != null)
			{
				flag = (from r in this.cachedRenderers
				where !r || !r.gameObject
				select r).Any<Renderer>();
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				this.cachedRenderers = (from c in base.GetComponentsInChildren<Renderer>()
				where c.enabled && c.GetComponent<IOutlineIgnore>() == null
				select c).ToArray<Renderer>();
			}
			return this.cachedRenderers;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000204F9 File Offset: 0x0001E6F9
		private void OnEnable()
		{
			OutlineEffect.AddOutline(this);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x00020501 File Offset: 0x0001E701
		private void OnDisable()
		{
			OutlineEffect.RemoveOutline(this);
		}

		// Token: 0x04002B14 RID: 11028
		public int color;

		// Token: 0x04002B15 RID: 11029
		public bool eraseRenderer;

		// Token: 0x04002B16 RID: 11030
		[HideInInspector]
		[NonSerialized]
		public int originalLayer;

		// Token: 0x04002B17 RID: 11031
		[HideInInspector]
		[NonSerialized]
		public Material[] originalMaterials;

		// Token: 0x04002B18 RID: 11032
		private Renderer[] cachedRenderers;
	}
}
