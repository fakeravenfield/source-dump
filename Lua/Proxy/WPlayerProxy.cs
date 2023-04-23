using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A25 RID: 2597
	[Proxy(typeof(WPlayer))]
	public class WPlayerProxy : IProxy
	{
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060052C0 RID: 21184 RVA: 0x0003D0DA File Offset: 0x0003B2DA
		// (set) Token: 0x060052C1 RID: 21185 RVA: 0x0003D0E1 File Offset: 0x0003B2E1
		public static bool allowMouseLook
		{
			get
			{
				return WPlayer.allowMouseLook;
			}
			set
			{
				WPlayer.allowMouseLook = value;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060052C2 RID: 21186 RVA: 0x0003D0E9 File Offset: 0x0003B2E9
		public static ActorProxy actor
		{
			get
			{
				return ActorProxy.New(WPlayer.GetActor());
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x0003D0F5 File Offset: 0x0003B2F5
		public static bool actorIsGrounded
		{
			get
			{
				return WPlayer.GetActorIsGrounded();
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x0003D0FC File Offset: 0x0003B2FC
		public static WTeam enemyTeam
		{
			get
			{
				return WPlayer.GetEnemyTeam();
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060052C5 RID: 21189 RVA: 0x0003D103 File Offset: 0x0003B303
		public static bool isSpectator
		{
			get
			{
				return WPlayer.GetIsSpectator();
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060052C6 RID: 21190 RVA: 0x0003D10A File Offset: 0x0003B30A
		public static bool nighvisionEnabled
		{
			get
			{
				return WPlayer.GetNighvisionEnabled();
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x0003D111 File Offset: 0x0003B311
		// (set) Token: 0x060052C8 RID: 21192 RVA: 0x00139598 File Offset: 0x00137798
		public static LoadoutSetProxy selectedLoadout
		{
			get
			{
				return LoadoutSetProxy.New(WPlayer.GetSelectedLoadout());
			}
			set
			{
				WeaponManager.LoadoutSet selectedLoadout = null;
				if (value != null)
				{
					selectedLoadout = value._value;
				}
				WPlayer.SetSelectedLoadout(selectedLoadout);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x0003D11D File Offset: 0x0003B31D
		public static SquadProxy squad
		{
			get
			{
				return SquadProxy.New(WPlayer.GetSquad());
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060052CA RID: 21194 RVA: 0x0003D129 File Offset: 0x0003B329
		public static WTeam team
		{
			get
			{
				return WPlayer.GetTeam();
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x0003D130 File Offset: 0x0003B330
		// (set) Token: 0x060052CC RID: 21196 RVA: 0x0003D137 File Offset: 0x0003B337
		public static bool useHelicopterAutoHoverMode
		{
			get
			{
				return WPlayer.GetUseHelicopterAutoHoverMode();
			}
			set
			{
				WPlayer.SetUseHelicopterAutoHoverMode(value);
			}
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x0003D13F File Offset: 0x0003B33F
		public static void MoveActor(Vector3Proxy delta)
		{
			if (delta == null)
			{
				throw new ScriptRuntimeException("argument 'delta' is nil");
			}
			WPlayer.MoveActor(delta._value);
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0003D15A File Offset: 0x0003B35A
		public static void ResetSliding()
		{
			WPlayer.ResetSliding();
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x0003D161 File Offset: 0x0003B361
		public static void ResetVelocity()
		{
			WPlayer.ResetVelocity();
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x0003D168 File Offset: 0x0003B368
		public static void SetFirstPersonRenderMode()
		{
			WPlayer.SetFirstPersonRenderMode();
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0003D16F File Offset: 0x0003B36F
		public static void SetThirdPersonRenderMode()
		{
			WPlayer.SetThirdPersonRenderMode();
		}
	}
}
