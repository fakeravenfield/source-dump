using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200098F RID: 2447
	[Wrapper(typeof(Screen))]
	public static class WScreen
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06003E2C RID: 15916 RVA: 0x0002A06C File Offset: 0x0002826C
		[Doc("The current screen height in pixels.[..]")]
		public static float height
		{
			get
			{
				return (float)Screen.height;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x0002A074 File Offset: 0x00028274
		[Doc("The current screen width in pixels.[..]")]
		public static float width
		{
			get
			{
				return (float)Screen.width;
			}
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0002A07C File Offset: 0x0002827C
		[Doc("Unlocks the mouse cursor, typically in order to allow the player to click UI elements.[..] This function only affects the ingame cursor. It does not affect the cursor state when a menu is open, etc.")]
		public static void UnlockCursor()
		{
			FpsActorController.instance.unlockCursorRavenscriptOverride = true;
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x0002A089 File Offset: 0x00028289
		[Doc("Undo ``UnlockCursor()``.[..] This function only affects the ingame cursor. It does not affect the cursor state when a menu is open, etc.")]
		public static void LockCursor()
		{
			FpsActorController.instance.unlockCursorRavenscriptOverride = false;
		}
	}
}
