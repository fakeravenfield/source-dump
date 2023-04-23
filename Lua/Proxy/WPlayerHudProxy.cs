using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A24 RID: 2596
	[Proxy(typeof(WPlayerHud))]
	public class WPlayerHudProxy : IProxy
	{
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x0003D0A5 File Offset: 0x0003B2A5
		// (set) Token: 0x060052B2 RID: 21170 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		public static bool hudGameModeEnabled
		{
			get
			{
				return WPlayerHud.hudGameModeEnabled;
			}
			set
			{
				WPlayerHud.hudGameModeEnabled = value;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x0003D0B4 File Offset: 0x0003B2B4
		// (set) Token: 0x060052B4 RID: 21172 RVA: 0x0003D0BB File Offset: 0x0003B2BB
		public static bool hudPlayerEnabled
		{
			get
			{
				return WPlayerHud.hudPlayerEnabled;
			}
			set
			{
				WPlayerHud.hudPlayerEnabled = value;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060052B5 RID: 21173 RVA: 0x0003D0C3 File Offset: 0x0003B2C3
		// (set) Token: 0x060052B6 RID: 21174 RVA: 0x0003D0CA File Offset: 0x0003B2CA
		public static bool killCameraEnabled
		{
			get
			{
				return WPlayerHud.killCameraEnabled;
			}
			set
			{
				WPlayerHud.killCameraEnabled = value;
			}
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x00139410 File Offset: 0x00137610
		public static void ClampElementTracking(int id, float clampSize, GameObjectProxy activateWhenClamped, GameObjectProxy deactivateWhenClamped)
		{
			GameObject activateWhenClamped2 = null;
			if (activateWhenClamped != null)
			{
				activateWhenClamped2 = activateWhenClamped._value;
			}
			GameObject deactivateWhenClamped2 = null;
			if (deactivateWhenClamped != null)
			{
				deactivateWhenClamped2 = deactivateWhenClamped._value;
			}
			WPlayerHud.ClampElementTracking(id, clampSize, activateWhenClamped2, deactivateWhenClamped2);
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00139440 File Offset: 0x00137640
		public static int RegisterElementTracking(TransformProxy transform, RectTransformProxy rectElement, GameObjectProxy activateWhenVisible)
		{
			Transform transform2 = null;
			if (transform != null)
			{
				transform2 = transform._value;
			}
			RectTransform rectElement2 = null;
			if (rectElement != null)
			{
				rectElement2 = rectElement._value;
			}
			GameObject activateWhenVisible2 = null;
			if (activateWhenVisible != null)
			{
				activateWhenVisible2 = activateWhenVisible._value;
			}
			return WPlayerHud.RegisterElementTracking(transform2, rectElement2, activateWhenVisible2);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0013947C File Offset: 0x0013767C
		public static int RegisterElementTracking(TransformProxy transform, Vector3Proxy localOffset, RectTransformProxy rectElement, GameObjectProxy activateWhenVisible)
		{
			Transform transform2 = null;
			if (transform != null)
			{
				transform2 = transform._value;
			}
			if (localOffset == null)
			{
				throw new ScriptRuntimeException("argument 'localOffset' is nil");
			}
			RectTransform rectElement2 = null;
			if (rectElement != null)
			{
				rectElement2 = rectElement._value;
			}
			GameObject activateWhenVisible2 = null;
			if (activateWhenVisible != null)
			{
				activateWhenVisible2 = activateWhenVisible._value;
			}
			return WPlayerHud.RegisterElementTracking(transform2, localOffset._value, rectElement2, activateWhenVisible2);
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x001394CC File Offset: 0x001376CC
		public static int RegisterElementTracking(ActorProxy actor, RectTransformProxy rectElement, GameObjectProxy activateWhenVisible)
		{
			Actor actor2 = null;
			if (actor != null)
			{
				actor2 = actor._value;
			}
			RectTransform rectElement2 = null;
			if (rectElement != null)
			{
				rectElement2 = rectElement._value;
			}
			GameObject activateWhenVisible2 = null;
			if (activateWhenVisible != null)
			{
				activateWhenVisible2 = activateWhenVisible._value;
			}
			return WPlayerHud.RegisterElementTracking(actor2, rectElement2, activateWhenVisible2);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x00139508 File Offset: 0x00137708
		public static int RegisterElementTracking(ActorProxy actor, Vector3Proxy worldOffset, RectTransformProxy rectElement, GameObjectProxy activateWhenVisible)
		{
			Actor actor2 = null;
			if (actor != null)
			{
				actor2 = actor._value;
			}
			if (worldOffset == null)
			{
				throw new ScriptRuntimeException("argument 'worldOffset' is nil");
			}
			RectTransform rectElement2 = null;
			if (rectElement != null)
			{
				rectElement2 = rectElement._value;
			}
			GameObject activateWhenVisible2 = null;
			if (activateWhenVisible != null)
			{
				activateWhenVisible2 = activateWhenVisible._value;
			}
			return WPlayerHud.RegisterElementTracking(actor2, worldOffset._value, rectElement2, activateWhenVisible2);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00139558 File Offset: 0x00137758
		public static int RegisterElementTracking(Vector3Proxy worldPosition, RectTransformProxy rectElement, GameObjectProxy activateWhenVisible)
		{
			if (worldPosition == null)
			{
				throw new ScriptRuntimeException("argument 'worldPosition' is nil");
			}
			RectTransform rectElement2 = null;
			if (rectElement != null)
			{
				rectElement2 = rectElement._value;
			}
			GameObject activateWhenVisible2 = null;
			if (activateWhenVisible != null)
			{
				activateWhenVisible2 = activateWhenVisible._value;
			}
			return WPlayerHud.RegisterElementTracking(worldPosition._value, rectElement2, activateWhenVisible2);
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0003D0D2 File Offset: 0x0003B2D2
		public static bool RemoveElementTracking(int id)
		{
			return WPlayerHud.RemoveElementTracking(id);
		}
	}
}
