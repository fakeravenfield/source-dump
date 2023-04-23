using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class TimedDetonation : MonoBehaviour
{
	// Token: 0x060006D6 RID: 1750 RVA: 0x00006610 File Offset: 0x00004810
	private void Start()
	{
		this.target = base.GetComponent<RemoteDetonatedProjectile>();
		this.timerAction.StartLifetime(this.delay);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0000662F File Offset: 0x0000482F
	private void Update()
	{
		if (this.timerAction.TrueDone())
		{
			if (this.target != null)
			{
				this.target.Detonate();
			}
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0000665D File Offset: 0x0000485D
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x040006CE RID: 1742
	private RemoteDetonatedProjectile target;

	// Token: 0x040006CF RID: 1743
	public float delay = 30f;

	// Token: 0x040006D0 RID: 1744
	private TimedAction timerAction = new TimedAction(1f, false);
}
