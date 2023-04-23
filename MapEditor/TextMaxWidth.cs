using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006B7 RID: 1719
	public class TextMaxWidth : MonoBehaviour, ILayoutElement
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x0001DC6D File Offset: 0x0001BE6D
		public float minWidth
		{
			get
			{
				return this._minWidth;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x0001DC75 File Offset: 0x0001BE75
		public float preferredWidth
		{
			get
			{
				return this._preferredWidth;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x0001DC7D File Offset: 0x0001BE7D
		public float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0001DC7D File Offset: 0x0001BE7D
		public float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x0001DC84 File Offset: 0x0001BE84
		public float preferredHeight
		{
			get
			{
				return this._preferredHeight;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0001DC7D File Offset: 0x0001BE7D
		public float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		public int layoutPriority
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x00101720 File Offset: 0x000FF920
		public void CalculateLayoutInputHorizontal()
		{
			this._preferredWidth = -1f;
			this._minWidth = -1f;
			if (base.transform.childCount > 0)
			{
				float a = -1f;
				float num = -1f;
				for (int i = 0; i < base.transform.childCount; i++)
				{
					ILayoutElement component = base.transform.GetChild(i).GetComponent<ILayoutElement>();
					a = Mathf.Max(a, component.preferredWidth);
					num = Mathf.Max(num, component.minWidth);
				}
				this._preferredWidth = Mathf.Min(a, this.maxWidth);
				this._minWidth = (this.adhereToMin ? num : -1f);
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x001017C8 File Offset: 0x000FF9C8
		public void CalculateLayoutInputVertical()
		{
			float num = -1f;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				ILayoutElement component = base.transform.GetChild(i).GetComponent<ILayoutElement>();
				num = Mathf.Max(num, component.preferredHeight);
			}
			this._preferredHeight = num;
		}

		// Token: 0x0400282A RID: 10282
		public float maxWidth = 600f;

		// Token: 0x0400282B RID: 10283
		public bool adhereToMin;

		// Token: 0x0400282C RID: 10284
		private float _preferredWidth = -1f;

		// Token: 0x0400282D RID: 10285
		private float _preferredHeight = -1f;

		// Token: 0x0400282E RID: 10286
		private float _minWidth = -1f;
	}
}
