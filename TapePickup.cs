using System;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class TapePickup : ItemPickup
{
	// Token: 0x06001081 RID: 4225 RVA: 0x0000D2E2 File Offset: 0x0000B4E2
	protected override bool Pickup()
	{
		PlayerPrefs.SetInt("unlockedmusic", 1);
		PlayerPrefs.Save();
		OverlayUi.ShowOverlayText("FOUND A TAPE!", 3.5f);
		GameManager.instance.secretTapeSound.Play();
		return true;
	}
}
