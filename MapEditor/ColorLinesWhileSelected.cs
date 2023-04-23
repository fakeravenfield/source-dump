using System;
using System.Linq;
using cakeslice;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000621 RID: 1569
	public class ColorLinesWhileSelected : MonoBehaviour, ISelectedNotify, IOutlineIgnore
	{
		// Token: 0x06002854 RID: 10324 RVA: 0x0001BD1B File Offset: 0x00019F1B
		private void Initialize()
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.OnInitialize();
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000FA4B8 File Offset: 0x000F86B8
		private void OnInitialize()
		{
			SelectableObject componentInParent = base.GetComponentInParent<SelectableObject>();
			this.lines = componentInParent.GetComponentsInChildren<LineRenderer>();
			this.gradients = (from L in this.lines
			select L.colorGradient).ToArray<Gradient>();
			this.selectionGradient = new Gradient
			{
				alphaKeys = new GradientAlphaKey[]
				{
					new GradientAlphaKey(1f, 0f)
				},
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(this.selectionColor, 0f)
				},
				mode = GradientMode.Fixed
			};
			foreach (LineRenderer lineRenderer in this.lines)
			{
				if (lineRenderer.GetComponent<IOutlineIgnore>() == null)
				{
					lineRenderer.gameObject.AddComponent<OutlineIgnore>();
				}
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0001BD32 File Offset: 0x00019F32
		public void Start()
		{
			this.Initialize();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000FA598 File Offset: 0x000F8798
		public void OnSelected()
		{
			this.Initialize();
			LineRenderer[] array = this.lines;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].colorGradient = this.selectionGradient;
			}
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000FA5D0 File Offset: 0x000F87D0
		public void OnDeselected()
		{
			if (this.lines != null)
			{
				for (int i = 0; i < this.lines.Length; i++)
				{
					this.lines[i].colorGradient = this.gradients[i];
				}
			}
		}

		// Token: 0x04002663 RID: 9827
		private Color selectionColor = new Color(1f, 0.73f, 0f);

		// Token: 0x04002664 RID: 9828
		private Gradient selectionGradient;

		// Token: 0x04002665 RID: 9829
		private LineRenderer[] lines;

		// Token: 0x04002666 RID: 9830
		private Gradient[] gradients;

		// Token: 0x04002667 RID: 9831
		private bool isInitialized;
	}
}
