using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000102 RID: 258
public class SquadLeaderKit : Weapon
{
	// Token: 0x06000792 RID: 1938 RVA: 0x00063B2C File Offset: 0x00061D2C
	protected override void Update()
	{
		base.Update();
		if (this.findRangeAction.TrueDone() && this.showScopeObject)
		{
			RaycastHit raycastHit;
			if (this.Raycast(out raycastHit))
			{
				this.lastRangeReading = Mathf.RoundToInt(Mathf.Min(999f, raycastHit.distance));
			}
			else
			{
				this.lastRangeReading = 999;
			}
			this.rangeText.text = this.lastRangeReading.ToString();
			this.findRangeAction.Start();
		}
		this.animator.SetBool("using", StrategyUi.IsOpen());
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00063BC0 File Offset: 0x00061DC0
	public override void Fire(Vector3 direction, bool useMuzzleDirection)
	{
		if (!StrategyUi.IsOpen())
		{
			StrategyUi.Show();
		}
		RaycastHit raycastHit;
		if (this.aiming && this.Raycast(out raycastHit))
		{
			StrategyUi.OpenContextMenuAtWorldPoint(raycastHit.point);
		}
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00063BF8 File Offset: 0x00061DF8
	private bool Raycast(out RaycastHit hitInfo)
	{
		Camera mainCamera = GameManager.GetMainCamera();
		Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
		RaycastHit raycastHit;
		bool flag = WaterLevel.Raycast(ray, out raycastHit);
		bool flag2 = Physics.Raycast(ray, out hitInfo, 999999f, 1);
		if (flag && (!flag2 || raycastHit.distance < hitInfo.distance))
		{
			hitInfo = raycastHit;
		}
		return flag || flag2;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00006E07 File Offset: 0x00005007
	public override void Unholster()
	{
		base.Unholster();
		this.animator.SetBool("using", false);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00006E20 File Offset: 0x00005020
	public override void Holster()
	{
		if (StrategyUi.IsOpen())
		{
			StrategyUi.Hide();
		}
		this.animator.SetBool("using", false);
		base.Holster();
	}

	// Token: 0x040007A3 RID: 1955
	private const int RANGE_RAY_MASK = 4097;

	// Token: 0x040007A4 RID: 1956
	private int lastRangeReading;

	// Token: 0x040007A5 RID: 1957
	private TimedAction findRangeAction = new TimedAction(0.3f, false);

	// Token: 0x040007A6 RID: 1958
	public Canvas rangeCanvas;

	// Token: 0x040007A7 RID: 1959
	public Text rangeText;
}
