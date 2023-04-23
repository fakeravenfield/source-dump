using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

// Token: 0x02000233 RID: 563
public class FirstPersonControllerInput : MonoBehaviour
{
	// Token: 0x06000F05 RID: 3845 RVA: 0x0000BFBC File Offset: 0x0000A1BC
	private void Awake()
	{
		this.controller = base.GetComponent<FirstPersonController>();
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0000BFCA File Offset: 0x0000A1CA
	private void Update()
	{
		this.controller.SetInput(-SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal), SteelInput.GetAxis(SteelInput.KeyBinds.Vertical), SteelInput.GetButtonDown(SteelInput.KeyBinds.Jump), -SteelInput.GetAxis(SteelInput.KeyBinds.AimX), SteelInput.GetAxis(SteelInput.KeyBinds.AimY));
	}

	// Token: 0x04000FD6 RID: 4054
	private FirstPersonController controller;
}
