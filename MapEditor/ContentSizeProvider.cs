using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200069C RID: 1692
	[ExecuteInEditMode]
	public class ContentSizeProvider : MonoBehaviour, ILayoutElement
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0001D7A1 File Offset: 0x0001B9A1
		private Rect boundingRect
		{
			get
			{
				if (this.cachedHashCode != this.GetHashCode() || this._boundingRect == Rect.zero)
				{
					this.CalculateBoudingRect();
				}
				return this._boundingRect;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x00100C14 File Offset: 0x000FEE14
		public float minWidth
		{
			get
			{
				if (this.adjustWidth)
				{
					return this.boundingRect.width;
				}
				return -1f;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x00100C40 File Offset: 0x000FEE40
		public float preferredWidth
		{
			get
			{
				if (this.adjustWidth)
				{
					return this.boundingRect.width + (float)this.margin.horizontal;
				}
				return -1f;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x0001D7D4 File Offset: 0x0001B9D4
		public float flexibleWidth
		{
			get
			{
				if (this.margin.horizontal > 0)
				{
					return (float)this.margin.horizontal;
				}
				return -1f;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x00100C78 File Offset: 0x000FEE78
		public float minHeight
		{
			get
			{
				if (this.adjustHeight)
				{
					return this.boundingRect.height;
				}
				return -1f;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x00100CA4 File Offset: 0x000FEEA4
		public float preferredHeight
		{
			get
			{
				if (this.adjustHeight)
				{
					return this.boundingRect.height + (float)this.margin.vertical;
				}
				return -1f;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x0001D7F6 File Offset: 0x0001B9F6
		public float flexibleHeight
		{
			get
			{
				if (this.margin.vertical > 0)
				{
					return (float)this.margin.vertical;
				}
				return -1f;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x0000257D File Offset: 0x0000077D
		public int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0001D818 File Offset: 0x0001BA18
		public void CalculateLayoutInputHorizontal()
		{
			this.CalculateBoudingRect();
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0001D818 File Offset: 0x0001BA18
		public void CalculateLayoutInputVertical()
		{
			this.CalculateBoudingRect();
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00100CDC File Offset: 0x000FEEDC
		private void CalculateBoudingRect()
		{
			IEnumerable<RectTransform> enumerable;
			if (this.includeThis)
			{
				enumerable = base.GetComponentsInChildren<RectTransform>();
			}
			else
			{
				enumerable = UtilsUI.GetChildren(base.GetComponent<RectTransform>());
			}
			if (enumerable.Any<RectTransform>())
			{
				this._boundingRect = UtilsUI.GetBoundingRect(enumerable);
			}
			else
			{
				this._boundingRect = Rect.zero;
			}
			this.cachedHashCode = this.GetHashCode();
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x00100D38 File Offset: 0x000FEF38
		public override int GetHashCode()
		{
			return ((((-1486773009 * -1521134295 + base.GetHashCode()) * -1521134295 + this.adjustWidth.GetHashCode()) * -1521134295 + this.adjustHeight.GetHashCode()) * -1521134295 + this.includeThis.GetHashCode()) * -1521134295 + EqualityComparer<RectOffset>.Default.GetHashCode(this.margin);
		}

		// Token: 0x040027EA RID: 10218
		public bool adjustWidth = true;

		// Token: 0x040027EB RID: 10219
		public bool adjustHeight = true;

		// Token: 0x040027EC RID: 10220
		public bool includeThis;

		// Token: 0x040027ED RID: 10221
		public RectOffset margin = new RectOffset();

		// Token: 0x040027EE RID: 10222
		private int cachedHashCode;

		// Token: 0x040027EF RID: 10223
		private Rect _boundingRect = Rect.zero;
	}
}
