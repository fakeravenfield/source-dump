using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000178 RID: 376
public class BattleFlagPanel : MonoBehaviour
{
	// Token: 0x060009C3 RID: 2499 RVA: 0x000088B9 File Offset: 0x00006AB9
	public void SetOwner(int team)
	{
		this.targetGraphic.CrossFadeColor(ColorScheme.TeamColorBrighter(team), 0.2f, true, false);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x000088D3 File Offset: 0x00006AD3
	public void SetSpawn(SpawnPoint spawn)
	{
		this.spawn = spawn;
		this.SetOwner(this.spawn.owner);
		base.StartCoroutine(this.UpdateColorRoutine());
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x000088FA File Offset: 0x00006AFA
	private IEnumerator UpdateColorRoutine()
	{
		bool flashPendingOwner = false;
		CapturePoint flag = this.spawn as CapturePoint;
		if (!(flag != null))
		{
			yield break;
		}
		for (;;)
		{
			if (flag.owner != flag.pendingOwner)
			{
				flashPendingOwner = !flashPendingOwner;
				this.targetGraphic.CrossFadeColor(ColorScheme.TeamColor(flashPendingOwner ? flag.pendingOwner : this.spawn.owner), 0.3f, false, false);
				yield return new WaitForSeconds(0.5f);
			}
			else if (flag.isContested)
			{
				flashPendingOwner = !flashPendingOwner;
				this.targetGraphic.CrossFadeColor(ColorScheme.TeamColor(flashPendingOwner ? -1 : flag.owner), 0.3f, false, false);
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				this.targetGraphic.CrossFadeColor(ColorScheme.TeamColor(flag.owner), 0.1f, false, false);
				flashPendingOwner = false;
				yield return new WaitForSeconds(0.2f);
			}
		}
	}

	// Token: 0x04000AB3 RID: 2739
	public Graphic targetGraphic;

	// Token: 0x04000AB4 RID: 2740
	private SpawnPoint spawn;
}
