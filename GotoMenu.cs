using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200025E RID: 606
public class GotoMenu : MonoBehaviour
{
	// Token: 0x0600109C RID: 4252 RVA: 0x0008A444 File Offset: 0x00088644
	private void Start()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-nointro")
			{
				this.GotoNextScene();
				return;
			}
		}
		this.nextSceneAction.StartLifetime(this.duration);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0000D3C2 File Offset: 0x0000B5C2
	private void LateUpdate()
	{
		if ((Input.anyKeyDown && this.CanSkip()) || this.nextSceneAction.TrueDone())
		{
			this.GotoNextScene();
		}
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0008A48C File Offset: 0x0008868C
	private void GotoNextScene()
	{
		this.loadingIndicator.SetActive(true);
		PlayerPrefs.SetInt("SeenIntro", 1);
		PlayerPrefs.Save();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0000476F File Offset: 0x0000296F
	private bool CanSkip()
	{
		return true;
	}

	// Token: 0x040011D7 RID: 4567
	public float duration = 10f;

	// Token: 0x040011D8 RID: 4568
	public GameObject loadingIndicator;

	// Token: 0x040011D9 RID: 4569
	private TimedAction nextSceneAction = new TimedAction(1f, false);
}
