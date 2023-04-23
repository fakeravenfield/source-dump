using System;

namespace Lua.Wrapper
{
	// Token: 0x0200096E RID: 2414
	[Name("IngameDialog")]
	[Doc("Use this class to play dialogs or text messages while ingame.")]
	public static class WIngameDialog
	{
		// Token: 0x06003D86 RID: 15750 RVA: 0x00029A44 File Offset: 0x00027C44
		[Doc("Print a dialog message using the specified actor pose.")]
		public static void PrintActorText(string actorPose, string text)
		{
			IngameDialog.PrintActorText(actorPose, text, "");
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x00029A52 File Offset: 0x00027C52
		[Doc("Print a dialog message using the specified actor pose, overriding the actor name.")]
		public static void PrintActorText(string actorPose, string text, string overrideName)
		{
			IngameDialog.PrintActorText(actorPose, text, overrideName);
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x00029A5C File Offset: 0x00027C5C
		[Doc("Hides the dialog box using a transition effect.")]
		public static void Hide()
		{
			IngameDialog.Hide();
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x00029A63 File Offset: 0x00027C63
		[Doc("Hides the dialog box using a transition effect after some time.")]
		public static void HideAfter(float duration)
		{
			IngameDialog.HideAfter(duration);
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x00029A5C File Offset: 0x00027C5C
		[Doc("Instantly hides the dialog box.")]
		public static void HideInstant()
		{
			IngameDialog.Hide();
		}
	}
}
