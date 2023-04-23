using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020001FB RID: 507
public class MutatorEntryPanel : MonoBehaviour
{
	// Token: 0x06000D94 RID: 3476 RVA: 0x0007CF1C File Offset: 0x0007B11C
	public void SetMutator(MutatorEntry mutator)
	{
		this.mutator = mutator;
		this.title.text = this.mutator.name;
		this.description.text = this.mutator.description;
		this.image.texture = this.mutator.menuImage;
		this.UpdatePanel();
		this.toggle.onValueChanged.AddListener(delegate(bool isOn)
		{
			this.mutator.isEnabled = isOn;
		});
		if (this.mutator.configuration.HasAnyFields())
		{
			this.configButton.onClick.AddListener(new UnityAction(this.OnConfigButtonPressed));
			return;
		}
		this.configButton.gameObject.SetActive(false);
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0000AFBF File Offset: 0x000091BF
	private void OnEnable()
	{
		this.UpdatePanel();
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0000AFC7 File Offset: 0x000091C7
	private void UpdatePanel()
	{
		if (this.mutator != null)
		{
			this.toggle.isOn = this.mutator.isEnabled;
		}
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0000AFE7 File Offset: 0x000091E7
	public void OnConfigButtonPressed()
	{
		MutatorBrowser.instance.OpenMutatorConfig(this.mutator);
	}

	// Token: 0x04000EB1 RID: 3761
	public Text title;

	// Token: 0x04000EB2 RID: 3762
	public Text description;

	// Token: 0x04000EB3 RID: 3763
	public RawImage image;

	// Token: 0x04000EB4 RID: 3764
	public Toggle toggle;

	// Token: 0x04000EB5 RID: 3765
	public Button configButton;

	// Token: 0x04000EB6 RID: 3766
	private MutatorEntry mutator;
}
