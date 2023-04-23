using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020002A0 RID: 672
public class HqTransmission : MonoBehaviour
{
	// Token: 0x060011D5 RID: 4565 RVA: 0x0008D444 File Offset: 0x0008B644
	private void Start()
	{
		this.backgroundPanel.canvasRenderer.SetColor(new Color(0f, 0f, 0f, 0f));
		this.fadeoutPanel.canvasRenderer.SetColor(new Color(0f, 0f, 0f, 0f));
		this.text.enabled = false;
		this.backgroundPanel.enabled = true;
		this.fadeoutPanel.enabled = true;
		base.StartCoroutine(this.RunAnimation());
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0000E03B File Offset: 0x0000C23B
	private IEnumerator RunAnimation()
	{
		this.backgroundPanel.CrossFadeAlpha(0.5f, 1f, false);
		yield return new WaitForSeconds(1f);
		int num;
		for (int i = 0; i < 5; i = num + 1)
		{
			this.audio.Play();
			this.text.enabled = true;
			yield return new WaitForSeconds(0.7f);
			this.text.enabled = false;
			yield return new WaitForSeconds(0.3f);
			num = i;
		}
		yield return new WaitForSeconds(1f);
		this.fadeoutPanel.CrossFadeAlpha(1f, 2f, false);
		yield return new WaitForSeconds(2.5f);
		SceneManager.LoadScene("Coastline UFO Cinematic");
		yield break;
	}

	// Token: 0x040012F3 RID: 4851
	public Image backgroundPanel;

	// Token: 0x040012F4 RID: 4852
	public Image fadeoutPanel;

	// Token: 0x040012F5 RID: 4853
	public Text text;

	// Token: 0x040012F6 RID: 4854
	public AudioSource audio;
}
