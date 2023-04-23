using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000989 RID: 2441
	[Name("PlayerHud")]
	[Doc("Use these methods to change the player HUD.")]
	public static class WPlayerHud
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000299E7 File Offset: 0x00027BE7
		// (set) Token: 0x06003E02 RID: 15874 RVA: 0x000299F3 File Offset: 0x00027BF3
		[Doc("Controls visibility of the game mode hud.")]
		public static bool hudGameModeEnabled
		{
			get
			{
				return GameManager.instance.hudEnabled;
			}
			set
			{
				GameManager.instance.hudEnabled = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x00029A00 File Offset: 0x00027C00
		// (set) Token: 0x06003E04 RID: 15876 RVA: 0x00029A0F File Offset: 0x00027C0F
		[Doc("Controls visibility of the player hud.")]
		public static bool hudPlayerEnabled
		{
			get
			{
				return !IngameUi.instance.forceHide;
			}
			set
			{
				IngameUi.SetVisibility(value);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x00029F16 File Offset: 0x00028116
		// (set) Token: 0x06003E06 RID: 15878 RVA: 0x00029F22 File Offset: 0x00028122
		[Doc("Controls the Killed By sequence.")]
		public static bool killCameraEnabled
		{
			get
			{
				return KillCamera.instance.enabled;
			}
			set
			{
				KillCamera.instance.enabled = value;
			}
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00029F2F File Offset: 0x0002812F
		[Doc("Makes the rectElement automatically follow the transform position.[..] This is done by modifying the anchoredPosition value of the Rect Transform. Please make sure that the rectElement is a child to a canvas object, and that its Anchor Min/Max values are set to 0. Returns the tracker ID.")]
		public static int RegisterElementTracking(Transform transform, RectTransform rectElement, GameObject activateWhenVisible)
		{
			return WPlayerHud.RegisterElementTracking(transform, Vector3.zero, rectElement, activateWhenVisible);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x0012F1FC File Offset: 0x0012D3FC
		[Doc("Makes the rectElement automatically follow the transform position with an offset.")]
		public static int RegisterElementTracking(Transform transform, Vector3 localOffset, RectTransform rectElement, GameObject activateWhenVisible)
		{
			return IngameUiWorker.Register(new IngameUiWorker.WorkItem
			{
				transform = transform,
				target = rectElement,
				position = localOffset,
				activateOnScreen = activateWhenVisible
			});
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00029F3E File Offset: 0x0002813E
		[Doc("Makes the rectElement automatically follow the actor.")]
		public static int RegisterElementTracking(Actor actor, RectTransform rectElement, GameObject activateWhenVisible)
		{
			return WPlayerHud.RegisterElementTracking(actor, Vector3.zero, rectElement, activateWhenVisible);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x0012F238 File Offset: 0x0012D438
		[Doc("Makes the rectElement automatically follow the actor with a world offset.")]
		public static int RegisterElementTracking(Actor actor, Vector3 worldOffset, RectTransform rectElement, GameObject activateWhenVisible)
		{
			return IngameUiWorker.Register(new IngameUiWorker.WorkItem
			{
				actor = actor,
				position = worldOffset,
				target = rectElement,
				activateOnScreen = activateWhenVisible,
				clamp = false
			});
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x0012F27C File Offset: 0x0012D47C
		[Doc("Makes the rectElement automatically follow the worldPosition.")]
		public static int RegisterElementTracking(Vector3 worldPosition, RectTransform rectElement, GameObject activateWhenVisible)
		{
			return IngameUiWorker.Register(new IngameUiWorker.WorkItem
			{
				position = worldPosition,
				target = rectElement,
				activateOnScreen = activateWhenVisible,
				clamp = false
			});
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00029F4D File Offset: 0x0002814D
		[Doc("Clamps the tracked element to the screen inside a border of size clampSize in pixels.[..]")]
		public static void ClampElementTracking(int id, float clampSize, GameObject activateWhenClamped, GameObject deactivateWhenClamped)
		{
			IngameUiWorker.ApplyClamp(id, clampSize, activateWhenClamped, deactivateWhenClamped);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00029F58 File Offset: 0x00028158
		[Doc("Removes tracking from the specified tracking ID. Returns true on successful removal.")]
		public static bool RemoveElementTracking(int id)
		{
			return IngameUiWorker.Remove(id);
		}
	}
}
