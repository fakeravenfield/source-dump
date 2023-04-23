using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006B1 RID: 1713
	public class SingleRowGrid : MonoBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x06002B3E RID: 11070 RVA: 0x00101218 File Offset: 0x000FF418
		public void SetLayoutHorizontal()
		{
			if (!this.grid)
			{
				this.grid = base.GetComponent<GridLayoutGroup>();
			}
			int childCount = base.transform.childCount;
			if (childCount > 0)
			{
				RectTransform rectTransform = base.transform as RectTransform;
				Vector2 cellSize = this.grid.cellSize;
				cellSize.x = (rectTransform.rect.width - this.grid.spacing.x * (float)(childCount - 1)) / (float)childCount;
				this.grid.cellSize = cellSize;
			}
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0000296E File Offset: 0x00000B6E
		public void SetLayoutVertical()
		{
		}

		// Token: 0x0400280F RID: 10255
		private GridLayoutGroup grid;
	}
}
