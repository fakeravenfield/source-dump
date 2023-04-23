using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009AC RID: 2476
	[Proxy(typeof(AiActorController))]
	public class AiActorControllerProxy : IProxy
	{
		// Token: 0x06003FCA RID: 16330 RVA: 0x0002B258 File Offset: 0x00029458
		[MoonSharpHidden]
		public AiActorControllerProxy(AiActorController value)
		{
			this._value = value;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x0002B267 File Offset: 0x00029467
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x0002B279 File Offset: 0x00029479
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x0002B28B File Offset: 0x0002948B
		// (set) Token: 0x06003FCE RID: 16334 RVA: 0x0002B298 File Offset: 0x00029498
		public bool alwaysChargeTarget
		{
			get
			{
				return WAiActorController.getAlwaysChargeTarget(this._value);
			}
			set
			{
				WAiActorController.SetAlwaysChargeTarget(this._value, value);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x0002B2A6 File Offset: 0x000294A6
		// (set) Token: 0x06003FD0 RID: 16336 RVA: 0x0002B2B3 File Offset: 0x000294B3
		public bool canJoinPlayerSquad
		{
			get
			{
				return WAiActorController.GetCanJoinPlayerSquad(this._value);
			}
			set
			{
				WAiActorController.SetCanJoinPlayerSquad(this._value, value);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x0002B2C1 File Offset: 0x000294C1
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x0002B2CE File Offset: 0x000294CE
		public bool canSprint
		{
			get
			{
				return WAiActorController.GetCanSprint(this._value);
			}
			set
			{
				WAiActorController.SetCanSprint(this._value, value);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x0002B2DC File Offset: 0x000294DC
		public ActorProxy currentAttackTarget
		{
			get
			{
				return ActorProxy.New(WAiActorController.GetCurrentAttackTarget(this._value));
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x0002B2EE File Offset: 0x000294EE
		public Vector3Proxy currentWaypoint
		{
			get
			{
				return Vector3Proxy.New(WAiActorController.getCurrentWaypoint(this._value));
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x0002B300 File Offset: 0x00029500
		public bool hasPath
		{
			get
			{
				return WAiActorController.GetHasPath(this._value);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0002B30D File Offset: 0x0002950D
		public bool hasTargetVehicle
		{
			get
			{
				return WAiActorController.GetHasTargetVehicle(this._value);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x0002B31A File Offset: 0x0002951A
		// (set) Token: 0x06003FD8 RID: 16344 RVA: 0x0002B327 File Offset: 0x00029527
		public bool ignoreFovCheck
		{
			get
			{
				return WAiActorController.GetIgnoreFovCheck(this._value);
			}
			set
			{
				WAiActorController.SetIgnoreFovCheck(this._value, value);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x0002B335 File Offset: 0x00029535
		public bool isDefaultMovementOverridden
		{
			get
			{
				return WAiActorController.getIsDefaultMovementOverridden(this._value);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x0002B342 File Offset: 0x00029542
		public bool isEnteringVehicle
		{
			get
			{
				return WAiActorController.GetIsEnteringVehicle(this._value);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x0002B34F File Offset: 0x0002954F
		public Vector3Proxy lastGotoPoint
		{
			get
			{
				return Vector3Proxy.New(WAiActorController.getLastGotoPoint(this._value));
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x0002B361 File Offset: 0x00029561
		public Vector3Proxy lastWaypoint
		{
			get
			{
				return Vector3Proxy.New(WAiActorController.getLastWaypoint(this._value));
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x0002B373 File Offset: 0x00029573
		// (set) Token: 0x06003FDE RID: 16350 RVA: 0x0002B380 File Offset: 0x00029580
		public float meleeChargeRange
		{
			get
			{
				return WAiActorController.GetMeleeChargeRange(this._value);
			}
			set
			{
				WAiActorController.SetMeleeChargeRange(this._value, value);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0002B38E File Offset: 0x0002958E
		// (set) Token: 0x06003FE0 RID: 16352 RVA: 0x0002B39B File Offset: 0x0002959B
		public AiActorController.SkillLevel skillLevel
		{
			get
			{
				return WAiActorController.GetSkillLevel(this._value);
			}
			set
			{
				WAiActorController.SetSkillLevel(this._value, value);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x0002B3A9 File Offset: 0x000295A9
		// (set) Token: 0x06003FE2 RID: 16354 RVA: 0x0002B3B6 File Offset: 0x000295B6
		public float targetFlightAltitude
		{
			get
			{
				return WAiActorController.getTargetFlightAltitude(this._value);
			}
			set
			{
				WAiActorController.setTargetFlightAltitude(this._value, value);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x0002B3C4 File Offset: 0x000295C4
		public VehicleProxy targetVehicle
		{
			get
			{
				return VehicleProxy.New(WAiActorController.GetTargetVehicle(this._value));
			}
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0002B3D6 File Offset: 0x000295D6
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0012FAC8 File Offset: 0x0012DCC8
		[MoonSharpHidden]
		public static AiActorControllerProxy New(AiActorController value)
		{
			if (value == null)
			{
				return null;
			}
			AiActorControllerProxy aiActorControllerProxy = (AiActorControllerProxy)ObjectCache.Get(typeof(AiActorControllerProxy), value);
			if (aiActorControllerProxy == null)
			{
				aiActorControllerProxy = new AiActorControllerProxy(value);
				ObjectCache.Add(typeof(AiActorControllerProxy), value, aiActorControllerProxy);
			}
			return aiActorControllerProxy;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0002B3DE File Offset: 0x000295DE
		public void CancelPath()
		{
			WAiActorController.CancelPath(this._value);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0002B3EB File Offset: 0x000295EB
		public void Goto(Vector3Proxy destination)
		{
			if (destination == null)
			{
				throw new ScriptRuntimeException("argument 'destination' is nil");
			}
			WAiActorController.Goto(this._value, destination._value);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0012FB14 File Offset: 0x0012DD14
		public void GotoAndEnterVehicle(VehicleProxy vehicle)
		{
			Vehicle vehicle2 = null;
			if (vehicle != null)
			{
				vehicle2 = vehicle._value;
			}
			WAiActorController.GotoAndEnterVehicle(this._value, vehicle2);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x0002B40C File Offset: 0x0002960C
		public void GotoDirect(Vector3Proxy destination)
		{
			if (destination == null)
			{
				throw new ScriptRuntimeException("argument 'destination' is nil");
			}
			WAiActorController.GotoDirect(this._value, destination._value);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x0012FB3C File Offset: 0x0012DD3C
		public bool IsInFOV(ActorProxy targetActor)
		{
			Actor targetActor2 = null;
			if (targetActor != null)
			{
				targetActor2 = targetActor._value;
			}
			return WAiActorController.IsInFOV(this._value, targetActor2);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x0002B42D File Offset: 0x0002962D
		public bool IsInFOV(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WAiActorController.IsInFOV(this._value, position._value);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x0002B44E File Offset: 0x0002964E
		public void LeaveVehicle()
		{
			WAiActorController.LeaveVehicle(this._value);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x0002B45B File Offset: 0x0002965B
		public void OverrideDefaultMovement()
		{
			WAiActorController.OverrideDefaultMovement(this._value);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x0002B468 File Offset: 0x00029668
		public void ReleaseDefaultMovementOverride()
		{
			WAiActorController.ReleaseDefaultMovementOverride(this._value);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x0002B475 File Offset: 0x00029675
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003145 RID: 12613
		[MoonSharpHidden]
		public AiActorController _value;
	}
}
