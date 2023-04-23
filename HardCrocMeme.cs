using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000255 RID: 597
public class HardCrocMeme : MonoBehaviour
{
	// Token: 0x06001074 RID: 4212 RVA: 0x00089468 File Offset: 0x00087668
	private void OnEnable()
	{
		if (GameManager.IsConnectedToSteam() && GameManager.instance.steamworks.GetSteamId() == 76561198043977165UL)
		{
			Dropdown component = base.GetComponent<Dropdown>();
			component.options[0].text = "Hard Croc Baby Mode (Easy peasy, perfect for when croc makes skill videos)";
			component.options[1].text = "Normal (Still kinda easy but maybe not for the croc)";
			component.options[2].text = "Hard!!! Croc (Avoid this one we don't want to lose you again for 3 months)";
		}
	}
}
