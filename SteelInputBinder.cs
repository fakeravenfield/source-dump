using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200035B RID: 859
public class SteelInputBinder : MonoBehaviour
{
	// Token: 0x060015E9 RID: 5609 RVA: 0x0009E4C0 File Offset: 0x0009C6C0
	private static void StoreInitialAxisValues()
	{
		if (SteelInputBinder.initialAxisValue == null)
		{
			SteelInputBinder.initialAxisValue = new float[SteelInput.UNITY_AXES.Length];
		}
		string[] bindableUnityAxes = SteelInput.GetBindableUnityAxes();
		for (int i = 0; i < bindableUnityAxes.Length; i++)
		{
			SteelInputBinder.initialAxisValue[i] = Input.GetAxisRaw(bindableUnityAxes[i]);
		}
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x0009E508 File Offset: 0x0009C708
	private void Start()
	{
		Button[] componentsInChildren = base.GetComponentsInChildren<Button>();
		if ((this.onlyPositive && componentsInChildren.Length < 1) || (!this.onlyPositive && componentsInChildren.Length < 2))
		{
			Debug.LogWarning("Steel Input Binder: Could not find enough buttons for input binder " + this.input.ToString());
			return;
		}
		this.positiveButton = componentsInChildren[0];
		this.positiveButton.onClick.AddListener(new UnityAction(this.PositiveRebind));
		this.positiveButtonLabel = componentsInChildren[0].GetComponentInChildren<Text>();
		if (!this.onlyPositive)
		{
			this.negativeButton = componentsInChildren[1];
			this.negativeButton.onClick.AddListener(new UnityAction(this.NegativeRebind));
			this.negativeButtonLabel = componentsInChildren[1].GetComponentInChildren<Text>();
		}
		this.UpdateLabels();
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x0009E5D0 File Offset: 0x0009C7D0
	private void Update()
	{
		if (this.waitingForRebind)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.CancelRebind();
				return;
			}
			if (Input.anyKeyDown)
			{
				foreach (object obj in SteelInput.keyCodes)
				{
					KeyCode keyCode = (KeyCode)obj;
					if (Input.GetKeyDown(keyCode))
					{
						this.BindKeyCode(keyCode);
						break;
					}
				}
			}
			string[] bindableUnityAxes = SteelInput.GetBindableUnityAxes();
			for (int i = 0; i < bindableUnityAxes.Length; i++)
			{
				float num = Input.GetAxisRaw(bindableUnityAxes[i]) - SteelInputBinder.initialAxisValue[i];
				if (Mathf.Abs(Mathf.Abs(num)) > 0.2f)
				{
					this.BindAxis(bindableUnityAxes[i], num < 0f);
				}
			}
		}
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000115B0 File Offset: 0x0000F7B0
	private void PositiveRebind()
	{
		this.positiveButtonLabel.text = "Press a button";
		this.waitingForRebind = true;
		this.rebindPositive = true;
		this.positiveButton.interactable = false;
		SteelInputBinder.StoreInitialAxisValues();
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x000115E1 File Offset: 0x0000F7E1
	private void NegativeRebind()
	{
		this.negativeButtonLabel.text = "Press a button";
		this.waitingForRebind = true;
		this.rebindPositive = false;
		this.negativeButton.interactable = false;
		SteelInputBinder.StoreInitialAxisValues();
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x0009E6A8 File Offset: 0x0009C8A8
	private void BindKeyCode(KeyCode keyCode)
	{
		if (this.rebindPositive)
		{
			SteelInput.BindPositiveKeyCode(this.input, keyCode);
			base.StartCoroutine(this.EnableButtonInteraction(this.positiveButton));
		}
		else
		{
			SteelInput.BindNegativeKeyCode(this.input, keyCode);
			base.StartCoroutine(this.EnableButtonInteraction(this.negativeButton));
		}
		this.waitingForRebind = false;
		this.UpdateLabels();
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x0009E70C File Offset: 0x0009C90C
	private void BindAxis(string axis, bool invert)
	{
		SteelInput.BindUnityAxis(this.input, axis, !this.rebindPositive ^ invert);
		base.StartCoroutine(this.EnableButtonInteraction(this.rebindPositive ? this.positiveButton : this.negativeButton));
		this.waitingForRebind = false;
		this.UpdateLabels();
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x00011612 File Offset: 0x0000F812
	private IEnumerator EnableButtonInteraction(Button button)
	{
		while (Input.GetMouseButton(0))
		{
			yield return new WaitForSecondsRealtime(0.1f);
		}
		button.interactable = true;
		yield break;
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x00011621 File Offset: 0x0000F821
	private void OnDisable()
	{
		this.CancelRebind();
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x0009E760 File Offset: 0x0009C960
	private void CancelRebind()
	{
		this.waitingForRebind = false;
		if (this.positiveButton != null)
		{
			this.positiveButton.interactable = true;
		}
		if (this.negativeButton != null)
		{
			this.negativeButton.interactable = true;
		}
		this.UpdateLabels();
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x0009E7B0 File Offset: 0x0009C9B0
	public void UpdateLabels()
	{
		if (this.positiveButtonLabel != null)
		{
			this.positiveButtonLabel.text = SteelInput.GetInput(this.input).PositiveLabel();
		}
		if (this.negativeButtonLabel != null)
		{
			this.negativeButtonLabel.text = SteelInput.GetInput(this.input).NegativeLabel();
		}
	}

	// Token: 0x04001857 RID: 6231
	private static float[] initialAxisValue;

	// Token: 0x04001858 RID: 6232
	private const string REBIND_MESSAGE = "Press a button";

	// Token: 0x04001859 RID: 6233
	private const float BIND_MIN_AXIS_CHANGE = 0.2f;

	// Token: 0x0400185A RID: 6234
	public SteelInput.KeyBinds input;

	// Token: 0x0400185B RID: 6235
	public bool onlyPositive;

	// Token: 0x0400185C RID: 6236
	private bool waitingForRebind;

	// Token: 0x0400185D RID: 6237
	private bool rebindPositive;

	// Token: 0x0400185E RID: 6238
	private Button positiveButton;

	// Token: 0x0400185F RID: 6239
	private Button negativeButton;

	// Token: 0x04001860 RID: 6240
	private Text positiveButtonLabel;

	// Token: 0x04001861 RID: 6241
	private Text negativeButtonLabel;
}
