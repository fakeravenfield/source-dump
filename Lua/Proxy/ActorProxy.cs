using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009AA RID: 2474
	[Proxy(typeof(Actor))]
	public class ActorProxy : IProxy
	{
		// Token: 0x06003F49 RID: 16201 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		[MoonSharpHidden]
		public ActorProxy(Actor value)
		{
			this._value = value;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x0002ABF7 File Offset: 0x00028DF7
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x0002AC09 File Offset: 0x00028E09
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x0002AC1B File Offset: 0x00028E1B
		public bool canBeSeated
		{
			get
			{
				return WActor.CanBeSeated(this._value);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x0002AC28 File Offset: 0x00028E28
		public bool canCapturePoint
		{
			get
			{
				return WActor.CanCapturePoint(this._value);
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x0002AC35 File Offset: 0x00028E35
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x0002AC42 File Offset: 0x00028E42
		public bool canDeployParachute
		{
			get
			{
				return WActor.CanDeployParachute(this._value);
			}
			set
			{
				WActor.CanDeployParachute(this._value, value);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x0002AC50 File Offset: 0x00028E50
		public SeatProxy activeSeat
		{
			get
			{
				return SeatProxy.New(WActor.GetActiveSeat(this._value));
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x0002AC62 File Offset: 0x00028E62
		public WeaponProxy activeWeapon
		{
			get
			{
				return WeaponProxy.New(WActor.GetActiveWeapon(this._value));
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x0002AC74 File Offset: 0x00028E74
		public VehicleProxy activeVehicle
		{
			get
			{
				return VehicleProxy.New(WActor.GetActiveVehicle(this._value));
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x0002AC86 File Offset: 0x00028E86
		public int actorIndex
		{
			get
			{
				return WActor.GetActorIndex(this._value);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x0002AC93 File Offset: 0x00028E93
		public int actorTeamIndex
		{
			get
			{
				return WActor.GetActorTeamIndex(this._value);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x0002ACA0 File Offset: 0x00028EA0
		public AiActorControllerProxy aiController
		{
			get
			{
				return AiActorControllerProxy.New(WActor.GetAiController(this._value));
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x0002ACB2 File Offset: 0x00028EB2
		// (set) Token: 0x06003F57 RID: 16215 RVA: 0x0002ACBF File Offset: 0x00028EBF
		public float balance
		{
			get
			{
				return WActor.GetBalance(this._value);
			}
			set
			{
				WActor.SetBalance(this._value, value);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x0002ACCD File Offset: 0x00028ECD
		public Vector3Proxy centerPosition
		{
			get
			{
				return Vector3Proxy.New(WActor.GetCenterPosition(this._value));
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x0002ACDF File Offset: 0x00028EDF
		public CapturePointProxy currentCapturePoint
		{
			get
			{
				return CapturePointProxy.New(WActor.GetCurrentCapturePoint(this._value));
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x0002ACF1 File Offset: 0x00028EF1
		// (set) Token: 0x06003F5B RID: 16219 RVA: 0x0002ACFE File Offset: 0x00028EFE
		public bool dropsAmmoOnKick
		{
			get
			{
				return WActor.GetDropsAmmoOnKick(this._value);
			}
			set
			{
				WActor.SetDropsAmmoOnKick(this._value, value);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x0002AD0C File Offset: 0x00028F0C
		public Vector3Proxy facingDirection
		{
			get
			{
				return Vector3Proxy.New(WActor.GetFacingDirection(this._value));
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x0002AD1E File Offset: 0x00028F1E
		// (set) Token: 0x06003F5E RID: 16222 RVA: 0x0002AD2B File Offset: 0x00028F2B
		public float health
		{
			get
			{
				return WActor.GetHealth(this._value);
			}
			set
			{
				WActor.SetHealth(this._value, value);
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x0002AD39 File Offset: 0x00028F39
		// (set) Token: 0x06003F60 RID: 16224 RVA: 0x0002AD46 File Offset: 0x00028F46
		public bool hitboxCollidersAreEnabled
		{
			get
			{
				return WActor.GetHitboxCollidersAreEnabled(this._value);
			}
			set
			{
				WActor.SetHitboxCollidersAreEnabled(this._value, value);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x0002AD54 File Offset: 0x00028F54
		public bool isAtResupplyCrate
		{
			get
			{
				return WActor.GetIsAtResupplyCrate(this._value);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x0002AD61 File Offset: 0x00028F61
		public bool isDeactivated
		{
			get
			{
				return WActor.GetIsDeactivated(this._value);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x0002AD6E File Offset: 0x00028F6E
		// (set) Token: 0x06003F64 RID: 16228 RVA: 0x0002AD7B File Offset: 0x00028F7B
		public bool isFrozen
		{
			get
			{
				return WActor.GetIsFrozen(this._value);
			}
			set
			{
				WActor.SetIsFrozen(this._value, value);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x0002AD89 File Offset: 0x00028F89
		public bool isReadyToSpawn
		{
			get
			{
				return WActor.GetIsReadyToSpawn(this._value);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x0002AD96 File Offset: 0x00028F96
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x0002ADA3 File Offset: 0x00028FA3
		public bool isRendered
		{
			get
			{
				return WActor.GetIsRendered(this._value);
			}
			set
			{
				WActor.SetIsRendered(this._value, value);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x0002ADB1 File Offset: 0x00028FB1
		public bool isResupplyingAmmo
		{
			get
			{
				return WActor.GetIsResupplyingAmmo(this._value);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x0002ADBE File Offset: 0x00028FBE
		public bool isResupplyingHealth
		{
			get
			{
				return WActor.GetIsResupplyingHealth(this._value);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x0002ADCB File Offset: 0x00028FCB
		public LadderProxy ladder
		{
			get
			{
				return LadderProxy.New(WActor.GetLadder(this._value));
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x0002ADDD File Offset: 0x00028FDD
		// (set) Token: 0x06003F6C RID: 16236 RVA: 0x0002ADEA File Offset: 0x00028FEA
		public float maxBalance
		{
			get
			{
				return WActor.GetMaxBalance(this._value);
			}
			set
			{
				WActor.SetMaxBalance(this._value, value);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x0002ADF8 File Offset: 0x00028FF8
		// (set) Token: 0x06003F6E RID: 16238 RVA: 0x0002AE05 File Offset: 0x00029005
		public float maxHealth
		{
			get
			{
				return WActor.GetMaxHealth(this._value);
			}
			set
			{
				WActor.SetMaxHealth(this._value, value);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x0002AE13 File Offset: 0x00029013
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x0002AE20 File Offset: 0x00029020
		public string name
		{
			get
			{
				return WActor.GetName(this._value);
			}
			set
			{
				WActor.SetName(this._value, value);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x0002AE2E File Offset: 0x0002902E
		public Vector3Proxy position
		{
			get
			{
				return Vector3Proxy.New(WActor.GetPosition(this._value));
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x0002AE40 File Offset: 0x00029040
		public QuaternionProxy rotation
		{
			get
			{
				return QuaternionProxy.New(WActor.GetRotation(this._value));
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x0002AE52 File Offset: 0x00029052
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x0002AE5F File Offset: 0x0002905F
		public float speedMultiplier
		{
			get
			{
				return WActor.GetSpeedMultiplier(this._value);
			}
			set
			{
				WActor.SetSpeedMultiplier(this._value, value);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x0002AE6D File Offset: 0x0002906D
		public SquadProxy squad
		{
			get
			{
				return SquadProxy.New(WActor.GetSquad(this._value));
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x0002AE7F File Offset: 0x0002907F
		public WTeam team
		{
			get
			{
				return WActor.GetTeam(this._value);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x0002AE8C File Offset: 0x0002908C
		public IList<Weapon> weaponSlots
		{
			get
			{
				return WActor.GetWeaponSlots(this._value);
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x0002AE99 File Offset: 0x00029099
		public Vector3Proxy velocity
		{
			get
			{
				return Vector3Proxy.New(WActor.GetVelocity(this._value));
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x0002AEAB File Offset: 0x000290AB
		public bool isAiming
		{
			get
			{
				return WActor.IsAiming(this._value);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x0002AEB8 File Offset: 0x000290B8
		public bool isBot
		{
			get
			{
				return WActor.IsBot(this._value);
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x0002AEC5 File Offset: 0x000290C5
		public bool isCrouching
		{
			get
			{
				return WActor.IsCrouching(this._value);
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x0002AED2 File Offset: 0x000290D2
		public bool isDead
		{
			get
			{
				return WActor.IsDead(this._value);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x0002AEDF File Offset: 0x000290DF
		public bool isDriver
		{
			get
			{
				return WActor.IsDriver(this._value);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x0002AEEC File Offset: 0x000290EC
		public bool isFallenOver
		{
			get
			{
				return WActor.IsFallenOver(this._value);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x0002AEF9 File Offset: 0x000290F9
		public bool isInWater
		{
			get
			{
				return WActor.IsInWater(this._value);
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x0002AF06 File Offset: 0x00029106
		public bool isOnLadder
		{
			get
			{
				return WActor.IsOnLadder(this._value);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x0002AF13 File Offset: 0x00029113
		public bool isParachuteDeployed
		{
			get
			{
				return WActor.IsParachuteDeployed(this._value);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x0002AF20 File Offset: 0x00029120
		public bool isPassenger
		{
			get
			{
				return WActor.IsPassenger(this._value);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x0002AF2D File Offset: 0x0002912D
		public bool isPlayer
		{
			get
			{
				return WActor.IsPlayer(this._value);
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x0002AF3A File Offset: 0x0002913A
		public bool isProne
		{
			get
			{
				return WActor.IsProne(this._value);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x0002AF47 File Offset: 0x00029147
		public bool isSeated
		{
			get
			{
				return WActor.IsSeated(this._value);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x0002AF54 File Offset: 0x00029154
		public bool isSprinting
		{
			get
			{
				return WActor.IsSprinting(this._value);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x0002AF61 File Offset: 0x00029161
		public bool isStanding
		{
			get
			{
				return WActor.IsStanding(this._value);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x0002AF6E File Offset: 0x0002916E
		public bool isSwimming
		{
			get
			{
				return WActor.IsSwimming(this._value);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x0002AF7B File Offset: 0x0002917B
		public bool needsResupply
		{
			get
			{
				return WActor.NeedsResupply(this._value);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x0002AF88 File Offset: 0x00029188
		public bool wasRecentlyInWater
		{
			get
			{
				return WActor.WasRecentlyInWater(this._value);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x0002AF95 File Offset: 0x00029195
		public ScriptEventProxy onTakeDamage
		{
			get
			{
				return ScriptEventProxy.New(WActor.GetOnTakeDamage(this._value));
			}
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x0002AFA7 File Offset: 0x000291A7
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x0012F6A8 File Offset: 0x0012D8A8
		[MoonSharpHidden]
		public static ActorProxy New(Actor value)
		{
			if (value == null)
			{
				return null;
			}
			ActorProxy actorProxy = (ActorProxy)ObjectCache.Get(typeof(ActorProxy), value);
			if (actorProxy == null)
			{
				actorProxy = new ActorProxy(value);
				ObjectCache.Add(typeof(ActorProxy), value, actorProxy);
			}
			return actorProxy;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0002AFAF File Offset: 0x000291AF
		public void Activate()
		{
			WActor.Activate(this._value);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0012F6F4 File Offset: 0x0012D8F4
		public void AddAccessory(MeshProxy mesh, Material[] materials)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			WActor.AddAccessory(this._value, mesh2, materials);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x0002AFBC File Offset: 0x000291BC
		public void ApplyTeamSkin()
		{
			WActor.ApplyTeamSkin(this._value);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x0012F71C File Offset: 0x0012D91C
		public bool CanBeDamagedBy(WeaponProxy weapon)
		{
			Weapon weapon2 = null;
			if (weapon != null)
			{
				weapon2 = weapon._value;
			}
			return WActor.CanBeDamagedBy(this._value, weapon2);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x0012F744 File Offset: 0x0012D944
		public bool CanEnterSeat(SeatProxy seat)
		{
			Seat seat2 = null;
			if (seat != null)
			{
				seat2 = seat._value;
			}
			return WActor.CanEnterSeat(this._value, seat2);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x0002AFC9 File Offset: 0x000291C9
		public void CutParachute()
		{
			WActor.CutParachute(this._value);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x0002AFD6 File Offset: 0x000291D6
		public bool Damage(float health)
		{
			return WActor.Damage(this._value, health);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x0012F76C File Offset: 0x0012D96C
		public bool Damage(ActorProxy source, float health, float balance, bool isSplash, bool isPiercing)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			return WActor.Damage(this._value, source2, health, balance, isSplash, isPiercing);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x0012F798 File Offset: 0x0012D998
		public bool Damage(ActorProxy source, float health, float balance, bool isSplash, bool isPiercing, Vector3Proxy point, Vector3Proxy direction, Vector3Proxy force)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			return WActor.Damage(this._value, source2, health, balance, isSplash, isPiercing, point._value, direction._value, force._value);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x0012F808 File Offset: 0x0012DA08
		public bool Damage(ActorProxy source, DamageInfoProxy info)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			if (info == null)
			{
				throw new ScriptRuntimeException("argument 'info' is nil");
			}
			return WActor.Damage(this._value, source2, info._value);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0002AFE4 File Offset: 0x000291E4
		public void Deactivate()
		{
			WActor.Deactivate(this._value);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0002AFF1 File Offset: 0x000291F1
		public void DeployParachute()
		{
			WActor.DeployParachute(this._value);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0012F844 File Offset: 0x0012DA44
		public bool EnterSeat(SeatProxy seat)
		{
			Seat seat2 = null;
			if (seat != null)
			{
				seat2 = seat._value;
			}
			return WActor.EnterSeat(this._value, seat2);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0012F86C File Offset: 0x0012DA6C
		public void EnterSeatForced(SeatProxy seat)
		{
			Seat seat2 = null;
			if (seat != null)
			{
				seat2 = seat._value;
			}
			WActor.EnterSeatForced(this._value, seat2);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x0012F894 File Offset: 0x0012DA94
		public bool EnterVehicle(VehicleProxy vehicle)
		{
			Vehicle vehicle2 = null;
			if (vehicle != null)
			{
				vehicle2 = vehicle._value;
			}
			return WActor.EnterVehicle(this._value, vehicle2);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x0012F8BC File Offset: 0x0012DABC
		public WeaponProxy EquipNewWeaponEntry(WeaponEntryProxy entry, int slotNumber, bool forceSwitchTo)
		{
			WeaponManager.WeaponEntry entry2 = null;
			if (entry != null)
			{
				entry2 = entry._value;
			}
			return WeaponProxy.New(WActor.EquipNewWeaponEntry(this._value, entry2, slotNumber, forceSwitchTo));
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x0012F8E8 File Offset: 0x0012DAE8
		public Weapon.Difficulty EvaluateShotDifficulty(ActorProxy target, WeaponProxy weapon)
		{
			Actor target2 = null;
			if (target != null)
			{
				target2 = target._value;
			}
			Weapon weapon2 = null;
			if (weapon != null)
			{
				weapon2 = weapon._value;
			}
			return WActor.EvaluateShotDifficulty(this._value, target2, weapon2);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0002AFFE File Offset: 0x000291FE
		public void ExitLadder()
		{
			WActor.ExitLadder(this._value);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0002B00B File Offset: 0x0002920B
		public void ExitVehicle()
		{
			WActor.ExitVehicle(this._value);
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x0002B018 File Offset: 0x00029218
		public void FallOver()
		{
			WActor.FallOver(this._value);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0002B025 File Offset: 0x00029225
		public TransformProxy GetHumanoidTransformAnimated(HumanBodyBones bone)
		{
			return TransformProxy.New(WActor.GetHumanoidTransformAnimated(this._value, bone));
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x0002B038 File Offset: 0x00029238
		public TransformProxy GetHumanoidTransformRagdoll(HumanBodyBones bone)
		{
			return TransformProxy.New(WActor.GetHumanoidTransformRagdoll(this._value, bone));
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0012F91C File Offset: 0x0012DB1C
		public void GetOnLadder(LadderProxy ladder)
		{
			Ladder ladder2 = null;
			if (ladder != null)
			{
				ladder2 = ladder._value;
			}
			WActor.GetOnLadder(this._value, ladder2);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x0002B04B File Offset: 0x0002924B
		public void InstantlyReloadCarriedWeapons()
		{
			WActor.InstantlyReloadCarriedWeapons(this._value);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x0002B058 File Offset: 0x00029258
		public bool IsWeaponUnholstered()
		{
			return WActor.IsWeaponUnholstered(this._value);
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0012F944 File Offset: 0x0012DB44
		public void Kill(ActorProxy killer)
		{
			Actor killer2 = null;
			if (killer != null)
			{
				killer2 = killer._value;
			}
			WActor.Kill(this._value, killer2);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x0002B065 File Offset: 0x00029265
		public void KillSilently()
		{
			WActor.KillSilently(this._value);
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x0002B072 File Offset: 0x00029272
		public void KnockOver(Vector3Proxy force)
		{
			if (force == null)
			{
				throw new ScriptRuntimeException("argument 'force' is nil");
			}
			WActor.KnockOver(this._value, force._value);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x0002B093 File Offset: 0x00029293
		public void RemoveAccessories()
		{
			WActor.RemoveAccessories(this._value);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public void RemoveWeapon(int slotNumber)
		{
			WActor.RemoveWeapon(this._value, slotNumber);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x0002B0AE File Offset: 0x000292AE
		public bool ResupplyAmmo()
		{
			return WActor.ResupplyAmmo(this._value);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0002B0BB File Offset: 0x000292BB
		public bool ResupplyHealth()
		{
			return WActor.ResupplyHealth(this._value);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0002B0C8 File Offset: 0x000292C8
		public void SetHumanoidBoneScale(HumanBodyBones bone, Vector3Proxy scale)
		{
			if (scale == null)
			{
				throw new ScriptRuntimeException("argument 'scale' is nil");
			}
			WActor.SetHumanoidBoneScale(this._value, bone, scale._value);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0002B0EA File Offset: 0x000292EA
		public void SetHumanoidBoneScale(HumanBodyBones bone, float scale)
		{
			WActor.SetHumanoidBoneScale(this._value, bone, scale);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0002B0F9 File Offset: 0x000292F9
		public void SetRagdollJointDrive(float spring, float stiffness)
		{
			WActor.SetRagdollJointDrive(this._value, spring, stiffness);
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0002B108 File Offset: 0x00029308
		public void SetRagdollJointDriveDefault()
		{
			WActor.SetRagdollJointDriveDefault(this._value);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0012F96C File Offset: 0x0012DB6C
		public void SetSkin(ActorSkinProxy actorSkin)
		{
			ActorSkin actorSkin2 = null;
			if (actorSkin != null)
			{
				actorSkin2 = actorSkin._value;
			}
			WActor.SetSkin(this._value, actorSkin2);
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x0012F994 File Offset: 0x0012DB94
		public void SetSkin(MeshProxy mesh, Material[] materials, int teamMaterialIndex)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			WActor.SetSkin(this._value, mesh2, materials, teamMaterialIndex);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x0002B115 File Offset: 0x00029315
		public void SetWeaponParentApproximateWorldScale(float scale)
		{
			WActor.SetWeaponParentApproximateWorldScale(this._value, scale);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0002B123 File Offset: 0x00029323
		public void SetWeaponParentScale(float scale)
		{
			WActor.SetWeaponParentScale(this._value, scale);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0002B131 File Offset: 0x00029331
		public void SpawnAt(Vector3Proxy position, QuaternionProxy rotation)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			WActor.SpawnAt(this._value, position._value, rotation._value);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0002B166 File Offset: 0x00029366
		public void SpawnAt(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			WActor.SpawnAt(this._value, position._value);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0012F9BC File Offset: 0x0012DBBC
		public bool SwapWithSeat(SeatProxy seat)
		{
			Seat seat2 = null;
			if (seat != null)
			{
				seat2 = seat._value;
			}
			return WActor.SwapWithSeat(this._value, seat2);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0012F9E4 File Offset: 0x0012DBE4
		public bool SwitchToSeat(SeatProxy seat)
		{
			Seat seat2 = null;
			if (seat != null)
			{
				seat2 = seat._value;
			}
			return WActor.SwitchToSeat(this._value, seat2);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0002B187 File Offset: 0x00029387
		public void TeleportTo(Vector3Proxy position, QuaternionProxy rotation)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			WActor.TeleportTo(this._value, position._value, rotation._value);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x0002B1BC File Offset: 0x000293BC
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003143 RID: 12611
		[MoonSharpHidden]
		public Actor _value;
	}
}
