using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200096F RID: 2415
	[Wrapper(typeof(Input), includeTarget = true)]
	public static class WInput
	{
		// Token: 0x06003D8B RID: 15755 RVA: 0x00029A6B File Offset: 0x00027C6B
		[Doc("Get key bind as button.[..] Returns true while the button is held down.")]
		public static bool GetKeyBindButton(SteelInput.KeyBinds keyBind)
		{
			return SteelInput.GetButton(keyBind);
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x00029A73 File Offset: 0x00027C73
		[Doc("Get key bind as button down.[..] Returns true the frame the button was pressed.")]
		public static bool GetKeyBindButtonDown(SteelInput.KeyBinds keyBind)
		{
			return SteelInput.GetButtonDown(keyBind);
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x00029A73 File Offset: 0x00027C73
		[Doc("Get key bind as button up.[..] Returns true the frame the button was released.")]
		public static bool GetKeyBindButtonUp(SteelInput.KeyBinds keyBind)
		{
			return SteelInput.GetButtonDown(keyBind);
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x00029A7B File Offset: 0x00027C7B
		[Doc("Get key bind as axis.[..] Returns a value between -1 and 1.")]
		public static float GetKeyBindAxis(SteelInput.KeyBinds keyBind)
		{
			return SteelInput.GetAxis(keyBind);
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x00029A83 File Offset: 0x00027C83
		[Getter]
		[Doc("The current mouse sensitivity in degrees per mouse unit.[..] The mouse sensitivity changes depending on current weapon aim zoom level.")]
		public static float GetMouseSensitivity()
		{
			return FpsActorController.instance.mouseSensitivity;
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x00029A8F File Offset: 0x00027C8F
		[Doc("Disables game key bind input from number row keys.[..] This is typically used to get player input from the number keys without triggering a weapon change.")]
		public static void DisableNumberRowInputs()
		{
			SteelInput.ignoreNumberRowBinds = true;
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x00029A97 File Offset: 0x00027C97
		[Doc("Enables game key bind input from number row keys.")]
		public static void EnableNumberRowInputs()
		{
			SteelInput.ignoreNumberRowBinds = false;
		}
	}
}
