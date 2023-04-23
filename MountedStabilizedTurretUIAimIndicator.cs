using System;
using UnityEngine;

// Token: 0x020002BD RID: 701
public class MountedStabilizedTurretUIAimIndicator : MonoBehaviour
{
	// Token: 0x060012BA RID: 4794 RVA: 0x0000ECC6 File Offset: 0x0000CEC6
	private void Awake()
	{
		this.rectTransform = (RectTransform)base.transform;
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x00090F24 File Offset: 0x0008F124
	private void Update()
	{
		try
		{
			Vector2 vector = new Vector2(MountedStabilizedTurretUIAimIndicator.GetClampedAnchorValue(this.turret.bearingTransform.localEulerAngles.y, this.turret.clampX), 1f - MountedStabilizedTurretUIAimIndicator.GetClampedAnchorValue(this.turret.pitchTransform.localEulerAngles.x, this.turret.clampY));
			this.rectTransform.anchorMin = vector;
			this.rectTransform.anchorMax = vector;
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x0000ECD9 File Offset: 0x0000CED9
	private static float GetClampedAnchorValue(float angle, MountedStabilizedTurret.Clamp clamp)
	{
		angle = Mathf.DeltaAngle(0f, angle);
		if (clamp.enabled)
		{
			return Mathf.InverseLerp(clamp.min, clamp.max, angle);
		}
		return 0.5f + angle / 360f;
	}

	// Token: 0x040013FB RID: 5115
	private RectTransform rectTransform;

	// Token: 0x040013FC RID: 5116
	public MountedStabilizedTurret turret;
}
