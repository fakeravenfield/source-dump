using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000987 RID: 2439
	[Name("Player")]
	[Doc("Use these methods to get the player state.")]
	public static class WPlayer
	{
		// Token: 0x06003DDD RID: 15837 RVA: 0x00029D2F File Offset: 0x00027F2F
		[Getter]
		public static WTeam GetTeam()
		{
			return (WTeam)GameManager.PlayerTeam();
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x00029D36 File Offset: 0x00027F36
		[Getter]
		public static WTeam GetEnemyTeam()
		{
			return (WTeam)Mathf.Clamp(1 - GameManager.PlayerTeam(), 0, 1);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00029D46 File Offset: 0x00027F46
		[Getter]
		public static bool GetIsSpectator()
		{
			return GameManager.IsSpectating();
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00029D4D File Offset: 0x00027F4D
		[Getter]
		public static bool GetNighvisionEnabled()
		{
			return NightVision.isEnabled;
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x00029D54 File Offset: 0x00027F54
		[Doc("Moves the first person player controller by the specified vector, respecting collisions with world geometry.")]
		public static void MoveActor(Vector3 delta)
		{
			FpsActorController.instance.controller.Move(delta);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00029D67 File Offset: 0x00027F67
		[Doc("Sets the velocity of the player controller to zero.[..] Useful for stopping falls, etc.")]
		public static void ResetVelocity()
		{
			FpsActorController.instance.controller.ResetVelocity();
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00029D78 File Offset: 0x00027F78
		[Doc("Stops the player controller from sliding on steep geometry.[..]")]
		public static void ResetSliding()
		{
			FpsActorController.instance.controller.ResetSliding();
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x00029D89 File Offset: 0x00027F89
		// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x00029D95 File Offset: 0x00027F95
		[Doc("Setting this value to false prevents the player from looking around.")]
		public static bool allowMouseLook
		{
			get
			{
				return FpsActorController.instance.allowMouseLookRavenscriptOverride;
			}
			set
			{
				FpsActorController.instance.allowMouseLookRavenscriptOverride = value;
			}
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x00029DA2 File Offset: 0x00027FA2
		[Getter]
		[Doc("Get the player squad.[..]")]
		public static Squad GetSquad()
		{
			return FpsActorController.instance.playerSquad;
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000295B7 File Offset: 0x000277B7
		[Getter]
		[Doc("Get the player actor.[..] This is the same as using ``ActorManager.playerActor``")]
		public static Actor GetActor()
		{
			return ActorManager.instance.player;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x00029DAE File Offset: 0x00027FAE
		[Getter]
		[Doc("Returns true if the player character controller is on the ground.[..]")]
		public static bool GetActorIsGrounded()
		{
			return FpsActorController.instance.OnGround();
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00029DBA File Offset: 0x00027FBA
		[Doc("Sets the player's third person actor model to only render shadows.")]
		public static void SetFirstPersonRenderMode()
		{
			FpsActorController.instance.EnableFirstPersonRenderingMode();
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x00029DC6 File Offset: 0x00027FC6
		[Doc("Sets the player's third person actor model to render normally.")]
		public static void SetThirdPersonRenderMode()
		{
			FpsActorController.instance.EnableThirdPersonRenderingMode();
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x00010CCC File Offset: 0x0000EECC
		[Getter]
		public static bool GetUseHelicopterAutoHoverMode()
		{
			return FpsActorController.instance.helicopterAutoHoverEnabled;
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00029DD2 File Offset: 0x00027FD2
		[Setter]
		public static void SetUseHelicopterAutoHoverMode(bool enabled)
		{
			FpsActorController.instance.helicopterAutoHoverEnabled = enabled;
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00029DDF File Offset: 0x00027FDF
		[Setter]
		public static void SetSelectedLoadout(WeaponManager.LoadoutSet loadout)
		{
			if (loadout == null)
			{
				loadout = new WeaponManager.LoadoutSet();
			}
			LoadoutUi.instance.loadout = loadout;
			LoadoutUi.instance.SaveCurrentLoadout();
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x0000C6AF File Offset: 0x0000A8AF
		[Getter]
		public static WeaponManager.LoadoutSet GetSelectedLoadout()
		{
			return LoadoutUi.instance.loadout;
		}
	}
}
