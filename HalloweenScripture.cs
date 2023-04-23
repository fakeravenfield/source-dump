using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class HalloweenScripture : MonoBehaviour
{
	// Token: 0x0600104D RID: 4173 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Start()
	{
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00088790 File Offset: 0x00086990
	private void Update()
	{
		FpsActorController instance = FpsActorController.instance;
		if (instance != null && Vector3.Distance(base.transform.position, instance.actor.Position()) < 2f && !instance.actor.dead && !instance.actor.fallenOver)
		{
			ScriptureUI.Show(this);
			PlayerPrefs.SetInt("scroll_" + this.number.ToString(), 1);
			PlayerPrefs.Save();
			try
			{
				SpiritTrack.instance.NextTrack();
			}
			catch (Exception)
			{
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001156 RID: 4438
	private const float ACTIVATE_DISTANCE = 2f;

	// Token: 0x04001157 RID: 4439
	public int number;

	// Token: 0x04001158 RID: 4440
	public string title;

	// Token: 0x04001159 RID: 4441
	[TextArea]
	public string text;
}
