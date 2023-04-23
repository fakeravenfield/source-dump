using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200028D RID: 653
public class AutoRowGridLayout : UIBehaviour
{
	// Token: 0x0600116E RID: 4462 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
	protected override void Awake()
	{
		base.Awake();
		this.grid = base.GetComponent<GridLayoutGroup>();
		this.rt = (RectTransform)base.transform;
		this.initialized = true;
		this.grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0000DB0E File Offset: 0x0000BD0E
	protected override void Start()
	{
		base.Start();
		this.CalculateGridSize();
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0000DB1C File Offset: 0x0000BD1C
	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		this.CalculateGridSize();
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0008BF00 File Offset: 0x0008A100
	public void CalculateGridSize()
	{
		if (!this.initialized)
		{
			return;
		}
		this.grid.constraintCount = (int)(this.rt.rect.height / (this.grid.cellSize.y + this.grid.spacing.y));
	}

	// Token: 0x04001289 RID: 4745
	private GridLayoutGroup grid;

	// Token: 0x0400128A RID: 4746
	private bool initialized;

	// Token: 0x0400128B RID: 4747
	private RectTransform rt;
}
