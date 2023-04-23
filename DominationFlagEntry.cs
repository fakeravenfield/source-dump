using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000175 RID: 373
public class DominationFlagEntry : MonoBehaviour
{
	// Token: 0x060009B4 RID: 2484 RVA: 0x0000884F File Offset: 0x00006A4F
	public void Initialize(CapturePoint flag)
	{
		this.flag = flag;
		base.GetComponentInChildren<Text>().text = flag.shortName;
		base.StartCoroutine(this.UpdateColorRoutine());
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00008876 File Offset: 0x00006A76
	private IEnumerator UpdateColorRoutine()
	{
		bool flashPendingOwner = false;
		for (;;)
		{
			if (this.flag.owner != this.flag.pendingOwner)
			{
				flashPendingOwner = !flashPendingOwner;
				this.flagImage.CrossFadeColor(ColorScheme.TeamColor(flashPendingOwner ? this.flag.pendingOwner : this.flag.owner), 0.3f, false, false);
				yield return new WaitForSeconds(0.5f);
			}
			else if (this.flag.isContested)
			{
				flashPendingOwner = !flashPendingOwner;
				this.flagImage.CrossFadeColor(ColorScheme.TeamColor(flashPendingOwner ? -1 : this.flag.owner), 0.3f, false, false);
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				this.flagImage.CrossFadeColor(ColorScheme.TeamColor(this.flag.owner), 0.1f, false, false);
				flashPendingOwner = false;
				yield return new WaitForSeconds(0.2f);
			}
		}
		yield break;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00008885 File Offset: 0x00006A85
	private void UpdateColor()
	{
		this.flagImage.color = ColorScheme.TeamColor(this.flag.owner);
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0006C2C8 File Offset: 0x0006A4C8
	public void Update()
	{
		this.UpdateColor();
		if (GameManager.PlayerTeam() >= 0 && !FpsActorController.instance.actor.dead)
		{
			Camera activeCamera = FpsActorController.instance.GetActiveCamera();
			Vector3 v = this.flag.transform.position - activeCamera.transform.position;
			Vector3 eulerAngles = Quaternion.FromToRotation(activeCamera.transform.forward.ToGround(), v.ToGround()).eulerAngles;
			this.circleEdge.transform.localEulerAngles = new Vector3(0f, 0f, -eulerAngles.y);
		}
	}

	// Token: 0x04000AA7 RID: 2727
	public RawImage flagImage;

	// Token: 0x04000AA8 RID: 2728
	public RawImage circleEdge;

	// Token: 0x04000AA9 RID: 2729
	private CapturePoint flag;
}
